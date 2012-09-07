using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Utils;

namespace Case03_JustOwin
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder
                .Use(Middleware.LogRequests)
                .Run(this);
        }

        private Task Invoke(IDictionary<string, object> env)
        {
            var data = Encoding.UTF8.GetBytes(string.Format(
                "You did a {0} at {1}",
                env["owin.RequestMethod"],
                env["owin.RequestPath"]));

            var responseHeaders = (IDictionary<string, string[]>)env["owin.ResponseHeaders"];
            var responseBody = (Stream)env["owin.ResponseBody"];

            env["owin.ResponseStatusCode"] = 200;
            responseHeaders["Content-Type"] = new[] { "text/plain" };
            responseBody.Write(data, 0, data.Length);
            return TaskHelpers.Completed();
        }
    }
}
