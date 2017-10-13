using PruebaHabilidadesFranciscoHuit.BackEnd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaHabilidadesFranciscoHuit.FrontEnd
{
    public partial class Usuario : System.Web.UI.Page
    {
        ClsUsuario clUsuario = ClsUsuario.getInstancia();
        public Boolean blnError;
        public String error;
        public String registroEliinar;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                Session["Modificar"] = false;
                error = "";
                blnError = false;                
                consultarUsuarios();
                recuperarPerfiles();
                protegerCampos(true);
            }
        }

        protected void btnAgregarNuevoUsuario_Click(object sender, EventArgs e)
        {            
            mvlPrincipal.ActiveViewIndex = 1;            
            Session["Modificar"] = false;
            limpiarCapos();
            protegerCampos(true);
            btnGrabar.Visible = true;
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            // hace todas las validaciones del formulario 
            int codigoPerfil = int.Parse(dpdPerfil.SelectedValue.ToString());            
            if (!validacionesFormulario())
            {
                return;
            }

            // Verifica si es Modificacion o Grabar
            if (Boolean.Parse(Session["Modificar"].ToString()))
            {                
                clUsuario.modificarUsuario(int.Parse(Session["CodigoUsuarioDel"].ToString()), txtNombreUsuario.Text, txtApellido.Text, txtEmail.Text, txtNombreUsuarioInicioSesion.Text, txtContraseñaUsuario.Text, codigoPerfil);
            }
            else
            {
                clUsuario.IngresarUsuario(txtNombreUsuario.Text,txtApellido.Text,txtEmail.Text,txtNombreUsuarioInicioSesion.Text,txtContraseñaUsuario.Text, codigoPerfil);
            }
            consultarUsuarios();
        }
        //GrdUsuarios_RowCommand
        protected void GrdUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                recuperarPerfiles();
                if (e.CommandName.Equals("btnEliminar"))
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    GridViewRow row = GrdUsuarios.Rows[index];
                    Label lblCodigoUsuario = (Label)row.FindControl("lblCodigoUsuario");
                    Label lblNombreUsuario = (Label)row.FindControl("lblNombreCompleto");
                    int codigoUsuario = int.Parse(lblCodigoUsuario.Text);
                    registroEliinar = lblNombreUsuario.Text;
                    Session["CodigoUsuarioDel"] = codigoUsuario;

                    String script1 = " $(function () {  $(\"#modalAdvertencia\").modal(\"show\"); });";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "invokeModal", script1, true);
                    updPrincipal.Update();                    
                }

                if (e.CommandName.Equals("btnModificar"))
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    GridViewRow row = GrdUsuarios.Rows[index];
                    Label lblCodigoUsuario = (Label)row.FindControl("lblCodigoUsuario");
                    int codigoUsuario = int.Parse(lblCodigoUsuario.Text);
                    Session["CodigoUsuarioDel"] = codigoUsuario;

                    // Recupera los datos del usuario seleccionado y asigna los valores al textbox
                    DataTable dtblInformacionUsuario = clUsuario.ConsultarUsuarioEspecifico(codigoUsuario);
                    foreach (DataRow registro in dtblInformacionUsuario.Rows)
                    {
                        txtNombreUsuario.Text = registro["Nombres"].ToString();
                        txtApellido.Text = registro["Apellidos"].ToString();
                        txtEmail.Text = registro["Email"].ToString();
                        txtNombreUsuarioInicioSesion.Text = registro["Login"].ToString();
                        txtContraseñaUsuario.Text = registro["Password"].ToString();
                        dpdPerfil.Text = registro["codigoRol"].ToString();                        
                    }

                    mvlPrincipal.ActiveViewIndex = 1;                    
                    Session["Modificar"] = true;
                    protegerCampos(true);
                }
                if (e.CommandName.Equals("btnConsultar"))
                {                    
                    int index = int.Parse(e.CommandArgument.ToString());
                    GridViewRow row = GrdUsuarios.Rows[index];
                    Label lblCodigoUsuario = (Label)row.FindControl("lblCodigoUsuario");
                    int codigoUsuario = int.Parse(lblCodigoUsuario.Text);
                    Session["CodigoUsuarioDel"] = codigoUsuario;
                    // Recupera los datos del usuario seleccionado y asigna los valores al textbox
                    DataTable dtblInformacionUsuario = clUsuario.ConsultarUsuarioEspecifico(codigoUsuario);
                    foreach (DataRow registro in dtblInformacionUsuario.Rows)
                    {
                        txtNombreUsuario.Text = registro["Nombres"].ToString();
                        txtApellido.Text = registro["Apellidos"].ToString();
                        txtEmail.Text = registro["Email"].ToString();
                        txtNombreUsuarioInicioSesion.Text = registro["Login"].ToString();
                        txtContraseñaUsuario.Text = registro["Password"].ToString();
                        dpdPerfil.Text = registro["codigoRol"].ToString();
                        protegerCampos(false);
                    }
                    mvlPrincipal.ActiveViewIndex = 1;
                    btnGrabar.Visible = false; 
                }
            }
            catch (Exception ex)
            {
                mostrarError("No se pudo recuperar la informacion del usuario " + ex.Message);
            }


        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            consultarUsuarios();            
        }

        protected void btnAceptarReg_Click(object sender, EventArgs e)
        {
            try
            {
                clUsuario.eliminarRegistro(int.Parse(Session["CodigoUsuarioDel"].ToString()));
                Response.Redirect("~/FrontEnd/Usuario.aspx", false);
            }
            catch (Exception ex)
            {
                Session["ErrorAPP"] = ex.Message;
                Response.Redirect("~/FrontEnd/Excepciones.aspx", false);
            }

        }
       
        private void consultarUsuarios()
        {
            // Consulta todos los datos del usuario y los mete al grid
            mvlPrincipal.ActiveViewIndex = 0;
            GrdUsuarios.DataSource = clUsuario.ConsultarUsuariosSistema();
            GrdUsuarios.DataBind();
            if (GrdUsuarios.Rows.Count > 0)
            {
                GrdUsuarios.UseAccessibleHeader = true;
                GrdUsuarios.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdUsuarios.FooterRow.TableSection = TableRowSection.TableFooter;
                string script1 = "$('#ContentPlaceHolder1_GrdUsuarios').DataTable({ \"scrollY\":        \"270px\", \"scrollCollapse\": false, \"paging\":         false   });";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ssss", script1, true);
            }

        }

        private void recuperarPerfiles()
        {
            dpdPerfil.DataSource = clUsuario.consultarRol();
            dpdPerfil.DataTextField = "DescripcionRol";
            dpdPerfil.DataValueField = "CodigoRol";
            dpdPerfil.DataBind();
        }        
        private void mostrtxarError(String errorApp)
        {
            // Muestra la ventana modal del error 
            error = errorApp;
            blnError = true;
            String script1 = " $(function () {  $(\"#modalError\").modal(\"show\"); });";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "invokeModal", script1, true);
            updPrincipal.Update();
            return;
        }
        public void limpiarCapos()
        {
            txtNombreUsuario.Text = "";
            txtApellido.Text = "";
            txtNombreUsuarioInicioSesion.Text = "";
            txtContraseñaUsuario.Text = "";
            txtEmail.Text = "";
            txtApellido.Attributes.Add("autocomplete", "off");
        }              
        public Boolean validacionesFormulario()
        {
            if (txtNombreUsuario.Text.Equals(""))
            {
                mostrarError("El nombre no puede estar en blanco");
                return false;
            }
            if (txtApellido.Text.Equals(""))
            {
                mostrarError("El apellido no puede estar en blanco");
                return false;
            }
            if (txtEmail.Text.Equals(""))
            {
                mostrarError("El Email no puede estar en blanco");
                return false;
            }
            if (txtNombreUsuarioInicioSesion.Text.Equals(""))
            {
                mostrarError("El login no puede estar en blanco");
                return false;
            }
            if (txtContraseñaUsuario.Text.Equals(""))
            {
                mostrarError("La contraseña no puede estar en blanco");
                return false;
            }            
            return true;
        }

        protected void btnCancelarReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/Usuario.aspx", false);
        }

        public void protegerCampos(Boolean habilitar)
        {
            txtNombreUsuario.ReadOnly = !habilitar;
            txtApellido.ReadOnly = !habilitar;
            txtEmail.ReadOnly = !habilitar;
            txtNombreUsuarioInicioSesion.ReadOnly = !habilitar;
            txtContraseñaUsuario.ReadOnly = !habilitar;
            dpdPerfil.Enabled = habilitar;
        }
        private void mostrarError(String errorApp)
        {
            // Muestra la ventana modal del error 
            error = errorApp;
            blnError = true;
            String script1 = " $(function () {  $(\"#modalError\").modal(\"show\"); });";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "invokeModal", script1, true);
            updPrincipal.Update();
            return;
        }
    }
}