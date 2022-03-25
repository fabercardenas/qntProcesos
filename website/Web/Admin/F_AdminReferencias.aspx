<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="F_AdminReferencias.aspx.cs" Inherits="F_AdminReferencias" %>

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
        .auto-style3 {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:70%;" align="center">
        <tr><td class="tdImgVerde03">Administración de Referencias</td></tr>
        <tr><td class="auto-style1">&nbsp;</td></tr>
        <tr id="trDatos" runat="server">
            <td>
                Filtrar <asp:DropDownList ID="ddlFiltroTipo" runat="server" AutoPostBack="True" CssClass="BotonTexto" OnSelectedIndexChanged="ddlFiltroTipo_SelectedIndexChanged">
            </asp:DropDownList>
                <br />
                <br />
                <asp:GridView ID="gdvReferencias" runat="server"  
                    AutoGenerateColumns="False" CssClass="gdvTableStyle" ShowFooter="False" AllowPaging="False" 
                    DataKeyNames="ID_referencia" OnSelectedIndexChanging="gdvAdminSalud_SelectedIndexChanging">
                    <Columns>
					    <asp:BoundField DataField="ref_valor" HeaderText="Valor"></asp:BoundField>
                        <asp:BoundField DataField="ref_nombre" HeaderText="Nombre"></asp:BoundField>
					    <asp:BoundField DataField="ref_descripcion" HeaderText="Descripcion"></asp:BoundField>
					    <asp:BoundField DataField="ref_parametro1" HeaderText="Parametro 1"></asp:BoundField>
                        <asp:BoundField DataField="ref_parametro2" HeaderText="Parametro 2"></asp:BoundField>
                        <asp:BoundField DataField="ref_parametro3" HeaderText="Parametro 3"></asp:BoundField>
                        <asp:BoundField DataField="ref_orden" HeaderText="Orden"></asp:BoundField>
                        <asp:BoundField DataField="ref_estado" HeaderText="Estado"></asp:BoundField>
                        <asp:BoundField DataField="ref_TipoNov" HeaderText="Tipo Nov"></asp:BoundField>
                        <asp:BoundField DataField="ref_ContableConcepto" HeaderText="Contable Concepto"></asp:BoundField>
                        <asp:BoundField DataField="ref_ContableCuenta" HeaderText="Contable Cuenta"></asp:BoundField>
                        <asp:BoundField DataField="ref_ContableNaturaleza" HeaderText="Contable Naturaleza"></asp:BoundField>
                        <asp:BoundField DataField="ref_ContableCuenta2" HeaderText="Contable Cuenta 2"></asp:BoundField>
                        <asp:BoundField DataField="ref_ContableNaturaleza2" HeaderText="Contable Naturaleza 2"></asp:BoundField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Button" SelectText="Editar" />
                    </Columns>
                    <HeaderStyle CssClass="gdvHeaderstyle" />
                    <RowStyle CssClass="gdvRowstyle" />
                    <AlternatingRowStyle CssClass="gdvAltrowstyle" />
                </asp:GridView>
                            <br />
            <asp:Button ID="btnCrear" runat="server" CssClass="BotonEnviar" Height="27px" 
                onclick="btnCrear_Click" Text="Crear Nuevo" />
                <asp:HiddenField ID="hdfIdReferencia" runat="server" />
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
                        <td class="auto-style1">Tipo</td>
                        <td class="auto-style2">
                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="BotonTexto">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Valor</td>
                        <td>
                            <asp:TextBox ID="txtValor" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator" runat="server" ControlToValidate="txtValor" Text="Este campo es requerido"></asp:requiredfieldvalidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nombre</td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Descripcion</td>
                        <td>
                            <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Parametro 1</td>
                        <td>
                            <asp:TextBox ID="txtParametro1" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="rfvParametro1" runat="server" ControlToValidate="txtParametro1" Text="Este campo es requerido"></asp:requiredfieldvalidator>
                            <asp:FilteredTextBoxExtender ID="ftbParametro1" runat="server" TargetControlID="txtParametro1" FilterMode="ValidChars" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Parametro 2</td>
                        <td>
                            <asp:TextBox ID="txtParametro2" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" ControlToValidate="txtParametro2" Text="Este campo es requerido"></asp:requiredfieldvalidator>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtParametro2" FilterMode="ValidChars" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Parametro 3</td>
                        <td>
                            <asp:TextBox ID="txtParametro3" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" ControlToValidate="txtParametro3" Text="Este campo es requerido"></asp:requiredfieldvalidator>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtParametro3" FilterMode="ValidChars" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Orden</td>
                        <td>
                            <asp:TextBox ID="txtOrden" runat="server"></asp:TextBox>
                            <asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" ControlToValidate="txtOrden" Text="Este campo es requerido"></asp:requiredfieldvalidator>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtOrden" FilterMode="ValidChars" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tipo Nov</td>
                        <td>
                            <asp:TextBox ID="txtTipoNov" runat="server" ></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="RangeValidator1" Type="Currency" ControlToValidate="txtTipoNov" MaximumValue='1' MinimumValue="-1" ErrorMessage="-1 ó 1 solamente" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contable Concepto</td>
                        <td>
                            <asp:TextBox ID="txtContableConcepto" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contable Cuenta</td>
                        <td>
                            <asp:TextBox ID="txtContableCuenta" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contable Naturaleza</td>
                        <td>
                            <asp:TextBox ID="txtContableNaturaleza" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Contable Cuenta 2</td>
                        <td>
                            <asp:TextBox ID="txtContableCuenta2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contable Naturaleza 2</td>
                        <td>
                            <asp:TextBox ID="txtContableNaturaleza2" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
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
