using RegApplPortal.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace RegApplPortal.DataAccess.DAO
{
    public class FileTableSet
    {
        private readonly DataTable resultTable;

        public DataTable FileResultTable
        {
            get { return resultTable; }
        }

        public FileTableSet(IEnumerable<File> list)
        {
            resultTable = new DataTable()
            {
                Columns =
                {
                    new DataColumn("FileID", typeof(long)),
                    new DataColumn("AttachmentDate", typeof(DateTime)),
                    new DataColumn("UserID", typeof(long)),
                    new DataColumn("FileName", typeof(string)),
                    new DataColumn("FIleUrl", typeof(string)),
                    new DataColumn("NominationID", typeof(long)),
                    new DataColumn("Comment", typeof(string)),
                    new DataColumn("StatementID", typeof(long)),
                    new DataColumn("StatementStatusID", typeof(long))
                }
            };
            FillTable(list);
        }

        private void FillTable(IEnumerable<File> list)
        {
            foreach (var item in list)
            {
                resultTable.Rows.Add(
                    item.Id,
                    item.AttachmentDate,
                    item.UserID,
                    item.FileName,
                    item.FIleUrl,
                    item.NominationID,
                    item.Comment,
                    item.StatementID,
                    item.StatementStatusID
                    );
            }
        }
    }
}

