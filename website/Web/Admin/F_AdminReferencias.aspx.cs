using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class F_AdminReferencias : System.Web.UI.Page
{
    Negocio.NUsuario nUsuario = new Negocio.NUsuario();
    Datos.DUtilidades dUtilidad = new Datos.DUtilidades();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable tbModulos = dUtilidad.consultaReferenciasModulos();
            ddlFiltroTipo.DataSource = tbModulos;
            ddlFiltroTipo.DataTextField = "ref_modulo";
            ddlFiltroTipo.DataValueField = "ref_modulo";
            ddlFiltroTipo.DataBind();

            ddlTipo.DataSource = tbModulos;
            ddlTipo.DataTextField = "ref_modulo";
            ddlTipo.DataValueField = "ref_modulo";
            ddlTipo.DataBind();

            consultarReferencias();
        }
    }

    void consultarReferencias()
    {
        gdvReferencias.DataSource = dUtilidad.consultaReferenciaXModulo(ddlFiltroTipo.SelectedValue,"ref_orden", "SalarioMinimo','Niveles Riesgo");
        if (ddlFiltroTipo.SelectedValue != "NovedadesLaborales")
        {
            gdvReferencias.Columns[7].Visible = false;
            gdvReferencias.Columns[8].Visible = false;
            gdvReferencias.Columns[9].Visible = false;
        }
        else
        {
            gdvReferencias.Columns[7].Visible = true;
            gdvReferencias.Columns[8].Visible = true;
            gdvReferencias.Columns[9].Visible = true;
        }
        gdvReferencias.DataBind();
        trDatos.Visible = true;
        trInsertar.Visible = false;
    }

    protected void gdvAdminSalud_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        btnGrabar.Text = "Grabar";
        DataTable tabla = dUtilidad.consultaReferenciaXID(gdvReferencias.DataKeys[e.NewSelectedIndex].Values[0].ToString());
        ltrCrear.Text = "Editar " + tabla.Rows[0]["ref_modulo"].ToString();
        ddlTipo.SelectedValue = tabla.Rows[0]["ref_modulo"].ToString();
        ddlTipo.Enabled = false;
        txtValor.Text = tabla.Rows[0]["ref_valor"].ToString();
        txtDescripcion.Text = tabla.Rows[0]["ref_descripcion"].ToString();
        txtNombre.Text = tabla.Rows[0]["ref_nombre"].ToString();
        txtParametro1.Text = tabla.Rows[0]["ref_parametro1"].ToString();
        txtParametro2.Text = tabla.Rows[0]["ref_parametro2"].ToString();
        txtParametro3.Text = tabla.Rows[0]["ref_parametro3"].ToString();
        txtTipoNov.Text = tabla.Rows[0]["ref_tipoNov"].ToString();
        txtContableConcepto.Text = tabla.Rows[0]["ref_ContableConcepto"].ToString();
        txtContableCuenta.Text = tabla.Rows[0]["ref_ContableCuenta"].ToString();
        txtContableNaturaleza.Text = tabla.Rows[0]["ref_ContableNaturaleza"].ToString();
        txtContableCuenta2.Text = tabla.Rows[0]["ref_ContableCuenta2"].ToString();
        txtContableNaturaleza2.Text = tabla.Rows[0]["ref_ContableNaturaleza2"].ToString();
        txtOrden.Text = tabla.Rows[0]["ref_orden"].ToString();
        chkEdtEstado.Checked = (bool)tabla.Rows[0]["ref_estado"];
        chkEdtEstado.Enabled = true;        
        trDatos.Visible = false;
        trInsertar.Visible = true;
        hdfIdReferencia.Value = gdvReferencias.DataKeys[e.NewSelectedIndex].Values[0].ToString();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        trDatos.Visible = true;
        trInsertar.Visible = false;
        lblMensaje.Text = "";
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        if (btnGrabar.Text == "Grabar")
        {
            dUtilidad.ReferenciasActualizar(Convert.ToInt32(hdfIdReferencia.Value), ddlTipo.SelectedValue, txtValor.Text, txtNombre.Text, txtDescripcion.Text, Convert.ToInt32(txtParametro1.Text),Convert.ToInt32(txtParametro2.Text), Convert.ToInt32(txtOrden.Text), Convert.ToInt32(txtTipoNov.Text), txtContableConcepto.Text, txtContableCuenta.Text, txtContableNaturaleza.Text, txtContableCuenta2.Text, txtContableNaturaleza2.Text, Convert.ToInt32(txtParametro3.Text), chkEdtEstado.Checked, (int)Session["ID_usuario"]);
            consultarReferencias();
        }
        else
        {
            dUtilidad.ReferenciasInsertar(ddlTipo.SelectedValue, this.txtValor.Text, this.txtNombre.Text, txtDescripcion.Text, Convert.ToInt32(txtParametro1.Text), Convert.ToInt32(txtParametro2.Text), Convert.ToInt32(txtOrden.Text), Convert.ToInt32(txtTipoNov.Text), txtContableConcepto.Text, txtContableCuenta.Text, txtContableNaturaleza.Text, txtContableCuenta2.Text, txtContableNaturaleza2.Text, Convert.ToInt32(txtParametro3.Text), (int)Session["ID_usuario"]);
            consultarReferencias();
        }
    }

    protected void btnCrear_Click(object sender, EventArgs e)
    {
        trDatos.Visible = false;
        trInsertar.Visible = true;
        btnGrabar.Text = "Crear";
        ltrCrear.Text = "Crear Referencia";
        txtValor.Text = "";
        txtDescripcion.Text = "";
        txtNombre.Text = "";
        txtParametro1.Text = "";
        txtParametro2.Text = "";
        txtParametro3.Text = "";
        txtContableConcepto.Text = "";
        txtTipoNov.Text = "";
        txtContableCuenta.Text = "";
        txtContableNaturaleza.Text = "";
        txtContableCuenta2.Text = "";
        txtContableNaturaleza2.Text = "";
        chkEdtEstado.Checked = true;
        chkEdtEstado.Enabled = false;
        ddlTipo.Enabled = true;

    }

    protected void ddlFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        consultarReferencias();
    }

    protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        consultarReferencias();
    }
}