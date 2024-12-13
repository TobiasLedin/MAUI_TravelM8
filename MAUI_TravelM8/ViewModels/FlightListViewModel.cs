using CommunityToolkit.Mvvm.ComponentModel;
using MAUI_TravelM8.Models;
using MAUI_TravelM8.Models.Departures;
using System.Collections.ObjectModel;

namespace MAUI_TravelM8.ViewModels
{
    public partial class FlightListViewModel : ObservableObject
    {

        [ObservableProperty]
        public partial ObservableCollection<Flight> Flights { get; set; }

        [ObservableProperty]
        public partial string DepartureAirport { get; set; } = string.Empty;

        public FlightListViewModel()
        {
            Flights = []; 
        }

    }
}
