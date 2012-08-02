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
                .Use<AppDelegate>(_ => App);
        }

        private Task<ResultParameters> App(CallParameters call)
        {
            var data = Encoding.UTF8.GetBytes(string.Format(
                "You did a {0} at {1}",
                call.Environment["owin.RequestMethod"],
                call.Environment["owin.RequestPath"]));

            return TaskHelpers.FromResult(new ResultParameters
            {
                Properties = new Dictionary<string, object>(),
                Status = 200,
                Headers = new Dictionary<string, string[]>
                {
                    {"Content-Type", new[] {"text/plain"}}
                },
                Body = output =>
                {
                    output.Write(data, 0, data.Length);
                    return TaskHelpers.Completed();
                }
            });
        }
    }
}
