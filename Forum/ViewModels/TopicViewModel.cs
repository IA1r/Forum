using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Dto;

namespace Forum.ViewModels
{

    /// <summary>
    /// The topic view model
    /// </summary>
    public class TopicViewModel
    {

        /// <summary>
        /// Gets or sets the topics.
        /// </summary>
        /// <value>
        /// The topics data transfer object.
        /// </value>
        public TopicDto[] Topics { get; set; }

        /// <summary>
        /// Gets or sets the section topic.
        /// </summary>
        /// <value>
        /// The section name.
        /// </value>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets the sections.
        /// </summary>
        /// <value>
        /// The sections name.
        /// </value>
        public string[] Sections { get; set; }

        /// <summary>
        /// Gets or sets the messages count of topic.
        /// </summary>
        /// <value>
        /// The messages count.
        /// </value>
        public Dictionary<int, int> MessagesCount { get; set; }
    }
}