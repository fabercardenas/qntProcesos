<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="ActivationFile.aspx.cs" Inherits="TDC_ActivationFile" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-title">
        <div class="title_left">
            <h3>Tarjetas para Activación - Paso 8.2</h3>
        </div>
    </div>
<div class="clearfix"></div>

<asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
<asp:Literal ID="ltrFechaPrevalidacion" runat="server" Visible="false"></asp:Literal>

<asp:Panel ID="pnlInicial" runat="server">
    <div class="x_panel">
        <div class="x_content">
            <div class="row">
                <div class="col-md-4 text-center">
                    <asp:LinkButton ID="lnbBloqueo" runat="server" CssClass="btn btn-info" OnClick="lnbBloqueo_Click" visible="True">
                            <span class="glyphicon glyphicon-file" ></span>
                            &nbsp;&nbsp;Generar Archivo Activación
                    </asp:LinkButton>
                    
                </div>
                <div class="col-md-4 text-center">
                </div>
                <div class="col-md-4 text-center">
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                </div>
            </div>
        </div>
    </div>
    <div id="dvIdConsulta" runat="server" class="x_panel">
        <%--Tarjeta--%>
        <div class="x_panel; col-md-6">
            <div class="clearfix"></div>
        </div> 
        <%--Fecha--%>
        <div class="x_panel; col-md-6">
            <div class="clearfix"></div>
         </div>
        <%--Consulta--%>
        <div class="x_content">
            <br />
            <br />
            <asp:GridView ID="gdvListaSolicitudes" runat="server" CssClass="table table-hover" 
                EmptyDataText="No hay solicitues con los filtros seleccionados" 
                    AutoGenerateColumns="False" Width="100%"
                    AllowPaging="true" PageSize="30" GridLines="None"
                    DataKeyNames="tdc_numeroDocumento">
                <Columns>
                    <asp:BoundField DataField="tdc_numeroDocumento" HeaderText="Documento" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="tdc_numeroTarjeta" HeaderText="No. Tarjeta" ItemStyle-HorizontalAlign="Center"  />
                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="tdc_numeroPagos" HeaderText="Número de Pagos" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="tdc_diasMora" HeaderText="Días Mora" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Panel>

</asp:Content>


