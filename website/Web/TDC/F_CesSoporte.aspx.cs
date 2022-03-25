using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.IO;

public partial class Web_Archivos_F_CesSoporte : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        ltrMensaje.Text = "";
        if ((fupArchivoExcel.PostedFile != null) && (fupArchivoExcel.PostedFile.ContentLength > 0))
        {
            //SUBE EL ARCHIVO
            Boolean banderaPDF = false;

            banderaPDF = cargarPDFCesantias();
            if (banderaPDF == true)
            {
                string nArchivo = "";
                cargarExcelCesantias(ref nArchivo);
            }
        }
        else
            ltrMensaje.Text = Messaging.Error("El archivo esta vacio");
        
    }

    public Boolean cargarPDFCesantias()
    {
        string narchivoPDF = "CES" + Session["ID_usuario"].ToString() + "_PDF" + DateTime.Today.Year.ToString() + DateTime.Today.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + System.IO.Path.GetFileName(archivoPDF.PostedFile.FileName);
        string extensionPDF = System.IO.Path.GetExtension(archivoPDF.PostedFile.FileName);
        string rutaPDF = Server.MapPath("../../Docs/SoportesSegSocial") + "/" + narchivoPDF;
        lblArchivoPDF.Text = narchivoPDF;

        //VALIDO SI ES EL PDF PARA QUE ME SUBAN SOPORTES
        if (extensionPDF.ToUpper() == ".PDF")
        {
            archivoPDF.PostedFile.SaveAs(rutaPDF);
            return true;
        }
        else
        {
            lblMensaje.Text = "El segundo soporte debe ser de tipo PDF (.PDF)";
            return false;
        }
    }

    Boolean cargarExcelCesantias(ref string narchivo)
    {   //el archivo es obligatorio
        if (fupArchivoExcel.HasFile == true)
        {
            if ((fupArchivoExcel.PostedFile != null) && (fupArchivoExcel.PostedFile.ContentLength > 0))
            {
                //SUBE EL ARCHIVO
                //string narchivo = System.IO.Path.GetFileName(this.fupFacArchivo.PostedFile.FileName);
                string extension = System.IO.Path.GetExtension(this.fupArchivoExcel.PostedFile.FileName);
                narchivo = "CES" + Session["ID_usuario"].ToString() + "_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Millisecond.ToString() + extension;

                string ruta = Server.MapPath("~\\Docs\\SoportesSegSocial\\") + narchivo;

                try
                {
                    if ((extension.ToUpper() == ".XLS") || (extension.ToUpper() == ".XLSX"))
                    {
                        fupArchivoExcel.PostedFile.SaveAs(ruta);
                        //CARGAR DEPENDIENDO DEL OPERADOR
                        switch (ddlOperador.SelectedValue)
                        {
                            case "AportesEnLinea":
                                cargarExcelCesantiasAportesEnLinea(ruta, extension, narchivo);
                                return true;
                            default:
                                return false;
                        }
                    }
                    else
                    {
                        this.ltrMensaje.Text = Messaging.Error("El tipo de archivo que intenta cargar no es valido");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    this.ltrMensaje.Text = Messaging.Error("Ocurrio una falla al cargar el archivo. Intente nuevamente, por favor.<br />" + ex.Message);
                    return false;
                }
            }
            else
            {
                this.ltrMensaje.Text = Messaging.Error("Por favor seleccione un archivo excel para cargar");
                return false;
            }
        }
        else
        {
            this.ltrMensaje.Text = Messaging.Error("Por favor seleccione un archivo excel para cargar");
            return false;
        }
    }

    bool cargarExcelCesantiasAportesEnLinea(string FilePath, string Extension, string nombreArchivo)
    {
        #region CONEXION EXCEL

        string conStr = "";
        DataGrid dgFaber = new DataGrid();

        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
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

        string errores = "", errorLinea = "";
        DataTable tbAfiliado;
        Negocio.NNomina nNomina = new Negocio.NNomina();
        string afi_documento = "";
        Negocio.NEmpleados nEmpleado = new Negocio.NEmpleados();
        try
        {
            #region VALIDAR EXTRUCTURA
            foreach (DataGridItem dgl in dgFaber.Items)
            {
                errorLinea = "";
                afi_documento = dgl.Cells[10].Text;

                if ((afi_documento != "&nbsp;") && (afi_documento != "") && (Negocio.NUtilidades.IsNumeric(afi_documento) == true))
                {
                    try
                    {
                        #region VALIDAR CAMPOS
                        tbAfiliado = nEmpleado.ConsultaXDocumento(afi_documento);
                        if (tbAfiliado.Rows.Count == 0)
                            errorLinea += "El empleado con documento " + afi_documento + " no existe. ";
                        if ((Negocio.NUtilidades.IsNumeric(Negocio.NUtilidades.stringMoneyToDouble(dgl.Cells[21].Text).ToString()) == false))
                            errorLinea += "Salario Base no es valido. ";
                        if ((Negocio.NUtilidades.IsNumeric(Negocio.NUtilidades.stringMoneyToDouble(dgl.Cells[25].Text).ToString()) == false))
                            errorLinea += "Valor Pagado no es valido. ";
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                        throw;
                    }

                }
                if (errorLinea != "")
                    errores += "Fila " + (dgl.ItemIndex + 2).ToString() + ", documento " + afi_documento + ": " + errorLinea + "<br />";
            }
            #endregion

            if (errores != "")
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
                string Clave = "";
                string Transaccion = "";
                string NombreSucursal = "";
                string FechaPago = "";
                string descripcionPlanilla = "";
                int periodo = 0;
                int numeroEmpleados = 0;
                string capturoDato = "";
                string banco = "";
                string nit = "";
                string nombreTemporal = "";
                double totalPagado = 0;
                double totalIndividual = 0;

                string tipoDocumento, Documento;
                String[] arrayNit;
                int indSucursal, indFechaPago, indTipoDoc, indDocumento, indBanco;
                int indPeriodo, indClave;
                string tipoPlanilla = dt.Rows[10][26].ToString();

                indSucursal = 15;
                indPeriodo = 1;
                indClave = 0;
                indFechaPago = 14;
                indBanco = 16;

                indTipoDoc = 6;
                indDocumento = 10;

                foreach (DataGridItem dgl in dgFaber.Items)
                {
                    #region CAPTURAR VALOR Y CONSULTAR DATA
                    try
                    {
                        //for (int index = 0; index < dgl.Cells.Count; index++)
                        //{
                        //    System.Diagnostics.Debug.WriteLine(index + ":" + dgl.Cells[index].Text);                            
                        //}
                        
                        switch (capturoDato)
                        {
                            case "Sucursal":
                                NombreSucursal = dgl.Cells[indSucursal].Text;
                                nombreTemporal = dgl.Cells[10].Text;
                                arrayNit = dgl.Cells[0].Text.Split(' ');
                                nit = arrayNit[1];
                                capturoDato = "";
                                break;
                            case "Periodo":
                                periodo = Convert.ToInt16( dgl.Cells[indPeriodo].Text);
                                numeroEmpleados = Convert.ToInt32(dgl.Cells[15].Text);
                                totalPagado = Negocio.NUtilidades.stringMoneyToDouble(dgl.Cells[27].Text);
                                capturoDato = "";
                                break;

                            case "Clave":
                                Clave = dgl.Cells[indClave].Text;
                                Transaccion = dgl.Cells[6].Text;
                                FechaPago = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dgl.Cells[indFechaPago].Text));
                                descripcionPlanilla = "Planilla " + Clave + ", sucursal " + NombreSucursal + " con fecha de pago " + FechaPago;
                                banco = dgl.Cells[indBanco].Text;
                                capturoDato = "";
                                break;
                            default:
                                break;
                        }

                        if (dgl.Cells[indSucursal].Text == "Sucursal Principal")
                            capturoDato = "Sucursal";

                        if (dgl.Cells[indPeriodo].Text == "Periodo")
                            capturoDato = "Periodo";

                        if (dgl.Cells[indClave].Text == "Clave")
                            capturoDato = "Clave";

                        tipoDocumento = dgl.Cells[indTipoDoc].Text;

                        if (tipoDocumento == "CC" || tipoDocumento == "CE" || tipoDocumento == "TI")
                        {
                            capturoDato = "Afiliado";
                            Documento = dgl.Cells[indDocumento].Text;

                            nNomina.cesInsertaPago(nit, tipoDocumento, Documento, dgl.Cells[12].Text, dgl.Cells[14].Text, dgl.Cells[16].Text, dgl.Cells[18].Text, "", "", Negocio.NUtilidades.stringMoneyToDouble(dgl.Cells[21].Text), Negocio.NUtilidades.stringMoneyToDouble(dgl.Cells[25].Text)
                                , periodo, numeroEmpleados, NombreSucursal, Clave, Transaccion, banco, FechaPago, totalPagado, 0, lblArchivoPDF.Text, Session["ID_usuario"].ToString());

                            totalIndividual += Negocio.NUtilidades.stringMoneyToDouble(dgl.Cells[25].Text);
                            cargados++;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    #endregion
                }
                dgFaber.DataSource = null;
                dgFaber.DataBind();

                if (cargados > 0)
                {
                    string mensaje = (cargados).ToString() + " registros cargados<br />";
                    mensaje += "<br />Empresa Temporal: " + nombreTemporal;
                    mensaje += "<br />NIT " + nit;
                    mensaje += "<br />Sucursal : " + NombreSucursal;
                    mensaje += "<br />Clave: " + Clave;
                    mensaje += "<br />Periodo : " + periodo.ToString();
                    mensaje += "<br />Transacción: " + Transaccion;
                    mensaje += "<br />Fecha de pago: " + FechaPago;
                    mensaje += "<br />Total aportes: " + string.Format("$ {0:N0}", totalIndividual);

                    ltrMensaje.Text = Messaging.Success(mensaje);
                    return true;
                }
                else
                {
                    ltrMensaje.Text = Messaging.Error("No se encontraron empleados coincidentes para cargar");
                    return false;
                }
                #endregion
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
