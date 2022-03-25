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
                narchivo = "Paso1" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Millisecond.ToString() + extension;

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
                    if (dgl.Cells.Count == 4)
                    {
                        afi_documento = dgl.Cells[1].Text;

                        if ((afi_documento != "&nbsp;") && (afi_documento != ""))
                        {
                            try
                            {
                                #region VALIDAR CAMPOS
                                if ((dgl.Cells[0].Text != "1") && (dgl.Cells[0].Text != "2"))
                                    errorLinea += "Tipo de documento no valido. ";
                                if (Negocio.NUtilidades.IsNumeric(afi_documento) == false)
                                    errorLinea += "El número de documento no es válido";
                                //if ((dgl.Cells[2].Text != "1") && (dgl.Cells[2].Text != "2"))
                                //    errorLinea += "Canal no valido. ";
                                tbValidarReferencia = Negocio.NUtilidades.consultaReferenciaXModulo_y_Valor("Canales", dgl.Cells[2].Text);
                                if(tbValidarReferencia.Rows.Count==0)
                                    errorLinea += "Canal no valido. ";

                                tbValidarReferencia = Negocio.NUtilidades.consultaReferenciaXModulo_y_Valor("Procesos", dgl.Cells[3].Text);
                                if (tbValidarReferencia.Rows.Count == 0)
                                    errorLinea += "Proceso no valido. ";

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
                    foreach (DataGridItem dgl in dgFaber.Items)
                    {
                        try
                        {
                            afi_documento = dgl.Cells[1].Text;
                            if ((afi_documento != "&nbsp;") && (afi_documento != ""))
                            {
                                nTDC.Insertar(dgl.Cells[0].Text, afi_documento, dgl.Cells[2].Text, dgl.Cells[3].Text, Session["ID_usuario"].ToString());
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

                    ltrMensaje.Text = (cargados).ToString() + " registros cargados";
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