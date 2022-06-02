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

public partial class TDC_LoadDelivery : System.Web.UI.Page
{
    Negocio.NEmpleados nEmpleado = new Negocio.NEmpleados();
    Datos.DClientes dClientes = new Datos.DClientes();
    Negocio.NTDC nTDC = new Negocio.NTDC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"] != null) && (!Page.IsPostBack))
        {
            dvIdConsulta.Visible = true;
            ltrFechaEntrega.Text = string.Format("{0:yyyyMMdd}", DateTime.Today);
            ltrNumeroGuia.Text = "";
            ConsultarSolicitudesXestado();
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
                narchivo = "Paso6" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Millisecond.ToString() + extension;

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

            string cli_documento = "";
            string errores = "", errorLinea = "";

            try
            {
                #region VALIDAR EXTRUCTURA
                DataTable tbValidarSolicitud;
                foreach (DataGridItem dgl in dgFaber.Items)
                {
                    errorLinea = "";
                    if (dgl.Cells.Count == 4)
                    {
                        cli_documento = dgl.Cells[1].Text;

                        if ((cli_documento != "&nbsp;") && (cli_documento != ""))
                        {
                            try
                            {
                                #region VALIDAR CAMPOS
                                if ((dgl.Cells[0].Text == "&nbsp;") && (dgl.Cells[0].Text == ""))
                                    errorLinea += "El número de tarejta es requerido. ";
                                if (Negocio.NUtilidades.IsDouble(dgl.Cells[0].Text) == false)
                                    errorLinea += "El número de tarejta no es válido. ";
                                if (Negocio.NUtilidades.IsDouble(cli_documento) == false)
                                    errorLinea += "El número de documento no válido. ";
                                if ((dgl.Cells[2].Text == "&nbsp;") && (dgl.Cells[2].Text == ""))
                                    errorLinea += "La Fecha de Entrega es requerida. ";
                                if (dgl.Cells[2].Text != "&nbsp;" & Negocio.NUtilidades.IsDate(dgl.Cells[2].Text) == false)
                                    errorLinea += "La Fecha de Entrega no es válida. ";
                                if ((dgl.Cells[3].Text == "&nbsp;") && (dgl.Cells[3].Text == ""))
                                    errorLinea += "El número de guia es requerido. ";
                                //VALIDAR EL PASO DE LA SOLICITUD PARA IDENTIFICAR SI PUEDE MODIFICARSE A ENTREGADA
                                tbValidarSolicitud = nTDC.consultaSolicitudXDocumento(cli_documento);
                                if (tbValidarSolicitud.Rows[0]["tdc_paso"].ToString() != "5")
                                    errorLinea += "La solicitud se encuentra en el paso " + tbValidarSolicitud.Rows[0]["tdc_paso"].ToString() + ". No puede ser modificada como entregada. ";
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
                        errorLinea += "El número de columnas no es válido. Se necesitan 4 y el registro tiene " + dgl.Cells.Count + ". ";

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
                    string documentosSF = "";
                    foreach (DataGridItem dgl in dgFaber.Items)
                    {
                        try
                        {
                            cli_documento = dgl.Cells[1].Text;                            
                            if ((cli_documento != "&nbsp;") && (cli_documento != ""))
                            {
                                nTDC.actualizaInfoXDocumento(dgl.Cells[0].Text, cli_documento,  dgl.Cells[2].Text, dgl.Cells[3].Text, Session["ID_usuario"].ToString(), nombreArchivo);
                                documentosSF += "\"" + cli_documento + "\",";
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

                    ltrMensaje.Text = Messaging.Success ((cargados).ToString() + " registros cargados exitosamente. ");
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
            ltrNombreArchivo.Text = "";
            validaArchivo(ref nombreArchivo);
        }
    }

    protected void lnbConsultarTarjeta_Click(object sender, EventArgs e)
    {
        ConsultarSolicitudesXtarjeta();
    }

    void ConsultarSolicitudesXestado()
    {
        gdvListaSolicitudes.DataSource = nTDC.ConsultarXestadoEntrega(5);
        gdvListaSolicitudes.DataBind();
    }

    public string TieneDireccion(string tdc_direccion)
    {
        string retornar = "";
        if (tdc_direccion.Length > 0)
            retornar = "<span class='glyphicon glyphicon-map-marker' style='color:red;'></span>";
        return retornar;
    }

    public string TienePrevalidacion(string tdc_fechaPrevalidacion)
    {
        string retornar = "";
        if (tdc_fechaPrevalidacion.Length > 0)
            retornar = "<span class='glyphicon glyphicon-ok-sign' style='color:green;'></span>";
        return retornar;
    }

    public string TieneValidacion(string tdc_fechaValidacion)
    {
        string retornar = "";
        if (tdc_fechaValidacion.Length > 0)
            retornar = "<span class='glyphicon glyphicon-ok-sign' style='color:green;'></span>";
        return retornar;
    }

    void ConsultarSolicitudesXtarjeta()
    {
        gdvListaSolicitudes.DataSource = nTDC.ConsultarXtarjetaEntrega(txtNumTarjeta.Text);
        gdvListaSolicitudes.DataBind();
    }

    protected void gdvListaSolicitudes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (txtFechaEntrega.Text != "" && txtNoGuia.Text != "" && txtFechaEntrega.Text != "&nbsp;" && txtNoGuia.Text != "&nbsp;")
        {
            gdvListaSolicitudes.SelectedIndex = e.NewSelectedIndex;
            string ltrCliDocumento = gdvListaSolicitudes.DataKeys[e.NewSelectedIndex].Values[0].ToString();
            string ltrCliTarjeta = gdvListaSolicitudes.DataKeys[e.NewSelectedIndex].Values[1].ToString();
            nTDC.actualizaInfoXDocumento(ltrCliTarjeta, ltrCliDocumento, txtFechaEntrega.Text, txtNoGuia.Text, Session["ID_usuario"].ToString(), "");
            ltrMensaje.Text = Messaging.Success("La tarjeta " + ltrCliTarjeta + " gestionada con éxito");
            ConsultarSolicitudesXestado();
        }
        else
            ltrMensaje.Text = Messaging.Error("Los campos de Fecha de Entrega y/o Número de Guía son obligatorios. ");
    }
   
}