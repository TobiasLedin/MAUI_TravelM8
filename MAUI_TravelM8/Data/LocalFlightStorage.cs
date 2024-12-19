using MAUI_TravelM8.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MAUI_TravelM8.Services
{
    public class LocalFlightStorage
    {
        private readonly string _filePath;
        private readonly ILogger<LocalFlightStorage> _logger;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public LocalFlightStorage(ILogger<LocalFlightStorage> logger)
        {
            _logger = logger;
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "trackedflights.json");

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public async Task<ActionResult<List<Flight>>> ReadTrackedFlights()
        {
            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                var flights = JsonSerializer.Deserialize<List<Flight>>(json, _jsonOptions) ?? new List<Flight>();

                return ActionResult<List<Flight>>.SuccessResult(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading file '{_filePath}': {ex.Message}");
                return ActionResult<List<Flight>>.FailureResult("Failed to read flight data.");
            }
        }

        public async Task<ActionResult<Flight>> AddTrackedFlight(Flight flight)
        {
            try
            {
                var result = await ReadTrackedFlights();
                var flights = result.Success ? result.Data! : new List<Flight>();

                flights.Add(flight);
                var updatedJson = JsonSerializer.Serialize(flights, _jsonOptions);
                await File.WriteAllTextAsync(_filePath, updatedJson);

                return ActionResult<Flight>.SuccessResult(flight);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error writing to file '{_filePath}': {ex.Message}");
                return ActionResult<Flight>.FailureResult("Failed to save flight data.");
            }
        }

        public async Task<ActionResult<Flight>> DeleteTrackedFlight(Flight flight)
        {
            try
            {
                var result = await ReadTrackedFlights();
                var flights = result.Success ? result.Data! : new List<Flight>();

                flights.Remove(flight);
                var updatedJson = JsonSerializer.Serialize(flights, _jsonOptions);
                await File.WriteAllTextAsync(_filePath, updatedJson);

                return ActionResult<Flight>.SuccessResult(flight);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error writing to file '{_filePath}': {ex.Message}");
                return ActionResult<Flight>.FailureResult("Failed to save flight data.");
            }
        }

        public async Task<ActionResult<List<Flight>>> DeleteAllTrackedFlights()
        {
            try
            {
                await File.WriteAllTextAsync(_filePath, "[]");

                return ActionResult<List<Flight>>.SuccessResult([]);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error writing to file '{_filePath}': {ex.Message}");
                return ActionResult<List<Flight>>.FailureResult("Failed to save flight data.");
            }
        }

    }
}
