namespace MAUI_TravelM8.Models.Departures
{
    public class DepartureData
    {
        public From? From { get; set; }
        public int NumberOfFlights { get; set; }
        public Flight[]? Flights { get; set; }
    }
}
