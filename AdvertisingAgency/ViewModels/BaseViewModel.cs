using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Mediator;

namespace AdvertisingAgency.ViewModels;

public class BaseViewModel(IMediator mediator) : ObservableObject
{
    protected async Task UpdateCollectionAsync<T>(ObservableCollection<T> collection,
        IQuery<IAsyncEnumerable<T>> updateQuery, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(260), cancellationToken).ConfigureAwait(false);
        await App.Current!.Dispatcher.DispatchAsync(collection.Clear).WaitAsync(cancellationToken).ConfigureAwait(false);

        IAsyncEnumerable<T> enumerable = await mediator.Send(updateQuery, cancellationToken)
            .ConfigureAwait(false);

        await foreach (T item in enumerable.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            await Task.Delay(TimeSpan.FromMilliseconds(35), cancellationToken).ConfigureAwait(false);
            await App.Current.Dispatcher.DispatchAsync(() => collection.Add(item)).WaitAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}