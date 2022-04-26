using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using OfficeOpenXml;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

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
            

            //string cli_nombre = ""; Modificación 26 de Abril
            string cli_documento = "";
            string errores = "", errorLinea = "";

            try
            {
                #region VALIDAR EXTRUCTURA
                foreach (DataGridItem dgl in dgFaber.Items)
                {
                    errorLinea = "";
                    if (dgl.Cells.Count == 14)
                    {
                        //cli_nombre = dgl.Cells[9].Text;
                        //if ((cli_nombre != "&nbsp;") && (cli_nombre != ""))
                        cli_documento = dgl.Cells[13].Text;
                        if ((cli_documento != "&nbsp;") && (cli_documento != ""))
                        {
                            try
                            {
                                #region VALIDAR CAMPOS
                                //if (Negocio.NUtilidades.IsNumeric(dgl.Cells[13].Text) == false)
                                //    errorLinea += "El número de documento no es válido. ";
                                if (Negocio.NUtilidades.IsNumeric(dgl.Cells[0].Text) == false)
                                    errorLinea += "El número de colocación no es válido. ";
                                if (dgl.Cells[3].Text != "&nbsp;" & Negocio.NUtilidades.IsDate(dgl.Cells[3].Text) == false)
                                    errorLinea += "La FechaRealce no tiene el formato de fecha requerido. ";
                                if (dgl.Cells[4].Text != "&nbsp;" & Negocio.NUtilidades.IsDate(dgl.Cells[4].Text) == false)
                                    errorLinea += "La FechaActivacion no tiene el formato de fecha requerido. ";
                                if (Negocio.NUtilidades.IsNumeric(dgl.Cells[8].Text) == false)
                                    errorLinea += "El número de contrato no es válido. ";
                                if (Negocio.NUtilidades.IsNumeric(dgl.Cells[12].Text) == false)
                                    errorLinea += "El número de tarjeta de crédito no es válido. ";
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
                        errorLinea += "El número de columnas no es válido. Se necesitan 14 y el registro tiene " + dgl.Cells.Count + ". ";

                    if (errorLinea != "")
                        errores += "Fila " + (dgl.ItemIndex + 2).ToString() + ", documento " + cli_documento + ": " + errorLinea + "<br />";
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
                    DataTable tbExcelResult = new DataTable();
                    tbExcelResult.Columns.Add("IdColocacion");
                    tbExcelResult.Columns.Add("NumeroTarjeta", Type.GetType("System.Int64"));
                    tbExcelResult.Columns.Add("TipoProducto");
                    tbExcelResult.Columns.Add("FechaRealce");
                    tbExcelResult.Columns.Add("FechaActivacion");
                    tbExcelResult.Columns.Add("MontoAprobado");
                    tbExcelResult.Columns.Add("NumeroIndentificacion");
                    tbExcelResult.Columns.Add("EstadoTarjeta");
                    tbExcelResult.Columns.Add("Contrato", Type.GetType("System.Int64"));
                    tbExcelResult.Columns.Add("Nombre");
                    tbExcelResult.Columns.Add("CopiadeNumeroTarjeta");
                    tbExcelResult.Columns.Add("ID_BMP");
                    tbExcelResult.Columns.Add("no_TarjetaDeCredito", Type.GetType("System.Int64"));
                    tbExcelResult.Columns.Add("DocumentoCliente");

                    GridView gdvDatos = new GridView();
                    
                    foreach (DataGridItem dgl in dgFaber.Items)
                    {
                        try
                        {
                            //cli_nombre = dgl.Cells[9].Text;
                            //if ((cli_nombre != "&nbsp;") && (cli_nombre != ""))
                            cli_documento = dgl.Cells[13].Text;
                            if ((cli_documento != "&nbsp;") && (cli_documento != ""))
                            {
                                DataTable tbInserta = nTDC.InsertarInfo(dgl.Cells[0].Text, dgl.Cells[3].Text, 
                                                  dgl.Cells[7].Text, dgl.Cells[8].Text, dgl.Cells[9].Text, Session["ID_usuario"].ToString(), nombreArchivo, dgl.Cells[12].Text, dgl.Cells[13].Text);
                                if ((tbInserta.Rows.Count > 0) && (tbInserta.Rows[0]["Respuesta"].ToString() == "200"))
                                {
                                    cargados++;
                                    DataRow filaExel = tbExcelResult.NewRow();
                                    filaExel["IdColocacion"] = dgl.Cells[0].Text;
                                    filaExel["NumeroTarjeta"] = dgl.Cells[1].Text;
                                    filaExel["TipoProducto"] = dgl.Cells[2].Text;
                                    filaExel["FechaRealce"] = dgl.Cells[3].Text;
                                    filaExel["FechaActivacion"] = "";
                                    filaExel["MontoAprobado"] = dgl.Cells[5].Text;
                                    filaExel["NumeroIndentificacion"] = dgl.Cells[6].Text;
                                    filaExel["EstadoTarjeta"] = dgl.Cells[7].Text;
                                    filaExel["Contrato"] = dgl.Cells[8].Text;
                                    filaExel["Nombre"] = dgl.Cells[9].Text;
                                    filaExel["CopiadeNumeroTarjeta"] = dgl.Cells[10].Text;
                                    filaExel["ID_BMP"] = "OK";
                                    filaExel["no_TarjetaDeCredito"] = dgl.Cells[12].Text;
                                    filaExel["DocumentoCliente"] = dgl.Cells[13].Text; ;
                                    tbExcelResult.Rows.Add(filaExel);
                                }
                                else
                                    fallas += cli_documento + " : " + tbInserta.Rows[0]["Mensaje"].ToString() +  "<br />";
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write("226 " + ex.Message);
                            throw;
                        }
                    }
                    gdvDatos.DataSource = tbExcelResult;
                    gdvDatos.DataBind();
                    #endregion

                    #region GENERACIÓN ARCHIVO DE SALIDA
                    if (gdvDatos.Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        StringWriter sw = new StringWriter(sb);
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        Page page = new Page();
                        HtmlForm form = new HtmlForm();
                        
                        page.EnableEventValidation = false;
                        page.DesignerInitialize();
                        page.Controls.Add(form);
                        form.Controls.Add(gdvDatos);
                        page.RenderControl(htw);

                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("Content-Disposition", "attachment; filename= ResultadoPaso2TDC" + string.Format("{0:ddMMyyyy}", DateTime.Today) + ".xls");
                        Response.Charset = "UTF-8";
                        Response.ContentEncoding = Encoding.Default;
                        Response.Write(sb.ToString());
                        Response.End();
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

    protected void lnbCargar_Click(object sender, EventArgs e)
    {
        if (Session["ID_usuario"] != null)
        {
            string nombreArchivo = "";
            validaArchivo(ref nombreArchivo);
        }
    }
}