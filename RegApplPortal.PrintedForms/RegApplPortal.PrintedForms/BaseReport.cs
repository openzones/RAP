using NPOI.SS.UserModel;
using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using RegApplPortal.PrintedForms.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RegApplPortal.PrintedForms
{
    public class BaseReport : PrintedForm
    {
        private List<Entities.Report.BaseReport> ListStatement;
        private List<ReferenceItem> ListSubjectInsurance;
        private List<ReferenceItem> ListStatus;
        private List<User> ListUser;

        #region Marks
        protected const string title = "$Title$";
        #endregion

        public BaseReport(  List<Entities.Report.BaseReport> listStatement,
                            List<ReferenceItem> listSubjectInsurance,
                            List<ReferenceItem> listStatus,
                            List<User> listUser)
        {
            ListStatement = listStatement;
            ListSubjectInsurance = listSubjectInsurance;
            ListStatus = listStatus;
            ListUser = listUser;
            TemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "Templates", "BaseReport.xlsx");
        }

        protected override void Process(ISheet table)
        {
            if (ListStatement == null || ListStatement.Count == 0)
            {
                table.FindCellByMacros(title).SetValue(GetNotNullString("Отчет по обращениям. Данных нет!"));
                return;
            }
            table.FindCellByMacros(title).SetValue(GetNotNullString(string.Format("Отчет по обращениям.")));
            int rowNumber = 3;
            for (int i = 0; i < ListStatement.Count; i++)   //можно и через foreach
            {
                var item = ListStatement[i];
                IRow row = table.CreateRow(rowNumber);
                row.CreateCell(0).SetValue(GetNotNullString((i + 1).ToString()));
                row.CreateCell(1).SetCellValue(item.Id);
                row.CreateCell(2).SetValue(GetNotNullString(item.Lastname));
                row.CreateCell(3).SetValue(GetNotNullString(item.Firstname));
                row.CreateCell(4).SetValue(GetNotNullString(item.Secondname));
                row.CreateCell(5).SetValue(GetNotNullString(item.Phone));
                row.CreateCell(6).SetValue(GetNotNullString(item.Email));
                row.CreateCell(7).SetValue(GetNotNullString(item.SubjectInsuranceName));
                row.CreateCell(8).SetValue(GetDateString(item.CreateDate, "dd.MM.yyyy")); //dd.MM.yyyy H:mm:ss
                row.CreateCell(9).SetValue(GetNotNullString(item.LastStatusName));
                row.CreateCell(10).SetValue(GetDateString(item.LastStatusDate, "dd.MM.yyyy"));
                row.CreateCell(11).SetValue(GetNotNullString(item.ClientID.ToString()));
                row.CreateCell(12).SetValue(GetNotNullString(item.VisitGroupID.ToString()));
                row.CreateCell(13).SetValue(GetNotNullString(item.CuratorFIO));
                row.CreateCell(14).SetValue(GetNotNullString(item.ResponsibleFIO));
                row.CreateCell(15).SetValue(GetNotNullString(item.ExecutiveFIO));
                row.CreateCell(16).SetValue(GetNotNullString(item.MedDocumentTypeName));
                row.CreateCell(17).SetValue(GetNotNullString(item.UnifiedPolicyNumber));
                row.CreateCell(18).SetValue(GetNotNullString(item.LocalityName));
                row.CreateCell(19).SetValue(GetDateString(item.IncidentDate, "dd.MM.yyyy"));
                row.CreateCell(20).SetValue(GetNotNullString(item.Description));
                row.CreateCell(21).SetValue(GetNotNullString(item.IncomingСhannelName));
                row.CreateCell(22).SetValue(GetNotNullString(item.AuthorFIO));
                row.CreateCell(23).SetValue(GetNotNullString(item.StatusComment));
                row.CreateCell(24).SetValue(GetNotNullString(item.AssignedToUserFIO));
                row.CreateCell(25).SetValue(GetDateString(item.ExecuteToDate, "dd.MM.yyyy"));
                row.CreateCell(26).SetValue(GetNotNullString(item.ExpertiseName));
                row.CreateCell(27).SetValue(GetDateString(item.ExpertiseDate, "dd.MM.yyyy"));
                if (item.FinancialSanctions.HasValue)
                    row.CreateCell(28).SetCellValue((double)item.FinancialSanctions);
                if (item.Straf.HasValue)
                    row.CreateCell(29).SetCellValue((double)item.Straf);
                row.CreateCell(30).SetValue(GetNotNullString(item.ReasonName));
                row.CreateCell(31).SetValue(GetNotNullString(item.DescriptionExecution));
                row.CreateCell(32).SetValue(GetNotNullString(item.LPU_Code));
                row.CreateCell(33).SetValue(GetNotNullString(item.LPU_Name));

                rowNumber++;
            }
        }
    }
}
