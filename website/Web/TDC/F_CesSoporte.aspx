<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="F_CesSoporte.aspx.cs" Inherits="Web_Archivos_F_CesSoporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tablaborde" id="Table1" style="WIDTH: 680px; HEIGHT: 135px" align="center" border="0">
	<tr>
		<td class="tdTituloModulo03" colspan="2">Cargar Soporte de Pago de Cesantias</td>
	</tr>
    <tr>
        <td colspan="2">
            <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td class="tdGris01" style="width: 250px">
            Operador</td>
        <td class="tdGris02">
            <asp:DropDownList ID="ddlOperador" runat="server" CssClass=" FormDropDown">
                <asp:ListItem Value="AportesEnLinea" Selected="True">Aportes en linea</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
		<td class="tdGris01">Seleccione el archivo Excel</td>
		<td class="tdGris02">
            <asp:FileUpload ID="fupArchivoExcel" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="fupArchivoExcel"
                ErrorMessage="*"></asp:RequiredFieldValidator></td>
	</tr>
    <tr>
        <td class="tdGris01">Seleccione el archivo PDF</td>
        <td class="tdGris02">
            <asp:FileUpload ID="archivoPDF" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="archivoPDF"
                ErrorMessage="*"></asp:RequiredFieldValidator></td>
    </tr>
	<tr>
		<td align="center" colspan="2">
            <asp:Button ID="btnCargar" runat="server" Text="Cargar Archivos" CssClass="btn btn-success" Height="35px" Width="143px" OnClick="btnCargar_Click" />
            <br />
            <asp:Label ID="lblMensaje" runat="server" CssClass="textError"></asp:Label>
		</td>
	</tr>
</table>
<asp:Label ID="lblArchivoPDF" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lblIdPeriodo" runat="server" Visible="False"></asp:Label>
</asp:Content>

