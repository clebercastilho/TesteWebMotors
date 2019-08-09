using WebMotors.Test.Domain.Entities;
using WebMotors.Test.Domain.Events;
using WebMotors.Test.Domain.Interfaces;
using WebMotors.Test.Domain.Interfaces.Repositories;
using WebMotors.Test.Domain.Interfaces.Services;
using WebMotors.Test.Domain.Interfaces.UoW;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMotors.Teste.Domain.DataObjects;

namespace WebMotors.Test.Domain.Services
{
    public class AnuncioService : Base.Service<Anuncio>, IAnuncioService
    {
        readonly IAnuncioRepository _anuncioRepository;

        public AnuncioService(IHandler<DomainNotification> domainNotification,
            IUnitOfWork unitOfWorkAtendimentos,
            IAnuncioRepository anuncioRepository) :
            base(domainNotification, unitOfWorkAtendimentos)
        {
            _anuncioRepository = anuncioRepository;
        }

        public List<Anuncio> ListarAnuncios(AnuncioFiltro filtros)
        {
            try
            {
                var anuncios = _anuncioRepository.ListarAnuncios(filtros).ToList();
                return anuncios;
            }
            catch (Exception ex)
            {
                NotificarException(DomainNotificationType.TechnicalError, ex);
                return new List<Anuncio>();
            }
        }

        public Anuncio ObterAnuncioPorId(int anuncioId)
        {
            try
            {
                var anuncio = _anuncioRepository.Listar().FirstOrDefault(a => a.Id == anuncioId);
                return anuncio;
            }
            catch (Exception ex)
            {
                NotificarException(DomainNotificationType.TechnicalError, ex);
                return new Anuncio();
            }
        }

        public bool IncluirAnuncio(Anuncio anuncio)
        {
            try
            {
                if (!VerificarEntidade(anuncio))
                    return false;

                _anuncioRepository.Incluir(anuncio);
                return Commit("Ocorreu um erro ao tentar incluir o novo anúncio.");
            }
            catch (Exception ex)
            {
                NotificarException(DomainNotificationType.TechnicalError, ex);
                return false;
            }
        }

        public bool AtualizarAnuncio(Anuncio anuncio)
        {
            try
            {
                if (!VerificarEntidade(anuncio))
                    return false;

                var anuncioAtual = _anuncioRepository.Listar().FirstOrDefault(a => a.Id == anuncio.Id);

                if(anuncioAtual == null)
                {
                    Notificar(DomainNotification.DomainNotificationFactory.Criar(DomainNotificationType.BusinessError,
                        "Nenhum anuncio encontrado para atualização."));
                    return false;
                }

                anuncioAtual.Marca = anuncio.Marca;
                anuncioAtual.Modelo = anuncio.Modelo;
                anuncioAtual.Versao = anuncio.Versao;
                anuncioAtual.Ano = anuncio.Ano;
                anuncioAtual.Quilometragem = anuncio.Quilometragem;
                anuncioAtual.Observacao = anuncio.Observacao;

                _anuncioRepository.Alterar(anuncioAtual);
                return Commit("Ocorreu um erro ao tentar atualizar o anúncio.");
            }
            catch (Exception ex)
            {
                NotificarException(DomainNotificationType.TechnicalError, ex);
                return false;
            }
        }

        public bool RemoverAnuncio(int anuncioId)
        {
            try
            {
                var anuncioAtual = _anuncioRepository.Listar().FirstOrDefault(a => a.Id == anuncioId);

                if(anuncioAtual == null)
                {
                    Notificar(DomainNotification.DomainNotificationFactory.Criar(DomainNotificationType.BusinessError,
                        "Nenhum anúncio encontrado para exclusão."));
                    return false;
                }

                _anuncioRepository.Excluir(anuncioAtual);
                return Commit("Ocorreu um erro ao tentar remover o anúncio");
            }
            catch (Exception ex)
            {
                NotificarException(DomainNotificationType.TechnicalError, ex);
                return false;
            }
        }
    }
}
