using RegApplPortal.DataAccess.DAO;
using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities.Report;
using RegApplPortal.Entities.Searching;
using RegApplPortal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegApplPortal.BusinessLogic
{
    public class ReportBusinessLogic : IReportBusinessLogic
    {
        public List<BaseReport> GetBaseReport(StatementSearchCriteria criteria)
        {
            return ReportDao.Instance.GetBaseReport(criteria);
        }
    }
}
