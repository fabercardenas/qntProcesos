<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="GetDebtDays.aspx.cs" Inherits="GetDebtDays" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Sincronización de Días Mora - Paso 8.1</div>
    <br />
    <div class="alert alert-info">
        En este módulo solicitará la sincronización con <b>Salesforce</b>  para traer la información de los días en mora y las cuotas pendientes de pago asociada a los clientes.<br />
    </div>
    <div class="form-inline">
        <asp:LinkButton ID="lnbCargar" runat="server" CssClass="btn btn-success" OnClick="lnbCargar_Click">
                        <span class="glyphicon glyphicon-refresh"></span>
                        &nbsp;Sincronizar Días Mora con Salesforce
                 </asp:LinkButton>
    </div>
    <br />
    <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
    
</asp:Content>

