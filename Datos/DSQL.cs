using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient ;
using System.Configuration;

namespace Datos
{
    public abstract class DSQL
    {

     #region CONSULTA Y DEVUELVE TABLE

        public DataTable procedureTable(string procedimiento, Boolean tieneParametros, Hashtable parametros = null, string key = "generalConnection")
        {
            using (SqlConnection conexion = new SqlConnection())
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings[key].ConnectionString;
                using (SqlCommand storedProcCommand = new SqlCommand(procedimiento, conexion))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(storedProcCommand))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            storedProcCommand.CommandType = CommandType.StoredProcedure;

                            if (tieneParametros == true)
                            {
                                foreach (DictionaryEntry parametro in parametros)
                                {
                                    storedProcCommand.Parameters.AddWithValue(parametro.Key.ToString(), parametro.Value);
                                }
                            }
                            storedProcCommand.CommandTimeout = 360000;
                            try
                            {
                                conexion.Open();
                                sda.Fill(dt);
                            }
                            catch (Exception error)
                            {
                                conexion.Close();
                                queryTable("INSERT INTO px_fallas(fa_modulo, fa_descripcion, fa_usuario) VALUES ('" + procedimiento + "','" + error.Message.Replace("'", "") + "','PROCEDURE')");
                                throw;
                            }
                            finally
                            {
                                conexion.Close();
                            }
                            conexion.Dispose();
                            return dt;
                        }
                    }
                }

            }

            //SqlCommand storedProcCommand = new SqlCommand(procedimiento, conexion);
            //SqlDataAdapter sda = new SqlDataAdapter(storedProcCommand);
            //DataTable dt= new DataTable();
            //storedProcCommand.CommandType = CommandType.StoredProcedure;

            //if (tieneParametros==true)
            //{
            //    foreach (DictionaryEntry parametro in parametros)
            //    {
            //        storedProcCommand.Parameters.AddWithValue(parametro.Key.ToString()  , parametro.Value);
            //    }
            //}
            //storedProcCommand.CommandTimeout = 360000;
            //try
            //{
            //    conexion.Open();
            //    sda.Fill(dt);
            //}
            //catch (Exception error)
            //{
            //    conexion.Close();
            //    queryTable("INSERT INTO px_fallas(fa_modulo, fa_descripcion, fa_usuario) VALUES ('" + procedimiento + "','" + error.Message.Replace("'", "") + "','PROCEDURE')");
            //    throw;
            //}
            //finally
            //{
            //    conexion.Close();
            //}
            //return dt;
        }

        public DataSet procedureDataSet(string procedimiento, Boolean tieneParametros, Hashtable parametros = null, string key = "generalConnection")
        {
            using (SqlConnection conexion = new SqlConnection())
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings[key].ConnectionString;
                SqlCommand storedProcCommand = new SqlCommand(procedimiento, conexion);
                SqlDataAdapter sda = new SqlDataAdapter(storedProcCommand);
                DataSet ds = new DataSet();
                storedProcCommand.CommandType = CommandType.StoredProcedure;

                if (tieneParametros == true)
                {
                    foreach (DictionaryEntry parametro in parametros)
                    {
                        storedProcCommand.Parameters.AddWithValue(parametro.Key.ToString(), parametro.Value);
                    }
                }
                storedProcCommand.CommandTimeout = 360000;
                try
                {
                    conexion.Open();
                    sda.Fill(ds);
                }
                catch (Exception error)
                {
                    conexion.Close();
                    queryTable("INSERT INTO px_fallas(fa_modulo, fa_descripcion, fa_usuario) VALUES ('" + procedimiento + "','" + error.Message.Replace("'", "") + "','PROCEDURE')");
                    throw;
                }
                finally
                {
                    conexion.Close();
                }
                conexion.Dispose();
                return ds;
            }
        }

        public DataTable queryTable(string query, string key = "generalConnection")
        {
            using (SqlConnection conexion = new SqlConnection())
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings[key].ConnectionString;
                using (SqlCommand queryCommand = new SqlCommand(query, conexion))
                { 
                    SqlDataAdapter sda = new SqlDataAdapter(queryCommand);
                    DataTable dt = new DataTable();
                    queryCommand.CommandType = CommandType.Text;
                    queryCommand.CommandTimeout = 360000;
                    conexion.Open();
                    sda.Fill(dt);
                    conexion.Close();
                    conexion.Dispose();
                return dt;
                }
            }
            
        }

        public DataTable SelectFromDataTable(DataTable dt, string filter, string sort="")
        {
            DataRow[] rows;
            DataTable dtNew;
            //copy table structure
            dtNew = dt.Clone();

            //sort and filter data
            rows = dt.Select(filter, sort);

            //fill dtNew with selected rows
            foreach (DataRow dr in rows)
            {
                dtNew.ImportRow(dr);
            }
            //return filtered dt
            return dtNew;
        }
        #endregion

    }

}
 