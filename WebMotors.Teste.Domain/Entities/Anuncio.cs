using System;
using System.Collections.Generic;
using System.Text;
using WebMotors.Test.Domain.Entities.Validacoes;

namespace WebMotors.Test.Domain.Entities
{
    public sealed class Anuncio : Base.Entidade<Anuncio>
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Versao { get; set; }
        public int Ano { get; set; }
        public int Quilometragem { get; set; }
        public string Observacao { get; set; }

        public Anuncio() : base(new AnuncioValidador())
        {
        }

        public override bool Validar()
        {
            return Validar(this);
        }
    }
}
