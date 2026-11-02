using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intento1
{
    internal class Operacion
    {
        public string CodigoOperacion { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public string Agencia { get; set; }

        public Operacion(string codigoOperacion, string tipo,
                         decimal monto, string agencia)
        {
            CodigoOperacion = codigoOperacion;
            Tipo = tipo;
            Monto = monto;
            Agencia = agencia;
        }
    }
}
