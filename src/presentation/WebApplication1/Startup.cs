using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RusMProject.Persistance.DbContextManager;
using RusMProject.WebAPI.Filters;
using RusMProject.WebAPI.Others;
using RusMProjectApplication;
using RusMProjectApplication.Statics;
using RusMProjectApplication.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RusMProject.WebAPI.Middlewares;
using Microsoft.AspNetCore.Http;

namespace WebApplication1
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
            services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ModelStateFilterAttribute), 2);
                opt.Filters.Add(typeof(UnhandledExceptionFilter), 1);
            })
            .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve)
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GeneralValidator>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });
            });
            services = DbContextRegistration.GenerateFromPersistance(services, Configuration);
            services = ServiceRegistrationFromApplication.GenerateStartupFromApplication(services);
            services.AddHealthChecks();
            services.AddCors(policy=>policy.AddDefaultPolicy(p=>p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }
            app.HealthCheck();
            app.UseHttpsRedirection();
            //app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseRouting();

            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
