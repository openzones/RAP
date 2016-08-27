using RegApplPortal.DataAccess.Core;
using RegApplPortal.DataAccess.Materializers;
using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RegApplPortal.DataAccess.DAO
{
    public class ReferencesDao : ItemDao
    {
        private static ReferencesDao _instance = new ReferencesDao();

        private ReferencesDao()
            : base(DatabaseAliases.RegApplPortal, new DatabaseErrorHandler())
        {
        }

        public static ReferencesDao Instance
        {
            get
            {
                return _instance;
            }
        }

        public List<ReferenceItem> GetList(string referenceName)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@ReferenceName", SqlDbType.NVarChar, referenceName);
            List<ReferenceItem> items = Execute_GetList<ReferenceItem>(ReferencesMaterializer.Instance, "Reference_GetList", parameters);
            return items;
        }

        public List<ReferenceUniversalItem> GetUniversalList(string referenceName)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@ReferenceName", SqlDbType.NVarChar, referenceName);
            List<ReferenceUniversalItem> items = Execute_GetList<ReferenceUniversalItem>(ReferenceUniversalItemMaterializer.Instance, "Reference_GetUniversalList", parameters);
            return items;
        }

        public void SaveUniversalReferenceItem(ReferenceUniversalItem item, string referenceName, bool flagUpdateOrInsert = false)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@ReferenceName", SqlDbType.NVarChar, referenceName);
            parameters.AddInputParameter("@FlagUpdateOrInsert", SqlDbType.Bit, flagUpdateOrInsert);

            parameters.AddInputParameter("@ID", SqlDbType.BigInt, item.Id);
            parameters.AddInputParameter("@Name", SqlDbType.NVarChar, item.Name);
            parameters.AddInputParameter("@DisplayName", SqlDbType.NVarChar, item.DisplayName);
            parameters.AddInputParameter("@Code", SqlDbType.NVarChar, item.Code);

            //parameters.AddInputParameter("@Lastname", SqlDbType.NVarChar, item.Lastname);
            //parameters.AddInputParameter("@Firstname", SqlDbType.NVarChar, item.Firstname);
            //parameters.AddInputParameter("@Secondname", SqlDbType.NVarChar, item.Secondname);

            Execute_StoredProcedure("Reference_SaveItem", parameters);
        }

        public void DeleteReferenceItem(long id, string referenceName)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@ReferenceName", SqlDbType.NVarChar, referenceName);
            parameters.AddInputParameter("@ID", SqlDbType.BigInt, id);
            Execute_StoredProcedure("Reference_DeleteItem", parameters);
        }

        public HashSet<DateTime> GetHolidays(int? year)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@TargetYear", SqlDbType.Int, year);
            HashSet<DateTime> dates = new HashSet<DateTime>();
            Execute_Reader("Holidays_Get", parameters,
                 (reader) =>
                 {
                     while (reader.Read())
                     {
                         dates.Add(reader.GetDateTime("Date"));
                     }
                 });
            return dates;
        }

        public HashSet<DateTime> GetExceptionalWorkingDays(int? year)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@TargetYear", SqlDbType.Int, year);
            HashSet<DateTime> dates = new HashSet<DateTime>();
            Execute_Reader("WorkingDates_Get", parameters,
                 (reader) =>
                 {
                     while (reader.Read())
                     {
                         dates.Add(reader.GetDateTime("Date"));
                     }
                 });
            return dates;
        }
		public List<ReferenceUniversalItem> GetAllReference()
		{
			return Execute_GetList(ReferenceGetAllMaterializer.Instance, "Reference_GetAll", null);
		}
	}
}
