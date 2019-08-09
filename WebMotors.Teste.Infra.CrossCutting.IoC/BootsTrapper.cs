using WebMotors.Test.Domain.Events;
using WebMotors.Test.Domain.Handlers;
using WebMotors.Test.Domain.Interfaces;
using WebMotors.Test.Domain.Interfaces.Repositories;
using WebMotors.Test.Domain.Interfaces.Services;
using WebMotors.Test.Domain.Services;
using WebMotors.Test.Infra.Data.Context;
using WebMotors.Test.Infra.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebMotors.Test.Domain.Interfaces.UoW;
using Amil.Atendimentos.Infra.Data.UoW;

namespace WebMotors.Test.Infra.CrossCutting
{
    public static class BootsTrapper
    {
        public static void AddDIConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<WebMotorsContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAnuncioService, AnuncioService>();
            services.AddScoped<IAnuncioRepository, AnuncioRepository>();
        }
    }
}
