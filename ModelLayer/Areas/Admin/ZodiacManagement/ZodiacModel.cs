using System;
using System.Collections.Generic;
using System.Text;

namespace ShuklaJi.ModelLayer.Areas.Admin.ZodiacManagement
{
   public class ZodiacModel
    {
        public string NameEng { get; set; }
        public int Id { get; set; }
        public string NameHindi { get; set; }
        public string Logo{ get; set; }
        public string RangeFrom { get; set; }
        public string RangeTo { get; set; }
        public string UserId { get; set; }
    }

    public class ZodiacReportModel
    {
        public int ZodiacReportId { get; set; }
        public int ZodiacId { get; set; }
        public string Description{ get; set; }
        public string ReportType{ get; set; }
        public DateTime RangeFrom { get; set; }
        public DateTime RangeTo { get; set; } 
        public string ZodiacNameEng { get; set; }
        public string ZodiacNameHindi{ get; set; }
        public int UserId { get; set; }
    }
}
