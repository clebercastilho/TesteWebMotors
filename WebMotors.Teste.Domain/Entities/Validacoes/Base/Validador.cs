using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebMotors.Test.Domain.Entities.Validacoes
{
    public abstract class Validador<T> : AbstractValidator<T>
    {
        public abstract void ConfigurarValidacoes();
    }
}
