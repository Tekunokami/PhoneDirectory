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
using System.Globalization;

namespace PhoneDirectory.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<List<ContactDTO>> GetAllContactsAsync()
        {
            var contacts = await _contactRepository.GetAllAsync();

            return contacts.Select(contact => new ContactDTO
            {
                Id = contact.Id,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Address = contact.Address,
                Birthday = contact.BirthDate.ToString("dd.MM.yyyy"),
                PhotoUrl = contact.ProfilePhotoPath,
                Note = contact.Note,
                CreatedAt = contact.CreatedAt
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
                Birthday = contact.BirthDate.ToString("dd.MM.yyyy"),
                PhotoUrl = contact.ProfilePhotoPath,
                Note = contact.Note,
                CreatedAt = contact.CreatedAt
            };
        }

        public async Task<ContactDTO> CreateContactAsync(CreateContactDTO createContactDto)
        {
            var validator = new CreateContactDTOValidator();
            var validation = validator.Validate(createContactDto);

            if (!validation.IsValid)
                throw new Exception(string.Join("; ", validation.Errors));

            var birthDateParsed = !string.IsNullOrEmpty(createContactDto.Birthday)
                ? DateTime.ParseExact(createContactDto.Birthday, "dd.MM.yyyy", CultureInfo.InvariantCulture)
                : DateTime.MinValue;  // Null değer gelirse default atama (isteğe göre değişebilir)

            var contact = new Contact
            {
                Name = createContactDto.Name,
                PhoneNumber = createContactDto.PhoneNumber,
                Email = createContactDto.Email,
                Address = createContactDto.Address,
                BirthDate = birthDateParsed,
                ProfilePhotoPath = !string.IsNullOrEmpty(createContactDto.PhotoUrl) ? createContactDto.PhotoUrl : createContactDto.PhotoPath,
                Note = createContactDto.Note,
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
                Birthday = contact.BirthDate.ToString("dd.MM.yyyy"),
                PhotoUrl = contact.ProfilePhotoPath,
                Note = contact.Note,
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

            var birthDateParsed = !string.IsNullOrEmpty(updateContactDto.Birthday)
                ? DateTime.ParseExact(updateContactDto.Birthday, "dd.MM.yyyy", CultureInfo.InvariantCulture)
                : existing.BirthDate; // Eğer yeni tarih verilmezse eskisini kullan.

            existing.Name = updateContactDto.Name;
            existing.PhoneNumber = updateContactDto.PhoneNumber;
            existing.Email = updateContactDto.Email;
            existing.Address = updateContactDto.Address;
            existing.BirthDate = birthDateParsed;
            existing.ProfilePhotoPath = !string.IsNullOrEmpty(updateContactDto.PhotoUrl) ? updateContactDto.PhotoUrl : updateContactDto.PhotoPath;
            existing.Note = updateContactDto.Note;

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
    }
}
