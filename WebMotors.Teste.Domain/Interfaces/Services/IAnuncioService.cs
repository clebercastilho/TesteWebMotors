using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebMotors.Test.Domain.Entities;
using WebMotors.Teste.Domain.DataObjects;

namespace WebMotors.Test.Domain.Interfaces.Services
{
    public interface IAnuncioService
    {
        List<Anuncio> ListarAnuncios(AnuncioFiltro filtros);
        Anuncio ObterAnuncioPorId(int anuncioId);
        bool IncluirAnuncio(Anuncio anuncio);
        bool AtualizarAnuncio(Anuncio anuncio);
        bool RemoverAnuncio(int anuncioId);
    }
}
