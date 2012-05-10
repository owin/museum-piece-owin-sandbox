using System;
using System.IO;
using System.Net.Http;
using System.Web.Http;
using Gate.Adapters.AspNetWebApi;
using Owin;
using Utils;

namespace Case04_JustWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "{controller}", new { controller = "Home" });
            builder
                .Use<AppTaskDelegate>(Middleware.LogRequests)
                .RunHttpServer(config);
        }
    }

    public class HomeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            Console.WriteLine("More stdout");
            return new HttpResponseMessage
            {
                Content = new StringContent("Hello World")
            };
        }
    }
}
