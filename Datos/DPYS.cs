using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace Datos
{
    public class DPYS : DSQL
    {
		public DataTable PorSincronizar()
		{
			return procedureTable("tdcPorSincronizar", false);
		}

		public DataTable ConsultarPazySalvoXemitir(string pys_estado, string filCampo, string reg_fechaInicial, string reg_fechaFinal, string cli_numeroDocumento, string perfilAprobacion)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@pys_estado", pys_estado);
			param.Add("@filCampo", filCampo);
			param.Add("@reg_fechaInicial", reg_fechaInicial);
			param.Add("@reg_fechaFinal", reg_fechaFinal);
			param.Add("@cli_numeroDocumento", cli_numeroDocumento);
			param.Add("@perfilAprobacion", perfilAprobacion);
			return procedureTable("pysConsultarPazySalvoXemitir", true, param);
		}

		public DataTable ConsultarPazySalvoXdocumento(string cli_numeroDocumento)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@cli_numeroDocumento", cli_numeroDocumento);
			return procedureTable("pysConsultarPazySalvoXdocumento", true, param);
		}

		public DataTable ActualizarXacuerdoRechazo(string ID_acuerdo, string pys_causal, string ID_usuarioFK, string perfilRechazo)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_acuerdo", ID_acuerdo);
			param.Add("@pys_causal", pys_causal);
			param.Add("@ID_usuarioFK", ID_usuarioFK);
			param.Add("@perfilRechazo", perfilRechazo);

			return procedureTable("pysActualizaXacuerdoRechazo", true, param);
		}

		public DataTable ActualizarXacuerdoAprobar(string ID_acuerdo, string ID_usuarioFK, string perfilAprueba)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_acuerdo", ID_acuerdo);
			param.Add("@ID_usuarioFK", ID_usuarioFK);
			param.Add("@perfilAprueba", perfilAprueba);

			return procedureTable("pysActualizaXacuerdoAprueba", true, param);
		}
	}
}
