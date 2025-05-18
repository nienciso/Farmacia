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
    public class LogicaArticulos
    {
        public static List<Articulo> BuscarArticulosPorCategoria(Categoria categoria)
        {
            try
            {
                if (categoria == null || string.IsNullOrWhiteSpace(categoria.Codigo))
                    throw new Exception("Debe proporcionar una categoría válida.");

                return PersistenciaArticulos.BuscarArticulosPorCategoria(categoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar los artículos por categoría: " + ex.Message);
            }
        }

        public static List<Articulo> ObtenerArticulosComprados(Cliente cliente)
        {
            if (cliente == null || string.IsNullOrWhiteSpace(cliente.Cedula))
                throw new Exception("Debe proporcionar un cliente válido.");

            return PersistenciaArticulos.ObtenerArticulosComprados(cliente);
        }

        public static Articulo BuscarArticulos(string codigo)
        {
            try
            {
                return PersistenciaArticulos.BuscarArticulos(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el Articulo : " + ex.Message);
            }
        }

        public static void Agregar(Articulo articulo)
        {
            if (articulo != null)
                PersistenciaArticulos.AgregarArticulo(articulo);
        }

        public static bool Modificar(Articulo articulo)
        {
            try
            {
                PersistenciaArticulos.ModificarArticulo(articulo);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void Eliminar(Articulo articulo)
        {
            if (articulo != null)
                PersistenciaArticulos.EliminarArticulo(articulo);
        }

        public static List<Articulo> ListarArticulos()
        {
            try
            {
                return PersistenciaArticulos.ListarArticulos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar articulos: " + ex.Message);
            }
        }
    }
}
