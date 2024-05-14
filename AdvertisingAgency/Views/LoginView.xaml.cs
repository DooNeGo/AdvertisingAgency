using AdvertisingAgency.ViewModels;

namespace AdvertisingAgency.Views;

public sealed partial class LoginView
{
	public LoginView(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void TextField_OnCompleted(object? sender, EventArgs e) => LogInButton.SendClicked();
}