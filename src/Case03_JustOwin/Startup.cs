using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Owin;
using Utils;

namespace Case03_JustOwin
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder
                .Use<AppTaskDelegate>(Middleware.LogRequests)
                .Use<AppDelegate>(_ => App);
        }

        private void App(IDictionary<string, object> env, ResultDelegate result, Action<Exception> fault)
        {
            var data = new ArraySegment<byte>(Encoding.UTF8.GetBytes(string.Format(
                "You did a {0} at {1}",
                env["owin.RequestMethod"],
                env["owin.RequestPath"])));

            result(
                "200 OK",
                new Dictionary<string, IEnumerable<string>>
                {
                    {"Content-Type", new[] {"text/plain"}}
                },
                (write, flush, end, cancellationToken) =>
                {
                    write(data);
                    end(null);
                });
        }
    }
}
