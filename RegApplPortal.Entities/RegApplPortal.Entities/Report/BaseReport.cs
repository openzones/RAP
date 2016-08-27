using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegApplPortal.Entities.Report
{
    public class BaseReport : StatementInfo
    {
        public string SubjectInsuranceName { get; set; }
        public string LastStatusName { get; set; }
        public string CuratorFIO { get; set; }
        public string ResponsibleFIO { get; set; }
        public string ExecutiveFIO { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public long? VisitGroupID { get; set; }
        public long? MedDocumentTypeID { get; set; }
        public string MedDocumentTypeName { get; set; }
        public string UnifiedPolicyNumber { get; set; }
        public long? LocalityID { get; set; }
        public string LocalityName { get; set; }
        public DateTime? IncidentDate { get; set; }
        public string Description { get; set; }
        public long? IncomingСhannelID { get; set; }
        public string IncomingСhannelName { get; set; }
        public long? AuthorID { get; set; }
        public string AuthorFIO { get; set; }
        public string StatusComment { get; set; }
        public long? AssignedToUserID { get; set; }
        public string AssignedToUserFIO { get; set; }
        public DateTime? ExecuteToDate { get; set; }
        public bool Validity { get; set; }
        public bool Judicial { get; set; }
        public long? ExpertiseID { get; set; }
        public string ExpertiseName { get; set; }
        public DateTime? ExpertiseDate { get; set; }
        public float? FinancialSanctions { get; set; }
        public float? Straf { get; set; }
        public long? ReasonID { get; set; }
        public string ReasonName { get; set; }
        public string DescriptionExecution { get; set; }
        public string LPU_Code { get; set; }
        public string LPU_Name { get; set; }


    }
}
