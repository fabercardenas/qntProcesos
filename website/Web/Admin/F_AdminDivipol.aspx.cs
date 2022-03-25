using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class F_AdminDivipol : System.Web.UI.Page
{
    Negocio.NUsuario nUsuario = new Negocio.NUsuario();
    Datos.DUtilidades dUtilidad = new Datos.DUtilidades();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Negocio.NUtilidades.cargarDLLDivipolaPais(ddlFiltroPais);
            consultarDivipol();
        }
    }

    void consultarDivipol()
    {
        gdvDivipol.DataSource = dUtilidad.consultaDivipolXPais(ddlFiltroPais.SelectedValue);
        gdvDivipol.DataBind();
        trDatos.Visible = true;
        trInsertar.Visible = false;
    }

    protected void gdvAdminSalud_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        btnGrabar.Text = "Grabar";
        ltrCrear.Text = "Editar Registro de Divipol";

        txtCodDepto.Text = gdvDivipol.DataKeys[e.NewSelectedIndex].Values[0].ToString();
        txtDepartamento.Text = gdvDivipol.DataKeys[e.NewSelectedIndex].Values[1].ToString();
        txtCodMuni.Text = gdvDivipol.DataKeys[e.NewSelectedIndex].Values[2].ToString();
        txtMunicipio.Text = gdvDivipol.DataKeys[e.NewSelectedIndex].Values[3].ToString();
        txtCodPais.Text = gdvDivipol.DataKeys[e.NewSelectedIndex].Values[4].ToString();
        txtPais.Text = gdvDivipol.DataKeys[e.NewSelectedIndex].Values[5].ToString();
        hdfGdvId.Value = e.NewSelectedIndex.ToString();
        trDatos.Visible = false;
        trInsertar.Visible = true;
        lblMensaje.Text = "";
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
        if (btnGrabar.Text == "Crear")
        {
            DataTable tabla = dUtilidad.divipolInsertar(txtCodPais.Text, txtPais.Text, txtCodDepto.Text, txtDepartamento.Text, txtCodMuni.Text, txtMunicipio.Text);
            if ((tabla != null) && (tabla.Rows.Count > 0) && (tabla.Rows[0]["respuesta"].ToString() == "Falla"))
                lblMensaje.Text = tabla.Rows[0]["Mensaje"].ToString();
            else
                consultarDivipol();
        }
        else
        {
            dUtilidad.divipolEditar(gdvDivipol.DataKeys[Convert.ToInt32(hdfGdvId.Value)].Values[4].ToString(),gdvDivipol.DataKeys[Convert.ToInt32(hdfGdvId.Value)].Values[0].ToString(), gdvDivipol.DataKeys[Convert.ToInt32(hdfGdvId.Value)].Values[2].ToString(),txtCodPais.Text, txtPais.Text, this.txtCodDepto.Text, this.txtDepartamento.Text, this.txtCodMuni.Text, this.txtMunicipio.Text);
            consultarDivipol();
        }
    }

    protected void btnCrear_Click(object sender, EventArgs e)
    {
        trDatos.Visible = false;
        trInsertar.Visible = true;
        btnGrabar.Text = "Crear";
        ltrCrear.Text = "Crear Registro de Divipol";
        txtCodPais.Text = ddlFiltroPais.SelectedValue;
        txtPais.Text = ddlFiltroPais.SelectedItem.Text;
        txtCodDepto.Text = "";
        txtDepartamento.Text = "";
        txtCodMuni.Text = "";
        txtMunicipio.Text = "";
    }

    protected void ddlFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        consultarDivipol();
    }

    protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        consultarDivipol();
    }

    protected void gdvDivipol_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvDivipol.PageIndex = e.NewPageIndex;
        consultarDivipol();
    }
}