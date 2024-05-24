using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Mediator;

namespace AdvertisingAgency.ViewModels;

public class BaseViewModel(IMediator mediator) : ObservableObject
{
    protected async Task UpdateCollectionAsync<T>(ObservableCollection<T> collection,
        IQuery<IAsyncEnumerable<T>> updateQuery, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(300), cancellationToken).ConfigureAwait(false);
        App.Current.Dispatcher.Dispatch(collection.Clear);

        IAsyncEnumerable<T> enumerable = await mediator.Send(updateQuery, cancellationToken)
            .ConfigureAwait(false);

        await foreach (T item in enumerable.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            await Task.Delay(TimeSpan.FromMilliseconds(40), cancellationToken).ConfigureAwait(false);
            App.Current.Dispatcher.Dispatch(() => collection.Add(item));
        }
    }
}