using System;
using System.Threading.Tasks;
using Gate;
using Owin;
using Utils;

namespace Case02_JustGate
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder
                .Use(Middleware.LogRequests)
                .UseGate((Func<Request, Response, Task>) App);
        }

        private async Task App(Request req, Response res)
        {
            res.ContentType = "text/plain";
            res.Write("You did a {0} at {1}", req.Method, req.Path);
        }
    }
}
