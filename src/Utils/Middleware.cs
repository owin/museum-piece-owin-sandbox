using System;
using System.IO;
using Owin;

namespace Utils
{
    public class Middleware
    {
        public static Func<AppDelegate, AppDelegate> LogRequests = app => call =>
        {
            object log;
            if (call.Environment.TryGetValue("host.TraceOutput", out log))
            {
                ((TextWriter)log).WriteLine("{0} {1}", call.Environment["owin.RequestMethod"], call.Environment["owin.RequestPath"]);
            }
            return app(call);
        };
    }
}