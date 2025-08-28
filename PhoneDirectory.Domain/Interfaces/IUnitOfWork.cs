// PhoneDirectory.Domain/Interfaces/IUnitOfWork.cs
using System;
using System.Threading.Tasks;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IContactRepository Contacts { get; }
        IGroupRepository Groups { get; }
        IRepository<ContactGroup> ContactGroups { get; }

        Task<int> CommitAsync();
    }
}