<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="ValidateDocuments.aspx.cs" Inherits="TDC_ValidateDocuments" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-title">
        <div class="title_left">
            <h3>Validar Documentación - Paso 7.3</h3>
        </div>
    </div>
<div class="clearfix"></div>

<asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>

<asp:Panel ID="pnlInicial" runat="server">
    <div id="dvIdConsulta" runat="server" class="x_panel">
        <%--Tarjeta--%>
        <div class="x_panel; col-md-6">
            <div class="x_title">
                <h2>Consulte por el Documento de Identidad</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="col-md-3" style="padding-top:18px;">
                    <label></label>
                    <asp:TextBox ID="txtNumDocumento" runat="server" CssClass="form-control" ValidationGroup="principal"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="fteNumTarjeta" runat="server" TargetControlID="txtNumDocumento" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfvNumTarjeta" runat="server" ControlToValidate="txtNumDocumento" ErrorMessage="* Campo Requerido" ValidationGroup="principal"> </asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3" style="padding-top:20px;">
                     <asp:LinkButton ID="lnbConsultarDocumento" runat="server" CssClass="btn btn-primary" OnClick="lnbConsultarDocumento_Click" ValidationGroup="principal">
                        <span class="glyphicon glyphicon-credit-card"></span>
                        &nbsp;&nbsp;Consultar
                    </asp:LinkButton>
                </div>
            </div> 
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
                    DataKeyNames="tdc_numeroDocumento, tdc_docIngresos, tdc_docIdentidad"
                    OnRowDataBound="gdvListaSolicitudes_RowDataBound" OnRowCommand="gdvListaSolicitudes_RowCommand"
                    >
                <Columns>
                    <asp:BoundField DataField="tdc_numeroDocumento" HeaderText="Documento" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="tdc_numeroTarjeta" HeaderText="No. Tarjeta" ItemStyle-HorizontalAlign="Center"  />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Left" />
                    <asp:TemplateField HeaderText="Flujo Digital" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# TieneFlujoDigital((Boolean)Eval("tdc_flujoDigital")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pagaré" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# TienePagare((Boolean)Eval("tdc_docPagare")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Doc. Identidad" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton  ID="lbtDocIdentidad" runat="server" CommandName="DocIdentidadRecibir" CommandArgument="<%# Container.DataItemIndex %>"
                                  Text="Recibido"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Soporte Ingresos" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton  ID="lbtSopIngresos" runat="server" CommandName="SopIngresosRecibir" CommandArgument="<%# Container.DataItemIndex %>"
                                  Text="Recibido"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_content">
            <div class="row">
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lnbGenerarSMS" runat="server" CssClass="btn btn-warning" OnClick="lnbGenerarSMS_Click">
                            <span class="glyphicon glyphicon-ok" ></span>
                            &nbsp;&nbsp;Generar Archivo SMS
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

</asp:Panel>

</asp:Content>
