using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.RequestModel
{
    public class Login
    {
        [Required(ErrorMessage = "Enter Login!", AllowEmptyStrings = false)]
        [RegularExpression(@"([A-Z0-9]){1}([A-Za-z0-9]){2,}", ErrorMessage = "Invalid Login, or short Login.")]
        public string UserLogin { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password!", AllowEmptyStrings = false)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}