using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebMotors.Test.Domain.Entities.Validacoes
{
    public sealed class AnuncioValidador : Validador<Anuncio>
    {
        public override void ConfigurarValidacoes()
        {
            RuleFor(a => a.Marca)
                .NotEmpty()
                .WithMessage("A marca do veículo informada no anúncio é inválida.");

            RuleFor(a => a.Modelo)
                .NotEmpty()
                .WithMessage("O modelo do veículo informado no anúncio é inválido.");

            RuleFor(a => a.Versao)
                .NotEmpty()
                .WithMessage("A versão do veículo informada no anúncio é inválida.");

            RuleFor(a => a.Ano)
                .NotEmpty()
                .WithMessage("O ano do veículo informado no anúncio é inválido.");

            RuleFor(a => a.Quilometragem)
                .NotEmpty()
                .WithMessage("A quilometragem do veículo informada no anúncio é inválida.");
        }
    }
}
