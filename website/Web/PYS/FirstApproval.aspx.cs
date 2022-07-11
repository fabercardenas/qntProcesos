﻿using System;
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
        int fCausalAsesor = 7;
        int fCausalSupervisor = 8;
        int fAprobar = 9;
        int fCheckAprobar = 10;
        int fRechazar = 11;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"]!=null) && (!Page.IsPostBack))
        {
            ltrMensaje.Text = "";
            txtListaDocumento.Text = "";
            txtFechaIni.Text = "";
            txtFechaFin.Text = "";
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
        gdvListaPazySalvo.DataSource = nPYS.ConsultarPazySalvoXemitir(ddlListaEstados.SelectedValue, ddlFiltroFecha.SelectedValue, txtFechaIni.Text, txtFechaFin.Text, txtListaDocumento.Text, "Asesor");
        gdvListaPazySalvo.DataBind();
        ltrTotalRegistros.Text = "(" + gdvListaPazySalvo.Rows.Count.ToString() + ") ";

        string myScript = "<script language='JavaScript'>function CheckActualizaVarios(Checkbox) {" +
                              " var GridVwHeaderChckbox = document.getElementById(\"" + gdvListaPazySalvo.ClientID.ToString() + "\");" +
                              "  for (i = 0; i < GridVwHeaderChckbox.rows.length; i++) " +
                              "  {  GridVwHeaderChckbox.rows[i].cells[" + (indicadorVarios - indicadorQuita).ToString() + "].getElementsByTagName(\"INPUT\")[0].checked = Checkbox.checked; } " +
                              "} </script>";

        Negocio.NUtilidades.SeguridadPerfilOcultar(Session["AccionesNoPermitidas"] as DataTable, Form.FindControl("ContentPlaceHolder1"), "PYS-->Validar Asesor", Session["ID_perfilFK"].ToString());
        gdvListaPazySalvo.Columns[fAprobar].Visible = Negocio.NUtilidades.SeguridadPerfilOcultarEnGrilla(Session["AccionesNoPermitidas"] as DataTable, "PYS-->Validar Asesor", "gdv_AprobarAsesor", gdvListaPazySalvo.Columns[fAprobar].Visible);
        gdvListaPazySalvo.Columns[fRechazar].Visible = Negocio.NUtilidades.SeguridadPerfilOcultarEnGrilla(Session["AccionesNoPermitidas"] as DataTable, "PYS-->Validar Asesor", "gdv_RechazarAsesor", gdvListaPazySalvo.Columns[fRechazar].Visible);
        gdvListaPazySalvo.Columns[fCheckAprobar].Visible = Negocio.NUtilidades.SeguridadPerfilOcultarEnGrilla(Session["AccionesNoPermitidas"] as DataTable, "PYS-->Validar Asesor", "lnbActualizarVarios", gdvListaPazySalvo.Columns[fCheckAprobar].Visible);
    }

    protected void ddlFiltroFecha_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtListaDocumento.Text = ""; 
        ltrMensaje.Text = "";
        if (ddlFiltroFecha.SelectedValue != "-1")
        {
            dvdFechas.Visible = true;
        }
        else
        {
            txtFechaIni.Text = "";
            txtFechaFin.Text = "";
            dvdFechas.Visible = false;
        }
    }

    protected void lnbListaConsultar_Click(object sender, EventArgs e)
    {
        if (txtListaDocumento.Text == "")
        {
            if ((ddlFiltroFecha.SelectedValue != "-1") && ((!Negocio.NUtilidades.IsDate(txtFechaIni.Text)) || (!Negocio.NUtilidades.IsDate(txtFechaFin.Text))))
            {
                ltrMensaje.Text = NMessaging.Error("Valide los campos de las fechas");
                return;
            }
            ltrMensaje.Text = "";
            ConsultarPazySalvoXemitir();
        }
        else
        {
            ddlListaEstados.SelectedValue = "-1";
            ddlFiltroFecha.SelectedValue = "-1";
            txtFechaIni.Text = "";
            txtFechaFin.Text = "";
            dvdFechas.Visible = false;
            ConsultarPazySalvoXemitir();
        }
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
        gdvListaPazySalvo.Columns[fAprobar].Visible = true;
        gdvListaPazySalvo.Columns[fCheckAprobar].Visible = true;
        ConsultarPazySalvoXemitir();
    }

    protected void gdvListaPazySalvo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gdvListaPazySalvo.EditIndex = e.RowIndex;
        GridViewRow Row = gdvListaPazySalvo.Rows[e.RowIndex];
        string idAcuerdo = gdvListaPazySalvo.DataKeys[e.RowIndex].Values[0].ToString();
        TextBox txtCausalRechazo = (TextBox)gdvListaPazySalvo.Rows[e.RowIndex].FindControl("txtCausalRechazo");
        nPYS.ActualizarXacuerdoRechazo(idAcuerdo, txtCausalRechazo.Text, Session["ID_usuario"].ToString(), "Asesor");
        ltrMensaje.Text = Messaging.Success("Acuerdo rechazado y actualizado para validación del supervisor");
        gdvListaPazySalvo.EditIndex = -1;
        gdvListaPazySalvo.Columns[fAprobar].Visible = true;
        gdvListaPazySalvo.Columns[fCheckAprobar].Visible = true;
        ConsultarPazySalvoXemitir();
    }

    protected void gdvListaPazySalvo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ltrMensaje.Text = "";
        int index = 0;
        int RowIndex = 0;
        if (e.CommandName != "Page")
        {
            index = Convert.ToInt32(e.CommandArgument);
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            RowIndex = oItem.RowIndex;
        }

        switch (e.CommandName)
        {
            case "Aprobar":
                nPYS.ActualizarXacuerdoAprobar(gdvListaPazySalvo.DataKeys[RowIndex].Values[0].ToString(), Session["ID_usuario"].ToString(), "Asesor");
                ConsultarPazySalvoXemitir();
                ltrMensaje.Text = Messaging.Success("Acuerdo actualizado para validación del supervisor");
                break;
            default:
                break;
        }
    }

    protected void lnbActualizarVarios_Click(object sender, EventArgs e)
    {
        int modificados = 0;

        #region RECORRO LOS CHECKBOX Y ACTUALIZO
        foreach (GridViewRow row in gdvListaPazySalvo.Rows)
        {
            CheckBox check = row.FindControl("chkActualizaVarios") as CheckBox;
            if (check.Checked)
            {
                switch (lnbActualizarVarios.Text)
                {
                    case "Aprobar varios":
                        nPYS.ActualizarXacuerdoAprobar(gdvListaPazySalvo.DataKeys[row.RowIndex].Values[0].ToString(), Session["ID_usuario"].ToString(), "Asesor");
                        modificados++;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region SI ACTUALICÉ ALGO, CARGO LA GRILLA Y EL MENSAJE
        if (modificados > 0)
        {
            switch (lnbActualizarVarios.Text)
            {
                case "Aprobar Varios":
                    ltrMensaje.Text = Messaging.Success(modificados.ToString() + " acuerdos aprobadas con éxito");
                    break;
                default:
                    break;
            }
            ConsultarPazySalvoXemitir();
        }
        else
            ltrMensaje.Text = Messaging.Error("Por favor seleccione uno o varios acuerdos para actualizar");

        #endregion
    }
}
