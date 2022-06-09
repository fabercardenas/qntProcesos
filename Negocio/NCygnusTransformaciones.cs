using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
	#region CLIENTES CON ACUERDO NUEVO - CUOTA INICIAL PAGADA
	/// <summary>
	/// Un cliente con su acuerdo
	/// </summary>
	public class ContactoAcuerdoNuevo
	{
		public string ID_Cliente__c { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? Birthdate { get; set; }
	}

	public class ContactoAcuerdoNuevoLista
	{
		public List<ContactoAcuerdoNuevo> ContactosNuevos { get; set; }
	}

	#endregion
}
