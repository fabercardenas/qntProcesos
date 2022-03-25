<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="F_AdminEmpresas.aspx.cs" Inherits="F_AdminEmpresas" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 150px;
            height: 22px;
        }
        .auto-style3 {
            height: 20px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:80%;" align="center">
        <tr><td class="tdImgVerde03">Administrar Empresas Temporales</td></tr>
        <tr><td class="auto-style1">&nbsp;</td></tr>
        <tr id="trDatos" runat="server">
            <td>
                Filtrar <asp:DropDownList ID="ddlFiltroTipo" runat="server" AutoPostBack="True" CssClass="BotonTexto" OnSelectedIndexChanged="ddlFiltroTipo_SelectedIndexChanged">
                <asp:ListItem>CESANTIAS</asp:ListItem>
                <asp:ListItem>AFP</asp:ListItem>
                <asp:ListItem>ARP</asp:ListItem>
                <asp:ListItem>CCF</asp:ListItem>
                <asp:ListItem>EPS</asp:ListItem>
            </asp:DropDownList>
                <asp:DropDownList ID="ddlFiltroEstado" runat="server" AutoPostBack="True" CssClass="BotonTexto" OnSelectedIndexChanged="ddlFiltroEstado_SelectedIndexChanged">
                <asp:ListItem Value="1">Activo</asp:ListItem>
                <asp:ListItem Value="0">Inactivo</asp:ListItem>
            </asp:DropDownList>
                <br />
                <br />
                <asp:GridView ID="gdvEmpresas" runat="server"  
                    AutoGenerateColumns="False" CssClass="gdvTableStyle" ShowFooter="True" 
                    DataKeyNames="ID_empresa" >
                    <Columns>
					    <asp:BoundField DataField="emp_nombreCorto" HeaderText="Empresa"></asp:BoundField>
					    <asp:BoundField DataField="emp_nit" HeaderText="NIT"></asp:BoundField>
					    <asp:BoundField DataField="emp_dv" HeaderText="DV"></asp:BoundField>
                        <asp:BoundField DataField="emp_telefono" HeaderText="Telefono"></asp:BoundField>
                        <asp:BoundField DataField="emp_correo" HeaderText="Correo"></asp:BoundField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Button" SelectText="Editar" />
                    </Columns>
                    <HeaderStyle CssClass="gdvHeaderstyle" />
                    <RowStyle CssClass="gdvRowstyle" />
                    <AlternatingRowStyle CssClass="gdvAltrowstyle" />
                </asp:GridView>
                            <br />
            <asp:Button ID="btnCrear" runat="server" CssClass="BotonEnviar" Height="27px" 
                onclick="btnCrear_Click" Text="Crear Nuevo" />
                <asp:HiddenField ID="hdfICodigoAnterior" runat="server" />
                </td>
        </tr>
        <tr id="trInsertar" runat="server">
            <td>
                <table width="100%">
                    <tr>
                        <td colspan="2" class="auto-style3">
                            <asp:label id="lblMensaje" CssClass="textError" runat="server"></asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Literal ID="ltrCrear" runat="server"></asp:Literal>&nbsp;
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Nombre</td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre" Text="*"></asp:requiredfieldvalidator>
                        </td>
                        <td>
                            Nombre Corto</td>
                        <td>
                            <asp:TextBox ID="txtNombreCorto" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombreCorto" Text="*"></asp:requiredfieldvalidator>
                        </td>
                    </tr>
                    <tr>
                        <td>NIT</td>
                        <td>
                            <asp:TextBox ID="txtNit" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator0" runat="server" ControlToValidate="txtNit" Text="*"></asp:requiredfieldvalidator>
                        </td>
                        <td>DV</td>
                        <td>
                            <asp:TextBox ID="txtDV" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator" runat="server" ControlToValidate="txtDV" Text="*"></asp:requiredfieldvalidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Slogan</td>
                        <td>
                            <asp:TextBox ID="txtSlogan" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Dirección</td>
                        <td>
                            <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Teléfono</td>
                        <td>
                            <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            e-mail</td>
                        <td>
                            <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">
                            Página web</td>
                        <td>
                            <asp:TextBox ID="txtPaginaWeb" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Código ARL</td>
                        <td>
                            <asp:TextBox ID="txtARL" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Facturación sigla</td>
                        <td>
                            <asp:TextBox ID="txtFacSigla" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Facturación Consecutivo</td>
                        <td>
                            <asp:TextBox ID="txtFacConsecutivo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Pago Sigla</td>
                        <td>
                            <asp:TextBox ID="txtRegSigla" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Pago Consecutivo</td>
                        <td>
                            <asp:TextBox ID="txtRegConsecutivo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Egreso Sigla</td>
                        <td>
                            <asp:TextBox ID="txtEgrSigla" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Egreso Consecutivo</td>
                        <td>
                            <asp:TextBox ID="txtEgrConsecutivo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
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
                        <td colspan="2" style="text-align:center">
                            <asp:button id="btnGrabar" CssClass="BotonEnviar" runat="server" Text="Grabar" OnClick="btnGrabar_Click" />
                        </td>
                        <td colspan="2" style="text-align:center">
                            <asp:button id="btnCancelar" CssClass="BotonEnviar" runat="server" CausesValidation="false" 
                                Text="Cancelar" OnClick="btnCancelar_Click"></asp:button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
