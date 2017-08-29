using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessModel.Managers;
using Core.EntityModel;
using System.Web.Security;
using Forum.RequestModel;
using Forum.ViewModels;
using Core.Interfaces.Business;
using Forum.Captcha;

namespace Forum.Controllers
{

    /// <summary>
    /// The accountn controller
    /// </summary>
    public class AccountController : Controller
    {

        /// <summary>
        /// The member manager
        /// </summary>
        private IMemberManager memberManager;

        public AccountController(IMemberManager memberManager)
        {
            this.memberManager = memberManager;
        }

        /// <summary>
        /// View of registration page.
        /// </summary>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// View of login page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Login the specified user.
        /// </summary>
        /// <param name="user">The user object.</param>
        [HttpPost]
        public ActionResult Login(Login user)
        {
            try
            {
                memberManager.Login(user.UserLogin, user.Password, user.RememberMe);
                return RedirectToAction("Sections", "Topic");
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("ErrorPage", "Account", new { error = ex.Message });
            }
        }

        /// <summary>
        /// View of specified user profile.
        /// </summary>
        /// <param name="login">The user login.</param>
        [HttpGet]
        [Authorize]
        public ActionResult UserProfile(string login)
        {
            try
            {
                return View(new ProfileViewModel
                {
                    UserProfile = memberManager.GetUser(login)
                });
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("ErrorPage", "Account", new { error = ex.Message });
            }

        }

        /// <summary>
        /// Log out.
        /// </summary>
        [Authorize]
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Sections", "Topic");
        }

        /// <summary>
        /// View of all users in application.
        /// </summary>
        [HttpGet]
        public ActionResult UsersList()
        {
            return View(new UsersListViewModel
            {
                UsersList = this.memberManager.GetUsers()
            });
        }

        /// <summary>
        /// Error page.
        /// </summary>
        /// <param name="error">The error.</param>
        [HttpGet]
        public ActionResult ErrorPage(string error)
        {
            return View("Error", null, error);
        }
    }
}
