using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using RestSharp;
using System.Net;

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
        dvDatosEnvio.Visible = true;
        string idCampaign = gdvLista.DataKeys[e.NewSelectedIndex].Values[0].ToString();
        var client = new RestClient(ConfigurationManager.AppSettings["inticoDominio"]);
        Dictionary<string, string> headers = new Dictionary<string, string>();

        var request = new RestRequest("colombia/ReportCampaign", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("apikey", ConfigurationManager.AppSettings["inticoApikey"]);
        request.AddHeader("user", ConfigurationManager.AppSettings["inticoUser"]);
        string bodyJson = "{\"data\":" +
                                "{" +
                                    "\"count\":\"10\"," +
                                    "\"pag\":\"1\"," +
                                    "\"idcampaign\":\"" + idCampaign + "\"" +
                                "}" +
                              "}";

        request.AddParameter("application/json", bodyJson, ParameterType.RequestBody);
        var response = client.Execute(request);

        if (response.StatusCode == HttpStatusCode.OK) 
        { 
            //pintar los datos
        }
    }
}
