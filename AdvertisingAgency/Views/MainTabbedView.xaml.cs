namespace AdvertisingAgency.Views;

public sealed partial class MainTabbedView
{
    public MainTabbedView(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        Children.Add(serviceProvider.GetRequiredService<AgencyInfoView>());
        Children.Add(serviceProvider.GetRequiredService<CreateAdRequestView>());
    }
}