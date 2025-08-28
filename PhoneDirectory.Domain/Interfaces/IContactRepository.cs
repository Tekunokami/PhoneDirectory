using PhoneDirectory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneDirectory.Domain.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<IEnumerable<Contact>> GetContactsByGroupIdAsync(int groupId);
        Task<IEnumerable<Contact>> GetContactsNotInGroupAsync(int groupId);
    }
}