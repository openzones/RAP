using System;
using System.Collections.Generic;
namespace RegApplPortal.Entities.Searching
{
    public class StatementSearchCriteria
    {
        public StatementSearchCriteria()
        {
            CountView = 100;
        }

        //Кол-во отображаемых записей в журнале
        public long? CountView { get; set; }

        public long? StatementID { get; set; }
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Lastname { get; set; }
        public DateTime? Birthday { get; set; }

        //Субъект страхования, справочник
        public long? SubjectInsuranceID { get; set; }

        //Дата создания 
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }

        //Дата последнего статуса     
        public DateTime? LastStatusDateFrom { get; set; }
        public DateTime? LastStatusDateTo { get; set; }

        //Последний статус 
        public long? LastStatementStatusID { get; set; }

        //Куратор
        public long? CuratorID { get; set; }

        //Ответственный
        public long? ResponsibleID { get; set; }

		// Причина обращения
		public long? ReasonID { get; set; }

		// формат экспертизы
		public long? ExpertiseID { get; set; }

		//Исполнитель
		public long? ExecutiveID { get; set; }

    }
}
