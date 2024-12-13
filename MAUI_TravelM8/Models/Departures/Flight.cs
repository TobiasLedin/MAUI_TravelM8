using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_TravelM8.Models.Departures
{
    public class Flight
    {
        public string? FlightId { get; set; }
        public string? ArrivalAirportSwedish { get; set; }
        public string? ArrivalAirportEnglish { get; set; }
        public Airlineoperator? AirlineOperator { get; set; }
        public Departuretime? DepartureTime { get; set; }
        public Locationandstatus? LocationAndStatus { get; set; }
        public Checkin? CheckIn { get; set; }
        public string[]? CodeShareData { get; set; }
        public Flightlegidentifier? FlightLegIdentifier { get; set; }
        public Viadestination[]? ViaDestinations { get; set; }
        public Remarksenglish[]? RemarksEnglish { get; set; }
        public Remarksswedish[]? RemarksSwedish { get; set; }
        public string? DiIndicator { get; set; }
    }
}

