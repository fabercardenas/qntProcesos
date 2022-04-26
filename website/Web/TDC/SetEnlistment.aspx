<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="SetEnlistment.aspx.cs" Inherits="TDC_SetEnlistment" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-title">
        <div class="title_left">
            <h3>Consulta todas las Solucitudes para Prevalidación</h3>
        </div>
    </div>
<div class="clearfix"></div>
<asp:HiddenField ID="hdfIdNomina" runat="server" />

<asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>

<asp:Panel ID="pnlInicial" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <h2>Filtre por los últimos 6 dígitos de la Tarjeta</h2>
            <div class="clearfix"></div>
        </div>
        <div class="x_content">
            <div class="col-md-4">
                <label></label>
                <asp:DropDownList ID="ddlCanales" runat="server" CssClass="form-control" Visible="false" >
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
               
            </div>
            <div class="col-md-4">
                
            </div>
            <div class="col-md-4" style="padding-top:20px;">
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click"
                        CssClass="btn btn-primary"  ValidationGroup="principal" Visible="false" />
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:GridView ID="gdvListaSolicitudes" runat="server" CssClass="table table-hover" 
                EmptyDataText="No hay solicitues con los filtros seleccionados" 
                    AutoGenerateColumns="False" Width="100%"
                    AllowPaging="true" PageSize="30" GridLines="None"
                    >
                <Columns>
                    <asp:BoundField DataField="tdc_numeroDocumento" HeaderText="Documento" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="tdc_numeroTarjeta" HeaderText="No. Tarjeta" ItemStyle-HorizontalAlign="Center"  />
                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Left" />
                    <asp:TemplateField HeaderText="Ubicación" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <span class="glyphicon glyphicon-ok"></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Prevalidación"  ShowSelectButton="true" SelectText="Validar" ButtonType="Link" ControlStyle-CssClass="btn btn-xs btn-success" ItemStyle-HorizontalAlign="Center"/>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Panel>

</asp:Content>


