using Core.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using Core.Dto;
using Core.Interfaces.Repository;
using Core.Interfaces.UoW;
using Core.Interfaces.Provider;


namespace DataModel.Repositories
{

    /// <summary>
    /// The topic repository
    /// </summary>
    public class TopicRepository : ITopicRepository
    {

        /// <summary>
        /// The unit of work
        /// </summary>
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// The user provider
        /// </summary>
        private ICustomMembershipProvider userProvider;

        public TopicRepository(IUnitOfWork unitOfWork, ICustomMembershipProvider userProvider)
        {
            this.unitOfWork = unitOfWork;
            this.userProvider = userProvider;
        }

        /// <summary>
        /// Creates the topic.
        /// </summary>
        /// <param name="topic">The topic dto object.</param>
        /// <param name="message">The message of topic.</param>
        public void CreateTopic(TopicDto topic, string message)
        {
            User user = this.unitOfWork.Set<User>().Find(this.userProvider.GetUserID());
            user.MessagesCount = (Convert.ToInt32(user.MessagesCount) + 1).ToString();
            this.unitOfWork.Entry<User>(user);

            this.unitOfWork
                .Set<Topic>()
                .Add(new Topic
            {
                AuthorID = user.UserID,
                Name = topic.Name,
                Password = topic.Password,
                PostedDate = DateTime.Now,
                IsLocked = false,
                SectionID = this.unitOfWork.Set<Section>()
                        .Where(s => s.Name == topic.Section)
                        .Select(s => s.SectionID)
                        .SingleOrDefault()
            });
            this.unitOfWork.Save();

            this.unitOfWork
                .Set<Message>()
                .Add(new Message
            {
                TopicID = this.unitOfWork.Set<Topic>()
                        .Where(t => t.Name == topic.Name)
                        .Select(t => t.TopicID)
                        .SingleOrDefault(),
                AuthorID = user.UserID,
                TopicMessage = message,
                PostedDate = DateTime.Now
            });

            this.unitOfWork.Save();
        }

        /// <summary>
        /// Checks the section of topic.
        /// </summary>
        /// <param name="section">The section topic.</param>
        public bool CheckSection(string section)
        {
            return this.unitOfWork.Set<Section>().Any(s => s.Name == section);
        }

        /// <summary>
        /// Gets the topics.
        /// </summary>
        /// <param name="section">The section topic.</param>
        public TopicDto[] GetTopics(string section)
        {
            return this.unitOfWork.Set<Topic>()
                .Where(t => t.Section.Name == section)
                .Select(t => new TopicDto
                {
                    ID = t.TopicID,
                    Name = t.Name,
                    Author = t.User.Login,
                    PostedDate = t.PostedDate,
                    IsLocked = t.IsLocked,
                    Section = t.Section.Name,
                    Password = t.Password
                })
                .OrderByDescending(t => t.PostedDate)
                .ToArray();
        }

        /// <summary>
        /// Gets the topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        public TopicDto GetTopic(int id)
        {
            return this.unitOfWork.Set<Topic>()
                .Where(t => t.TopicID == id)
                .Select(t => new TopicDto
                {
                    ID = t.TopicID,
                    Name = t.Name,
                    IsLocked = t.IsLocked
                })
                .SingleOrDefault();
        }


        /// <summary>
        /// Creates the section.
        /// </summary>
        /// <param name="name">The section name.</param>
        public void CreateSection(string name)
        {
            this.unitOfWork.Set<Section>()
                .Add(new Section
                {
                    Name = name
                });
            this.unitOfWork.Save();
        }

        /// <summary>
        /// Gets the sections.
        /// </summary>
        public string[] GetSections()
        {
            return this.unitOfWork.Set<Section>()
                .Select(s => s.Name)
                .ToArray();
        }


        /// <summary>
        /// Deletes the section.
        /// </summary>
        /// <param name="name">The section name.</param>
        public void DeleteSection(string name)
        {
            Section section = this.unitOfWork.Set<Section>()
                                  .Where(s => s.Name == name)
                                  .Single();

            this.unitOfWork.Set<Section>().Remove(section);
            this.unitOfWork.Save();
        }

        /// <summary>
        /// Changes the section.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="section">The section of topic.</param>
        public void ChangeSection(int id, string section)
        {
            Topic topic = this.unitOfWork.Set<Topic>().Find(id);
            topic.SectionID = this.unitOfWork.Set<Section>()
                .Where(s => s.Name == section)
                .Select(s => s.SectionID)
                .SingleOrDefault();

            this.unitOfWork.Entry<Topic>(topic);
            this.unitOfWork.Save();
        }

        /// <summary>
        /// Searches the topics.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        public TopicDto[] SearchTopics(string keyword)
        {
            if (this.unitOfWork.Set<Topic>().Any(t => t.Name.Contains(keyword)))

                return this.unitOfWork.Set<Topic>()
                   .Where(t => t.Name.Contains(keyword))
                   .Select(topic => new TopicDto
                   {
                       ID = topic.TopicID,
                       Name = topic.Name,
                       Password = topic.Password
                   }).ToArray();

            return null;

        }

        /// <summary>
        /// Gets the topics count of section.
        /// </summary>
        public Dictionary<string, int> GetTopicsCount()
        {
            Topic[] topics = this.unitOfWork.Set<Topic>().ToArray();

            Dictionary<string, int> topicCount = this.unitOfWork.Set<Topic>().ToArray()
                .Select(t => new
                {
                    Key = t.Section.Name,
                    Value = topics.Count(count => count.Section.Name == t.Section.Name)
                })
                .Distinct()
                .ToDictionary(t => t.Key, t => t.Value);

            return topicCount;
        }

        /// <summary>
        /// Lock or unlock the topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="IsLocked">if set to <c>true</c> topic will be locked. 
        /// otherwise, will be unlocked</param>
        public void LockOrUnLock(int id, bool IsLocked)
        {
            Topic topic = this.unitOfWork.Set<Topic>()
                .Where(t => t.TopicID == id)
                .SingleOrDefault();

            topic.IsLocked = IsLocked;

            this.unitOfWork.Entry<Topic>(topic);
            this.unitOfWork.Save();
        }


        /// <summary>
        /// Validates the password of topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="password">The topic password.</param>
        public bool ValidatePassword(int id, string password)
        {
            if (this.unitOfWork.Set<Topic>().Any(t => t.TopicID == id && t.Password == password))
                return true;

            return false;
        }


        /// <summary>
        /// Deletes the topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        public void Delete(int id)
        {
            Topic topic = this.unitOfWork.Set<Topic>().Find(id);
            Message[] messages = this.unitOfWork.Set<Message>()
                                    .Where(m => m.TopicID == id)
                                    .ToArray();

            if (messages != null)
            {
                foreach (Message message in messages)
                {
                    this.unitOfWork.Set<Message>().Remove(message);
                }
                this.unitOfWork.Save();
            }

            this.unitOfWork.Set<Topic>().Remove(topic);
            this.unitOfWork.Save();
        }

    }
}
