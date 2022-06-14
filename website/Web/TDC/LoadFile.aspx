<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="LoadFile.aspx.cs" Inherits="TDC_LoadFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Carga Tarjetas Aprobadas - Paso 1</div>
    <br />
    <div class="alert alert-info">
        En este módulo iniciará el proceso para las Tarjetas Aprobadas y cargará el archivo con los clientes aprobados.
        <br />
        Automáticamente se sincronizará con SALESFORECE y traerá Nombres, Apellidos y cupo de los clientes aprobados en el archivo de carga.
        <br />
        Al finalizar el proceso prodrá generar el archivo para entragar a Finandina.
        <br />
        <br />
        Puede descargar un formato de ejemplo carga pulsando click sobre el ícono
        <a href="../../Formatos/FormatoTDC_P1.xlsx" target="_blank">
         <span class="glyphicon glyphicon-save" style="font-size:19px;"></span>
            </a>
    </div>
    <asp:Literal ID="ltrNombreArchivo" runat="server" Visible="false"></asp:Literal>
    <div class="form-inline">
        Seleccione el archivo   <asp:FileUpload ID="fupArchivo" runat="server" CssClass="form-control" />
                <asp:LinkButton ID="lnbCargar" runat="server" CssClass="btn btn-success" OnClick="lnbCargar_Click">
                        <span class="glyphicon glyphicon-arrow-up"></span>
                        &nbsp;Cargar Archivo
                 </asp:LinkButton>
        <br />
    </div>
    <br />
    <div class="col-md-9">
        <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
    </div> 
    <div class=" col-md-3 form-inline" id="dvDescarga" runat="server" visible="false">
        <asp:LinkButton ID="lnbDescargar" runat="server"  CssClass="btn btn-lg btn-primary" OnClick="lnbDescargar_Click">
            <span class="glyphicon glyphicon-arrow-down"></span>
            &nbsp;Descargar Archivo
        </asp:LinkButton>
    </div>
    
</asp:Content>

