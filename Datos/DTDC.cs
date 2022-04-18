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
			 string tdc_proceso, string ID_usuarioRegistraFK, string tdc_archivoCarga)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_tipoDocumento", tdc_tipoDocumento);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@tdc_canal", tdc_canal);
			param.Add("@tdc_proceso", tdc_proceso);
			param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
			param.Add("@tdc_archivoCarga", tdc_archivoCarga);

            return procedureTable("tdcInsertar", true, param);
		}

        #endregion

        #region PASO 2
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

		#region PASO 3

		public DataTable InsertarInfo(string tdc_numeroDocumento, string tdc_IdColocacion, string tdc_numeroTarjeta,
			 string tdc_tipoProducto, string tdc_fechaRealce, string tdc_fechaActivacion, string tdc_Valor,
			 string tdc_estado, string tdc_contrato, string tdc_nombre, string tdc_copiaNumeroTarjeta, string ID_usuarioRegistraFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@tdc_IdColocacion", tdc_IdColocacion);
			param.Add("@tdc_numeroTarjeta", tdc_numeroTarjeta);
			param.Add("@tdc_tipoProducto", tdc_tipoProducto);
			param.Add("@tdc_fechaRealce", tdc_fechaRealce);
			param.Add("@tdc_fechaActivacion", tdc_fechaActivacion);
			param.Add("@tdc_Valor", tdc_Valor);
			param.Add("@tdc_estado", tdc_estado);
			param.Add("@tdc_contrato", tdc_contrato);
			param.Add("@tdc_nombre", tdc_nombre);
			param.Add("@tdc_copiaNumeroTarjeta", tdc_copiaNumeroTarjeta);
			param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);


			return procedureTable("tdcInsertarInfo", true, param);
		}
		#endregion
	}
}
