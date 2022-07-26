using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace Datos
{
    public class DPagos : DSQL
    {
		#region   INSERTAR CARGUE 
		public DataTable InsertarCargue(string car_codigoCargue, string ID_usuarioRegistroFK, string car_archivoCargue,
			 string car_numeroDocumento, string car_valorPago, string car_fechaPago, string car_horaPago, string car_identificadorUnico, string car_canal, string car_IDacuerdo)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@car_codigoCargue", car_codigoCargue);
			param.Add("@ID_usuarioRegistroFK", ID_usuarioRegistroFK);
			param.Add("@car_archivoCargue", car_archivoCargue);
			param.Add("@car_numeroDocumento", car_numeroDocumento);
			param.Add("@car_valorPago", car_valorPago);
			param.Add("@car_fechaPago", car_fechaPago);
			param.Add("@car_horaPago", car_horaPago);
			param.Add("@car_identificadorUnico", car_identificadorUnico);
			param.Add("@car_canal", car_canal);
			param.Add("@car_IDacuerdo", car_IDacuerdo);

			return procedureTable("pagInsertar", true, param);
		}
		#endregion
	}
}
