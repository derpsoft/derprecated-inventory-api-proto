namespace BausCode.Api.Jobs
{
    using System;

    public static class StringExtensions
    {
        public static string PercentEncode(this string input)
        {
            return Uri.EscapeDataString(input);
        }
    }
}
