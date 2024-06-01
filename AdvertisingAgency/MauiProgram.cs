using AdvertisingAgency.Application;
using AdvertisingAgency.Infrastructure;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using UraniumUI;

namespace AdvertisingAgency;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFontAwesomeIconFonts();
        });

        builder.Services.AddApplication()
            .AddInfrastructure()
            .AddUI();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}