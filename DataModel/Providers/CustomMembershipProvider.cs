using Core.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Core.Dto;
using System.Web;
using System.Data.Entity;
using System.IO;
using Core.Interfaces.Provider;
using Core.Interfaces.UoW;
using Newtonsoft.Json;

namespace DataModel.Providers
{

    /// <summary>
    /// The Custom Membership Provider
    /// </summary>
    public class CustomMembershipProvider : MembershipProvider, ICustomMembershipProvider
    {
        /// <summary>
        /// The entity context
        /// </summary>
        private IUnitOfWork unitOfWork;
        public CustomMembershipProvider(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user object.</param>
        public void CreateUser(User user)
        {
            user.RoleID = this.unitOfWork.Set<Role>().Where(r => r.Name == "User").SingleOrDefault().RoleID;
            user.StatusID = this.unitOfWork.Set<Status>().Where(s => s.Name == "Junior").SingleOrDefault().StatusID;
            user.MessagesCount = "0";
            user.RegistrationDate = DateTime.Now;
            user.Image = "/Content/Images/unknown.jpg";
            user.Country = "unknown";
            user.ICQ = "unknown";
            user.Skype = "unknown";
            user.VK = "unknown";

            this.unitOfWork.Set<User>().Add(user);
            this.unitOfWork.Save();
        }


        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="userlogin">The user login.</param>
        /// <param name="rememberMe">if set to <c>true</c> login will remain in the cookie.</param>
        public void Login(string userlogin, bool rememberMe)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (
                    1,
                    userlogin,
                    DateTime.Now,
                    DateTime.Now.AddDays(365),
                    rememberMe,
                    JsonConvert.SerializeObject(GetUser(userlogin)),
                    FormsAuthentication.FormsCookiePath
                );

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            cookie.Expires = rememberMe ? ticket.Expiration : DateTime.MinValue;
            HttpContext.Current.Response.SetCookie(cookie);
        }


        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public int GetUserID()
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                return JsonConvert.DeserializeObject<UserDto>(ticket.UserData).UserID;
            }
            else
                return 0;
        }


        /// <summary>
        /// Checks whether there is already a registered user.
        /// </summary>
        /// <param name="login">The login.</param>
        public bool CheckUser(string login)
        {
            return this.unitOfWork.Set<User>().Any(u => u.Login == login);
        }


        /// <summary>
        /// Checks whether there is already a registered email.
        /// </summary>
        /// <param name="email">The email.</param>
        public bool CheckEmail(string email)
        {
            return this.unitOfWork.Set<User>().Any(u => u.Email == email);
        }

        /// <summary>
        /// Checks whether there is already used vk link
        /// </summary>
        /// <param name="Vklink">The vk link.</param>
        public bool CheckVkLink(string Vklink)
        { 
            if(Vklink != null)
                return this.unitOfWork.Set<User>().Any(u => u.VK == Vklink);

            return false;
        }


        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="login">The user login.</param>
        public UserDto GetUser(string login)
        {
            UserDto user = this.unitOfWork.Set<User>()
                   .Where(u => u.Login == login)
                   .Select(u => new UserDto
                   {
                       UserID = u.UserID,
                       Name = u.Name,
                       Login = u.Login,
                       Password = u.Password,
                       Email = u.Email,
                       Status = u.Status.Name,
                       IsBanned = u.IsBanned,
                       Role = u.Role.Name,
                       Image = u.Image,
                       Country = u.Country,
                       MessagesCount = u.MessagesCount,
                       ICQ = u.ICQ,
                       VK = u.VK,
                       Skype = u.Skype,
                       RegistrationDate = u.RegistrationDate,
                   })
                   .SingleOrDefault();

            return user;
        }


        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="isBanned"> is banned.</param>
        public UserDto[] GetUsers(bool? isBanned = null)
        {
            if (isBanned == null)
            {
                UserDto[] user = this.unitOfWork.Set<User>()
                                .Select(u => new UserDto
                                {
                                    UserID = u.UserID,
                                    Login = u.Login,
                                    IsBanned = u.IsBanned,
                                })
                                .ToArray();
                return user;
            }
            else
            {
                UserDto[] user = this.unitOfWork.Set<User>()
                                .Where(u => u.IsBanned == isBanned)
                                .Select(u => new UserDto
                                {
                                    UserID = u.UserID,
                                    Login = u.Login,
                                    IsBanned = u.IsBanned,
                                })
                                .ToArray();
                return user;
            }

        }


        /// <summary>
        /// Updates the user profile.
        /// </summary>
        /// <param name="newUserProfile">The new user profile.</param>
        public void UpdateUserProfile(UserDto newUserProfile)
        {
            User user = this.unitOfWork.Set<User>().Find(newUserProfile.UserID);

            string imageName = null;
            string imagePath = null;

            if (user.Image != newUserProfile.Image)
            {
                imageName = Path.GetFileName(newUserProfile.Image);
                imagePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images"), imageName);
                File.Copy(newUserProfile.Image, imagePath);

                user.Image = "/Content/Images/" + imageName;
            }


            user.Name = newUserProfile.Name;
            user.Password = newUserProfile.Password;
            user.ConfirmPassword = newUserProfile.Password;
            user.Email = newUserProfile.Email;
            user.Country = newUserProfile.Country;
            user.ICQ = newUserProfile.ICQ;
            user.VK = newUserProfile.VK;
            user.Skype = newUserProfile.Skype;

            this.unitOfWork.Entry<User>(user);
            this.unitOfWork.Save();
        }


        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="login">The user login.</param>
        /// <param name="password">The user password.</param>
        public override bool ValidateUser(string login, string password)
        {
            if (this.unitOfWork.Set<User>().Any(u => u.Login == login && u.Password == password))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Determines whether the user match is role or status.
        /// </summary>
        /// <param name="role">The user role.</param>
        /// <param name="status">The user status.</param>
        public bool IsRoleOrStatus(string role, string status)
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                UserDto currentUser = JsonConvert.DeserializeObject<UserDto>(ticket.UserData);

                if (currentUser != null && currentUser.Role == role || currentUser.Status == status)
                    return true;
                return false;
            }
            else
                return false;
        }


        /// <summary>
        /// Ban the user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <param name="report">The report.</param>
        public void BanUser(int id, string report)
        {
            User user = this.unitOfWork.Set<User>().Find(id);
            user.IsBanned = true;

            this.unitOfWork.Entry<User>(user);
            this.unitOfWork.Save();

            Banned banUser = new Banned
            {
                UserID = id,
                Status = true,
                Report = report,
                Date = DateTime.Now
            };

            this.unitOfWork.Set<Banned>().Add(banUser);
            this.unitOfWork.Save();
        }


        /// <summary>
        /// Unban user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        public void UnBanUser(int id)
        {
            User user = this.unitOfWork.Set<User>().Find(id);
            user.IsBanned = false;

            this.unitOfWork.Entry<User>(user);
            this.unitOfWork.Save();

            Banned banUser = new Banned
            {
                UserID = id,
                Status = false,
                Report = "Unbanned",
                Date = DateTime.Now
            };

            this.unitOfWork.Set<Banned>().Add(banUser);
            this.unitOfWork.Save();
        }



        //---------------------------------------------------------------------------------------------------------------//

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }


    }
}
