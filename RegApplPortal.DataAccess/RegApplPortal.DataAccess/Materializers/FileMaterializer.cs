using RegApplPortal.DataAccess.Core;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RegApplPortal.DataAccess.Materializers
{
    public class FileMaterializer : IMaterializer<File>
    {
        private static readonly FileMaterializer _instance = new FileMaterializer();

        public static FileMaterializer Instance
        {
            get
            {
                return _instance;
            }
        }

        public File Materialize(DataReaderAdapter reader)
        {
            return Materialize_List(reader).FirstOrDefault();
        }

        public List<File> Materialize_List(DataReaderAdapter reader)
        {
            List<File> items = new List<File>();

            while (reader.Read())
            {
                File obj = ReadItemFields(reader);
                items.Add(obj);
            }

            return items;
        }

        public File ReadItemFields(DataReaderAdapter dataReader, File item = null)
        {
            if (item == null)
            {
                item = new File();
            }
            item.Id = dataReader.GetInt64("FileID");
            item.StatementID = dataReader.GetInt64Null("StatementID");
            item.StatementStatusID = dataReader.GetInt64Null("StatementStatusID");
            item.AttachmentDate = dataReader.GetDateTime("AttachmentDate");
            item.UserID = dataReader.GetInt64("UserID");
            item.FileName = dataReader.GetString("FileName");
            item.FIleUrl = dataReader.GetString("FIleUrl");
            item.NominationID = dataReader.GetInt64Null("NominationID");
            item.Comment = dataReader.GetString("Comment");
            return item;
        }
    }
}
