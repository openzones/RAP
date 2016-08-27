using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities.Report;
using RegApplPortal.Entities.Searching;
using System;
using System.Collections.Generic;

namespace RegApplPortal.Interfaces
{
    public interface IReportBusinessLogic
    {
        List<BaseReport> GetBaseReport(StatementSearchCriteria criteria);
    }
}
