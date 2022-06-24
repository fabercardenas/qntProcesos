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

		public DataTable ConsultarPazySalvoXemitir()
		{
			Hashtable param = new Hashtable(1);
			return procedureTable("pysConsultarPazySalvoXemitir", true, param);
		}
	}
}
