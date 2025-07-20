using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Application.DTOs.Contact
{
    public class CreateContactDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfilePhotoPath { get; set; }

    }
}
