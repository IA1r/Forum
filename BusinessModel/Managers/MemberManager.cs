using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Providers;
using Core.EntityModel;
using System.Web.Security;
using Core.Dto;
using Core.Interfaces.Provider;
using Core.Interfaces.Business;

namespace BusinessModel.Managers
{

    /// <summary>
    /// The Member Manager
    /// </summary>
    public class MemberManager : IMemberManager
    {

        /// <summary>
        /// The membership provider
        /// </summary>
        private ICustomMembershipProvider provider;

        public MemberManager(ICustomMembershipProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user object.</param>
        public void CreateUser(User user)
        {
            if (this.provider.CheckUser(user.Login))
                throw new ArgumentException("The user is already registered.");
            if (this.provider.CheckEmail(user.Email))
                throw new ArgumentException("The email is already registered.");

            this.provider.CreateUser(user);
        }


        /// <summary>
        /// Logins the specified userlogin.
        /// </summary>
        /// <param name="userlogin">The user login.</param>
        /// <param name="password">The user password.</param>
        /// <param name="rememberMe">if set to <c>true</c> login will remain in the cookie.</param>
        public void Login(string userlogin, string password, bool rememberMe)
        {
            if (this.provider.ValidateUser(userlogin, password))
            {
                this.provider.Login(userlogin, rememberMe);
                return;
            }

            throw new ArgumentException("Wrong Login or password");
        }


        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="login">The user login.</param>
        public UserDto GetUser(string login)
        {
            if (this.provider.CheckUser(login))
                return this.provider.GetUser(login);

            throw new ArgumentException("The user doesn't exist");
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="isBanned"> is banned.</param>
        public UserDto[] GetUsers(bool? isBanned = null)
        {
            return this.provider.GetUsers(isBanned);
        }

        /// <summary>
        /// Updates the user profile.
        /// </summary>
        /// <param name="newUserProfile">The new user profile.</param>
        public void UpdateUserProfile(UserDto newUserProfile)
        {
            if(this.provider.CheckEmail(newUserProfile.Email))
                throw new ArgumentException("The email is already registered.");
            if(this.provider.CheckVkLink(newUserProfile.VK))
                throw new ArgumentException("The VK link is already used.");

            this.provider.UpdateUserProfile(newUserProfile);
        }

        /// <summary>
        /// Determines whether the user match is role or status.
        /// </summary>
        /// <param name="role">The user role.</param>
        /// <param name="status">The user status.</param>
        public bool IsRoleOrStatus(string role = null, string status = null)
        {
            return this.provider.IsRoleOrStatus(role);
        }

        /// <summary>
        /// Ban the user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <param name="report">The report.</param>
        public void BanUser(int id, string report)
        {
            if (!string.IsNullOrWhiteSpace(report))
                this.provider.BanUser(id, report);

            else
                throw new ArgumentException("The report shouldn't be empty");
        }

        /// <summary>
        /// Unban user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        public void UnBanUser(int id)
        {
            this.provider.UnBanUser(id);
        }
    }
}
