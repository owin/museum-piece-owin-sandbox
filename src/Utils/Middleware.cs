using System.IO;
using Owin;

namespace Utils
{
    public class Middleware
    {
        public static AppTaskDelegate LogRequests(AppTaskDelegate app)
        {
            return
                env =>
                {
                    object log;
                    if (env.TryGetValue("host.TraceOutput", out log))
                    {                   
                        ((TextWriter)log).WriteLine("{0} {1}", env["owin.RequestMethod"], env["owin.RequestPath"]);
                    }
                    return app(env);
                };
        }
    }
}
