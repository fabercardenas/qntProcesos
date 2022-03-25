<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="F_ContratacionMasiva.aspx.cs" Inherits="Web_Archivos_F_Migracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Carga masiva de empleados por reintegro ó reingreso</div>
    <br />
    <div class="alert alert-warning">
        En este módulo cargará masivamente empleados que <strong> ya existen en el sistema</strong> y se encuentran sin contrato activo para la empresa temporal asociada,
        generando los contratos y ordenes de contratación respectivas.
        <br />
        Los datos básicos del empleado no cambiarán.
        Puede descargar un formato de ejemplo pulsando click sobre el ícono
        <a href="../../Docs/Formatos/FormatoReingresosMasivos.xlsx" target="_blank">
             <span class="glyphicon glyphicon-save" style="font-size:19px;"></span>
        </a>
    </div>
    <table style="width:1000px;">
        <tr>
            <td class="tdGris01" >
                Seleccione el archivo<asp:FileUpload ID="fupArchivo" runat="server" CssClass="txtBordeGris" />
            </td>
        </tr>
        <tr>
            <td style="text-align:center;"><br />
                <asp:Button ID="btnCargar" runat="server" CssClass="BotonEnviar02" Text="Cargar archivo" OnClick="btnCargar_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>


</asp:Content>

