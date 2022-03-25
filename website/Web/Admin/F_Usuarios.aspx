<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="F_Usuarios.aspx.cs" Inherits="Web_Admin_F_Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:790px;" align="center">
        <tr><td class="tdImgVerde03">Edición de Usuarios</td></tr>
        <tr>
			<td style="height: 39px" class="tdGris02"><asp:label id="lblMensaje" CssClass="textError" runat="server"></asp:label>
			    <br />Filtrar
                <asp:DropDownList ID="ddlFiltroEstado" runat="server" AutoPostBack="True" 
                    CssClass="BotonTexto" 
                    onselectedindexchanged="ddlFiltroEstado_SelectedIndexChanged">
                    <asp:ListItem Value="1" Selected="True">Usuarios Activos</asp:ListItem>
                    <asp:ListItem Value="0">Usuarios Inactivos</asp:ListItem>
                </asp:DropDownList>
           </td>
		</tr>
        <tr><td id="tdInicial" runat="server" ><br />
            <asp:GridView ID="gdvUsuarios" runat="server" BackColor="White"  
                AutoGenerateColumns="False" AllowPaging="true"
                GridLines="Horizontal" BorderColor="#ECECEC"
                DataKeyNames="ID_usuario" PageSize="15" 
                CssClass="gdvTableStyle"  
                OnRowEditing="EditarUsuario" 
                OnPageIndexChanging="PaginarUsuario"  
                >
                <Columns>
                    <asp:BoundField DataField="usr_nombre1" HeaderText="Primer Nombre"></asp:BoundField>
					<asp:BoundField DataField="usr_nombre2" HeaderText="Segundo Nombre"></asp:BoundField>
					<asp:BoundField DataField="usr_apellido1" HeaderText="Primer Apellido"></asp:BoundField>
					<asp:BoundField DataField="usr_apellido2" HeaderText="Segundo Apellido"></asp:BoundField>
					<asp:BoundField DataField="usr_userName" HeaderText="Usuario" SortExpression="usr_userName"></asp:BoundField>
					<asp:BoundField DataField="per_nombre" HeaderText="Perfil" SortExpression="per_nombre"></asp:BoundField>
                    <asp:CommandField ButtonType="Button" EditText="Editar" ShowEditButton="True">
                    <ControlStyle CssClass="BotonEnviarEnGrilla" />
                    </asp:CommandField>
                </Columns>
                <HeaderStyle CssClass="gdvHeaderstyle" />
                <RowStyle CssClass="gdvRowstyle" />
                <AlternatingRowStyle CssClass="gdvAltrowstyle" />
            </asp:GridView>
            <br />
            <asp:Button ID="btnCrear" runat="server" CssClass="btn btn-primary"
                onclick="btnCrear_Click" Text="Crear nuevo usuario" />
                
            </td>
        </tr>
        <tr><td id="tdEditar" runat="server">
            <table id="table3" style="WIDTH: 371px;" cellspacing="1" cellpadding="1"
					width="371" border="0">
					<tr>
						<td class="tdImgAzul01" colspan="2">Edición&nbsp;de&nbsp;Usuario
                            <asp:label id="lblIdUsuario" runat="server" Visible="false" />
                            <asp:label id="lblEdtClave" runat="server" Visible="false" />
                        </td>
					</tr>
					<tr>
						<td class="tdGris01">Documento:</td>
						<td class="tdGris02"><asp:label id="lblEdtDocumento" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td class="tdGris01">Primer Nombre:
							<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtEdtNombre1" ErrorMessage="RequiredFieldValidator">*</asp:requiredfieldvalidator></td>
						<td class="tdGris02"><asp:textbox id="txtEdtNombre1" CssClass="BotonTexto" runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="tdGris01">Segundo Nombre:</td>
						<td class="tdGris02"><asp:textbox id="txtEdtNombre2" CssClass="BotonTexto" runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="tdGris01">Primer Apellido:
							<asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtEdtApel1" ErrorMessage="RequiredFieldValidator">*</asp:requiredfieldvalidator></td>
						<td class="tdGris02" style="HEIGHT: 28px"><asp:textbox id="txtEdtApel1" CssClass="BotonTexto" runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="tdGris01">Segunto Apellido:</td>
						<td class="tdGris02"><asp:textbox id="txtEdtApel2" CssClass="BotonTexto" runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="tdGris01">Nombre de usuario:
							<asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtEdtUsuario" ErrorMessage="RequiredFieldValidator">*</asp:requiredfieldvalidator></td>
						<td class="tdGris02"><asp:textbox id="txtEdtUsuario" CssClass="BotonTexto" runat="server"></asp:textbox></td>
					</tr>
                    <tr>
						<td class="tdGris01">E-mail:<asp:requiredfieldvalidator 
                                id="RequiredFieldValidator8" runat="server" ControlToValidate="txtEdtMail" 
                                ErrorMessage="RequiredFieldValidator">*</asp:requiredfieldvalidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="txtInsMail" ErrorMessage="*" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
						<td class="tdGris02">
                            <asp:textbox id="txtEdtMail" CssClass="FormTextoGris" 
                                runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="tdGris01">Perfil:</td>
						<td class="tdGris02">
                            <asp:dropdownlist id="ddlEditPerfil" CssClass="FormDropDown" AutoPostBack="true" 
                                runat="server" onselectedindexchanged="ddlEditPerfil_SelectedIndexChanged"></asp:dropdownlist></td>
					</tr>
					<tr id="trEdtEmpresaUsuaria" runat="server" visible="false">
						<td class="tdGris01">Empresa asociada</td>
						<td class="tdGris02">
                            <asp:dropdownlist id="ddlEdtEmpresaUsuaria" CssClass="FormDropDown" 
                                runat="server"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td class="tdGris01"><asp:checkbox id="chkEdtClave" CssClass="BotonTexto" runat="server" Text="Restaurar Clave"></asp:checkbox>:</td>
						<td class="tdGris01"><asp:checkbox id="chkEdtEstado" CssClass="BotonTexto" runat="server" Text="Estado"></asp:checkbox></td>
					</tr>
					<tr>
						<td align="center" colspan="2"><asp:button id="btnEdtEnviar" 
                                CssClass="BotonEnviar" runat="server" Text="Grabar" 
                                onclick="btnEdtEnviar_Click"></asp:button>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:button id="btnEdtCancelar" CssClass="BotonEnviar" runat="server" CausesValidation="false" 
                                Text="Cancelar" onclick="btnEdtCancelar_Click"></asp:button></td>
					</tr>
				</table>
        </td></tr>
        <tr><td id="tdInsertar" runat="server">
            <table id="table1" style="WIDTH: 371px;" cellspacing="1" cellpadding="1" width="371" border="0">
					<tr>
						<td class="tdImgAzul01" colspan="2">Crear Usuario 
                            </td>
					</tr>
					<tr>
						<td class="tdGris01">Documento:</td>
						<td class="tdGris02"><asp:textbox id="txtInsDocumento" CssClass="FormTextoGris" 
                                runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="tdGris01">Primer Nombre:
							<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtInsNombre1" ErrorMessage="RequiredFieldValidator">*</asp:requiredfieldvalidator></td>
						<td class="tdGris02"><asp:textbox id="txtInsNombre1" CssClass="FormTextoGris" 
                                runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="tdGris01">Segundo Nombre:</td>
						<td class="tdGris02"><asp:textbox id="txtInsNombre2" CssClass="FormTextoGris" 
                                runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="tdGris01">Primer Apellido:
							<asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" ControlToValidate="txtInsApellido1" ErrorMessage="RequiredFieldValidator">*</asp:requiredfieldvalidator></td>
						<td class="tdGris02" style="HEIGHT: 28px"><asp:textbox id="txtInsApellido1" 
                                CssClass="FormTextoGris" runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="tdGris01">Segunto Apellido:</td>
						<td class="tdGris02"><asp:textbox id="txtInsApellido2" CssClass="FormTextoGris" 
                                runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="tdGris01">Nombre de usuario:
							<asp:requiredfieldvalidator id="RequiredFieldValidator6" runat="server" ControlToValidate="txtInsUserName" ErrorMessage="RequiredFieldValidator">*</asp:requiredfieldvalidator></td>
						<td class="tdGris02">
                            <asp:textbox id="txtInsUserName" CssClass="FormTextoGris" 
                                runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="tdGris01">E-mail:<asp:requiredfieldvalidator 
                                id="RequiredFieldValidator7" runat="server" ControlToValidate="txtInsMail" 
                                ErrorMessage="RequiredFieldValidator">*</asp:requiredfieldvalidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtInsMail" ErrorMessage="*" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
						<td class="tdGris02">
                            <asp:textbox id="txtInsMail" CssClass="FormTextoGris" 
                                runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="tdGris01">Perfil:</td>
						<td class="tdGris02">
                            <asp:dropdownlist id="ddlInsPerfil" CssClass="FormDropDown" 
                                runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlInsPerfil_SelectedIndexChanged"></asp:dropdownlist></td>
					</tr>
					<tr id="trInsEmpresaUsuaria" runat="server" visible="false">
						<td class="tdGris01">Empresa asociada</td>
						<td class="tdGris02">
                            <asp:dropdownlist id="ddlInsEmpresaUsuaria" CssClass="FormDropDown" 
                                runat="server"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td align="center" colspan="2">
                            <asp:button id="btnInsGrabar" CssClass="btn btn-success" runat="server" 
                                Text="Crear" onclick="btnInsGrabar_Click" ></asp:button>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:button id="btnInsCancelar" CssClass="btn btn-default" runat="server" Text="Cancelar" CausesValidation="false" onclick="btnInsCancelar_Click"></asp:button>
                        </td>
					</tr>
				</table>
        </td></tr>
    </table>
</asp:Content>

