//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Topic
    {
        public Topic()
        {
            this.Messages = new HashSet<Message>();
        }
    
        public int TopicID { get; set; }
        public string Name { get; set; }
        public int AuthorID { get; set; }
        public System.DateTime PostedDate { get; set; }
        public bool IsLocked { get; set; }
        public int SectionID { get; set; }
        public string Password { get; set; }
    
        public virtual ICollection<Message> Messages { get; set; }
        public virtual User User { get; set; }
        public virtual Section Section { get; set; }
    }
}
