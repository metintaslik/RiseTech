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

            entity.IsActive = true;
            await _context.Directories.AddAsync(entity);
            return (await _context.SaveChangesAsync() == 1) ? entity : null;
        }

        public async Task<bool> DeleteDirectoryAsync(Directory entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public IEnumerable<Directory> GetDirectories()
        {
            return _context.Directories.Where(x => x.IsActive == true).ToList();
        }

        public async Task<Directory> GetDirectoryAsync(Directory entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return await _context.Directories.Include(x => x.Contacts).FirstOrDefaultAsync();
        }

        public async Task<Directory> UpdateDirectoryAsync(Directory entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Update(entity);
            return (await _context.SaveChangesAsync() == 1) ? entity : null;
        }
    }
}