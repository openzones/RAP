using RegApplPortal.BusinessLogic;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities;
using RegApplPortal.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RegApplPortal.WebApps
{
    public static class ReferencesProvider
    {
        private static IReferenceBusinessLogic referenceBusinessLogic = new ReferenceBusinessLogic();
        private static IUserBusinessLogic userBusinessLogic = new UserBusinessLogic();
        private static IStatementBusinessLogic statementBusinessLogic = new StatementBusinessLogic();
        private static ConcurrentDictionary<string, List<ReferenceItem>> referencesPool = new ConcurrentDictionary<string, List<ReferenceItem>>();
        private static HashSet<DateTime> holidays;
        private static HashSet<DateTime> exceptionalWorkingDays;

        public static List<SelectListItem> GetReferences(string referenceName, string selectedValue = null, bool withDefaultEmpty = false)
        {
            List<ReferenceItem> items = GetReferenceItems(referenceName);
            return GetSelectListItems(referenceName, selectedValue, withDefaultEmpty, items);
        }

        private static List<SelectListItem> GetSelectListItems(string referenceName, string selectedValue, bool withDefaultEmpty, List<ReferenceItem> filteredReferences)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            //как пример: справочник DepartmentRef - не сортировать
            if (referenceName != Constants.DeliveryCenterRef)
            {
                filteredReferences.Sort((a, b) => a.Name.CompareTo(b.Name));
            }

            if (withDefaultEmpty)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "Значение не выбрано",
                    Value = ""
                });
            }

            listItems.AddRange(filteredReferences.Select(item => new SelectListItem()
            {
                Text = item.Name,
                Value = item.Id.ToString(),
                Selected = !string.IsNullOrEmpty(selectedValue) && selectedValue == item.Code.ToString()
            }));

            return listItems;
        }

        public static List<ReferenceItem> GetReferenceItems(string referenceName)
        {
            List<ReferenceItem> items;
            if (referencesPool.ContainsKey(referenceName))
            {
                items = referencesPool[referenceName];
            }
            else
            {
                items = referenceBusinessLogic.GetReferencesList(referenceName);
                referencesPool.AddOrUpdate(referenceName, items, (key, value) => value);
            }
            items.Sort((a, b) => a.Name.CompareTo(b.Name));
            return items;
        }

        public static List<ReferenceUniversalItem> GetUniversalReference(string referenceName)
        {
            List<ReferenceUniversalItem> listRefItemUniversal = referenceBusinessLogic.GetUniversalList(referenceName);
            listRefItemUniversal.Sort((a, b) => a.Name.CompareTo(b.Name));
            return listRefItemUniversal;
        }

        public static List<SelectListItem> GetRoles()
        {
            return userBusinessLogic.Role_GetList().Select(role => new SelectListItem() { Text = role.Description, Value = role.Id.ToString() }).ToList();
        }

        public static User GetUser(long id)
        {
            return userBusinessLogic.User_Get(id);
        }

		public static List<SelectListItem> GetCoordinators()
		{
			List<SelectListItem> listItems = new List<SelectListItem>();
			List<Entities.User> listUser = userBusinessLogic.Find("");
			listUser.Sort((a, b) => a.Lastname.CompareTo(b.Lastname));

			foreach (var user in listUser)
			{
				foreach (var role in user.Roles)
				{
					if (role.Id == 3)
						listItems.Add(new SelectListItem()
						{
							Text = user.Lastname + " " + user.Firstname + " " + user.Secondname,
							Value = user.Email
						});
				}
			}
			return listItems;
		}

		public static List<SelectListItem> GetUsers(string selectedValue = null, bool withDefaultEmpty = false)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<Entities.User> listUser = userBusinessLogic.Find("");
            listUser.Sort((a, b) => a.Lastname.CompareTo(b.Lastname));

            if (withDefaultEmpty)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "Значение не выбрано",
                    Value = ""
                });
            }

            foreach (var user in listUser)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = user.Lastname + " " + user.Firstname + " " + user.Secondname,
                    Value = user.Id.ToString(),
                    Selected = !string.IsNullOrEmpty(selectedValue) && selectedValue == user.Id.ToString()
                });
            }
            return listItems;
        }

        public static List<SelectListItem> GetReferencesDisplayName(bool withDefaultEmpty = false)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            if (withDefaultEmpty)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "Значение не выбрано",
                    Value = ""
                });
            }

            List<ReferenceUniversalItem> listRefUniversal = new List<ReferenceUniversalItem>();
            listRefUniversal = referenceBusinessLogic.GetUniversalList(Constants.ReferenceRef);
            listRefUniversal.Sort((a, b) => a.Name.CompareTo(b.Name));

            listItems.AddRange(listRefUniversal.Select(item => new SelectListItem()
            {
                Text = item.DisplayName + " [" + item.Name + "]",
                Value = item.Id.ToString()
            }).ToList());

            return listItems;
        }

        public static HashSet<DateTime> GetHolidays()
        {
            if (holidays == null)
            {
                holidays = referenceBusinessLogic.GetHolidays(null);
            }

            return holidays;
        }

        public static HashSet<DateTime> GetExceptionalWorkingDays()
        {
            if (exceptionalWorkingDays == null)
            {
                exceptionalWorkingDays = referenceBusinessLogic.GetExceptionalWorkingDays(null);
            }
            return exceptionalWorkingDays;
        }

        public static Statement GetStatement(long statementId)
        {
            return statementBusinessLogic.Statement_Get(statementId);
        }

    }
}