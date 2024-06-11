namespace AdvertisingAgency.Extensions;

public static class ListExtensions
{
    public static async Task AddRangeFromAsyncEnumarableAsync<T>(
        this List<T> list,
        IAsyncEnumerable<T> enumerable,
        CancellationToken cancellationToken = default)
    {
        await foreach (T item in enumerable.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            list.Add(item);
        }
    }
}