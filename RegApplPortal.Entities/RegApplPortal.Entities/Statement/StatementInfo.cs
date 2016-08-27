using RegApplPortal.Entities.Core;
using System;

namespace RegApplPortal.Entities
{
	public class StatementInfo : DataObject
	{
		//Срок обработки	Флаг, или выделение записи.	
		//Срок на ответственном Флаг, или выделение записи.

		//Номер
		//long Id в DataObject

		//ФИО     Да
		public string Firstname { get; set; }
		public string Secondname { get; set; }
		public string Lastname { get; set; }
		public DateTime? Birthday { get; set; }

		//Субъект страхования, справочник
		public long? SubjectInsuranceID { get; set; }

		//Дата создания 
		public DateTime CreateDate { get; set; }

		//Дата последнего статуса     
		public DateTime? LastStatusDate { get; set; }

		//ID клиента на портале   
		public long? ClientID { get; set; }

		//Последний статус 
		public long? LastStatementStatusID { get; set; }

		//Куратор Из статуса
		public long? CuratorID { get; set; }

		//Ответственный   Из статуса.	
		public long? ResponsibleID { get; set; }

		// Причина обращения
		public long? ReasonID { get; set; }

		// формат экспертизы
		public long? ExpertiseID { get; set; }

		//Исполнитель Из статуса.	
		public long? ExecutiveID { get; set; }
    }
}
