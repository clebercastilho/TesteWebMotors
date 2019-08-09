using WebMotors.Test.Domain.Entities.Validacoes;
using WebMotors.Test.Domain.Events;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMotors.Test.Domain.Entities.Base
{
    public abstract class Entidade<T>
    {
        readonly Validador<T> _validador;

        ValidationResult resultadoValidacao;

        protected Entidade(Validador<T> validador)
        {
            _validador = validador;
        }

        public abstract bool Validar();

        public bool Validar(T entidade)
        {
            _validador.ConfigurarValidacoes();
            resultadoValidacao = _validador.Validate(entidade);

            return resultadoValidacao.IsValid;
        }

        public List<DomainNotification> ObterNotificacoes()
        {
            return resultadoValidacao.Errors
                .Select(e => new DomainNotification(DomainNotificationType.BusinessValidation, e.PropertyName, e.ErrorMessage))
                .ToList();
        }

    }
}
