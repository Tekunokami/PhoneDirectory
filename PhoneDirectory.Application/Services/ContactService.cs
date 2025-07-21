using PhoneDirectory.Application.DTOs.Contact;
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using PhoneDirectory.Application.Validators;
using System.IO;


namespace PhoneDirectory.Application.Services
{
    public class ContactService : IContactService
    {
        public async Task<List<ContactDTO>> GetAllContactsAsync()
        {
            var contacts = await _contactRepository.GetAllAsync();

            return contacts.Select(c => new ContactDTO
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                Address = c.Address,
                BirthDate = c.BirthDate,
                ProfilePhotoPath = c.ProfilePhotoPath
            }).ToList();
        }
        public async Task<ContactDTO> GetContactByIdAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);

            if (contact == null)
                return null;

            return new ContactDTO
            {
                Id = contact.Id,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Address = contact.Address,
                BirthDate = contact.BirthDate,
                ProfilePhotoPath = contact.ProfilePhotoPath
            };
        }


        public async Task<ContactDTO> CreateContactAsync(CreateContactDTO createContactDto)
        {
            var validator = new CreateContactDTOValidator();
            var validation = validator.Validate(createContactDto);

            if (!validation.IsValid)
                throw new Exception(string.Join("; ", validation.Errors));


            var contact = new Contact
            {
                Name = createContactDto.Name,
                PhoneNumber = createContactDto.PhoneNumber,
                Email = createContactDto.Email,
                Address = createContactDto.Address,
                BirthDate = createContactDto.BirthDate,
                ProfilePhotoPath = createContactDto.ProfilePhotoPath,
                CreatedAt = DateTime.UtcNow 
            };

            await _contactRepository.AddAsync(contact);

            return new ContactDTO
            {
                Id = contact.Id,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Address = contact.Address,
                BirthDate = contact.BirthDate,
                ProfilePhotoPath = contact.ProfilePhotoPath,
                CreatedAt = contact.CreatedAt 
            };
        }



        public async Task<bool> UpdateContactAsync(UpdateContactDTO updateContactDto)
        {
            var validator = new UpdateContactDTOValidator();
            var validation = validator.Validate(updateContactDto);

            if (!validation.IsValid)
                throw new Exception(string.Join("; ", validation.Errors));

            var existing = await _contactRepository.GetByIdAsync(updateContactDto.Id);
            if (existing == null)
                return false;

            existing.Name = updateContactDto.Name;
            existing.PhoneNumber = updateContactDto.PhoneNumber;
            existing.Email = updateContactDto.Email;
            existing.Address = updateContactDto.Address;
            existing.BirthDate = updateContactDto.BirthDate;
            existing.ProfilePhotoPath = updateContactDto.ProfilePhotoPath;

            await _contactRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<string> SaveProfilePhotoAsync(int contactId, byte[] fileBytes, string fileExtension)
        {
            var contact = await _contactRepository.GetByIdAsync(contactId);
            if (contact == null)
                throw new Exception("Contact not found");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(uploadsFolder, fileName);

            await File.WriteAllBytesAsync(filePath, fileBytes);

            contact.ProfilePhotoPath = $"/uploads/{fileName}";
            await _contactRepository.UpdateAsync(contact);
            await _contactRepository.SaveAsync();

            return contact.ProfilePhotoPath;
        }



        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null)
                return false;

            await _contactRepository.DeleteAsync(id); 
            return true;
        }


        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        

    }
}
