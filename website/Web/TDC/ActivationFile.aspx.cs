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

public partial class TDC_ActivationFile : System.Web.UI.Page
{
    Negocio.NTDC nTDC = new Negocio.NTDC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"]!=null) && (!Page.IsPostBack))
        {
            Literal ltrTituloModulo = (Literal)this.Master.FindControl("ltrTituloModulo");
            ltrTituloModulo.Text = "<span class='fa fa-credit-card'></span> <b>Tarjetas para Activación - Paso 8.2</b>";
            dvIdConsulta.Visible = true;
            ConsultarSolicitudesXestado();
        }
    }

    void ConsultarSolicitudesXestado()
    {
        gdvListaSolicitudes.DataSource = nTDC.ConsultarXestado(8);
        gdvListaSolicitudes.DataBind();
    }

    #region GENERACIÓN ARCHIVO DE ACTIVACIÓN
    protected void lnbBloqueo_Click(object sender, EventArgs e)
    {
        consultaSolicitudXDiasMora(nTDC.consultaSolicitudXDiasMora());
    }

    protected void consultaSolicitudXDiasMora(DataTable tbl)
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
            Response.AddHeader("Content-Disposition", "attachment; filename= ActivacionTarjetas" + ltrFechaPrevalidacion.Text + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            ltrMensaje.Text = Messaging.Warning("No hay Tarjetas de Crédito por Activar, valide los días mora o el paso de las solicitudes");
    }
    #endregion
    
}
