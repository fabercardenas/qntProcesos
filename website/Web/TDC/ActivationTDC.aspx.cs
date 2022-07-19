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

public partial class TDC_ActivationTDC : System.Web.UI.Page
{
    Negocio.NEmpleados nEmpleado = new Negocio.NEmpleados();
    Datos.DClientes dClientes = new Datos.DClientes();
    Negocio.NTDC nTDC = new Negocio.NTDC();

    protected void Page_Load(object sender, EventArgs e)
    {
        Literal ltrTituloModulo = (Literal)this.Master.FindControl("ltrTituloModulo");
        ltrTituloModulo.Text = "<span class='fa fa-credit-card'></span> <b>Carga de Archivo para Activación TDC - Paso 9</b>";
    }

    Boolean validaArchivo(ref string narchivo)
    {   //el archivo es obligatorio
        if (fupArchivo.HasFile == true)
        {
            if ((fupArchivo.PostedFile != null) && (fupArchivo.PostedFile.ContentLength > 0))
            {
                //SUBE EL ARCHIVO
                string extension = System.IO.Path.GetExtension(this.fupArchivo.PostedFile.FileName);
                narchivo = "Paso9" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Millisecond.ToString() + extension;

                string ruta = Server.MapPath("~\\files\\TDC\\") + narchivo;

                try
                {
                    if ((extension.ToUpper() == ".XLS") || (extension.ToUpper() == ".XLSX"))
                    {
                        fupArchivo.PostedFile.SaveAs(ruta);
                        ltrNombreArchivo.Text = narchivo;
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

            string cli_tarjeta = "";
            string errores = "", errorLinea = "";

            try
            {
                #region VALIDAR EXTRUCTURA
                DataTable tbValidarSolicitud;
                foreach (DataGridItem dgl in dgFaber.Items)
                {
                    errorLinea = "";
                    if (dgl.Cells.Count == 2)
                    {
                        cli_tarjeta = dgl.Cells[0].Text;

                        if ((cli_tarjeta != "&nbsp;") && (cli_tarjeta != ""))
                        {
                            try
                            {
                                #region VALIDAR CAMPOS
                                if (Negocio.NUtilidades.IsDouble(cli_tarjeta) == false)
                                    errorLinea += "El número de documento no es válido. ";
                                if (Negocio.NUtilidades.IsDate(dgl.Cells[1].Text) == false)
                                    errorLinea += "La Fecha de Activación no es válida. ";
                                if ((dgl.Cells[1].ToString() == "&nbsp;") && (dgl.Cells[1].ToString() == ""))
                                    errorLinea += "La Fecha de Activación es requerida. ";
                                //VALIDAR QUE ESTÉ EN EL PASO 8 PARA ACTIVACIÓN
                                tbValidarSolicitud = nTDC.ConsultarXtarjetaActiva(cli_tarjeta);
                                if (tbValidarSolicitud.Rows.Count > 0)
                                    errorLinea += "Esta tarjeta no puede ser activada porque se encuentra en el paso " + tbValidarSolicitud.Rows[0]["tdc_paso"].ToString() + ". ";
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
                        errorLinea += "El número de columnas no es válido. Se necesitan 2 y el registro tiene " + dgl.Cells.Count + ". ";

                    if (errorLinea != "")
                        errores += "Fila " + (dgl.ItemIndex + 2).ToString() + ", Tarjeta " + cli_tarjeta + ": " + errorLinea + "<br />";
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
                    string documentosSF = "";
                    foreach (DataGridItem dgl in dgFaber.Items)
                    {
                        try
                        {
                            cli_tarjeta = dgl.Cells[0].Text;                            
                            if ((cli_tarjeta != "&nbsp;") && (cli_tarjeta != ""))
                            {
                                nTDC.ActualizarInfoActivacion(cli_tarjeta, dgl.Cells[1].Text, Session["ID_usuario"].ToString(), nombreArchivo);
                                documentosSF += "\"" + cli_tarjeta + "\",";
                                cargados++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write("226 " + ex.Message);
                            throw;
                        }
                    }
                    #endregion

                    ltrMensaje.Text = Messaging.Success ((cargados).ToString() + " registros cargados. Decargue el archivo para Validación de las tarjetas Activadas");
                    dgFaber.DataSource = null;
                    dgFaber.DataBind();
                    dvDescarga.Visible = true;
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
            ltrNombreArchivo.Text = "";
            validaArchivo(ref nombreArchivo);
        }
    }

    protected void lnbDescargar_Click(object sender, EventArgs e)
    {
        if (Session["ID_usuario"] != null)
        {
            #region GENERACIÓN FORMULARIO DE SALIDA
            consultaSolicitudXArchivo(nTDC.consultaSolicitudXArchivoActi(ltrNombreArchivo.Text));
            #endregion
        }
    }

    protected void consultaSolicitudXArchivo(DataTable tbl)
    {
        if (tbl.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page page = new Page();
            HtmlForm form = new HtmlForm();

            GridView gdvDatos = new GridView();
            gdvDatos.DataSource = tbl;
            gdvDatos.DataBind();

            page.EnableEventValidation = false;
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(gdvDatos);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename= TarejtasActivadasTDC" + string.Format("{0:ddMMyyyy}", DateTime.Today) + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
    }
}