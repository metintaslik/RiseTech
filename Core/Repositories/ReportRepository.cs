using Core.Services;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ReportRepository : IReportService
    {
        private readonly RiseTechDBContext _context;
        private readonly Report report;

        public ReportRepository(RiseTechDBContext context)
        {
            _context = context;
            report = new Report();
        }

        public async Task<Report> ReporterAsync()
        {
            report.HighToLowCountByLocation = await _context.Contacts
                .GroupBy(x => x.Location)
                .Select(x => new ReportCounter { Location = x.Key, Counter = x.Count() })
                .OrderByDescending(x => x.Counter).ToListAsync();

            report.TelephoneCountByLocation = await _context.Contacts
                .GroupBy(x => x.Location)
                .Select(x => new ReportCounter { Location = x.Key, Counter = x.Count() })
                .OrderByDescending(x => x.Counter).ToListAsync();

            report.DirectoryCountByLocation = await _context.Contacts
                .GroupBy(x => x.Location)
                .Select(x => new ReportCounter { Location = x.Key, Counter = x.Distinct().Count() }).ToListAsync();

            return report;
        }
    }
}