using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Negocio
{
    public class NCygnus
    {
		public async Task<ContactoAcuerdoNuevoLista> SincronizarAcuerdosAsync(DateTime fechaHoy)
		{
			//solicitar token salesforce
			Negocio.NSalesforce salesforce = new NSalesforce();
			Dictionary<string, string> authData = salesforce.GetToken();

			Task<ContactoAcuerdoNuevoLista> resultadoServicio = SfAcuerdosNuevos(authData["AuthToken"], authData["ServiceUrl"], fechaHoy);
			ContactoAcuerdoNuevoLista listado = await resultadoServicio;

			return listado;
		}

		public static async Task<ContactoAcuerdoNuevoLista> SfAcuerdosNuevos(string AuthToken, string ServiceUrl, DateTime fechaHoy)
		{
			//joining together the json format string sample:"{"key":"valus"}";
			string requestMessage = "{\"fechaHoy\":\"" + string.Format("{0:yyyy-MM-dd}", fechaHoy) + "\"}";

			HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");

			////create url using package name in URL
			string endpoint = ServiceUrl + "/services/apexrest/CygnusAcuerdosNuevos";

			////create request message associated with POST verb
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint);

			////return JSON to the caller
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			////add token to header
			request.Headers.Add("Authorization", "Bearer " + AuthToken);

			////add content to HttpRequestMessage;
			request.Content = content;

			HttpClient putClient = new HttpClient();
			//call endpoint async
			HttpResponseMessage response = await putClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			string result = response.Content.ReadAsStringAsync().Result;
			ContactoAcuerdoNuevoLista contactoReturn = JsonConvert.DeserializeObject<ContactoAcuerdoNuevoLista>(result);

			return contactoReturn;
		}
	}
}
