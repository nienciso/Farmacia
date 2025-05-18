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
    public partial class ABMArticulos : System.Web.UI.Page
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
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = txtCodigo.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string tamaño = txtTamaño.Text.Trim();
                string codigoCategoria = txtCodigoCategoria.Text.Trim();
                string tipoPresentacion = ddlTipoPresentacion.Text.Trim();
                decimal precio;

                if (string.IsNullOrEmpty(nombre))
                {
                    lblMensaje.CssClass = "error";
                    lblMensaje.Text = "Error: El nombre no puede estar vacío.";
                    return;
                }

                if (string.IsNullOrEmpty(codigo) || string.IsNullOrEmpty(tamaño) || string.IsNullOrEmpty(codigoCategoria) || string.IsNullOrEmpty(tipoPresentacion) || !decimal.TryParse(txtPrecio.Text, out precio))
                {
                    lblMensaje.CssClass = "error";
                    lblMensaje.Text = "Error: Complete todos los campos correctamente.";
                    return;
                }
 
                Categoria categoria = LogicaCategorias.BuscarCategoria(codigoCategoria); 
                if (categoria == null)
                {
                    lblMensaje.CssClass = "error";
                    lblMensaje.Text = "Error: Categoría no válida.";
                    return;
                }

                Articulo articulo = new Articulo(codigo, nombre, tamaño, categoria, tipoPresentacion, precio);
                LogicaArticulos.Agregar(articulo);

                lblMensaje.CssClass = "success";
                lblMensaje.Text = "Artículo agregado con éxito.";

                txtCodigo.Text = "";
                txtNombre.Text = "";
                txtTamaño.Text = "";
                txtCodigoCategoria.Text = "";
                ddlTipoPresentacion.Text = "";
                txtPrecio.Text = "";
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "error";
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = txtCodigo.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string tamaño = txtTamaño.Text.Trim();
                string precioTexto = txtPrecio.Text.Trim();

                if (string.IsNullOrEmpty(codigo))
                {
                    lblMensaje.Text = "Debe ingresar un código de artículo.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                Articulo articulo = LogicaArticulos.BuscarArticulos(codigo);
                if (articulo == null)
                {
                    lblMensaje.Text = "Artículo no encontrado.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                Categoria categoria = LogicaCategorias.BuscarCategoria(articulo.Categoria.Codigo);
                if (categoria == null)
                {
                    lblMensaje.Text = "Error: Categoría no válida.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (string.IsNullOrEmpty(nombre) && string.IsNullOrEmpty(tamaño) && string.IsNullOrEmpty(precioTexto))
                {
                    txtNombre.Text = articulo.Nombre;
                    txtTamaño.Text = articulo.Tamaño;
                    txtPrecio.Text = articulo.Precio.ToString();
                    txtCodigoCategoria.Text = categoria.Codigo;
                    txtCodigoCategoria.Enabled = false;

                    lblMensaje.Text = "Datos cargados. Ahora puede modificarlos.";
                    lblMensaje.ForeColor = System.Drawing.Color.Blue;
                    return;
                }

                articulo.Nombre = nombre;
                articulo.Tamaño = tamaño;
                articulo.Precio = decimal.Parse(precioTexto);

                LogicaArticulos.Modificar(articulo);

                lblMensaje.Text = "Artículo modificado con éxito.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = txtCodigo.Text.Trim();

                if (string.IsNullOrEmpty(codigo))
                {
                    lblMensaje.CssClass = "error";
                    lblMensaje.Text = "Error: Debe ingresar un código de artículo.";
                    return;
                }

                Articulo articulo = LogicaArticulos.BuscarArticulos(codigo);

                if (articulo == null)
                {
                    lblMensaje.CssClass = "error";
                    lblMensaje.Text = "Error: No se encontró un artículo con el código ingresado.";
                    return;
                }

                LogicaArticulos.Eliminar(articulo);

                lblMensaje.CssClass = "success";
                lblMensaje.Text = "Artículo eliminado correctamente.";

                txtCodigo.Text = "";
                txtNombre.Text = "";
                txtTamaño.Text = "";
                txtCodigoCategoria.Text = "";
                ddlTipoPresentacion.SelectedIndex = 0;
                txtPrecio.Text = "";

                txtNombre.Enabled = false;
                txtTamaño.Enabled = false;
                txtCodigoCategoria.Enabled = false;
                ddlTipoPresentacion.Enabled = false;
                txtPrecio.Enabled = false;
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "error";
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}