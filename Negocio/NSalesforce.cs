using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Negocio
{
    class NSalesforce
    {

        private Dictionary<string, string> GetToken(string LoginEndpoint)
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

            return new Dictionary<string, string> {
                {"AuthToken" , (string)obj["access_token"] },
                {"ServiceUrl" , (string)obj["instance_url"] }
            };
        }
    }
}
