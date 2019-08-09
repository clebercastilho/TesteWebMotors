using System;

namespace WebMotors.Test.Domain.Interfaces
{
    public interface IDomainEvent
    {
        DateTime DataOcorrencia { get; }
    }
}
