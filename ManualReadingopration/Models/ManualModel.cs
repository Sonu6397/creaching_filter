using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManualReadingopration.Models
{
    public class ManualModel
    {
        public int SiteId { get; set; }
        public string DataBaseName { get; set; }
        public string MeterId { get; set; }
        public Nullable<double> MainReading { get; set; }
        public Nullable<double> DG_reading { get; set; }
        public string Engineer_Name { get; set; }
        public string joiningdate { get; set; }
    }
}