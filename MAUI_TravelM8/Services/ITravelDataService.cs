using MAUI_TravelM8.Models;

namespace MAUI_TravelM8.Services
{
    public interface ITravelDataService
    {
        Task<ActionResult<List<Airport>>> GetAirportData();
        Task<ActionResult<FlightResponse>> GetAirportDepartures(Airport airport, DateTime date, string? flightNumber);

        Task<ActionResult<FlightResponse>> GetDepartureSpecificUpdate(Airport airport, DateTime date, string flightNumber, string continuationToken);
        Task<ActionResult<FlightResponse>> ReadStoredDepartureData();

    }
}