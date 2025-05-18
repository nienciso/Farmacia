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
    public class PersistenciaCliente
    {

        public static Cliente BuscarCI(string Cedula)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("BuscarCI", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@cedula", Cedula);

                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            Cliente cliente = new Cliente(
                                 lector["Cedula"].ToString(),
                                lector["Nombre"].ToString(),
                                 lector["NumeroTarjeta"].ToString(),
                                 lector["Telefono"].ToString()
                                );
                            return cliente;
                        }
                        else
                        {
                            return null;
                        }
                    }


                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar el paciente: " + ex.Message);
                }
            }

        }

        public static void AgregarCliente(Cliente cliente)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                using (SqlCommand comando = new SqlCommand("AgregarCliente", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@cedula", cliente.Cedula);
                    comando.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    comando.Parameters.AddWithValue("@NumeroTarjeta", cliente.NumeroTarjeta);

                    if (cliente.Telefono == null || cliente.Telefono.Trim() == "")
                        comando.Parameters.AddWithValue("@Telefono", DBNull.Value);
                    else
                        comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                    SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    comando.Parameters.Add(retorno);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();

                        int resultado = Convert.ToInt32(retorno.Value);

                        if (resultado == -1)
                            throw new Exception("El cliente ya existe. No se puede agregar.");
                        else if (resultado == -2)
                            throw new Exception("Error inesperado al agregar el cliente.");
                        else if (resultado == 1)
                            Console.WriteLine("Cliente agregado correctamente.");
                        else
                            throw new Exception("Error desconocido.");
                    }
                    catch (SqlException sqlEx)
                    {
                        throw new Exception("Error al conectar con la base de datos: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {         
                        throw new Exception("Ocurrió un error al agregar el cliente: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
            }
        }

        public static void EliminarCliente(Cliente Ccliente)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                using (SqlCommand comando = new SqlCommand("EliminarCliente", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@cedula", Ccliente.Cedula);

                    SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    comando.Parameters.Add(retorno);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();

                        int resultado = Convert.ToInt32(retorno.Value);

                        if (resultado == -1)
                            throw new Exception("Cédula del cliente no existe - No se puede Eliminar.");
                        else if (resultado == -2)
                            throw new Exception("El cliente tiene ventas asociadas - No se puede Eliminar.");
                        else if (resultado == -3)
                            throw new Exception("Error inesperado al eliminar el cliente.");
                        else if (resultado != 1)
                            throw new Exception("Error desconocido al intentar eliminar el cliente.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Ocurrió un error: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
            }
        }

        public static void ModificarCliente(Cliente Ccliente)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                using (SqlCommand comando = new SqlCommand("ModificarCliente", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@cedula", Ccliente.Cedula);
                    comando.Parameters.AddWithValue("@nombre", Ccliente.Nombre);
                    comando.Parameters.AddWithValue("@NumeroTarjeta", Ccliente.NumeroTarjeta);
                    comando.Parameters.AddWithValue("@Telefono", Ccliente.Telefono);

                    SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    comando.Parameters.Add(retorno);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();

                        int resultado = Convert.ToInt32(retorno.Value);

                        if (resultado == -1)
                            throw new Exception("El cliente no existe - No se puede modificar.");
                        else if (resultado == -2)
                            throw new Exception("Error inesperado al modificar el cliente.");
                        else if (resultado != 1)
                            throw new Exception("Error desconocido al intentar modificar el cliente.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Ocurrió un error: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
            }
        }

        public static List<Cliente> ListarClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmd = new SqlCommand("ListarClientes", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente(
                         reader["Cedula"].ToString(),
                        reader["Nombre"].ToString(),
                         reader["NumeroTarjeta"].ToString(),
                         reader["Telefono"].ToString()
                        );
                    clientes.Add(cliente);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return clientes;

        }
    }
}
