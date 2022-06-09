<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="LoadDelivery.aspx.cs" Inherits="TDC_LoadDelivery" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="dvIdCargaArchivo">
        <div class="tdImgVerde04">Consulta Confirmación de Entrega - Paso 6</div>
        <br />
        <div class="alert alert-info">
            Este módulo le permitirá llevar el control de las Tarjetas de Crédito <b>entregadas con éxito</b>  por medio de dos mecanismos: (1) Automático, por medio de un archivo (2) Manual, buscar por número de tarjeta
            <br />
            Puede descargar un formato de ejemplo carga para el proceso automático pulsando click sobre el ícono
            <a href="../../Formatos/FormatoTDC_P6.xlsx" target="_blank">
             <span class="glyphicon glyphicon-save" style="font-size:19px;"></span>
                </a>
        </div>
        <asp:Literal ID="ltrNombreArchivo" runat="server" Visible="false"></asp:Literal>
        <div class="form-inline">
            Seleccione el archivo para la carga automática   <asp:FileUpload ID="fupArchivo" runat="server" CssClass="form-control" />
                    <asp:LinkButton ID="lnbCargar" runat="server" CssClass="btn btn-success" OnClick="lnbCargar_Click">
                            <span class="glyphicon glyphicon-open"></span>
                            &nbsp;Cargar Archivo
                     </asp:LinkButton>
            <br />
        </div>
        <br />
        <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
        <asp:Literal ID="ltrNumeroGuia" runat="server" Visible="false"></asp:Literal>
        <asp:Literal ID="ltrFechaEntrega" runat="server" Visible="false"></asp:Literal>
        
    </div>

    <asp:Panel ID="pnlInicial" runat="server">
        <div id="dvIdConsulta" runat="server" class="x_panel">
            <%--Tarjeta--%>
            <div class="x_panel; col-md-6">
                <div class="x_title">
                    <h2>Filtre por los últimos 6 dígitos de la Tarjeta</h2>
                    <div class="clearfix"></div>
                    <br />
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
                         <asp:LinkButton ID="lnbConsultarTarjeta" runat="server" CssClass="btn btn-info" OnClick="lnbConsultarTarjeta_Click" ValidationGroup="principal">
                            <span class="glyphicon glyphicon-credit-card"></span>
                            &nbsp;&nbsp;Filtrar
                        </asp:LinkButton>
                    </div>
                </div> 
                <div class="clearfix"></div>
            </div> 
            <%--Fecha--%>
            <div class="x_panel; col-md-6">
                <div class="alert alert-warning" style="margin-bottom:0px;">
                    Indique la <b>Fecha de Entrega y el Número de Guía</b> de las Tarjetas de Crédito a gestionar, luego de click en el botón <b>Entregada</b>
                </div><div class="clearfix"></div>
                <div class="col-md-3" style="padding-top:18px;">
                        <label>Fecha de Entrega</label>
                        <asp:TextBox ID="txtFechaEntrega" runat="server" CssClass="form-control" ValidationGroup="secundario"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFechaEntrega" runat="server" ControlToValidate="txtFechaEntrega" ErrorMessage="* Campo Requerido" ValidationGroup="secundario"> </asp:RequiredFieldValidator>
                        <asp:CalendarExtender ID="cleFechaEntrega" runat="server" TargetControlID="txtFechaEntrega" Format="yyyy-MM-dd"></asp:CalendarExtender>
                </div>
                <div class="col-md-3" style="padding-top:18px;">
                        <label>Número de Guía</label>
                        <asp:TextBox ID="txtNoGuia" runat="server" CssClass="form-control" ValidationGroup="secundario"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNoGuia" runat="server" ControlToValidate="txtNoGuia" ErrorMessage="* Campo Requerido" ValidationGroup="secundario"> </asp:RequiredFieldValidator>
                </div>
                <div class="clearfix"></div>
             </div>
            <%--Consulta--%>
            <div class="x_content">
                <asp:GridView ID="gdvListaSolicitudes" runat="server" CssClass="table table-hover" 
                    EmptyDataText="No hay solicitues con los filtros seleccionados" 
                        AutoGenerateColumns="False" Width="100%"
                        AllowPaging="true" PageSize="30" GridLines="None"
                        DataKeyNames="tdc_numeroDocumento, tdc_numeroTarjeta" OnSelectedIndexChanging="gdvListaSolicitudes_SelectedIndexChanging"
                        >
                    <Columns>
                        <asp:BoundField DataField="tdc_numeroDocumento" HeaderText="Documento" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="tdc_numeroTarjeta" HeaderText="No. Tarjeta" ItemStyle-HorizontalAlign="Center"  />
                        <asp:BoundField DataField="NOMBRE" HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Left" />
                        <%--<asp:TemplateField HeaderText="Ubicación" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# TieneDireccion(Eval("tdc_direccion").ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prevalidación" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# TienePrevalidacion(Eval("tdc_fechaPrevalidacion").ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Validación" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# TieneValidacion(Eval("tdc_fechaValidacion").ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText=""  ShowSelectButton="true" SelectText="Entregada" ButtonType="Link" ControlStyle-CssClass="btn btn-xs btn-warning" ItemStyle-HorizontalAlign="Center"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
</asp:Panel>
    
</asp:Content>

