﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using Swashbuckle.AspNetCore.Swagger;
using Yaba.Common;
using Yaba.Common.Budget;
using Yaba.Common.Payment;
using Yaba.Entities;
using Yaba.Entities.Budget;
using Yaba.Entities.Budget.Repository;
using Yaba.Entities.Tab.Repository;
using Yaba.Entities.User.Repository;
using Yaba.Web.Options;
using Yaba.Web.Payments;
using PayPal;

namespace Yaba.Web
{
	public class Startup
	{
        public Startup(IConfiguration configuration)
		{
            /* IHostingEnvironment env
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            */

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

			StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["Priv"]);

            //services.Configure<PayPal.SDKConfigHandler>(Configuration.GetSection("PayPal"));
            //services.Configure<IConfiguration>(Configuration.GetSection("PayPal"));

			services.AddScoped<IYabaDBContext, YabaDBContext>();

			//services.AddScoped<IPaymentRepository, StripePay>();
			services.AddScoped<IBudgetRepository, EFBudgetRepository>();
			services.AddScoped<ITabRepository, EFTabRepository>();
			services.AddScoped<IItemRepository, EFItemRepository>();
			services.AddScoped<IEntryRepository, EFEntryRepository>();
			services.AddScoped<ICategoryRepository, EFCategoryRepository>();
			services.AddScoped<IGoalRepository, EFGoalRepository>();
			services.AddScoped<IRecurringRepository, EFRecurringRepository>();
			services.AddScoped<IUserRepository, EFUserRepository>();

			var auth0options = new Auth0Options();
			Configuration.GetSection("Auth0").Bind(auth0options);
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.Authority = $"https://{auth0options.Domain}/";
				options.Audience = auth0options.ApiIdentifier;
			});

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
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, YabaDBContext context)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			if (env.IsProduction())
			{
				// force https in production
				app.UseRewriter(new RewriteOptions().AddRedirectToHttps());
			}

			app.UseAuthentication();

			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yaba REST API");
			});

			context.Database.EnsureCreated();
		}
	}
}
