using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PruebaHabilidadesFranciscoHuit.BackEnd
{
    public class ClsProducto
    {
        ClsConexionBD conexion = ClsConexionBD.getInstancia();
        private static ClsProducto instancia;

        public static ClsProducto getInstancia()
        {
            if (instancia == null)
            {
                instancia = new ClsProducto();
            }
            return instancia;
        }
        public ClsProducto()
        {            
        }        
        public void ingresarproducto(int codigoProveedor, String NombreProducto, String DescripcionProducto, int existencia, String Precio, String Descuento, Byte[] arregloBytes)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StringConexionPrincipal"].ToString());
            con.Open();          
            try
            {                
                    SqlCommand com = new SqlCommand("INSERT INTO PHF_Producto (CodigoProveedor, NombreProducto, DescripcionProducto, Existencia, Precio, Descuento, Imagen, EstadoProducto) " +
                                "VALUES (@CodigoProveedor, @NombreProducto, @DescripcionProducto, @Existencia, @Precio, @Descuento, @Imagen, 1)", con);
                    com.Parameters.AddWithValue("@CodigoProveedor", codigoProveedor);
                    com.Parameters.AddWithValue("@NombreProducto", NombreProducto);
                    com.Parameters.AddWithValue("@DescripcionProducto", DescripcionProducto);
                    com.Parameters.AddWithValue("@Existencia", existencia);
                    com.Parameters.AddWithValue("@Precio", Precio);
                    com.Parameters.AddWithValue("@Descuento", Descuento);
                    com.Parameters.AddWithValue("@Imagen", arregloBytes);
                    com.ExecuteNonQuery();                
            }
            catch (SqlException ex)
            {
            }

        }
        public void ingresarproducto(int codigoProveedor, String NombreProducto, String DescripcionProducto, int existencia, String Precio, String Descuento)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StringConexionPrincipal"].ToString());
            con.Open();
            try
            {                
                SqlCommand com = new SqlCommand("INSERT INTO PHF_Producto (CodigoProveedor, NombreProducto, DescripcionProducto, Existencia, Precio, Descuento, EstadoProducto) " +
                            "VALUES (@CodigoProveedor, @NombreProducto, @DescripcionProducto, @Existencia, @Precio, @Descuento, 1)", con);
                com.Parameters.AddWithValue("@CodigoProveedor", codigoProveedor);
                com.Parameters.AddWithValue("@NombreProducto", NombreProducto);
                com.Parameters.AddWithValue("@DescripcionProducto", DescripcionProducto);
                com.Parameters.AddWithValue("@Existencia", existencia);
                com.Parameters.AddWithValue("@Precio", Precio);
                com.Parameters.AddWithValue("@Descuento", Descuento);
                com.ExecuteNonQuery();                
            }
            catch (SqlException ex)
            {
            }

        }
        public void modificarproducto(int codigoproducto, int codigoProveedor, String NombreProducto, String DescripcionProducto, int existencia, String Precio, String Descuento)
        {            
            conexion.ejecutarQuery("UPDATE PHF_producto Set codigoProveedor = " + codigoProveedor+", NombreProducto = '"+NombreProducto+"',  DescripcionProducto = '"+DescripcionProducto+"', Existencia = "+existencia+", Precio = "+Precio+", Descuento = "+Descuento+"  WHERE Codigoproducto = "+ codigoproducto);
        }
        public DataTable consultarproductoesSistema()
        {
            return conexion.hacerConsulta(" SELECT a.*, b.DescripcionProveedor FROM PHF_producto a inner join PHF_Proveedor b on a.CodigoProveedor = b.CodigoProveedor WHERE a.Estadoproducto = 1");
        }
        public DataTable consultarproductoEspecifico(int codigoproducto)
        {
            return conexion.hacerConsulta(" SELECT * FROM PHF_producto  WHERE Codigoproducto = " + codigoproducto);
        }
        public void eliminarRegistro(int codigoproducto) 
        {
            try
            {
                conexion.ejecutarQuery(" UPDATE PHF_producto SET Estadoproducto = 0 WHERE Codigoproducto = " + codigoproducto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}