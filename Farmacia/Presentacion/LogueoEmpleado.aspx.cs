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
    public partial class LogueoEmpleado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string Usuario = txtUsuario.Text.Trim();
                string Contrasena = txtContrasena.Text.Trim();

                Response.Write("<script>console.log('Usuario: " + Usuario + " , Contraseña: " + Contrasena + "');</script>");

                Empleado unEmpleado = LogicaEmpleado.Logueo(Usuario,Contrasena);

                if (unEmpleado != null)
                {
                    Session["Empleado"] = unEmpleado;

                    Response.Redirect("Bienvenida.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Datos incorrectos');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
    }
}