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

public partial class TDC_GetRequest : System.Web.UI.Page
{
    Negocio.NTDC nTDC = new Negocio.NTDC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"]!=null) && (!Page.IsPostBack))
        {
            ltrMensaje.Text = "";
            ltrMensaje.Visible = false;
            btnGenerarExcel.Visible = false;
            txtConsultaFiltro.Text = "";
        }
    }

    protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltrMensaje.Text = "";
        ltrMensaje.Visible = false;
        txtConsultaFiltro.Text = "";
        if (ddlFiltro.SelectedValue == "tdc_paso")
        {
            dvFiltro2.Visible = true;
            dvConsultaF.Visible = false;
        }
        else
        {
            dvFiltro2.Visible = false;
            btnGenerarExcel.Visible = false;
            dvConsultaF.Visible = true;
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ltrMensaje.Visible = false;
        switch (ddlFiltro.SelectedValue)
        {
            case "tdc_numeroDocumento":
            case "tdc_contrato":
            case "tdc_numeroTarjeta":
                gdvListaSolicitudes.Visible = false;
                FrvConsultarSolicitud();
                break;
            case "tdc_paso":
                frvConsultarSolicitud.Visible = false;
                ConsultarSolicitudes();
                break;
        }
        
    }

    void FrvConsultarSolicitud()
    {
        DataTable tbSolicitud = nTDC.consultaSolicitudGeneral(ddlFiltro.SelectedValue, txtConsultaFiltro.Text);
        if (tbSolicitud.Rows.Count > 0)
        {
            frvConsultarSolicitud.DataSource = tbSolicitud;
            frvConsultarSolicitud.DataBind();
            frvConsultarSolicitud.Visible = true;
            Literal ltrHdfPasoActual = (Literal)frvConsultarSolicitud.FindControl("ltrHdfPasoActual");
            Literal ltrHdfDireccion = (Literal)frvConsultarSolicitud.FindControl("ltrHdfDireccion");
            Literal ltrMensajePaso = (Literal)frvConsultarSolicitud.FindControl("ltrMensajePaso");
            if ((ltrHdfPasoActual.Text == "4") && (ltrHdfDireccion.Text == ""))
                ltrMensajePaso.Text = "<span class='btn btn-danger' ><span class='fa fa-warning'></span> No tiene ubicación</span><br />";
            else
                ltrMensajePaso.Text = "";

            CargarDDLCambioPaso(ltrHdfPasoActual.Text);
        }
        else
        {
            frvConsultarSolicitud.Visible = false;
            ltrMensaje.Visible = true;
            ltrMensaje.Text = Negocio.NMessaging.Error("No hay solicitudes con este " + ddlFiltro.SelectedItem.Text);
        }
        
    }

    public void CargarDDLCambioPaso(string pasoActual)
    {

        DropDownList ddlCambioPaso = (DropDownList)frvConsultarSolicitud.FindControl("ddlCambioPaso");
        ListItem paso1 = new ListItem("Carga Tarjetas Aprobadas - Paso 1", "1");
        ListItem paso2 = new ListItem("Carga de Información Tarjetas - Paso 2", "2");
        ListItem paso3 = new ListItem("Sincronización de Ubicaciones - Paso 3", "3");
        ListItem paso4 = new ListItem("Consulta Prevalidación - Paso 4", "4");
        ListItem paso5 = new ListItem("Consulta Validación - Paso 5", "5");
        ListItem paso6 = new ListItem("Consulta Confirmación de Entrega - Paso 6", "6");

        switch (pasoActual)
        {
            case "2":
                ddlCambioPaso.Items.Add(paso1);
                break;
            case "3":
                ddlCambioPaso.Items.Add(paso2);
                ddlCambioPaso.Items.Add(paso1);
                break;
            case "4":
                ddlCambioPaso.Items.Add(paso3);
                ddlCambioPaso.Items.Add(paso2);
                ddlCambioPaso.Items.Add(paso1);
                break;
            case "5":
                ddlCambioPaso.Items.Add(paso4);
                ddlCambioPaso.Items.Add(paso3);
                ddlCambioPaso.Items.Add(paso2);
                ddlCambioPaso.Items.Add(paso1);
                break;
            case "6":
                ddlCambioPaso.Items.Add(paso5);
                ddlCambioPaso.Items.Add(paso4);
                ddlCambioPaso.Items.Add(paso3);
                ddlCambioPaso.Items.Add(paso2);
                ddlCambioPaso.Items.Add(paso1);
                break;
            case "8":
                ddlCambioPaso.Items.Add(paso6);
                ddlCambioPaso.Items.Add(paso5);
                ddlCambioPaso.Items.Add(paso4);
                ddlCambioPaso.Items.Add(paso3);
                ddlCambioPaso.Items.Add(paso2);
                ddlCambioPaso.Items.Add(paso1);
                break;
        }
        ddlCambioPaso.DataBind();
    }

    protected void btnDevolverPaso_Click(object sender, EventArgs e)
    {
        DropDownList ddlCambio = (DropDownList)frvConsultarSolicitud.FindControl("ddlCambioPaso");
        ddlCambio.Visible = true;
        Button btnDevuelve = (Button)frvConsultarSolicitud.FindControl("btnDevolver");
        btnDevuelve.Visible = true;

    }
    protected void btnDevolver_Click(object sender, EventArgs e)
    {
        Literal idSolicitud = (Literal)frvConsultarSolicitud.FindControl("ltrIdSolicitud");
        Literal ltrHdfPasoActual = (Literal)frvConsultarSolicitud.FindControl("ltrHdfPasoActual");
        DropDownList ddlCambioPaso = (DropDownList)frvConsultarSolicitud.FindControl("ddlCambioPaso");
        nTDC.DevolverSolicitud(idSolicitud.Text, ltrHdfPasoActual.Text, ddlCambioPaso.SelectedValue, Session["ID_usuario"].ToString());
        FrvConsultarSolicitud();

    }

    void ConsultarSolicitudes()
    {
        DataTable tbSolicitud = nTDC.consultaSolicitudGeneral(ddlFiltro.SelectedValue, ddlPasos.SelectedValue);
        if (tbSolicitud.Rows.Count > 0)
        {
            gdvListaSolicitudes.DataSource = tbSolicitud;
            gdvListaSolicitudes.DataBind();
            gdvListaSolicitudes.Visible = true;
            btnGenerarExcel.Visible = true;
            btnGenerarExcel.Text = "Generar Archivo con " + gdvListaSolicitudes.Rows.Count.ToString() + " Registros";
            gdvListaSolicitudes.Columns[3].Visible = false;
            gdvListaSolicitudes.Columns[4].Visible = false;
            switch (ddlPasos.SelectedValue)
            {
                case "2":
                    gdvListaSolicitudes.Columns[3].Visible = true;
                    gdvListaSolicitudes.Columns[4].Visible = true;
                    break;
            }
        }
        else
        {
            gdvListaSolicitudes.Visible = false;
            btnGenerarExcel.Visible = false;
            ltrMensaje.Visible = true;
            ltrMensaje.Text = Negocio.NMessaging.Error("No hay solicitudes con este " + ddlFiltro.SelectedItem.Text);
        }
    }

    #region GENERACIÓN ARCHIVO CONSULTA POR PASOS
    protected void btnGenerarExcel_Click(object sender, EventArgs e)
    {
        consultaSolicitudArchivoXpaso(nTDC.consultaSolicitudGeneral(ddlFiltro.SelectedValue, ddlPasos.SelectedValue));
    }

    protected void consultaSolicitudArchivoXpaso(DataTable tbl)
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
            Response.AddHeader("Content-Disposition", "attachment; filename= Consulta" + ddlPasos.SelectedItem.Text + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
    }
    #endregion

}
