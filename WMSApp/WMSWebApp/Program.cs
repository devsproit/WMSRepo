using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sentry;
namespace WMSWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseSentry(o =>
            {
                o.Dsn = "https://c2fb5badb4ad02029b37fe00303a891b@o440458.ingest.sentry.io/4505720250302464";
                // When configuring for the first time, to see what the SDK is doing:
                o.Debug = true;
                // Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
                // We recommend adjusting this value in production.
                o.TracesSampleRate = 1.0;
            })
            .UseStartup<Startup>();



    }
}
