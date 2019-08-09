using WebMotors.Test.Domain.Entities;
using WebMotors.Test.Domain.Interfaces.Repositories;
using WebMotors.Test.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMotors.Teste.Domain.DataObjects;

namespace WebMotors.Test.Infra.Data.Repositories
{
    public sealed class AnuncioRepository : Base.RepositoryBase<Anuncio>, IAnuncioRepository
    {
        public AnuncioRepository(WebMotorsContext ctx) : base(ctx)
        {

        }

        public IQueryable<Anuncio> ListarAnuncios(AnuncioFiltro filtros)
        {
            var query = from a in _ctx.Anuncios
                        where
                            (string.IsNullOrEmpty(filtros.Marca) || a.Marca == filtros.Marca) &&
                            (string.IsNullOrEmpty(filtros.Modelo) || a.Modelo == filtros.Modelo) &&
                            (filtros.AnoDesde <= 0 || a.Ano >= filtros.AnoDesde) &&
                            (filtros.AnoAte <= 0 || a.Ano <= filtros.AnoAte)
                        select a;

            return query;
        }

        public async Task<List<Anuncio>> ListarAnunciosAsync(AnuncioFiltro filtros)
        {
            return await ListarAnuncios(filtros).ToListAsync();
        }
    }
}
