using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_TravelM8.Models.Departures
{
    public class Departuretime
    {
        public DateTime ScheduledUtc { get; set; }
        public DateTime ActualUtc { get; set; }
        public DateTime EstimatedUtc { get; set; }
    }
}
