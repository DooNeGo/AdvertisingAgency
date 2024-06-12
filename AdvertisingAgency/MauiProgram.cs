using AdvertisingAgency.Application;
using AdvertisingAgency.Infrastructure;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui;
using Microsoft.Extensions.Logging;
using UraniumUI;

namespace AdvertisingAgency;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseDevExpress()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                fonts.AddFontAwesomeIconFonts();
            });

        builder.Services.AddApplication()
            .AddInfrastructure()
            .AddUI()
            .AddSingleton<IGlobalContext, GlobalContext>()
            .AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}