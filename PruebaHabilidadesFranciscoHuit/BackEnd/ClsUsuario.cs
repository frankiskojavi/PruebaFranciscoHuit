using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PruebaHabilidadesFranciscoHuit.BackEnd
{
    public class ClsUsuario
    {
        ClsConexionBD conexion = ClsConexionBD.getInstancia();
        private static ClsUsuario instancia;

        public static ClsUsuario getInstancia()
        {
            if (instancia == null)
            {
                instancia = new ClsUsuario();
            }
            return instancia;
        }
        public ClsUsuario()
        {
            conexion.abrirConexion();
        }
        public Boolean ConsultarCredencialesUsuario(string nombreUsuario, string passwordUsuario)
        {
            DataTable tblConsultaBancos = new DataTable();
            tblConsultaBancos = conexion.hacerConsulta("select * from PHF_Usuario where Login = '" + nombreUsuario + "' and Password = '" + passwordUsuario + "' and EstadoUsuario = 1");
            // Si el usuario existe, graba toda la informacion del usuario en variables de sesion
            if (tblConsultaBancos.Rows.Count > 0)
            {
                HttpContext.Current.Session["CodigoUsuario"] = tblConsultaBancos.Rows[0]["CodigoUsuario"].ToString();
                HttpContext.Current.Session["NombreUsuario"] = tblConsultaBancos.Rows[0]["Nombres"].ToString() + " " + tblConsultaBancos.Rows[0]["Apellidos"].ToString();                
                HttpContext.Current.Session["Email"] = tblConsultaBancos.Rows[0]["Email"].ToString();
                HttpContext.Current.Session["Login"] = tblConsultaBancos.Rows[0]["Login"].ToString();
                HttpContext.Current.Session["Password"] = tblConsultaBancos.Rows[0]["Password"].ToString();
                HttpContext.Current.Session["CodigoRol"] = tblConsultaBancos.Rows[0]["CodigoRol"].ToString();
                HttpContext.Current.Session["EstadoUsuario"] = tblConsultaBancos.Rows[0]["EstadoUsuario"].ToString();
                HttpContext.Current.Session["FechaUltimoAcceso"] = tblConsultaBancos.Rows[0]["FechaUltimoAcceso"].ToString();
                return true;
            }
            return false;

        }
        public void ActualizarFecha(String Codigo)
        {
            conexion.ejecutarQuery("UPDATE PHF_Usuario SET FechaUltimoAcceso = GETDATE() WHERE CodigoUsuario = '" + Codigo + "'");
        }
        public void IngresarUsuario(String nombres, String apellidos, String email, String login, String password, int codigoRol)
        {
            try
            {
                conexion.ejecutarQuery(" INSERT INTO PHF_Usuario (Nombres, Apellidos, Email, Login, Password, CodigoRol, EstadoUsuario)  " +
                                       " VALUES ('" +nombres+"','"+apellidos+"','"+email+"','"+login+"','"+password+"',"+codigoRol+",1)");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void modificarUsuario(int codigoUsuario, String nombres, String apellidos, String email, String login, String password, int codigoRol)
        {            
            conexion.ejecutarQuery("UPDATE PHF_Usuario Set Nombres = '"+nombres+"', Apellidos = '"+apellidos+"', Email = '"+email+"', Login = '"+login+"', Password= '"+password+"', CodigoRol = "+codigoRol+" WHERE CodigoUsuario = "+codigoUsuario);
        }

        public DataTable consultarRol()
        {
            return conexion.hacerConsulta(" SELECT CodigoRol, DescripcionRol FROM PHF_Rol");
        }
        public DataTable ConsultarUsuariosSistema()
        {
            return conexion.hacerConsulta(" select CodigoUsuario, Nombres + ' ' + Apellidos as NombreCompleto, Email, Login, b.DescripcionRol, EstadoUsuario = case when EstadoUsuario = 1 then 'Activo' ELSE 'Inactivo' END   " + Environment.NewLine +
                                          " FROM PHF_Usuario a " + Environment.NewLine +
                                          " INNER JOIN PHF_Rol b on a.CodigoRol = b.CodigoRol "+
                                          " WHERE EstadoUsuario = 1");
        }
        public DataTable ConsultarUsuarioEspecifico(int codigoUsuario)
        {
            return conexion.hacerConsulta(" select * from PHF_Usuario  where CodigoUsuario = " + codigoUsuario);
        }
        public void eliminarRegistro(int CodigoUsuario) 
        {
            try
            {
                conexion.ejecutarQuery(" UPDATE PHF_Usuario SET EstadoUsuario = 0 WHERE CodigoUsuario = " + CodigoUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}