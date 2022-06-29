using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using Ionic.Zip;
using OfficeOpenXml;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System.Net.Http.Headers;
using Negocio;
using System.Threading.Tasks;


public partial class PYS_FirstApproval : System.Web.UI.Page
{
    Negocio.NPYS nPYS = new Negocio.NPYS();

    #region OBJETOS 
        int fNumeroRadicado = 0;
        int fTipoDocumento = 1;
        int fNumeroDocumento = 2;
        int fNumeroAcuerdo = 3;
        int fNombresProductos = 4;
        int fFechaUltimoPago = 5;
        int fFechaSincronizacion = 6;
        int fAprobar = 7;
        int fCheckAprobar = 8;
        int fCausalAsesor = 9;
        int fRechazar = 10;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"]!=null) && (!Page.IsPostBack))
        {
            dvIdConsulta.Visible = true;
            ConsultarPazySalvoXemitir();
        }
    }

    protected async void lnbCargar_Click(object sender, EventArgs e)
    {
        //buscar las cedulas que esten sin actualizar
        Negocio.NPYS nPYS = new NPYS();
        Dictionary<string, string> resultado = await nPYS.Sincronizar(Session["ID_usuario"].ToString());
        ltrMensaje.Text = "";
        foreach (var mensaje in resultado)
        {
            //storedProcCommand.Parameters.AddWithValue(mensaje.Key.ToString(), parametro.Value);
            if (mensaje.Key.ToString() == "warning")
                ltrMensaje.Text += NMessaging.Warning(mensaje.Value);

            if (mensaje.Key.ToString() == "error")
                ltrMensaje.Text += NMessaging.Error(mensaje.Value);

            //if (resultado.ContainsKey("success"))
            if (mensaje.Key.ToString() == "success")
                ltrMensaje.Text += NMessaging.Success(mensaje.Value);
        }

    }

    void ConsultarPazySalvoXemitir()
    {
        int indicadorVarios = 0;
        int indicadorQuita = 0;
        gdvListaPazySalvo.DataSource = nPYS.ConsultarPazySalvoXemitir();
        gdvListaPazySalvo.DataBind();
        ltrTotalRegistros.Text = "(" + gdvListaPazySalvo.Rows.Count.ToString() + ") ";

        string myScript = "<script language='JavaScript'>function CheckActualizaVarios(Checkbox) {" +
                              " var GridVwHeaderChckbox = document.getElementById(\"" + gdvListaPazySalvo.ClientID.ToString() + "\");" +
                              "  for (i = 0; i < GridVwHeaderChckbox.rows.length; i++) " +
                              "  {  GridVwHeaderChckbox.rows[i].cells[" + (indicadorVarios - indicadorQuita).ToString() + "].getElementsByTagName(\"INPUT\")[0].checked = Checkbox.checked; } " +
                              "} </script>";
    }

    void ConsultarPazySalvos(string pys_estado, string filtro, string txtFechaIni, string txtFechaFin)
    {
        int indicadorVarios = 0;
        int indicadorQuita = 0;
        gdvListaPazySalvo.DataSource = nPYS.ConsultarPazySalvoXemitir();
        gdvListaPazySalvo.DataBind();
        ltrTotalRegistros.Text = "(" + gdvListaPazySalvo.Rows.Count.ToString() + ") ";

        string myScript = "<script language='JavaScript'>function CheckActualizaVarios(Checkbox) {" +
                              " var GridVwHeaderChckbox = document.getElementById(\"" + gdvListaPazySalvo.ClientID.ToString() + "\");" +
                              "  for (i = 0; i < GridVwHeaderChckbox.rows.length; i++) " +
                              "  {  GridVwHeaderChckbox.rows[i].cells[" + (indicadorVarios - indicadorQuita).ToString() + "].getElementsByTagName(\"INPUT\")[0].checked = Checkbox.checked; } " +
                              "} </script>";
    }

    protected void ddlFiltroFecha_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltrMensaje.Text = "";
        ltrMensaje.Visible = false;
        if (ddlFiltroFecha.SelectedValue != "-1")
            dvdFechas.Visible = true;
        else
            dvdFechas.Visible = false;
    }

    protected void gdvListaPazySalvo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdvListaPazySalvo.EditIndex = e.NewEditIndex;
        ConsultarPazySalvoXemitir();
        gdvListaPazySalvo.Columns[fAprobar].Visible = false;
        gdvListaPazySalvo.Columns[fCheckAprobar].Visible = false;
        gdvListaPazySalvo.Columns[fCausalAsesor].Visible = true;

    }

    protected void gdvListaPazySalvo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvListaPazySalvo.EditIndex = -1;
        ConsultarPazySalvoXemitir();
        gdvListaPazySalvo.Columns[fAprobar].Visible = true;
        gdvListaPazySalvo.Columns[fCheckAprobar].Visible = true;
        gdvListaPazySalvo.Columns[fCausalAsesor].Visible = false;
    }

    protected void gdvIncapacidades_RowUpdating(object sender, GridViewCancelEditEventArgs e)
    {
        gdvListaPazySalvo.EditIndex = e.RowIndex;
        GridViewRow Row = gdvListaPazySalvo.Rows[e.RowIndex];
        TextBox CausalAsesor = (TextBox)Row.Cells[fCausalAsesor].Controls[0];

    }

    protected void lnbListaConsultar_Click(object sender, EventArgs e)
    {
        ConsultarPazySalvos(string pys_estado, string filtro, string txtFechaIni, string txtFechaFin);
    }
}
