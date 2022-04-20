<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="LoadFile.aspx.cs" Inherits="TDC_LoadFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Carga de archivo paso 1</div>
    <br />
    <div class="alert alert-info">
        En este módulo iniciará el proceso para las Tarjetas Aprobadas y cargará el archivo con los clientes aprobados
        <br />
        Puede descargar un formato de ejemplo carga pulsando click sobre el ícono
        <a href="../../Formatos/FormatoTDC_P1.xlsx" target="_blank">
         <span class="glyphicon glyphicon-save" style="font-size:19px;"></span>
            </a>
    </div>
    <div class="form-inline">
        Seleccione el archivo   <asp:FileUpload ID="fupArchivo" runat="server" CssClass="form-control" />
                <asp:Button ID="btnCargar" runat="server" CssClass="btn btn-success" Text="Cargar archivo" OnClick="btnCargar_Click" />
                <asp:LinkButton ID="lnbValidar" runat="server" CssClass="btn btn-green btn-block">
                        <span class="glyphicon glyphicon-ok pull-left"></span>&nbsp;Validar empleados sin nomina
                 </asp:LinkButton>

    </div>

    <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
    
</asp:Content>

