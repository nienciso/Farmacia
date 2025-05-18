using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Farmacia;

namespace Persistencia
{
    public class PersistenciaEmpleado
    {
        public static Empleado BuscarEmpleado(string usuario)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("BuscarEmpleados", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@Usuario", usuario);

                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            Empleado empleado = new Empleado(
                                lector["Usuario"].ToString(),
                                lector["Nombre"].ToString(),
                                lector["Contrasena"].ToString()
                            );
                            return empleado;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar el empleado: " + ex.Message);
                }
            }
        }

        public static Empleado Logueo(string Usuario, string Contrasena)
        { 
        Empleado empleado = null;

        SqlConnection conexion = new SqlConnection(Conexion.Cnn);
        SqlCommand comando = new SqlCommand("LogueoEmpleado", conexion);
        comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Usuario", Usuario);
            comando.Parameters.AddWithValue("@Contrasena", Contrasena);

            try
            {
                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    if (lector.Read())
                    {
                        empleado = new Empleado(
                                Convert.ToString(lector["Usuario"]), Convert.ToString(lector["Nombre"]),
                                 Convert.ToString(lector["Contrasena"]));
                    }
                }
                lector.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
            return empleado;
        }

    }


}
