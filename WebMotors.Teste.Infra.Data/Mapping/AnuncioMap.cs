using WebMotors.Test.Domain.Entities;
using WebMotors.Test.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebMotors.Test.Infra.Data.Mapping
{
    public sealed class AnuncioMap : EntityTypeConfiguration<Anuncio>
    {
        public override void Map(EntityTypeBuilder<Anuncio> builder)
        {
            builder.ToTable("tb_AnuncioWebmotors")
                .HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("ID").HasColumnType("INTEGER").IsRequired();

            builder.Property(t => t.Marca)
                .HasColumnName("Marca").HasColumnType("VARCHAR").HasMaxLength(45).IsRequired();

            builder.Property(t => t.Modelo)
                .HasColumnName("Modelo").HasColumnType("VARCHAR").HasMaxLength(45).IsRequired();

            builder.Property(t => t.Versao)
                .HasColumnName("Versao").HasColumnType("VARCHAR").HasMaxLength(45).IsRequired();

            builder.Property(t => t.Ano)
                .HasColumnName("Ano").HasColumnType("INTEGER").IsRequired();

            builder.Property(t => t.Quilometragem)
                .HasColumnName("Quilometragem").HasColumnType("INTEGER").IsRequired();

            builder.Property(t => t.Observacao)
                .HasColumnName("Observacao").HasColumnType("TEXT").IsRequired();
        }
    }
}
