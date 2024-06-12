using AdvertisingAgency.ViewModels;

namespace AdvertisingAgency.Views;

public sealed partial class CreateAccountView
{
    public CreateAccountView(CreateAccountViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}