using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Reservation.API
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
                    webBuilder.UseUrls("http://*:9001");
                    webBuilder.UseStartup<Startup>();
                });
    }
}