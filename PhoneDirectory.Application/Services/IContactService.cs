using PhoneDirectory.Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDirectory.Application.Services
{
    public interface IContactService
    {
        Task<List<ContactDTO>> GetAllContactsAsync();
        Task<ContactDTO> GetContactByIdAsync(int id);
        Task<ContactDTO> CreateContactAsync(CreateContactDTO createContactDto);
        Task<bool> UpdateContactAsync(UpdateContactDTO updateContactDto);
        Task<bool> DeleteContactAsync(int id);
        Task<string> SaveProfilePhotoAsync(int contactId, byte[] fileBytes, string fileExtension);

    }
}
