using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities.Searching;
using RegApplPortal.Entities.Sorting;
using System;
using System.Collections.Generic;

namespace RegApplPortal.Interfaces
{
    public interface IStatementBusinessLogic
    {
        List<StatementInfo> Statement_Find(StatementSearchCriteria criteria);

        /// <summary>
        /// Создание и обновление ТОЛЬКО таблицы dbo.Statement в БД, для сохранения всего объекта используйте Statement_SaveAll
        /// </summary>
        /// <param name="Statement"></param>
        /// <returns></returns>
        long Statement_Save(Statement Statement);
        /// <summary>
        /// Создание и сохранение целиком объект Statement со всеми статусами, файлами, исполнением
        /// </summary>
        /// <param name="Statement"></param>
        /// <returns></returns>
        long Statement_SaveAll(Statement Statement);
        Statement Statement_Get(long id);

        /// <summary>
        /// Достаем статус по его ID
        /// </summary>
        /// <param name="statementStatusID"></param>
        /// <returns></returns>
        StatementStatus StatementStatus_Get(long statementStatusID);

        /// <summary>
        /// Достаем список статусов принадлежащих заявке (по StatementID)
        /// </summary>
        /// <param name="statementID"></param>
        /// <returns></returns>
        List<StatementStatus> StatementStatus_GetByStatementID(long statementID);
        long? StatementStatus_Save(List<StatementStatus> listStatementStatus);

        File File_Get(long fileId);
        List<File> File_GetByStatementID(long statementID);
        List<File> File_GetByStatementStatusID(long statementStatusID);
        long? File_Save(List<File> listFiles);

        Execution Execution_Get(long executionID);
        Execution Execution_GetByStatementID(long statementID);
        long Execution_Save(Execution Execution);

    }
}
