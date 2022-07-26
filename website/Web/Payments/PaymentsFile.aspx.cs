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

public partial class TDC_PaymentsFile : System.Web.UI.Page
{
    Negocio.NPagos nPagos = new Negocio.NPagos();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"] != null) && (!Page.IsPostBack))
        {
            Literal ltrTituloModulo = (Literal)this.Master.FindControl("ltrTituloModulo");
            ltrTituloModulo.Text = "<span class='fa fa-cloud-upload'></span> <b>Cargar Archivo para Registro de Pagos</b>";
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
                narchivo = "Pagos" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Millisecond.ToString() + extension;

                string ruta = Server.MapPath("~\\files\\PAGOS\\") + narchivo;

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
            DataGrid dgPagos = new DataGrid();

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
            dgPagos.Caption = Path.GetFileName(FilePath);
            dgPagos.DataSource = dt;
            dgPagos.DataBind();
            #endregion

            string cli_documento = "";
            string errores = "", errorLinea = "";

            try
            {
                #region VALIDAR EXTRUCTURA
                DataTable tbValidarReferencia;
                foreach (DataGridItem dgl in dgPagos.Items)
                {
                    errorLinea = "";
                    if (dgl.Cells.Count == 7)
                    {
                        cli_documento = dgl.Cells[0].Text;

                        if ((cli_documento != "&nbsp;") && (cli_documento != ""))
                        {
                            try
                            {
                                #region VALIDAR CAMPOS
                                if (Negocio.NUtilidades.IsDouble(cli_documento) == false)
                                    errorLinea += "El número de documento no es válido. ";
                                if ((dgl.Cells[1].Text == "&nbsp;") && (dgl.Cells[1].Text == ""))
                                    errorLinea += "El valor pagado es requerido. ";
                                if (Negocio.NUtilidades.IsDouble(dgl.Cells[1].Text) == false)
                                    errorLinea += "El valor pagado no es válido. ";
                                if ((dgl.Cells[2].Text == "&nbsp;") && (dgl.Cells[2].Text == ""))
                                    errorLinea += "La fecha de pagado es requerida. ";
                                if (Negocio.NUtilidades.IsDate(dgl.Cells[2].Text) == false)
                                    errorLinea += "La Fecha de pago no es válida. ";
                                if ((dgl.Cells[3].Text == "&nbsp;") && (dgl.Cells[3].Text == ""))
                                    errorLinea += "La hora de pagado es requerida. ";
                                //PENDIENTE VALIDAR EL FORMATO DE LA HORA 18:45
                                if ((dgl.Cells[4].Text == "&nbsp;") && (dgl.Cells[4].Text == ""))
                                    errorLinea += "El identificador único es requerido. ";
                                //VALIDAR EL CANAL DE PAGO DE LA TABLA DE REFERENCIAS
                                tbValidarReferencia = Negocio.NUtilidades.consultaReferenciaXModulo_y_Valor("Canales de Pago", dgl.Cells[5].Text);
                                if(tbValidarReferencia.Rows.Count==0)
                                    errorLinea += "Canal de Pago no válido. ";
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
                        errorLinea += "El número de columnas no es válido. Se necesitan 7 y el registro tiene " + dgl.Cells.Count + ". ";

                    if (errorLinea != "")
                        errores += "Fila " + (dgl.ItemIndex + 2).ToString() + ", documento " + cli_documento + ": " + errorLinea + "<br />";
                }
                #endregion

                if ((errores != ""))
                {
                    ltrMensaje.Text = Messaging.Error("Hay errores en el archivo") + errores + "<br />";
                    dgPagos.DataSource = null;
                    dgPagos.DataBind();
                    return false;
                }
                else
                {
                    #region INSERTAR
                    int cargados = 0;
                    string documentosSF = "";
                    string cCargue = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

                    foreach (DataGridItem dgl in dgPagos.Items)
                    {
                        try
                        {
                            cli_documento = dgl.Cells[0].Text;                            
                            if ((cli_documento != "&nbsp;") && (cli_documento != ""))
                            {
                                nPagos.InsertarCargue(cCargue, Session["ID_usuario"].ToString(), nombreArchivo, cli_documento, dgl.Cells[1].Text, dgl.Cells[2].Text, dgl.Cells[3].Text, dgl.Cells[4].Text, dgl.Cells[5].Text, dgl.Cells[6].Text);
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

                    ltrMensaje.Text = Messaging.Success ((cargados).ToString() + " registros cargados.");
                    dgPagos.DataSource = null;
                    dgPagos.DataBind();
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

}