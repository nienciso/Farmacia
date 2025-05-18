using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia
{
    public class Categoria
    {
        private string codigo;
        private string nombre;

        public string Codigo
        {
            get { return codigo; }
            set
            {
                if (value.Trim().Length == 6)
                    codigo = value;
                else
                    throw new Exception("El Código debe tener exactamente 6 caracteres. 'MED001'.");
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (value.Trim().Length > 0)
                    nombre = value;
                else
                    throw new Exception("Ingrese un Nombre, no puede estar vacío");
            }
        }

        public Categoria(string codigo, string nombre)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
        }

        public override string ToString()
        {
            return "\n - Codigo : " + Codigo + "\n - nombre : " + nombre;
        }
    }
}
