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
            string ruta = Server.MapPath("~\\website\\files\\TDC\\") + nombreArchivo;
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
            string errores = "", errorLinea = "";
            int fila = 1;
            foreach (string registro in arreglo)
            {
                errorLinea = "";
                string[] datos = registro.Split(';');
                if (datos[0] != "CodigoDeceval" && datos[0] != "" && datos[0] != "&nbsp;")
                {
                    if (Negocio.NUtilidades.IsDouble(datos[10].ToString()) == false)
                        errorLinea += "El número de documento no válido. ";
                    //validar que la fecha de grabacion del pagare este correcta

                    //validar que no tenga pagare cargado

                    if (errorLinea != "")
                        errores += "Fila " + fila.ToString() + ", documento " + datos[10].ToString() + ": " + errorLinea + "<br />";
                    fila++;
                }
            }

            #endregion

            #region 3) SI LA ESTRUCTURA ESTA BIEN, INSERTA LOS DATOS
            if ((errores != ""))
            {
                ltrMensaje.Text = Messaging.Error("Hay errores en el archivo") + errores + "<br />";
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
                        //insertar en la base de datos
                        //Ejemplo:
                        //nTDC.actualizaInfoXDocumento(dgl.Cells[0].Text.......
                        cargados++;
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