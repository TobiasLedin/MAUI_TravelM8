using MAUI_TravelM8.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAUI_TravelM8.Models
{
    public class FlightResponse
    {
        [JsonPropertyName("flights")]
        [JsonConverter(typeof(FlightListJsonConverter))]
        public List<Flight>? Flights { get; set; }

        [JsonPropertyName("continuationtoken")]
        public string? ContinuationToken { get; set; }

        [JsonPropertyName("from")]
        public FromDetails? From { get; set; }

        [JsonPropertyName("numberOfFlights")]
        public int? NumberOfFlights { get; set; }
    }

    public class Flight
    {
        [JsonPropertyName("flightId")]
        public string? FlightId { get; set; }

        [JsonPropertyName("arrivalAirportSwedish")]
        public string? ArrivalAirportSwedish { get; set; }

        [JsonPropertyName("arrivalAirportEnglish")]
        public string? ArrivalAirportEnglish { get; set; }

        [JsonPropertyName("airlineOperator")]
        public AirlineOperator? AirlineOperator { get; set; }

        [JsonPropertyName("departureTime")]
        public DepartureTime? DepartureTime { get; set; }

        [JsonPropertyName("locationAndStatus")]
        public LocationAndStatus? LocationAndStatus { get; set; }

        [JsonPropertyName("checkIn")]
        public CheckIn? CheckIn { get; set; }

        [JsonPropertyName("codeShareData")]
        public List<string>? CodeShareData { get; set; }

        [JsonPropertyName("flightLegIdentifier")]
        public FlightLegIdentifier? FlightLegIdentifier { get; set; }

        [JsonPropertyName("viaDestinations")]
        public List<ViaDestination>? ViaDestinations { get; set; }

        [JsonPropertyName("remarksEnglish")]
        public List<Remark>? RemarksEnglish { get; set; }

        [JsonPropertyName("remarksSwedish")]
        public List<Remark>? RemarksSwedish { get; set; }

        [JsonPropertyName("diIndicator")]
        public string? DiIndicator { get; set; }

        public string? ContinuationToken { get; set; }

        public string? DepartureAirport { get; set; }

        public static Flight FromNested(JsonElement element)
        {
            if (element.TryGetProperty("departure", out JsonElement departureElement))
            {
                return JsonSerializer.Deserialize<Flight>(departureElement.GetRawText()) ?? throw new Exception("JsonSerializer error");
            }
            return JsonSerializer.Deserialize<Flight>(element.GetRawText()) ?? throw new Exception("JsonSerializer error");
        }
    }

    public class AirlineOperator
    {
        [JsonPropertyName("iata")]
        public string? Iata { get; set; }

        [JsonPropertyName("icao")]
        public string? Icao { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class DepartureTime
    {
        [JsonPropertyName("scheduledUtc")]
        public DateTime ScheduledUtc { get; set; }

        [JsonPropertyName("estimatedUtc")]
        public DateTime? EstimatedUtc { get; set; }

        [JsonPropertyName("actualUtc")]
        public DateTime? ActualUtc { get; set; }

        public DateTime SchDepTimeLocal => ScheduledUtc.ToLocalTime();
        public DateTime? EstDepTimeLocal => EstimatedUtc?.ToLocalTime();
        public DateTime? ActDepTimeLocal => ActualUtc?.ToLocalTime();

        public bool IsEstDepTimeLocalVisible => EstimatedUtc.HasValue;
        public bool IsActDepTimeLocalVisible => ActualUtc.HasValue;
    }

    public class LocationAndStatus
    {
        [JsonPropertyName("terminal")]
        public string? Terminal { get; set; }

        [JsonPropertyName("gate")]
        public string? Gate { get; set; }

        [JsonPropertyName("gateAction")]
        public string? GateAction { get; set; }

        [JsonPropertyName("gateActionSwedish")]
        public string? GateActionSwedish { get; set; }

        [JsonPropertyName("gateActionEnglish")]
        public string? GateActionEnglish { get; set; }

        [JsonPropertyName("gateOpenUtc")]
        public DateTime? GateOpenUtc { get; set; }

        [JsonPropertyName("gateCloseUtc")]
        public DateTime? GateCloseUtc { get; set; }

        [JsonPropertyName("flightLegStatus")]
        public string? FlightLegStatus { get; set; }

        [JsonPropertyName("flightLegStatusSwedish")]
        public string? FlightLegStatusSwedish { get; set; }

        [JsonPropertyName("flightLegStatusEnglish")]
        public string? FlightLegStatusEnglish { get; set; }
    }

    public class CheckIn
    {
        [JsonPropertyName("checkInStatus")]
        public string? CheckInStatus { get; set; }

        [JsonPropertyName("checkInStatusSwedish")]
        public string? CheckInStatusSwedish { get; set; }

        [JsonPropertyName("checkInStatusEnglish")]
        public string? CheckInStatusEnglish { get; set; }

        [JsonPropertyName("checkInDeskFrom")]
        public int? CheckInDeskFrom { get; set; }

        [JsonPropertyName("checkInDeskTo")]
        public int? CheckInDeskTo { get; set; }
    }

    public class FlightLegIdentifier
    {
        [JsonPropertyName("ifplId")]
        public string? IfplId { get; set; }

        [JsonPropertyName("callsign")]
        public string? Callsign { get; set; }

        [JsonPropertyName("aircraftRegistration")]
        public string? AircraftRegistration { get; set; }

        [JsonPropertyName("ssrCode")]
        public string? SsrCode { get; set; }

        [JsonPropertyName("flightId")]
        public string? FlightId { get; set; }

        [JsonPropertyName("flightDepartureDateUtc")]
        public DateTime? FlightDepartureDateUtc { get; set; }

        [JsonPropertyName("departureAirportIata")]
        public string? DepartureAirportIata { get; set; }

        [JsonPropertyName("arrivalAirportIata")]
        public string? ArrivalAirportIata { get; set; }

        [JsonPropertyName("departureAirportIcao")]
        public string? DepartureAirportIcao { get; set; }

        [JsonPropertyName("arrivalAirportIcao")]
        public string? ArrivalAirportIcao { get; set; }
    }

    public class ViaDestination
    {
        [JsonPropertyName("airportIATA")]
        public string? AirportIATA { get; set; }

        [JsonPropertyName("airportSwedish")]
        public string? AirportSwedish { get; set; }

        [JsonPropertyName("airportEnglish")]
        public string? AirportEnglish { get; set; }
    }

    public class Remark
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("indicator")]
        public string? Indicator { get; set; }
    }

    public class FromDetails
    {
        [JsonPropertyName("departureAirportIata")]
        public string? DepartureAirportIata { get; set; }

        [JsonPropertyName("departureAirportIcao")]
        public string? DepartureAirportIcao { get; set; }

        [JsonPropertyName("departureAirportSwedish")]
        public string? DepartureAirportSwedish { get; set; }

        [JsonPropertyName("departureAirportEnglish")]
        public string? DepartureAirportEnglish { get; set; }

        [JsonPropertyName("flightDepartureDate")]
        public string? FlightDepartureDate { get; set; }
    }

}
