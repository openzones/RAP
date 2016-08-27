using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using System;
using System.Collections.Generic;

namespace RegApplPortal.Interfaces
{
    public interface IReferenceBusinessLogic
    {
        List<ReferenceUniversalItem> GetUniversalList(string referenceName);
        void SaveUniversalReferenceItem(ReferenceUniversalItem item, string referenceName, bool flagUpdateOrInsert = false);
        void DeleteReferenceItem(long id, string referenceName);
        List<ReferenceItem> GetReferencesList(string referenceName);
        HashSet<DateTime> GetHolidays(int? year);
        HashSet<DateTime> GetExceptionalWorkingDays(int? year);
        List<ReferenceUniversalItem> GetAllReference();
    }
}
