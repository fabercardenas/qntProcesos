<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="GetRequest.aspx.cs" Inherits="TDC_GetRequest" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-title">
        <div class="title_left">
            <h3>Consulta solicitudes</h3>
        </div>
    </div>
<div class="clearfix"></div>
<asp:HiddenField ID="hdfIdNomina" runat="server" />

<asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>

<asp:Panel ID="pnlInicial" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <h2>Selecciona los filtros de consulta</h2>
            <div class="clearfix"></div>
        </div>
        <div class="x_content">
            <div class="col-md-4">
                <label>Canal</label>
                <asp:DropDownList ID="ddlCanales" runat="server" CssClass="form-control" >
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label>Proceso</label>
                <asp:DropDownList ID="ddlProcesos" runat="server" 
                        CssClass="form-control" ValidationGroup="principal" >
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                
            </div>
            <div class="col-md-4" style="padding-top:20px;">
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click"
                        CssClass="btn btn-primary"  ValidationGroup="principal" />
            </div>
            <br />
            <br />
            <br />
            <asp:GridView ID="gdvListaSolicitudes" runat="server" CssClass="table table-hover" 
                EmptyDataText="No hay solicitues con los filtros seleccionados" 
                    AutoGenerateColumns="False" Width="100%"
                    AllowPaging="true" PageSize="30" GridLines="None" 
                    >
                <Columns>
                    <asp:BoundField DataField="tdc_tipoDocumento" HeaderText="Tipo Documento" />
                    <asp:BoundField DataField="tdc_numeroDocumento" HeaderText="Documento" />
                    <asp:BoundField DataField="canal" HeaderText="Canal" />
                    <asp:BoundField DataField="proceso" HeaderText="Proceso" />
                    <asp:BoundField DataField="tdc_direccion" HeaderText="Direccion" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Panel>

</asp:Content>


