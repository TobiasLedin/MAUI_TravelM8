using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_TravelM8.Models.Departures
{
    public class Locationandstatus
    {
        public string? Terminal { get; set; }
        public string? FlightLegStatus { get; set; }
        public string? FlightLegStatusSwedish { get; set; }
        public string? FlightLegStatusEnglish { get; set; }
        public string? Gate { get; set; }
        public string? GateAction { get; set; }
        public string? GateActionSwedish { get; set; }
        public string? GateActionEnglish { get; set; }
        public DateTime GateOpenUtc { get; set; }
        public DateTime GateCloseUtc { get; set; }
    }
}
