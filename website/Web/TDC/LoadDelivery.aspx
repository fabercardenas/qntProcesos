<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="LoadDelivery.aspx.cs" Inherits="TDC_LoadDelivery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Carga de Archivo Soporte de Entrega</div>
    <br />
    <div class="alert alert-info">
        En este módulo cargará el archivo de soporte de entrega suministrado por el proveedor de repartición de
        <br />
        Tarjetas de Crédito.
        <br />
        <br />
        Puede descargar un formato de ejemplo carga pulsando click sobre el ícono
        <a href="../../Formatos/FormatoTDC_P6.xlsx" target="_blank">
         <span class="glyphicon glyphicon-save" style="font-size:19px;"></span>
            </a>
    </div>
    <asp:Literal ID="ltrNombreArchivo" runat="server" Visible="false"></asp:Literal>
    <div class="form-inline">
        Seleccione el archivo   <asp:FileUpload ID="fupArchivo" runat="server" CssClass="form-control" />
                <asp:LinkButton ID="lnbCargar" runat="server" CssClass="btn btn-success" OnClick="lnbCargar_Click">
                        <span class="glyphicon glyphicon-open"></span>
                        &nbsp;Cargar Archivo
                 </asp:LinkButton>
        <br />
    </div>
    <br />
    <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>

    <div class="form-inline" id="dvDescarga" runat="server" visible="false">
        <asp:LinkButton ID="lnbDescargar" runat="server" CssClass="btn btn-primary" OnClick="lnbDescargar_Click">
            <span class="glyphicon glyphicon-arrow-down"></span>
            &nbsp;Descargar Archivo
        </asp:LinkButton>
    </div>
    
</asp:Content>

