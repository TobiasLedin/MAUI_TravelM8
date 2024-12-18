using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using MAUI_TravelM8.Models;

namespace MAUI_TravelM8.Helpers
{
    public class FlightListJsonConverter : JsonConverter<List<Flight>>
    {
        public override List<Flight> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var flights = new List<Flight>();
            using (var document = JsonDocument.ParseValue(ref reader))
            {
                foreach (var flightElement in document.RootElement.EnumerateArray())
                {
                    var flight = Flight.FromNested(flightElement);
                    if (flight != null)
                    {
                        flights.Add(flight);
                    }
                }
            }
            return flights;
        }

        public override void Write(Utf8JsonWriter writer, List<Flight> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
