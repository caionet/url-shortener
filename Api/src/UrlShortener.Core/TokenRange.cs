namespace UrlShortener.Core
{
    public record TokenRange
    {
        public TokenRange(long start, long end)
        {
            if(end < start)
            {
                throw new ArgumentException("End token must be greater than or equal to start token.");
            }
            
            Start = start;
            End = end;
        }
        public long Start { get;}
        public long End { get; }
    }
}