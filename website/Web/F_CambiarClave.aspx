<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="F_CambiarClave.aspx.cs" Inherits="Web_F_CambiarClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table cellSpacing="0" cellPadding="0" align="center" border="0">
	<tr>
		<td class="tdImgVerde03" align="left" colSpan="2">Cambio de 
			Contraseña&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
	</tr>
	<tr>
		<td colSpan="2" align="left" class="textError" style="padding:20px; background-color:white;">
			<asp:label CssClass="textError" ForeColor="Red" id="lbl_error_usuario" runat="server" Font-Bold="true"></asp:label></td>
	</tr>
	<tr>
		<td>
			<table id="tablaCambio" style="width:100%; background-color:white;" align="center" border="0" runat="server">
				<tr>
					<td class="tdGris01" style="width:200px; padding:20px;">Contraseña actual:
					</td>
					<td style="padding:10px;">
						<asp:textbox id="tb_clave_actual" runat="server" CssClass="form-control" TextMode="Password"></asp:textbox>
						<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="digite su clave actua"
							ControlToValidate="tb_clave_actual">*</asp:RequiredFieldValidator>
					</td>
				</tr>
				<tr>
					<td class="tdGris01" style="padding:20px;">Nueva contraseña:</td>
					<td style="padding:10px;">
						<asp:TextBox id="tb_clave_nueva" runat="server" CssClass="form-control" 
                            TextMode="Password" MaxLength="20"></asp:TextBox>
						<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Digite una nueva clave"
							ControlToValidate="tb_clave_nueva">*</asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td class="tdGris01" style="padding:20px;">Confimar nueva 
						contraseña:</td>
					<td style="padding:10px;">
						<asp:TextBox id="tb_confirma" runat="server" CssClass="form-control" 
                            TextMode="Password" MaxLength="20"></asp:TextBox>
						<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="Las claves no coinciden" ControlToValidate="tb_confirma"
							ControlToCompare="tb_clave_nueva">*</asp:CompareValidator></td>
				</tr>
				<tr>
					<td align="center" colspan="2" style="padding:10px;">
                        <asp:Button ID="btnActualizar" runat="server" 
                            Text="Actualizar" CssClass="btn btn-success" onclick="btnActualizar_Click"></asp:Button></td>
				</tr>
    </table>
		</td>
	</tr>
  </table>
</asp:Content>

