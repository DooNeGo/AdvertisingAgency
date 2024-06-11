using AdvertisingAgency.Domain;
using AdvertisingAgency.ViewModels.CreateCampaign;
using CommunityToolkit.Diagnostics;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AdvertisingAgency.Views.CreateCampaign;

public sealed partial class ChooseCampaignGoalView
{
    private const double SelectedThickness = 1.5;

    private readonly Color _primaryColor = (Color)Microsoft.Maui.Controls.Application.Current?.Resources["Primary"]!
        ?? ThrowHelper.ThrowArgumentNullException<Color>(nameof(_primaryColor), "Undefined primary color key in application resources");
    private readonly ChooseCampaignGoalViewModel _viewModel;

    private Border? _selection;
    private Brush? _previousBrush;
    private double? _previousThickness;

    public ChooseCampaignGoalView(ChooseCampaignGoalViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
        viewModel.PropertyChanged += OnCampaignGoalChanged;
        viewModel.CampaignGoals.CollectionChanged += OnCampaignGoalsChanged;
    }

    private void OnCampaignGoalsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (_viewModel.CampaignGoal is not null)
        {
            UpdateSelection();
        }
    }

    private void OnCampaignGoalChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(_viewModel.CampaignGoal))
        {
            UpdateSelection();
        }
    }

    private void UpdateSelection()
    {
        if (_selection is not null)
        {
            _selection.Stroke = _previousBrush;
            _selection.StrokeThickness = _previousThickness!.Value;
        }

        _selection = GetSelectedBorderOrDefault();
        if (_selection is null) return;

        _previousBrush = _selection.Stroke;
        _previousThickness = _selection.StrokeThickness;

        _selection.StrokeThickness = SelectedThickness;
        _selection.Stroke = _primaryColor;
    }

    private Border? GetSelectedBorderOrDefault() =>
        (Border?)VerticalStackLayout.Children.FirstOrDefault(view =>
            (((BindableObject)view).BindingContext as CampaignGoal)?.Id == _viewModel.CampaignGoal?.Id);
}