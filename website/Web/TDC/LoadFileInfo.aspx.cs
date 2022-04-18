using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

public partial class TDC_LoadFile : System.Web.UI.Page
{
    Negocio.NEmpleados nEmpleado = new Negocio.NEmpleados();
    Datos.DClientes dClientes = new Datos.DClientes();
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    Boolean validaArchivo(ref string narchivo)
    {   //el archivo es obligatorio
        if (fupArchivo.HasFile == true)
        {
            if ((fupArchivo.PostedFile != null) && (fupArchivo.PostedFile.ContentLength > 0))
            {
                //SUBE EL ARCHIVO
                string extension = System.IO.Path.GetExtension(this.fupArchivo.PostedFile.FileName);
                narchivo = "Paso2" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Millisecond.ToString() + extension;

                string ruta = Server.MapPath("~\\files\\TDC\\") + narchivo;

                try
                {
                    if ((extension.ToUpper() == ".XLS") || (extension.ToUpper() == ".XLSX"))
                    {
                        fupArchivo.PostedFile.SaveAs(ruta);
                        return ImportarAGrilla(ruta, extension, narchivo);
                    }
                    else
                    {
                        this.ltrMensaje.Text = Messaging.Error("El tipo de archivo que intenta cargar no es valido");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    this.ltrMensaje.Text = Messaging.Error("Ocurrio una falla al cargar el archivo. Intente nuevamente, por favor." + ex.Message);
                    return false;
                }
            }
            else
            {
                this.ltrMensaje.Text = Messaging.Error("Por favor seleccione un archivo a cargar");
                return false;
            }
        }
        else
            return true;
    }

    bool ImportarAGrilla(string FilePath, string Extension, string nombreArchivo)
    {
        if (Session["ID_usuario"] != null)
        {
            #region CONEXION EXCEL

            string conStr = "";
            DataGrid dgFaber = new DataGrid();

            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, "YES");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            //Bind Data to GridView
            dgFaber.Caption = Path.GetFileName(FilePath);
            dgFaber.DataSource = dt;
            dgFaber.DataBind();
            #endregion

            Negocio.NTDC nTDC = new Negocio.NTDC();
            

            string afi_documento = "";
            string errores = "", errorLinea = "";

            try
            {
                #region VALIDAR EXTRUCTURA
                DataTable tbValidarReferencia;
                foreach (DataGridItem dgl in dgFaber.Items)
                {
                    errorLinea = "";
                    if (dgl.Cells.Count == 11)
                    {
                        afi_documento = dgl.Cells[6].Text;

                        if ((afi_documento != "&nbsp;") && (afi_documento != ""))
                        {
                            try
                            {
                                #region VALIDAR CAMPOS
                                if (Negocio.NUtilidades.IsNumeric(dgl.Cells[0].Text) == false)
                                    errorLinea += "El número de colocación no es válido";
                                if (Negocio.NUtilidades.IsDouble(dgl.Cells[1].Text) == false)
                                    errorLinea += "El número de tarjeta no es válido";
                                if (Negocio.NUtilidades.IsNumeric(afi_documento) == false)
                                    errorLinea += "El número de documento no es válido";
                                if (dgl.Cells[3].Text != "&nbsp;" & Negocio.NUtilidades.IsDate(dgl.Cells[3].Text) == false)
                                    errorLinea += "La FechaRealce no tiene el formato de fecha requerido";
                                if (dgl.Cells[4].Text != "&nbsp;" & Negocio.NUtilidades.IsDate(dgl.Cells[4].Text) == false)
                                    errorLinea += "La FechaActivacion no tiene el formato de fecha requerido";
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                Response.Write("226 - " + ex.Message);
                                throw;
                            }
                        }
                    }
                    else
                        errorLinea += "El número de columnas no es válido. Se necesitan 11 y el registro tiene " + dgl.Cells.Count + ". ";

                    if (errorLinea != "")
                        errores += "Fila " + (dgl.ItemIndex + 2).ToString() + ", documento " + afi_documento + ": " + errorLinea + "<br />";
                }
                #endregion

                if ((errores != ""))
                {
                    ltrMensaje.Text = Messaging.Error("Hay errores en el archivo") + errores + "<br />";
                    dgFaber.DataSource = null;
                    dgFaber.DataBind();
                    return false;
                }
                else
                {
                    #region INSERTAR
                    int cargados = 0;
                    string fallas = "";
                    foreach (DataGridItem dgl in dgFaber.Items)
                    {
                        try
                        {
                            afi_documento = dgl.Cells[6].Text;
                            if ((afi_documento != "&nbsp;") && (afi_documento != ""))
                            {
                                DataTable tbInserta = nTDC.InsertarInfo(dgl.Cells[6].Text, dgl.Cells[0].Text, dgl.Cells[1].Text, dgl.Cells[2].Text, dgl.Cells[3].Text, dgl.Cells[4].Text, dgl.Cells[5].Text, 
                                                  dgl.Cells[7].Text, dgl.Cells[8].Text, dgl.Cells[9].Text, dgl.Cells[10].Text, Session["ID_usuario"].ToString());
                                if ((tbInserta.Rows.Count > 0) && (tbInserta.Rows[0]["Respuesta"].ToString() == "200"))
                                    cargados++;
                                else
                                    fallas += afi_documento + " : " + tbInserta.Rows[0]["Mensaje"].ToString() +  "<br />";
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write("226 " + ex.Message);
                            throw;
                        }
                    }

                    #endregion
                    if (fallas.Length > 0)
                        fallas = "<br />Se presentaron fallas en los siguientes registros:<br />" + fallas;
                    ltrMensaje.Text = Messaging.Success((cargados).ToString() + " registros cargados" + fallas);
                    dgFaber.DataSource = null;
                    dgFaber.DataBind();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Response.Write("300 " + ex.Message);
                throw;
            }

        }
        else
            return false;
    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        if (Session["ID_usuario"] != null)
        {
            string nombreArchivo = "";
            validaArchivo(ref nombreArchivo);
        }
    }
}