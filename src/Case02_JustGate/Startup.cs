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
                .Use<AppTaskDelegate>(Middleware.LogRequests)
                .RunDirect(App);
        }

        private void App(Request req, Response res)
        {
            res.ContentType = "text/plain";
            res.Write("You did a {0} at {1}", req.Method, req.Path);
            res.End();
        }
    }
}
