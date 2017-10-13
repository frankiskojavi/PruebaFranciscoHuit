using PruebaHabilidadesFranciscoHuit.BackEnd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaHabilidadesFranciscoHuit.FrontEnd
{
    public partial class TestDelete : System.Web.UI.Page
    {
        ClsConexionBD conectar = ClsConexionBD.getInstancia();
        protected void Page_Load(object sender, EventArgs e)
        {            
                string strQuery = "select Name, ContentType, Imagen from PHF_TestImage where Name= 'pruebaJPG.jpg'";
                SqlCommand cmd = new SqlCommand(strQuery);                
                DataTable dt = conectar.hacerConsulta(strQuery);
                if (dt != null)
                {
                    Byte[] bytes = (Byte[])dt.Rows[0]["Imagen"];
                    string base64String = Convert.ToBase64String(bytes);
                    imagenPrueba.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String);
                }            
        }
        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = ConfigurationManager.ConnectionStrings["StringConexionPrincipal"].ToString();
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int length = FileUpload1.PostedFile.ContentLength;
            byte[] picSize = new byte[length];
            HttpPostedFile uplImage = FileUpload1.PostedFile;
            uplImage.InputStream.Read(picSize, 0, length);

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StringConexionPrincipal"].ToString()))
            {
                con.Open();
                SqlCommand com = new SqlCommand("INSERT INTO PHF_TestImage (imagen) values (@Picture)", con);
                try
                {                    
                    com.Parameters.AddWithValue("@Picture", picSize);
                    com.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {                    
                }
            }
        }
    }
}