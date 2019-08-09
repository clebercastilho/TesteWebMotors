using WebMotors.Test.Api.Configuration;
using WebMotors.Test.Domain.Events;
using WebMotors.Test.Infra.CrossCutting;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebMotors.Test.Api
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
            services.AddCors();
            services.AddMvc();
            
            services.AddDIConfiguration(Configuration);
            services.AddSwaggerConfig();
            services.AddAutoMapper();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHttpContextAccessor accessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });
            
            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(s =>
            {
                s.RoutePrefix = "api-docs";
                s.SwaggerEndpoint("v1/swagger.json", "Api v1");
            });
             
            DomainEvent.ContainerAccessor = () => accessor.HttpContext.RequestServices;
        }
    }
}
