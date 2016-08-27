using RegApplPortal.BusinessLogic;
using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities.Searching;
using RegApplPortal.Entities.Sorting;
using RegApplPortal.Entities.Report;
using RegApplPortal.Interfaces;
using RegApplPortal.PrintedForms;
using RegApplPortal.WebApps.Models;
using RegApplPortal.WebApps.Models.PrintedForms;
using RegApplPortal.WebApps.Security;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.Linq;

namespace RegApplPortal.WebApps.Controllers
{
    [AuthorizeUser]
    public class PrintedFormsController : RegApplPortalController
    {

        IReferenceBusinessLogic referenceBusinessLogic;
        IReportBusinessLogic reportBusinessLogic;

        public PrintedFormsController()
        {
            referenceBusinessLogic = new ReferenceBusinessLogic();
            reportBusinessLogic = new ReportBusinessLogic();
        }

        public ActionResult Index()
        {
            PrintedFormsModel model = new PrintedFormsModel();
            return View(model);
        }

        public ActionResult GetBaseReport(BaseReportModel model)
        {
            StatementSearchCriteria criteria = new StatementSearchCriteria();
            criteria.CountView = null;
            criteria.CreateDateFrom = model.DateBaseReportFrom;
            criteria.CreateDateTo = model.DateBaseReportTo.AddDays(1);
            criteria.LastStatusDateFrom = model.BaseReportStatusDateFrom;
            if (model.BaseReportStatusDateTo.HasValue)
            {
                criteria.LastStatusDateTo = model.BaseReportStatusDateTo.Value.AddDays(1);
            }
            criteria.LastStatementStatusID = model.StatusID;

            List<Entities.Report.BaseReport> list = reportBusinessLogic.GetBaseReport(criteria);
            List<ReferenceItem> listSubjectInsurance = ReferencesProvider.GetReferenceItems(Constants.RefSubjectInsurance);
            List<ReferenceItem> listStatus = ReferencesProvider.GetReferenceItems(Constants.RefStatus);
            List<User> listUser = userBusinessLogic.Find(" ");
            PrintedForms.BaseReport printedForm = new PrintedForms.BaseReport(list, listSubjectInsurance, listStatus, listUser);
            return File(printedForm.GetExcel(),
                System.Net.Mime.MediaTypeNames.Application.Octet, "Отчет по обращениям.xlsx");
        }
    }
}