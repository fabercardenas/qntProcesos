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
using System.Collections;

public partial class TDC_LoadPromissory : System.Web.UI.Page
{
    Negocio.NEmpleados nEmpleado = new Negocio.NEmpleados();
    Datos.DClientes dClientes = new Datos.DClientes();
    Negocio.NTDC nTDC = new Negocio.NTDC();

    protected void Page_Load(object sender, EventArgs e)
    {
        Literal ltrTituloModulo = (Literal)this.Master.FindControl("ltrTituloModulo");
        ltrTituloModulo.Text = "<span class='fa fa-newspaper-o'></span> <b>Carga de Pagaré - Paso 7.2</b>";
    }

    Boolean validaArchivo(ref string narchivo)
    {   //el archivo es obligatorio
        if (fupArchivo.HasFile == true)
        {
            if ((fupArchivo.PostedFile != null) && (fupArchivo.PostedFile.ContentLength > 0))
            {
                //SUBE EL ARCHIVO
                string extension = System.IO.Path.GetExtension(this.fupArchivo.PostedFile.FileName);
                narchivo = "Pagare" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Millisecond.ToString() + extension;

                string ruta = Server.MapPath("~\\files\\TDC\\") + narchivo;

                try
                {
                    if ((extension.ToUpper() == ".CSV"))
                    {
                        fupArchivo.PostedFile.SaveAs(ruta);
                        ltrNombreArchivo.Text = narchivo;
                        return ImportarArchivo(ruta, extension, narchivo);
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

    bool ImportarArchivo(string FilePath, string Extension, string nombreArchivo)
    {
        if (Session["ID_usuario"] != null)
        {
            string ruta = Server.MapPath("~\\files\\TDC\\") + nombreArchivo;
            StreamReader elArchivo = File.OpenText(ruta);
            string linea;
            ArrayList arreglo = new ArrayList();
            #region 1) RECORRER EL ARCHIVO Y CARGAR LAS LINEAS EN UN ARREGLO PARA VER SI ESTA BIEN EL ARCHIVO
            do
                {
                    linea = elArchivo.ReadLine();
                    if (linea != null)
                        arreglo.Add(linea);
                } while (linea != null);
            #endregion

            #region 2) REVISAR TODAS LAS LINEAS PARA SABER SI LA ESTRUCTURA ESTA BIEN
            DataTable tbValidarPagare;
            string errores = "", errorLinea = "";
            int fila = 1;
            foreach (string registro in arreglo)
            {
                errorLinea = "";
                string[] datos = registro.Split(';');
                if (datos.Length == 20)
                {
                    if (datos[0] != "CodigoDeceval" && datos[0] != "" && datos[0] != "&nbsp;")
                    {
                        if ((datos[8].ToString() == "&nbsp;") || (datos[8].ToString() == ""))
                            errorLinea += "Tipo Pagaré requerido. ";
                        if ((datos[9].ToString() == "&nbsp;") || (datos[9].ToString() == ""))
                            errorLinea += "Tipo de Documento Otorgante requerido. ";
                        if (Negocio.NUtilidades.IsDouble(datos[10].ToString()) == false)
                            errorLinea += "El número de documento no válido. ";
                        if ((datos[10].ToString() == "&nbsp;") || (datos[10].ToString() == ""))
                            errorLinea += "Número de Documento Otorgante requerido. ";
                        if ((datos[12].ToString() == "&nbsp;") || (datos[12].ToString() == ""))
                            errorLinea += "Nombre otorgante requerido. ";
                        if (Negocio.NUtilidades.IsDate(datos[17].ToString()) == false)
                            errorLinea += "Fecha de Grabación del Pagaré no válida. ";
                        if ((datos[18].ToString() == "&nbsp;") || (datos[18].ToString() == ""))
                            errorLinea += "Nombre estado Pagaré requerido. ";
                        //VALIDAR QUE NO TENGA PAGARÉ CARGADO
                        tbValidarPagare = nTDC.consultaSolicitudXDocumento(datos[10].ToString());
                        if (tbValidarPagare.Rows.Count > 0)
                        {
                            if (tbValidarPagare.Rows[0]["tdc_docPagare"].ToString() != "False")
                                errorLinea += "La solicitud ya cuenta con un Pagaré, registrado el " + tbValidarPagare.Rows[0]["tdc_fechaRegistroPagare"].ToString() + ". ";
                        }
                        else
                            errorLinea += "El documento " + datos[10].ToString() + " no cuenta con una solicitud en la aplicación. ";
                        fila++;
                }
                }
                else
                    errorLinea += "El número de columnas no es válido. Se necesitan 20 y el registro tiene " + datos.Length + ". ";
                    if (errorLinea != "")
                        errores += "Fila " + fila.ToString() + ", documento " + datos[10].ToString() + ": " + errorLinea + "<br />";
            }
            #endregion

            #region 3) SI LA ESTRUCTURA ESTA BIEN, INSERTA LOS DATOS
            if ((errores != ""))
            {
                ltrMensaje.Text = Messaging.Error("Hay errores en el archivo ") + errores + "<br />";
                return false;
            }
            else 
            { 
                int cargados = 0;
                foreach (string registro in arreglo)
                {
                    string[] datos = registro.Split(';');
                    if (datos[0] != "CodigoDeceval" && datos[0] != "" && datos[0] != "&nbsp;")
                    {
                        if ((datos[10].ToString() != "&nbsp;") && (datos[10].ToString() != ""))
                        {
                            nTDC.InsertarPagare(datos[0].ToString(), datos[8].ToString(), datos[9].ToString(), datos[10].ToString(), datos[12].ToString(), datos[14].ToString(), datos[15].ToString(), datos[16].ToString(), datos[17].ToString(), datos[18].ToString(), datos[19].ToString(), Session["ID_usuario"].ToString());
                            cargados++;
                        }
                    }
                }
                ltrMensaje.Text = Messaging.Success((cargados).ToString() + " registros cargados exitosamente. ");
                return true;
            }
            #endregion
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