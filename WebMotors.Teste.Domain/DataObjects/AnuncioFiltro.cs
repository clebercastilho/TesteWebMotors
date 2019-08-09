using System;
using System.Collections.Generic;
using System.Text;

namespace WebMotors.Teste.Domain.DataObjects
{
    public class AnuncioFiltro
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int AnoDesde { get; set; }
        public int AnoAte { get; set; }
    }
}
