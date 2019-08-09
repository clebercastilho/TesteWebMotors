using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace WebMotors.Test.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                var info = new Info();
                info.Version = "v1";
                info.Title = "Web Motors Teste API";
                info.Description = "API de gerenciamento de Anuncios da Web Motors.";
                info.TermsOfService = "Nenhum";
                info.Contact = new Contact { Name = "", Email = "", Url = "http://localhost:" };
                info.License = new License { Name = "MIT", Url = "http://www.google.com.br" };

                s.SwaggerDoc("v1", info);
            });
        }
    }
}
