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
    public partial class ListadoInteractivoDeClientes : System.Web.UI.Page
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
                try
                {
                    var clientes = LogicaClientes.listarClientes();
                    gvClientes.DataSource = clientes;
                    gvClientes.DataBind();
                    Session["Clientes"] = clientes;

                    LimpiarDetallesVenta();
                    LimpiarDetallesArticulo();
                }
                catch (Exception ex)
                {
                    lblCedulaCliente.Text = "Error al cargar clientes: " + ex.Message;
                }
            }
        }

        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvClientes.SelectedRow;
                string cedulaCliente = HttpUtility.HtmlDecode(row.Cells[0].Text).Trim();

                if (string.IsNullOrWhiteSpace(cedulaCliente))
                {
                    lblCedulaCliente.Text = "Cédula no válida.";
                    return;
                }

                var clientes = Session["Clientes"] as List<Cliente>;
                if (clientes == null)
                {
                    lblCedulaCliente.Text = "No se encontraron clientes en sesión.";
                    return;
                }

                var cliente = clientes.FirstOrDefault(c => c.Cedula == cedulaCliente);
                if (cliente == null)
                {
                    lblCedulaCliente.Text = $"Cliente con cédula {cedulaCliente} no encontrado.";
                    return;
                }

                lblCedulaCliente.Text = cliente.Cedula;
                lblNombreCliente.Text = cliente.Nombre;
                lblNumeroTarjetaCliente.Text = cliente.NumeroTarjeta;
                lblTelefonoCliente.Text = cliente.Telefono;

                var ventasCliente = LogicaAltaDeVenta.ObtenerVentasPorCliente(cliente);
                gvVentas.DataSource = ventasCliente;
                gvVentas.DataBind();
                Session["VentasCliente"] = ventasCliente;

                if (ventasCliente == null || !ventasCliente.Any())
                {
                    LimpiarDetallesVenta();
                    LimpiarDetallesArticulo();
                }

                gvArticulosComprados.Visible = false;
                lblMontoTotal.Text = "No se mostrarán artículos.";
            }
            catch (Exception ex)
            {
                lblCedulaCliente.Text = "Error inesperado: " + ex.Message;
            }
        }

        protected void gvVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvVentas.SelectedRow;

                string numeroVentaTexto = row.Cells[0].Text.Trim();
                string fechaTexto = row.Cells[1].Text.Trim();

                if (!int.TryParse(numeroVentaTexto, out int numeroVenta))
                {
                    lblVentaSeleccionada.Text = "Número de venta inválido.";
                    return;
                }

                if (!DateTime.TryParseExact(fechaTexto, "dd/MM/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime fecha))
                {
                    lblFechaVenta.Text = "Fecha inválida.";
                    return;
                }

                lblVentaSeleccionada.Text = numeroVenta.ToString();
                lblFechaVenta.Text = fecha.ToString("dd/MM/yyyy");

                var ventasCliente = Session["VentasCliente"] as List<Venta>;
                if (ventasCliente == null)
                {
                    lblEstadoVenta.Text = "Ventas no encontradas.";
                    return;
                }

                var venta = ventasCliente.FirstOrDefault(v => v.NumeroVenta == numeroVenta);
                if (venta == null)
                {
                    lblEstadoVenta.Text = "Venta no encontrada.";
                    return;
                }

                lblEstadoVenta.Text = venta.Estado ?? "N/A";
                lblDireccionVenta.Text = venta.Direccion ?? "N/A";
                lblEmpleadoVenta.Text = venta.Empleado?.Nombre ?? "Empleado no asignado";
                lblClienteVenta.Text = venta.Cliente?.Nombre ?? "Cliente no asignado";

                var articulos = LogicaArticulos.ObtenerArticulosComprados(venta.Cliente);

                gvArticulosComprados.Visible = true;

                if (articulos != null && articulos.Count > 0)
                {
                    gvArticulosComprados.DataSource = articulos;
                    gvArticulosComprados.DataBind();

                    decimal montoVenta = 0;
                    foreach (var articulo in articulos)
                    {
                        montoVenta += articulo.Precio;
                    }

                    lblMontoTotal.Text = montoVenta.ToString("C");
                }
                else
                {
                    gvArticulosComprados.DataSource = null;
                    gvArticulosComprados.DataBind();
                    lblMontoTotal.Text = "No se encontraron artículos para esta venta.";
                }
            }
            catch (Exception ex)
            {
                lblEstadoVenta.Text = "Error en venta: " + ex.Message;
            }
        }

        private void LimpiarDetallesVenta()
        {
            lblVentaSeleccionada.Text = "";
            lblFechaVenta.Text = "";
            lblEstadoVenta.Text = "";
            lblDireccionVenta.Text = "";
            lblEmpleadoVenta.Text = "";
            lblClienteVenta.Text = "";
            gvVentas.DataSource = null;
            gvVentas.DataBind();
        }

        private void LimpiarDetallesArticulo()
        {
            lblMontoTotal.Text = "";
            gvArticulosComprados.DataSource = null;
            gvArticulosComprados.DataBind();
            gvArticulosComprados.Visible = false;
        }
    }
}