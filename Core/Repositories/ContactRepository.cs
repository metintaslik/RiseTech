using Core.Services;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ContactRepository : IContactService
    {
        RiseTechDBContext _context;

        public ContactRepository(RiseTechDBContext context)
        {
            _context = context;
        }

        public async Task<Contact> AddContactAsync(Contact entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Contacts.AddAsync(entity);
            return (await _context.SaveChangesAsync() == 1) ? entity : null;
        }

        public async Task<bool> DeleteContactAsync(Contact entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Contacts.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public IEnumerable<Contact> GetContacts()
        {
            return _context.Contacts.ToList();
        }

        public async Task<Contact> UpdateContactAsync(Contact entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Update(entity);
            return await _context.SaveChangesAsync() == 1 ? entity : null;
        }

        public Task DeleteContactAsync()
        {
            throw new NotImplementedException();
        }
    }
}