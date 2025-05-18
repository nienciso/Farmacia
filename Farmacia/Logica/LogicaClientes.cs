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
    public class LogicaClientes
    {

        public static Cliente Buscar(string cedula)
        {
            try
            {
                return PersistenciaCliente.BuscarCI(cedula);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el paciente : " + ex.Message);
            }
        }

        public static void Agregar(Cliente cliente)
        {
            if(cliente != null)
                PersistenciaCliente.AgregarCliente(cliente);
        }

        public static void Modificar(Cliente cliente)
        {
            if (cliente != null)
                PersistenciaCliente.ModificarCliente(cliente);
        }

        public static void Eliminar(Cliente cliente)
        {
            if (cliente != null)
                PersistenciaCliente.EliminarCliente(cliente);
        }

        public static List<Cliente> listarClientes()
        {
            try
            {
                return PersistenciaCliente.ListarClientes();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar Clientes: "+ ex.Message);
            }
        }
        
    }

}
