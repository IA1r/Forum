using Core.Dto;
using Core.EntityModel;
using Core.Interfaces.Provider;
using Core.Interfaces.Repository;
using Core.Interfaces.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repositories
{
    public class MessageRepository : IMessageRepository
    {

        /// <summary>
        /// The unit of work
        /// </summary>
        private IUnitOfWork unitOfWork;


        /// <summary>
        /// The user provider
        /// </summary>
        private ICustomMembershipProvider userProvider;

        public MessageRepository(IUnitOfWork unitOfWork, ICustomMembershipProvider userProvider)
        {
            this.unitOfWork = unitOfWork;
            this.userProvider = userProvider;
        }

        /// <summary>
        /// Gets the messages of topic.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        public MessageDto[] GetMessages(int id)
        {
            return this.unitOfWork.Set<Message>()
                .Where(m => m.TopicID == id)
                .Select(m => new MessageDto
                {
                    MessageID = m.MessageID,
                    TopicID = m.TopicID,
                    Message = m.TopicMessage,
                    PostedDate = m.PostedDate,
                    LastEdit = m.LastEdit,
                    Author = new UserDto
                    {
                        Name = m.User.Login,
                        Image = m.User.Image,
                        Country = m.User.Country,
                        MessagesCount = m.User.MessagesCount,
                        RegistrationDate = m.User.RegistrationDate
                    }
                }).ToArray();
        }

        /// <summary>
        /// Gets the messages count of topics.
        /// </summary>
        public Dictionary<int, int> GetMessagesCount()
        {
            Message[] messages = this.unitOfWork.Set<Message>().ToArray();

            Dictionary<int, int> messagesCount = this.unitOfWork.Set<Topic>().ToArray()
                .Select(t => new
                {
                    Key = t.TopicID,
                    Value = messages.Count(count => count.TopicID == t.TopicID)
                })
                .ToDictionary(m => m.Key, m => m.Value);

            return messagesCount;
        }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="id">The topic identifier.</param>
        /// <param name="message">The message topic.</param>
        public void CreateMessage(int id, string message)
        {
            User user = this.unitOfWork.Set<User>().Find(this.userProvider.GetUserID());
            user.MessagesCount = (Convert.ToInt32(user.MessagesCount) + 1).ToString();

            if (Convert.ToInt32(user.MessagesCount) >= 100) user.StatusID = this.unitOfWork.Set<Status>()
                .Where(s => s.Name == "Middle")
                .Select(s => s.StatusID)
                .SingleOrDefault();
            if (Convert.ToInt32(user.MessagesCount) >= 500) user.StatusID = this.unitOfWork.Set<Status>()
                .Where(s => s.Name == "Senior")
                .Select(s => s.StatusID)
                .SingleOrDefault();

            this.unitOfWork.Entry<User>(user);

            this.unitOfWork
                .Set<Message>()
                .Add(new Message
                {
                    TopicID = id,
                    AuthorID = user.UserID,
                    TopicMessage = message,
                    PostedDate = DateTime.Now
                });

            this.unitOfWork.Save();
        }

        /// <summary>
        /// Edits the message.
        /// </summary>
        /// <param name="newMessage">The modified message.</param>
        public void EditMessage(MessageDto newMessage)
        {
            Message message = this.unitOfWork.Set<Message>().Find(newMessage.MessageID);
            message.TopicMessage = newMessage.Message;
            message.LastEdit = DateTime.Now;

            this.unitOfWork.Entry<Message>(message);
            this.unitOfWork.Save();
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="id">The message identifier.</param>
        public void Delete(int id)
        {
            Message message = this.unitOfWork.Set<Message>().Find(id);

            this.unitOfWork.Set<Message>().Remove(message);
            this.unitOfWork.Save();
        }
    }

}
