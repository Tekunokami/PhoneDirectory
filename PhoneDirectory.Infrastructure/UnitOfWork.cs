using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;
using PhoneDirectory.Infrastructure.Context;
using PhoneDirectory.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace PhoneDirectory.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PhoneDirectoryDbContext _context;
        public IContactRepository Contacts { get; private set; }
        public IGroupRepository Groups { get; private set; }
        public IRepository<ContactGroup> ContactGroups { get; private set; }

        public UnitOfWork(PhoneDirectoryDbContext context)
        {
            _context = context;
            Contacts = new ContactRepository(_context);
            Groups = new GroupRepository(_context);
            ContactGroups = new GenericRepository<ContactGroup>(_context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}