    using System;
    using System.Collections.Generic;

    namespace PhoneDirectory.Domain.Entities
    {
        public class Group
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; } 
            public DateTime CreatedAt { get; set; }  
            public string PhotoUrl { get; set; }     

            public ICollection<ContactGroup> ContactGroups { get; set; } = new List<ContactGroup>();

            public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        }
    }
