using RegApplPortal.DataAccess.DAO;
using RegApplPortal.Entities;
using RegApplPortal.Entities.Searching;
using RegApplPortal.Entities.Sorting;
using RegApplPortal.Entities.Core;
using RegApplPortal.Interfaces;
using System.Collections.Generic;
using System;
using RegApplPortal.Entities.Core.Exceptions;

namespace RegApplPortal.BusinessLogic
{
    public class StatementBusinessLogic : IStatementBusinessLogic
    {
        public List<StatementInfo> Statement_Find(StatementSearchCriteria criteria)
        {
            return StatementDao.Instance.Statement_Find(criteria);
        }

        public long Statement_Save(Statement statement)
        {
            if (statement == null)
            {
                throw new ArgumentNullException("Statement can't be null");
            }

            long statementId = StatementDao.Instance.Statement_Save(statement);
            return statementId;
        }

        public long Statement_SaveAll(Statement statement)
        {
            //при таком сохранении обязательно должен быть хотя бы 1 статус
            if (statement.StatementStatuses.Count < 1)
            {
                throw new ArgumentNullException("Должен существовать, хотя бы один статус");
            }
            return StatementDao.Instance.Statement_SaveAll(statement);
        }

        public Statement Statement_Get(long id)
        {
            Statement statement = StatementDao.Instance.Statement_Get(id);
            if (statement == null)
            {
                throw new DataObjectNotFoundException(string.Format("Заявка клиента с идентификатором {0} не найдено", id));
            }
            return statement;
        }

        public StatementStatus StatementStatus_Get(long StatementStatusID)
        {
            return StatementDao.Instance.StatementStatus_Get(StatementStatusID);
        }
        public List<StatementStatus> StatementStatus_GetByStatementID(long StatementID)
        {
            return StatementDao.Instance.StatementStatus_GetByStatementID(StatementID);
        }

        public long? StatementStatus_Save(List<StatementStatus> listStatementStatus)
        {
            if (listStatementStatus != null && listStatementStatus.Count > 0)
            {
                return StatementDao.Instance.StatementStatus_Save(listStatementStatus);
            }
            else
            {
                throw new DataObjectNotFoundException(string.Format("Список статусов, отправленных для сохранения в БД, оказался пустой."));
            }


        }

        public File File_Get(long fileId)
        {
            return StatementDao.Instance.File_Get(fileId);
        }
        public List<File> File_GetByStatementID(long statementID)
        {
            return StatementDao.Instance.File_GetByStatementID(statementID);
        }
        public List<File> File_GetByStatementStatusID(long statementStatusID)
        {
            return StatementDao.Instance.File_GetByStatementStatusID(statementStatusID);
        }
        public long? File_Save(List<File> listFiles)
        {
            if (listFiles != null && listFiles.Count > 0)
            {
                return StatementDao.Instance.File_Save(listFiles);
            }
            else
            {
                throw new DataObjectNotFoundException(string.Format("Список файлов, отправленных для сохранения в БД, оказался пустой."));
            }

        }

        public Execution Execution_Get(long executionID)
        {
            return StatementDao.Instance.Execution_Get(executionID);
        }

        public Execution Execution_GetByStatementID(long statementID)
        {
            return StatementDao.Instance.Execution_GetByStatementID(statementID);
        }
        public long Execution_Save(Execution Execution)
        {
            return StatementDao.Instance.Execution_Save(Execution);
        }
    }
}
