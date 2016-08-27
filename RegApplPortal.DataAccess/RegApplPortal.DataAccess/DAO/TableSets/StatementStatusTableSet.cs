using RegApplPortal.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace RegApplPortal.DataAccess.DAO
{
    public class StatementStatusTableSet
    {
        private readonly DataTable resultTable;

        public DataTable StatementStatusResultTable
        {
            get { return resultTable; }
        }

        public StatementStatusTableSet(IEnumerable<StatementStatus> list)
        {
            resultTable = new DataTable()
            {
                Columns =
                {
                    new DataColumn("ID", typeof(long)),
                    new DataColumn("StatementID", typeof(long)),
                    new DataColumn("UserID", typeof(long)),
                    new DataColumn("StatusDate", typeof(DateTime)),
                    new DataColumn("StatusID", typeof(long)),
                    new DataColumn("Comment", typeof(string)),
                    new DataColumn("AssignedToUserID", typeof(long)),
                    new DataColumn("ExecuteToDate", typeof(DateTime))
                }
            };
            FillTable(list);
        }

        private void FillTable(IEnumerable<StatementStatus> list)
        {
            foreach (var item in list)
            {
                resultTable.Rows.Add(
                    item.Id,
                    item.StatementID,
                    item.UserID,
                    item.StatusDate,
                    item.StatusID,
                    item.Comment,
                    item.AssignedToUserID,
                    item.ExecuteToDate
                    );
            }
        }
    }

}
