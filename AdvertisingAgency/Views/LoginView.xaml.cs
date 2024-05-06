using AdvertisingAgency.ViewModels;

namespace AdvertisingAgency.Views;

public sealed partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}