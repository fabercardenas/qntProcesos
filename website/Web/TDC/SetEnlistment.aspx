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

<asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
<asp:Literal ID="Literal1" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="ltrFechaPrevalidacion" runat="server" Visible="false"></asp:Literal>

<asp:Panel ID="pnlInicial" runat="server">
    <div id="dvIdConsulta" runat="server" class="x_panel">
        <%--Tarjeta--%>
        <div class="x_panel; col-md-6">
            <div class="x_title">
                <h2>Filtre por los últimos 6 dígitos de la Tarjeta</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="col-md-3" style="padding-top:18px;">
                    <label></label>
                    <asp:TextBox ID="txtNumTarjeta" runat="server" CssClass="form-control" MaxLength="6" ValidationGroup="principal"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="fteNumTarjeta" runat="server" TargetControlID="txtNumTarjeta" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfvNumTarjeta" runat="server" ControlToValidate="txtNumTarjeta" ErrorMessage="* Campo Requerido" ValidationGroup="principal"> </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator Display="Dynamic" ID="revNumTarjeta" runat="server" ControlToValidate="txtNumTarjeta" ValidationExpression="^[\s\S]{6,}$" ErrorMessage="min. ultimos 6 dígitos" ValidationGroup="principal"></asp:RegularExpressionValidator>
                </div>
                <div class="col-md-3" style="padding-top:20px;">
                     <asp:LinkButton ID="lnbConsultarTarjeta" runat="server" CssClass="btn btn-primary" OnClick="lnbConsultarTarjeta_Click" ValidationGroup="principal">
                        <span class="glyphicon glyphicon-credit-card"></span>
                        &nbsp;&nbsp;Consultar
                    </asp:LinkButton>
                </div>
            </div> 
            <div class="clearfix"></div>
        </div> 
        <%--Fecha--%>
        <div class="x_panel; col-md-6">
            <div class="x_title">
                <h2>Filtre por Fecha de Prevalidación</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="col-md-3" style="padding-top:18px;">
                    <label></label>
                    <asp:TextBox ID="txtFechaPreV" runat="server" CssClass="form-control" ValidationGroup="secundario"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFechaPreV" runat="server" ControlToValidate="txtFechaPreV" ErrorMessage="* Campo Requerido" ValidationGroup="secundario"> </asp:RequiredFieldValidator>
                    <asp:CalendarExtender ID="cleFechaPrevalidacion" runat="server" TargetControlID="txtFechaPreV" Format="yyyy-MM-dd"></asp:CalendarExtender>
                </div>
                <div class="col-md-3" style="padding-top:20px;">
                    <asp:LinkButton ID="lnbConsultarFecha" runat="server" CssClass="btn btn-primary" OnClick="lnbConsultarFecha_Click" ValidationGroup="secundario">
                        <span class="glyphicon glyphicon-calendar"></span>
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
                    DataKeyNames="tdc_numeroDocumento" OnSelectedIndexChanging="gdvListaSolicitudes_SelectedIndexChanging"
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
    <div class="x_panel">
        <div class="x_content">
            <div class="col-md-3">
                <asp:LinkButton ID="lnbStikers" runat="server" CssClass="btn btn-info" OnClick="lnbStikers_Click" visible="false">
                        <span class="glyphicon glyphicon-comment" ></span>
                        &nbsp;&nbsp;Generar Stikers
                </asp:LinkButton>
            </div>
            <div class="col-md-3">
                <asp:LinkButton ID="lnbBloqueo" runat="server" CssClass="btn btn-info" OnClick="lnbBloqueo_Click" visible="false">
                        <span class="glyphicon glyphicon-file" ></span>
                        &nbsp;&nbsp;Generar Archivo Bloqueo
                </asp:LinkButton>
            </div>
            <div class="col-md-3">
                <asp:LinkButton ID="lnbAcuse" runat="server" CssClass="btn btn-info" OnClick="lnbAcuse_Click" visible="false">
                        <span class="glyphicon glyphicon-save-file" ></span>
                        &nbsp;&nbsp;Generar Acuse de Recibido
                </asp:LinkButton>
            </div>
            <div class="col-md-3">
                <asp:LinkButton ID="lnbTerminar" runat="server" CssClass="btn btn-warning" OnClick="lnbTerminar_Click">
                        <span class="glyphicon glyphicon-ok" ></span>
                        &nbsp;&nbsp;Terminar Prevalidación
                </asp:LinkButton>
            </div>
        </div>
    </div>

</asp:Panel>

</asp:Content>


