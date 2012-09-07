using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Utils
{
    using AppDelegate = Func<IDictionary<string, object>, Task>;

    public static class Middleware
    {
        public static readonly Func<AppDelegate, AppDelegate> LogRequests = next => env =>
        {
            object log;
            if (env.TryGetValue("host.TraceOutput", out log))
            {
                ((TextWriter)log).WriteLine("{0} {1}", env["owin.RequestMethod"], env["owin.RequestPath"]);
            }
            return next(env);
        };
    }
}