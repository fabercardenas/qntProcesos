using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_Admin_F_Perfiles : System.Web.UI.Page
{
    Negocio.NUsuario nUsuario = new Negocio.NUsuario();
    Datos.DUtilidades dUtilidad = new Datos.DUtilidades();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cargarDdlPaginas();
            consultarPerfiles();
        }
    }

    void cargarDdlPaginas()
    {
        ddlFiltroPaginas.DataSource = dUtilidad.acc_consultaPaginas();
        ddlFiltroPaginas.DataTextField = "acc_pagina";
        ddlFiltroPaginas.DataValueField = "acc_pagina";
        ddlFiltroPaginas.DataBind();
    }
    void consultarPerfiles()
    {
        ddlFiltroPerfiles.DataSource = nUsuario.perConsultaGeneral("1");
        ddlFiltroPerfiles.DataTextField = "per_nombre";
        ddlFiltroPerfiles.DataValueField = "ID_perfil";
        ddlFiltroPerfiles.DataBind();
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ltrMensaje.Text = "";
        chkAcciones.DataSource = dUtilidad.acc_consultaAcciones(ddlFiltroPaginas.SelectedValue);
        chkAcciones.DataTextField = "acc_nombre";
        chkAcciones.DataValueField = "ID_accion";
        chkAcciones.DataBind();
        btnGuardar.Visible = true;

        DataTable tbAccionesPerfil = dUtilidad.acc_consultaAccionesXPerfil(ddlFiltroPaginas.SelectedValue, ddlFiltroPerfiles.SelectedValue );
        if (tbAccionesPerfil.Rows.Count > 0)
        {
            for (int i = 0; i < tbAccionesPerfil.Rows.Count; i++)
            {
                for (int j = 0; j < chkAcciones.Items.Count; j++)
                {
                    if (tbAccionesPerfil.Rows[i]["ID_accionFK"].ToString() == chkAcciones.Items[j].Value)
                    {
                        chkAcciones.Items[j].Selected = true;
                    }
                }
            }
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (Session["ID_usuario"] != null)
        {
            dUtilidad.acc_EliminaAccionesXPerfil(ddlFiltroPaginas.SelectedValue, ddlFiltroPerfiles.SelectedValue);

            for (int j = 0; j < chkAcciones.Items.Count; j++)
            {
                if (chkAcciones.Items[j].Selected == true)
                    dUtilidad.acc_InsertaAccionesXPerfil(chkAcciones.Items[j].Value, ddlFiltroPerfiles.SelectedValue, Session["ID_usuario"].ToString());

            }
            ltrMensaje.Text = Messaging.Success("Acciones por perfil actualizadas correctamente");
        }
    }
}