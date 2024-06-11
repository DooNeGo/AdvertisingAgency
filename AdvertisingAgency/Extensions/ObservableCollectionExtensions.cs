using System.Collections.ObjectModel;

namespace AdvertisingAgency.Extensions;

public static class ObservableCollectionExtensions
{
    public static void AddRange<T>(this ObservableCollection<T> observable, IEnumerable<T> enumerable)
    {
        foreach (T item in enumerable)
        {
            observable.Add(item);
        }
    }
}
