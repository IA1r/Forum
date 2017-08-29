using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{

    /// <summary>
    /// The message data transfer object
    /// </summary>
    public class MessageDto
    {

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public int MessageID { get; set; }

        /// <summary>
        /// Gets or sets the topic identifier.
        /// </summary>
        /// <value>
        /// The topic identifier.
        /// </value>
        public int TopicID { get; set; }

        /// <summary>
        /// Gets or sets the message topic.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the posted date pf message.
        /// </summary>
        /// <value>
        /// The posted date.
        /// </value>
        public DateTime PostedDate { get; set; }

        /// <summary>
        /// Gets or sets the last edit of message.
        /// </summary>
        /// <value>
        /// The last edit.
        /// </value>
        public DateTime? LastEdit { get; set; }

        /// <summary>
        /// Gets or sets the author of message.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public UserDto Author { get; set; }
    }
}
