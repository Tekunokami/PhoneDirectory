using System;
using System.Collections.Generic;

namespace PhoneDirectory.Domain.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfilePhotoPath { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? GroupId { get; set; } 
        public Group Group { get; set; }   

        public ICollection<ContactGroup> ContactGroups { get; set; } = new List<ContactGroup>();
    }
}