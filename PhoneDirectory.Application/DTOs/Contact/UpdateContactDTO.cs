using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Application.DTOs.Contact
{
    public class UpdateContactDTO : CreateContactDTO
    {
        public int Id { get; set; }
    }
}
