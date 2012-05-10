using Gate;
using Owin;

namespace Case02_JustGate
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder
                .Use<AppTaskDelegate>(LogRequests)
                .RunDirect(App);
        }

        private void App(Request req, Response res)
        {
            res.ContentType = "text/plain";
            res.Write("You did a {0} at {1}", req.Method, req.Path);
            res.End();
        }

        private AppTaskDelegate LogRequests(AppTaskDelegate app)
        {
            return
                env =>
                {
                    var req = new Request(env);
                    req.TraceOutput.WriteLine("{0} {1}", req.Method, req.Path);

                    return app(env);
                };
        }
    }
}
