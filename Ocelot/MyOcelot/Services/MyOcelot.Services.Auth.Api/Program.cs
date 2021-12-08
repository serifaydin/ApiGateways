using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MyOcelot.Services.Auth.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:5002");
                    webBuilder.UseStartup<Startup>();
                });
    }
}