using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;
using PhoneDirectory.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.Infrastructure.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(PhoneDirectoryDbContext context) : base(context) { }

        public async Task<IEnumerable<Contact>> GetContactsByGroupIdAsync(int groupId)
        {
            return await _context.Contacts
                .Where(c => c.ContactGroups.Any(cg => cg.GroupId == groupId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactsNotInGroupAsync(int groupId)
        {
            return await _context.Contacts
                .Where(c => !c.ContactGroups.Any(cg => cg.GroupId == groupId))
                .ToListAsync(); // <-- THIS IS THE MISSING PART
        }
    }
}