using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.RequestModel
{
    public class ChangeSectionRequestModel
    {
        public int TopicID { get; set; }
        public string Section { get; set; }
    }
}