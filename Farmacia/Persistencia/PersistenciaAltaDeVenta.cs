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
    public class PersistenciaAltaDeVenta
    {
        public static List<Venta> ObtenerVentasPorCliente(Cliente cliente)
        {
            List<Venta> ventas = new List<Venta>();

            if (cliente == null || string.IsNullOrWhiteSpace(cliente.Cedula))
                throw new Exception("Cliente inválido");

            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("ObtenerVentasPorCliente", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@clienteCedula", cliente.Cedula);
                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            int numeroVenta = Convert.ToInt32(lector["NumeroVenta"]);
                            DateTime fecha = Convert.ToDateTime(lector["Fecha"]);
                            string estado = lector["Estado"].ToString();
                            string direccion = lector["Direccion"].ToString();
                            int cantidadNumero = Convert.ToInt32(lector["cantidadNumero"]);
                            string usuario = lector["Usuario"].ToString();

                            Empleado empleado = PersistenciaEmpleado.BuscarEmpleado(usuario);
                            if (empleado == null)
                                throw new Exception("No se encontró el empleado asociado a la venta.");

                            string codigoArticulo = lector["CodigoArticulo"].ToString();  

                            Articulo articulo = PersistenciaArticulos.BuscarArticulos(codigoArticulo);
                            if (articulo == null)
                                throw new Exception("No se encontró el artículo asociado a la venta.");

                            Venta venta = new Venta(numeroVenta, fecha, estado, direccion, cantidadNumero, empleado, articulo, cliente);

                            ventas.Add(venta);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener las ventas por cliente: " + ex.Message);
                }
            }

            return ventas;
        }

        public static List<Venta> ObtenerVentasPendientes()
        {
            List<Venta> ventasPendientes = new List<Venta>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
                {
                    SqlCommand comando = new SqlCommand("ObtenerVentasPendientes", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            string cedula = lector["CedulaCliente"].ToString();
                            string codigoArticulo = lector["CodigoArticulo"].ToString();
                            string usuarioEmpleado = lector["Usuario"].ToString(); 

                            
                            Cliente cliente = PersistenciaCliente.BuscarCI(cedula);
                           
                            Articulo articulo = PersistenciaArticulos.BuscarArticulos(codigoArticulo);

                            
                            if (cliente == null)
                                throw new Exception($"No se encontró el cliente con cédula: {cedula}");

                            if (articulo == null)
                                throw new Exception($"No se encontró el artículo con código: {codigoArticulo}");

                            
                            Empleado empleado = PersistenciaEmpleado.BuscarEmpleado(usuarioEmpleado);
                            if (empleado == null)
                                throw new Exception($"No se encontró el empleado con usuario: {usuarioEmpleado}");

                            Venta venta = new Venta(
                                numeroVenta: Convert.ToInt32(lector["NumeroVenta"]),
                                fecha: Convert.ToDateTime(lector["Fecha"]),
                                estado: lector["Estado"].ToString(),
                                direccion: lector["Direccion"].ToString(),
                                cantidadNumero: Convert.ToInt32(lector["cantidadNumero"]),
                                empleado: empleado,  
                                articulo: articulo,
                                cliente: cliente
                            );

                            ventasPendientes.Add(venta);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ventas pendientes: " + ex.Message);
            }

            return ventasPendientes;
        }

        public static Venta BuscarVenta(int numeroVenta)
        {
            if (numeroVenta <= 0)
                throw new Exception("Debe ingresar un número de venta válido.");

            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("BuscarVenta", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@numeroVenta", numeroVenta);

                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            string cedulaCliente = lector["Cedula"].ToString();
                            string codigoArticulo = lector["Codigo"].ToString();
                            string usuarioEmpleado = lector["Usuario"].ToString();

                            Cliente cliente = PersistenciaCliente.BuscarCI(cedulaCliente);
                            Articulo articulo = PersistenciaArticulos.BuscarArticulos(codigoArticulo);
                            Empleado empleado = PersistenciaEmpleado.BuscarEmpleado(usuarioEmpleado);

                            if (cliente == null)
                                throw new Exception("No se encontró el cliente asociado a la venta.");

                            if (articulo == null)
                                throw new Exception("No se encontró el artículo asociado a la venta.");

                            if (empleado == null)
                                throw new Exception("No se encontró el empleado asociado a la venta.");

                            Venta venta = new Venta(
                                Convert.ToInt32(lector["NumeroVenta"]),
                                Convert.ToDateTime(lector["Fecha"]),
                                lector["Estado"].ToString(),
                                lector["Direccion"].ToString(),
                                Convert.ToInt32(lector["cantidadNumero"]),
                                empleado,
                                articulo,
                                cliente
                            );

                            return venta;
                        }
                        else
                        {
                            return null; 
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar la venta: " + ex.Message);
                }
            }
        }

        public static int RegistrarVenta(Venta venta, string usuarioEmpleado)
        {
            int numeroVentaGenerado = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                SqlCommand comando = new SqlCommand("RegistrarVenta", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                comando.Parameters.AddWithValue("@Cantidad", venta.CantidadNumero);
                comando.Parameters.AddWithValue("@Direccion", venta.Direccion);
                comando.Parameters.AddWithValue("@Usuario", usuarioEmpleado);
                comando.Parameters.AddWithValue("@Cedula", venta.Cliente.Cedula);
                comando.Parameters.AddWithValue("@CodigoArticulo", venta.Articulo.Codigo);

                SqlParameter paramNumeroVenta = new SqlParameter("@NumeroVenta", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                comando.Parameters.Add(paramNumeroVenta);

                try
                {
                    Console.WriteLine($"Cantidad: {venta.CantidadNumero}");
                    Console.WriteLine($"Direccion: {venta.Direccion}");
                    Console.WriteLine($"Usuario: {usuarioEmpleado}");
                    Console.WriteLine($"Cedula Cliente: {venta.Cliente.Cedula}");
                    Console.WriteLine($"Código Artículo: {venta.Articulo.Codigo}");

                    conexion.Open();
                    comando.ExecuteNonQuery();

                    numeroVentaGenerado = Convert.ToInt32(paramNumeroVenta.Value);
                    Console.WriteLine($"Venta registrada con éxito. Número de Venta: {numeroVentaGenerado}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al registrar la venta: " + ex.Message);
                }
            }

            return numeroVentaGenerado;
        }

        public static bool ActualizarEstado(int numeroVenta, string nuevoEstado)
        {
            if (numeroVenta <= 0)
                throw new Exception("Debe ingresar un número de venta válido.");

            if (string.IsNullOrEmpty(nuevoEstado))
                throw new Exception("El nuevo estado no puede estar vacío.");

            using (SqlConnection conexion = new SqlConnection(Conexion.Cnn))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("ActualizarEstado", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@numeroVenta", numeroVenta);
                    comando.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);

                    conexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar el estado de la venta: " + ex.Message);
                }
            }
        }

    }
}