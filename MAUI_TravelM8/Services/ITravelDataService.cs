using MAUI_TravelM8.Models;

namespace MAUI_TravelM8.Services
{
    public interface ITravelDataService
    {
        Task<ActionResult<List<Airport>>> GetAirportData();
        Task<ActionResult<FlightResponse>> GetAirportDepartures(string airportIata, DateTime date, string? flightNumber);
        Task<ActionResult<FlightResponse>> GetDepartureSpecificUpdate(string airportIata, DateTime date, string flightNumber, string continuationToken);
    }
}