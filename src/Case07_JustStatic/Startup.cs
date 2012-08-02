using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using Owin;
using Utils;
using Gate.Middleware;
using Gate;

namespace Case07_JustStatic
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder
                .Use(Middleware.LogRequests)
                .Use(RootIsAlphaHtml)
                .UseStatic("public");
        }

        AppDelegate RootIsAlphaHtml(AppDelegate app)
        {
            return call =>
            {
                var req = new Request(call);
                if (req.Path == "/")
                {
                    req.Path = "/Alpha.html";
                }
                return app(call);
            };
        }
    }
}
