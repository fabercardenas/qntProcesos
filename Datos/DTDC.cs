using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace Datos
{
	public class DTDC : DSQL
	{

		#region   PASO 1 
		public DataTable Insertar(string tdc_tipoDocumento, string tdc_numeroDocumento, string tdc_canal, 
			 string tdc_proceso, string ID_usuarioRegistraFK, string tdc_archivoCargaP1, string tdc_fechaVenta)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_tipoDocumento", tdc_tipoDocumento);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@tdc_canal", tdc_canal);
			param.Add("@tdc_proceso", tdc_proceso);
			param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
			param.Add("@tdc_archivoCargaP1", tdc_archivoCargaP1);
			param.Add("@tdc_fechaVenta", tdc_fechaVenta);

            return procedureTable("tdcInsertar", true, param);
		}

		public DataTable ActualizaNombres(string tdc_numeroDocumento, string tdc_nombre1,string tdc_nombre2, 
			string tdc_apellido1, string tdc_apellido2, int tdc_cupoAsignado)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@tdc_nombre1", tdc_nombre1);
			param.Add("@tdc_nombre2", tdc_nombre2);
			param.Add("@tdc_apellido1", tdc_apellido1);
			param.Add("@tdc_apellido2", tdc_apellido2);
			param.Add("@tdc_cupoAsignado", tdc_cupoAsignado);
			
			return procedureTable("tdcActualizaNombres", true, param);
		}

		public DataTable consultaSolicitudXDocumento(string tdc_numeroDocumento)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);

			return procedureTable("tdcConsultarSolicXdocumento", true, param);
		}

		public DataTable consultaSolicitudXArchivo(string tdc_archivoCargaP1)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_archivoCargaP1", tdc_archivoCargaP1);

			return procedureTable("tdcConsultarSolicXarchivo", true, param);
		}

		#endregion

		#region PASO 2

		public DataTable InsertarInfo(string tdc_IdColocacion, string tdc_fechaRealce, string tdc_estado, string tdc_contrato, string tdc_nombre, string ID_usuarioRegistraFK, string tdc_archivoCargaP2, string tdc_numeroTarjeta, string tdc_numeroDocumentoFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_IdColocacion", tdc_IdColocacion);
			param.Add("@tdc_fechaRealce", tdc_fechaRealce);
			param.Add("@tdc_estado", tdc_estado);
			param.Add("@tdc_contrato", tdc_contrato);
			param.Add("@tdc_nombre", tdc_nombre);
			param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
			param.Add("@tdc_archivoCargaP2", tdc_archivoCargaP2);
			param.Add("@tdc_numeroTarjeta", tdc_numeroTarjeta);
			param.Add("@tdc_numeroDocumentoFK", tdc_numeroDocumentoFK);

			return procedureTable("tdcInsertarInfo", true, param);
		}
		#endregion

		#region PASO 3
		public DataTable Consultar(string tdc_canal, string tdc_proceso)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_canal", tdc_canal);
			param.Add("@tdc_proceso", tdc_proceso);

			return procedureTable("tdcConsultar", true, param);
		}

		public DataTable ConsultarSinDireccion(string tdc_canal, string tdc_proceso)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_canal", tdc_canal);
			param.Add("@tdc_proceso", tdc_proceso);

			return procedureTable("tdcConsultar", true, param);
		}

		public DataTable PorSincronizar()
		{
			return procedureTable("tdcPorSincronizar", false);
		}

		public DataTable ActualizaDatosUbicacion(string tdc_numeroDocumento, string tdc_ciudad, string tdc_direccion, string tdc_correo,
												string tdc_telefono, string tdc_celular, string ID_usuarioSincronizaDatosFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@tdc_ciudad", tdc_ciudad);
			param.Add("@tdc_direccion", tdc_direccion);
			param.Add("@tdc_correo", tdc_correo);
			param.Add("@tdc_telefono", tdc_telefono);
			param.Add("@tdc_celular", tdc_celular);
			param.Add("@ID_usuarioSincronizaDatosFK", ID_usuarioSincronizaDatosFK);

			return procedureTable("tdcActualizaDatosUbicacion", true, param);
		}
		#endregion

		#region PASO 4

		public DataTable ConsultarXestado(Int16 tdc_paso)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_paso", tdc_paso);

			return procedureTable("tdcConsultarXestado", true, param);
		}

		public DataTable ConsultarXtarjeta(string tdc_numeroTarjeta)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroTarjeta", tdc_numeroTarjeta);

			return procedureTable("tdcConsultarXtarjeta", true, param);
		}

		public DataTable actualizaSolicitudXDocumento(string tdc_numeroDocumento, string ID_usuarioPrevalidacionFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@ID_usuarioPrevalidacionFK", ID_usuarioPrevalidacionFK);

			return procedureTable("tdcActualizaSolicXdocumento", true, param);
		}

		public DataTable consultaSolicitudXfechaPrevalidacion(string tdc_fechaPrevalidacion)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_fechaPrevalidacion", tdc_fechaPrevalidacion);

			return procedureTable("tdcConsultarSolicXfechaPrevalidacion", true, param);
		}

		public DataTable consultaSolicitudXfPreValiStikcer(string tdc_fechaPrevalidacion)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_fechaPrevalidacion", tdc_fechaPrevalidacion);

			return procedureTable("tdcConsultarSolicXfPreValiStikcer", true, param);
		}

		public DataTable consultaSolicitudXacuseRecibido(string tdc_fechaPrevalidacion)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_fechaPrevalidacion", tdc_fechaPrevalidacion);

			return procedureTable("tdcConsultarSolicXacuseRecibido", true, param);
		}

		#endregion

		#region PASO 5

		public DataTable ConsultarXestadoVal(Int16 tdc_paso)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_paso", tdc_paso);

			return procedureTable("tdcConsultarXestadoVal", true, param);
		}

		public DataTable ConsultarXtarjetaVal(string tdc_numeroTarjeta)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroTarjeta", tdc_numeroTarjeta);

			return procedureTable("tdcConsultarXtarjetaVal", true, param);
		}

		public DataTable consultaSolicitudXfechaValidacion(string tdc_fechaValidacion)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_fechaValidacion", tdc_fechaValidacion);

			return procedureTable("tdcConsultarSolicXfechaValidacion", true, param);
		}

		public DataTable actualizaSolicitudValXDocumento(string tdc_numeroDocumento, string ID_usuarioValidacionFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@ID_usuarioValidacionFK", ID_usuarioValidacionFK);

			return procedureTable("tdcActualizaSolicValXdocumento", true, param);
		}

		#endregion

		#region PASO 6

		public DataTable actualizaInfoXDocumento(string tdc_numeroTarjeta, string tdc_numeroDocumento, string tdc_fechaEntrega, string tdc_numeroGuia, string ID_usuarioEntregaFK, string tdc_archivoEntregaP6)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroTarjeta", tdc_numeroTarjeta);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@tdc_fechaEntrega", tdc_fechaEntrega);
			param.Add("@tdc_numeroGuia", tdc_numeroGuia);
			param.Add("@ID_usuarioEntregaFK", ID_usuarioEntregaFK);
			param.Add("@tdc_archivoEntregaP6", tdc_archivoEntregaP6);

			return procedureTable("tdcActualizaInfoXdocumento", true, param);
		}

		public DataTable ConsultarXtarjetaEntrega(string tdc_numeroTarjeta)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroTarjeta", tdc_numeroTarjeta);

			return procedureTable("tdcConsultarXtarjetaEntrega", true, param);
		}

		public DataTable ConsultarXestadoEntrega(Int16 tdc_paso)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_paso", tdc_paso);

			return procedureTable("tdcConsultarXestadoEntrega", true, param);
		}

		#endregion

		#region DOCUMENTAL TDC

		public DataTable PorSincronizarFD()
		{
			return procedureTable("tdcPorSincronizarFD", false);
		}
		
		/// <summary>
		/// Actualiza el flujo digital. Salesforce solo devuelve los que tienen flujo digital completo
		/// </summary>
		/// <param name="tdc_numeroDocumento"></param>
		/// <param name="tdc_ciudad"></param>
		/// <param name="tdc_direccion"></param>
		/// <param name="tdc_correo"></param>
		/// <param name="tdc_telefono"></param>
		/// <param name="tdc_celular"></param>
		/// <param name="ID_usuarioSincronizaDatosFK"></param>
		/// <returns></returns>
		
		public DataTable ActualizaFD(string tdc_numeroDocumento, string ID_usuarioFDFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@ID_usuarioFDFK", ID_usuarioFDFK);

			return procedureTable("tdcActualizaFlujoDigital", true, param);
		}

		public DataTable InsertarPagare(string doc_codigoDeceval, string doc_tipoPagare, string doc_tipoDocOtorgante, string doc_documentoOtorgante, string doc_nombreOtorgante, 
			string doc_nombreCodeudor, string doc_tipoDocCodeudor, string doc_documentoCodeudor, string doc_fechaGrabacionPagare, string doc_estadoPagare, string doc_fechaFirmaPagare, string ID_usuarioPagareFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@doc_codigoDeceval", doc_codigoDeceval);
			param.Add("@doc_tipoPagare", doc_tipoPagare);
			param.Add("@doc_tipoDocOtorgante", doc_tipoDocOtorgante);
			param.Add("@doc_documentoOtorgante", doc_documentoOtorgante);
			param.Add("@doc_nombreOtorgante", doc_nombreOtorgante);
			param.Add("@doc_nombreCodeudor", doc_nombreCodeudor);
			param.Add("@doc_tipoDocCodeudor", doc_tipoDocCodeudor);
			param.Add("@doc_documentoCodeudor", doc_documentoCodeudor);
			param.Add("@doc_fechaGrabacionPagare", doc_fechaGrabacionPagare);
			param.Add("@doc_estadoPagare", doc_estadoPagare);
			param.Add("@doc_fechaFirmaPagare", doc_fechaFirmaPagare);
			param.Add("@ID_usuarioPagareFK", ID_usuarioPagareFK);

			return procedureTable("tdcInsertarPagare", true, param);
		}

		public DataTable ConsultarDocumental()
		{
			Hashtable param = new Hashtable(1);
			return procedureTable("tdcConsultarDocumental", true, param);
		}

		public DataTable actualizaDocIngresosXDocumento(string tdc_numeroDocumento, string ID_usuarioDocIngresoFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@ID_usuarioDocIngresoFK", ID_usuarioDocIngresoFK);

			return procedureTable("tdcActualizaDocIngresosXdocumento", true, param);
		}

		public DataTable actualizaDocIdentidadXDocumento(string tdc_numeroDocumento, string ID_usuarioDocIdentFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@ID_usuarioDocIdentFK", ID_usuarioDocIdentFK);

			return procedureTable("tdcActualizaDocIdentidadXdocumento", true, param);
		}

		#endregion

		#region PASO 8

		public DataTable PorSincronizarDiasMora()
		{
			return procedureTable("tdcPorSincronizarDiasMora", false);
		}

		public DataTable ActualizaDiasMora(string tdc_numeroDocumento, Int32 tdc_numeroPagos, Int32 tdc_diasMora, string ID_usuarioDMFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@tdc_numeroPagos", tdc_numeroPagos);
			param.Add("@tdc_diasMora", tdc_diasMora);
			param.Add("@ID_usuarioDMFK", ID_usuarioDMFK);

			return procedureTable("tdcActualizaDiasMora", true, param);
		}

		public DataTable consultaSolicitudXDiasMora()
		{
			Hashtable param = new Hashtable(1);
			return procedureTable("tdcConsultarSolicXDiasMora", true, param);
		}

		public DataTable ConsultarXtarjetaActiva(string tdc_numeroTarjeta)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroTarjeta", tdc_numeroTarjeta);

			return procedureTable("tdcConsultarXtarjetaActiva", true, param);
		}

		public DataTable ActualizarInfoActivacion(string tdc_numeroTarjeta, string tdc_fechaActivacion, string ID_usuarioActivacionFK,
			 string tdc_archivoActivacionP9)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroTarjeta", tdc_numeroTarjeta);
			param.Add("@tdc_fechaActivacion", tdc_fechaActivacion);
			param.Add("@ID_usuarioActivacionFK", ID_usuarioActivacionFK);
			param.Add("@tdc_archivoActivacionP9", tdc_archivoActivacionP9);

			return procedureTable("tdcActualizaInfoActivacion", true, param);
		}

		public DataTable consultaSolicitudXArchivoActi(string tdc_archivoActivacionP9)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_archivoActivacionP9", tdc_archivoActivacionP9);

			return procedureTable("tdcConsultarSolicXarchivoActi", true, param);
		}

		#endregion

		#region REPORTES

		public DataTable consultaSolicitudGeneral(string filCampo, string filFiltro)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@filCampo", filCampo);
			param.Add("@filFiltro", filFiltro);

			return procedureTable("tdcConsultaGeneral", true, param);
		}

		public DataTable DevolverSolicitud(string ID_tdcSolicitud, string tdc_pasoAnterior, string tdc_pasoNuevo, string ID_usuarioActualizaPaso)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_tdcSolicitud", ID_tdcSolicitud);
			param.Add("@tdc_pasoAnterior", tdc_pasoAnterior);
			param.Add("@tdc_pasoNuevo", tdc_pasoNuevo);
			param.Add("@ID_usuarioActualizaPaso", ID_usuarioActualizaPaso);
			return procedureTable("tdcDevolverPaso", true, param);
		}

		#endregion

	}
}
