using AsyncAwaitBestPractices;

namespace AdvertisingAgency.Behaviours;

public sealed class OnAppearingBehaviour : Behavior<VisualElement>
{
    protected override void OnAttachedTo(VisualElement bindable)
    {
        base.OnAttachedTo(bindable);

        bindable.Loaded += (_, _) =>
        {
            bindable.Opacity = 0;
            bindable.FadeTo(1, 400, Easing.Default).SafeFireAndForget();
        };
    }
}