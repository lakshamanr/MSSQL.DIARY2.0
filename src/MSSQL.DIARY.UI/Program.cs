using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MSSQL.DIARY.COMN.Helper;
namespace MSSQL.DIARY.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
       
            CreateWebHostBuilder(args)
                .UseKestrel() 
                .UseWebRoot("www")
                .UseIISIntegration() 
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}