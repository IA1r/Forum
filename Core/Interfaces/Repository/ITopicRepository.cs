using Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repository
{
    public interface ITopicRepository
    {

        /// <summary>
        /// Creates the topic.
        /// </summary>
        /// <param name="topic">The topic dto object.</param>
        /// <param name="message">The message of topic.</param>
        void CreateTopic(TopicDto topic, string message);

        /// <summary>
        /// Checks the section of topic.
        /// </summary>
        /// <param name="section">The section topic.</param>
        bool CheckSection(string section);

        /// <summary>
        /// Gets the topics.
        /// </summary>
        /// <param name="section">The section topic.</param>
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
        /// Changes the section.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="section">The section of topic.</param>
        void ChangeSection(int id, string section);

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
        /// Searches the topics.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        TopicDto[] SearchTopics(string keyword);

        /// <summary>
        /// Gets the topics count of section.
        /// </summary>
        Dictionary<string, int> GetTopicsCount();

        /// <summary>
        /// Lock or unlock the topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="IsLocked">if set to <c>true</c> topic will be locked. 
        /// otherwise, will be unlocked</param>
        void LockOrUnLock(int id, bool IsLocked);

        /// <summary>
        /// Validates the password of topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="password">The topic password.</param>
        bool ValidatePassword(int id, string password);

        /// <summary>
        /// Deletes the topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        void Delete(int id);
    }
}
