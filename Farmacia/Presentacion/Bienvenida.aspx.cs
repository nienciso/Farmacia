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
    public partial class Bienvenida : System.Web.UI.Page
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
                Empleado unEmpleado = (Empleado)Session["Empleado"];

                
                Label lbl = (Label)this.Master.FindControl("lblEmpleado");
                if (lbl != null)
                    lbl.Text = unEmpleado.Nombre;
            }
        }
    }
}