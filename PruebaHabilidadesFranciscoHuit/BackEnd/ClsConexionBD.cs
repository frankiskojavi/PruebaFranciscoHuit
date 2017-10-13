﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime;
using System.Web;

namespace PruebaHabilidadesFranciscoHuit.BackEnd
{
    public class ClsConexionBD
    {
        SqlConnection Comm;
        SqlCommand Comando;
        private static ClsConexionBD instancia;
        public static ClsConexionBD getInstancia()
        {
            if (instancia == null)
            {
                instancia = new ClsConexionBD();
            }
            return instancia;
        }

        public void abrirConexion()
        {
            try
            {
                Comm.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(" Ocurrio un error al intentar abrir la conexion " + ex.Message);
            }

        }
        public void cerrarConexion()
        {
            try
            {
                Comm.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public ClsConexionBD()
        {
            Comm = new SqlConnection(ConfigurationManager.ConnectionStrings["StringConexionPrincipal"].ToString());  
            Comando = new SqlCommand();
        }
        public DataTable hacerConsulta(String query)
        {
            try
            {
                Comando.CommandText = query;
                Comando.Connection = Comm;
                SqlDataAdapter Adaper = new SqlDataAdapter(Comando);
                DataSet Datos = new DataSet();
                Adaper.Fill(Datos);
                return Datos.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ejecutarQuery(String query)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand(query, Comm);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
