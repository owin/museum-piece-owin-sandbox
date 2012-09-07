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
                .UseGate((Action<Request>)RootIsAlphaHtml)
                .UseStatic("public");
        }

        void RootIsAlphaHtml(Request req)
        {
            if (req.Path == "/")
            {
                req.Path = "/Alpha.html";
            }
        }
    }
}
