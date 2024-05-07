using AdvertisingAgency.ViewModels;

namespace AdvertisingAgency.Views;

public sealed partial class RequestsView
{
    public RequestsView(RequestsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}