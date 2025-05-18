using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Farmacia;
using Persistencia;

namespace Persistencia
{
    public class PersistenciaArticulos
    {
        public static List<Articulo> BuscarArticulosPorCategoria(Categoria categoria)
        {
            if (categoria == null || string.IsNullOrEmpty(categoria.Codigo))
            {
                throw new Exception("Debe proporcionar una categoría válida.");
            }

            List<Articulo> articulos = new List<Articulo>();

            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("BuscarArticulosPorCategoria", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@CodigoCategoria", categoria.Codigo);

                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Articulo articulo = new Articulo(
                                lector["Codigo"].ToString(),
                                lector["Nombre"].ToString(),
                                lector["Tamaño"].ToString(),
                                categoria,
                                lector["TipoPresentacion"].ToString(),
                                Convert.ToDecimal(lector["Precio"])
                            );

                            articulos.Add(articulo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar los artículos por categoría: " + ex.Message);
                }
            }

            return articulos;
        }

        public static List<Articulo> ObtenerArticulosComprados(Cliente cliente)
        {
            if (cliente == null || string.IsNullOrWhiteSpace(cliente.Cedula))
            {
                throw new Exception("Debe proporcionar un cliente válido.");
            }

            List<Articulo> listaArticulos = new List<Articulo>();

            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("ObtenerArticulosComprados", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@CedulaCliente", cliente.Cedula);

                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            try
                            {
                                string tamaño = lector["Tamaño"] != DBNull.Value ? lector["Tamaño"].ToString().Trim() : "Tamaño no disponible";

                                decimal tamañoDecimal;
                                if (decimal.TryParse(tamaño, out tamañoDecimal))
                                {
                                    tamaño = tamañoDecimal.ToString();
                                }

                                string codigoArticulo = lector["CodigoArticulo"].ToString();
                                string nombreArticulo = lector["NombreArticulo"].ToString();
                                string tipoPresentacion = lector["TipoPresentacion"].ToString();
                                decimal precio = Convert.ToDecimal(lector["Precio"]);

                                if (string.IsNullOrWhiteSpace(codigoArticulo) || string.IsNullOrWhiteSpace(nombreArticulo) || string.IsNullOrWhiteSpace(tipoPresentacion))
                                {
                                    throw new Exception("Uno de los campos obligatorios está vacío: CodigoArticulo, NombreArticulo o TipoPresentacion.");
                                }

                                string codigoCategoria = lector["CodigoCategoria"].ToString();
                                string nombreCategoria = lector["NombreCategoria"].ToString();

                                Categoria categoria = new Categoria(codigoCategoria, nombreCategoria); 

                                Articulo articulo = new Articulo(
                                    codigoArticulo,
                                    nombreArticulo,
                                    tamaño,
                                    categoria,
                                    tipoPresentacion,
                                    precio
                                );

                                listaArticulos.Add(articulo);
                            }
                            catch (Exception innerEx)
                            {
                                throw new Exception($"Error al procesar un artículo: {innerEx.Message}", innerEx);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener los artículos comprados por el cliente: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }

            return listaArticulos;
        }

        public static Articulo BuscarArticulos(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
                throw new Exception("Debe ingresar un código.");

            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("BuscarArticulo", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@Codigo", codigo);

                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            string codigoCategoria = Convert.ToString(lector["CodigoCategoria"]);

                            if (string.IsNullOrEmpty(codigoCategoria))
                            {
                                throw new Exception("La categoría del artículo no se encuentra asignada o está vacía.");
                            }

                            Categoria categoria = PersistenciaCategorias.BuscarCategoria(codigoCategoria);

                            if (categoria == null)
                            {
                                throw new Exception("No se encontró la categoría del artículo.");
                            }

                            Articulo articulo = new Articulo(
                                lector["Codigo"].ToString(),
                                lector["Nombre"].ToString(),
                                lector["Tamaño"].ToString(),
                                categoria,
                                lector["TipoPresentacion"].ToString(),
                                Convert.ToDecimal(lector["Precio"])
                            );

                            return articulo;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar el artículo: " + ex.Message);
                }
            }
        }

        public static void AgregarArticulo(Articulo articulo)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                using (SqlCommand comando = new SqlCommand("RegistrarArticulo", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@codigo", articulo.Codigo);
                    comando.Parameters.AddWithValue("@nombre", articulo.Nombre);
                    comando.Parameters.AddWithValue("@tamaño", articulo.Tamaño);
                    comando.Parameters.AddWithValue("@codigoCategoria", articulo.Categoria.Codigo);
                    comando.Parameters.AddWithValue("@tipoPresentacion", articulo.TipoPresentacion);
                    comando.Parameters.AddWithValue("@precio", articulo.Precio);

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
                            throw new Exception("Artículo duplicado - no se puede agregar.");
                        else if (resultado == -2)
                            throw new Exception("Código de categoría no válido - no se puede agregar.");
                        else if (resultado == -3)
                            throw new Exception("Ocurrió un error inesperado al agregar el artículo.");
                        else if (resultado != 1)
                            throw new Exception("Error desconocido al agregar el artículo.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Ocurrió un error al agregar el artículo: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
            }
        }

        public static void EliminarArticulo(Articulo articulo)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                using (SqlCommand comando = new SqlCommand("EliminarArticulo", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@codigo", articulo.Codigo);

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
                            throw new Exception("El artículo no existe - No se puede eliminar.");
                        else if (resultado == -2)
                            throw new Exception("El artículo está asociado a registros - No se puede eliminar.");
                        else if (resultado == -3)
                            throw new Exception("Error inesperado al eliminar el artículo.");
                        else if (resultado != 1)
                            throw new Exception("Error desconocido al intentar eliminar el artículo.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Ocurrió un error al eliminar el artículo: " + ex.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
            }
        }

        public static void ModificarArticulo(Articulo articulo)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                using (SqlCommand comando = new SqlCommand("ModificarArticulo", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@codigo", articulo.Codigo);
                    comando.Parameters.AddWithValue("@nombre", articulo.Nombre);
                    comando.Parameters.AddWithValue("@tamaño", articulo.Tamaño);
                    comando.Parameters.AddWithValue("@codigoCategoria", articulo.Categoria.Codigo);
                    comando.Parameters.AddWithValue("@tipoPresentacion", articulo.TipoPresentacion);
                    comando.Parameters.AddWithValue("@precio", articulo.Precio);

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
                            throw new Exception("El artículo no existe.");
                        if (resultado == -2)
                            throw new Exception("Ocurrió un error inesperado.");
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

        public static List<Articulo> ListarArticulos()
        {
            List<Articulo> articulos = new List<Articulo>();
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmd = new SqlCommand("ListarArticulos", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string codigoCategoria = reader["CodigoCategoria"].ToString();

                    Categoria categoria = PersistenciaCategorias.BuscarCategoria(codigoCategoria);

                    Articulo articulo = new Articulo(
                        reader["Codigo"].ToString(),
                        reader["Nombre"].ToString(),
                        reader["Tamaño"].ToString(),
                        categoria,
                        reader["TipoPresentacion"].ToString(),
                        Convert.ToDecimal(reader["Precio"])
                    );

                    articulos.Add(articulo);
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

            return articulos;
        }
    }
}