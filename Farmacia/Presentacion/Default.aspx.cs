using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Farmacia;
using Logica;

namespace Presentacion
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["logout"] == "true")
            {
                Session.Clear();
                Session.Abandon();
            }

            if (!IsPostBack)
            {
                CargarVentasPendientes();
            }
        }

        private void CargarVentasPendientes()
        {
            try
            {
                List<Venta> ventasPendientes = LogicaAltaDeVenta.ObtenerVentasPendientes();
                gvVentasPendientes.DataSource = ventasPendientes;
                gvVentasPendientes.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar las ventas pendientes: " + ex.Message);
            }
        }
    }
}