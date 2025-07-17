using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;
using PhoneDirectory.Infrastructure.Context;
using System.Threading.Tasks;

namespace PhoneDirectory.Infrastructure.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(PhoneDirectoryDbContext context) : base(context)
        {


        }

        public async Task<bool> UpdateAsync(Contact contact)
        {
            var existing = await _context.Contacts.FindAsync(contact.Id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return false;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
