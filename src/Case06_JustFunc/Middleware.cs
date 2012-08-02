using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace Case06_JustFunc
{
    using AppAction = Func< // Call
        IDictionary<string, object>, // Environment
        IDictionary<string, string[]>, // Headers
        Stream, // Body
        Task<Tuple< //Result
            IDictionary<string, object>, // Properties
            int, // Status
            IDictionary<string, string[]>, // Headers
            Func< // CopyTo
                Stream, // Body
                Task>>>>; // Done

    public class Middleware
    {
        public static Func<AppAction, AppAction> LogRequests = app => (env, headers, body) =>
        {
            object log;
            if (env.TryGetValue("host.TraceOutput", out log))
            {
                ((TextWriter)log).WriteLine("{0} {1}", env["owin.RequestMethod"], env["owin.RequestPath"]);
            }
            return app(env, headers, body);
        };
    }
}
