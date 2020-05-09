using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace web
{
    public class Startup
    {
        private readonly ILogger _logger;
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _logger = logger;
            Configuration = configuration;
            InitializeAndConfigureEvolveMigrations();
        }

        public IConfiguration Configuration { get; }

        private void InitializeAndConfigureEvolveMigrations()
        {
            try
            {
                var cnx = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));

                var evolve = new Evolve.Evolve(cnx, msg => _logger.LogInformation(msg))
                {
                    Locations = new[] { "Resources/db/migration/postgres" },
                    IsEraseDisabled = true,
                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Database migration failed.", ex);
                throw;
            }
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc();
            services.AddEntityFrameworkNpgsql()
             .AddDbContext<EFApplicationContext>(options => options.UseNpgsql(Configuration.GetConnectionString("FuncionariosDB")));

            IServiceCollection serviceCollection = services.AddDbContextPool<EFApplicationContext>(optionsAction =>
            {
                optionsAction.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                optionsAction.UseLazyLoadingProxies(true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
