namespace MAUI_TravelM8.Views;

public partial class FlightSearch : ContentPage
{
    public FlightSearch(FlightSearchViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        AirportPicker.ItemDisplayBinding = new Binding("DisplayName");
    }

}
