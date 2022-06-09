using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Negocio;
namespace qntCygnusWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Sincronizar();
            string responseSoap = "<ETHOS><crearPersonas><Resultado>1.00</Resultado><MensajeError>1.00</MensajeError><CodigoCygnus>34.00</CodigoCygnus></crearPersonas></ETHOS>";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseSoap);

            string xpath = "/ETHOS/crearPersonas";
            var nodes = xmlDoc.SelectNodes(xpath);

            foreach (XmlNode childrenNode in nodes)
            {
                lblMensaje.Text = "Resultado: " + childrenNode["Resultado"].InnerText + "\n";
                lblMensaje.Text += "MensajeError: " + childrenNode["MensajeError"].InnerText + "\n";
                lblMensaje.Text += "CodigoCygnus: " + childrenNode["CodigoCygnus"].InnerText + "\n";
            }
        }

        public async void Sincronizar()
        {
            NCygnus salesforce = new NCygnus();
            ContactoAcuerdoNuevoLista resultado = await salesforce.SincronizarAcuerdosAsync(DateTime.Today);

            if ((resultado != null) && (resultado.ContactosNuevos!=null) && (resultado.ContactosNuevos.Count>0))
            {
                Cygnus.WSPersonasCygnusSoapClient cygnus = new Cygnus.WSPersonasCygnusSoapClient();
                string esResidente = "1";
                foreach (ContactoAcuerdoNuevo contacto in resultado.ContactosNuevos)
                {
                    if (contacto.Birthdate==null)
                    {
                        contacto.Birthdate = Convert.ToDateTime("1900-01-01");
                    }
                    string responseSoap = cygnus.WC_CREAR_PERSONAS("", "N", "", contacto.ID_Cliente__c, "11", "1", "", "", contacto.FirstName, contacto.LastName, "11001",
                                                    string.Format("{0:dd-MM-yyyy}", contacto.Birthdate), esResidente, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                                                    "", "", "", "", "", "cygnus", "sqlebs", "E3C720DA4004");
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseSoap);
                }

            }

            
            //Cygnus.RespuestaPermite haber = otro.pruebaConexion("cygnus", "sqlebs", "E3C720DA4004");

            
        }
    }
}
