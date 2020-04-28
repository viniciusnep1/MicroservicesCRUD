using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using services;
using System;

namespace MicroserviceCRUD
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        private IContainer applicationContainer;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql()
                 .AddDbContext<entities.EFApplicationContext>(
                     options => options.UseNpgsql(
                         Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();

            services.AddSingleton(_ => new JsonSerializer
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateFormatString = "dd/MM/yyyy HH:mm:ss"
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
#pragma warning disable CC0030 // Make Local Variable Constant.
                var cultureBr = "pt-BR";
#pragma warning restore CC0030 // Make Local Variable Constant.
                var suppCulture = new[]
                {
                    new CultureInfo(cultureBr)
                };

                options.DefaultRequestCulture = new RequestCulture(cultureBr);
                options.SupportedCultures = suppCulture;
                options.SupportedUICultures = suppCulture;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials().Build());
            });

            services.AddDirectoryBrowser();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<ServicesModule>();
            containerBuilder.RegisterModule<WebModule>();

            containerBuilder.Populate(services);

            applicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(applicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseHttpMethodOverride();

            app.UseMvc();
        }


    }
}
