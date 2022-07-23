using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System.Text;
using System.Net.Http.Headers;
using Negocio;
using System.Threading.Tasks;
using System.Collections;

public partial class GetInfoRequest : System.Web.UI.Page
{
    #region ELIMINAR AL TERMINAR
    public const string LoginEndpoint = "https://test.salesforce.com/services/oauth2/token";
    public const string ApiEndpoint = "/services/data/v36.0/"; //Use your org's version number
    public static string ServiceUrl;
    public static string AuthToken;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"] != null) && (!Page.IsPostBack))
        {
            Literal ltrTituloModulo = (Literal)this.Master.FindControl("ltrTituloModulo");
            ltrTituloModulo.Text = "<span class='fa fa-codepen'></span> <b>Sincronización de Ubicaciones - Paso 3</b>";
        }

        #region APGO
        /*
        string query = "SELECT id, Contacto__r.ID_Cliente__c, Contacto__r.Direccin_Residencia__c from ContactLocation__c  where Contacto__r.Direccin_Residencia__c !=''";
        JObject objConsulta = JObject.Parse(QueryRecord(Client, query));

        if ((string)objConsulta["totalSize"] == "1")
        {
            // Only one record, use it
            string cedula = (string)objConsulta["records"][0]["Contacto__r.ID_Cliente__c"];
            string direccion = (string)objConsulta["records"][0]["Contacto__r.Direccin_Residencia__c"];
        }
        if ((string)obj["totalSize"] == "0")
        {
            // No record, create an Account
        }
        else
        {
            string cedula = (string)objConsulta["records"][0]["Contacto__r.ID_Cliente__c"];
            string direccion = (string)objConsulta["records"][0]["Contacto__r.Direccin_Residencia__c"];
            // Multiple records, either filter further to determine correct Account or choose the first result
        }
        */

        /*
        string endpoint = "/services/apexrest/GestionSateliteHistorico";

        string restQuery = ServiceUrl + endpoint;

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, restQuery);
        request.Headers.Add("Authorization", "Bearer " + AuthToken);
        request.Method = HttpMethod.Post;
        var bodyEnable = new Negocio.geoUserUpdateGroup()
        {
            Identifier = "",
            GroupIdentifier = "",
            Custom1 = "",
            Custom2 = "",
            Custom3 = "@"
        };
        var serializedBodyEnable = JsonConvert.SerializeObject(bodyEnable);
        //request.Content = serializedBodyEnable;

        HttpResponseMessage finalResponse = Client.SendAsync(request).Result;
        string algo = finalResponse.Content.ReadAsStringAsync().Result;
        */
        #endregion
    }

    protected async void lnbCargar_Click(object sender, EventArgs e)
    {
        //buscar las cedulas que esten sin actualizar
        Negocio.NTDC nTDC = new NTDC();
        Dictionary<string, string> resultado = await nTDC.Sincronizar(Session["ID_usuario"].ToString());
        ltrMensaje.Text = "";
        foreach (var mensaje in resultado)
        {
            //storedProcCommand.Parameters.AddWithValue(mensaje.Key.ToString(), parametro.Value);
            if (mensaje.Key.ToString()=="warning")
                ltrMensaje.Text += NMessaging.Warning(mensaje.Value);

            if (mensaje.Key.ToString() == "error")
                ltrMensaje.Text += NMessaging.Error(mensaje.Value);

            //if (resultado.ContainsKey("success"))
            if (mensaje.Key.ToString() == "success")
                ltrMensaje.Text += NMessaging.Success(mensaje.Value);
        }

    }

    #region PARA ELIMINAR AL TERMINAR // TIENE EL QUERYRECORD
    private void GetToken()
    {
        HttpClient Client = new HttpClient();
        HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            {"grant_type", "password"},
            {"client_id", "3MVG9rrOVHA54N6PJLV78rRcYv.rBxNgKHPHKiLvb6pWsS5A4KJ9uK72AAQ0wyivNuBG3s8VeOdmKyRtpp_nq"},
            {"client_secret", "F239BA4172C95CD901899D84ED9C52685631ADF73805C96718CC03BD0FCC248A"},
            {"username", "agomez@qnt.com.co.uat"},
            {"password", "1qazse4rfvgy7axzvfGgGLxj13rGXBLn3LnlaX"}
        });
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
        HttpResponseMessage message = Client.PostAsync(LoginEndpoint, content).Result;

        string response = message.Content.ReadAsStringAsync().Result;
        JObject obj = JObject.Parse(response);

        AuthToken = (string)obj["access_token"];
        ServiceUrl = (string)obj["instance_url"];
    }

    private string QueryRecord(HttpClient client, string queryMessage)
    {
        string restQuery = ServiceUrl + ApiEndpoint+ "query?q=" + queryMessage;

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, restQuery);
        request.Headers.Add("Authorization", "Bearer " + AuthToken);

        //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = client.SendAsync(request).Result;
        return response.Content.ReadAsStringAsync().Result;
    }

    private void GetData()
    {
        string endpoint = ServiceUrl + "/services/apexrest/ProcesosDatosBasicos";
        HttpClient Client = new HttpClient();
        string requestMessage = "{\"contactos\":[\"1112226613\"]}";

        HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");
        HttpResponseMessage message = Client.PostAsync(endpoint, content).Result;

        string response = message.Content.ReadAsStringAsync().Result;
        JObject obj = JObject.Parse(response);
    }

    private void GetInfo(HttpClient client)
    {
        string endpoint = "/services/apexrest/ProcesosDatosBasicos";
        string serviceUrl = ServiceUrl + endpoint;

        //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, serviceUrl);
        //request.Headers.Add("Authorization", "Bearer " + AuthToken);
        //request.Method = HttpMethod.Post;
        //List<Negocio.InfoBasico> empleadosEnviar = new List<Negocio.InfoBasico> { };

        //var bodyEnable = new Negocio.InfoBasico()
        //{
        //    ID_Cliente__c = "1112226613"
        //};
        //empleadosEnviar.Add(bodyEnable);
        //var serializedBody = JsonConvert.SerializeObject(empleadosEnviar, Formatting.Indented);
        ////request.AddParameter("application/json", serializedBody, ParameterType.RequestBody);



        //var serializedBodyEnable = JsonConvert.SerializeObject(empleadosEnviar);
        ////request.Content = serializedBodyEnable;

        //HttpResponseMessage finalResponse = client.SendAsync(request).Result;
        //string algo = finalResponse.Content.ReadAsStringAsync().Result;

    }

    private void GetInfo2()
    {
        string endpoint = "/services/apexrest/ProcesosDatosBasicos";
        var client = new RestClient();
        var request = new RestRequest(ServiceUrl + endpoint, Method.Post);
        request.AddHeader("Content-type", "application/json");
        request.AddHeader("Authorization", "Bearer " + AuthToken);

        List<String> empleadosEnviar = new List<String> { };
        
        empleadosEnviar.Add("1112226613");
        
        //var response = client.ExecuteAsync(request);
        //serializedBody = "contactos:" + serializedBody;
        //request.AddParameter("application/json", serializedBody, ParameterType.RequestBody);
        
        //string bodyFaber = "'contactos': ['1112226613', '1112226613']";
        string bodyFaber = "{\"contactos\":[\"1112226613\"]}";
        request.AddParameter("application/json", bodyFaber, ParameterType.RequestBody);
        request.Method = Method.Post;
        RestResponse finalResponse = client.ExecuteAsync(request).Result;
        
    }
    #endregion
}