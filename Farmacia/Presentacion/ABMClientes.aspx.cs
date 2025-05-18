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
    public partial class ABMClientes : System.Web.UI.Page
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
                string cedula = txtCI.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string numeroTarjeta = txtTarjeta.Text.Trim();
                string telefono = txtTelefono.Text.Trim();

                if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombre) ||
                    string.IsNullOrEmpty(numeroTarjeta) || string.IsNullOrEmpty(telefono))
                {
                    lblMensaje.CssClass = "error";
                    lblMensaje.Text = "Debe completar todos los campos.";
                    return;
                }

                Cliente nuevoCliente = new Cliente(cedula, nombre, numeroTarjeta, telefono);

                LogicaClientes.Agregar(nuevoCliente);

                lblMensaje.CssClass = "success";
                lblMensaje.Text = "Cliente agregado con éxito.";

                txtCI.Text = "";
                txtNombre.Text = "";
                txtTarjeta.Text = "";
                txtTelefono.Text = "";
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
                string cedula = txtCI.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string numeroTarjeta = txtTarjeta.Text.Trim();
                string telefono = txtTelefono.Text.Trim();

                if (string.IsNullOrEmpty(cedula))
                {
                    throw new Exception("Debe ingresar la cédula del cliente.");
                }

                Cliente cliente = LogicaClientes.Buscar(cedula);

                if (cliente == null)
                {
                    throw new Exception("Cliente no encontrado.");
                }

                if (string.IsNullOrEmpty(nombre) && string.IsNullOrEmpty(numeroTarjeta) && string.IsNullOrEmpty(telefono))
                {
                    txtNombre.Text = cliente.Nombre;
                    txtTarjeta.Text = cliente.NumeroTarjeta;
                    txtTelefono.Text = cliente.Telefono;
                    lblMensaje.Text = "Datos cargados. Ahora puede modificarlos.";
                    lblMensaje.ForeColor = System.Drawing.Color.Blue;
                    return;
                }

                cliente.Nombre = nombre;
                cliente.NumeroTarjeta = numeroTarjeta;
                cliente.Telefono = telefono;

                LogicaClientes.Modificar(cliente);

                lblMensaje.Text = "Cliente modificado con éxito.";
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
                string cedula = txtCI.Text.Trim();

                if (string.IsNullOrEmpty(cedula))
                {
                    lblMensaje.CssClass = "error";
                    lblMensaje.Text = "Error: Debe ingresar una cédula para eliminar.";
                    return;
                }

                Cliente cliente = LogicaClientes.Buscar(cedula);

                if (cliente == null)
                {
                    lblMensaje.CssClass = "error";
                    lblMensaje.Text = "Error: Cliente no encontrado.";
                    return;
                }

                LogicaClientes.Eliminar(cliente);

                lblMensaje.CssClass = "success";
                lblMensaje.Text = "Cliente eliminado correctamente.";

                txtCI.Text = "";
                txtNombre.Text = "";
                txtTarjeta.Text = "";
                txtTelefono.Text = "";

                txtNombre.Enabled = false;
                txtTarjeta.Enabled = false;
                txtTelefono.Enabled = false;
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "error";
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}