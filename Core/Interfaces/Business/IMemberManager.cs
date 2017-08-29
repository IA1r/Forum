using Core.Dto;
using Core.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Business
{
    public interface IMemberManager
    {
        void CreateUser(User user);
        UserDto GetUser(string login);
        UserDto[] GetUsers(bool? isBanned = null);
        void UpdateUserProfile(UserDto newUserProfile);
        void Login(string userlogin, string password, bool rememberMe);
        bool IsRoleOrStatus(string role = null, string status = null);
        void BanUser(int id, string report);
        void UnBanUser(int id);
    }
}
