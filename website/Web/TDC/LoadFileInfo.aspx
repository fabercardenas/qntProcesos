﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="LoadFileInfo.aspx.cs" Inherits="TDC_LoadFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Carga de archivo paso 2</div>
    <br />
    <div class="alert alert-warning">
        En este módulo cargará la informacion complementaria de tarjeta de crédito
        <br />
        Al finalizar el proceso de carga se generará automáticamente un archivo para validar 
        <br />
        que registros fueron insertados correctamente y cuales deben ser validados nuevamente.
        <br />
        <br />
        Puede descargar un formato de ejemplo carga pulsando click sobre el ícono
        <a href="../../Formatos/FormatoTDC_P2.xlsx" target="_blank">
         <span class="glyphicon glyphicon-save" style="font-size:19px;"></span>
            </a>
    </div>
    <div class="form-inline">
        Seleccione el archivo   <asp:FileUpload ID="fupArchivo" runat="server" CssClass="form-control" />
                <asp:LinkButton ID="lnbCargar" runat="server" CssClass="btn btn-success" OnClick="lnbCargar_Click">
                        <span class="glyphicon glyphicon-resize-vertical"></span>
                        &nbsp;Cargar Archivo
                 </asp:LinkButton>

    </div>

    <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
    
</asp:Content>
