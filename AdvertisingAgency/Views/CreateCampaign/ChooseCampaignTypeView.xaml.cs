using AdvertisingAgency.ViewModels.CreateCampaign;

namespace AdvertisingAgency.Views.CreateCampaign;

public sealed partial class ChooseCampaignTypeView
{
    private Border? _selection;

    public ChooseCampaignTypeView(ChooseCampaignTypeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        
        viewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName is not nameof(viewModel.CampaignType)) return;
            if (_selection != null) _selection.Stroke = Brush.White;
            
            _selection = (Border)VerticalStackLayout.Children.First(view =>
                ((BindableObject)view).BindingContext == viewModel.CampaignType);
            _selection.Stroke = (Color)Microsoft.Maui.Controls.Application.Current?.Resources["Primary"]!;
        };
    }
}