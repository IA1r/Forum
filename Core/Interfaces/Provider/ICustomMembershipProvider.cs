using Core.Dto;
using Core.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Provider
{
    public interface ICustomMembershipProvider
    {

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user object.</param>
        void CreateUser(User user);

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        int GetUserID();

        /// <summary>
        /// Checks whether there is already a registered user.
        /// </summary>
        /// <param name="login">The login.</param>
        bool CheckUser(string login);

        /// <summary>
        /// Checks whether there is already a registered email.
        /// </summary>
        /// <param name="email">The email.</param>
        bool CheckEmail(string email);

        /// <summary>
        /// Checks whether there is already used vk link
        /// </summary>
        /// <param name="Vklink">The vk link.</param>
        bool CheckVkLink(string Vklink);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="login">The user login.</param>
        UserDto GetUser(string login);

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="isBanned"> is banned.</param>
        UserDto[] GetUsers(bool? isBanned = null);

        /// <summary>
        /// Updates the user profile.
        /// </summary>
        /// <param name="newUserProfile">The new user profile.</param>
        void UpdateUserProfile(UserDto newUserProfile);

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="login">The user login.</param>
        /// <param name="password">The user password.</param>
        bool ValidateUser(string login, string password);

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="userlogin">The user login.</param>
        /// <param name="rememberMe">if set to <c>true</c> login will remain in the cookie.</param>
        void Login(string userlogin, bool rememberMe);

        /// <summary>
        /// Determines whether the user match is role or status.
        /// </summary>
        /// <param name="role">The user role.</param>
        /// <param name="status">The user status.</param>
        bool IsRoleOrStatus (string role = null, string status = null);

        /// <summary>
        /// Ban the user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <param name="report">The report.</param>
        void BanUser(int id, string report);

        /// <summary>
        /// Unban user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        void UnBanUser(int id);
    }
}
