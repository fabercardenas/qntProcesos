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
    public class NPagos
    {
        private readonly Datos.DPagos dPagos = new Datos.DPagos();

        #region   INSERTAR CARGUE 
        
		public DataTable InsertarCargue(string car_codigoCargue, string ID_usuarioRegistroFK, string car_archivoCargue,
             string car_numeroDocumento, string car_valorPago, string car_fechaPago, string car_horaPago, string car_identificadorUnico, string car_canal, string car_IDacuerdo)
        {
            return dPagos.InsertarCargue(car_codigoCargue, ID_usuarioRegistroFK, car_archivoCargue, car_numeroDocumento, car_valorPago, car_fechaPago, car_horaPago, car_identificadorUnico, car_canal, car_IDacuerdo);
        }

		#endregion

		#region   PROCESAR CARGUE 

		public async Task<Dictionary<string, string>> Sincronizar(string ID_usuarioSincronizaDatosFK)
		{
			DataTable tbDatos = dPagos.PorSincronizar();
			Dictionary<string, string> response = new Dictionary<string, string>();
			if (tbDatos.Rows.Count > 0)
			{
				//organizar el array de strings, de cedulas para consultar
				string documentos = "";
				for (int i = 0; i < tbDatos.Rows.Count; i++)
				{
					documentos += "\"" + tbDatos.Rows[i]["tdc_numeroDocumento"] + "\",";
				}

				//consumir servicio y actualizar datos de clientes
				//return SincronizarAsync(documentos.Substring(0, documentos.Length - 1));
				return await SincronizarAsync(documentos.Substring(0, documentos.Length - 1), ID_usuarioSincronizaDatosFK);
			}
			else
			{
				response.Add("warning", "No hay registros por procesar");
			}
			return response;
		}

		public static async Task<Dictionary<string, string>> SincronizarAsync(string documentos, string ID_usuarioSincronizaDatosFK)
		{
			//solicitar token salesforce
			Negocio.NSalesforce salesforce = new NSalesforce();
			Dictionary<string, string> authData = salesforce.GetToken();

			Task<ContactoLista> resultadoServicio = SfExtraerDatos(authData["AuthToken"], authData["ServiceUrl"], documentos);

			ContactoLista listado = await resultadoServicio;
			Dictionary<string, string> response = new Dictionary<string, string>();

			//validar la longitud de listado
			if ((listado != null) && (listado.contactos.Count > 0))
			{
				Datos.DTDC elTDC = new Datos.DTDC();
				Int32 totalProcesados = 0;
				Int32 totalFallidos = 0;
				foreach (var item in listado.contactos)
				{
					Console.WriteLine("id: " + item.Id + ", Direccion: " + item.Direccin_Residencia__c);
					//actualizar cada cliente con los datos de ubicacion

					//si no tiene direccion y/o no tienen ciudad no actualiza
					if ((item.Direccin_Residencia__c != null) && (item.Direccin_Residencia__c.ToString() != "") && (item.Ciudad_residencia__c.ToString() != null) && (item.Ciudad_residencia__c.ToString() != ""))
					{
						elTDC.ActualizaDatosUbicacion(item.ID_Cliente__c, item.Ciudad_residencia__c.ToString(), item.Direccin_Residencia__c.ToString(),
						item.UpCorreo__c.ToString(), item.UpTelefonoFijo__c.ToString(), item.UpCelular__c.ToString(), ID_usuarioSincronizaDatosFK);
						totalProcesados++;
					}
					else
						totalFallidos++;
				}
				if (totalProcesados > 0)
					response.Add("success", "Registros actualizados: " + totalProcesados.ToString());
				if (totalFallidos > 0)
					response.Add("error", "Registros sin dirección : " + totalFallidos.ToString());
				return response;
			}
			else
			{
				response.Add("error", "No se encontraron coinciencias en salesforce");
				return response;
			}
		}

		public static async Task<ContactoLista> SfExtraerDatos(string AuthToken, string ServiceUrl, string documentos)
		{
			//joining together the json format string sample:"{"key":"valus"}";
			string requestMessage = "{\"contactos\":[" + documentos + "]}";
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
			HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");
			////create url using package name in URL
			string endpoint = ServiceUrl + "/services/apexrest/BPMDatosBasicos";

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
			ContactoLista contactoReturn = JsonConvert.DeserializeObject<ContactoLista>(result);

			return contactoReturn;
		}

		#endregion
	}
}
