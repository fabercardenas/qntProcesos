using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = System.Configuration.ConfigurationManager.AppSettings["nameSite"].ToString();
    }

    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        Negocio.NUsuario nUsuario = new Negocio.NUsuario();
        DataTable dataUsuario;
        //string encriptado = nUsuario.CrearEncriptadoSHA1("jurado");

        dataUsuario = nUsuario.Autenticar(this.txtUsuario.Text, this.txtClave.Text);

        if ((dataUsuario!= null) && (dataUsuario.Rows.Count>0))
        {
            Session["usr_userName"] = dataUsuario.Rows[0]["usr_userName"].ToString();
            Session["usr_documento"] = dataUsuario.Rows[0]["usr_documento"].ToString();
            Session["nombreUsuario"] = dataUsuario.Rows[0]["usr_nombre1"].ToString() + "  " + dataUsuario.Rows[0]["usr_apellido1"].ToString();
            Session["ultimoAcceso"] = DateTime.Now;
            Session["ID_usuario"] = (int)dataUsuario.Rows[0]["ID_usuario"];
            Session["ID_empresaFK"] = dataUsuario.Rows[0]["ID_empresaFK"].ToString();
            Session["ID_perfilFK"] = dataUsuario.Rows[0]["ID_perfilFK"].ToString();
            Session["per_nivel"] = dataUsuario.Rows[0]["per_nivel"].ToString();
            this.lblMensaje.Text = "";
            nUsuario.ActualizaUltimoIngreso(dataUsuario.Rows[0]["ID_usuario"].ToString());
            Session.Add("MenuXPerfil", nUsuario.ConsultaMenuXPerfil((int)dataUsuario.Rows[0]["ID_perfilFK"]));
            Session.Add("AccionesNoPermitidas", nUsuario.consultaAccionesNoPermitidas((int)dataUsuario.Rows[0]["ID_perfilFK"]));

            try
            {
                FormsAuthentication.RedirectFromLoginPage(Session["usr_userName"].ToString(), false);
            }
            catch (Exception)
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
            this.lblMensaje.Text = Messaging.Error("Usuario o clave incorrectos");
    }

}