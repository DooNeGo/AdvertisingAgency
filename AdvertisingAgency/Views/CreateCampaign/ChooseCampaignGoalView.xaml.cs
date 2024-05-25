using AdvertisingAgency.ViewModels.CreateCampaign;

namespace AdvertisingAgency.Views.CreateCampaign;

public sealed partial class ChooseCampaignGoalView
{
    private readonly Color _primaryColor = (Color)Microsoft.Maui.Controls.Application.Current?.Resources["Primary"]!;
    private const double SelectedThickness = 1.5;
    
    private Border? _selection;
    private Brush? _previousBrush;
    private double? _previousThickness;

    public ChooseCampaignGoalView(ChooseCampaignGoalViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        
        viewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName is not nameof(viewModel.CampaignGoal)) return;
            if (_selection is not null)
            {
                _selection.Stroke = _previousBrush;
                _selection.StrokeThickness = _previousThickness!.Value;
            }
            
            _selection = (Border)VerticalStackLayout.Children.First(view =>
                ((BindableObject)view).BindingContext == viewModel.CampaignGoal);
            _previousBrush = _selection.Stroke;
            _previousThickness = _selection.StrokeThickness;
            
            _selection.StrokeThickness = SelectedThickness;
            _selection.Stroke = _primaryColor;
        };
    }
}