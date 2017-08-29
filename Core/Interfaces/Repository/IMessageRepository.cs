using Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repository
{
    public interface IMessageRepository
    {
        /// <summary>
        /// Gets the messages of topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        MessageDto[] GetMessages(int id);

        /// <summary>
        /// Gets the messages count of topics.
        /// </summary>
        Dictionary<int, int> GetMessagesCount();

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="message">The message topic.</param>
        void CreateMessage(int id, string message);

        /// <summary>
        /// Edits the message.
        /// </summary>
        /// <param name="newMessage">The modified message.</param>
        void EditMessage(MessageDto newMessage);

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="id">The message identifier.</param>
        void Delete(int id);
    }
}
