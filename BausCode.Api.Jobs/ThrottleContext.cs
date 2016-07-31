using System;

namespace BausCode.Api.Jobs
{
    public static class ThrottleContext
    {
        public static T Execute<T>(Counter throttle, Func<T> func)
        {
            var result = default(T);
            if (!throttle.IsMax())
            {
                result = func.Invoke();
                throttle.Increment();
            }
            return result;
        }
    }
}