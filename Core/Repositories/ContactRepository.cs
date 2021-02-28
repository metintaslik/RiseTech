using Core.Services;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

            entity.IsActive = true;
            await _context.Contacts.AddAsync(entity);
            return (await _context.SaveChangesAsync() == 1) ? entity : null;
        }

        public async Task<Contact> InactiveContactAsync(Contact entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var contact = await _context.Contacts.FindAsync(entity.Id);
            contact.IsActive = false;
            _context.Entry(contact).State = EntityState.Modified;
            return await _context.SaveChangesAsync() == 1 ? contact : null;
        }
    }
}