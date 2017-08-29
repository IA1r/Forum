using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.RequestModel
{
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = "Enter Login!", AllowEmptyStrings = false)]
        [RegularExpression(@"([A-Z0-9]){1}([A-Za-z0-9]){2,}", ErrorMessage = "Invalid Login, or short Login.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter Name!", AllowEmptyStrings = false)]
        [RegularExpression(@"([A-Z]){1}([A-Za-z]){2,}", ErrorMessage = "Invalid name, or short Name. Try somethink like: Air, Yoruichi, Byakuya")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Email", AllowEmptyStrings = false)]
        [RegularExpression(@"([A-Za-z0-9])+@([a-z])+\.(ua|net|com|ru)", ErrorMessage = "Invalid Email. Try something like: ichika@i.ua, fresha1r@outlook.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Set password!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "The password should be at least 8 characters and no more than 14")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string ReCaptchaResponse { get; set; }
    }
}