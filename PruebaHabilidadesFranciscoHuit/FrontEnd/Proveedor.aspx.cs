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
    public partial class Proveedor : System.Web.UI.Page
    {
        ClsProveedor clProveedor = ClsProveedor.getInstancia();
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
                consultarProveedores();                
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
            if (!validacionesFormulario())
            {
                return;
            }
            // Verifica si es Modificacion o Grabar
            if (Boolean.Parse(Session["Modificar"].ToString()))
            {                
                clProveedor.modificarProveedor(int.Parse(Session["CodigoProveedorDEL"].ToString()), txtNombreProveedor.Text, txtNombreEmpresa.Text, txtNombreEncargado.Text, txtTelefono.Text, txtEmail.Text, txtDireccion.Text);
            }
            else
            {
                clProveedor.ingresarProveedor(txtNombreProveedor.Text, txtNombreEmpresa.Text, txtNombreEncargado.Text, txtTelefono.Text, txtEmail.Text, txtDireccion.Text);
            }
            consultarProveedores();
        }
        //GrdProveedores_RowCommand
        protected void GrdProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {                
                if (e.CommandName.Equals("btnEliminar"))
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    GridViewRow row = GrdProveedores.Rows[index];
                    Label lblCodigoProveedor = (Label)row.FindControl("lblCodigoProveedor");
                    Label lblDescripcionProveedor = (Label)row.FindControl("lblDescripcionProveedor");
                    int codigoProveedor = int.Parse(lblCodigoProveedor.Text);
                    registroEliinar = lblDescripcionProveedor.Text;
                    Session["CodigoProveedorDEL"] = codigoProveedor;

                    String script1 = " $(function () {  $(\"#modalAdvertencia\").modal(\"show\"); });";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "invokeModal", script1, true);
                    updPrincipal.Update();                    
                }

                if (e.CommandName.Equals("btnModificar"))
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    GridViewRow row = GrdProveedores.Rows[index];
                    Label lblCodigoProveedor = (Label)row.FindControl("lblCodigoProveedor");
                    int codigoProveedor = int.Parse(lblCodigoProveedor.Text);
                    Session["CodigoProveedorDEL"] = codigoProveedor;
                    // Recupera los datos del usuario seleccionado y asigna los valores al textbox
                    DataTable dtblInformacionUsuario = clProveedor.consultarProveedorEspecifico(codigoProveedor);
                    foreach (DataRow registro in dtblInformacionUsuario.Rows)
                    {
                        txtNombreProveedor.Text = registro["DescripcionProveedor"].ToString();
                        txtNombreEmpresa.Text = registro["NombreEmpresa"].ToString();
                        txtNombreEncargado.Text = registro["Encargado"].ToString();
                        txtTelefono.Text = registro["Telefono"].ToString();
                        txtEmail.Text = registro["Email"].ToString();
                        txtDireccion.Text = registro["Direccion"].ToString();
                        protegerCampos(false);
                    }
                    mvlPrincipal.ActiveViewIndex = 1;                    
                    Session["Modificar"] = true;
                    protegerCampos(true);
                }
                if (e.CommandName.Equals("btnConsultar"))
                {                    
                    int index = int.Parse(e.CommandArgument.ToString());
                    GridViewRow row = GrdProveedores.Rows[index];
                    Label lblCodigoProveedor = (Label)row.FindControl("lblCodigoProveedor");
                    int codigoProveedor = int.Parse(lblCodigoProveedor.Text);
                    Session["CodigoProveedorDEL"] = codigoProveedor;
                    // Recupera los datos del usuario seleccionado y asigna los valores al textbox
                    DataTable dtblInformacionUsuario = clProveedor.consultarProveedorEspecifico(codigoProveedor);
                    foreach (DataRow registro in dtblInformacionUsuario.Rows)
                    {
                        txtNombreProveedor.Text = registro["DescripcionProveedor"].ToString();
                        txtNombreEmpresa.Text = registro["NombreEmpresa"].ToString();
                        txtNombreEncargado.Text = registro["Encargado"].ToString();
                        txtTelefono.Text = registro["Telefono"].ToString();
                        txtEmail.Text = registro["Email"].ToString();
                        txtDireccion.Text = registro["Direccion"].ToString();
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
            consultarProveedores();            
        }

        protected void btnAceptarReg_Click(object sender, EventArgs e)
        {
            try
            {
                clProveedor.eliminarRegistro(int.Parse(Session["CodigoProveedorDel"].ToString()));
                Response.Redirect("~/FrontEnd/Proveedor.aspx", false);
            }
            catch (Exception ex)
            {
                Session["ErrorAPP"] = ex.Message;
                Response.Redirect("~/FrontEnd/Excepciones.aspx", false);
            }

        }
       
        private void consultarProveedores()
        {
            // Consulta todos los datos del usuario y los mete al grid
            mvlPrincipal.ActiveViewIndex = 0;
            GrdProveedores.DataSource = clProveedor.consultarProveedoresSistema();
            GrdProveedores.DataBind();
            if (GrdProveedores.Rows.Count > 0)
            {
                GrdProveedores.UseAccessibleHeader = true;
                GrdProveedores.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdProveedores.FooterRow.TableSection = TableRowSection.TableFooter;
                string script1 = "$('#ContentPlaceHolder1_GrdProveedores').DataTable({ \"scrollY\":        \"270px\", \"scrollCollapse\": false, \"paging\":         false   });";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ssss", script1, true);
            }

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
            txtNombreProveedor.Text = "";
            txtNombreEmpresa.Text = "";
            txtNombreEncargado.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtDireccion.Text = "";
        }              
        public Boolean validacionesFormulario()
        {
            if (txtNombreProveedor.Text.Equals(""))
            {
                mostrarError("El nombre no puede estar en blanco");
                return false;
            }
            if (txtNombreEmpresa.Text.Equals(""))
            {
                mostrarError("La empresa no puede estar en blanco");
                return false;
            }
            if (txtNombreEncargado.Text.Equals(""))
            {
                mostrarError("El nombre del encargado no puede estar en blanco");
                return false;
            }
            if (txtTelefono.Text.Equals(""))
            {
                mostrarError("El telefono no puede estar en blanco");
                return false;
            }
            if (txtEmail.Text.Equals(""))
            {
                mostrarError("El email no puede estar en blanco");
                return false;
            }
            if (txtDireccion.Text.Equals(""))
            {
                mostrarError("La direccion no puede estar en blanco");
                return false;
            }
            return true;
        }

        protected void btnCancelarReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrontEnd/Proveedor.aspx", false);
        }

        public void protegerCampos(Boolean habilitar)
        {
            txtNombreProveedor.ReadOnly = !habilitar;
            txtNombreEmpresa.ReadOnly = !habilitar;
            txtNombreEncargado.ReadOnly = !habilitar;
            txtTelefono.ReadOnly = !habilitar;
            txtEmail.ReadOnly = !habilitar;
            txtDireccion.ReadOnly = !habilitar;
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