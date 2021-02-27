using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IDirectoryService
    {
        Task<Directory> AddDirectoryAsync(Directory entity);
        IEnumerable<Directory> GetDirectories();
        Task<Directory> GetDirectoryAsync(Directory entity);
        Task<Directory> UpdateDirectoryAsync(Directory entity);
        Task<bool> DeleteDirectoryAsync(Directory entity);
    }
}