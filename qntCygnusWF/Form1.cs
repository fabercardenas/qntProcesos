using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Configuration;
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
            Sincronizar();
            //string responseSoap = "<ETHOS><crearPersonas><Resultado>1.00</Resultado><MensajeError>1.00</MensajeError><CodigoCygnus>34.00</CodigoCygnus></crearPersonas></ETHOS>";
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(responseSoap);

            //string xpath = "/ETHOS/crearPersonas";
            //var nodes = xmlDoc.SelectNodes(xpath);

            //foreach (XmlNode childrenNode in nodes)
            //{
            //    lblMensaje.Text = "Resultado: " + childrenNode["Resultado"].InnerText + "\n";
            //    lblMensaje.Text += "MensajeError: " + childrenNode["MensajeError"].InnerText + "\n";
            //    lblMensaje.Text += "CodigoCygnus: " + childrenNode["CodigoCygnus"].InnerText + "\n";
            //}
        }

        public async void Sincronizar()
        {
            NCygnus salesforce = new NCygnus();
            //ContactoAcuerdoNuevoLista resultado = await salesforce.SincronizarAcuerdosAsync(DateTime.Today);
            ContactoAcuerdoNuevoLista resultado = await salesforce.SincronizarAcuerdosAsync(Convert.ToDateTime(ConfigurationManager.AppSettings["diaSincroniza"].ToString()));

            if ((resultado != null) && (resultado.ContactosNuevos!=null) && (resultado.ContactosNuevos.Count>0))
            {
                Cygnus.WSPersonasCygnusSoapClient cygnus = new Cygnus.WSPersonasCygnusSoapClient();
                CygnusCredito.WSSimuladorCreditoQntSoapClient cygnusCredito = new CygnusCredito.WSSimuladorCreditoQntSoapClient();
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

                    string xpath = "/ETHOS/crearPersonas";
                    var nodes = xmlDoc.SelectNodes(xpath);

                    foreach (XmlNode childrenNode in nodes)
                    {
                        lblMensaje.Text = "Resultado: " + childrenNode["Resultado"].InnerText + "\n";
                        lblMensaje.Text += "MensajeError: " + childrenNode["MensajeError"].InnerText + "\n";
                        lblMensaje.Text += "CodigoCygnus: " + childrenNode["CodigoCygnus"].InnerText + "\n";

                        //si la persona esta bien, le mando el acuerdo en cygnus
                        if (childrenNode["MensajeError"].InnerText == "1.00")
                        {
                            //leer los acuerdo producto 
                            string productos = "";
                            for (int i = 0; i < contacto.AcuerdoProductos.Count; i++)
                            {
                                productos += contacto.AcuerdoProductos[i].Entidad__c + ";" + contacto.AcuerdoProductos[i].Valor_Calculado__c + ";" + string.Format("{0:N2}", contacto.AcuerdoProductos[i].PART_PROD_OPOR__c) + ";";
                            }
                            productos = productos.Substring(0, productos.Length - 1);
                            productos = productos.Replace(',', '.');

                            string lineaCredito = "1";
                            //cuando debo pasar a la linea 2?

                            //????????????????????????????????????????????????????????????????????????????

                            CygnusCredito.RespuestaCrea respCredito = cygnusCredito.creaCredito(contacto.Vr_Total_Acuerdo__c.ToString(), contacto.Plazo_aceptado__c.ToString(), string.Format("{0:MM-dd-yyyy}", DateTime.Today),
                                                                                "", lineaCredito, "mensual", contacto.ID_Cliente__c, "mensual", "", "", "", "", "", "", "", "C", "C",
                                                                                contacto.AcuerdoId, productos, "cygnus", "sqlebs", "E3C720DA4004");
                            if (respCredito.R_s_mensaje == "SUCCESS")
                            {
                                //creo bien, enviar el codigo a salesforce
                            }
                        }
                    }
                }
            }

            
            //Cygnus.RespuestaPermite haber = otro.pruebaConexion("cygnus", "sqlebs", "E3C720DA4004");

            
        }
    }
}
