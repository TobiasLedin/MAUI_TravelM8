using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_TravelM8.Models;
using MAUI_TravelM8.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MAUI_TravelM8.ViewModels
{
    [QueryProperty(nameof(AddFlight), "AddFlight")]
    public partial class TrackedFlightsViewModel : ObservableObject
    {
        private readonly LocalFlightStorage _localstorage;

        [ObservableProperty]
        public partial ObservableCollection<Flight> Flights { get; set; }

        [ObservableProperty]
        public partial Flight? AddFlight { get; set; }

        [ObservableProperty]
        public partial bool HasFlights { get; set; }

        [ObservableProperty]
        public partial bool HasNoFlights { get; set; }

        public TrackedFlightsViewModel(LocalFlightStorage localstorage)
        {
            _localstorage = localstorage;
            Flights = [];
        }

        partial void OnAddFlightChanged(Flight? value)
        {
            if (value != null)
            {
                Flights.Add(value);

                HasFlights = Flights.Count > 0;
                HasNoFlights = Flights.Count == 0;
            }
        }

        partial void OnFlightsChanged(ObservableCollection<Flight> value)
        {
            HasNoFlights = value.Count == 0;
            HasFlights = value.Count > 0;
        }

        [RelayCommand]
        private void DeleteFlight(Flight? flight)
        {
            if (flight != null)
            {
                Flights.Remove(flight);
            }
        }

        private async void LoadTrackedFlights()
        {

        }

        private async void StoreTrackedFlights()
        {

        }

    }
}
