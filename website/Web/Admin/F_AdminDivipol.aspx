<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="F_AdminDivipol.aspx.cs" Inherits="F_AdminDivipol" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 200px;
            height: 22px;
        }
        .auto-style2 {
            height: 22px;
        }
        .auto-style3 {
            height: 20px;
        }
        .auto-style4 {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:70%;" align="center">
        <tr><td class="tdImgVerde03">Administración de División Política</td></tr>
        <tr><td class="auto-style1">&nbsp;</td></tr>
        <tr id="trDatos" runat="server">
            <td>
                Filtrar <asp:DropDownList ID="ddlFiltroPais" runat="server" AutoPostBack="True" CssClass="BotonTexto" OnSelectedIndexChanged="ddlFiltroTipo_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <br />
                <asp:GridView ID="gdvDivipol" runat="server"  
                    AutoGenerateColumns="False" CssClass="gdvTableStyle" AllowPaging="true" PageSize="20" OnPageIndexChanging="gdvDivipol_PageIndexChanging"
                    DataKeyNames="COD_DEPTO,DEPARTAMENTO,COD_MUNI,MUNICIPIO,COD_PAIS,PAIS" OnSelectedIndexChanging="gdvAdminSalud_SelectedIndexChanging">
                    <Columns>
					    <asp:BoundField DataField="COD_PAIS" HeaderText="COD PAIS"></asp:BoundField>
					    <asp:BoundField DataField="PAIS" HeaderText="PAIS"></asp:BoundField>
					    <asp:BoundField DataField="COD_DEPTO" HeaderText="COD DEPARTAMENTO"></asp:BoundField>
                        <asp:BoundField DataField="DEPARTAMENTO" HeaderText="DEPARTAMENTO"></asp:BoundField>
                        <asp:BoundField DataField="COD_MUNI" HeaderText="COD MUNICIPIO"></asp:BoundField>
                        <asp:BoundField DataField="MUNICIPIO" HeaderText="MUNICIPIO"></asp:BoundField>
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
                        <td class="auto-style1">Código de País</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtCodPais" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator0" runat="server" ControlToValidate="txtCodPais" Text="Este campo es requerido"></asp:requiredfieldvalidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">País</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtPais" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtPais" Text="Este campo es requerido"></asp:requiredfieldvalidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Codigo Departamento</td>
                        <td>
                            <asp:TextBox ID="txtCodDepto" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator" runat="server" ControlToValidate="txtCodDepto" Text="Este campo es requerido"></asp:requiredfieldvalidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Departamento</td>
                        <td class="auto-style4">
                            <asp:TextBox ID="txtDepartamento" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtDepartamento" Text="Este campo es requerido"></asp:requiredfieldvalidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Código Muncipio</td>
                        <td>
                            <asp:TextBox ID="txtCodMuni" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtCodMuni" Text="Este campo es requerido"></asp:requiredfieldvalidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Municipio</td>
                        <td>
                            <asp:TextBox ID="txtMunicipio" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtMunicipio" Text="Este campo es requerido"></asp:requiredfieldvalidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdGris01"><asp:HiddenField ID="hdfGdvId" runat="server" /></td>
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
                            <asp:button id="btnGrabar" CssClass="BotonEnviar" runat="server" Text="Grabar" OnClick="btnGrabar_Click" />
                        </td>
                        <td>
                            <asp:button id="btnCancelar" CssClass="BotonEnviar" runat="server" CausesValidation="false" 
                                Text="Cancelar" OnClick="btnCancelar_Click"></asp:button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
