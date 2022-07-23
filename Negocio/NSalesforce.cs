using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Collections.Specialized;

namespace Negocio
{
    class NSalesforce
    {

        public Dictionary<string, string> GetToken()
        {
            HttpClient Client = new HttpClient();
            string client_id, client_secret, username, password, endPoint;
            if (ConfigurationManager.AppSettings["typeApp"].ToString() == "web")
            {
                client_id = ConfigurationManager.AppSettings["sfClientId"].ToString();
                client_secret = ConfigurationManager.AppSettings["sfSecret"].ToString();
                username = ConfigurationManager.AppSettings["sfUser"].ToString();
                password = ConfigurationManager.AppSettings["sfPass"].ToString();
                endPoint = ConfigurationManager.AppSettings["sfEndpoint"].ToString();
            }
            else
            {
                //PARA LOS WINFORMS ENVIO A SF DE PRODUCCION
                client_id = "3MVG9vrJTfRxlfl58q_V_FAMCZhERWpy_AeDkPkhUXkFV0cAOJBx3SndBPIBkkFSAd36KTFQjzpLyWXTQ13kA";
                client_secret = "C438908D938283A4EF1D5994C8B8745359AF0FF286FF6B51F39529FA84D8A642";
                username = "agomez@qnt.com.co";
                password = "1qazse4rfvgy7FpfbsEfKtKkdylwM8UkYuTyi";
                endPoint = "https://login.salesforce.com/services/oauth2/token";
            }

            HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"client_id", client_id},
                {"client_secret", client_secret},
                {"username", username},
                {"password", password}
            });
            string LoginEndpoint = endPoint;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            HttpResponseMessage message = Client.PostAsync(LoginEndpoint, content).Result;

            string response = message.Content.ReadAsStringAsync().Result;
            JObject obj = JObject.Parse(response);

            return new Dictionary<string, string> {
                {"AuthToken" , (string)obj["access_token"] },
                {"ServiceUrl" , (string)obj["instance_url"] }
            };
        }

        public string QueryRecord(string queryMessage)
        {
            Dictionary<string, string> authData = GetToken();

            string restQuery = authData["ServiceUrl"] + "/services/data/v36.0/" + "query?q=" + queryMessage;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, restQuery);
            request.Headers.Add("Authorization", "Bearer " + authData["AuthToken"]);

            //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.SendAsync(request).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
