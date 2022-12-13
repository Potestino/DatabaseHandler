using DatabaseHandler;
using DatabaseHandler.Extensions;
using DatabaseHandler.Sample.Domain.Interfaces.Repositories;
using DatabaseHandler.Sample.Domain.Interfaces.Services;
using DatabaseHandler.Sample.Infra.Repositories;
using DatabaseHandler.Sample.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace DatabaseHandler.Sample.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DatabaseHandler Sample API",
                    Version = "v1.0",
                    Description = "API .NETCore 3.1 DatabaseHandler",
                    Contact = new OpenApiContact
                    {
                        Name = "DatabaseHandler Sample API",
                        Email = "",
                    }
                });
            });

            services.AddDatabaseHandler(Configuration);

            services.AddScoped<IFooService1, FooService1>();
            services.AddScoped<IFooService2, FooService2>();
            services.AddScoped<IFooService3, FooService3>();
            services.AddScoped<IMassiveDataService, MassiveDataService>();

            services.AddScoped<IFooRepository1, FooRepository1>();
            services.AddScoped<IFooRepository2, FooRepository2>();
            services.AddScoped<IFooRepository3, FooRepository3>();
            services.AddScoped<IMassiveDataRepository, MassiveDataRepository>();
        }

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
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/v1/swagger.json", "Values API-CORE");
            });
        }
    }
}
