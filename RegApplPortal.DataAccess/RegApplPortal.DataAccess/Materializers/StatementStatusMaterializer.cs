using RegApplPortal.DataAccess.Core;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RegApplPortal.DataAccess.Materializers
{
    public class StatementStatusMaterializer : IMaterializer<StatementStatus>
    {
        private static readonly StatementStatusMaterializer _instance = new StatementStatusMaterializer();

        public static StatementStatusMaterializer Instance
        {
            get
            {
                return _instance;
            }
        }

        public StatementStatus Materialize(DataReaderAdapter reader)
        {
            return Materialize_List(reader).FirstOrDefault();
        }

        public List<StatementStatus> Materialize_List(DataReaderAdapter reader)
        {
            List<StatementStatus> items = new List<StatementStatus>();

            while (reader.Read())
            {
                StatementStatus obj = ReadItemFields(reader);
                items.Add(obj);
            }

            return items;
        }

        public StatementStatus ReadItemFields(DataReaderAdapter dataReader, StatementStatus item = null)
        {
            if (item == null)
            {
                item = new StatementStatus();
            }
            item.Id = dataReader.GetInt64("ID");
            item.StatementID = dataReader.GetInt64("StatementID");
            item.UserID = dataReader.GetInt64("UserID");
            item.StatusDate = dataReader.GetDateTime("StatusDate");
            item.StatusID = dataReader.GetInt64("StatusID");
            item.Comment = dataReader.GetString("Comment");
            item.AssignedToUserID = dataReader.GetInt64Null("AssignedToUserID");
            item.ExecuteToDate = dataReader.GetDateTimeNull("ExecuteToDate");
            return item;
        }
    }
}
