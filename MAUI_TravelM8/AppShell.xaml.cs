using MAUI_TravelM8.Views;

namespace MAUI_TravelM8
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(FlightList), typeof(FlightList));

            Routing.RegisterRoute(nameof(TrackedFlights), typeof(TrackedFlights));
        }
    }
}
