using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_TravelM8.Models;
using MAUI_TravelM8.Services;
using System.Collections.ObjectModel;

namespace MAUI_TravelM8.ViewModels
{
    [QueryProperty(nameof(Flights), "Flights")]
    [QueryProperty(nameof(DepartureAirport), "DepartureAirport")]
    [QueryProperty(nameof(DepartureDate), "DepartureDate")]
    public partial class FlightListViewModel : ObservableObject
    {
        private readonly ITravelDataService _dataService;

        [ObservableProperty]
        public partial ObservableCollection<Flight> Flights { get; set; }

        [ObservableProperty]
        public partial string DepartureAirport { get; set; }

        [ObservableProperty]
        public partial string DepartureDate { get; set; }

        [ObservableProperty]
        public partial Flight? SelectedFlight { get; set; }

        [ObservableProperty]
        public partial ObservableCollection<Flight> SelectedFlights { get; set; }

        public FlightListViewModel(ITravelDataService dataService)
        {
            _dataService = dataService;
            DepartureAirport = string.Empty;
            SelectedFlight = null;
            SelectedFlights = [];
        }

        partial void OnSelectedFlightChanged(Flight? value)
        {
            if (value != null)
            {
                PromptAddToSelectedFlights(value);
            }
        }

        private async void PromptAddToSelectedFlights(Flight flight)
        {
            bool answer = await Shell.Current.DisplayAlert(
                "Add Flight",
                $"Do you want to track {flight.FlightId}?",
                "Yes",
                "No");

            if (answer)
            {
                if (!SelectedFlights.Any(f => f.FlightId == flight.FlightId))
                {
                    SelectedFlights.Add(flight);
                    //TODO: Redirect to Tracker page
                }
            }

            SelectedFlight = null;
        }
    }
}
        //public FlightListViewModel(ITravelDataService dataService)
        //{
        //    _dataService = dataService;
        //    DepartureAirport = string.Empty;
        //    //Flights = [];
        //    SelectedFlight = null;
        //    //_ = Task.Run(LoadStoredFlightData);
        //}

        //private async Task LoadStoredFlightData()
        //{
        //    if (Flights.Count == 0)
        //    {
        //        var result = await _dataService.ReadStoredDepartureData();

        //        if (result.Success)
        //        {
        //            Flights = new ObservableCollection<Flight>(result.Data?.Flights!);
        //            DepartureAirport = result.Data?.From?.DepartureAirportSwedish!;
        //            DepartureDate = result.Data?.From?.FlightDepartureDate!;
        //        }

        //        //TODO: Error message handling
        //    }
        //}

    

