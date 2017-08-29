using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Dto;

namespace Forum.ViewModels
{

    /// <summary>
    /// The creation topic view model
    /// </summary>
    public class CreateTopicViewModel
    {

        /// <summary>
        /// Gets or sets the topic.
        /// </summary>
        /// <value>
        /// The topic data transfer object.
        /// </value>
        public TopicDto Topic { get; set; }

        /// <summary>
        /// Gets or sets the section name.
        /// </summary>
        /// <value>
        /// The section name.
        /// </value>
        public string Section { get; set; }
    }
}