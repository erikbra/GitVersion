using System;

namespace GitVersion.Models
{
    public static class Utils
    {
        public static T GetCachedOrUnderlying<T>(string context, string methodName, T cached, Func<T> underlying)
        {
            Status s = cached is null ?  Status.CalledUnderlying : Status.Existed;

            var val = cached ?? underlying();

            Stats.Called(context, methodName, s);
            return val;
        }
    }
}
