using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_F_CambiarClave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["nombreUsuario"] = null;
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        if (Session["ID_usuario"] != null)
        {
            Negocio.NUsuario nUsuario = new Negocio.NUsuario();
            lbl_error_usuario.Text = nUsuario.ActualizaClave(Session["ID_usuario"].ToString(), Session["usr_userName"].ToString(), tb_clave_actual.Text, tb_clave_nueva.Text);
        }
    }
}