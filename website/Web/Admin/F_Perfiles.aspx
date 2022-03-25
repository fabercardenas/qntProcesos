<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="F_Perfiles.aspx.cs" Inherits="Web_Admin_F_Perfiles" %>

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
        <tr><td class="tdImgVerde03" colspan="2">Administración de Perfiles</td></tr>
        <tr id="trDatos" runat="server">
            <td colspan="2">
                Filtrar:&nbsp; <asp:DropDownList ID="ddlFiltroEstado" runat="server" AutoPostBack="True" CssClass="BotonTexto" OnSelectedIndexChanged="ddlFiltroEstado_SelectedIndexChanged">
                <asp:ListItem Value="1">Activo</asp:ListItem>
                <asp:ListItem Value="0">Inactivo</asp:ListItem>
            </asp:DropDownList>
                <br />
                <asp:GridView ID="gdvPerfiles" runat="server"  AutoGenerateColumns="False" CssClass="gdvTableStyle" 
                    DataKeyNames="ID_perfil" OnSelectedIndexChanging="gdvPerfiles_SelectedIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="per_nombre" HeaderText="Perfil"></asp:BoundField>
					    <asp:BoundField DataField="per_proceso" HeaderText="Proceso"></asp:BoundField>
					    <asp:BoundField DataField="per_correoProceso" HeaderText="Correo Proceso"></asp:BoundField>
					    <asp:BoundField DataField="per_nivel" HeaderText="Nivel"></asp:BoundField>
                        <asp:BoundField DataField="per_estado" HeaderText="Estado"></asp:BoundField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Button" SelectText="Editar" />
                    </Columns>
                    <HeaderStyle CssClass="gdvHeaderstyle" />
                    <RowStyle CssClass="gdvRowstyle" />
                    <AlternatingRowStyle CssClass="gdvAltrowstyle" />
                </asp:GridView>
                            <br />
                <asp:Button ID="btnCrear" runat="server" CssClass="btn btn-primary" 
                    onclick="btnCrear_Click" Text="Crear Nuevo" />
                            <asp:HiddenField ID="hdfIdPerfil" runat="server" />
            </td>
        </tr>
        <tr id="trInsertar" runat="server">
            <td style="vertical-align:top;">
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:Literal ID="ltrCrear" runat="server"></asp:Literal>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Perfil</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Proceso</td>
                        <td>
                            <asp:DropDownList ID="ddlProceso" runat="server">
                                <asp:ListItem Text="Recepcion"></asp:ListItem>
                                <asp:ListItem Text="Contratacion"></asp:ListItem>
                                <asp:ListItem Text="Nomina"></asp:ListItem>
                                <asp:ListItem Text="Usuaria"></asp:ListItem>
                                <asp:ListItem Text="Laboratorio"></asp:ListItem>
                                <asp:ListItem Text="root"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Correo Proceso</td>
                        <td>
                            <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nivel
                        </td>
                        <td>
                            <asp:TextBox ID="txtNivel" runat="server">5</asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtNivel_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtNivel">
                            </asp:FilteredTextBoxExtender>
                            <asp:RangeValidator ID="rfvNivel" runat="server" ControlToValidate="txtNivel" Type="Integer" MinimumValue="1" MaximumValue="50" ErrorMessage="*"></asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="rfvqNivel" runat="server" ControlToValidate="txtNivel" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdGris01"><asp:checkbox id="chkEdtEstado" CssClass="BotonTexto" runat="server" Text="Estado" Checked="True"></asp:checkbox></td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:button id="btnGrabar" CssClass="btn btn-success" runat="server" Text="Grabar" OnClick="btnGrabar_Click" />
                        </td>
                        <td>
                            <asp:button id="btnCancelar" CssClass="btn btn-default" runat="server" CausesValidation="false" Text="Cancelar" OnClick="btnCancelar_Click"></asp:button>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align:top;">
                <table width="100%">
                    <tr>
                        <td colspan="2">Editar Acceso a Módulos</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBoxList ID="chkModulos" runat="server" RepeatColumns="2" CssClass="checkbox">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
