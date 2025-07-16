using Microsoft.EntityFrameworkCore;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.Infrastructure.Context
{
    public class PhoneDirectoryDbContext : DbContext
    {
        public PhoneDirectoryDbContext(DbContextOptions<PhoneDirectoryDbContext> options)
            : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
