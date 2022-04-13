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
    public class NTDC 
    {
        private readonly Datos.DTDC dTDC = new Datos.DTDC();

		#region   PASO 1 
		public DataTable Insertar(string tdc_tipoDocumento, string tdc_numeroDocumento, string tdc_canal,
			 string tdc_proceso, string ID_usuarioRegistraFK)
		{
			return dTDC.Insertar(tdc_tipoDocumento, tdc_numeroDocumento, tdc_canal, tdc_proceso, ID_usuarioRegistraFK);
		}

		public DataTable Consultar(string tdc_canal, string tdc_proceso)
		{
			return dTDC.Consultar(tdc_canal, tdc_proceso);
		}

		#endregion

		#region PASO 2
		public Dictionary<string, string> Sincronizar()
		{
			DataTable tbDatos = dTDC.PorSincronizar();
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
				SincronizarAsync(documentos.Substring(0, documentos.Length - 1));
			}
			else
			{
				response.Add("warning", "No hay registros por procesar");				
			}
			return response;
		}

		public static async void SincronizarAsync(string documentos)
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

					//si no tiene direccion no actualiza
					if ((item.Direccin_Residencia__c != null) && (item.Direccin_Residencia__c.ToString() != ""))
					{
						elTDC.ActualizaDatosUbicacion(item.ID_Cliente__c, item.Ciudad_residencia__c.ToString(), item.Direccin_Residencia__c.ToString(),
						item.UpCorreo__c.ToString(), item.UpTelefonoFijo__c.ToString(), item.UpCelular__c.ToString(), "1");
						totalProcesados++;
					}
					else
						totalFallidos++;
				}
				response.Add("success", "Registros actualizados: " + totalProcesados.ToString() + ". Registros sin dirección : " + totalFallidos.ToString());
				//return response;
			}
			else
			{
				response.Add("error", "No se encontraron coinciencias en salesforce");
				//return response;
			}
		}

		public static async Task<ContactoLista> SfExtraerDatos(string AuthToken, string ServiceUrl, string documentos)
		{
			//joining together the json format string sample:"{"key":"valus"}";
			string requestMessage = "{\"contactos\":[" + documentos +  "]}";

			HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");

			////create url using package name in URL
			string endpoint = ServiceUrl + "/services/apexrest/ProcesosDatosBasicos";

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

		#region PASO 3
		public DataTable InsertarInfo(string ID_tdcSolicitudFK, string tdc_numeroTarjeta,
			 string tdc_tipoProducto, string tdc_fechaRealce, string tdc_fechaActivacion, string tdc_Valor,
			 string tdc_estado, string tdc_contrato, string tdc_nombre, string tdc_copiaNumeroTarjeta, string ID_usuarioRegistraFK)
		{
			return dTDC.InsertarInfo(ID_tdcSolicitudFK, tdc_numeroTarjeta,tdc_tipoProducto, tdc_fechaRealce, tdc_fechaActivacion, tdc_Valor,
			 tdc_estado, tdc_contrato, tdc_nombre, tdc_copiaNumeroTarjeta, ID_usuarioRegistraFK);
		}
		#endregion
	}
}
