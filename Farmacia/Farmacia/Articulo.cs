using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia
{
    public class Articulo
    {
        private string codigo;
        private string nombre;
        private string tamaño;
        private Categoria categoria;
        private string tipoPresentacion;
        private decimal precio;

        public string Codigo
        {
            get { return codigo; }
            set
            {
                if (value.Trim().Length == 10)
                    codigo = value;
                else
                    throw new Exception("El Código debe tener exactamente 10 caracteres.");
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

        public string Tamaño
        {
            get { return tamaño; }
            set
            {
                if (value.Trim().Length > 0)
                    tamaño = value;
                else
                    throw new Exception("Ingrese un Tamaño, no puede estar vacío.");
            }
        }

        public Categoria Categoria
        {
            get { return categoria; }
            set
            {
                if (value != null)
                    categoria = value;
                else
                    throw new Exception("La categoría no puede ser nula.");
            }
        }

        public string TipoPresentacion
        {
            get { return tipoPresentacion; }
            set
            {
                string[] valoresPermitidos = { "Unidad", "Blister", "Sobre", "Frasco" };

                if (valoresPermitidos.Contains(value.Trim()))
                    tipoPresentacion = value;
                else
                    throw new Exception("Tipo de Presentación no válido. Debe ser 'Unidad', 'Blister', 'Sobre' o 'Frasco'.");
            }
        }

        public decimal Precio
        {
            get { return precio; }
            set
            {
                if (value > 0)
                    precio = value;
                else
                    throw new Exception("El precio debe ser mayor a 0.");
            }
        }

        public Articulo(string codigo, string nombre, string tamaño, Categoria categoria, string tipoPresentacion, decimal precio)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Tamaño = tamaño;
            this.Categoria = categoria;
            this.TipoPresentacion = tipoPresentacion;
            this.Precio = precio;
        }

        public override string ToString()
        {
            return "\n - Código: " + Codigo + "\n - Nombre: " + Nombre + "\n - Tamaño: " + Tamaño + "\n - Código Categoría: " + categoria + "\n - Tipo Presentación: " + TipoPresentacion + "\n - Precio: " + Precio;
        }
    }
}
