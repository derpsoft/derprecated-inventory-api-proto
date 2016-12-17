namespace Derprecated.Api.Jobs
{
    using System;

    public static class DateTimeExtensions
    {
        public static DateTime RoundToNearestInterval(this DateTime dt, TimeSpan d)
        {
            var f = 0;
            var m = (double) (dt.Ticks%d.Ticks)/d.Ticks;
            if (m >= 0.5)
                f = 1;
            return new DateTime(((dt.Ticks/d.Ticks) + f)*d.Ticks, dt.Kind);
        }
    }
}
