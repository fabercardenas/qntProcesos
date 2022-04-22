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

		public DataTable InsertarInfo(string tdc_IdColocacion, string tdc_fechaRealce, string tdc_estado, string tdc_contrato, string tdc_nombre, string ID_usuarioRegistraFK, string tdc_archivoCargaP2)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_IdColocacion", tdc_IdColocacion);
			param.Add("@tdc_fechaRealce", tdc_fechaRealce);
			param.Add("@tdc_estado", tdc_estado);
			param.Add("@tdc_contrato", tdc_contrato);
			param.Add("@tdc_nombre", tdc_nombre);
			param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
			param.Add("@tdc_archivoCargaP2", tdc_archivoCargaP2);


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

		
	}
}
