using System.Collections;

namespace AdvertisingAgency.TemplateSelectors;

public sealed class SpecialFirstItemSelector : DataTemplateSelector
{
    public DataTemplate? SpecialTemplate { get; set; }

    public DataTemplate? DefaultTemplate { get; set; }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        IEnumerable? itemsSource = BindableLayout.GetItemsSource(container);
        if (itemsSource is not IList list) return DefaultTemplate;
        return list.IndexOf(item) is 0 ? SpecialTemplate : DefaultTemplate;
    }
}