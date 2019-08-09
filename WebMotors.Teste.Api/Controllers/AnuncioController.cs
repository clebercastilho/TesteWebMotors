using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMotors.Test.Domain.Entities;
using WebMotors.Test.Domain.Events;
using WebMotors.Test.Domain.Interfaces;
using WebMotors.Test.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebMotors.Teste.Domain.DataObjects;
using WebMotors.Teste.Api.ViewModels;

namespace WebMotors.Test.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/anuncios")]
    public class AnuncioController : Base.BaseController
    {
        readonly IAnuncioService _anuncioService;
        readonly IMapper _mapper;

        public AnuncioController(IHandler<DomainNotification> domainNotificationHandler,
            IMapper mapper,
            IAnuncioService anuncioService) : 
            base(domainNotificationHandler)
        {
            _anuncioService = anuncioService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult ListarAnuncios([FromQuery] AnuncioFiltro filtros)
        {
            try
            {
                var anuncios = _anuncioService.ListarAnuncios(filtros);
                var lista = _mapper.Map<List<AnuncioViewModel>>(anuncios);

                return Resposta(lista);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult ObterAnuncioPorId(int id)
        {
            try
            {
                var anuncio = _anuncioService.ObterAnuncioPorId(id);
                var dados = _mapper.Map<AnuncioViewModel>(anuncio);

                return Resposta(dados);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IActionResult IncluirAnuncio([FromBody] AnuncioViewModel dados)
        {
            try
            {
                var anuncio = _mapper.Map<Anuncio>(dados);
                var sucesso = _anuncioService.IncluirAnuncio(anuncio);

                return Resposta(sucesso);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IActionResult AtualizarAnuncio([FromBody] AnuncioViewModel dados)
        {
            try
            {
                var anuncio = _mapper.Map<Anuncio>(dados);
                var sucesso = _anuncioService.AtualizarAnuncio(anuncio);

                return Resposta(sucesso);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult RemoverAnuncio(int id)
        {
            try
            {
                var sucesso = _anuncioService.RemoverAnuncio(id);

                return Resposta(sucesso);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
