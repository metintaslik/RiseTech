using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Report
    {
        public List<ReportCounter> HighToLowCountByLocation { get; set; }
        public List<ReportCounter> DirectoryCountByLocation { get; set; }
        public List<ReportCounter> TelephoneCountByLocation { get; set; }
    }

    public class ReportCounter
    {
        public string Location { get; set; }
        public int Counter { get; set; }
    }
}