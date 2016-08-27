using RegApplPortal.Entities.Core;
using RegApplPortal.Entities.Validation;
using System;
using System.Collections.Generic;

namespace RegApplPortal.Entities
{
    public class Statement : ValidatableModel<Statement>
    {
        public Statement()
        {
            StatementStatuses = new List<StatementStatus>();
            Files = new List<File>();
            this.Execution = new Execution();
            CreateDate = DateTime.Now;
			UpdateDate = DateTime.Now;
			IncidentDate = DateTime.Now;
            validator = new StatementValidator();
			Execution = new Execution();
        }

        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public long LastStatementStatusID { get; set; }

        /// <summary>
        /// Куратор
        /// </summary>
        public long? CuratorID { get; set; }

        /// <summary>
        /// Ответственный
        /// </summary>
        public long? ResponsibleID { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public long? ExecutiveID { get; set; }
        public DateTime? LastStatusDate { get; set; }
        public long StatementTypeID { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public DateTime? Birthday { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
		public long? ExpertiseID { get; set; }
		public string Email { get; set; }
        public long? ClientID { get; set; }
        public long? VisitGroupID { get; set; }
        public long? MedDocumentTypeID { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public string UnifiedPolicyNumber { get; set; }
        public long SubjectInsuranceID { get; set; }
        public long? LocalityID { get; set; }
        public DateTime IncidentDate { get; set; }
        public string Description { get; set; }
        public long IncomingChannelID { get; set; }
		public long? ReasonID { get; set; } 
        public DateTime? UpdateDate { get; set; }

        public List<StatementStatus> StatementStatuses { get; set; }
        public List<File> Files { get; set; }
        public Execution Execution { get;set;}
    }
}
