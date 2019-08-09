using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMotors.Test.Domain.Events;
using WebMotors.Test.Domain.Handlers;
using WebMotors.Test.Domain.Interfaces;

namespace WebMotors.Test.Api.Controllers.Base
{
    [Produces("application/json")]
    public class BaseController : Controller
    {
        readonly IHandler<DomainNotification> _domainNotificationHandler;

        public BaseController(IHandler<DomainNotification> domainNotificationHandler)
        {
            _domainNotificationHandler = domainNotificationHandler;
        }

        protected bool ValidarOperacao()
        {
            return !_domainNotificationHandler.ExistemNotificacoes();
        }


        protected IActionResult Resposta(object resultado = null)
        {
            if (ValidarOperacao())
            {
                return Ok(new
                {
                    sucesso = true,
                    dados = resultado
                });
            }

            var notificacoes = _domainNotificationHandler.ObterValores();
            var lastNotification = notificacoes.Last();

            var responseStatusCode = (lastNotification.Tipo == DomainNotificationType.TechnicalError ||
                lastNotification.Tipo == DomainNotificationType.DatabaseError) ?
                StatusCodes.Status500InternalServerError :
                StatusCodes.Status400BadRequest;

            return StatusCode(responseStatusCode, new
            {
                sucesso = false,
                erros = notificacoes.Select(n => new
                {
                    Code = (int)n.Tipo,
                    NotificationType = n.Tipo.ToString(),
                    Message = n.ToString()
                }).ToArray()
            });
        }

        protected IActionResult InternalServerError(Exception ex)
        {
            var errorList = new List<string> { ex.Message };
            if (ex.InnerException != null)
                errorList.Add(ex.InnerException.Message);

            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                sucesso = false,
                erros = errorList.Select(n => new
                {
                    Code = (int)DomainNotificationType.TechnicalError,
                    NotificationType = DomainNotificationType.TechnicalError.ToString(),
                    Message = n
                }).ToArray()
            });
        }
    }
}