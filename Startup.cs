using Bookstore.Models;
using Bookstore.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore
{
    public class Startup
    {
        private IConfiguration configration;

        public Startup(IConfiguration configration)
        {
            this.configration = configration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();                                                        //add MVC
            //services.AddSingleton<IBookstoreRepository<Author>, AuthorRepository>();  //<main Repo<Model Type>, authrepo>() //AddSingleton Dependency injection func
            //services.AddSingleton<IBookstoreRepository<Book>, BookRepository>();
            services.AddScoped<IBookstoreRepository<Author>, AuthorDbRepository>();
            services.AddScoped<IBookstoreRepository<Book>, BookDbRepository>();
            services.AddDbContext<BookstoreDbContext>(options =>
            {
                options.UseSqlServer(configration.GetConnectionString("SqlCon"));  //name of db in appsettings.json
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();                    //to allow to use static file like link bootstrap
            app.UseMvc(route =>
            {
                route.MapRoute("default", "{controller=Book}/{action=Index}/{id?}");      //add default route to Book index.cshtml
            });
        }
    }
}
