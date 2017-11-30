using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Yaba.Common;
using Yaba.Common.Budget;
using Yaba.Entities;
using Yaba.Entities.Budget;

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
            services.AddScoped<ICategoryRepository, EFCategoryRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Yaba REST API",
                    Version = "v1",
                });
            });
                
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yaba REST API");
            });
        }
    }
}
