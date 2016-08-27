using RegApplPortal.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegApplPortal.WebApps.Models.PrintedForms
{
    public class BaseReportModel
    {
        #region Constructors
        public BaseReportModel()
        {
            DateBaseReportFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateBaseReportTo = DateTime.Now;
            Statuses = ReferencesProvider.GetReferences(Constants.RefStatus, null, true);
        }
        #endregion

        [DisplayName("Дата создания с")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public DateTime DateBaseReportFrom { get; set; }

        [DisplayName("Дата создания по (включительно)")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public DateTime DateBaseReportTo { get; set; }

        [DisplayName("Дата статуса с")]
        public DateTime? BaseReportStatusDateFrom { get; set; }

        [DisplayName("Дата статуса по (включительно)")]
        public DateTime? BaseReportStatusDateTo { get; set; }

        [DisplayName("Статус")]
        public long? StatusID { get; set; }
        public List<SelectListItem> Statuses { get; set; }
    }
}