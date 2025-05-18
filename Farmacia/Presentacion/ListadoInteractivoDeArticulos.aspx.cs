using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Logica;
using Farmacia;

namespace Presentacion
{
    public partial class ListadoInteractivoDeArticulos : System.Web.UI.Page
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
                    CargarCategorias(); 
                    Session["Articulos"] = null;
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Ocurrió un error al cargar la página: " + ex.Message;
                    throw new Exception("Ocurrió un error.", ex);
                }
            }
        }

        private void CargarVentasPendientes()
        {
            try
            {
                List<Venta> ventas = LogicaAltaDeVenta.ObtenerVentasPendientes();

                if (ventas != null && ventas.Count > 0)
                {
                    gvVentas.DataSource = ventas;
                    gvVentas.DataBind();
                }
                else
                {
                    gvVentas.DataSource = null;
                    gvVentas.DataBind();
                    lblMensaje.Text = "No hay ventas pendientes.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar ventas pendientes: " + ex.Message;
                throw new Exception("Ocurrió un error.", ex);
            }
        }

        private void CargarCategorias()
        {
            try
            {
                var categorias = LogicaCategorias.BuscarTodasCategorias();

                if (categorias != null && categorias.Count > 0)
                {
                    ddlCategoria.DataSource = categorias;
                    ddlCategoria.DataTextField = "Nombre";
                    ddlCategoria.DataValueField = "Codigo";
                    ddlCategoria.DataBind();

                    ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", ""));
                }
                else
                {
                    ddlCategoria.Items.Clear();
                    ddlCategoria.Items.Add(new ListItem("No se encontraron categorías", ""));
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al cargar las categorías: " + ex.Message;
                throw new Exception("Ocurrió un error al cargar las categorías.", ex);
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string codigoCategoria = ddlCategoria.SelectedValue;

                if (string.IsNullOrEmpty(codigoCategoria))
                {
                    ddlArticulo.Items.Clear();
                    ddlArticulo.Items.Add(new ListItem("Seleccione una categoría", ""));

                    gvVentas.DataSource = null;
                    gvVentas.DataBind();

                    lblCodigo.Text = "";
                    lblNombre.Text = "";
                    lblCategoria.Text = "";
                    lblPresentacion.Text = "";
                    lblTamaño.Text = "";
                    lblPrecio.Text = "";
                    lblNumVenta.Text = "";
                    lblFechaVenta.Text = "";
                    lblEstadoVenta.Text = "";
                    lblDireccion.Text = "";
                    lblCantidad.Text = "";
                    lblCliente.Text = "";
                    lblMensaje.Text = "Seleccione una categoría válida.";

                    return;
                }

                CargarArticulos(codigoCategoria);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al cargar los artículos: " + ex.Message;
                throw new Exception("Ocurrió un error al cargar los artículos.", ex);
            }
        }

        private void CargarArticulos(string codigoCategoria)
        {
            try
            {
                Categoria categoria = LogicaCategorias.BuscarCategoria(codigoCategoria);

                if (categoria != null)
                {
                    var articulos = LogicaArticulos.BuscarArticulosPorCategoria(categoria);

                    if (articulos != null && articulos.Count > 0)
                    {
                        Session["Articulos"] = articulos;

                        ddlArticulo.DataSource = articulos;
                        ddlArticulo.DataTextField = "Nombre";
                        ddlArticulo.DataValueField = "Codigo";
                        ddlArticulo.DataBind();

                        ddlArticulo.Items.Insert(0, new ListItem("Seleccione un artículo", ""));
                    }
                    else
                    {
                        ddlArticulo.Items.Clear();
                        ddlArticulo.Items.Add(new ListItem("No se encontraron artículos", ""));
                        Session["Articulos"] = null;

                        lblCodigo.Text = "";
                        lblNombre.Text = "";
                        lblCategoria.Text = "";
                        lblPresentacion.Text = "";
                        lblTamaño.Text = "";
                        lblPrecio.Text = "";

                        lblNumVenta.Text = "";
                        lblFechaVenta.Text = "";
                        lblEstadoVenta.Text = "";
                        lblDireccion.Text = "";
                        lblCantidad.Text = "";
                        lblCliente.Text = "";
                        gvVentas.DataSource = null;
                        gvVentas.DataBind();

                        lblMensaje.Text = "No se encontraron artículos para esta categoría.";
                    }
                }
                else
                {
                    ddlArticulo.Items.Clear();
                    ddlArticulo.Items.Add(new ListItem("Categoría no encontrada", ""));
                    Session["Articulos"] = null;

                    lblCodigo.Text = "";
                    lblNombre.Text = "";
                    lblCategoria.Text = "";
                    lblPresentacion.Text = "";
                    lblTamaño.Text = "";
                    lblPrecio.Text = "";

                    lblNumVenta.Text = "";
                    lblFechaVenta.Text = "";
                    lblEstadoVenta.Text = "";
                    lblDireccion.Text = "";
                    lblCantidad.Text = "";
                    lblCliente.Text = "";
                    gvVentas.DataSource = null;
                    gvVentas.DataBind();

                    lblMensaje.Text = "No se encontró la categoría.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al cargar los artículos: " + ex.Message;
                throw new Exception("Ocurrió un error al cargar los artículos.", ex);
            }
        }

        protected void ddlArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblCodigo.Text = "";
                lblNombre.Text = "";
                lblCategoria.Text = "";
                lblPresentacion.Text = "";
                lblTamaño.Text = "";
                lblPrecio.Text = "";
                lblMensaje.Text = "";

                lblNumVenta.Text = "";
                lblFechaVenta.Text = "";
                lblEstadoVenta.Text = "";
                lblDireccion.Text = "";
                lblCantidad.Text = "";
                lblCliente.Text = "";

                gvVentas.DataSource = null;
                gvVentas.DataBind();

                string codigoArticuloStr = ddlArticulo.SelectedValue.Trim();

                if (string.IsNullOrEmpty(codigoArticuloStr))
                {
                    lblMensaje.Text = "Seleccione un artículo válido.";
                    return;
                }

                var articulos = Session["Articulos"] as List<Articulo>;

                if (articulos != null)
                {

                    Articulo articulo = articulos.FirstOrDefault(a => a.Codigo == codigoArticuloStr);

                    if (articulo != null)
                    {

                        lblCodigo.Text = articulo.Codigo;
                        lblNombre.Text = articulo.Nombre;
                        lblCategoria.Text = articulo.Categoria?.Nombre ?? "Sin categoría";
                        lblPresentacion.Text = articulo.TipoPresentacion;
                        lblTamaño.Text = articulo.Tamaño ?? "Desconocido";
                        lblPrecio.Text = articulo.Precio.ToString("C");

                        List<Venta> ventasPendientes = LogicaAltaDeVenta.ObtenerVentasPendientes();
                        List<Venta> ventasFiltradas = new List<Venta>();

                        foreach (Venta venta in ventasPendientes)
                        {
                            List<Articulo> articulosComprados = LogicaArticulos.ObtenerArticulosComprados(venta.Cliente);
                            if (articulosComprados.Any(a => a.Codigo == codigoArticuloStr))
                            {
                                ventasFiltradas.Add(venta);
                            }
                        }

                        if (ventasFiltradas.Count > 0)
                        {
                            gvVentas.DataSource = ventasFiltradas;
                            gvVentas.DataBind();
                            lblMensaje.Text = $"Se encontraron {ventasFiltradas.Count} venta(s) con este artículo.";
                        }
                        else
                        {
                            lblMensaje.Text = "No se encontraron ventas para este artículo.";
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "Artículo no encontrado en la lista.";
                    }
                }
                else
                {
                    lblMensaje.Text = "No hay artículos cargados en sesión.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al procesar el artículo: " + ex.Message;
            }
        }

        protected void gvVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvVentas.SelectedRow;
                string numeroVentaStr = row.Cells[0].Text;

                Empleado empleado = Session["Empleado"] as Empleado;

                if (empleado == null)
                {
                    lblNumVenta.Text = "Error: No se encontró el empleado.";
                    return;
                }

                if (int.TryParse(numeroVentaStr, out int numeroVenta))
                {
                    Venta venta = LogicaAltaDeVenta.BuscarVenta(numeroVenta);

                    if (venta != null)
                    {
                        lblNumVenta.Text = venta.NumeroVenta.ToString();
                        lblFechaVenta.Text = venta.Fecha.ToString("dd/MM/yyyy");
                        lblEstadoVenta.Text = venta.Estado;
                        lblDireccion.Text = venta.Direccion;
                        lblCantidad.Text = venta.CantidadNumero.ToString();
                        lblCliente.Text = venta.Cliente.Nombre;
                    }
                    else
                    {
                        lblNumVenta.Text = "Venta no encontrada.";
                    }
                }
                else
                {
                    lblNumVenta.Text = "Error: Número de venta inválido.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al procesar la venta: " + ex.Message;
            }
        }
    }
}
