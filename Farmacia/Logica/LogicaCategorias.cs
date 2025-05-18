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
    public class LogicaCategorias
    {
        public static Categoria BuscarCategoria(string codigo)
        {
            try
            {
                return PersistenciaCategorias.BuscarCategoria(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar la categoria : " + ex.Message);
            }
        }

        public static List<Categoria> BuscarTodasCategorias()
        {
            try
            {
                return PersistenciaCategorias.BuscarTodasLasCategorias();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las categorías: " + ex.Message);
            }
        }

        public static void Agregar(Categoria categoria)
        {
            if (categoria != null)
                PersistenciaCategorias.AgregarCategoria(categoria);
        }

        public static void Modificar(Categoria categoria)
        {
            if (categoria != null)
                PersistenciaCategorias.ModificarCategoria(categoria);
        }

        public static void Eliminar(Categoria categoria)
        {
            if (categoria != null)
                PersistenciaCategorias.EliminarCategoria(categoria);
        }

    }
}
