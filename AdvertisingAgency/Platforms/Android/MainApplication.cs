using Android.App;
using Android.Runtime;
using AndroidX.AppCompat.App;

namespace AdvertisingAgency;

[Application]
public sealed class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Clipboard.SetTextAsync(e.ExceptionObject.ToString());
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
