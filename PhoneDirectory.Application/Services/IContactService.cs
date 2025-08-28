using PhoneDirectory.Application.DTOs.Contact;
using System.Collections.Generic;
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
        Task AddContactToGroupAsync(int contactId, int groupId);
        Task RemoveContactFromGroupAsync(int contactId, int groupId);
    }
}
