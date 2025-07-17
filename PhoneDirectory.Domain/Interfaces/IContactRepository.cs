using PhoneDirectory.Domain.Entities;
using System.Threading.Tasks;

namespace PhoneDirectory.Domain.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<bool> UpdateAsync(Contact contact);
        Task<bool> DeleteAsync(int id);

    }
}
