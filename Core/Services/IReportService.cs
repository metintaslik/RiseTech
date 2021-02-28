using Data.Models;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IReportService
    {
        Task<Report> ReporterAsync();
    }
}