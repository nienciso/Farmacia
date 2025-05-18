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
    public class LogicaEmpleado
    {
        public static Empleado BuscarEmpleado(string usuario)
        {
            try
            {
                return PersistenciaEmpleado.BuscarEmpleado(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error el Empleado : " + ex.Message);
            }
        }

        public static Empleado Logueo(string Usuario, string Contrasena)
        {
            Empleado empleado = null;

            empleado = PersistenciaEmpleado.Logueo(Usuario,Contrasena);

            return empleado;

        }

    }
}
