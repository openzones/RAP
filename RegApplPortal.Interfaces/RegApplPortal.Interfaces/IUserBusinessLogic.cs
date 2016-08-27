using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using System.Collections.Generic;

namespace RegApplPortal.Interfaces
{
    public interface IUserBusinessLogic
    {
        long User_Save(User.SaveData data);
        User User_Get(long userId);
        User User_GetByLogin(string login);

        List<User> Find(string name);
        List<Role> Role_GetList();
    }
}
