using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Dto;
using PagedList;

namespace Forum.ViewModels
{

    /// <summary>
    /// The message view model
    /// </summary>
    public class MessageViewModel
    {

        /// <summary>
        /// Gets or sets the topic.
        /// </summary>
        /// <value>
        /// The topic data transfer object.
        /// </value>
        public TopicDto Topic { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages data transfer object.
        /// </value>
        public IPagedList<MessageDto> Messages { get; set; }      
    }
}