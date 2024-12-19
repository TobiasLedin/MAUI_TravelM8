using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_TravelM8.Models;
using MAUI_TravelM8.Services;
using MAUI_TravelM8.Views;
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
        public partial bool HasFlights { get; set; }

        [ObservableProperty]
        public partial bool HasNoFlights { get; set; }

        public FlightListViewModel(ITravelDataService dataService)
        {
            _dataService = dataService;
            Flights = [];
            DepartureDate = string.Empty;
            DepartureAirport = string.Empty;
            SelectedFlight = null;
        }

        partial void OnSelectedFlightChanged(Flight? value)
        {
            if (value != null)
            {
                PromptAddToTrackedFlights(value);
            }
        }

        partial void OnFlightsChanged(ObservableCollection<Flight> value)
        {
            HasNoFlights = value.Count == 0;
            HasFlights = value.Count > 0;
        }

        private async void PromptAddToTrackedFlights(Flight flight)
        {
            bool answer = await Shell.Current.DisplayAlert(
                "Add Flight",
                $"Do you want to track {flight.FlightId}?",
                "Yes",
                "No");

            if (answer)
            {
                try
                {
                    var response = await _dataService.GetAirportDepartures(flight.FlightLegIdentifier!.DepartureAirportIata!, flight.DepartureTime!.SchDepTimeLocal, flight.FlightId);

                    if (response.Success)
                    {
                        var departureWithToken = response.Data!.Flights![0];
                        departureWithToken.ContinuationToken = response.Data?.ContinuationToken;
                        departureWithToken.DepartureAirport = DepartureAirport;

                        var navigationParam = new Dictionary<string, object>()
                        {
                            ["AddFlight"] = departureWithToken
                        };

                        await Shell.Current.GoToAsync(nameof(TrackedFlights), navigationParam);
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert(
                            "",
                            "Flight is already tracked",
                            "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
                finally
                {
                    SelectedFlight = null;
                }

            }
        }
    }
}




