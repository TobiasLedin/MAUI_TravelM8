using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_TravelM8.Models;
using MAUI_TravelM8.Services;
using MAUI_TravelM8.Views;
using System.Collections.ObjectModel;

namespace MAUI_TravelM8
{
    public partial class FlightSearchViewModel : ObservableObject
    {

        private readonly ITravelDataService _dataService;

        [ObservableProperty]
        public partial bool IsLoading { get; set; }

        [ObservableProperty]
        public partial string? FlightNumberInput { get; set; }

        [ObservableProperty]
        public partial bool IsDatePickerVisible { get; set; }

        [ObservableProperty]
        public partial DateTime MinAllowedDate { get; set; }

        [ObservableProperty]
        public partial DateTime SelectedDate { get; set; }

        [ObservableProperty]
        public partial Airport SelectedAirport { get; set; }

        [ObservableProperty]
        public partial bool IsSearchBtnEnabled { get; set; }

        [ObservableProperty]
        public partial string ErrorMessage { get; set; } = string.Empty;

        public IEnumerable<Airport> Airports { get; set; } = new List<Airport>()
        {
            new() { IATA = "ARN", Name = "STOCKHOLM ARLANDA", DisplayName ="Stockholm ARN" },
            new() { IATA = "BMA", Name = "STOCKHOLM BROMMA", DisplayName ="Stockholm BMA" },
            new() { IATA = "GOT", Name = "GÖTEBORG LANDVETTER", DisplayName ="Göteborg" },
            new() { IATA = "MMX", Name = "MALMÖ AIRPORT", DisplayName ="Malmö" },
            new() { IATA = "UME", Name = "UMEÅ", DisplayName ="Umeå" },
            new() { IATA = "OSD", Name = "ÅRE ÖSTERSUND", DisplayName ="Åre Östersund" },
            new() { IATA = "LLA", Name = "LULEÅ KALLAX", DisplayName ="Luleå" },
            new() { IATA = "VBY", Name = "VISBY AIRPORT", DisplayName ="Visby" },
            new() { IATA = "KRN", Name = "KIRUNA", DisplayName ="Kiruna" },
            new() { IATA = "RNB", Name = "RONNEBY", DisplayName ="Ronneby" },
        };

        public FlightSearchViewModel(ITravelDataService dataService)
        {
            _dataService = dataService;
            SelectedDate = DateTime.UtcNow;
            MinAllowedDate = DateTime.UtcNow;
            SelectedAirport = Airports.First(x => x.IATA == "ARN");
        }

        [RelayCommand]
        private async Task SearchFlights()
        {
            IsLoading = true;

            try
            {
                var result = await _dataService.GetAirportDepartures(SelectedAirport.IATA!, SelectedDate, FlightNumberInput);

                if (result.Success && result.Data != null && result.Data.Flights?.Count > 0)
                {
                    var flights = result.Data.Flights
                                    .Where(x =>
                                        x.DepartureTime.SchDepTimeLocal.Date == SelectedDate.Date &&
                                        x.LocationAndStatus.FlightLegStatus == "SCH")
                                    .OrderBy(x => x.DepartureTime?.ScheduledUtc);

                    if (!flights.Any())
                    {
                        await Shell.Current.DisplayAlert("", "No flights found", "OK");
                        return;
                    }

                    var navigationParam = new Dictionary<string, object>
                    {
                        ["DepartureAirport"] = SelectedAirport.DisplayName!,
                        ["DepartureDate"] = result.Data.From?.FlightDepartureDate!,
                        ["Flights"] = new ObservableCollection<Flight>(
                            result.Data.Flights
                                .Where(x =>
                                    x.DepartureTime.SchDepTimeLocal.Date == SelectedDate.Date &&
                                    x.LocationAndStatus.FlightLegStatus == "SCH")
                                .OrderBy(x => x.DepartureTime?.ScheduledUtc)) ?? new ObservableCollection<Flight>(),
                        
                    };

                    await Shell.Current.GoToAsync(nameof(FlightList), navigationParam);
                       
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }

    }
}
