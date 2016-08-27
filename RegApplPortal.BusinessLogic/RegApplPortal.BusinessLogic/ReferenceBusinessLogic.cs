using RegApplPortal.DataAccess.DAO;
using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using RegApplPortal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegApplPortal.BusinessLogic
{
    public class ReferenceBusinessLogic : IReferenceBusinessLogic
    {
        /// <summary>
        /// Получаем справочник через "универсальный" метод, 
        /// </summary>
        /// <param name="referenceName"></param>
        /// <returns></returns>
        public List<ReferenceUniversalItem> GetUniversalList(string referenceName)
        {
            return ReferencesDao.Instance.GetUniversalList(referenceName);
        }

        /// <summary>
        /// Сохраняем запись в справочник
        /// </summary>
        /// <param name="item"></param>
        /// <param name="referenceName"></param>
        /// <param name="flagUpdateOrInsert"></param>
        public void SaveUniversalReferenceItem(ReferenceUniversalItem item, string referenceName, bool flagUpdateOrInsert = false)
        {
            ReferencesDao.Instance.SaveUniversalReferenceItem(item, referenceName, flagUpdateOrInsert);
        }

        /// <summary>
        /// Удаляем запись по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="referenceName"></param>
        public void DeleteReferenceItem(long id, string referenceName)
        {
            ReferencesDao.Instance.DeleteReferenceItem(id, referenceName);
        }

        /// <summary>
        /// Returns a list of specified reference
        /// </summary>
        /// <param name="referenceName">A name of reference</param>
        /// <returns>list of Reference's items</returns>
        public List<ReferenceItem> GetReferencesList(string referenceName)
        {
            return ReferencesDao.Instance.GetList(referenceName);
        }

        /// <summary>
        /// Returns a list of holidays 
        /// </summary>
        public HashSet<DateTime> GetHolidays(int? year)
        {
            return ReferencesDao.Instance.GetHolidays(year);
        }

        /// <summary>
        /// Returns a list of exceptional working days
        /// </summary>
        public HashSet<DateTime> GetExceptionalWorkingDays(int? year)
        {
            return ReferencesDao.Instance.GetExceptionalWorkingDays(year);
        }

		/// <summary>
		/// Returns a list of directories
		/// </summary>
		public List<ReferenceUniversalItem> GetAllReference()
		{
			return ReferencesDao.Instance.GetAllReference();
		}
	}
}
