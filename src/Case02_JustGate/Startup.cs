using Gate;
using Owin;

namespace Case02_JustGate
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.RunDirect(
                (req, res) =>
                {
                    res.ContentType = "text/plain";
                    res.Write("You did a {0} at {1}", req.Method, req.Path);
                    res.End();
                });
        }
    }
}
