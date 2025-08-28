using PhoneDirectory.Application.DTOs.Group;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneDirectory.Application.Services
{
    public interface IGroupService
    {
        Task<List<GroupDTO>> GetAllGroupsAsync();
        Task<GroupDTO> GetGroupByIdAsync(int id);
        Task<GroupDTO> CreateGroupAsync(CreateGroupDTO groupDto); 
        Task<bool> UpdateGroupAsync(UpdateGroupDTO groupDto);    
        Task<bool> DeleteGroupAsync(int id);                      
    }
}