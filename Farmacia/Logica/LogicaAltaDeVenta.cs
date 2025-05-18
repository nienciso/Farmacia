using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using Farmacia;
using Persistencia;

namespace Logica
{
    public class LogicaAltaDeVenta
    {

        public static List<Venta> ObtenerVentasPorCliente(Cliente cliente)
        {
            try
            {
                if (cliente == null || string.IsNullOrWhiteSpace(cliente.Cedula))
                    throw new Exception("Debe proporcionar un cliente válido.");

                return PersistenciaAltaDeVenta.ObtenerVentasPorCliente(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ventas por cliente: " + ex.Message);
            }
        }

        public static List<Venta> ObtenerVentasPendientes()
        {
            try
            {
                List<Venta> ventasPendientes = PersistenciaAltaDeVenta.ObtenerVentasPendientes();

                return ventasPendientes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ventas pendientes: " + ex.Message);
            }
        }

        public static Venta BuscarVenta(int numeroVenta)
        {
            try
            {
                return PersistenciaAltaDeVenta.BuscarVenta(numeroVenta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el Número de venta: " + ex.Message);
            }
        }

        public static void Agregar(Venta venta)
        {
            if (venta != null)
            {              
                string usuarioEmpleado = venta.Empleado.Usuario;
                PersistenciaAltaDeVenta.RegistrarVenta(venta, usuarioEmpleado);
            }
        }

        public static bool ActualizarEstado(int numeroVenta, string nuevoEstado)
        {
            try
            {
                return PersistenciaAltaDeVenta.ActualizarEstado(numeroVenta, nuevoEstado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado de la venta: " + ex.Message);
            }
        }

    }
}
