using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intento1
{
    internal class Usuario
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }

        public Usuario(string NombreUsuario, string Contrasena)
        {
            this.NombreUsuario = NombreUsuario;
            this.Contrasena = Contrasena;
        }
    }
}
