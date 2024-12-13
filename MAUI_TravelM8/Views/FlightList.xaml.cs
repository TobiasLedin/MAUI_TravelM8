using MAUI_TravelM8.ViewModels;

namespace MAUI_TravelM8.Views;

[QueryProperty("ViewModel", "FlightListViewModel")]
public partial class FlightList : ContentPage
{
    public FlightListViewModel ViewModel
	{
		set => BindingContext = value;
	}

    public FlightList()
	{
		InitializeComponent();
	}
}