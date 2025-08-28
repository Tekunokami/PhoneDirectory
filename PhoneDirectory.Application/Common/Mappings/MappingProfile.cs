using AutoMapper;
using PhoneDirectory.Application.DTOs.Contact;
using PhoneDirectory.Application.DTOs.Group;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateContactDTO, Contact>();
            CreateMap<UpdateContactDTO, Contact>();
            CreateMap<Contact, ContactDTO>();

            CreateMap<CreateGroupDTO, Group>();
            CreateMap<UpdateGroupDTO, Group>();
            CreateMap<Group, GroupDTO>()
                .ForMember(dest => dest.ContactCount, opt => opt.MapFrom(src => src.ContactGroups.Count));
        }
    }
}