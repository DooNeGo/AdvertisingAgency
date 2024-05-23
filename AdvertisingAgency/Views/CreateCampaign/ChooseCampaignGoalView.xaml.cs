using AdvertisingAgency.ViewModels.CreateCampaign;

namespace AdvertisingAgency.Views.CreateCampaign;

public sealed partial class ChooseCampaignGoalView
{
    private Border? _selection;
    
    public ChooseCampaignGoalView(ChooseCampaignGoalViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        viewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName is not nameof(viewModel.CampaignGoal)) return;
            if (_selection != null) _selection.Stroke = Brush.White;
            
            _selection = (Border)VerticalStackLayout.Children.First(view =>
                ((BindableObject)view).BindingContext == viewModel.CampaignGoal);
            _selection.Stroke = (Color)Microsoft.Maui.Controls.Application.Current?.Resources["Primary"]!;
        };
    }
}