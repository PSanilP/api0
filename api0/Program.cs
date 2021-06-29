using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.IO;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.Extensions.Configuration;


namespace api0
{
    public class Program
    {
        public static string cnstr;
        public static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            cnstr = configuration["MyCustomKey"];
            Console.WriteLine(cnstr);
            //string foo = configuration.GetSection("Logging").Value;


            var host = new WebHostBuilder()
                .UseKestrel(option => option.AllowSynchronousIO = true)
                //   .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://localhost:5678", "http://localhost:5679", "http://*:5678")
                .UseIISIntegration()
                .ConfigureServices(services => { services.AddHealthChecks(); services.AddRouting(); })

                .Configure(app =>
                    {
                        app.UseRouting();
                        app.Map("/test", Handlers.TestHandler);
                        app.Map("/echo", Handlers.EchoHandler);
                        app.Map("/foo", Handlers.fooHandler);
                        app.Map("/hello", Handlers.helloHandler);
                    })
                .Build();
            host.Run();
        }

    }

}


