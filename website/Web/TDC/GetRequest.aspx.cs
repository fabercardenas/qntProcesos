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
            txtConsultaFiltro.Text = "";
        }
    }

    protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltrMensaje.Text = "";
        txtConsultaFiltro.Text = "";
        if (ddlFiltro.SelectedValue == "Paso")
        {
            dvFiltro2.Visible = true;
            dvConsultaF.Visible = false;
        }
        else
        {
            dvFiltro2.Visible = false;
            dvConsultaF.Visible = true;
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        switch (ddlFiltro.SelectedValue)
        {
            case "tdc_numeroDocumento":
            case "tdc_contrato":
            case "tdc_numeroTarjeta":
                frvCondultarSolicitud.DataSource = nTDC.consultaSolicitudGeneral(ddlFiltro.SelectedValue, txtConsultaFiltro.Text);
                frvCondultarSolicitud.DataBind();
                break;
            case "Paso":
                gdvListaSolicitudes.DataSource = nTDC.consultaSolicitudGeneral(ddlFiltro.SelectedValue, txtConsultaFiltro.Text);
                gdvListaSolicitudes.DataBind();
                break;
        }
        
    }

    protected void btnDevolverPaso_Click(object sender, EventArgs e)
    {
        DropDownList ddlCambio = (DropDownList)frvCondultarSolicitud.FindControl("ddlCambioPaso");
        ddlCambio.Visible = true;
        Button btnDevuelve = (Button)frvCondultarSolicitud.FindControl("btnDevolver");
        btnDevuelve.Visible = true;

    }
    protected void btnDevolver_Click(object sender, EventArgs e)
    {

    }

    void ConsultarSolicitudes()
    {
        gdvListaSolicitudes.DataSource = nTDC.ConsultarDocumental();
        gdvListaSolicitudes.DataBind();
    }

}
