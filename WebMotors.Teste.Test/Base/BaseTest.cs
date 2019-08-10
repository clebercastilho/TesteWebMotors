using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebMotors.Test.Domain.Events;
using WebMotors.Test.Domain.Handlers;
using WebMotors.Test.Domain.Interfaces;
using WebMotors.Test.Domain.Interfaces.UoW;

namespace WebMotors.Teste.Test.Base
{
    public class BaseTest
    {
        protected Mock<IHttpContextAccessor> accessor;
        protected Mock<IServiceProvider> provider;
        protected IHandler<DomainNotification> handler;
        protected DefaultHttpContext context;
        protected Mock<IUnitOfWork> _uow;

        public BaseTest()
        {
            context = new DefaultHttpContext();
            provider = new Mock<IServiceProvider>();
            accessor = new Mock<IHttpContextAccessor>();
            handler = new DomainNotificationHandler();
            _uow = new Mock<IUnitOfWork>();

            SetupMockingProvider();
            SetupMockingContextAccessor();

            DomainEvent.ContainerAccessor = () => context.RequestServices;
        }

        private void SetupMockingContextAccessor()
        {
            accessor.Setup(_ => _.HttpContext).Returns(context);
        }

        private void SetupMockingProvider()
        {
            provider.Setup(r => r.GetService(typeof(IHandler<DomainNotification>))).Returns(handler);
            context.RequestServices = provider.Object;
        }
    }
}
