using WebMotors.Test.Domain.Entities.Base;
using WebMotors.Test.Domain.Events;
using WebMotors.Test.Domain.Handlers;
using WebMotors.Test.Domain.Interfaces;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebMotors.Test.Domain.Interfaces.UoW;

namespace WebMotors.Test.Domain.Services.Base
{
    public abstract class Service<T>
    {
        protected DomainNotificationHandler _domainNotification;
        protected IUnitOfWork _unitOfWork;

        protected Service(
            IHandler<DomainNotification> domainNotification, 
            IUnitOfWork unitOfWork)
        {
            _domainNotification = (DomainNotificationHandler)domainNotification;
            _unitOfWork = unitOfWork;
        }

        protected bool VerificarEntidade(Entidade<T> entidade)
        {
            if (entidade.Validar())
                return true;

            var notificacoes = entidade.ObterNotificacoes();

            if (!notificacoes.Any())
                return true;

            notificacoes.ToList().ForEach(DomainEvent.Raise);
            return false;
        }

        public void Notificar(DomainNotification notification)
        {
            DomainEvent.Raise(notification);
        }

        public void NotificarException(DomainNotificationType notificationType, Exception exception)
        {
            DomainEvent.Raise(new DomainNotification(notificationType, exception.Message));

            if (exception.InnerException != null)
                DomainEvent.Raise(new DomainNotification(notificationType, exception.InnerException.Message));
        }

        public void NotificarDbException(DbException exception)
        {
            DomainEvent.Raise(new DomainNotification(DomainNotificationType.DatabaseError, exception.Message));

            if (exception.InnerException != null)
                DomainEvent.Raise(new DomainNotification(DomainNotificationType.DatabaseError, exception.InnerException.Message));
        }

        protected bool Commit(string mensagemErroCommit)
        {
            if (_domainNotification.ExistemNotificacoes())
                return false;

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                Notificar(DomainNotification.DomainNotificationFactory.Erro(mensagemErroCommit));
                Notificar(DomainNotification.DomainNotificationFactory.Erro(ex.Message));
                
                return false;
            }

            return true;
        }
    }
}
