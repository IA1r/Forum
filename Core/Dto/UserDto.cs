using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Dto
{

    /// <summary>
    /// The user data transfer object
    /// </summary>
    public class UserDto
    {

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user login.
        /// </summary>
        /// <value>
        /// The login.
        /// </value>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        public bool IsBanned { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the user avatar.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the user country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the messages count of user.
        /// </summary>
        /// <value>
        /// The messages count.
        /// </value>
        public string MessagesCount { get; set; }

        /// <summary>
        /// Gets or sets the icq of user.
        /// </summary>
        /// <value>
        /// The icq.
        /// </value>
        public string ICQ { get; set; }

        /// <summary>
        /// Gets or sets the VK of user.
        /// </summary>
        /// <value>
        /// The vk.
        /// </value>
        public string VK { get; set; }

        /// <summary>
        /// Gets or sets the skype of user.
        /// </summary>
        /// <value>
        /// The skype.
        /// </value>
        public string Skype { get; set; }

        /// <summary>
        /// Gets or sets the registration date of user.
        /// </summary>
        /// <value>
        /// The registration date.
        /// </value>
        public DateTime RegistrationDate { get; set; }
    }
}
