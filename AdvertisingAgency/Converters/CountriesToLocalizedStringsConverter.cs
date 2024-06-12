using AdvertisingAgency.Domain;

namespace AdvertisingAgency.Converters;

public sealed class CountriesToLocalizedStringsConverter
    : CollectionToLocalizedStringsConverter<Country, CountryToLocalizedStringConverter>;

public sealed class LanguagesToLocalizedStringsConverter
    : CollectionToLocalizedStringsConverter<Language, LanguageToLocalizedStringConverter>;