using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_Admin_F_Perfiles : System.Web.UI.Page
{
    Negocio.NUsuario nUsuario = new Negocio.NUsuario();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            consultarPerfiles();
            consultaModulos();
        }
    }

    void consultarPerfiles()
    {
        gdvPerfiles.DataSource = nUsuario.perConsultaGeneral(ddlFiltroEstado.SelectedValue);
        gdvPerfiles.DataBind();
        trDatos.Visible = true;
        trInsertar.Visible = false;
    }

    void consultaModulos()
    {
        chkModulos.DataSource = nUsuario.ModulosConsultar();
        chkModulos.DataTextField = "Nombre_Item";
        chkModulos.DataValueField = "Id_MenuItem";
        chkModulos.DataBind();
    }

    protected void gdvPerfiles_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        btnGrabar.Text = "Grabar";
        ltrCrear.Text = "Editar Perfil";
        DataTable tabla = nUsuario.perConsultaXID((int)gdvPerfiles.DataKeys[e.NewSelectedIndex].Value);
        hdfIdPerfil.Value = gdvPerfiles.DataKeys[e.NewSelectedIndex].Value.ToString();
        txtNombre.Text = tabla.Rows[0]["per_nombre"].ToString();
        ddlProceso.SelectedValue = tabla.Rows[0]["per_proceso"].ToString();
        txtCorreo.Text = tabla.Rows[0]["per_correoProceso"].ToString();
        txtNivel.Text = tabla.Rows[0]["per_nivel"].ToString();
        chkEdtEstado.Checked = (bool)tabla.Rows[0]["per_estado"];
        trDatos.Visible = false;
        trInsertar.Visible = true;

        //RECORRER LOS PERMISOS A LOS QUE TIENE ACCESO
        for (int j = 0; j < chkModulos.Items.Count; j++)
            chkModulos.Items[j].Selected = false ;

        DataTable tbMenuPerfil = nUsuario.ModulosConsultarXPerfil((int)gdvPerfiles.DataKeys[e.NewSelectedIndex].Value);
        if (tbMenuPerfil.Rows.Count > 0)
        {
            for (int i = 0; i < tbMenuPerfil.Rows.Count; i++)
            {
                for (int j = 0; j < chkModulos.Items.Count; j++)
                {
                    if (tbMenuPerfil.Rows[i]["Id_ItemGrupo"].ToString() == chkModulos.Items[j].Value)
                    {
                        chkModulos.Items[j].Selected = true;
                    }
                }
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        trDatos.Visible = true;
        trInsertar.Visible = false;
        ltrCrear.Text = "";

    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        if (Session["ID_usuario"] != null)
        {
            if (btnGrabar.Text == "Grabar")
            {
                nUsuario.EditarPerfiles(hdfIdPerfil.Value, this.txtNombre.Text, this.txtNivel.Text, ddlProceso.SelectedValue, this.txtCorreo.Text, this.chkEdtEstado.Checked, Session["ID_usuario"].ToString());
                nUsuario.ModuloXPerfilEliminar(hdfIdPerfil.Value);
                for (int j = 0; j < chkModulos.Items.Count; j++)
                {
                    if (chkModulos.Items[j].Selected == true)
                        nUsuario.ModuloXPerfilInsertar(hdfIdPerfil.Value, txtNombre.Text, chkModulos.Items[j].Value);
                }
            }
            else
            {
                DataTable tbInsertado = nUsuario.InsertarPerfiles(this.txtNombre.Text, this.txtNivel.Text, ddlProceso.SelectedValue, this.txtCorreo.Text, Session["ID_usuario"].ToString());
                for (int j = 0; j < chkModulos.Items.Count; j++)
                {
                    if (chkModulos.Items[j].Selected == true)
                        nUsuario.ModuloXPerfilInsertar(tbInsertado.Rows[0]["ID_perfil"].ToString(), txtNombre.Text, chkModulos.Items[j].Value);
                }
            }
            consultarPerfiles();
        }
    }

    protected void btnCrear_Click(object sender, EventArgs e)
    {
        trDatos.Visible = false;
        trInsertar.Visible = true;
        btnGrabar.Text = "Crear";
        ltrCrear.Text = "Crear Perfil";
        txtNombre.Text = "";
        txtCorreo.Text = "";
        txtNivel.Text = "5";
        chkEdtEstado.Checked = true;
        chkEdtEstado.Enabled = false;
        for (int j = 0; j < chkModulos.Items.Count; j++)
            chkModulos.Items[j].Selected = false;
    }

    protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        consultarPerfiles();
    }
}