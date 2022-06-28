<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="FirstApproval.aspx.cs" Inherits="PYS_FirstApproval" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Paz y Salvos disponibles a Validar por los Asesores</div>
    <br />
    <div class="alert alert-info">
        En este módulo los asesores pueden aprobar o rechazar la generación del Paz y salvo para los clientes de QNT que han efectuado el último pago
        <br />
        según la sincronización efectuada con SALESFORECE, sincronización que se efectúa automáticamente todas las noches.
        <br />
    </div>
    <div class="form-inline">
        <asp:LinkButton ID="lnbCargar" runat="server" CssClass="btn btn-info" OnClick="lnbCargar_Click">
                        <span class="glyphicon glyphicon-refresh"></span>
                        &nbsp;Sincronizar Clientes para Paz y Salvo con Salesforce
        </asp:LinkButton>
        <br />
    </div>

    <div class="page-title">
        <div class="title_left">
            <h3>Paz y Salvos Pendientes por aprobar&nbsp; 
                <asp:Literal ID="ltrTotalRegistros" runat="server"></asp:Literal>
            </h3>
        </div>
    </div>
<div class="clearfix"></div>

<asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
<asp:Literal ID="ltrFechaPrevalidacion" runat="server" Visible="false"></asp:Literal>

<asp:Panel ID="pnlInicial" runat="server">
    <div class="x_panel">
        <div class="x_content">
            <div class="row">
                <div style="clear:left;">
                    <div class="col-md-4">
                        <br />
                        Filtrar Paz y salvo en estado&nbsp; 
                        <asp:DropDownList ID="ddlListaEstados" runat="server" CssClass="dropdown" >
                            <asp:ListItem Value="-1" Text="Seleccione"></asp:ListItem>
                            <asp:ListItem>Generado</asp:ListItem>
                            <asp:ListItem>Devuelto</asp:ListItem>
                        </asp:DropDownList>
                        
                        &nbsp;y por&nbsp;  
                        <asp:DropDownList ID="ddlFiltroFecha" runat="server" CssClass="dropdown" OnSelectedIndexChanged="ddlFiltroFecha_SelectedIndexChanged" AutoPostBack ="true">
                            <asp:ListItem Value="-1" Text="Seleccione el filtro"></asp:ListItem>
                            <asp:ListItem>Fecha de Pago</asp:ListItem>
                            <asp:ListItem>Fecha de Proceso</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div id="dvdFechas" runat="server" visible="false">
                       <div class="col-md-2">
                            Desde la Fecha
                            <asp:TextBox ID="txtFechaIni" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFechaEntrega" runat="server" ControlToValidate="txtFechaIni" ErrorMessage="* Campo Requerido" ValidationGroup="secundario"> </asp:RequiredFieldValidator>
                            <asp:CalendarExtender ID="cleFechaEntrega" runat="server" TargetControlID="txtFechaIni" Format="yyyy-MM-dd"></asp:CalendarExtender>
                       </div> 
                       <div class="col-md-2">
                            Hasta la Fecha
                            <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFechaFin" ErrorMessage="* Campo Requerido" ValidationGroup="secundario"> </asp:RequiredFieldValidator>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFechaFin" Format="yyyy-MM-dd"></asp:CalendarExtender>
                       </div>
                    </div>
                   <div class="col-md-1">
                       <br />
                       <asp:LinkButton ID="lnbListaConsultar" runat="server" CssClass="btn btn-sm btn-warning">Consultar</asp:LinkButton>
                        <%--<asp:LinkButton ID="lnbListaConsultar" runat="server" CssClass="btn btn-sm btn-warning" OnClick="lnbListaConsultar_Click">Consultar</asp:LinkButton>--%>
                   </div>
                   <div class="col-md-2">
                   </div>
                   <div class="col-md-1">
                       <br />
                        <asp:LinkButton ID="lnbActualizarVarios" runat="server" CssClass="btn btn-sm btn-primary">Aprobar varios</asp:LinkButton>
                        <%--<asp:LinkButton ID="lnbActualizarVarios" runat="server" CssClass="btn btn-sm btn-primary" Visible="false" OnClick="lnbActualizarVarios_Click" >Aprobar varios</asp:LinkButton>--%>
                        <br />
                   </div>
                </div>
            </div>
            <div class="row" runat="server">
                <div style="clear:left;">
                    <div class="col-md-10">
                        <br />
                        <asp:TextBox ID="txtListaDocumento" runat="server" placeholder="Del documento o los documentos No" Width="100%"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="ftbListaDocumento" runat="server" TargetControlID="txtListaDocumento" FilterType="Numbers, Custom" ValidChars="|"></asp:FilteredTextBoxExtender>                
                    </div>
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
            <asp:GridView ID="gdvListaPazySalvo" runat="server" CssClass="table table-hover" 
                EmptyDataText="No hay Paz y Salvos con los filtros seleccionados" 
                    AutoGenerateColumns="False" Width="100%"
                    AllowPaging="true" PageSize="30" GridLines="None"
                    DataKeyNames="ID_acuerdo">
                <Columns>
                    <asp:BoundField DataField="pys_numeroRadicado" HeaderText="Número Radicado" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="ref_nombre" HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="Center"  />
                    <asp:BoundField DataField="cli_numeroDocumento" HeaderText="Número Documento" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="acu_numeroAcuerdo" HeaderText="Número Acuerdo" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="acu_nombresProductos" HeaderText="Productos" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="pys_fechaUltimoPago" HeaderText="Ultimo Pago" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="pys_fechaSincronizacionSF" HeaderText="Fecha Proceso" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:TemplateField ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>Apr. Varios
                            <asp:CheckBox ID="chkActualizaVariosEncabezado" runat="server" onclick="CheckActualizaVarios(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkActualizaVarios" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Panel>

</asp:Content>


