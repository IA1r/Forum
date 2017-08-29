using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Forum.Captcha
{
    public class ReCaptcha
    {
        public static bool IsValid(string response)
        {
            string secretKey = "6LcbNSYTAAAAABKnWWmXVADY2louAY8Ufeb5f9cW";
            WebClient client = new WebClient();
            string result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            JObject obj = JObject.Parse(result);

            return (bool)obj.SelectToken("success");
        }
    }
}