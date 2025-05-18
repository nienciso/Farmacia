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
    public class PersistenciaCategorias
    {
        public static Categoria BuscarCategoria(string codigo)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("BuscarCategoria", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@codigo", codigo);
                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            return new Categoria(
                                lector["Codigo"].ToString(),
                                lector["Nombre"].ToString()
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar la categoría: " + ex.Message);
                }
            }
        }

        public static List<Categoria> BuscarTodasLasCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();

            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("BuscarTodasCategorias", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            categorias.Add(new Categoria(
                                lector["Codigo"].ToString(),
                                lector["Nombre"].ToString()
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener todas las categorías: " + ex.Message);
                }
            }

            return categorias;
        }

        public static void AgregarCategoria(Categoria categoria)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("AgregarCategoria", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@codigo", categoria.Codigo);
            comando.Parameters.AddWithValue("@nombre", categoria.Nombre);

            SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

                int resultado = Convert.ToInt32(retorno.Value);

                if (resultado == -1)
                    throw new Exception("Categoría duplicada - no se puede agregar.");
                if (resultado == -2)
                    throw new Exception("No se puede agregar.");
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

        public static void EliminarCategoria(Categoria categoria)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                using (SqlCommand comando = new SqlCommand("EliminarCategoria", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@codigo", categoria.Codigo);

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
                            throw new Exception("La categoría no existe - No se puede eliminar.");
                        else if (resultado == -2)
                            throw new Exception("La categoría tiene artículos asociados - No se puede eliminar.");
                        else if (resultado == -3)
                            throw new Exception("Error inesperado al eliminar la categoría.");
                        else if (resultado != 1)
                            throw new Exception("Error desconocido al intentar eliminar la categoría.");
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

        public static void ModificarCategoria(Categoria categoria)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                using (SqlCommand comando = new SqlCommand("ModificarCategoria", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@codigo", categoria.Codigo);
                    comando.Parameters.AddWithValue("@nombre", categoria.Nombre);

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
                            throw new Exception("La categoría no existe - No se puede modificar.");
                        else if (resultado == -2)
                            throw new Exception("Error inesperado al modificar la categoría.");
                        else if (resultado != 1)
                            throw new Exception("Error desconocido al intentar modificar la categoría.");
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

        public static List<Categoria> ListarCategorias()
        {
            List<Categoria> listaCategorias = new List<Categoria>();
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = new SqlCommand("ListarCategorias", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Categoria categoria = new Categoria(
                        reader["Codigo"].ToString(),
                        reader["Nombre"].ToString()
                    );
                    listaCategorias.Add(categoria);
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

            return listaCategorias;
        }
    }
}
