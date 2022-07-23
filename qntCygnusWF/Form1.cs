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
            lblMensaje.Text = "";
            txtMensaje.Text = "";
            lblTotal.Text = "";
            try
            {
                //MUESTRO EL BOTON PARA QUE SELECCIONEN EL DIA DE SINCRONIZACION
                if (ConfigurationSettings.AppSettings["sincronizaAutomatico"].ToString() == "SI")
                {
                    Sincronizar(DateTime.Today);
                    lblTitulo.Visible = false;
                    clrFecha.Visible = false;
                    btnSincronizar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Se revienta " + ex.Message;
                throw;
            }
            
            //for (int i = 0; i < 90; i++)
            //{
            //    lblMensaje.Text += "Resultado: " + i.ToString() + "\n\n";
            //}
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

        public async void Sincronizar(DateTime fechaSincroniza)
        {
            NCygnus salesforce = new NCygnus();
            //ContactoAcuerdoNuevoLista resultado = await salesforce.SincronizarAcuerdosAsync(DateTime.Today);
            ContactoAcuerdoNuevoLista resultado = await salesforce.SincronizarAcuerdosAsync(fechaSincroniza);

            if ((resultado != null) && (resultado.ContactosNuevos!=null) && (resultado.ContactosNuevos.Count>0))
            {
                Cygnus.WSPersonasCygnusSoapClient cygnus = new Cygnus.WSPersonasCygnusSoapClient();
                CygnusCredito.WSSimuladorCreditoQntSoapClient cygnusCredito = new CygnusCredito.WSSimuladorCreditoQntSoapClient();
                string esResidente = "1";
                lblTotal.Text = "En salesforce se encontraron " + resultado.ContactosNuevos.Count.ToString() + " acuerdos";
                
                for (int sf = 0; sf < resultado.ContactosNuevos.Count; sf++)
                {
                    ContactoAcuerdoNuevo contacto = resultado.ContactosNuevos[sf];
                
                    if (contacto.Birthdate == null)
                    {
                        contacto.Birthdate = Convert.ToDateTime("1900-01-01");
                    }
                    string responseSoap = cygnus.WC_CREAR_PERSONAS("", "N", "", contacto.ID_Cliente__c, "11", "1", "", "", contacto.FirstName, contacto.LastName, "11001",
                                                    string.Format("{0:dd-MM-yyyy}", contacto.Birthdate), esResidente, "", "", "", "", "", "", "", "", "", "", "57", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                                                    "", "", "", "", "", "cygnus", "sqlebs", "E3C720DA4004");
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseSoap);

                    string xpath = "/ETHOS/crearPersonas";
                    var nodes = xmlDoc.SelectNodes(xpath);

                    foreach (XmlNode childrenNode in nodes)
                    {
                        txtMensaje.Text += "Cliente : " + contacto.ID_Cliente__c + "  fue creado en Cygnnus con código : " + childrenNode["CodigoCygnus"].InnerText + Environment.NewLine;
                        

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
                            string vrCuotaInicial = contacto.Vr_Cuota_1__c.ToString();
                            if ((contacto.Plazo_aceptado__c == 1) && (vrCuotaInicial=="0"))
                            {
                                vrCuotaInicial = contacto.Vr_Cuota_Mensual__c.ToString();
                            }

                            CygnusCredito.RespuestaCrea respCredito = cygnusCredito.creaCredito(contacto.Vr_Total_Acuerdo__c.ToString(), contacto.Plazo_aceptado__c.ToString(), string.Format("{0:MM-dd-yyyy}", DateTime.Today),
                                                                                "", lineaCredito, "mensual", contacto.ID_Cliente__c, "mensual", vrCuotaInicial, string.Format("{0:MM-dd-yyyy}", DateTime.Today), "", "", "", "", "", "C", "C",
                                                                                contacto.AcuerdoId, productos, "cygnus", "sqlebs", "E3C720DA4004");
                            if (respCredito.R_s_mensaje == "SUCCESS")
                            {
                                //creo bien, guardar log en bpm
                                txtMensaje.Text += "Acuerdo en sf " + contacto.AcuerdoId + " fue creado en Cygnus con código " + respCredito.R_numRadic + Environment.NewLine + Environment.NewLine;
                            }
                            else
                                txtMensaje.Text += "Acuerdo en sf " + contacto.AcuerdoId + " No pudo ser creado en Cygnus" + Environment.NewLine + Environment.NewLine;
                        }
                    }
                } // fin recorrer resultado.ContactosNuevos
                //enviar a salesforce el conjunto de registros exitosos con la info de cygnus
            }
            else
                lblTotal.Text = "No hay acuerdos en salesforce por procesar para esta fecha";

            //Cygnus.RespuestaPermite haber = otro.pruebaConexion("cygnus", "sqlebs", "E3C720DA4004");
            if (ConfigurationSettings.AppSettings["sincronizaAutomatico"].ToString() != "SI")
            {
                btnSincronizar.Visible = true;
                lblMensaje.Text = "";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnSincronizar.Visible = false;
            lblMensaje.Text = "Sincronizando, por favor espere ...";
            txtMensaje.Text = "";
            lblTotal.Text = "";
            Sincronizar(clrFecha.Value);
        }
    }
}
