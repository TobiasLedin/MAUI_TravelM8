using MAUI_TravelM8.ViewModels;

namespace MAUI_TravelM8.Views;

public partial class TrackedFlights : ContentPage
{
	public TrackedFlights(TrackedFlightsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}