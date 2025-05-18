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
    public class Conexion
    {
        private static string _cnn = @"Data source = localhost ; Initial Catalog = Farmacia_BD; Integrated security = true ; password = root";

        public static string Cnn
        {
            get { return _cnn; }
        }
    }
}
