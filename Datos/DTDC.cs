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
			 string tdc_proceso, string ID_usuarioRegistraFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@tdc_tipoDocumento", tdc_tipoDocumento);
			param.Add("@tdc_numeroDocumento", tdc_numeroDocumento);
			param.Add("@tdc_canal", tdc_canal);
			param.Add("@tdc_proceso", tdc_proceso);
			param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            return procedureTable("tdcInsertar", true, param);
		}

		#endregion

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
	}
}
