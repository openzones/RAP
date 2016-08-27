using RegApplPortal.DataAccess.Core;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RegApplPortal.DataAccess.Materializers
{
    public class StatementInfoMaterializer : IMaterializer<StatementInfo>
    {
        private static readonly StatementInfoMaterializer _instance = new StatementInfoMaterializer();

        public static StatementInfoMaterializer Instance
        {
            get
            {
                return _instance;
            }
        }

        public StatementInfo Materialize(DataReaderAdapter reader)
        {
            return Materialize_List(reader).FirstOrDefault();
        }

        public List<StatementInfo> Materialize_List(DataReaderAdapter reader)
        {
            List<StatementInfo> items = new List<StatementInfo>();

            while (reader.Read())
            {
                StatementInfo obj = ReadItemFields(reader);
                items.Add(obj);
            }

            return items;
        }

        public StatementInfo ReadItemFields(DataReaderAdapter dataReader, StatementInfo item = null)
        {
            if (item == null)
            {
                item = new StatementInfo();
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
			item.ReasonID = dataReader.GetInt64Null("ReasonID");
			item.ExpertiseID = dataReader.GetInt64Null("ExpertiseID");			
            item.ExecutiveID = dataReader.GetInt64Null("ExecutiveID");

            return item;
        }
    }
}
