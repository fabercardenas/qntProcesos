<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="ActivationTDC.aspx.cs" Inherits="TDC_ActivationTDC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Carga de Archivo para Activación TDC - Paso 9</div>
    <br />
    <div class="alert alert-info">
        En este módulo se activarán las Tarjetas Aprobadas cargando el archivo suministrado por la entidad respectiva.
        <br />
        Al finalizar el proceso prodrá generar el archivo de validación de Activación.
        <br />
        <br />
        Puede descargar un formato de ejemplo carga pulsando click sobre el ícono
        <a href="../../Formatos/FormatoTDC_P9.xlsx" target="_blank">
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
    <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>

    <div class="form-inline" id="dvDescarga" runat="server" visible="false">
        <asp:LinkButton ID="lnbDescargar" runat="server" CssClass="btn btn-primary" OnClick="lnbDescargar_Click">
            <span class="glyphicon glyphicon-arrow-down"></span>
            &nbsp;Descargar Archivo
        </asp:LinkButton>
    </div>
    
</asp:Content>

