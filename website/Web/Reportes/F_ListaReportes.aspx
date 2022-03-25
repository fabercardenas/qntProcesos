<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="F_ListaReportes.aspx.cs" Inherits="Web_Reportes_F_ListaReportes" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Ajax.Net" Namespace="RJS.Web.WebControl"  TagPrefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
        .style1
        {
            font-family: "Calibri";
            font-size: 12px;
            color: #000000;
            text-decoration: none;
            Padding-left: 5px;
            Padding-right: 5px;
            height: 18px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function url(URL) {
            hidden = open(URL, 'NewWindow', 'top=0,left=0,status=yes,resizable=yes,scrollbars=yes');
        }
    </script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table align="center" >
    <tr><td class="tdImgVerde03" style="text-align:center;" colspan="2">REPORTE DE CARTERA
        </td><td style="text-align:center; width:25px;">&nbsp;</td>
        <td class="tdImgVerde03" style="text-align:center;" colspan="2">REPORTE DE 
            MOVIMIENTOS</td></tr>
    <tr>
        <td class="tdBlanco02" style="text-align:center;">Generar reporte de cartera con los siguientes 
            filtros:</td>
        <td class="tdBlanco02" style="text-align:center;">&nbsp;</td>
        <td class="tdBlanco02" style="text-align:center;">&nbsp;</td>
        <td class="tdBlanco02" style="text-align:center;">Genererar reporte de movimientos 
            con los siguientes filtros: </td>
        <td class="tdBlanco02" style="text-align:center;">&nbsp;</td>
    </tr>
    <tr>
        <td class="tdBlanco02" style="text-align:left; height:30px;">Empresa</td>
        <td class="tdBlanco02" style="text-align:left;">
            <asp:DropDownList ID="ddlcenEmpresa" runat="server" CssClass="txtBordeGris" 
                AutoPostBack="True" onselectedindexchanged="ddlcenEmpresa_SelectedIndexChanged">
            </asp:DropDownList></td>
        <td class="tdBlanco02" style="text-align:left;">
            &nbsp;</td>
        <td class="tdBlanco02" style="text-align:left;">
            Empresa</td>
        <td class="tdBlanco02" style="text-align:left;">
            <asp:DropDownList ID="ddlmovEmpresa" runat="server" CssClass="txtBordeGris" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlmovEmpresa_SelectedIndexChanged" >
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td class="tdBlanco02" style="text-align:left; height:30px;">Tercero</td>
        <td class="tdBlanco02" style="text-align:left;">
            <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" 
                CssClass="txtBordeGris" 
                onselectedindexchanged="ddlClientes_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:listsearchextender ID="ddlClientes_ListSearchExtender" runat="server" PromptCssClass="lblMarron02"
                Enabled="True" TargetControlID="ddlClientes" QueryPattern="Contains" 
                PromptText="Digite el nombre">
            </asp:listsearchextender>
        </td>
        <td class="tdBlanco02" style="text-align:left;">
            &nbsp;</td>
        <td class="tdBlanco02" style="text-align:left;">
            Tercero</td>
        <td class="tdBlanco02" style="text-align:left;">
            <asp:DropDownList ID="ddlmovClientes" runat="server" AutoPostBack="True" 
                CssClass="txtBordeGris" 
                onselectedindexchanged="ddlmovClientes_SelectedIndexChanged" >
            </asp:DropDownList>
            <asp:listsearchextender ID="ddlmovClientes_ListSearchExtender" runat="server" PromptCssClass="lblMarron02"
                Enabled="True" TargetControlID="ddlmovClientes" QueryPattern="Contains" 
                PromptText="Digite el nombre">
            </asp:listsearchextender>
        </td>
    </tr>
    <tr>
        <td class="style1" style="text-align:left; height:30px;">Centro de Costo</td>
        <td class="style1" style="text-align:left;">
            <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="txtBordeGris">
            </asp:DropDownList>
        </td>
        <td class="style1" style="text-align:left;">
            &nbsp;</td>
        <td class="style1" style="text-align:left;">
            Tipo de movimiento</td>
        <td class="style1" style="text-align:left;">
            <asp:DropDownList ID="ddlmovTipoMovimiento" runat="server" 
                CssClass="txtBordeGris" AutoPostBack="True" 
                onselectedindexchanged="ddlmovTipoMovimiento_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="-1">Todos</asp:ListItem>
                <asp:ListItem Value="FC">Facturas</asp:ListItem>
                <asp:ListItem Value="RC">Registros de Pago</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="text-align:center; height:30px;" colspan="2">
            <asp:Button ID="btnGenerar" runat="server" Text="Generar Reporte Cartera" 
                CssClass="BotonEnviar" />
        </td>
        <td class="style1" style="text-align:left;">
            &nbsp;</td>
        <td class="style1" style="text-align:left;">
            Desde la fecha</td>
        <td class="style1" style="text-align:left;">
            <asp:TextBox ID="txtMovFechaInicio" runat="server" CssClass="txtBordeGris" 
                Height="21px" Width="92px" ReadOnly="True" ></asp:TextBox>
            <rjs:PopCalendar ID="PopCalendar2" runat="server" 
                Control="txtMovFechaInicio"
                Format="yyyy mm dd"
                Separator="-" AutoPostBack="True" onselectionchanged="PopCalendar2_SelectionChanged"
            />
                
        </td>
    </tr>
    <tr>
        <td class="style1" style="text-align:left; height:30px;">&nbsp;</td>
        <td class="style1" style="text-align:left;">
            &nbsp;</td>
        <td class="style1" style="text-align:left;">
            &nbsp;</td>
        <td class="style1" style="text-align:left;">
            Hasta la fecha</td>
        <td class="style1" style="text-align:left;">
            <asp:TextBox ID="txtMovFechaFin" runat="server" CssClass="txtBordeGris" 
                Height="21px" Width="92px" ReadOnly="True"></asp:TextBox>
            <rjs:PopCalendar ID="PopCalendar1" runat="server" 
                Control="txtMovFechaFin"
                Format="yyyy mm dd"
                Separator="-" AutoPostBack="True" onselectionchanged="PopCalendar1_SelectionChanged"
            />
        </td>
    </tr>
    <tr>
        <td style="text-align:center; height:30px;" colspan="2">
            &nbsp;</td>
        <td style="text-align:center; height:30px;">
            &nbsp;</td>
        <td style="text-align:center; height:30px;" colspan="2">
            <asp:Button ID="btnGeneraMovimientos" runat="server" Text="Generar Reporte Movimientos" 
                CssClass="BotonEnviar" />
        </td>
    </tr>
    <tr>
        <td style="text-align:center; height:30px;" colspan="2">
            &nbsp;</td>
        <td style="text-align:center; height:30px;">
            &nbsp;</td>
        <td style="text-align:center; height:30px;" colspan="2">
            &nbsp;</td>
    </tr>
    <tr><td class="tdImgVerde03" style="text-align:center;" colspan="2">REPORTE DE CUENTAS POR PAGAR
        </td><td style="text-align:center; width:25px;">&nbsp;</td>
        <td style="text-align:center;" colspan="2" class="tdImgVerde03">LISTADOS DE EMPLEDOS</td></tr>
    <tr>
        <td class="tdBlanco02" style="text-align:center;">Generar reporte de cuentas por pagar con los siguientes 
            filtros:</td>
        <td class="tdBlanco02" style="text-align:center;">&nbsp;</td>
        <td class="tdBlanco02" style="text-align:center;">&nbsp;</td>
        <td class="tdBlanco02" style="text-align:center;"></td>
        <td class="tdBlanco02" style="text-align:center;">&nbsp;</td>
    </tr>
    <tr>
        <td class="tdBlanco02" style="text-align:left; height:30px;">Empresa</td>
        <td class="tdBlanco02" style="text-align:left;">
            <asp:DropDownList ID="ddlproEmpresa" runat="server" CssClass="txtBordeGris" 
                AutoPostBack="True" >
            </asp:DropDownList></td>
        <td class="tdBlanco02" style="text-align:left;">
            &nbsp;</td>
        <td class="tdBlanco02" style="text-align:left;">
            Empresa</td>
        <td class="tdBlanco02" style="text-align:left;">
            <asp:DropDownList ID="ddlNomEmpresa" runat="server" CssClass="txtBordeGris" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlmovEmpresa_SelectedIndexChanged" >
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td class="tdBlanco02" style="text-align:left; height:30px;">Tercero</td>
        <td class="tdBlanco02" style="text-align:left;">
            <asp:DropDownList ID="ddlProveedores" runat="server" AutoPostBack="True" 
                CssClass="txtBordeGris" >
            </asp:DropDownList>
            <asp:listsearchextender ID="Listsearchextender1" runat="server" PromptCssClass="lblMarron02"
                Enabled="True" TargetControlID="ddlClientes" QueryPattern="Contains" 
                PromptText="Digite el nombre">
            </asp:listsearchextender>
        </td>
        <td class="tdBlanco02" style="text-align:left;">
            &nbsp;</td>
        <td class="tdBlanco02" style="text-align:left;">
            Tercero</td>
        <td class="tdBlanco02" style="text-align:left;">
            <asp:listsearchextender ID="Listsearchextender2" runat="server" PromptCssClass="lblMarron02"
                Enabled="True" TargetControlID="ddlmovClientes" QueryPattern="Contains" 
                PromptText="Digite el nombre">
            </asp:listsearchextender>
            <asp:DropDownList ID="ddlNomClientes" runat="server" AutoPostBack="True" 
                CssClass="txtBordeGris" 
                onselectedindexchanged="ddlmovClientes_SelectedIndexChanged" >
            </asp:DropDownList>
            <asp:listsearchextender ID="ddlNomClientes_ListSearchExtender" runat="server" PromptCssClass="lblMarron02"
                Enabled="True" TargetControlID="ddlNomClientes" QueryPattern="Contains" 
                PromptText="Digite el nombre">
            </asp:listsearchextender>
            <asp:listsearchextender ID="ddlNomClientes_ListSearchExtender2" runat="server" PromptCssClass="lblMarron02"
                Enabled="True" TargetControlID="ddlNomClientes" QueryPattern="Contains" 
                PromptText="Digite el nombre">
            </asp:listsearchextender>
        </td>
    </tr>
    <tr>
        <td class="tdBlanco02" style="text-align:center;">
            <asp:Button ID="btnGenerarCuentas" runat="server" Text="Generar Reporte Cuentas x pagar" 
                CssClass="BotonEnviar" />
        </td>
        <td class="tdBlanco02" style="text-align:center;">&nbsp;</td>
        <td class="tdBlanco02" style="text-align:center;">&nbsp;</td>
        <td class="tdBlanco02" style="text-align:center;" colspan="2">
            <asp:Button ID="btnGeneraListaEmpleados" runat="server" Text="Generar Listado Empleados" 
                CssClass="BotonEnviar" OnClick="btnGeneraListaEmpleados_Click" />
        </td>
    </tr>
</table>
</asp:Content>

