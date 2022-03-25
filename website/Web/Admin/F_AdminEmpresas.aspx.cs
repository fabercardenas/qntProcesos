using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class F_AdminEmpresas : System.Web.UI.Page
{
    Negocio.NUsuario nUsuario = new Negocio.NUsuario();
    Datos.DUtilidades dUtiliadades = new Datos.DUtilidades();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            consultarEmpresas();
        }
    }

    void consultarEmpresas()
    {
        gdvEmpresas.DataSource = dUtiliadades.consultaEmpresas();
        gdvEmpresas.DataBind();
        trDatos.Visible = true;
        trInsertar.Visible = false;
    }

    protected void gdvAdminSalud_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        btnGrabar.Text = "Grabar";
        ltrCrear.Text = "Editar Empresa";
        DataTable tabla = Negocio.NUtilidades.consultaEmpresaXID((int)gdvEmpresas.DataKeys[e.NewSelectedIndex].Values[0]);

        txtNombre.Text = tabla.Rows[0]["emp_nombre"].ToString();
        txtNombreCorto.Text = tabla.Rows[0]["emp_nombreCorto"].ToString();
        txtNit.Text = tabla.Rows[0]["emp_nit"].ToString();
        txtDV.Text = tabla.Rows[0]["emp_dv"].ToString();
        txtSlogan.Text = tabla.Rows[0]["emp_slogan"].ToString();
        txtDireccion.Text = tabla.Rows[0]["emp_direccion"].ToString();
        txtTelefono.Text = tabla.Rows[0]["emp_telefono"].ToString();
        txtCorreo.Text = tabla.Rows[0]["emp_correo"].ToString();
        txtPaginaWeb.Text = tabla.Rows[0]["emp_paginaWeb"].ToString();
        txtARL.Text = tabla.Rows[0]["emp_ARP"].ToString();
        txtFacSigla.Text = tabla.Rows[0]["emp_facSigla"].ToString();
        txtFacConsecutivo.Text = tabla.Rows[0]["emp_facConsecutivo"].ToString();
        txtRegSigla.Text = tabla.Rows[0]["emp_regSigla"].ToString();
        txtRegConsecutivo.Text = tabla.Rows[0]["emp_regConsecutivo"].ToString();
        txtEgrSigla.Text = tabla.Rows[0]["emp_egrSigla"].ToString();
        txtEgrConsecutivo.Text = tabla.Rows[0]["emp_egrConsecutivo"].ToString();

        chkEdtEstado.Checked = (bool)tabla.Rows[0]["emp_estado"];
        chkEdtEstado.Enabled = true;        
        trDatos.Visible = false;
        trInsertar.Visible = true;
        
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        trDatos.Visible = true;
        trInsertar.Visible = false;
        lblMensaje.Text = "";
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        //lblMensaje.Text = "";
        //if (btnGrabar.Text == "Grabar")
        //{
        //    DataTable tabla = Negocio.NUtilidades.EditarAdminSalud(ddlTipo.SelectedValue, this.txtDV.Text, hdfICodigoAnterior.Value, this.txtNit.Text, this.txtNombre.Text, this.txtNombreCorto.Text, this.txtSlogan.Text, this.txtTelefono.Text, this.chkEdtEstado.Checked);
        //    if ((tabla != null) && (tabla.Rows.Count > 0) && (tabla.Rows[0]["respuesta"].ToString() == "Falla"))
        //        lblMensaje.Text = tabla.Rows[0]["Mensaje"].ToString();
        //    else
        //        consultarEmpresas();

        //}
        //else
        //{
        //    DataTable tabla = Negocio.NUtilidades.InsertarAdminSalud(ddlTipo.SelectedValue, this.txtDV.Text, this.txtNit.Text, this.txtNombre.Text, this.txtNombreCorto.Text, this.txtSlogan.Text, this.txtTelefono.Text);
        //    if ((tabla != null) && (tabla.Rows.Count > 0) && (tabla.Rows[0]["respuesta"].ToString() == "Falla"))
        //        lblMensaje.Text = tabla.Rows[0]["Mensaje"].ToString();
        //    else
        //        consultarEmpresas();
        //}
    }

    protected void btnCrear_Click(object sender, EventArgs e)
    {
        trDatos.Visible = false;
        trInsertar.Visible = true;
        btnGrabar.Text = "Crear";
        ltrCrear.Text = "Crear Empresa";
        txtDV.Text = "";
        txtNit.Text = "";
        txtNombre.Text = "";
        txtNombreCorto.Text = "";
        txtDireccion.Text = "";
        txtTelefono.Text = "";
        txtSlogan.Text = "";
        chkEdtEstado.Checked = true;
        chkEdtEstado.Enabled = false;
    }

    protected void ddlFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        consultarEmpresas();
    }

    protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        consultarEmpresas();
    }
}