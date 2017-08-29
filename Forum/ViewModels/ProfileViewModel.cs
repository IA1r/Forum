using Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.ViewModels
{

    /// <summary>
    /// The profile view model
    /// </summary>
    public class ProfileViewModel
    {

        /// <summary>
        /// Gets or sets the user profile.
        /// </summary>
        /// <value>
        /// The user profile data transfer object.
        /// </value>
        public UserDto UserProfile { get; set; }
    }
}