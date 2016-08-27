using RegApplPortal.Entities.Core;
using System;

namespace RegApplPortal.Entities
{
    public class Execution : DataObject
    {
        public long StatementID { get; set; }
		public bool Validity { get; set; }
		public bool Judicial { get; set; }
		public DateTime? ExpertiseDate { get; set; }
        public float? FinancialSanctions  { get; set; }
        public float? Straf { get; set; }        
        public string DescriptionExecution { get; set; }
        public string LPU_Code { get; set; }
        public string LPU_Name { get; set; }
    }
}
