<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="LoadFileInfo.aspx.cs" Inherits="TDC_LoadFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Carga de Archivo - Paso 2</div>
    <br />
    <div class="alert alert-warning">
        En este módulo cargará la informacion complementaria asociada a la Tarjeta de Crédito para los clientes
        <br />
        Al finalizar el proceso de carga se generará automáticamente un archivo para validar 
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
    <br />
    <div class="col-md-9">
        <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
    </div>
    <div class="col-md-3">
        <asp:LinkButton ID="lnbExportarResultados" runat="server" CssClass="btn btn-lg btn-primary" Visible="false" OnClick="lnbExportarResultados_Click">
            <span class="glyphicon glyphicon-indent-left"></span>
            &nbsp;Exportar resultados
        </asp:LinkButton>
    </div>
    
</asp:Content>

