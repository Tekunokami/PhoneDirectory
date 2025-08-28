using AutoMapper;
using PhoneDirectory.Application.Common.Exceptions;
using PhoneDirectory.Application.DTOs.Contact;
using PhoneDirectory.Application.Validators;
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentValidation;

namespace PhoneDirectory.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ContactDTO>> GetAllContactsAsync()
        {
            var contacts = await _unitOfWork.Contacts.GetAllAsync();
            return _mapper.Map<List<ContactDTO>>(contacts);
        }

        public async Task<ContactDTO> GetContactByIdAsync(int id)
        {
            var contact = await _unitOfWork.Contacts.GetByIdAsync(id);
            return _mapper.Map<ContactDTO>(contact);
        }

        public async Task<ContactDTO> CreateContactAsync(CreateContactDTO createContactDto)
        {
            var validator = new CreateContactDTOValidator();
            var validationResult = await validator.ValidateAsync(createContactDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var contact = _mapper.Map<Contact>(createContactDto);
            contact.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Contacts.AddAsync(contact);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ContactDTO>(contact);
        }

        public async Task<bool> UpdateContactAsync(UpdateContactDTO updateContactDto)
        {
            var validator = new UpdateContactDTOValidator();
            var validationResult = await validator.ValidateAsync(updateContactDto); 
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existing = await _unitOfWork.Contacts.GetByIdAsync(updateContactDto.Id);
            if (existing == null)
                return false;

            _mapper.Map(updateContactDto, existing);
            _unitOfWork.Contacts.Update(existing);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _unitOfWork.Contacts.GetByIdAsync(id);
            if (contact == null)
                return false;

            _unitOfWork.Contacts.Delete(contact);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task AddContactToGroupAsync(int contactId, int groupId)
        {
            var existing = await _unitOfWork.ContactGroups.GetAsync(cg => cg.ContactId == contactId && cg.GroupId == groupId);
            if (existing == null)
            {
                var contactGroup = new ContactGroup { ContactId = contactId, GroupId = groupId };
                await _unitOfWork.ContactGroups.AddAsync(contactGroup);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task RemoveContactFromGroupAsync(int contactId, int groupId)
        {
            var contactGroup = await _unitOfWork.ContactGroups.GetAsync(cg => cg.ContactId == contactId && cg.GroupId == groupId);
            if (contactGroup != null)
            {
                _unitOfWork.ContactGroups.Delete(contactGroup);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<string> SaveProfilePhotoAsync(int contactId, byte[] fileBytes, string fileExtension)
        {
            var contact = await _unitOfWork.Contacts.GetByIdAsync(contactId);
            if (contact == null)
                throw new CustomValidationException(new List<string> { "Contact not found." });

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            await File.WriteAllBytesAsync(filePath, fileBytes);
            contact.ProfilePhotoPath = $"/uploads/{fileName}";

            _unitOfWork.Contacts.Update(contact);
            await _unitOfWork.CommitAsync();

            return contact.ProfilePhotoPath;
        }

        public async Task<List<ContactDTO>> GetContactsByGroupIdAsync(int groupId)
        {
            var contacts = await _unitOfWork.Contacts.GetContactsByGroupIdAsync(groupId);
            return _mapper.Map<List<ContactDTO>>(contacts);
        }


    }
}