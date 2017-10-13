using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaHabilidadesFranciscoHuit.FrontEnd
{
    public partial class PHFMaestro : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["NombreUsuario"] == null) Response.Redirect("~/FrontEnd/InicioSesion.aspx", true);
            if (!IsPostBack)
            {                
                verificarPermisos();
            }
        }
        protected void lnkPaginaPrincipal_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/Principal.aspx", false);
        }
        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/InicioSesion.aspx", false);
        }

        protected void lnkUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/Usuario.aspx", false);
        }
        public void verificarPermisos()
        {
            if (Session["CodigoRol"].ToString().Equals("1"))
            {
                lnkProductos.Visible = false;
                lnkProveedores.Visible = false;
                lnkUsuarios.Visible = true;
            }else
            {
                lnkProductos.Visible = true;
                lnkProveedores.Visible = true;
                lnkUsuarios.Visible = false;
            }
        }

        protected void lnkProveedores_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/Proveedor.aspx", false);
        }

        protected void lnkProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/Producto.aspx", false);
        }
    }
}