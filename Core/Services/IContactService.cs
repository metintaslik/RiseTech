using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IContactService
    {
        Task<Contact> AddContactAsync(Contact entity);
        IEnumerable<Contact> GetContacts();
        Task<Contact> UpdateContactAsync(Contact entity);
        Task<bool> DeleteContactAsync(Contact entity);
    }
}