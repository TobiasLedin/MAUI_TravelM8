using MAUI_TravelM8.ViewModels;

namespace MAUI_TravelM8.Views;

public partial class FlightList : ContentPage
{
    public FlightList(FlightListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}