using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class WebForms_Reportes_R_EnvioInterrapidisimo : System.Web.UI.Page
{ 
    Negocio.NReportes nReporte = new Negocio.NReportes();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((!Page.IsPostBack) && (Request.QueryString["idEmp"] != null))
            consultarDatos();
        else
            CerrarVentana();
    }

    void consultarDatos()
    {
        Label lblTituloReporte = (Label)this.Master.FindControl("lblTitulo");
        lblTituloReporte.Text = "Reporte de Empleados General";                  
        
        Literal ltrGeneradoPor = (Literal)this.Master.FindControl("ltrGeneradoPor");
        ltrGeneradoPor.Text = "<span style='font-weight:normal;'>Generado por : " + Session["nombreUsuario"].ToString() + "</span>";

        Literal ltrFechaGenerado = (Literal)this.Master.FindControl("ltrFechaGenerado");
        ltrFechaGenerado.Text = "<span style='font-weight:normal;'>Fecha Generado: " + string.Format("{0:yyyy-MM-dd}", DateTime.Now) + "</span>";

        Literal ltrHoraGenerado = (Literal)this.Master.FindControl("ltrHoraGenerado");
        ltrHoraGenerado.Text = "<span style='font-weight:normal;'>Hora Generado: " + string.Format("{0:HH:mm}", DateTime.Now) + "</span>";

        DataTable tabla = nReporte.empGeneral(Request.QueryString["idEmp"].ToString(), Request.QueryString["filCampo"].ToString(), Request.QueryString["filFiltro"].ToString());
        if (tabla.Rows.Count > 0)
        {
            if (Request.QueryString["idEmp"].ToString() != "-1")
            {
                gdvDatos.Columns[0].Visible = false;
                lblTituloReporte.Text += "<br />" + tabla.Rows[0]["emp_nombre"].ToString();
            }

            if (Request.QueryString["filCampo"].ToString() == "ID_empresaUsuariaFK")
            { //Quito el nombre de la empresa de las columnas
                gdvDatos.Columns[1].Visible = false;
                lblTituloReporte.Text += "<br />" + tabla.Rows[0]["cli_nombre"].ToString();
            }

            Image repImgLogo = (Image)Master.FindControl("imgLogoEmpresa");
            if ((tabla.Rows.Count > 0) && (Request.QueryString["idEmp"].ToString() != "-1"))
                repImgLogo.ImageUrl = ConfigurationManager.AppSettings["RutaLogo"] + tabla.Rows[0]["emp_logoSmall"].ToString();
            else
                repImgLogo.ImageUrl = ConfigurationManager.AppSettings["RutaLogo"] + "logoReciboCaja.png";
            
        }
        gdvDatos.DataSource = tabla;
        gdvDatos.DataBind();
    }

    public string CerrarVentana()
    {
        string script = "window.close();";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cerrar", script, true);
        return "";
    }

    public string estadoContrato(int con_estado)
    {
        switch (con_estado)
        {
            case 1:
                return "Activo";
            case 0:
                return "Inactivo";
            case -1:
                return "Anulado";
            default:
                return "";
        }
    }
}