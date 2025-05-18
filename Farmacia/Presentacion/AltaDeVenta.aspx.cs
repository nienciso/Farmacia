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
    public partial class AltaDeVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Empleado"] == null)
            {

                Response.Redirect("~/Default.aspx");
                return;
            }
            if (!IsPostBack)
            {
                CargarArticulos();
                CargarClientes();
            }
        }

        private void CargarArticulos()
        {
            List<Articulo> articulos = LogicaArticulos.ListarArticulos();

            if (articulos != null && articulos.Count > 0)
            {
                ddlArticulo.DataSource = articulos;
                ddlArticulo.DataTextField = "Nombre";
                ddlArticulo.DataValueField = "Codigo";
                ddlArticulo.DataBind();
                ddlArticulo.Items.Insert(0, new ListItem("Seleccione un artículo", ""));

                Session["Articulos"] = articulos;
            }
        }
        private void CargarClientes()
        {
            List<Cliente> clientes = LogicaClientes.listarClientes();
            ddlCliente.DataSource = clientes;
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataValueField = "Cedula";
            ddlCliente.DataBind();
            ddlCliente.Items.Insert(0, new ListItem("Seleccione un cliente", ""));

            Session["Clientes"] = clientes;
        }

        protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                object objEmpleado = Session["Empleado"];
                if (objEmpleado == null || !(objEmpleado is Empleado))
                {
                    MostrarMensaje("Sesión expirada o datos no disponibles. Vuelva a cargar la página.", true);
                    return;
                }
                Empleado empleado = (Empleado)objEmpleado;

                string direccion = txtDireccion.Text.Trim();
                string cedula = ddlCliente.SelectedValue;
                string codigo = ddlArticulo.SelectedValue;
                string cantidadText = txtCantidad.Text.Trim();

                if (string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(cedula) ||
                    string.IsNullOrEmpty(codigo) || string.IsNullOrEmpty(cantidadText))
                {
                    MostrarMensaje("Debe completar todos los campos.", true);
                    return;
                }

                if (!int.TryParse(cantidadText, out int cantidad) || cantidad <= 0)
                {
                    MostrarMensaje("La cantidad debe ser un número válido mayor a 0.", true);
                    return;
                }

                var clientes = Session["Clientes"] as List<Cliente>;
                var articulos = Session["Articulos"] as List<Articulo>;

                if (clientes == null || articulos == null)
                {
                    MostrarMensaje("Sesión expirada o datos no disponibles. Vuelva a cargar la página.", true);
                    return;
                }

                Cliente cliente = clientes.FirstOrDefault(c => c.Cedula == cedula);
                Articulo articulo = articulos.FirstOrDefault(a => a.Codigo == codigo);

                if (cliente == null || articulo == null)
                {
                    MostrarMensaje("Cliente o artículo no válido.", true);
                    return;
                }

                Venta nuevaVenta = new Venta(0, DateTime.Now, "Armado", direccion, cantidad, empleado, articulo, cliente);
                LogicaAltaDeVenta.Agregar(nuevaVenta);

                MostrarMensaje("Venta registrada con éxito.", false);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrió un error inesperado: " + ex.Message, true);
            }
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensaje.CssClass = esError ? "error" : "success";
            lblMensaje.Text = mensaje;
        }

        private void LimpiarCampos()
        {
            txtDireccion.Text = "";
            ddlCliente.SelectedIndex = 0;
            txtCantidad.Text = "";
            ddlArticulo.SelectedIndex = 0;
        }
    }
}