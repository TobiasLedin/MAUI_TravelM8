using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_TravelM8.Models.Departures
{
    public class Flightlegidentifier
    {
        public string? Callsign { get; set; }
        public string? FlightId { get; set; }
        public DateTime FlightDepartureDateUtc { get; set; }
        public string? DepartureAirportIata { get; set; }
        public string? ArrivalAirportIata { get; set; }
        public string? DepartureAirportIcao { get; set; }
        public string? ArrivalAirportIcao { get; set; }
        public string? AircraftRegistration { get; set; }
        public string? SsrCode { get; set; }
    }
}
