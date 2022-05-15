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

		#endregion

	}
}
