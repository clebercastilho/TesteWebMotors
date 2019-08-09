using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebMotors.Test.Infra.Data.Extensions
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }

    public abstract class QueryTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(QueryTypeBuilder<TEntity> builder);
    }
}
