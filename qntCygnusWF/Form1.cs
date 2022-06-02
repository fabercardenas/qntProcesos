using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qntCygnusWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Cygnus.WSPersonasCygnusSoapClient otro = new Cygnus.WSPersonasCygnusSoapClient();
            //Cygnus.RespuestaPermite haber = otro.pruebaConexion("cygnus", "sqlebs", "E3C720DA4004");

            lblMensaje.Text = otro.WC_CREAR_PERSONAS("", "N","","123456789","11","1","","","Los Nombres","Los Apellidos","11001",
                                                    "01-01-1980","1","","","","","","","","","","","","","","","","","","","","","","","","","","",
                                                    "","","","","", "cygnus", "sqlebs", "E3C720DA4004");
        }
    }
}
