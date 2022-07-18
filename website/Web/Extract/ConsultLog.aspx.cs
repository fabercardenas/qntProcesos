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
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

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

    protected async void gdvLista_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        dvDatosEnvio.Visible = true;
        try
        {
            string idCampaign = gdvLista.DataKeys[e.NewSelectedIndex].Values[0].ToString();

            //joining together the json format string sample:"{"key":"valus"}";
            string requestMessage = "{\"data\":" +
                                    "{" +
                                        "\"count\":\"10\"," +
                                        "\"pag\":\"1\"," +
                                        "\"idcampaign\":\"" + idCampaign + "\"" +
                                    "}" +
                                  "}";

            HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");
            string endpoint = ConfigurationManager.AppSettings["inticoDominio"].ToString() + "colombia/ReportCampaign";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //request.Headers.Add("Content-Type", "application/json");
            request.Headers.Add("apikey", ConfigurationManager.AppSettings["inticoApikey"].ToString());
            request.Headers.Add("user", ConfigurationManager.AppSettings["inticoUser"].ToString());
            request.Content = content;

            HttpClient putClient = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //call endpoint async
            HttpResponseMessage response = await putClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string result = response.Content.ReadAsStringAsync().Result;
            dynamic details = JsonConvert.DeserializeObject(result);
            if (details.feedback.Count > 0)
            {
                dvIntInformacion.Visible = true;
                ltrIntMensaje.Text = "";
                ltrIntFechaCarga.Text = details.feedback[0].FECHA_CARGA.ToString();
                ltrIntEstado.Text = details.feedback[0].DESESTADO.ToString();
                ltrIntHoraEstado.Text = details.feedback[0].FECHORAESTADO.ToString();
                ltrIntHoraEstado.Text = details.feedback[0].FECHORAESTADO.ToString();
                ltrIntBrowser.Text = details.feedback[0].browser.ToString();
                ltrIntOs.Text = details.feedback[0].os.ToString();
                ltrIntUbicacion.Text = details.feedback[0].country.ToString() + details.feedback[0].location.ToString();
            }
            else
            {
                dvIntInformacion.Visible = false;
                ltrIntMensaje.Text = Messaging.ErrorSimple("Procesado sin informacion disponible");
            }
        }
        catch (Exception)
        {
            dvIntInformacion.Visible = false;
            ltrIntMensaje.Text = Messaging.ErrorSimple("No hay disponibilidad del servicio de intico");
            throw;
        }
        

    }
}
