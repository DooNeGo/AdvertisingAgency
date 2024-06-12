using CommunityToolkit.Diagnostics;
using System.Collections.ObjectModel;
using System.Globalization;

namespace AdvertisingAgency.CustomCollections
{
    public sealed class LocalizedCollection<TFrom, TElementConveter> : ObservableCollection<string>
        where TElementConveter : IValueConverter, new()
    {
        private readonly TElementConveter _converter = new();

        public LocalizedCollection() { }

        public LocalizedCollection(IEnumerable<TFrom> enumerable) => AddRange(enumerable);

        public TFrom GetConvertedBack(int index) => ConvertBack(this[index]);

        public void Add(TFrom item) => Add(Convert(item));

        public void AddRange(IEnumerable<TFrom> collection) => AddRange(collection.Select(Convert));

        public void AddRange(IEnumerable<string> collection)
        {
            foreach (string item in collection)
            {
                Add(item);
            }
        }

        public List<T> ConvertAll<T>(Func<string, T> func) => this.Select(func).ToList();

        public List<TFrom> ToDefaultList() => ConvertAll(ConvertBack);

        private TFrom ConvertBack(string item) => (TFrom)_converter
            .ConvertBack(item, typeof(TFrom), null, CultureInfo.CurrentCulture)!
            ?? ThrowHelper.ThrowInvalidDataException<TFrom>($"ConvertedBack value was null({item})");

        private string Convert(TFrom item) => (string)_converter
            .Convert(item, typeof(string), null, CultureInfo.CurrentCulture)!
            ?? ThrowHelper.ThrowInvalidDataException<string>($"Converted value was null({item})");
    }
}
