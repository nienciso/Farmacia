using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia
{
    public class Empleado
    {
        private string usuario;
        private string nombre;
        private string contrasena;

        public string Usuario
        {
            get { return usuario; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Ingrese un usuario, no puede estar vacío");

                usuario = value;
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Ingrese un nombre, no puede estar vacío");

                nombre = value;
            }
        }

        public string Contrasena
        {
            get { return contrasena; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Ingrese una contraseña, no puede estar vacía");

                contrasena = value;
            }
        }

        public Empleado(string usuario, string nombre, string contrasena)
        {
            Usuario = usuario;
            Nombre = nombre;
            Contrasena = contrasena;
        }

        public override string ToString()
        {
            return "\n - Usuario : " + Usuario + "\n - Nombre : " + Nombre + "\n - Contraseña : " + Contrasena;
        }
    }
}
