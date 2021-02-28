using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IDirectoryService
    {
        Task<Directory> AddDirectoryAsync(Directory entity);
        Task<IEnumerable<Directory>> GetDirectoriesAsync();
        Task<Directory> GetDirectoryAsync(Guid uuid);
        Task<Directory> UpdateDirectoryAsync(Directory entity);
        Task<bool> DeleteDirectoryAsync(Guid uuid);
    }
}