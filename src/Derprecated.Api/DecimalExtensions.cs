namespace Derprecated.Api
{
    using System;

    public static class DecimalExtensions
    {
        public static void ThrowIfGreaterThan(this decimal input, decimal greaterThan)
        {
            if (input > greaterThan)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public static void ThrowIfLessThan(this decimal input, decimal lessThan)
        {
            if (input < lessThan)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
