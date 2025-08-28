using PhoneDirectory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PhoneDirectory.Infrastructure.Context
{
    public class PhoneDirectoryDbContext : DbContext
    {
        public PhoneDirectoryDbContext(DbContextOptions<PhoneDirectoryDbContext> options)
             : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ContactGroup> ContactGroups { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactGroup>()
                .HasKey(cg => new { cg.ContactId, cg.GroupId });

            modelBuilder.Entity<ContactGroup>()
                .HasOne(cg => cg.Contact)
                .WithMany(c => c.ContactGroups) 
                .HasForeignKey(cg => cg.ContactId);

            modelBuilder.Entity<ContactGroup>()
                .HasOne(cg => cg.Group)
                .WithMany(g => g.ContactGroups) 
                .HasForeignKey(cg => cg.GroupId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
