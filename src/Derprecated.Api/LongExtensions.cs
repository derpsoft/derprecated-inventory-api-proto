namespace Derprecated.Api
{
    using System;

    public static class LongExtensions
    {
      public static void ThrowIfGreaterThan(this long input, long greaterThan)
      {
          if (input > greaterThan)
          {
              throw new ArgumentOutOfRangeException();
          }
      }

      public static void ThrowIfLessThan(this long input, long lessThan)
      {
          if (input < lessThan)
          {
              throw new ArgumentOutOfRangeException();
          }
      }

      public static void ThrowIfGreaterThan(this ulong input, ulong greaterThan)
      {
          if (input > greaterThan)
          {
              throw new ArgumentOutOfRangeException();
          }
      }

      public static void ThrowIfLessThan(this ulong input, ulong lessThan)
      {
          if (input < lessThan)
          {
              throw new ArgumentOutOfRangeException();
          }
      }
    }
}
