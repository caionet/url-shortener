namespace UrlShortener.Core
{
    public static class Base62EncodingExtensions
    {
        public static string EncodeToBase62(this long number)
        {
            const string alphanumeric = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        
            var result = new Stack<char>();
            while (number > 0)
            {
                result.Push(alphanumeric[(int)(number % 62)]);
                number /= 62;
            } 
        
            return new (result.ToArray());
        }
    }
}