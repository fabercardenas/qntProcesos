<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="LoadPromissory.aspx.cs" Inherits="TDC_LoadPromissory" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="dvIdCargaArchivo">
        <div class="tdImgVerde04">Carga de Pagaré</div>
        <br />
        <div class="alert alert-info">
            Este módulo le permitirá cargar la infromación asociada al <b>Pagaré</b>
            <br />
            Puede descargar un formato de ejemplo pulsando click sobre el ícono
            <a href="../../Formatos/FormatoTDC_Pagare.csv" target="_blank">
             <span class="glyphicon glyphicon-save" style="font-size:19px;"></span>
                </a>
        </div>
        <asp:Literal ID="ltrNombreArchivo" runat="server" Visible="false"></asp:Literal>
        <div class="form-inline">
            Seleccione el archivo <asp:FileUpload ID="fupArchivo" runat="server" CssClass="form-control" />
                    <asp:LinkButton ID="lnbCargar" runat="server" CssClass="btn btn-success" OnClick="lnbCargar_Click">
                            <span class="glyphicon glyphicon-open"></span>
                            &nbsp;Cargar Archivo
                     </asp:LinkButton>
            <br />
        </div>
        <br />
        <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
    </div>
</asp:Content>

