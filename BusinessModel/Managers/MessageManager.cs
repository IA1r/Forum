using Core.Dto;
using Core.Interfaces.Business;
using Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Managers
{
    public class MessageManager : IMessageManager
    {
        private IMessageRepository repository;
        public MessageManager(IMessageRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets the messages of topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        public MessageDto[] GetMessages(int id)
        {
            return this.repository.GetMessages(id);
        }

        /// <summary>
        /// Gets the messages count.
        /// </summary>
        public Dictionary<int, int> GetMessagesCount()
        {
            return this.repository.GetMessagesCount();
        }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="message">The message of topic.</param>
        public void CreateMessage(int id, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                this.repository.CreateMessage(id, message);
            else
                throw new ArgumentException("The message shouldn't be empty");
        }

        /// <summary>
        /// Edits the message.
        /// </summary>
        /// <param name="newMessage">The modified message.</param>
        public void EditMessage(MessageDto newMessage)
        {
            this.repository.EditMessage(newMessage);
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="id">The message identifier.</param>
        public void Delete(int id)
        {
            this.repository.Delete(id);
        }
    }
}
