using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Repositories;
using Core.EntityModel;
using Core.Dto;
using Core.Interfaces.Business;
using Core.Interfaces.Repository;

namespace BusinessModel.Managers
{

    /// <summary>
    /// Topic manager
    /// </summary>
    public class TopicManager : ITopicManager
    {

        /// <summary>
        /// The topic repository
        /// </summary>
        private ITopicRepository repository;
        public TopicManager(ITopicRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Creates the topic.
        /// </summary>
        /// <param name="topic">The topi dto objectc.</param>
        /// <param name="message">The message topic.</param>
        public void CreateTopic(TopicDto topic, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                this.repository.CreateTopic(topic, message);
            else
                throw new ArgumentException("The message shouldn't be empty");

        }

        /// <summary>
        /// Checks the section of topic.
        /// </summary>
        /// <param name="section">The section topic.</param>
        public bool CheckSection(string section)
        {
            if (this.repository.CheckSection(section))
                return true;

            throw new ArgumentException("Wrong section");
        }

        /// <summary>
        /// Creates the section.
        /// </summary>
        /// <param name="name">The section name.</param>
        public void CreateSection(string name)
        {
            if (!this.repository.CheckSection(name))
                this.repository.CreateSection(name);
            else
                throw new ArgumentException("Section already exist");
        }


        /// <summary>
        /// Deletes the section.
        /// </summary>
        /// <param name="name">The section name.</param>
        public void DeleteSection(string name)
        {
            this.repository.DeleteSection(name);
        }

        /// <summary>
        /// Gets the topics.
        /// </summary>
        /// <param name="section">The section of topics.</param>
        public TopicDto[] GetTopics(string section)
        {
            if (CheckSection(section))
                return this.repository.GetTopics(section);

            throw new ArgumentException("Wrong section");
        }

        /// <summary>
        /// Gets the topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        public TopicDto GetTopic(int id)
        {
            TopicDto topic = this.repository.GetTopic(id);

            if (topic != null)
                return topic;
            else
                throw new ArgumentException("This topic doesn't exist");
        }


        /// <summary>
        /// Gets the sections.
        /// </summary>
        public string[] GetSections()
        {
            return repository.GetSections();
        }

        /// <summary>
        /// Changes the section of topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="section">The section of topics.</param>
        public void ChangeSection(int id, string section)
        {
            repository.ChangeSection(id, section);
        }

        /// <summary>
        /// Searches the topics.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        public TopicDto[] SearchTopics(string keyword)
        {
            TopicDto[] topics = this.repository.SearchTopics(keyword);

            if (topics != null)
                return topics;

            throw new ArgumentException("No Results");
        }

        /// <summary>
        /// Gets the topics count.
        /// </summary>
        public Dictionary<string, int> GetTopicsCount()
        {
            return this.repository.GetTopicsCount();
        }

        /// <summary>
        /// Lock the or unlock the topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="IsLocked">if set to <c>true</c> topic will be locked. 
        /// otherwise, will be unlocked</param>
        public void LockOrUnLock(int id, bool IsLocked)
        {
            this.repository.LockOrUnLock(id, IsLocked);
        }

        /// <summary>
        /// Validates the password of topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        public bool ValidatePassword(int id, string password)
        {
           return this.repository.ValidatePassword(id, password);
        }


        /// <summary>
        /// Deletes the topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        public void Delete(int id)
        {
            this.repository.Delete(id);
        }
    }
}
