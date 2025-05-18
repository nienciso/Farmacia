using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia
{
    public class Venta
    {
        private int numeroVenta;
        private DateTime fecha;
        private string estado;
        private string direccion;
        private int cantidadNumero;
        private Empleado empleado;
        private Articulo articulo;
        private Cliente cliente;

        public int NumeroVenta
        {
            get { return numeroVenta; }
            private set { numeroVenta = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set
            {
                if (value <= DateTime.Now)
                    fecha = value;
                else
                    throw new Exception("Debe ingresar una fecha válida.");
            }
        }

        public string Estado
        {
            get { return estado; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    estado = value;
                else
                    throw new Exception("El estado no puede estar vacío.");
            }
        }

        public string Direccion
        {
            get { return direccion; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    direccion = value;
                else
                    throw new Exception("La dirección no puede estar vacía.");
            }
        }

        public int CantidadNumero
        {
            get { return cantidadNumero; }
            set
            {
                if (value > 0)
                    cantidadNumero = value;
                else
                    throw new Exception("La cantidad debe ser mayor a 0.");
            }
        }

        public Empleado Empleado
        {
            get { return empleado; }
            set
            {
                if (value != null)
                    empleado = value;
                else
                    throw new Exception("El empleado no puede ser nulo.");
            }
        }

        public Articulo Articulo
        {
            get { return articulo; }
            set
            {
                if (value != null)
                    articulo = value;
                else
                    throw new Exception("El artículo no puede ser nulo.");
            }
        }

        public Cliente Cliente
        {
            get { return cliente; }
            set
            {
                if (value != null)
                    cliente = value;
                else
                    throw new Exception("El cliente no puede ser nulo.");
            }
        }

        public Venta(int numeroVenta, DateTime fecha, string estado, string direccion, int cantidadNumero, Empleado empleado, Articulo articulo, Cliente cliente)
        {
            NumeroVenta = numeroVenta;
            Fecha = fecha;
            Estado = estado;
            Direccion = direccion;
            CantidadNumero = cantidadNumero;
            Empleado = empleado;
            Articulo = articulo;
            Cliente = cliente;
        }

        public override string ToString()
        {
            return $"\n - Numero Venta : {numeroVenta}" +
                   $"\n - Fecha : {Fecha}" +
                   $"\n - Estado : {Estado}" +
                   $"\n - Direccion : {Direccion}" +
                   $"\n - Cantidad Numero : {CantidadNumero}" +
                   $"\n - Empleado Responsable : {Empleado.Nombre}" +
                   $"\n - Codigo Articulo : {Articulo.Codigo}" +
                   $"\n - Cedula Cliente : {Cliente.Cedula}";
        }
    }
}
