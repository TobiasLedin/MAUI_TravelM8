using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_TravelM8.Models.Departures
{
    public class Checkin
    {
        public int CheckInDeskFrom { get; set; }
        public int CheckInDeskTo { get; set; }
        public string? CheckInStatus { get; set; }
        public string? CheckInStatusSwedish { get; set; }
        public string? CheckInStatusEnglish { get; set; }
    }
}
