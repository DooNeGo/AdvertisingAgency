using AdvertisingAgency.Domain;
using AdvertisingAgency.ViewModels.CreateCampaign;
using CommunityToolkit.Diagnostics;
using System.ComponentModel;

namespace AdvertisingAgency.Views.CreateCampaign;

public sealed partial class ChooseCampaignTypeView
{
    private const double SelectedThickness = 1.5;

    private readonly Color _primaryColor = (Color)Microsoft.Maui.Controls.Application.Current?.Resources["Primary"]!
        ?? ThrowHelper.ThrowArgumentNullException<Color>(nameof(_primaryColor), "Undefined primary color key in application resources");
    private readonly ChooseCampaignTypeViewModel _viewModel;

    private Border? _selection;
    private Brush? _previousBrush;
    private double? _previousThickness;

    public ChooseCampaignTypeView(ChooseCampaignTypeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
        viewModel.PropertyChanged += OnCampaignGoalChanged;
    }

    private void OnCampaignGoalChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not nameof(_viewModel.CampaignType)) return;
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        RemovePreviousSelection();
        _selection = GetSelectedBorderOrDefault();
        MemorizeDefaultState();
        MakeNextSelection();
    }

    private void MakeNextSelection()
    {
        if (_selection is null) return;
        _selection.StrokeThickness = SelectedThickness;
        _selection.Stroke = _primaryColor;
    }

    private void MemorizeDefaultState()
    {
        if (_selection is null) return;
        _previousBrush = _selection.Stroke;
        _previousThickness = _selection.StrokeThickness;
    }

    private void RemovePreviousSelection()
    {
        if (_selection is null) return;
        _selection.Stroke = _previousBrush;
        _selection.StrokeThickness = _previousThickness!.Value;
    }

    private Border? GetSelectedBorderOrDefault() =>
        (Border?)VerticalStackLayout.Children.FirstOrDefault(view =>
            ((BindableObject)view).BindingContext as CampaignType? == _viewModel.CampaignType);
}