using PruebaHabilidadesFranciscoHuit.BackEnd;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaHabilidadesFranciscoHuit.FrontEnd
{
    public partial class Producto : System.Web.UI.Page
    {
        ClsProveedor clProveedor = ClsProveedor.getInstancia();
        ClsProducto clProducto = ClsProducto.getInstancia();
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
                consultarProductos();
                recuperarProveedores();           
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
            recuperarProveedores();
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            // hace todas las validaciones del formulario      
            int codigoProveedor = int.Parse(dpdProveedor.SelectedValue.ToString());
            if (!validacionesFormulario())
            {
                return;
            }
            // Verifica si es Modificacion o Grabar
            if (Boolean.Parse(Session["Modificar"].ToString()))
            {                
                clProducto.modificarproducto(int.Parse(Session["CodigoProdocutoDel"].ToString()), codigoProveedor, txtNombreProducto.Text, txtDescripcionProducto.Text, int.Parse(txtExistencia.Text), txtPrecio.Text, txtDescuento.Text);
            }
            else
            {
                clProducto.ingresarproducto(codigoProveedor, txtNombreProducto.Text, txtDescripcionProducto.Text, int.Parse(txtExistencia.Text), txtPrecio.Text, txtDescuento.Text);
            }

            consultarProductos();
        }
        //GrdProveedores_RowCommand

       

        protected void GrdProducto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            recuperarProveedores();
            try
            {                
                if (e.CommandName.Equals("btnEliminar"))
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    GridViewRow row = GrdProducto.Rows[index];
                    Label lblCodigoProducto = (Label)row.FindControl("lblCodigoProducto");
                    Label lblDescripcionProducto = (Label)row.FindControl("lblDescripcionProducto");
                    int codigoProducto = int.Parse(lblCodigoProducto.Text);
                    registroEliinar = lblDescripcionProducto.Text;
                    Session["CodigoProdocutoDel"] = codigoProducto;

                    String script1 = " $(function () {  $(\"#modalAdvertencia\").modal(\"show\"); });";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "invokeModal", script1, true);
                    updPrincipal.Update();                    
                }

                if (e.CommandName.Equals("btnModificar"))
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    GridViewRow row = GrdProducto.Rows[index];
                    Label lblCodigoProducto = (Label)row.FindControl("lblCodigoProducto");
                    int codigoProducto = int.Parse(lblCodigoProducto.Text);
                    Session["CodigoProdocutoDel"] = codigoProducto;
                    // Recupera los datos del usuario seleccionado y asigna los valores al textbox
                    DataTable dtblInformacionUsuario = clProducto.consultarproductoEspecifico(codigoProducto);
                    foreach (DataRow registro in dtblInformacionUsuario.Rows)
                    {
                        txtNombreProducto.Text = registro["NombreProducto"].ToString();
                        dpdProveedor.Text = registro["CodigoProveedor"].ToString();
                        txtDescripcionProducto.Text = registro["DescripcionProducto"].ToString();
                        txtExistencia.Text = registro["Existencia"].ToString();
                        txtPrecio.Text = registro["Precio"].ToString();
                        txtDescuento.Text = registro["Descuento"].ToString();
                        protegerCampos(false);
                    }
                    mvlPrincipal.ActiveViewIndex = 1;                    
                    Session["Modificar"] = true;
                    protegerCampos(true);
                }
                if (e.CommandName.Equals("btnConsultar"))
                {                    
                    int index = int.Parse(e.CommandArgument.ToString());
                    GridViewRow row = GrdProducto.Rows[index];
                    Label lblCodigoProducto = (Label)row.FindControl("lblCodigoProducto");
                    int codigoProducto = int.Parse(lblCodigoProducto.Text);
                    Session["CodigoProdocutoDel"] = codigoProducto;
                    // Recupera los datos del usuario seleccionado y asigna los valores al textbox
                    DataTable dtblInformacionUsuario = clProducto.consultarproductoEspecifico(codigoProducto);
                    foreach (DataRow registro in dtblInformacionUsuario.Rows)
                    {
                        txtNombreProducto.Text = registro["NombreProducto"].ToString();
                        dpdProveedor.Text = registro["CodigoProveedor"].ToString();
                        txtDescripcionProducto.Text = registro["DescripcionProducto"].ToString();
                        txtExistencia.Text = registro["Existencia"].ToString();
                        txtPrecio.Text = registro["Precio"].ToString();
                        txtDescuento.Text = registro["Descuento"].ToString();
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
            consultarProductos();            
        }

        protected void btnAceptarReg_Click(object sender, EventArgs e)
        {
            try
            {
                clProveedor.eliminarRegistro(int.Parse(Session["CodigoProdocuto"].ToString()));
                Response.Redirect("~/FrontEnd/Productos.aspx", false);
            }
            catch (Exception ex)
            {
                Session["ErrorAPP"] = ex.Message;
                Response.Redirect("~/FrontEnd/Excepciones.aspx", false);
            }

        }
       
        private void consultarProductos()
        {
            // Consulta todos los datos del usuario y los mete al grid
            mvlPrincipal.ActiveViewIndex = 0;
            GrdProducto.DataSource = clProducto.consultarproductoesSistema();
            GrdProducto.DataBind();
            if (GrdProducto.Rows.Count > 0)
            {
                GrdProducto.UseAccessibleHeader = true;
                GrdProducto.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdProducto.FooterRow.TableSection = TableRowSection.TableFooter;
                string script1 = "$('#ContentPlaceHolder1_GrdProducto').DataTable({ \"scrollY\":        \"270px\", \"scrollCollapse\": false, \"paging\":         false   });";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ssss", script1, true);
            }

        }
        private void recuperarProveedores()
        {
            dpdProveedor.DataSource = clProveedor.consultarProveedoresSistema();
            dpdProveedor.DataTextField = "DescripcionProveedor";
            dpdProveedor.DataValueField = "CodigoProveedor";
            dpdProveedor.DataBind();
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
            txtNombreProducto.Text = "";
            txtDescripcionProducto.Text = "";
            txtExistencia.Text = "";
            txtPrecio.Text = "";
            txtDescuento.Text = "";            
        }              
        public Boolean validacionesFormulario()
        {
            if (txtNombreProducto.Text.Equals(""))
            {
                mostrarError("El nombre no puede estar en blanco");
                return false;
            }
            if (txtDescripcionProducto.Text.Equals(""))
            {
                mostrarError("La descripcion no puede estar en blanco");
                return false;
            }
            if (txtExistencia.Text.Equals(""))
            {
                mostrarError("La existencia no puede estar en blanco");
                return false;
            }
            if (txtPrecio.Text.Equals(""))
            {
                mostrarError("El precio no puede estar en blanco");
                return false;
            }
            if (txtDescuento.Text.Equals(""))
            {
                mostrarError("El descuento no puede estar en blanco");
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
            dpdProveedor.Enabled = habilitar;
            txtNombreProducto.ReadOnly = !habilitar;
            txtDescripcionProducto.ReadOnly = !habilitar;
            txtExistencia.ReadOnly = !habilitar;
            txtPrecio.ReadOnly = !habilitar;
            txtDescuento.ReadOnly = !habilitar;            
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