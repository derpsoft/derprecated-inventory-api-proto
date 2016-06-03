using System;

namespace BausCode.Api.Jobs
{
    public static class StringExtensions
    {
        public static string PercentEncode(this string input)
        {
            return Uri.EscapeDataString(input);
        }
    }
}