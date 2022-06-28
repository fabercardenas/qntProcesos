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

    protected void ddlFiltroFecha_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltrMensaje.Text = "";
        ltrMensaje.Visible = false;
        if (ddlFiltroFecha.SelectedValue != "-1")
            dvdFechas.Visible = true;
        else
            dvdFechas.Visible = false;
    }
    #region GENERACIÓN ARCHIVO DE ACTIVACIÓN

    #endregion
}
