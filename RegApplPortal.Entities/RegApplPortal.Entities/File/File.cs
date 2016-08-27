using RegApplPortal.Entities.Core;
using System;

namespace RegApplPortal.Entities
{
    public class File : DataObject
    {
        public DateTime AttachmentDate { get; set; }
        public long UserID { get; set; }
        public string FileName {get;set;}
        public string FIleUrl { get; set; }
		public long? NominationID { get; set; }
		public string Comment { get; set; }
		public long? StatementID { get; set; }
		public long? StatementStatusID { get; set; }
    }
}
