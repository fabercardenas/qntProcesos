using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_Admin_F_Usuarios : System.Web.UI.Page
{
    Datos.DUsuarios dUsuario = new Datos.DUsuarios();
    Negocio.NUsuario nUsuario = new Negocio.NUsuario();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cargarUsuarios();
            cargarPerfiles();
        }
    }

 #region Metodos de Carga

    void cargarUsuarios()
    {
        this.gdvUsuarios.DataSource = nUsuario.ConsultaGeneral(Int16.Parse(this.ddlFiltroEstado.SelectedValue));
        this.gdvUsuarios.DataBind();
        this.tdInicial.Visible = true;
        this.tdEditar.Visible = false;
        this.tdInsertar.Visible = false;
    }

    void cargarPerfiles()
    {
        DataTable tabla;
        tabla = dUsuario.ConsultaPerfiles();
        ddlEditPerfil.DataSource = tabla;
        ddlEditPerfil.DataTextField = "per_nombre";
        ddlEditPerfil.DataValueField = "ID_perfil";
        ddlEditPerfil.DataBind();

        ddlInsPerfil.DataSource = tabla;
        ddlInsPerfil.DataTextField = "per_nombre";
        ddlInsPerfil.DataValueField = "ID_perfil";
        ddlInsPerfil.DataBind();
    }

    protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarUsuarios();
    }

    protected void PaginarUsuario(object sender, GridViewPageEventArgs e)
    {
        gdvUsuarios.PageIndex = e.NewPageIndex;
        cargarUsuarios();
    }
 
 #endregion

 #region  Editar Usuario

    protected void EditarUsuario(object sender, GridViewEditEventArgs e)
    {
        this.tdInicial.Visible = false;
        this.tdEditar.Visible = true;

        DataTable tabla;
        tabla = dUsuario.ConsultarXID(this.gdvUsuarios.DataKeys[e.NewEditIndex].Value.ToString());
        this.lblMensaje.Text = "";
        this.chkEdtClave.Checked = false;
        this.lblIdUsuario.Text = tabla.Rows[0]["ID_usuario"].ToString();
        this.txtEdtNombre1.Text = tabla.Rows[0]["usr_nombre1"].ToString();
        this.txtEdtNombre2.Text = tabla.Rows[0]["usr_nombre2"].ToString();
        this.txtEdtApel1.Text = tabla.Rows[0]["usr_apellido1"].ToString();
        this.txtEdtApel2.Text = tabla.Rows[0]["usr_apellido2"].ToString();
        this.txtEdtUsuario.Text = tabla.Rows[0]["usr_userName"].ToString();
        this.lblEdtClave.Text = tabla.Rows[0]["usr_clave"].ToString();
        this.ddlEditPerfil.SelectedValue = tabla.Rows[0]["ID_perfilFK"].ToString();
        txtEdtMail.Text = tabla.Rows[0]["usr_mail"].ToString();
        this.chkEdtEstado.Checked = Convert.ToBoolean((Int16.Parse(tabla.Rows[0]["usr_estado"].ToString())));
        
        if (int.Parse(tabla.Rows[0]["per_nivel"].ToString()) >= 50)
        {
            DataTable dbtClientes;
            ddlEdtEmpresaUsuaria.Items.Clear();
            if (int.Parse(tabla.Rows[0]["per_nivel"].ToString()) == 50)
            {
                Datos.DClientes dCliente = new Datos.DClientes();
                dbtClientes = dCliente.ConsultaGeneral(1, Session["per_nivel"].ToString(), Session["ID_empresaFK"].ToString());
            }
            else
            {
                Datos.DUtilidades dUtilidad = new Datos.DUtilidades();
                dbtClientes = dUtilidad.consultaLaboratorios();
            }
            ddlEdtEmpresaUsuaria.DataSource = dbtClientes;
            ddlEdtEmpresaUsuaria.DataTextField = "cli_nombre";
            ddlEdtEmpresaUsuaria.DataValueField = "ID_cliente";
            ddlEdtEmpresaUsuaria.DataBind();

            trEdtEmpresaUsuaria.Visible = true;
            try
            {
                ddlEdtEmpresaUsuaria.SelectedValue = tabla.Rows[0]["ID_empresaFK"].ToString();
            }
            catch (Exception)
            {
                
            }
            
        }
        else
            trEdtEmpresaUsuaria.Visible = false;
    }
    protected void btnEdtCancelar_Click(object sender, EventArgs e)
    {
        this.tdEditar.Visible = false;
        this.tdInicial.Visible = true;
        this.gdvUsuarios.EditIndex = -1;
        this.cargarUsuarios();
    }
    protected void btnEdtEnviar_Click(object sender, EventArgs e)
    {
        nUsuario.Editar(this.lblIdUsuario.Text, this.lblEdtDocumento.Text, Convert.ToInt16(this.ddlEditPerfil.SelectedValue), 1,ddlEdtEmpresaUsuaria.SelectedValue, this.txtEdtNombre1.Text, this.txtEdtNombre2.Text, txtEdtApel1.Text, txtEdtApel2.Text, txtEdtMail.Text, "", "", txtEdtUsuario.Text, lblEdtClave.Text , this.chkEdtEstado.Checked, chkEdtClave.Checked ); 
        tdEditar.Visible = false;
        tdInicial.Visible = true;
        gdvUsuarios.EditIndex = -1;
        cargarUsuarios();
    }

