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
            Negocio.NUtilidades.cargarDDLReferenciaXModulo(ddlCanales, "Canales", true);
            Negocio.NUtilidades.cargarDDLReferenciaXModulo(ddlProcesos, "Procesos", true);
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ConsultarSolicitudes();
    }

    void ConsultarSolicitudes()
    {
        gdvListaSolicitudes.DataSource = nTDC.Consultar(ddlCanales.SelectedValue, ddlProcesos.SelectedValue);
        gdvListaSolicitudes.DataBind();
    }
}
