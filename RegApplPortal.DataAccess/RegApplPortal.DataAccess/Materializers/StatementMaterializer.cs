using RegApplPortal.DataAccess.Core;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RegApplPortal.DataAccess.Materializers
{
    public class StatementMaterializer : IMaterializer<Statement>
    {
        private static readonly StatementMaterializer _instance = new StatementMaterializer();

        public static StatementMaterializer Instance
        {
            get
            {
                return _instance;
            }
        }

        public Statement Materialize(DataReaderAdapter reader)
        {
            return Materialize_List(reader).FirstOrDefault();
        }

        public List<Statement> Materialize_List(DataReaderAdapter reader)
        {
            List<Statement> items = new List<Statement>();
            Dictionary<long, Statement> statementsById = new Dictionary<long, Statement>();

            while (reader.Read())
            {
                Statement obj = ReadItemFields(reader);
                statementsById.Add(obj.Id, obj);
                items.Add(obj);
            }

            reader.NextResult();
            while (reader.Read())
            {
                long statementId = reader.GetInt64("StatementId");
                Statement obj = statementsById[statementId];
                obj.StatementStatuses.Add(StatementStatusMaterializer.Instance.ReadItemFields(reader));
            }

            //public List<File> Files
            reader.NextResult();
            while (reader.Read())
            {
                long statementId = reader.GetInt64("StatementId");
                Statement obj = statementsById[statementId];
                obj.Files.Add(FileMaterializer.Instance.ReadItemFields(reader));
            }

            //public Execution Execution { get; set; }
            reader.NextResult();
            while (reader.Read())
            {
                long statementId = reader.GetInt64("StatementId");
                Statement obj = statementsById[statementId];
                obj.Execution = ExecutionMaterializer.Instance.ReadItemFields(reader);
            }

            return items;
        }

        public Statement ReadItemFields(DataReaderAdapter dataReader, Statement item = null)
        {
            if (item == null)
            {
                item = new Statement();
            }
            item.Id = dataReader.GetInt64("ID");
            item.CreateDate = dataReader.GetDateTime("CreateDate");
            item.LastStatementStatusID = dataReader.GetInt64("LastStatementStatusID");
            item.CuratorID = dataReader.GetInt64Null("CuratorID");
            item.ResponsibleID = dataReader.GetInt64Null("ResponsibleID");
            item.ExecutiveID = dataReader.GetInt64Null("ExecutiveID");
            item.LastStatusDate = dataReader.GetDateTimeNull("LastStatusDate");
            item.StatementTypeID = dataReader.GetInt64("StatementTypeID");
            item.Lastname = dataReader.GetString("Lastname");
            item.Firstname = dataReader.GetString("Firstname");
            item.Secondname = dataReader.GetString("Secondname");
            item.Birthday = dataReader.GetDateTimeNull("Birthday");
            item.Sex = dataReader.GetString("Sex");
            item.Phone = dataReader.GetString("Phone");
            item.Email = dataReader.GetString("Email");
            item.ClientID = dataReader.GetInt64Null("ClientID");
            item.VisitGroupID = dataReader.GetInt64Null("VisitGroupID");
            item.MedDocumentTypeID = dataReader.GetInt64Null("MedDocumentTypeID");
            item.Series = dataReader.GetString("Series");
            item.Number = dataReader.GetString("Number");
			item.ReasonID = dataReader.GetInt64Null("ReasonID");
			item.UnifiedPolicyNumber = dataReader.GetString("UnifiedPolicyNumber");
            item.SubjectInsuranceID  = dataReader.GetInt64Null("SubjectInsuranceID") ?? default(long);
            item.LocalityID  = dataReader.GetInt64Null("LocalityID");
            item.IncidentDate = dataReader.GetDateTime("IncidentDate");
            item.Description = dataReader.GetString("Description");
			item.ExpertiseID = dataReader.GetInt64Null("ExpertiseID");
			item.IncomingChannelID  = dataReader.GetInt64Null("IncomingСhannelID") ?? default(long);
            item.UpdateDate = dataReader.GetDateTimeNull("UpdateDate");
            return item;
        }
    }
}
