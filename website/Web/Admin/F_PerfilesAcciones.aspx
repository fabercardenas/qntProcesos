<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="F_PerfilesAcciones.aspx.cs" Inherits="Web_Admin_F_Perfiles" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 150px;
            height: 22px;
        }
        .auto-style2 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:70%;" align="center">
        <tr><td class="tdImgVerde03" colspan="2">Administración de Acciones Por Perfiles</td></tr>
        <tr id="trDatos" runat="server">
            <td>
                Consultar permisos del modulo&nbsp; <asp:DropDownList ID="ddlFiltroPaginas" runat="server" CssClass="BotonTexto"></asp:DropDownList>
                para el perfil <asp:DropDownList ID="ddlFiltroPerfiles" runat="server" CssClass="BotonTexto"></asp:DropDownList>
                <asp:Button ID="btnConsultar" runat="server" CssClass="BotonEnviar" Height="27px" OnClick="btnConsultar_Click" Text="Consultar" />
                <br />
                <br />
                <asp:HiddenField ID="hdfIdPerfil" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top;">
                <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
                <asp:CheckBoxList ID="chkAcciones" runat="server" RepeatColumns="2" CssClass="checkbox" CellPadding="5" CellSpacing="5">
                </asp:CheckBoxList>
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-warning" Text="Guardar y actualizar" Visible="false" OnClick="btnGuardar_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
