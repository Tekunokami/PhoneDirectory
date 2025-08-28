using AutoMapper;
using FluentValidation;
using PhoneDirectory.Application.DTOs.Group;
using PhoneDirectory.Application.Validators;
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneDirectory.Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GroupDTO>> GetAllGroupsAsync()
        {
            var groups = await _unitOfWork.Groups.GetAllGroupsWithContactCountAsync();
            return _mapper.Map<List<GroupDTO>>(groups);
        }

        public async Task<GroupDTO> GetGroupByIdAsync(int id)
        {
            var group = await _unitOfWork.Groups.GetByIdAsync(id);
            if (group == null) return null;

            return _mapper.Map<GroupDTO>(group);
        }

        public async Task<GroupDTO> CreateGroupAsync(CreateGroupDTO groupDto)
        {
            var validator = new CreateGroupDTOValidator();
            var validationResult = await validator.ValidateAsync(groupDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var group = _mapper.Map<Group>(groupDto);
            group.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Groups.AddAsync(group);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GroupDTO>(group);
        }

        public async Task<bool> UpdateGroupAsync(UpdateGroupDTO groupDto)
        {
            var validator = new UpdateGroupDTOValidator();
            var validationResult = await validator.ValidateAsync(groupDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingGroup = await _unitOfWork.Groups.GetByIdAsync(groupDto.Id);
            if (existingGroup == null)
            {
                return false;
            }

            _mapper.Map(groupDto, existingGroup);
            _unitOfWork.Groups.Update(existingGroup);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteGroupAsync(int id)
        {
            var group = await _unitOfWork.Groups.GetByIdAsync(id);
            if (group == null)
            {
                return false;
            }

            _unitOfWork.Groups.Delete(group);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}