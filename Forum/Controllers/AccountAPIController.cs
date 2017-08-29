using Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessModel.Managers;
using Forum.RequestModel;
using Core.Interfaces.Business;
using Core.EntityModel;
using Forum.Captcha;
using System.Web.Helpers;

namespace Forum.Controllers
{

    /// <summary>
    /// The account API controller
    /// </summary>
    public class AccountAPIController : ApiController
    {

        /// <summary>
        /// The member manager
        /// </summary>
        private IMemberManager memberManager;
        public AccountAPIController(IMemberManager memberManager)
        {
            this.memberManager = memberManager;
        }
        /// <summary>
        /// The user data transfer object
        /// </summary>
        private UserDto user;

        /// <summary>
        /// Updates the user profile.
        /// </summary>
        /// <param name="updatedUserProfile">The updated user profile.</param>
        public HttpResponseMessage UpdateProfile([FromBody]UpdateUserProfileRequest updatedUserProfile)
        {
            try
            {
                user = new UserDto
                {
                    UserID = updatedUserProfile.UserID,
                    Name = updatedUserProfile.Name,
                    Password = updatedUserProfile.Password,
                    Email = updatedUserProfile.Email,
                    Country = updatedUserProfile.Country,
                    Image = updatedUserProfile.Image,
                    ICQ = updatedUserProfile.ICQ,
                    VK = updatedUserProfile.VK,
                    Skype = updatedUserProfile.Skype
                };
                memberManager.UpdateUserProfile(user);

                return Request.CreateResponse(HttpStatusCode.OK);
            }

            catch (ArgumentException ex)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { Error = ex.Message });
            }
           
        }

        /// <summary>
        /// Register the specified user.
        /// </summary>
        /// <param name="user">The user register model.</param>
        [HttpPost]
        public HttpResponseMessage Register([FromBody]RegisterRequestModel user)
        {
            try
            {
                if (ReCaptcha.IsValid(user.ReCaptchaResponse))
                {
                    memberManager.CreateUser(new User
                    {
                        Login = user.Login,
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        ConfirmPassword = user.ConfirmPassword
                    });
                    memberManager.Login(user.Login, user.Password, false); 

                    string mainPage = this.Url.Link("Default", new
                    {
                        Controller = "Topic",
                        Action = "Sections",
                    });

                    return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, MainPage = mainPage });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { Error = "Invalid Captcha"}); 
                }
            }
            catch (ArgumentException ex)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { Error = ex.Message });
            }
        }


        /// <summary>
        /// Submits the ban to user.
        /// </summary>
        /// <param name="banModel">The ban model.</param>
        [HttpPost]
        public HttpResponseMessage SubmitBan([FromBody]BanRequestModel banModel)
        {
            try
            {
                memberManager.BanUser(banModel.UserID, banModel.Report);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ArgumentException ex)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { Error = ex.Message });
            }
           
        }


        /// <summary>
        /// Unban user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpPost]
        public void SubmitUnBan(int id)
        {
            memberManager.UnBanUser(id);
        }


        /// <summary>
        /// Gets the all users in application.
        /// </summary>
        /// <param name="IsBanned">The is banned user.</param>
        [HttpGet]
        public UserDto[] GetUsers(bool? IsBanned)
        {
            return this.memberManager.GetUsers(IsBanned);
        }



        //-------------------------------------------------------------------------------------------------------//

        // GET api/accountapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/accountapi/5
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/accountapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/accountapi/5
        public void Delete(int id)
        {
        }
    }
}
