using MAUI_TravelM8.Models;
using MAUI_TravelM8.Models.Departures;

namespace MAUI_TravelM8.Services
{
    public interface ITravelDataService
    {
        Task<ActionResult<List<Airport>>> GetAirportData();
        Task<ActionResult<List<Flight>>> GetAirportDepartures(Airport airport, DateTime date);

    }
}