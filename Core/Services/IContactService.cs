using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IContactService
    {
        Task<Contact> AddContactAsync(Contact entity);
        Task<Contact> InactiveContactAsync(Contact entity);
    }
}