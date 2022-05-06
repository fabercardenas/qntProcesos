using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using Ionic.Zip;
using OfficeOpenXml;

public partial class TDC_SetValidation : System.Web.UI.Page
{
    Negocio.NTDC nTDC = new Negocio.NTDC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"]!=null) && (!Page.IsPostBack))
        {
            dvIdConsulta.Visible = true;
            ltrFechaValidacion.Text = string.Format("{0:yyyyMMdd}", DateTime.Today);
            ConsultarSolicitudesXestado();
        }
    }


    protected void lnbConsultarTarjeta_Click(object sender, EventArgs e)
    {
        ConsultarSolicitudesXtarjeta();
    }

    protected void lnbConsultarFecha_Click(object sender, EventArgs e)
    {
        ltrFechaValidacion.Text = string.Format("{0:yyyyMMdd}", txtFechaVal.Text);
        DataTable ValidacionFecha = nTDC.consultaSolicitudXfechaValidacion(ltrFechaValidacion.Text);
        if (ValidacionFecha.Rows.Count>0)
        {
            ltrMensaje.Text = Messaging.Success("Validaciones encontradas para la Fecha Seleccionada " + ValidacionFecha.Rows.Count);
            lnbEnvio.Visible = true;
            lnbTerminar.Visible = false;
            dvIdConsulta.Visible = false;
        }
        else
            ltrMensaje.Text = Messaging.Warning("No hay Validaciones para la fecha seleccionada");
    }

    void ConsultarSolicitudesXestado()
    {
        gdvListaSolicitudes.DataSource = nTDC.ConsultarXestadoVal(4);
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

    void ConsultarSolicitudesXtarjeta()
    {
        gdvListaSolicitudes.DataSource = nTDC.ConsultarXtarjetaVal(txtNumTarjeta.Text);
        gdvListaSolicitudes.DataBind();
    }

    protected void gdvListaSolicitudes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        gdvListaSolicitudes.SelectedIndex = e.NewSelectedIndex;
        string ltrCliDocumento = gdvListaSolicitudes.DataKeys[e.NewSelectedIndex].Value.ToString();
        nTDC.actualizaSolicitudValXDocumento(gdvListaSolicitudes.DataKeys[e.NewSelectedIndex].Value.ToString(), Session["ID_usuario"].ToString());
        ltrMensaje.Text = Messaging.Success("Documento " + ltrCliDocumento + " Validado con éxito");
        ConsultarSolicitudesXestado();
    }

    protected void lnbTerminar_Click(object sender, EventArgs e)
    {
        ltrFechaValidacion.Text = string.Format("{0:yyyyMMdd}", DateTime.Today);
        DataTable ValidacionProce = nTDC.consultaSolicitudXfechaValidacion(ltrFechaValidacion.Text);
        if (ValidacionProce.Rows.Count > 0)
        {
            ltrMensaje.Text = Messaging.Success("Validaciones encontradas para la Fecha Seleccionada " + ValidacionProce.Rows.Count);
            lnbTerminar.Visible = false;
            lnbEnvio.Visible = true;
            dvIdConsulta.Visible = false;
        }
        else
            ltrMensaje.Text = Messaging.Warning("No hay Validaciones ejecutadas el día de hoy");
    }

    #region GENERACIÓN ARCHIVO DE ENVIO INTERRAPIDISIMO
    protected void lnbEnvio_Click(object sender, EventArgs e)
    {
        consultaSolicitudXfechaValidacion(nTDC.consultaSolicitudXfechaValidacion(ltrFechaValidacion.Text));
    }

    protected void consultaSolicitudXfechaValidacion(DataTable tbl)
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
            Response.AddHeader("Content-Disposition", "attachment; filename= Entrega_Interrapidisimo" + ltrFechaValidacion.Text + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
    }
    #endregion
    
}
