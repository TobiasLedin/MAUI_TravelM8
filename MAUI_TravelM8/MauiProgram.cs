using MAUI_TravelM8.Services;
using MAUI_TravelM8.ViewModels;
using MAUI_TravelM8.Views;
using Microsoft.Extensions.Logging;

namespace MAUI_TravelM8
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    //fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    //fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddFont("Roboto-Black.ttf", "RobotoBlack");
                    fonts.AddFont("Roboto-BlackItalic.ttf", "RobotoBlackItalic");
                    fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                    fonts.AddFont("Roboto-BoldItalic.ttf", "RobotoBoldItalic");
                    fonts.AddFont("Roboto-Italic.ttf", "RobotoItalic");
                    fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                    fonts.AddFont("Roboto-Light.ttf", "RobotoLight");
                    fonts.AddFont("Roboto-LightItalic.ttf", "RobotoLightItalic");
                    fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
                    fonts.AddFont("Roboto-MediumItalic.ttf", "RobotoMediumItalic");
                    fonts.AddFont("Roboto-Thin.ttf", "RobotoThin");
                    fonts.AddFont("Roboto-ThinItalic.ttf", "RobotoThinItalic");
                });

            builder.Services.AddSingleton<ITravelDataService, TravelDataService>();
            builder.Services.AddSingleton<FlightSearchViewModel>();

            Routing.RegisterRoute(nameof(FlightList), typeof(FlightList));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
