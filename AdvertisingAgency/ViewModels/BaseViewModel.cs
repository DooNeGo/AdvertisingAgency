using CommunityToolkit.Mvvm.ComponentModel;
using Mediator;
using System.Collections.ObjectModel;

namespace AdvertisingAgency.ViewModels;

public abstract class BaseViewModel(IMediator mediator) : ObservableObject
{
    protected async Task UpdateCollectionAsync<T>(ObservableCollection<T> collection,
        IQuery<IAsyncEnumerable<T>> updateQuery, CancellationToken cancellationToken = default)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(260), cancellationToken).ConfigureAwait(false);
        await App.Current!.Dispatcher.DispatchAsync(collection.Clear).ConfigureAwait(false);

        IAsyncEnumerable<T> enumerable = await mediator.Send(updateQuery, cancellationToken)
            .ConfigureAwait(false);

        await foreach (T item in enumerable.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            await Task.Delay(TimeSpan.FromMilliseconds(35), cancellationToken).ConfigureAwait(false);
            await App.Current.Dispatcher.DispatchAsync(() => collection.Add(item)).ConfigureAwait(false);
        }
    }
}