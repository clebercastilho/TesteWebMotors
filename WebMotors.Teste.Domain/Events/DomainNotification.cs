using System;
using WebMotors.Test.Domain.Interfaces;

namespace WebMotors.Test.Domain.Events
{
    public enum DomainNotificationType
    {
        BusinessError = 1000,
        BusinessValidation = 1001,
        TechnicalError = 1002,
        DatabaseError = 1003
    }

    public sealed class DomainNotification: IDomainEvent
    {
        public DomainNotificationType Tipo { get; private set; }
        public string PropertyName { get; private set; }
        public string Valor { get; private set; }
        public DateTime DataOcorrencia { get; private set; }

        public DomainNotification(DomainNotificationType type, string valor)
        {
            Tipo = type;
            Valor = valor;
            DataOcorrencia = DateTime.Now;
            PropertyName = string.Empty;
        }

        public DomainNotification(DomainNotificationType type, string property, string valor)
        {
            Tipo = type;
            Valor = valor;
            DataOcorrencia = DateTime.Now;
            PropertyName = property;
        }

        public override string ToString()
        {
            if (Tipo == DomainNotificationType.BusinessValidation && !string.IsNullOrWhiteSpace(PropertyName))
                return $"Campo {PropertyName}; {Valor}";

            return Valor;
        }

        public static class DomainNotificationFactory
        {
            public static DomainNotification Criar(DomainNotificationType type, string message)
            {
                return new DomainNotification(type, message);
            }

            public static DomainNotification Validation(string message)
            {
                return Criar(DomainNotificationType.BusinessValidation, message);
            }

            public static DomainNotification Erro(string message)
            {
                return Criar(DomainNotificationType.TechnicalError, message);
            }
        }
    }
}
