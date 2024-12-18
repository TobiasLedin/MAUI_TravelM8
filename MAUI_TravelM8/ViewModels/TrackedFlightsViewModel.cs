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
            LoadTrackedFlights();
        }


        partial void OnAddFlightChanged(Flight? value)
        {
            if (value != null)
            {
                Flights.Add(value);
                AddTrackedFlight(value);

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
                DeleteTrackedFlight(flight);
            }
        }

        private async void LoadTrackedFlights()
        {
            var readResult = await _localstorage.ReadTrackedFlights();

            if (readResult.Success && readResult.Data != null)
            {
                Flights = new ObservableCollection<Flight>(readResult.Data);
                return;
            }

            if (!readResult.Success)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to load saved flights from localstorage", "OK");
            }

            OnPropertyChanged(nameof(Flights));
        }

        private async void AddTrackedFlight(Flight flight)
        {
            var addResult = await _localstorage.AddTrackedFlight(flight);

            if (!addResult.Success)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to save flight to localstorage", "OK");
            }
        }

        private async void DeleteTrackedFlight(Flight flight)
        {
            var deleteResult = await _localstorage.DeleteTrackedFlight(flight);

            if (!deleteResult.Success)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to delete flight from localstorage", "OK");
            }
        }

    }
}
