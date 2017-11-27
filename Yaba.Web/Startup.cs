using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yaba.Common;
using Yaba.Entities;

namespace Yaba.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<YabaDBContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                if (connectionString == "inmemory")
                {
                    options.UseInMemoryDatabase("yaba-in-memory");
                }
                else
                {
                    options.UseSqlServer(connectionString);
                }
            });

            services.AddScoped<IYabaDBContext, YabaDBContext>();
            services.AddScoped<IBudgetRepository, EFBudgetRepository>();
                
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
