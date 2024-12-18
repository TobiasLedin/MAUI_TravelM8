using MAUI_TravelM8.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MAUI_TravelM8.Services
{
    public class LocalFlightStorage
    {
        private readonly string _fileName;
        private readonly ILogger<LocalFlightStorage> _logger;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public LocalFlightStorage(string fileName, ILogger<LocalFlightStorage> logger)
        {
            _fileName = fileName;
            _logger = logger;

            if (!File.Exists(_fileName))
            {
                File.WriteAllText(_fileName, "[]");
            }
        }

        public async Task<ActionResult<List<Flight>>> ReadTrackedFlightData()
        {
            try
            {
                var json = await File.ReadAllTextAsync(_fileName);
                var flights = JsonSerializer.Deserialize<List<Flight>>(json, _jsonOptions) ?? new List<Flight>();

                return ActionResult<List<Flight>>.SuccessResult(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading file '{_fileName}': {ex.Message}");
                return ActionResult<List<Flight>>.FailureResult("Failed to read flight data.");
            }
        }

        public async Task<ActionResult<Flight>> AddTrackedFlightData(Flight flight)
        {
            try
            {
                var result = await ReadTrackedFlightData();
                var flights = result.Success ? result.Data! : new List<Flight>();

                flights.Add(flight);
                var updatedJson = JsonSerializer.Serialize(flights, _jsonOptions);
                await File.WriteAllTextAsync(_fileName, updatedJson);

                return ActionResult<Flight>.SuccessResult(flight);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error writing to file '{_fileName}': {ex.Message}");
                return ActionResult<Flight>.FailureResult("Failed to save flight data.");
            }
        }

    }
}
