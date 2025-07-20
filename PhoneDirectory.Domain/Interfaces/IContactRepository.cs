using PhoneDirectory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneDirectory.Domain.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<Contact> GetByIdAsync(int id);
        Task<List<Contact>> GetAllAsync();
        Task AddAsync(Contact contact);
        Task<bool> UpdateAsync(Contact contact);
        Task<bool> DeleteAsync(int id);
    }

}
