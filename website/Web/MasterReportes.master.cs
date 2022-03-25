using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Web_MasterReportes : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = ConfigurationManager.AppSettings["nameSite"];
        //imgLogoOficina.ImageUrl = ConfigurationManager.AppSettings["RutaLogo"] + Session["of_logo"].ToString();
    }

    public Label _lblTituloReporte
    {
        get { return lblTitulo; }
        set { lblTitulo = value; }
    }

    public string CerrarVentanaMaestra()
    {
        string script = "window.close();";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar", script, true);
        return "";
    }
}
