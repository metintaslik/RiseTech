using Core.Services;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

            report.DirectoryCountByLocation = await (from x in _context.Contacts
                                                     group x by x.Location into loc
                                                     select new ReportCounter
                                                     {
                                                         Location = loc.Key,
                                                         Counter = loc.Select(l => l.PersonId).Distinct().Count()
                                                     }).OrderByDescending(x=>x.Counter).ToListAsync();
            return report;
        }
    }
}