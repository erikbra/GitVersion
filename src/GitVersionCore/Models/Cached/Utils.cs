using System;

namespace GitVersion.Models
{
    public static class Utils
    {
        public static T GetCachedOrUnderlying<T>(string fullMethodName, T cached, Func<T> underlying)
        {
            var msg = fullMethodName;

            Status s = cached is null ?  Status.CalledUnderlying : Status.Existed;

            var val = cached ?? underlying();

            Stats.Called(msg, s);
            return val;
        }
    }
}
