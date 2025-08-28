// PhoneDirectory.Infrastructure/Repositories/GroupRepository.cs
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;
using PhoneDirectory.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneDirectory.Infrastructure.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(PhoneDirectoryDbContext context) : base(context)
        {
        }

        public async Task<List<Group>> GetAllGroupsWithContactCountAsync()
        {
            return await _dbSet.Include(g => g.ContactGroups).ToListAsync();
        }
    }
}