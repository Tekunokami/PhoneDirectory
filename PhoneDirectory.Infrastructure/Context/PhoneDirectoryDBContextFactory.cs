using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PhoneDirectory.Infrastructure.Context;

namespace PhoneDirectory.Infrastructure
{
    public class PhoneDirectoryDbContextFactory : IDesignTimeDbContextFactory<PhoneDirectoryDbContext>
    {
        public PhoneDirectoryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PhoneDirectoryDbContext>();
            optionsBuilder.UseSqlServer("Server=TEKUNOKAMI\\SQLEXPRESS;Database=PhoneDirectoryDb;Trusted_Connection=True;");

            return new PhoneDirectoryDbContext(optionsBuilder.Options);
        }
    }
}
