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
		public DateTime FechaPagoCuotaInicial__c { get; set; }
		public double Vr_Total_Acuerdo__c { get; set; }
		public Int16 Plazo_aceptado__c { get; set; }
		public string AcuerdoName { get; set; }
		public string AcuerdoId { get; set; }

		public List<AcuerdoProducto> AcuerdoProductos { get; set; }
	}

	public class ContactoAcuerdoNuevoLista
	{
		public List<ContactoAcuerdoNuevo> ContactosNuevos { get; set; }
	}

	public class AcuerdoProducto
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Entidad__c { get; set; }
		public double Valor_Calculado__c { get; set; }
		public double PART_PROD_OPOR__c { get; set; }

	}
	#endregion
}