#endregion

    protected void btnInsCancelar_Click(object sender, EventArgs e)
    {
        tdInicial.Visible = true;
        tdInsertar.Visible = false;
    }
    protected void btnInsGrabar_Click(object sender, EventArgs e)
    {
        lblMensaje.Text = nUsuario.Insertar(txtInsDocumento.Text, Convert.ToInt16(this.ddlInsPerfil.SelectedValue), 1,ddlInsEmpresaUsuaria.SelectedValue, txtInsNombre1.Text, txtInsNombre2.Text, txtInsApellido1.Text, txtInsApellido2.Text,txtInsMail.Text, "", "", txtInsUserName.Text, txtInsUserName.Text);
        cargarUsuarios();
    }
    protected void btnCrear_Click(object sender, EventArgs e)
    {
        tdInicial.Visible = false;
        tdInsertar.Visible = true;
    }
    
    protected void ddlInsPerfil_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable tbPerfil = dUsuario.ConsultaPerfilXID(ddlInsPerfil.SelectedValue);
        if (int.Parse(tbPerfil.Rows[0]["per_nivel"].ToString()) >= 50)
        {
            DataTable dbtClientes;
            ddlInsEmpresaUsuaria.Items.Clear();
            if (int.Parse(tbPerfil.Rows[0]["per_nivel"].ToString()) == 50)
            {
                Datos.DClientes dCliente = new Datos.DClientes();
                dbtClientes = dCliente.ConsultaGeneral(1, Session["per_nivel"].ToString(), Session["ID_empresaFK"].ToString());
            }
            else
            {
                Datos.DUtilidades dUtilidad = new Datos.DUtilidades();
                dbtClientes = dUtilidad.consultaLaboratorios();
            }
            ddlInsEmpresaUsuaria.DataSource = dbtClientes;
            ddlInsEmpresaUsuaria.DataTextField = "cli_nombre";
            ddlInsEmpresaUsuaria.DataValueField = "ID_cliente";
            ddlInsEmpresaUsuaria.DataBind();

            trInsEmpresaUsuaria.Visible = true;
        }
        else
            trInsEmpresaUsuaria.Visible = false;
    }
    
    protected void ddlEditPerfil_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable tbPerfil = dUsuario.ConsultaPerfilXID(ddlEditPerfil.SelectedValue);
        if (int.Parse(tbPerfil.Rows[0]["per_nivel"].ToString()) >= 50)
        {
            DataTable dbtClientes;
            ddlEdtEmpresaUsuaria.Items.Clear();
            if (int.Parse(tbPerfil.Rows[0]["per_nivel"].ToString()) == 50)
            {
                Datos.DClientes dCliente = new Datos.DClientes();
                dbtClientes = dCliente.ConsultaGeneral(1, Session["per_nivel"].ToString(), Session["ID_empresaFK"].ToString());
            }
            else
            {
                Datos.DUtilidades dUtilidad = new Datos.DUtilidades();
                dbtClientes = dUtilidad.consultaLaboratorios();
            }
            ddlEdtEmpresaUsuaria.DataSource = dbtClientes;
            ddlEdtEmpresaUsuaria.DataTextField = "cli_nombre";
            ddlEdtEmpresaUsuaria.DataValueField = "ID_cliente";
            ddlEdtEmpresaUsuaria.DataBind();

            trEdtEmpresaUsuaria.Visible = true;
        }
        else
            trEdtEmpresaUsuaria.Visible = false;
    }
}