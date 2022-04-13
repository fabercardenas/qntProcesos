using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio
{
	//DEFINICION DE CLASES PARA TRANSFORMACION DE DATOS
	public class ContactoBasico
	{
		public string Id { get; set; }
		public string ID_Cliente__c { get; set; }
		public string Ciudad_residencia__c { get; set; }
		public string Direccin_Residencia__c { get; set; }
		public string UpCelular__c { get; set; }
		public string UpTelefonoFijo__c { get; set; }
		public string UpCorreo__c { get; set; }
	}

	public class ContactoLista
	{
		public List<ContactoBasico> contactos { get; set; }
	}
}
