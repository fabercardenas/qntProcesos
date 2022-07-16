using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace Datos
{
    public class DBpmSrv : DSQL
    {
		
		public DataTable LogInsertar(string ID_cliente, string ext_nombre, string ext_mail, 
									 string ext_pdf, string ext_data, string ext_estadoEnvio, 
									 string ext_machineName, string ext_manchineUser)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_cliente", ID_cliente);
			param.Add("@ext_nombre", ext_nombre);
			param.Add("@ext_mail", ext_mail);
			param.Add("@ext_pdf", ext_pdf);
			param.Add("@ext_data", ext_data);
			param.Add("@ext_estadoEnvio", ext_estadoEnvio);
			param.Add("@ext_machineName", ext_machineName);
			param.Add("@ext_manchineUser", ext_manchineUser);

			return procedureTable("extLogInsertar", true, param);
		}

		public DataTable extConsultaXDocumento(string ID_cliente)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_cliente", ID_cliente);

			return procedureTable("extConsultaXDocumento", true, param);
		}
	}
}
