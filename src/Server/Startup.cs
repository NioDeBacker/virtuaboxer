﻿using Persistence.Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Client.Infrastructure;
using VirtuaBoxer.Shared.Boxers;
using Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("VirtuaBoxer"));
            services.AddDbContext<VirtuaBoxerDbContext>(options =>
                options.UseSqlServer(builder.ConnectionString)
                    .EnableSensitiveDataLogging(Configuration.GetValue<bool>("Logging:EnableSqlParameterLogging")));

            services.AddControllersWithViews().AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<BoxerDto.Mutate.Validator>();
                config.ImplicitlyValidateChildProperties = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => $"{x.DeclaringType.Name}.{x.Name}");
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VirtuaBoxer API", Version = "v1" });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration["Auth0:Authority"];
                options.Audience = Configuration["Auth0:ApiIdentifier"];
            });

            /*services.AddAuth0AuthenticationClient(config =>
            {
                config.Domain = Configuration["Auth0:Authority"];
                config.ClientId = Configuration["Auth0:ClientId"];
                config.ClientSecret = Configuration["Auth0:ClientSecret"];
            });

            services.AddAuth0ManagementClient().AddManagementAccessToken();*/

            services.AddRazorPages();
            services.AddScoped<IBoxerService, BoxerService>();
            services.AddScoped<VirtuaBoxerDataInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VirtuaBoxerDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VirtuaBoxer API"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

            VirtuaBoxerDataInitializer.Seed(dbContext);
        }
    }
}