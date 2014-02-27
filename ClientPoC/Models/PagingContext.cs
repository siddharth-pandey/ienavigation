using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientPoC.Model;

namespace ClientPoC.Model
{
    public class PagingContext : QueryStatistics
    {
        public Int32 CurrentPage { get; set; }
        public Int32 ResultsPerPage { get; set; }
        public Int32 SkippedResults { get; set; }
        public Int32 TotalResults { get; set; }
        public Int32 Skip { get; set; }
        public Int32 CurrentResultFrom { get; set; }
        public Int32 CurrentResultTo { get; set; }
        public Int32 Pages { get; set; }
        public String SearchDescription { get; set; }
    }
}