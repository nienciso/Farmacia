using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia
{
    public class Cliente
    {
        private string cedula;
        private string nombre;
        private string numeroTarjeta;
        private string telefono;

        public string Cedula
        {
            get { return cedula; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Ingrese una cédula, no puede estar vacía.");

                cedula = value.Trim();
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Ingrese un nombre, no puede estar vacío.");

                nombre = value.Trim();
            }
        }


        public string NumeroTarjeta
        {
            get { return numeroTarjeta; }
            set
            {
                if (value.Length == 16 && value.All(char.IsDigit))
                    numeroTarjeta = value;
                else
                    throw new Exception("El numero de tarjeta debe tener exactamente 16 dígitos.");
            }
        }

        public string Telefono
        {
            get { return telefono; }
            set
            {
                if (value.Length == 9 && value.All(char.IsDigit))
                    telefono = value;
                else
                    throw new Exception("El Teléfono debe tener exactamente 9 dígitos.");
            }
        }

        public Cliente(string cedula, string nombre, string numeroTarjeta, string Telefono)
        {
            Cedula = cedula;
            Nombre = nombre;
            NumeroTarjeta = numeroTarjeta;
            telefono = Telefono;
        }

        public override string ToString()
        {
            return "\n - Cedula : " + Cedula + "\n - Nombre : " + Nombre + "\n - NumeroTarjeta : " + NumeroTarjeta + "\n - Telefono : " + Telefono;
        }
    }
}
