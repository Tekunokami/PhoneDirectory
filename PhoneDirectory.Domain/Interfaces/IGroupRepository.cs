using PhoneDirectory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneDirectory.Domain.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<List<Group>> GetAllGroupsWithContactCountAsync();
    }
}