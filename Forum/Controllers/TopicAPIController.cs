using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Forum.RequestModel;
using Core.Dto;
using BusinessModel.Managers;
using Core.Interfaces.Business;

namespace Forum.Controllers
{

    /// <summary>
    /// The topic API controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class TopicAPIController : ApiController
    {
        /// <summary>
        /// The message data transfer object
        /// </summary>
        //private MessageDto message;

        /// <summary>
        /// The topic manager
        /// </summary>
        private ITopicManager topicManager;
        private IMemberManager memberManager;
        private IMessageManager messageManager;
        public TopicAPIController(ITopicManager topicManager, IMemberManager memberManager, IMessageManager messageManager)
        {
            this.topicManager = topicManager;
            this.memberManager = memberManager;
            this.messageManager = messageManager;
        }

        /// <summary>
        /// Edits the message.
        /// </summary>
        /// <param name="editedMessage">The edited message.</param>
        [HttpPost]
        public void EditMessage([FromBody]MessageRequest editedMessage)
        {
            MessageDto message = new MessageDto
            {
                MessageID = editedMessage.ID,
                Message = editedMessage.Message
            };

            this.messageManager.EditMessage(message);
        }

        /// <summary>
        /// Changes the section of topic.
        /// </summary>
        /// <param name="request">The request of changes.</param>
        [HttpPost]
        public void ChangeSection([FromBody]ChangeSectionRequestModel request)
        {
            if (this.memberManager.IsRoleOrStatus("Admin"))
                this.topicManager.ChangeSection(request.TopicID, request.Section);

        }

        [HttpGet]
        public HttpResponseMessage CreateSection(string name)
        {
            try
            {
                this.topicManager.CreateSection(name);

                string newUrl = this.Url.Link("Default", new
                {
                    Controller = "Topic",
                    Action = "Sections"
                });

                return Request.CreateResponse(HttpStatusCode.OK, new { MainPage = newUrl });
            }
            catch (ArgumentException ex)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { Error = ex.Message });
            }
            
        }

        /// <summary>
        /// Gets the topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="password">The topic password.</param>
        [HttpGet]
        public HttpResponseMessage GetTopic(int id, string password)
        {
            if (!this.topicManager.ValidatePassword(id, password))
                return Request.CreateResponse(new { Success = false });

            string newUrl = this.Url.Link("Default", new
            {
                Controller = "Topic",
                Action = "Topic",
                id = id
            });

            return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
        }

        /// <summary>
        /// Deletes the message in topic.
        /// </summary>
        /// <param name="id">The message identifier.</param>
        [HttpPost]
        public void DeleteMessage(int id)
        {
            this.messageManager.Delete(id);
        }


        /// <summary>
        /// Deletes the topic.
        ///</summary>
        /// <param name="id">The topic identifier.</param>
        [HttpPost]
        public void DeleteTopic(int id)
        {
            this.topicManager.Delete(id);
        }

        [HttpGet]
        public void DeleteSection(string name)
        {
            this.topicManager.DeleteSection(name);
        }

        /// <summary>
        /// Searches the Topic.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        [HttpGet]
        public HttpResponseMessage SearchTopics(string keyword)
        {
            try
            {
                TopicDto[] topics = this.topicManager.SearchTopics(keyword);
                return Request.CreateResponse(HttpStatusCode.OK, new { Topics = topics });
            }
            catch (ArgumentException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Error = ex.Message });
            }


        }

        //----------------------------------------------------------------------------------------------------------//

        // GET api/topicapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/topicapi/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
