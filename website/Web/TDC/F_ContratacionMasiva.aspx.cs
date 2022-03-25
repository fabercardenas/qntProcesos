using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Collections;
using System.Configuration;

public partial class Web_Archivos_F_Migracion : System.Web.UI.Page
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
                //string narchivo = System.IO.Path.GetFileName(this.fupFacArchivo.PostedFile.FileName);
                string extension = System.IO.Path.GetExtension(this.fupArchivo.PostedFile.FileName);
                narchivo = "Migracion" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Millisecond.ToString() + extension;

                string ruta = Server.MapPath("~\\Docs\\") + narchivo;

                try
                {
                    if ((extension.ToUpper() == ".XLS") || (extension.ToUpper() == ".TXT") || (extension.ToUpper() == ".XLSX"))
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
                    this.ltrMensaje.Text = Messaging.Error("Ocurrio una falla al cargar el archivo. Intente nuevamente, por favor.<br />" + ex.Message);
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

        Negocio.NEmpleados nEmpleado = new Negocio.NEmpleados();

        string afi_documento = "";
        string errores = "", errorLinea = "";
        DataTable tbAfiliado, tbDatos;

        try
        {
            #region VALIDAR EXTRUCTURA
            foreach (DataGridItem dgl in dgFaber.Items)
            {
                errorLinea = "";
                afi_documento = dgl.Cells[3].Text;

                if ((afi_documento != "&nbsp;") && (afi_documento != ""))
                {
                    try
                    {
                        #region VALIDAR CAMPOS
                        if (Negocio.NUtilidades.IsDouble(afi_documento) == false)
                            errorLinea += "El número de documento no es válido";
                            tbAfiliado = nEmpleado.ConsultaXDocumento(afi_documento);
                        if (tbAfiliado.Rows.Count == 0)
                            errorLinea += "El empleado con documento " + afi_documento + " no existe. ";
                        if (Negocio.NUtilidades.IsNumeric(dgl.Cells[5].Text) == false)
                            errorLinea += "ID de cargo no válido, debe ser un número. ";
                        if (Negocio.NUtilidades.IsDate(dgl.Cells[7].Text.Substring(0, 10)) == false)
                            errorLinea += "La fecha de inicio de contrato no es valida. ";
                        if (Negocio.NUtilidades.IsNumeric(dgl.Cells[8].Text) == false)
                            errorLinea += "Salario no valido. ";
                        if ((dgl.Cells[9].Text!="&nbsp;") && (Negocio.NUtilidades.IsDate(dgl.Cells[9].Text) == false))
                            errorLinea += "Fecha tentativa de retiro no valida. ";
                        if ((dgl.Cells[10].Text != "OBRA O LABOR") && (dgl.Cells[10].Text != "TERMINO FIJO") && (dgl.Cells[10].Text != "TERMINO INDEFINIDO"))
                            errorLinea += "Tipo de contrato no valido. ";
                        if ((dgl.Cells[11].Text != "Tiempo Completo") && (dgl.Cells[10].Text != "Medio Tiempo") && (dgl.Cells[10].Text != "Tiempo Parcial"))
                            errorLinea += "Jornada laboral no valida. ";

                        //verifico que el numero de cédula solo venga una vez en el archivo
                        //if(dt.Compute("count(*)","CEDULA='" + afi_documento + "'"))

                        tbDatos = Negocio.NUtilidades.consultaCargoXID(dgl.Cells[5].Text);
                        if (tbDatos.Rows.Count == 0)
                            errorLinea += "Cargo no valido. ";

                        tbDatos = Negocio.NUtilidades.consultaEmpresaXNombreCorto(dgl.Cells[0].Text);
                        if(tbDatos.Rows.Count==0)
                            errorLinea += "Empresa temporal no valida. ";
                        else //la temporal existe, verifico que no tenga contrato activo con esta temporal
                        {
                            if ((tbAfiliado.Rows.Count > 0) && (dClientes.mig_ExisteContrato(tbDatos.Rows[0]["ID_empresa"].ToString(), tbAfiliado.Rows[0]["ID_afiliado"].ToString()) == true))
                                errorLinea += "El empleado tiene contrato activo con " + dgl.Cells[0].Text + ". ";
                        }

                        if (dClientes.mig_clienteConsultarXNombre(dgl.Cells[1].Text)==0)
                            errorLinea += "Empresa cliente no valida. ";

                        if (dClientes.mig_centroConsultarXNombre(dgl.Cells[2].Text, dClientes.mig_clienteConsultarXNombre(dgl.Cells[1].Text).ToString()) == 0)
                            errorLinea += "Centro de costo no valido. ";
                        #endregion
                    }
                    catch (Exception ex)
                    {
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

                int idCliente, idCentroCosto, idSede;
                int cargados = 0;
                string idTemporal;
                DataRow rEmp;
                DataTable tbUltimoContrato;

                foreach (DataGridItem dgl in dgFaber.Items)
                {
                    afi_documento = dgl.Cells[3].Text;
                    if ((afi_documento != "&nbsp;") && (afi_documento != ""))
                    {
                        try
                        {
                            //Response.Write("si llega<br />");
                            //Response.Write(afi_documento);
                            tbDatos = Negocio.NUtilidades.consultaEmpresaXNombreCorto(dgl.Cells[0].Text);
                            idTemporal = tbDatos.Rows[0]["ID_empresa"].ToString();
                            idCliente = dClientes.mig_clienteConsultarXNombre(dgl.Cells[1].Text);
                            idCentroCosto = dClientes.mig_centroConsultarXNombre(dgl.Cells[2].Text, idCliente.ToString());
                            tbDatos = nEmpleado.ConsultaXDocumento(afi_documento);
                            rEmp = tbDatos.Rows[0];
                            tbUltimoContrato = nEmpleado.ContratosConsultarXEmpleado(rEmp["ID_afiliado"].ToString());

                            idSede = (int)tbUltimoContrato.Rows[0]["ID_sedeFK"];
                            //Response.Write("si llega a sede<br />");
                            //Response.Write(idSede);
                            #region ORDEN DE CONTRATACION
                            string apellido1 = (rEmp["afi_apellido1"].ToString() == "&nbsp;") ? "" : rEmp["afi_apellido1"].ToString();
                            string apellido2 = (rEmp["afi_apellido2"].ToString() == "&nbsp;") ? "" : rEmp["afi_apellido2"].ToString();
                            string nombre1 = (rEmp["afi_nombre1"].ToString() == "&nbsp;") ? "" : rEmp["afi_nombre1"].ToString();
                            string nombre2 = (rEmp["afi_nombre2"].ToString() == "&nbsp;") ? "" : rEmp["afi_nombre2"].ToString();
                            string fechaTentativaRetiro = (dgl.Cells[9].Text != "&nbsp;") ? string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dgl.Cells[9].Text)) : "";
                            string tipoContrato = dgl.Cells[10].Text;
                            string jornadaLaboral = dgl.Cells[11].Text;
                            DataTable tbOrden = nEmpleado.OrdenContratacionInsertar(idTemporal, idCliente.ToString(), idCentroCosto.ToString(), rEmp["afi_tipoDocumento"].ToString(),
                                                    afi_documento, apellido1, apellido2, rEmp["afi_nombre1"].ToString(), rEmp["afi_nombre2"].ToString(),
                                                    dgl.Cells[5].Text, dgl.Cells[6].Text, dgl.Cells[8].Text, "0", 
                                                    string.Format("{0:yyyy-MM-dd 08:00}", Convert.ToDateTime(dgl.Cells[7].Text)), "", "Reintegro", Session["ID_usuario"].ToString(), 
                                                    "empresaUsuaria", tipoContrato, jornadaLaboral, tbUltimoContrato.Rows[0]["con_nivelRiesgo"].ToString(),
                                                    "11","001", fechaTentativaRetiro);
                            //Response.Write("si llega a orden de contratacion<br />");
                            //Response.Write(idSede);
                            #endregion

                            nEmpleado.ContratoInsertar(rEmp["ID_afiliado"].ToString(), tbOrden.Rows[0]["ID_laboratorioFK"].ToString(), string.Format("{0:yyyy-MM-dd}"
                                            , Convert.ToDateTime(dgl.Cells[7].Text)), "REIN_" + rEmp["ID_afiliado"].ToString(), "Apto", "", "0",""
                                            , idTemporal, idCliente.ToString(), idCentroCosto.ToString(), idSede.ToString(), int.Parse(tbOrden.Rows[0]["ID_ordenContratacion"].ToString())
                                            , dgl.Cells[6].Text , tbUltimoContrato.Rows[0]["con_nivelRiesgo"].ToString(), string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dgl.Cells[7].Text))
                                            , tipoContrato, "Si" , "No", jornadaLaboral, dgl.Cells[5].Text, int.Parse(dgl.Cells[8].Text), 0
                                            , Session["ID_usuario"].ToString(), "No","11","001", fechaTentativaRetiro);

                            nEmpleado.OrdenContratacionActualizaEstado(tbOrden.Rows[0]["ID_ordenContratacion"].ToString(), "5", Session["ID_usuario"].ToString(), "");

                            cargados++;
                        }
                        catch (Exception)
                        {

                            throw;
                        }
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
            
            throw;
        }
        

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