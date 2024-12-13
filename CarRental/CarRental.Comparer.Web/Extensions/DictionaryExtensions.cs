namespace CarRental.Comparer.Web.Extensions;

public static class DictionaryExtensions
{
    public static TValue? PopValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey keyName)
        where TKey : notnull
    {
        if (dictionary.TryGetValue(keyName, out var value))
        {
            dictionary.Remove(keyName);
            return value;
        }

        return default;
    }
}
