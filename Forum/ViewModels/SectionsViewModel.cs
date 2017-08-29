using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.ViewModels
{
    public class SectionsViewModel
    {
        public string[] Sections { get; set; }
        public Dictionary<string, int> TopicsCount { get; set; }
    }
}