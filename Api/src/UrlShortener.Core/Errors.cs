namespace UrlShortener.Core
{
    public static class Errors
    {
        public static Error MissingValue(string fieldName) =>  new("MISSING_VALUE", $"{fieldName} cannot be empty");
    }
}