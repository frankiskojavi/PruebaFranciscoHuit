using PruebaHabilidadesFranciscoHuit.BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaHabilidadesFranciscoHuit.FrontEnd
{
    public partial class InicioSesion : System.Web.UI.Page
    {
        public Boolean blnError = false;
        public string error = "";
        ClsUsuario usuarios = ClsUsuario.getInstancia();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Session.Clear();
                }
                catch (Exception ex)
                {
                    Session["ErrorAPP"] = ex.Message;
                    Response.Redirect("~/FrontEnd/Excepciones.aspx", false);
                }

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                blnError = false;
                string nombreUsuario = txtNombreUsuario.Text;
                string passwordUsuario = txtContraseña.Text;
                if (!usuarios.ConsultarCredencialesUsuario(nombreUsuario, passwordUsuario))
                {
                    error = "El nombre y la contraseña no se encuentran registrados en el sistema. ";
                    blnError = true;
                    return;
                }
                else
                {
                    Response.Redirect("~/FrontEnd/Principal.aspx", false);
                    usuarios.ActualizarFecha(Session["CodigoUsuario"].ToString());
                }
            }
            catch (Exception ex)
            {
                Session["ErrorAPP"] = " Ocurrio un error al recuperar la informacion del usuario " + ex.Message;
                Response.Redirect("~/FrontEnd/Excepciones.aspx");
            }
        }
    }
}
