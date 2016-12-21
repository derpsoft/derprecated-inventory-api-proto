namespace Derprecated.Api
{
    using System;

    public static class IntegerExtensions
    {
        public static void ThrowIfGreaterThan(this int input, int greaterThan)
        {
            if (input > greaterThan)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public static void ThrowIfLessThan(this int input, int lessThan)
        {
            if (input < lessThan)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
