namespace AdvertisingAgency.Behaviours;

public sealed class OnAppearingBehaviour : Behavior<VisualElement>
{
    protected override void OnAttachedTo(VisualElement bindable)
    {
        base.OnAttachedTo(bindable);
        
        bindable.Loaded += async (_, _) =>
        {
            double opacity = bindable.Opacity;
            bindable.Opacity = 0;
            await bindable.FadeTo(opacity, 400, Easing.Default)
                .ConfigureAwait(false);
        };
    }
}