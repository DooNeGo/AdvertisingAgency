using AdvertisingAgency.ViewModels;

namespace AdvertisingAgency.Views;

public sealed partial class AgencyInfoView
{
    public AgencyInfoView(AgencyInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}