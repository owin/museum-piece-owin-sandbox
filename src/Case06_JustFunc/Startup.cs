using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace Case06_JustFunc
{
    using AppAction = Func< // Call
        IDictionary<string, object>, // Environment
        IDictionary<string, string[]>, // Headers
        Stream, // Body
        Task<Tuple< //Result
            IDictionary<string, object>, // Properties
            int, // Status
            IDictionary<string, string[]>, // Headers
            Func< // CopyTo
                Stream, // Body
                Task>>>>; // Done

    using ResultTuple = Tuple< //Result
            IDictionary<string, object>, // Properties
            int, // Status
            IDictionary<string, string[]>, // Headers
            Func< // CopyTo
                Stream, // Body
                Task>>;

    public class Startup
    {
        public AppAction Configuration(IDictionary<string, object> props)
        {
            AppAction pipeline = App;

            pipeline = Middleware.LogRequests(pipeline);

            return pipeline;
        }



        private async Task<ResultTuple> App(
            IDictionary<string, object> env,
            IDictionary<string, string[]> headers,
            Stream body)
        {
            var data = Encoding.UTF8.GetBytes(string.Format(
                "You did a {0} at {1}",
                env["owin.RequestMethod"],
                env["owin.RequestPath"]));

            await Task.Delay(250);

            return new ResultTuple(
                new Dictionary<string, object> { { "owin.ReasonPhrase", "I'm a Teapot" } },
                418,
                new Dictionary<string, string[]>
                {
                    {"Content-Type", new[] {"text/plain"}}
                },
                async output =>
                {
                    await output.WriteAsync(data, 0, data.Length);
                });
        }
    }
}