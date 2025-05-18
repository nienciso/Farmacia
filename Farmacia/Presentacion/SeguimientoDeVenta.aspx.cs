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
    public partial class SeguimientoDeVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Empleado"] == null)
            {
               
                Response.Redirect("~/Default.aspx"); 
                return; 
            }
        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            btnConfirmarCambio.Enabled = false;

            if (!int.TryParse(txtNumeroVenta.Text, out int numeroVenta))
            {
                lblMensaje.CssClass = "error";
                lblMensaje.Text = "Ingrese un número de venta válido.";
                return;
            }

            Empleado empleado = Session["Empleado"] as Empleado;

            if (empleado == null)
            {
                lblMensaje.CssClass = "error";
                lblMensaje.Text = "No se ha encontrado el empleado asociado a la venta.";
                return;
            }

            Venta venta = LogicaAltaDeVenta.BuscarVenta(numeroVenta);

            if (venta == null)
            {
                lblMensaje.CssClass = "error";
                lblMensaje.Text = "Número de venta no encontrado.";
                return;
            }

            lblEstadoActual.Text = venta.Estado;
            string estadoSiguiente = ObtenerSiguienteEstado(venta.Estado);
            lblEstadoSiguiente.Text = estadoSiguiente ?? "-";

            if (estadoSiguiente != null)
                btnConfirmarCambio.Enabled = true;
            else
                lblMensaje.Text = "Esta venta ya está en su estado final.";
        }



        private string ObtenerSiguienteEstado(string estadoActual)
        {
            switch (estadoActual)
            {
                case "Armado": return "Envío";
                case "Envío": return "Entregado";
                case "Entregado": return "Devuelto";
                default: return null;
            }
        }

        protected void btnConfirmarCambio_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";

            if (!int.TryParse(txtNumeroVenta.Text, out int numeroVenta))
            {
                lblMensaje.CssClass = "error";
                lblMensaje.Text = "Ingrese un número de venta válido.";
                return;
            }

            string nuevoEstado = lblEstadoSiguiente.Text;
            if (LogicaAltaDeVenta.ActualizarEstado(numeroVenta, nuevoEstado))
            {
                lblMensaje.CssClass = "success";
                lblMensaje.Text = "Estado actualizado correctamente.";
                lblEstadoActual.Text = nuevoEstado;
                lblEstadoSiguiente.Text = ObtenerSiguienteEstado(nuevoEstado) ?? "-";
                btnConfirmarCambio.Enabled = false;
            }
            else
            {
                lblMensaje.CssClass = "error";
                lblMensaje.Text = "Error al actualizar el estado.";
            }
        }
    }
}