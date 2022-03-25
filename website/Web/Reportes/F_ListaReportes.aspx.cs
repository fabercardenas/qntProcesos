using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;

public partial class Web_Reportes_F_ListaReportes : System.Web.UI.Page
{
    Datos.DClientes dCliente = new Datos.DClientes();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Negocio.NUtilidades.cargarDDLempresas(ddlcenEmpresa, Convert.ToInt16(Session["ID_empresaOutsourcing"]),true);
            Negocio.NUtilidades.cargarDDLempresas(ddlmovEmpresa, Convert.ToInt16(Session["ID_empresaOutsourcing"]), false);
            Negocio.NUtilidades.cargarDDLempresas(ddlproEmpresa, Convert.ToInt16(Session["ID_empresaOutsourcing"]), false);
            Negocio.NUtilidades.cargarDDLempresas(ddlNomEmpresa, Convert.ToInt16(Session["ID_empresaOutsourcing"]), false);

            ddlcenEmpresa.SelectedIndex = 0;
            ddlmovEmpresa.SelectedIndex = 0;
            txtMovFechaInicio.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            txtMovFechaFin.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Today);

            cargarClientes();
            cargarProveedores();
            cargarCentros();
            btnGenerar.Attributes.Add("onclick", "javascript:url('R_CarteraGeneral.aspx?ud=CDQ290CLK2DF09KDFS0932OP&IdEmp=" + ddlcenEmpresa.SelectedValue + "&IdCli=0');");
            btnGeneraMovimientos.Attributes.Add("onclick", "javascript:url('R_Movimientos.aspx?ud=CDQ290CLK2DF09KDFS0932OP&IdEmp=" + ddlcenEmpresa.SelectedValue + "&fInicio=" + txtMovFechaInicio.Text + "&fFin=" + txtMovFechaFin.Text + "&IdCli=0&IdMov=0');");
            btnGenerarCuentas.Attributes.Add("onclick", "javascript:url('R_CuentasXPagar.aspx?ud=CDQ290CLK2DF09KDFS0932OP&IdEmp=" + ddlcenEmpresa.SelectedValue + "&IdPro=0');");           
        }
    }

    void cargarClientes()
    {
        DataTable tabla = new DataTable();
        tabla = dCliente.ConsultaGeneral(1, Session["per_nivel"].ToString(), Session["ID_empresaFK"].ToString());//->1 clientes, 0 proveedores

        ddlClientes.DataSource = tabla;
        ddlClientes.DataTextField = "cli_nombre";
        ddlClientes.DataValueField = "ID_cliente";
        ddlClientes.DataBind();

        ListItem lt = new ListItem();
        lt.Text = "Seleccione uno";
        lt.Value = "-1";
        ddlClientes.Items.Insert(0, lt);

        ddlmovClientes.DataSource = tabla;
        ddlmovClientes.DataTextField = "cli_nombre";
        ddlmovClientes.DataValueField = "ID_cliente";
        ddlmovClientes.DataBind();

        ddlNomClientes.DataSource = tabla;
        ddlNomClientes.DataTextField = "cli_nombre";
        ddlNomClientes.DataValueField = "ID_cliente";
        ddlNomClientes.DataBind();

        ListItem lt2 = new ListItem();
        lt2.Text = "Seleccione uno";
        lt2.Value = "-1";
        ddlmovClientes.Items.Insert(0, lt2);
    }

    void cargarProveedores()
    {
        DataTable tabla = new DataTable();
        tabla = dCliente.ConsultaGeneral(0, Session["per_nivel"].ToString(), Session["ID_empresaFK"].ToString());//->1 clientes, 0 proveedores

        ddlProveedores.DataSource = tabla;
        ddlProveedores.DataTextField = "cli_nombre";
        ddlProveedores.DataValueField = "ID_cliente";
        ddlProveedores.DataBind();

        ListItem lt = new ListItem();
        lt.Text = "Seleccione uno";
        lt.Value = "-1";
        ddlProveedores.Items.Insert(0, lt);
    }

    void cargarCentros()
    {
        ddlCentroCosto.Items.Clear();
        if (ddlClientes.SelectedValue != "-1")
        {
            ddlCentroCosto.DataSource = dCliente.centroConsultarXCliente(ddlClientes.SelectedValue);
            ddlCentroCosto.DataTextField = "cen_nombre";
            ddlCentroCosto.DataValueField = "ID_centroCosto";
            ddlCentroCosto.DataBind();
        }
        ListItem lt = new ListItem();
        lt.Text = "Seleccione uno";
        lt.Value = "-1";
        ddlCentroCosto.Items.Insert(0, lt);
    }

    protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarCentros();
        urlReporte();
    }

    protected void ddlcenEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        urlReporte();
    }

    void urlReporte()
    {
        string variables = "?ud=CDQ290CLK2DF09KDFS0932OP";
        variables += "&IdEmp=" + ddlcenEmpresa.SelectedValue ;

        if (ddlClientes.SelectedValue != "-1")
            variables += "&IdCli=" + ddlClientes.SelectedValue;
        else
            variables += "&IdCli=0";

        if (ddlCentroCosto.SelectedValue != "-1")
            variables += "&IdCen=" + ddlCentroCosto.SelectedValue;

        btnGenerar.Attributes.Add("onclick", "javascript:url('R_CarteraGeneral.aspx" + variables  + "');");
    }


    void urlReporteMovimientos()
    {
        string variables = "?ud=CDQ290CLK2DF09KDFS0932OP";
        variables += "&IdEmp=" + ddlmovEmpresa.SelectedValue;

        if (ddlmovClientes.SelectedValue != "-1")
            variables += "&IdCli=" + ddlmovClientes.SelectedValue;
        else
            variables += "&IdCli=0";

        if (ddlmovTipoMovimiento.SelectedValue != "-1")
            variables += "&IdMov=" + ddlmovTipoMovimiento.SelectedValue;
        else
            variables += "&IdMov=0";

        btnGeneraMovimientos.Attributes.Add("onclick", "javascript:url('R_Movimientos.aspx" + variables + "&fInicio=" + txtMovFechaInicio.Text + "&fFin=" + txtMovFechaFin.Text + "');");
    }

    protected void ddlmovEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        urlReporteMovimientos();
    }

    protected void ddlmovClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        urlReporteMovimientos();
    }

    protected void ddlmovTipoMovimiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        urlReporteMovimientos();
    }

    protected void PopCalendar2_SelectionChanged(object sender, EventArgs e)
    {
        urlReporteMovimientos();
    }
    protected void PopCalendar1_SelectionChanged(object sender, EventArgs e)
    {
        urlReporteMovimientos();
    }

    protected void btnGeneraListaEmpleados_Click(object sender, EventArgs e)
    {

    }

    protected void ExportarAExcel(object sender, ImageClickEventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=MesaVotos.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView gdvMesas = new GridView();
            //To Export all pages
            gdvMesas.AllowPaging = false;
            //this.cargarDatosMesas();

            gdvMesas.HeaderRow.BackColor = Color.Silver;
            gdvMesas.HeaderRow.ForeColor = Color.White;

            foreach (TableCell cell in gdvMesas.HeaderRow.Cells)
            {
                cell.BackColor = gdvMesas.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in gdvMesas.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gdvMesas.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gdvMesas.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            gdvMesas.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {

    }

}
