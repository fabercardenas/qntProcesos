<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultLog.aspx.cs" Inherits="CosultLog" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="clearfix"></div>

<asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>

<asp:Panel ID="pnlInicial" runat="server">
    <div id="dvIdConsulta" runat="server">
        <%--Tarjeta--%>
        <div class="x_panel">
            <div class="row" >
                <div class="col-md-2" style="margin-top:5px;">
                    Documento del cliente
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtNumDocumento" runat="server" CssClass="form-control" ValidationGroup="principal"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="fteNumTarjeta" runat="server" TargetControlID="txtNumDocumento" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfvNumTarjeta" runat="server" ControlToValidate="txtNumDocumento" ErrorMessage="* Campo Requerido" ValidationGroup="principal"> </asp:RequiredFieldValidator>
                </div>
                <div class="col-md-5 text-left">
                     <asp:LinkButton ID="lnbConsultarDocumento" runat="server" CssClass="btn btn-primary" OnClick="lnbConsultarDocumento_Click" ValidationGroup="principal">
                        <span class="fa fa-send"></span>
                        &nbsp;&nbsp;Consultar
                    </asp:LinkButton>
                </div>
                
            </div>
            <div style="margin-top:-10px; border-top:2px solid #E6E9ED;">
            </div> 
            <div class="clearfix"></div>
            <div class="x_content">
            <br />
            aqui
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
    </div>

</asp:Panel>

</asp:Content>
