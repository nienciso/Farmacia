using System;
using System.Web;
using System.Web.UI;
using Farmacia;
using Logica;

namespace Presentacion
{
    public partial class UnaMPEmpleado : System.Web.UI.MasterPage
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
                object objEmpleado = Session["Empleado"];
                if (objEmpleado == null || !(objEmpleado is Empleado))
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                Empleado unEmpleado = (Empleado)objEmpleado;
                lblEmpleado.Text = unEmpleado.Nombre;
            }
        }

        protected void Menu1_MenuItemClick(object sender, System.Web.UI.WebControls.MenuEventArgs e)
        {

        }
    }
}