using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.EntityModel;
using BusinessModel.Managers;
using Forum.ViewModels;
using Core.Dto;
using Core.Interfaces.Business;
using PagedList;

namespace Forum.Controllers
{

    /// <summary>
    /// The Topic Controller
    /// </summary>
    public class TopicController : Controller
    {

        /// <summary>
        /// The topic manager
        /// </summary>
        private ITopicManager topicManager;

        /// <summary>
        /// The member manager
        /// </summary>
        private IMemberManager memberManager;

        private IMessageManager messageManager;
        public TopicController(IMemberManager memberManager, ITopicManager topicManager, IMessageManager messageManager)
        {
            this.topicManager = topicManager;
            this.memberManager = memberManager;
            this.messageManager = messageManager;
        }

        /// <summary>
        /// View of topics
        /// </summary>
        /// <param name="section">The section topic.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Topics(string section)
        {
            try
            {
                topicManager.CheckSection(section);
                return View(new TopicViewModel
                {
                    Section = section,
                    Sections = this.topicManager.GetSections(),
                    Topics = this.topicManager.GetTopics(section),
                    MessagesCount = this.messageManager.GetMessagesCount()

                });
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("ErrorPage", new { error = ex.Message });
            }
        }

        /// <summary>
        /// View of section
        /// </summary>
        [HttpGet]
        public ActionResult Sections()
        {
            return View(new SectionsViewModel
            {
                Sections = this.topicManager.GetSections(),
                TopicsCount = topicManager.GetTopicsCount()
            });
        }

        /// <summary>
        /// View of creation topic.
        /// </summary>
        /// <param name="section">The section topic.</param>
        [HttpGet]
        [Authorize]
        public ActionResult CreateTopic(string section)
        {
            try
            {
                topicManager.CheckSection(section);
                return View(new CreateTopicViewModel
                {
                    Section = section
                });
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("ErrorPage", new { error = ex.Message });
            }
        }

        /// <summary>
        /// Create the topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="message">The message topic.</param>
        [HttpPost]
        [Authorize]
        public ActionResult CreateTopic(TopicDto topic, string message)
        {
            try
            {
                topicManager.CreateTopic(topic, message);
                return RedirectToAction("Topics", "Topic", new { section = topic.Section });
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("ErrorPage", new { error = ex.Message });
            }
        }

        /// <summary>
        /// View of specified topics.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        [HttpGet]
        public ActionResult Topic(int id, int page = 1)
        {
            try
            {
                return View(new MessageViewModel
                {
                    Topic = topicManager.GetTopic(id),
                    Messages = this.messageManager.GetMessages(id).ToPagedList(page, 10)
                });
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("ErrorPage", new { error = ex.Message });
            }
        }

        /// <summary>
        /// Sending message to specified topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="message">The message topic.</param>
        [HttpPost]
        [Authorize]
        public ActionResult Topic(int id, string message)
        {
            try
            {
                this.messageManager.CreateMessage(id, message);
                return RedirectToAction("Topic", "Topic", routeValues: new { id = id });
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("ErrorPage", new { error = ex.Message });
            }
        }

       
        /// <summary>
        /// Locks the or unlock the specified topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="IsLocked">if set to <c>true</c> topic will be locked. 
        /// otherwise, will be unlocked</param>
        /// <param name="section">The section topic.</param>
        [HttpGet]
        public ActionResult LockOrUnLock(int id, bool IsLocked, string section)
        {
            if (this.memberManager.IsRoleOrStatus("Admin"))
            {
                this.topicManager.LockOrUnLock(id, IsLocked);
                return RedirectToAction("Topics", "Topic", new { section = section });
            }
            else
                return RedirectToAction("ErrorPage", new { error = "You have no access for this option" });
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
