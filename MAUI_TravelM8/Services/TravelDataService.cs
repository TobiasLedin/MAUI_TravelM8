using MAUI_TravelM8.Models;
using MAUI_TravelM8.Models.Departures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MAUI_TravelM8.Services
{
    /// <summary>
    /// Service that retrieves Airport and Flight data
    /// </summary>
    public class TravelDataService : ITravelDataService
    {
        private HttpClient _httpClient;
        private readonly ILogger<TravelDataService> _logger;
        private readonly string _fileName = FileSystem.AppDataDirectory + "/FlightQueryData.json";


        public TravelDataService(ILogger<TravelDataService> logger)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.swedavia.se/");
            _logger = logger;
            //TODO: Add Resilience policies
        }

 
        public async Task<ActionResult<List<Airport>>> GetAirportData()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "airportinfo/v2/airports");
            request.Headers.Add("Ocp-Apim-Subscription-Key", "8ce1f38ac8374c2e8c23178c84fa7c6e"); // TODO: Store securely!

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var airports = await response.Content.ReadFromJsonAsync<List<Airport>>();

                if (airports != null && airports.Count > 0)
                {
                    return ActionResult<List<Airport>>.SuccessResult(airports);
                }
            }

            return ActionResult<List<Airport>>.FailureResult("Failed to retrieve airport data");
        }

        public async Task<ActionResult<DepartureData>> GetAirportDepartures(Airport airport, DateTime date)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"flightinfo/v2/{airport.IATA}/departures/{date:yyyy-MM-dd}");
            request.Headers.Host = "api.swedavia.se";
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Ocp-Apim-Subscription-Key", "fa800e41a2cc4306a5d7235258a7f06a"); // TODO: Store securely!

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var departureData = await response.Content.ReadFromJsonAsync<DepartureData>();

                if (departureData != null)
                {
                    await StoreDepartureData(departureData);

                    return ActionResult<DepartureData>.SuccessResult(departureData);
                }
            }

            return ActionResult<DepartureData>.FailureResult("Failed to retrieve departure data");
        }

        private async Task StoreDepartureData(DepartureData data)
        {
            try
            {
                await File.WriteAllTextAsync(_fileName, JsonSerializer.Serialize(data));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to write to local file! Error message: {ex}");
            }
        }

        public async Task<ActionResult<DepartureData>> ReadStoredDepartureData()
        {
            if (File.Exists(_fileName))
            {
                try
                {
                    var departureDataJson = await File.ReadAllTextAsync(_fileName);
                    var departureData = JsonSerializer.Deserialize<DepartureData>(departureDataJson);

                    return ActionResult<DepartureData>.SuccessResult(departureData ?? throw new Exception());
                }
                catch (Exception ex)
                {
                    return ActionResult<DepartureData>.FailureResult($"Failed to read local file. Error: {ex}");
                }
            }

            return ActionResult<DepartureData>.FailureResult("Failed to read local file");
        }
    }
}
