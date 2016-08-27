using RegApplPortal.DataAccess.Core;
using RegApplPortal.DataAccess.Core.Helpers;
using RegApplPortal.DataAccess.Materializers;
using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities.Report;
using RegApplPortal.Entities.Searching;
using RegApplPortal.Entities.Sorting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RegApplPortal.DataAccess.DAO
{
    public class ReportDao : ItemDao
    {
        private static ReportDao _instance = new ReportDao();

        private ReportDao()
            : base(DatabaseAliases.RegApplPortal, new DatabaseErrorHandler())
        {
        }

        public static ReportDao Instance
        {
            get
            {
                return _instance;
            }
        }

        public List<BaseReport> GetBaseReport(StatementSearchCriteria criteria)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@CountView", SqlDbType.BigInt, criteria.CountView);
            parameters.AddInputParameter("@StatementID", SqlDbType.BigInt, criteria.StatementID);
            parameters.AddInputParameter("@Firstname", SqlDbType.NVarChar, criteria.Firstname);
            parameters.AddInputParameter("@Secondname", SqlDbType.NVarChar, criteria.Secondname);
            parameters.AddInputParameter("@Lastname", SqlDbType.NVarChar, criteria.Lastname);
            parameters.AddInputParameter("@Birthday", SqlDbType.Date, criteria.Birthday);
            parameters.AddInputParameter("@SubjectInsuranceID", SqlDbType.BigInt, criteria.SubjectInsuranceID);
            parameters.AddInputParameter("@CreateDateFrom", SqlDbType.Date, criteria.CreateDateFrom);
            parameters.AddInputParameter("@CreateDateTo", SqlDbType.Date, criteria.CreateDateTo);
            parameters.AddInputParameter("@LastStatusDateFrom", SqlDbType.Date, criteria.LastStatusDateFrom);
            parameters.AddInputParameter("@LastStatusDateTo", SqlDbType.Date, criteria.LastStatusDateTo);
            parameters.AddInputParameter("@LastStatementStatusID", SqlDbType.BigInt, criteria.LastStatementStatusID);
            parameters.AddInputParameter("@CuratorID", SqlDbType.BigInt, criteria.CuratorID);
            parameters.AddInputParameter("@ResponsibleID", SqlDbType.BigInt, criteria.ResponsibleID);
            parameters.AddInputParameter("@ExecutiveID", SqlDbType.BigInt, criteria.ExecutiveID);

            List<BaseReport> result = Execute_GetList(BaseReportMaterializer.Instance, "report.BaseReport", parameters);
            //List<BaseReport> result = Execute_GetList(BaseReportMaterializer.Instance, "report.BaseReport", parameters);
            return result;
        }

    }
}
