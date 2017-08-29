using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.RequestModel
{
    public class UpdateUserProfileRequest
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
        public string ICQ { get; set; }
        public string VK { get; set; }
        public string Skype { get; set; }
    }
}