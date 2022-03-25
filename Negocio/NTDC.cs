using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Negocio
{
    public class NTDC 
    {
        Datos.DTDC dTDC = new Datos.DTDC();

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
		public void Sincronizar()
		{
			DataTable tbDatos = dTDC.PorSincronizar();
			if (tbDatos.Rows.Count > 0)
			{ 
				//organizar el array de strings, de cedulas para consultar

			}
		}
		#endregion
	}
}
