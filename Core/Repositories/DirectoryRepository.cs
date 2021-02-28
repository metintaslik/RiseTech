using Core.Services;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class DirectoryRepository : IDirectoryService
    {
        private readonly RiseTechDBContext _context;

        public DirectoryRepository(RiseTechDBContext context)
        {
            _context = context;
        }

        public async Task<Directory> AddDirectoryAsync(Directory entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Uuid = Guid.NewGuid();
            entity.IsActive = true;
            await _context.Directories.AddAsync(entity);
            return (await _context.SaveChangesAsync() == 1) ? entity : null;
        }

        public async Task<bool> DeleteDirectoryAsync(Guid uuid)
        {
            try
            {
                if (uuid == null)
                    throw new ArgumentNullException(nameof(uuid));

                var directory = await _context.Directories.Include(x=>x.Contacts).Where(x=>x.Uuid == uuid).FirstOrDefaultAsync();
                _context.Contacts.RemoveRange(directory.Contacts);
                _context.Directories.Remove(directory);
                return await _context.SaveChangesAsync() == 1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Directory>> GetDirectoriesAsync()
        {
            return await _context.Directories.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<Directory> GetDirectoryAsync(Guid uuid)
        {
            if (uuid == null)
                throw new ArgumentNullException(nameof(uuid));

            var directory = await _context.Directories.FindAsync(uuid);
            directory.Contacts = await _context.Contacts.Where(x => x.PersonId == uuid && x.IsActive == true).ToListAsync();
            return directory;
        }

        public async Task<Directory> UpdateDirectoryAsync(Directory entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Directories.Update(entity);
            return (await _context.SaveChangesAsync() == 1) ? entity : null;
        }
    }
}