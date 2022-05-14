<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="GetDigitalFlow.aspx.cs" Inherits="GetDigitalFlow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Sincronización de Flujo Digital</div>
    <br />
    <div class="alert alert-warning">
        En este módulo solicitará la sincronización con SALESFORECE para traer la información de Flujo Difital asociada a los clientes.<br />
    </div>
    <div class="form-inline">
        <asp:LinkButton ID="lnbCargar" runat="server" CssClass="btn btn-info" OnClick="lnbCargar_Click">
                        <span class="glyphicon glyphicon-refresh"></span>
                        &nbsp;Sincronizar FD con Salesforce
                 </asp:LinkButton>
    </div>
    <br />
    <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
    
</asp:Content>

