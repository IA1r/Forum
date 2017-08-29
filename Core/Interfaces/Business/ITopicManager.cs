using Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Business
{
    public interface ITopicManager
    {
        /// <summary>
        /// Creates the topic.
        /// </summary>
        /// <param name="topic">The topi dto objectc.</param>
        /// <param name="message">The message topic.</param>
        void CreateTopic(TopicDto topic, string message);

        /// <summary>
        /// Checks the section of topic.
        /// </summary>
        /// <param name="section">The section topic.</param>
        bool CheckSection(string section);

        /// <summary>
        /// Gets the topics.
        /// </summary>
        /// <param name="section">The section of topics.</param>
        TopicDto[] GetTopics(string section);

        /// <summary>
        /// Gets the topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        TopicDto GetTopic(int id);

        /// <summary>
        /// Gets the sections.
        /// </summary>
        string[] GetSections();

        /// <summary>
        /// Creates the section.
        /// </summary>
        /// <param name="name">The section name.</param>
        void CreateSection(string name);

        /// <summary>
        /// Deletes the section.
        /// </summary>
        /// <param name="name">The section name.</param>
        void DeleteSection(string name);

        /// <summary>
        /// Changes the section of topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="section">The section of topics.</param>
        void ChangeSection(int id, string section);

        /// <summary>
        /// Searches the topics.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        TopicDto[] SearchTopics(string keyword);

        /// <summary>
        /// Gets the topics count.
        /// </summary>
        Dictionary<string, int> GetTopicsCount();

        /// <summary>
        /// Lock the or unlock the topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="IsLocked">if set to <c>true</c> topic will be locked. 
        /// otherwise, will be unlocked</param>
        void LockOrUnLock(int id, bool IsLocked);

        /// <summary>
        /// Validates the password of topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        bool ValidatePassword(int id, string password);

        /// <summary>
        /// Deletes the topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        void Delete(int id);

    }
}
