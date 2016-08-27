using RegApplPortal.DataAccess.Core;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities.Report;
using RegApplPortal.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RegApplPortal.DataAccess.Materializers
{
    public class BaseReportMaterializer : IMaterializer<BaseReport>
    {
        private static readonly BaseReportMaterializer _instance = new BaseReportMaterializer();

        public static BaseReportMaterializer Instance
        {
            get
            {
                return _instance;
            }
        }

        public BaseReport Materialize(DataReaderAdapter reader)
        {
            return Materialize_List(reader).FirstOrDefault();
        }

        public List<BaseReport> Materialize_List(DataReaderAdapter reader)
        {
            List<BaseReport> items = new List<BaseReport>();

            while (reader.Read())
            {
                BaseReport obj = ReadItemFields(reader);
                items.Add(obj);
            }

            return items;
        }

        public BaseReport ReadItemFields(DataReaderAdapter dataReader, BaseReport item = null)
        {
            if (item == null)
            {
                item = new BaseReport();
            }
            item.Id = dataReader.GetInt64("ID");
            item.Firstname = dataReader.GetString("FirstName");
            item.Secondname = dataReader.GetString("Secondname");
            item.Lastname = dataReader.GetString("Lastname");
            item.Birthday = dataReader.GetDateTimeNull("Birthday");
            item.SubjectInsuranceID = dataReader.GetInt64Null("SubjectInsuranceID");
            item.CreateDate = dataReader.GetDateTime("CreateDate");
            item.LastStatusDate = dataReader.GetDateTimeNull("LastStatusDate");
            item.ClientID = dataReader.GetInt64Null("ClientID");
            item.LastStatementStatusID = dataReader.GetInt64Null("LastStatementStatusID");
            item.CuratorID = dataReader.GetInt64Null("CuratorID");
            item.ResponsibleID = dataReader.GetInt64Null("ResponsibleID");
            item.ExecutiveID = dataReader.GetInt64Null("ExecutiveID");

            item.SubjectInsuranceName = dataReader.GetString("SubjectInsuranceName");
            item.LastStatusName = dataReader.GetString("LastStatusName");
            item.CuratorFIO = dataReader.GetString("CuratorFIO");
            item.ResponsibleFIO = dataReader.GetString("ResponsibleFIO");
            item.ExecutiveFIO = dataReader.GetString("ExecutiveFIO");
            item.Phone = dataReader.GetString("Phone");
            item.Email = dataReader.GetString("Email");
            item.VisitGroupID = dataReader.GetInt64Null("VisitGroupID");
            item.MedDocumentTypeID = dataReader.GetInt64Null("MedDocumentTypeID");
            item.MedDocumentTypeName = dataReader.GetString("MedDocumentTypeName");
            item.UnifiedPolicyNumber = dataReader.GetString("UnifiedPolicyNumber");
            item.LocalityID = dataReader.GetInt64Null("LocalityID");
            item.LocalityName = dataReader.GetString("LocalityName");
            item.IncidentDate = dataReader.GetDateTimeNull("IncidentDate");
            item.Description = dataReader.GetString("Description");
            item.IncomingСhannelID = dataReader.GetInt64Null("IncomingСhannelID");
            item.IncomingСhannelName = dataReader.GetString("IncomingСhannelName");
            item.AuthorID = dataReader.GetInt64Null("AuthorID");
            item.AuthorFIO = dataReader.GetString("AuthorFIO");
            item.StatusComment = dataReader.GetString("StatusComment");
            item.AssignedToUserID = dataReader.GetInt64Null("AssignedToUserID");
            item.AssignedToUserFIO = dataReader.GetString("AssignedToUserFIO");
            item.ExecuteToDate = dataReader.GetDateTimeNull("ExecuteToDate");
            item.Validity = dataReader.GetBoolean("Validity");
            item.Judicial = dataReader.GetBoolean("Judicial");
            item.ExpertiseID = dataReader.GetInt64Null("ExpertiseID");
            item.ExpertiseName = dataReader.GetString("ExpertiseName");
            item.ExpertiseDate = dataReader.GetDateTimeNull("ExpertiseDate");
            item.FinancialSanctions = dataReader.GetFloatNull("FinancialSanctions");
            item.Straf = dataReader.GetFloatNull("Straf");
            item.ReasonID = dataReader.GetInt64Null("ReasonID");
            item.ReasonName = dataReader.GetString("ReasonName");
            item.DescriptionExecution = dataReader.GetString("DescriptionExecution");
            item.LPU_Code = dataReader.GetString("LPU_Code");
            item.LPU_Name = dataReader.GetString("LPU_Name");

            return item;
        }

    }
}
