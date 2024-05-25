using AdvertisingAgency.ViewModels;
using UraniumUI.Extensions;

namespace AdvertisingAgency.Views;

public sealed partial class CreateAccountView
{
	public CreateAccountView(CreateAccountViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void Button_OnClicked(object? sender, EventArgs e) => Navigation.PopAsync().FireAndForget();
}