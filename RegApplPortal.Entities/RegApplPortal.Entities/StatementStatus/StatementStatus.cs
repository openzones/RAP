using RegApplPortal.Entities.Core;
using System;

namespace RegApplPortal.Entities
{
    public class StatementStatus : DataObject
    {
		public StatementStatus()
		{
			StatusDate = DateTime.Now; 
        }
        public long StatementID { get; set; }
        public long UserID { get; set; }
        public DateTime StatusDate { get; set; }
        public long StatusID { get; set; }
        public string Comment { get; set; }
        public long? AssignedToUserID { get; set; }
        public DateTime? ExecuteToDate { get; set; }
    }
}
