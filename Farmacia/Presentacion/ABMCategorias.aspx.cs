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
    public partial class ABMCategorias : System.Web.UI.Page
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

                if (string.IsNullOrEmpty(codigo) || string.IsNullOrEmpty(nombre))
                {
                    lblMensaje.CssClass = "error";
                    lblMensaje.Text = "Debe ingresar un código y un nombre.";
                    return;
                }

                Categoria categoria = new Categoria(codigo, nombre);
                LogicaCategorias.Agregar(categoria);

                lblMensaje.CssClass = "Success";
                lblMensaje.Text = "Categoría agregada con éxito.";


                txtCodigo.Text = "";
                txtNombre.Text = "";
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "error";
                lblMensaje.Text = "Ocurrió un error: " + ex.Message;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = txtCodigo.Text.Trim();
                string nombre = txtNombre.Text.Trim();

                if (string.IsNullOrEmpty(codigo) || string.IsNullOrEmpty(nombre))
                    throw new Exception("Todos los campos son obligatorios.");

                Categoria categoria = new Categoria(codigo, nombre);

                LogicaCategorias.Modificar(categoria);

                lblMensaje.Text = "Categoría modificada con éxito.";
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
                if (txtCodigo == null)
                    throw new Exception("txtCodigo no inicializado.");

                string codigo = txtCodigo.Text?.Trim();
                string nombre = txtNombre.Text?.Trim();

                if (string.IsNullOrEmpty(codigo))
                    throw new Exception("Debe ingresar un código para eliminar.");

                Categoria categoriaAEliminar = new Categoria(codigo, nombre);

                LogicaCategorias.Eliminar(categoriaAEliminar);

                lblMensaje.CssClass = "success";
                lblMensaje.Text = "Categoría eliminada con éxito.";
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "error";
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}