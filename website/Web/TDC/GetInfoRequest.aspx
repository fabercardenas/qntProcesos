<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="GetInfoRequest.aspx.cs" Inherits="GetInfoRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Sincronización de Ubicaciones - Paso 3</div>
    <br />
    <div class="alert alert-warning">
        En este módulo solicitará la sincronización con SALESFORECE para traer la información de ubicaiones asociada a los clientes<br />
        que han finalizado los pasos anteriores<br />
        Campos a sincronizar: Dirección y Ciudad de Residencia, Teléfono y Celular de contacto
        <br />
        <br />
        
    </div>
    <div class="form-inline">
        <asp:LinkButton ID="lnbCargar" runat="server" CssClass="btn btn-info" OnClick="lnbCargar_Click">
                        <span class="glyphicon glyphicon-refresh"></span>
                        &nbsp;Sincronizar ubicaciones con Salesforce
                 </asp:LinkButton>
    </div>

    <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
    
</asp:Content>

