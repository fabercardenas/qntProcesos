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

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;



public partial class TDC_LoadFile : System.Web.UI.Page
{
    Negocio.NEmpleados nEmpleado = new Negocio.NEmpleados();
    Datos.DClientes dClientes = new Datos.DClientes();
    Negocio.NTDC nTDC = new Negocio.NTDC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"] != null) && (!Page.IsPostBack))
        {
            Literal ltrTituloModulo = (Literal)this.Master.FindControl("ltrTituloModulo");
            ltrTituloModulo.Text = "<span class='fa fa-codepen'></span> <b>Carga de Tarjetas Aprobadas - Paso 1</b>";
        }
    }

    Boolean validaArchivo(ref string narchivo)
    {   //el archivo es obligatorio
        if (fupArchivo.HasFile == true)
        {
            if ((fupArchivo.PostedFile != null) && (fupArchivo.PostedFile.ContentLength > 0))
            {
                //SUBE EL ARCHIVO
                string extension = System.IO.Path.GetExtension(this.fupArchivo.PostedFile.FileName);
                narchivo = "Paso1" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Millisecond.ToString() + extension;

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

            string afi_documento = "";
            string errores = "", errorLinea = "";

            try
            {
                #region VALIDAR EXTRUCTURA
                DataTable tbValidarReferencia;
                DataTable tbValidarSolicitud;
                foreach (DataGridItem dgl in dgFaber.Items)
                {
                    errorLinea = "";
                    if (dgl.Cells.Count == 5)
                    {
                        afi_documento = dgl.Cells[1].Text;

                        if ((afi_documento != "&nbsp;") && (afi_documento != ""))
                        {
                            try
                            {
                                #region VALIDAR CAMPOS
                                if ((dgl.Cells[0].Text != "1") && (dgl.Cells[0].Text != "2"))
                                    errorLinea += "Tipo de documento no válido. ";
                                if (Negocio.NUtilidades.IsDouble(afi_documento) == false)
                                    errorLinea += "El número de documento no es válido. ";
                                //VALIDAR QUE NO EXISTA UNA SOLICITUD PARA EL DOCUMENTO
                                tbValidarSolicitud = nTDC.consultaSolicitudXDocumento(afi_documento);
                                if (tbValidarSolicitud.Rows.Count > 0)
                                    errorLinea += "Ya existe una solicitud para este documento y se encuentra en el paso " + tbValidarSolicitud.Rows[0]["tdc_paso"].ToString() + ". ";
                                    //esto se puede permitir si el cliente ha finalizado el proceso, por ahora no se ha definido cual es el paso final
                                tbValidarReferencia = Negocio.NUtilidades.consultaReferenciaXModulo_y_Valor("Canales", dgl.Cells[2].Text);
                                if(tbValidarReferencia.Rows.Count==0)
                                    errorLinea += "Canal no válido. ";
                                tbValidarReferencia = Negocio.NUtilidades.consultaReferenciaXModulo_y_Valor("Procesos", dgl.Cells[3].Text);
                                if (tbValidarReferencia.Rows.Count == 0)
                                    errorLinea += "Proceso no válido. ";
                                if (Negocio.NUtilidades.IsDate(dgl.Cells[4].Text) == false)
                                    errorLinea += "la Fecha de Venta no es válida. ";
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
                        errorLinea += "El número de columnas no es válido. Se necesitan 5 y el registro tiene " + dgl.Cells.Count + ". ";

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
                    string documentosSF = "";
                    foreach (DataGridItem dgl in dgFaber.Items)
                    {
                        try
                        {
                            afi_documento = dgl.Cells[1].Text;                            
                            if ((afi_documento != "&nbsp;") && (afi_documento != ""))
                            {
                                nTDC.Insertar(dgl.Cells[0].Text, afi_documento, dgl.Cells[2].Text, dgl.Cells[3].Text, Session["ID_usuario"].ToString(), nombreArchivo, dgl.Cells[4].Text);
                                documentosSF += "\"" + afi_documento + "\",";
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

                    #region SINCRONIZAR SALESFORCE
                    if (documentosSF.Length > 0)
                    {
                        nTDC.SincronizarNombres(documentosSF.Substring(0, documentosSF.Length - 1));
                    }
                    #endregion

                    ltrMensaje.Text = Messaging.Success ((cargados).ToString() + " registros cargados. Decargue el archivo de Solicitud de Emisión TDC para Finandina");
                    dgFaber.DataSource = null;
                    dgFaber.DataBind();
                    dvDescarga.Visible = true;
                    lnbDescargar.Visible = true;
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
            lnbDescargar.Visible = false;
            validaArchivo(ref nombreArchivo);
        }
    }

    protected void lnbDescargar_Click(object sender, EventArgs e)
    {
        if (Session["ID_usuario"] != null)
        {
            #region GENERACIÓN FORMULARIO DE SALIDA
            consultaSolicitudXArchivo(nTDC.consultaSolicitudXArchivo(ltrNombreArchivo.Text));
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
            Response.AddHeader("Content-Disposition", "attachment; filename= SolicituddeEmisionTDC" + string.Format("{0:ddMMyyyy}", DateTime.Today) + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
    }
}