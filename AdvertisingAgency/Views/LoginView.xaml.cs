using AdvertisingAgency.ViewModels;
using AsyncAwaitBestPractices;

namespace AdvertisingAgency.Views;

public sealed partial class LoginView
{
	private readonly IServiceProvider _provider;

	public LoginView(LoginViewModel viewModel, IServiceProvider provider)
	{
		_provider = provider;
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void TextField_OnCompleted(object? sender, EventArgs e) => LogInButton.SendClicked();

	private async void Button_OnClicked(object? sender, EventArgs e)
	{
		await Entry.HideSoftInputAsync(CancellationToken.None);
		await Navigation.PushAsync(_provider.GetRequiredService<CreateAccountView>());
	}

	private void LogInButton_OnClicked(object? sender, EventArgs e) =>
		Entry.HideSoftInputAsync(CancellationToken.None).SafeFireAndForget();
}