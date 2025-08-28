// PhoneDirectory.Infrastructure.Repositories/ContactRepository.cs
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;
using PhoneDirectory.Infrastructure.Context;

namespace PhoneDirectory.Infrastructure.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(PhoneDirectoryDbContext context) : base(context)
        {
        }

    }
}