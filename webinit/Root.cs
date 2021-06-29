using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace webinit
{
    public class Handlers
    {
        public static void helloHandler( IApplicationBuilder app)
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
    }
}
