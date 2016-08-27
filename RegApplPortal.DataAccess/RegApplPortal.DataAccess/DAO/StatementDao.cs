using RegApplPortal.DataAccess.Core;
using RegApplPortal.DataAccess.Core.Helpers;
using RegApplPortal.DataAccess.Materializers;
using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities.Searching;
using RegApplPortal.Entities.Sorting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RegApplPortal.DataAccess.DAO
{
	public class StatementDao : ItemDao
	{
		private static StatementDao _instance = new StatementDao();

		private StatementDao()
            : base(DatabaseAliases.RegApplPortal, new DatabaseErrorHandler())
        {
		}

		public static StatementDao Instance
		{
			get
			{
				return _instance;
			}
		}

        public List<StatementInfo> Statement_Find(StatementSearchCriteria criteria)
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
			parameters.AddInputParameter("@ReasonID", SqlDbType.BigInt, criteria.ReasonID);
			parameters.AddInputParameter("@ExpertiseID", SqlDbType.BigInt, criteria.ExpertiseID);			
            parameters.AddInputParameter("@ExecutiveID", SqlDbType.BigInt, criteria.ExecutiveID);
            //SqlParameter totalCountParameter = parameters.AddOutputParameter("@total_count", SqlDbType.Int);
            //parameters.AddInputParameter("@sort_criteria", SqlDbType.Structured, DaoHelper.GetSortFieldsTable(sortCriteria));
            //parameters.AddInputParameter("@Page_size", SqlDbType.Int, pageRequest.PageSize);
            //parameters.AddInputParameter("@Page_number", SqlDbType.Int, pageRequest.PageNumber);
            List<StatementInfo> result = Execute_GetList(StatementInfoMaterializer.Instance, "Statement_Find", parameters);
            return result;
        }


        public long Statement_Save(Statement statement)
		{
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@CreateDate", SqlDbType.DateTime, statement.CreateDate);
            parameters.AddInputParameter("@LastStatementStatusID", SqlDbType.BigInt, statement.LastStatementStatusID);
            parameters.AddInputParameter("@CuratorID", SqlDbType.BigInt, statement.CuratorID);
            parameters.AddInputParameter("@ResponsibleID", SqlDbType.BigInt, statement.ResponsibleID);
            parameters.AddInputParameter("@ExecutiveID", SqlDbType.BigInt, statement.ExecutiveID);
            parameters.AddInputParameter("@LastStatusDate", SqlDbType.DateTime, statement.LastStatusDate);
            parameters.AddInputParameter("@StatementTypeID", SqlDbType.BigInt, statement.StatementTypeID);
            parameters.AddInputParameter("@Lastname", SqlDbType.NVarChar, statement.Lastname);
            parameters.AddInputParameter("@Firstname", SqlDbType.NVarChar, statement.Firstname);
            parameters.AddInputParameter("@Secondname", SqlDbType.NVarChar, statement.Secondname);
            parameters.AddInputParameter("@Birthday", SqlDbType.DateTime, statement.Birthday);
            parameters.AddInputParameter("@Sex", SqlDbType.NVarChar, statement.Sex);
            parameters.AddInputParameter("@Phone", SqlDbType.NVarChar, statement.Phone);
            parameters.AddInputParameter("@Email", SqlDbType.NVarChar, statement.Email);
            parameters.AddInputParameter("@ClientID", SqlDbType.BigInt, statement.ClientID);
			parameters.AddInputParameter("@ReasonID", SqlDbType.BigInt, statement.ReasonID);
			parameters.AddInputParameter("@VisitGroupID", SqlDbType.BigInt, statement.VisitGroupID);
            parameters.AddInputParameter("@MedDocumentTypeID", SqlDbType.BigInt, statement.MedDocumentTypeID);
            parameters.AddInputParameter("@Series", SqlDbType.NVarChar, statement.Series);
            parameters.AddInputParameter("@Number", SqlDbType.NVarChar, statement.Number);
            parameters.AddInputParameter("@UnifiedPolicyNumber", SqlDbType.NVarChar, statement.UnifiedPolicyNumber);
            parameters.AddInputParameter("@SubjectInsuranceID", SqlDbType.BigInt, statement.SubjectInsuranceID);
            parameters.AddInputParameter("@LocalityID", SqlDbType.BigInt, statement.LocalityID);
            parameters.AddInputParameter("@IncidentDate", SqlDbType.DateTime, statement.IncidentDate);
            parameters.AddInputParameter("@Description", SqlDbType.NVarChar, statement.Description);
			parameters.AddInputParameter("@ExpertiseID", SqlDbType.NVarChar, statement.ExpertiseID);
			parameters.AddInputParameter("@IncomingСhannelID", SqlDbType.BigInt, statement.IncomingChannelID);
            parameters.AddInputParameter("@UpdateDate", SqlDbType.DateTime, DateTime.Now);
            SqlParameter StatementID = parameters.AddInputOutputParameter("@StatementID", SqlDbType.BigInt, statement.Id);
            Execute_StoredProcedure("Statement_Save", parameters);
            return (long)StatementID.Value;
		}

        public long Statement_SaveAll(Statement statement)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@CreateDate", SqlDbType.DateTime, statement.CreateDate);
            parameters.AddInputParameter("@LastStatementStatusID", SqlDbType.BigInt, statement.LastStatementStatusID);
            parameters.AddInputParameter("@CuratorID", SqlDbType.BigInt, statement.CuratorID);
            parameters.AddInputParameter("@ResponsibleID", SqlDbType.BigInt, statement.ResponsibleID);
            parameters.AddInputParameter("@ExecutiveID", SqlDbType.BigInt, statement.ExecutiveID);
            parameters.AddInputParameter("@LastStatusDate", SqlDbType.DateTime, statement.LastStatusDate);
            parameters.AddInputParameter("@StatementTypeID", SqlDbType.BigInt, statement.StatementTypeID);
            parameters.AddInputParameter("@Lastname", SqlDbType.NVarChar, statement.Lastname);
            parameters.AddInputParameter("@Firstname", SqlDbType.NVarChar, statement.Firstname);
            parameters.AddInputParameter("@Secondname", SqlDbType.NVarChar, statement.Secondname);
            parameters.AddInputParameter("@Birthday", SqlDbType.DateTime, statement.Birthday);
            parameters.AddInputParameter("@Sex", SqlDbType.NVarChar, statement.Sex);
            parameters.AddInputParameter("@Phone", SqlDbType.NVarChar, statement.Phone);
            parameters.AddInputParameter("@Email", SqlDbType.NVarChar, statement.Email);
			parameters.AddInputParameter("@ReasonID", SqlDbType.BigInt, statement.ReasonID);
			parameters.AddInputParameter("@ClientID", SqlDbType.BigInt, statement.ClientID);
            parameters.AddInputParameter("@VisitGroupID", SqlDbType.BigInt, statement.VisitGroupID);
            parameters.AddInputParameter("@MedDocumentTypeID", SqlDbType.BigInt, statement.MedDocumentTypeID);
            parameters.AddInputParameter("@Series", SqlDbType.NVarChar, statement.Series);
            parameters.AddInputParameter("@Number", SqlDbType.NVarChar, statement.Number);
            parameters.AddInputParameter("@UnifiedPolicyNumber", SqlDbType.NVarChar, statement.UnifiedPolicyNumber);
            parameters.AddInputParameter("@SubjectInsuranceID", SqlDbType.BigInt, statement.SubjectInsuranceID);
            parameters.AddInputParameter("@LocalityID", SqlDbType.BigInt, statement.LocalityID);
            parameters.AddInputParameter("@IncidentDate", SqlDbType.DateTime, statement.IncidentDate);
            parameters.AddInputParameter("@Description", SqlDbType.NVarChar, statement.Description);
            parameters.AddInputParameter("@IncomingСhannelID", SqlDbType.BigInt, statement.IncomingChannelID);
			parameters.AddInputParameter("@ExpertiseID", SqlDbType.BigInt, statement.ExpertiseID);
			parameters.AddInputParameter("@UpdateDate", SqlDbType.DateTime, DateTime.Now);
            SqlParameter StatementID = parameters.AddInputOutputParameter("@StatementID", SqlDbType.BigInt, statement.Id);

            StatementStatusTableSet statusSet = new StatementStatusTableSet(statement.StatementStatuses);
            parameters.AddInputParameter("@tableStatementStatus", SqlDbType.Structured, statusSet.StatementStatusResultTable);
            FileTableSet fileSet = new FileTableSet(statement.Files);
            parameters.AddInputParameter("@tableFile", SqlDbType.Structured, fileSet.FileResultTable);

            parameters.AddInputParameter("@Validity", SqlDbType.Bit, statement.Execution.Validity);
            parameters.AddInputParameter("@Judicial", SqlDbType.Bit, statement.Execution.Judicial);
            parameters.AddInputParameter("@ExpertiseDate", SqlDbType.DateTime, statement.Execution.ExpertiseDate);
            parameters.AddInputParameter("@FinancialSanctions", SqlDbType.Float, statement.Execution.FinancialSanctions);
            parameters.AddInputParameter("@Straf", SqlDbType.Float, statement.Execution.Straf);
            parameters.AddInputParameter("@DescriptionExecution", SqlDbType.NVarChar, statement.Execution.DescriptionExecution);
            parameters.AddInputParameter("@LPU_Code", SqlDbType.NVarChar, statement.Execution.LPU_Code);
            parameters.AddInputParameter("@LPU_Name", SqlDbType.NVarChar, statement.Execution.LPU_Name);
            SqlParameter ExecutionID = parameters.AddInputOutputParameter("@ExecutionID", SqlDbType.BigInt, statement.Execution.Id);

            Execute_StoredProcedure("Statement_SaveAll", parameters);
            return (long)StatementID.Value;
        }

        public Statement Statement_Get(long id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@StatementID", SqlDbType.BigInt, id);
            Statement result = Execute_Get(StatementMaterializer.Instance, "Statement_Get", parameters);
            return result;
        }

        public StatementStatus StatementStatus_Get(long StatementStatusID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@StatementStatusID", SqlDbType.BigInt, StatementStatusID);
            StatementStatus result = Execute_Get(StatementStatusMaterializer.Instance, "StatementStatus_Get", parameters);
            return result;
        }
        public List<StatementStatus> StatementStatus_GetByStatementID(long StatementID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@StatementID", SqlDbType.BigInt, StatementID);
            List<StatementStatus> result = Execute_GetList(StatementStatusMaterializer.Instance, "StatementStatus_GetByStatementID", parameters);
            return result;
        }

        public long? StatementStatus_Save(List<StatementStatus> listStatementStatus)
        {
            if (listStatementStatus.Count == 1)
            {
                StatementStatus statementStatus = listStatementStatus[0];
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.AddInputParameter("@StatementID", SqlDbType.BigInt, statementStatus.StatementID);
                parameters.AddInputParameter("@UserID", SqlDbType.BigInt, statementStatus.UserID);
                parameters.AddInputParameter("@StatusDate", SqlDbType.DateTime, statementStatus.StatusDate);
                parameters.AddInputParameter("@StatusID", SqlDbType.BigInt, statementStatus.StatusID);
                parameters.AddInputParameter("@Comment", SqlDbType.NVarChar, statementStatus.Comment);
                parameters.AddInputParameter("@AssignedToUserID", SqlDbType.BigInt, statementStatus.AssignedToUserID);
                parameters.AddInputParameter("@ExecuteToDate", SqlDbType.DateTime, statementStatus.ExecuteToDate);
                SqlParameter StatementStatusID = parameters.AddInputOutputParameter("@StatementStatusID", SqlDbType.BigInt, statementStatus.Id);
                Execute_StoredProcedure("StatementStatus_Save", parameters);
                return (long)StatementStatusID.Value;
            }
            else
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                StatementStatusTableSet set = new StatementStatusTableSet(listStatementStatus);
                parameters.AddInputParameter("@tableStatementStatus", SqlDbType.Structured, set.StatementStatusResultTable);
                Execute_StoredProcedure("StatementStatus_SaveTable", parameters);
                return null;
            }
        }


        public File File_Get(long fileId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@FileID", SqlDbType.BigInt, fileId);
            return Execute_Get<File>(FileMaterializer.Instance, "File_Get", parameters);
        }
        public List<File> File_GetByStatementID(long statementID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@StatementID", SqlDbType.BigInt, statementID);
            return Execute_GetList<File>(FileMaterializer.Instance, "File_GetByStatementID", parameters);
        }
        public List<File> File_GetByStatementStatusID(long statementStatusID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@StatementStatusID", SqlDbType.BigInt, statementStatusID);
            return Execute_GetList<File>(FileMaterializer.Instance, "File_GetByStatementStatusID", parameters);
        }


        public long? File_Save(List<File> listFiles)
        {
            if(listFiles.Count == 1)
            {
                File file = listFiles[0];
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.AddInputParameter("@AttachmentDate", SqlDbType.DateTime, file.AttachmentDate);
                parameters.AddInputParameter("@UserID", SqlDbType.BigInt, file.UserID);
                parameters.AddInputParameter("@FileName", SqlDbType.NVarChar, file.FileName);
                parameters.AddInputParameter("@FIleUrl", SqlDbType.NVarChar, file.FIleUrl);
                parameters.AddInputParameter("@NominationID", SqlDbType.BigInt, file.NominationID);
                parameters.AddInputParameter("@Comment", SqlDbType.NVarChar, file.Comment);
                parameters.AddInputParameter("@StatementID", SqlDbType.BigInt, file.StatementID);
                parameters.AddInputParameter("@StatementStatusID", SqlDbType.BigInt, file.StatementStatusID);
                SqlParameter FileID = parameters.AddInputOutputParameter("@FileID", SqlDbType.BigInt, file.Id);
                Execute_StoredProcedure("File_Save", parameters);
                return (long)FileID.Value;
            }
            else
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                FileTableSet set = new FileTableSet(listFiles);
                parameters.AddInputParameter("@tableFile", SqlDbType.Structured, set.FileResultTable);
                Execute_StoredProcedure("File_SaveTable", parameters);
                return null;
            }
        }

        public Execution Execution_Get(long executionID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@ExecutionID", SqlDbType.BigInt, executionID);
            return Execute_Get(ExecutionMaterializer.Instance, "Execution_Get", parameters);
        }

        public Execution Execution_GetByStatementID(long statementID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@StatementID", SqlDbType.BigInt, statementID);
            return Execute_Get(ExecutionMaterializer.Instance, "Execution_GetByStatementID", parameters);
        }

        public long Execution_Save(Execution Execution)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.AddInputParameter("@StatementID", SqlDbType.BigInt, Execution.StatementID);
            parameters.AddInputParameter("@Validity", SqlDbType.Bit, Execution.Validity);
            parameters.AddInputParameter("@Judicial", SqlDbType.Bit, Execution.Judicial);
            parameters.AddInputParameter("@ExpertiseDate", SqlDbType.DateTime, Execution.ExpertiseDate);
            parameters.AddInputParameter("@FinancialSanctions", SqlDbType.Float, Execution.FinancialSanctions);
            parameters.AddInputParameter("@Straf", SqlDbType.Float, Execution.Straf);            
            parameters.AddInputParameter("@DescriptionExecution", SqlDbType.NVarChar, Execution.DescriptionExecution);
            parameters.AddInputParameter("@LPU_Code", SqlDbType.NVarChar, Execution.LPU_Code);
            parameters.AddInputParameter("@LPU_Name", SqlDbType.NVarChar, Execution.LPU_Name);
            SqlParameter ExecutionID = parameters.AddInputOutputParameter("@ExecutionID", SqlDbType.BigInt, Execution.Id);
            Execute_StoredProcedure("Execution_Save", parameters);
            return (long)ExecutionID.Value;
        }


    }
}
