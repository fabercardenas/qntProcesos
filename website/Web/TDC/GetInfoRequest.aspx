<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="GetInfoRequest.aspx.cs" Inherits="GetInfoRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Sincronizar Ubicaciones Paso 3</div>
    <br />
    <div class="alert alert-warning">
        Sincronizar ubicaciones
        <br />
        
    </div>
    <div class="form-inline">
        <asp:Button ID="btnCargar" runat="server" CssClass="btn btn-info" Text="Sincronizar ubicaciones con Salesforce" OnClick="btnCargar_Click" />
    </div>

    <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
    
</asp:Content>

