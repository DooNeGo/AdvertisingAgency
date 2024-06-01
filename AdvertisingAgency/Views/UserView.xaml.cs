using AdvertisingAgency.ViewModels;

namespace AdvertisingAgency.Views;

public sealed partial class UserView
{
    public UserView(UserViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}