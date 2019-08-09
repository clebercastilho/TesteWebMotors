using WebMotors.Test.Domain.Entities;
using WebMotors.Test.Infra.Data.Extensions;
using WebMotors.Test.Infra.Data.Mapping;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace WebMotors.Test.Infra.Data.Context
{
    public class WebMotorsContext : DbContext
    {
        private readonly IHostingEnvironment _env;

        public WebMotorsContext(IHostingEnvironment env)
        {
            _env = env;
        }

        public DbSet<Anuncio> Anuncios { get; set; }

        private void MappingRepositoryModels(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new AnuncioMap());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MappingRepositoryModels(modelBuilder);

            //removendo comportamento cascade delete
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                                .SelectMany(t => t.GetForeignKeys())
                                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true)
                  .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("SqlServerConnectionString"));
        }
    }
}
