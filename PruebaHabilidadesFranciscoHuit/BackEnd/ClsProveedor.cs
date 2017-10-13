using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PruebaHabilidadesFranciscoHuit.BackEnd
{
    public class ClsProveedor
    {
        ClsConexionBD conexion = ClsConexionBD.getInstancia();
        private static ClsProveedor instancia;

        public static ClsProveedor getInstancia()
        {
            if (instancia == null)
            {
                instancia = new ClsProveedor();
            }
            return instancia;
        }
        public ClsProveedor()
        {            
        }        
        public void ingresarProveedor(String descripcionProveedor, String nombreEmpresa, String encargado, String telefono, String email, String direccion)
        {
            try
            {
                conexion.ejecutarQuery(" INSERT INTO PHF_Proveedor (DescripcionProveedor, NombreEmpresa, Encargado, Telefono, Email, Direccion, EstadoProveedor) " +
                                       " VALUES ('"+descripcionProveedor + "','"+nombreEmpresa+"','"+encargado+"','"+telefono+"','"+email+"','"+direccion+"', 1)");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void modificarProveedor(int codigoProveedor, String descripcionProveedor, String nombreEmpresa, String encargado, String telefono, String email, String direccion)
        {            
            conexion.ejecutarQuery("UPDATE PHF_Proveedor Set DescripcionProveedor = '" +descripcionProveedor +"', NombreEmpresa = '" + nombreEmpresa +"', Encargado = '" + encargado + "', telefono = '"+telefono+"', Email = '" + email+"', Direccion = '"+direccion+"'  WHERE CodigoProveedor = "+ codigoProveedor);
        }
        public DataTable consultarProveedoresSistema()
        {
            return conexion.hacerConsulta(" SELECT * FROM PHF_Proveedor WHERE EstadoProveedor = 1");
        }
        public DataTable consultarProveedorEspecifico(int codigoProveedor)
        {
            return conexion.hacerConsulta(" SELECT * FROM PHF_Proveedor  WHERE CodigoProveedor = " + codigoProveedor);
        }
        public void eliminarRegistro(int codigoProveedor) 
        {
            try
            {
                conexion.ejecutarQuery(" UPDATE PHF_Proveedor SET EstadoProveedor = 0 WHERE CodigoProveedor = " + codigoProveedor);
                conexion.ejecutarQuery(" UPDATE PHF_Producto SET EstadoProducto = 0 WHERE CodigoProveedor = " + codigoProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}