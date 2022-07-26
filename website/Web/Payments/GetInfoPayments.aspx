<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="GetInfoPayments.aspx.cs" Inherits="GetInfoPayments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="alert alert-warning">
        En este módulo solicitará la sincronización con SALESFORECE para traer la información de Clientes, Acuerdos, Productos por acuerdo, Plan de pagos y Registro de Pagos<br />
    </div>
    <div class="form-inline">
        <asp:LinkButton ID="lnbCargar" runat="server" CssClass="btn btn-info" OnClick="lnbCargar_Click">
                        <span class="glyphicon glyphicon-refresh"></span>
                        &nbsp;Sincronizar con Salesforce
                 </asp:LinkButton>
    </div>
    <br />
    <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
    
</asp:Content>

