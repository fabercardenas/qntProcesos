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

public partial class TDC_SetEnlistment: System.Web.UI.Page
{
    Negocio.NTDC nTDC = new Negocio.NTDC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"]!=null) && (!Page.IsPostBack))
        {
            ConsultarSolicitudesXestado();
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        
    }

    void ConsultarSolicitudesXestado()
    {
        gdvListaSolicitudes.DataSource = nTDC.ConsultarXestado(3);
        gdvListaSolicitudes.DataBind();
    }
}
