using CommunityToolkit.Maui.Animations;

namespace AdvertisingAgency.Behaviours;

internal sealed class CollectionAppearAnimation : Behavior
{
    protected override void OnAttachedTo(BindableObject bindable)
    {
        if (bindable is CollectionView collectionView)
        {
            collectionView.ChildAdded += CollectionView_ChildAdded;
        }
    }

    private void CollectionView_ChildAdded(object? sender, ElementEventArgs e)
    {
        if (e.Element is not VisualElement element) return;
        
    }
}
