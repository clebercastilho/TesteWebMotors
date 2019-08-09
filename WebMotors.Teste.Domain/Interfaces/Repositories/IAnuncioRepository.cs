using WebMotors.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMotors.Teste.Domain.DataObjects;

namespace WebMotors.Test.Domain.Interfaces.Repositories
{
    public interface IAnuncioRepository : Base.IRepository<Anuncio>
    {
        IQueryable<Anuncio> ListarAnuncios(AnuncioFiltro filtros);
        Task<List<Anuncio>> ListarAnunciosAsync(AnuncioFiltro filtros);
    }
}
