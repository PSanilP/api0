using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace api0
{
    public class Handlers
    {
        public static void helloHandler(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapGet("/{name:alpha}", async context =>
                {
                    string w = (context.Request.RouteValues["name"] != null ? context.Request.RouteValues["name"].ToString() : "");
                    await context.Response.WriteAsync("Hello " + w + "!");
                });
            });
        }

        public static void fooHandler(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/{bar}/{id:int?}", async context =>
                {

                    int id = (context.Request.RouteValues["id"] != null ? int.Parse(context.Request.RouteValues["id"].ToString()) : 0);
                    string bar = (context.Request.RouteValues["bar"] != null ? context.Request.RouteValues["bar"].ToString() : "");
                    context.Response.ContentType = context.Request.ContentType;
                    await context.Response.WriteAsync("foo " + bar + ":" + id);

                });

            });
        }

        public static void EchoHandler(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.ContentType = context.Request.ContentType;
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new
                    {
                        StatusCode = context.Response.StatusCode.ToString(),
                        PathBase = context.Request.PathBase.Value.Trim('/'),
                        Path = context.Request.Path.Value.Trim('/'),
                        Method = context.Request.Method,
                        Scheme = context.Request.Scheme,
                        ContentType = context.Request.ContentType,
                        ContentLength = (long?)context.Request.ContentLength,
                        Content = new StreamReader(context.Request.Body).ReadToEnd(),
                        QueryString = context.Request.QueryString.ToString(),
                        Query = context.Request.Query.ToDictionary(item => item.Key, item => item.Value, StringComparer.OrdinalIgnoreCase)
                    })
                );
            });
        }

        public static void TestHandler(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.ContentType = context.Request.ContentType;

                var content = new StreamReader(context.Request.Body).ReadToEnd();
                var action_ = context.Request.Method;
                if (action_ == "POST")
                {
                    var myJObject = Newtonsoft.Json.Linq.JObject.Parse(content);
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(myJObject));
                }

                if (action_ == "GET")
                {
                    //C:\Users\nanda\source\repos\api0\src\api0\bin\Debug\net5.0\           AppDomain.CurrentDomain.BaseDirectory;
                    //    await context.Response.WriteAsync(_config["MyCustomKey"]);

                    var s1 = "arvind " + api0.Program.cnstr;
                    //  var s1 = AppDomain.CurrentDomain.BaseDirectory;
                    // var s1 = Environment.GetEnvironmentVariable("launchUrl");
                  //   s1 = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    await context.Response.WriteAsync(s1);

                    // string[] Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
                    // await context.Response.WriteAsync(JsonConvert.SerializeObject(Summaries));
                }

            });
        }

        // endclass
    }
    //end namespace
}
