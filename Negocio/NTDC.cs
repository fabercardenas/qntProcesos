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
			 string tdc_proceso, string ID_usuarioRegistraFK, string tdc_archivoCargaP1, string tdc_fechaVenta)
		{
			return dTDC.Insertar(tdc_tipoDocumento, tdc_numeroDocumento, tdc_canal, tdc_proceso, ID_usuarioRegistraFK, tdc_archivoCargaP1, tdc_fechaVenta);
		}

		public void SincronizarNombres(string documentos)
		{
			SincronizarNombresAsync(documentos);
		}

		public async void SincronizarNombresAsync(string documentos)
		{
			//solicitar token salesforce
			Negocio.NSalesforce salesforce = new NSalesforce();
			Dictionary<string, string> authData = salesforce.GetToken();

			Task<ContactoNombresLista> resultadoServicio = SfExtraerNombres(authData["AuthToken"], authData["ServiceUrl"], documentos);

			ContactoNombresLista listado = await resultadoServicio;
			Dictionary<string, string> response = new Dictionary<string, string>();

			//validar la longitud de listado
			if ((listado != null) && (listado.Contactos.Count > 0))
			{
				Datos.DTDC elTDC = new Datos.DTDC();
				Int32 totalProcesados = 0;
				Int32 totalFallidos = 0;
				foreach (var item in listado.Contactos)
				{
					//actualizar nombres de clientes
					if ((item.FirstName != null) && (item.LastName != null))
					{
						string[] nombres = item.FirstName.Split(' ');
						string nombre2 = "";
						if (nombres.Length > 1)
							nombre2 = nombres[1];
						else
							nombre2 = item.MiddleName ?? "";

						string[] apellidos = item.LastName.Split(' ');
						string apellido2 = "";
						if (apellidos.Length > 1)
							apellido2 = apellidos[1];

						elTDC.ActualizaNombres(item.ID_Cliente__c, nombres[0], nombre2, apellidos[0], apellido2, item.Valor_Cupo__c);
						totalProcesados++;
					}
					else
						totalFallidos++;
				}
				response.Add("success", "Registros actualizados: " + totalProcesados.ToString() + ". Registros fallidos : " + totalFallidos.ToString());
				//return response;
			}
			else
			{
				response.Add("error", "No se encontraron coinciencias en salesforce");
				//return response;
			}
		}

		public async Task<ContactoNombresLista> SfExtraerNombres(string AuthToken, string ServiceUrl, string documentos)
		{
			//joining together the json format string sample:"{"key":"valus"}";
			string requestMessage = "{\"contactos\":[" + documentos + "]}";

			HttpContent content = new StringContent(requestMessage, Encoding.UTF8, "application/json");

			//SERVICIO DE CONSULTA SALESFORCE - PARA NOMBRES VA POR PUT
			string endpoint = ServiceUrl + "/services/apexrest/BPMDatosBasicos";

			////create request message associated with POST verb
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, endpoint);

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
			ContactoNombresLista contactoReturn = JsonConvert.DeserializeObject<ContactoNombresLista>(result);

			return contactoReturn;
		}

		public DataTable Consultar(string tdc_canal, string tdc_proceso)
		{
			return dTDC.Consultar(tdc_canal, tdc_proceso);
		}

		public DataTable consultaSolicitudXDocumento(string tdc_numeroDocumento)
		{
			return dTDC.consultaSolicitudXDocumento(tdc_numeroDocumento);
		}

		

		public DataTable consultaSolicitudXArchivo(string tdc_archivoCargaP1)
		{
			return dTDC.consultaSolicitudXArchivo(tdc_archivoCargaP1);
		}

		#endregion

		#region PASO 2
		public DataTable InsertarInfo(string tdc_IdColocacion, string tdc_fechaRealce, string tdc_estado, string tdc_contrato, string tdc_nombre, string ID_usuarioRegistraFK, string tdc_archivoCargaP2, string tdc_numeroTarjeta, string tdc_numeroDocumentoFK)
		{
			return dTDC.InsertarInfo(tdc_IdColocacion, tdc_fechaRealce, tdc_estado, tdc_contrato, tdc_nombre, ID_usuarioRegistraFK, tdc_archivoCargaP2, tdc_numeroTarjeta, tdc_numeroDocumentoFK);
		}
		#endregion

		#region PASO 3
		public async Task<Dictionary<string, string>> Sincronizar(string ID_usuarioSincronizaDatosFK)
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
				if(totalProcesados>0)
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
			string requestMessage = "{\"contactos\":[" + documentos +  "]}";

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

		#region PASO 4

		public DataTable ConsultarXestado(Int16 tdc_paso)
		{
			return dTDC.ConsultarXestado(tdc_paso);
		}

		public DataTable ConsultarXtarjeta(string tdc_numeroTarjeta)
		{
			return dTDC.ConsultarXtarjeta(tdc_numeroTarjeta);
		}

		public DataTable actualizaSolicitudXDocumento(string tdc_numeroDocumento, string ID_usuarioPrevalidacionFK)
		{
			return dTDC.actualizaSolicitudXDocumento(tdc_numeroDocumento, ID_usuarioPrevalidacionFK);
		}

		public DataTable consultaSolicitudXfechaPrevalidacion(string tdc_fechaPrevalidacion)
		{
			return dTDC.consultaSolicitudXfechaPrevalidacion(tdc_fechaPrevalidacion);
		}

		public DataTable consultaSolicitudXfPreValiStikcer(string tdc_fechaPrevalidacion)
		{
			return dTDC.consultaSolicitudXfPreValiStikcer(tdc_fechaPrevalidacion);
		}

		#endregion

		#region PASO 5
		public DataTable ConsultarXestadoVal(Int16 tdc_paso)
		{
			return dTDC.ConsultarXestadoVal(tdc_paso);
		}

		public DataTable ConsultarXtarjetaVal(string tdc_numeroTarjeta)
		{
			return dTDC.ConsultarXtarjetaVal(tdc_numeroTarjeta);
		}

		public DataTable consultaSolicitudXfechaValidacion(string tdc_fechaValidacion)
		{
			return dTDC.consultaSolicitudXfechaValidacion(tdc_fechaValidacion);
		}

		public DataTable actualizaSolicitudValXDocumento(string tdc_numeroDocumento, string ID_usuarioValidacionFK)
		{
			return dTDC.actualizaSolicitudValXDocumento(tdc_numeroDocumento, ID_usuarioValidacionFK);
		}

		#endregion

		#region PASO 6

		public DataTable actualizaInfoXDocumento(string tdc_numeroTarjeta, string tdc_numeroDocumento, string tdc_fechaEntrega, string tdc_numeroGuia, string ID_usuarioEntregaFK, string tdc_archivoEntregaP6)
		{
			return dTDC.actualizaInfoXDocumento(tdc_numeroTarjeta, tdc_numeroDocumento, tdc_fechaEntrega, tdc_numeroGuia, ID_usuarioEntregaFK, tdc_archivoEntregaP6);
		}

		public DataTable ConsultarXtarjetaEntrega(string tdc_numeroTarjeta)
		{
			return dTDC.ConsultarXtarjetaEntrega(tdc_numeroTarjeta);
		}

		public DataTable ConsultarXestadoEntrega(Int16 tdc_paso)
		{
			return dTDC.ConsultarXestadoEntrega(tdc_paso);
		}

		#endregion

		#region DOCUMENTAL TDC
		public async Task<Dictionary<string, string>> SincronizarFD(string ID_usuarioSincronizaDatosFK)
		{
			DataTable tbDatos = dTDC.PorSincronizarFD();
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

		#endregion

	}
}
