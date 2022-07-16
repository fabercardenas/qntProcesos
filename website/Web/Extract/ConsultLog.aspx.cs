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

public partial class CosultLog : System.Web.UI.Page
{
    Datos.DBpmSrv extracto = new Datos.DBpmSrv();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"] != null) && (!Page.IsPostBack))
        {
            Literal ltrTituloModulo = (Literal)this.Master.FindControl("ltrTituloModulo");
            ltrTituloModulo.Text = "<span class='fa fa-file-pdf-o'></span> <b>Consultar Log de Envio de Extractos</b>";
            dvIdConsulta.Visible = true;
        }
    }
    protected void lnbConsultarDocumento_Click(object sender, EventArgs e)
    {
        gdvLista.DataSource = extracto.extConsultaXDocumento(txtNumDocumento.Text);
        gdvLista.DataBind();
    }

    protected void gdvLista_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var client = new RestClient(ConfigurationManager.AppSettings["dominio"]);
        dvDatosEnvio.Visible = true;
    }
}
