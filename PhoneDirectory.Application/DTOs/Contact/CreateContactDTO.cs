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
        public string Birthday { get; set; } 
        public string PhotoPath { get; set; }
        public string PhotoUrl { get; set; }
        public string Note { get; set; }
    }



}
