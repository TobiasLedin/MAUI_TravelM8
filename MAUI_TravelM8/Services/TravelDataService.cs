using MAUI_TravelM8.Models;
using MAUI_TravelM8.Models.Departures;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MAUI_TravelM8.Services
{
    /// <summary>
    /// Service that retrieves Airport and Flight data
    /// </summary>
    public class TravelDataService : ITravelDataService
    {
        private HttpClient _httpClient;
        private readonly IConfiguration _config;


        public TravelDataService(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.swedavia.se/");
            _config = config;
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

        public async Task<ActionResult<List<Flight>>> GetAirportDepartures(Airport airport, DateTime date)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"flightinfo/v2/{airport.IATA}/departures/{date:yyyy-MM-dd}");
            request.Headers.Host = "api.swedavia.se";
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Ocp-Apim-Subscription-Key", "fa800e41a2cc4306a5d7235258a7f06a"); // TODO: Store securely!

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<DepartureApiResponse>();

                if (apiResponse?.Flights != null)
                {
                    var flights = apiResponse.Flights.ToList();

                    return ActionResult<List<Flight>>.SuccessResult(flights);
                }
            }

            return ActionResult<List<Flight>>.FailureResult("Failed to retrieve departure data");
        }

    }
}
