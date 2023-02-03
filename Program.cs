using Bookstore.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            //var webHost = CreateWebHostBuilder(args).Build();
            //RunMigration(webHost);
            //webHost.Run();
        }

        //private static void RunMigration(IWebHost webHost)
        //{
        //    using(var scope = webHost.Services.CreateScope())
        //    {
        //        var db = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
        //        db.Database.Migrate();
        //    }
        //}

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
