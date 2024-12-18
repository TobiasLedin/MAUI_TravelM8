using MAUI_TravelM8.Helpers;
using MAUI_TravelM8.Models;
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
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new FlightListJsonConverter() }
        };

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

        public async Task<ActionResult<FlightResponse>> GetAirportDepartures(string airportIata, DateTime date, string? flightNumber)
        {
            var requestUri = $"flightinfo/v2/{airportIata}/departures/{date:yyyy-MM-dd}";

            if (!string.IsNullOrEmpty(flightNumber))
            {
                requestUri = $"flightinfo/v2/query?filter=airport eq '{airportIata}' and scheduled eq '{date:yyMMdd}' and flightType eq 'D' and flightId eq '{flightNumber}'";
            }

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Host = "api.swedavia.se";
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Ocp-Apim-Subscription-Key", "fa800e41a2cc4306a5d7235258a7f06a"); // TODO: Store securely!

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var flightResponse = await response.Content.ReadFromJsonAsync<FlightResponse>(_jsonOptions);

                if (flightResponse != null)
                {
                    //await StoreDepartureData(FlightResponse);

                    return ActionResult<FlightResponse>.SuccessResult(flightResponse);
                }
            }

            return ActionResult<FlightResponse>.FailureResult("Failed to retrieve departure data");
        }

        public async Task<ActionResult<FlightResponse>> GetDepartureSpecificUpdate(string airportIata, DateTime date, string flightNumber, string continuationToken)
        {

            var requestUri = 
                $"flightinfo/v2/query" +
                $"?filter=airport eq '{airportIata}' and scheduled eq '{date:yy-MM-dd}' and flightType eq 'D' and flightId eq '{flightNumber}'" +
                $"&continuationtoken={continuationToken}";


            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Host = "api.swedavia.se";
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Ocp-Apim-Subscription-Key", "fa800e41a2cc4306a5d7235258a7f06a"); // TODO: Store securely!

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var flightResponse = await response.Content.ReadFromJsonAsync<FlightResponse>(_jsonOptions);

                if (flightResponse != null)
                {
                    //await StoreDepartureData(FlightResponse);

                    return ActionResult<FlightResponse>.SuccessResult(flightResponse);
                }
            }

            return ActionResult<FlightResponse>.FailureResult("Failed to retrieve departure data");
        }

        //private JsonSerializerOptions SerialiserOptions()
        //{
        //    return new JsonSerializerOptions
        //    { 
        //        PropertyNameCaseInsensitive = true, 
        //        Converters = { new FlightListJsonConverter() }
        //    };
        //}

    }
}
