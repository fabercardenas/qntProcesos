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

public partial class TDC_ValidateDocuments : System.Web.UI.Page
{
    Negocio.NTDC nTDC = new Negocio.NTDC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"] != null) && (!Page.IsPostBack))
        {
            dvIdConsulta.Visible = true;
            ConsultarDocumental();
        }
    }


    protected void lnbConsultarDocumento_Click(object sender, EventArgs e)
    {
        ConsultarSolicitudesXdocumento();
        lnbGenerarSMS.Visible = false;
    }

    void ConsultarDocumental()
    {
        gdvListaSolicitudes.DataSource = nTDC.ConsultarDocumental();
        gdvListaSolicitudes.DataBind();
        if (gdvListaSolicitudes.Rows.Count > 0)
            lnbGenerarSMS.Visible = true;
        else
            lnbGenerarSMS.Visible = false;
    }

    public string TieneFlujoDigital(Boolean tdc_flujoDigital)
    {
        string retornar = "";
        if (tdc_flujoDigital)
            retornar = "<span class='glyphicon glyphicon-ok'></span>";
        return retornar;
    }

    public string TienePagare(Boolean tdc_docPagare)
    {
        string retornar = "";
        if (tdc_docPagare)
            retornar = "<span class='glyphicon glyphicon-ok'></span>";
        return retornar;
    }

    void ConsultarSolicitudesXdocumento()
    {
        gdvListaSolicitudes.DataSource = nTDC.consultaSolicitudXDocumento(txtNumDocumento.Text);
        gdvListaSolicitudes.DataBind();
    }

    protected void lnbGenerarSMS_Click(object sender, EventArgs e)
    {
        string popupScript = "<script language='JavaScript'>" +
                             "window.open('FileSMS.aspx','SMS', 'width=400, height=80,location=no, menubar=no,status=no,scrollbars=no,target=blank')</script>";
        ClientScript.RegisterStartupScript(this.GetType(), "SMS", popupScript);
    }

    protected void gdvListaSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Boolean docIngresos = (Boolean)gdvListaSolicitudes.DataKeys[e.Row.DataItemIndex].Values[1];
            LinkButton buttonSopIngresos = (LinkButton)e.Row.Cells[6].FindControl("lbtSopIngresos");

            Boolean docIdentidad = (Boolean)gdvListaSolicitudes.DataKeys[e.Row.DataItemIndex].Values[2];
            LinkButton buttonDocIdentidad = (LinkButton)e.Row.Cells[5].FindControl("lbtDocIdentidad");

            if (buttonSopIngresos != null)
            {
                if (docIngresos)
                {
                    buttonSopIngresos.Text = "<span class='glyphicon glyphicon-ok'></span>";
                    buttonSopIngresos.Enabled = false;
                }
                else
                {
                    buttonSopIngresos.Text = "<span class='btn btn-xs btn-success'>Recibido</span>";
                }
            }

            if (buttonDocIdentidad != null)
            {
                if (docIdentidad)
                {
                    buttonDocIdentidad.Text = "<span class='glyphicon glyphicon-ok'></span>";
                    buttonDocIdentidad.Enabled = false;
                }
                else
                {
                    buttonDocIdentidad.Text = "<span class='btn btn-xs btn-success'>Recibido</span>";
                }
            }
        }
    }

    protected void gdvListaSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SopIngresosRecibir")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ltrCliDocumento = gdvListaSolicitudes.DataKeys[index].Values[0].ToString();
            nTDC.actualizaDocIngresosXDocumento(ltrCliDocumento, Session["ID_usuario"].ToString());
            ltrMensaje.Text = Messaging.Success("Soporte de Ingresos para el Documento " + ltrCliDocumento + " actualizado con éxito");
            ConsultarDocumental();
        }

        if (e.CommandName == "DocIdentidadRecibir")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ltrCliDocumento = gdvListaSolicitudes.DataKeys[index].Values[0].ToString();
            nTDC.actualizaDocIdentidadXDocumento(ltrCliDocumento, Session["ID_usuario"].ToString());
            ltrMensaje.Text = Messaging.Success("Documento de Identidad para el Documento " + ltrCliDocumento + " actualizado con éxito");
            ConsultarDocumental();
        }
    }
}
