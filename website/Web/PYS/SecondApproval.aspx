<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="SecondApproval.aspx.cs" Inherits="PYS_SecondApproval" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function url(URL) {
            hidden = open(URL, 'NewWindow', 'top=0,left=0,width=800,height=600,status=yes,resizable=yes,scrollbars=yes');
        }

        function CheckActualizaVarios(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=gdvListaPazySalvo.ClientID %>");
            for (i = 0; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[10].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">Paz y Salvos disponibles a Validar por los Supervisores
        <asp:Literal ID="ltrTotalRegistros" runat="server"></asp:Literal>
    </div>
    <br />
    <div class="alert alert-info">
        En este módulo los supervisores pueden aprobar o rechazar la generación del Paz y salvo para los clientes de QNT que han efectuado el último pago
        <br />
        según la sincronización efectuada con SALESFORECE, sincronización que se efectúa automáticamente todas las noches.
        <br />
    </div>
<div class="clearfix"></div>

<asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
<asp:Panel ID="pnlInicial" runat="server">
    <div class="x_panel">
        <div class="x_content">
            <div class="row">
                <div style="clear:left;">
                    <div class="col-md-4">
                        <br />
                        Filtrar Paz y salvo en estado&nbsp; 
                        <asp:DropDownList ID="ddlListaEstados" runat="server" CssClass="dropdown">
                            <asp:ListItem Value="-1" Text="Seleccione" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Generado"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Devuelto"></asp:ListItem>
                        </asp:DropDownList>
                        
                        &nbsp;y por&nbsp;  
                        <asp:DropDownList ID="ddlFiltroFecha" runat="server" CssClass="dropdown" OnSelectedIndexChanged="ddlFiltroFecha_SelectedIndexChanged" AutoPostBack ="true">
                            <asp:ListItem Value="-1" Text="Seleccione el filtro" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="pys_fechaUltimoPago" Text="Fecha de Pago"></asp:ListItem>
                            <asp:ListItem Value="pys_fechaSincronizacionSF" Text="Fecha de Proceso"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div id="dvdFechas" runat="server" visible="false">
                       <div class="col-md-2">
                            Desde la Fecha
                            <asp:TextBox ID="txtFechaIni" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFechaIni" runat="server" ControlToValidate="txtFechaIni" ErrorMessage="* Campo Requerido"> </asp:RequiredFieldValidator>
                            <asp:CalendarExtender ID="cleFechaIni" runat="server" TargetControlID="txtFechaIni" Format="yyyy-MM-dd"></asp:CalendarExtender>
                       </div> 
                       <div class="col-md-2">
                            Hasta la Fecha
                            <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFechaFin" runat="server" ControlToValidate="txtFechaFin" ErrorMessage="* Campo Requerido"> </asp:RequiredFieldValidator>
                            <asp:CalendarExtender ID="cleFechaFin" runat="server" TargetControlID="txtFechaFin" Format="yyyy-MM-dd"></asp:CalendarExtender>
                       </div>
                    </div>
                   <div class="col-md-1">
                       <br />
                       <asp:LinkButton ID="lnbListaConsultar" runat="server" CssClass="btn btn-sm btn-warning" OnClick="lnbListaConsultar_Click" CausesValidation="true">Consultar</asp:LinkButton>
                   </div>
                   <div class="col-md-2">
                   </div>
                   <div class="col-md-1">
                       <br />
                        <asp:LinkButton ID="lnbActualizarVarios" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnbActualizarVarios_Click" >Aprobar varios</asp:LinkButton>
                        <br />
                   </div>
                </div>
            </div>
            <div class="row" runat="server">
                <div style="clear:left;">
                    <div class="col-md-3">
                        <br />
                        <asp:TextBox ID="txtListaDocumento" runat="server" placeholder="Del documento no." Width="100%"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="ftbListaDocumento" runat="server" TargetControlID="txtListaDocumento" FilterType="Numbers, Custom" ValidChars="|"></asp:FilteredTextBoxExtender>                
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="dvIdConsulta" runat="server" class="x_panel">
        <%--Consulta--%>
        <div class="x_content">
            <br />
            <br />
            <asp:GridView ID="gdvListaPazySalvo" runat="server" CssClass="table table-hover" 
                EmptyDataText="No hay Paz y Salvos con el Documento o los filtros seleccionados" 
                    AutoGenerateColumns="False" Width="100%"
                    AllowPaging="true" PageSize="30" GridLines="None"
                    DataKeyNames="ID_acuerdo" OnRowCommand="gdvListaPazySalvo_RowCommand"  OnRowEditing="gdvListaPazySalvo_RowEditing" OnRowCancelingEdit="gdvListaPazySalvo_RowCancelingEdit" OnRowUpdating="gdvListaPazySalvo_RowUpdating">
                <Columns>
                    <%--0--%><asp:BoundField DataField="pys_numeroRadicado" HeaderText="Número Radicado" ItemStyle-HorizontalAlign="Center" ReadOnly="true"  />
                    <%--1--%><asp:BoundField DataField="ref_nombre" HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="Center" ReadOnly="true"  />
                    <%--2--%><asp:BoundField DataField="cli_numeroDocumento" HeaderText="Número Documento" ItemStyle-HorizontalAlign="Center" ReadOnly="true"  />
                    <%--3--%><asp:BoundField DataField="acu_numeroAcuerdo" HeaderText="Número Acuerdo" ItemStyle-HorizontalAlign="Center" ReadOnly="true"  />
                    <%--4--%><asp:BoundField DataField="acu_nombresProductos" HeaderText="Productos" ItemStyle-HorizontalAlign="Center" ReadOnly="true"  />
                    <%--5--%><asp:BoundField DataField="pys_fechaUltimoPago" HeaderText="Ultimo Pago" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" ReadOnly="true"  />
                    <%--6--%><asp:BoundField DataField="pys_fechaSincronizacionSF" HeaderText="Fecha Proceso" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" ReadOnly="true"  />
                    <%--7--%><asp:BoundField DataField="pys_causalAsesor" HeaderText="Causal Asesor" ItemStyle-HorizontalAlign="Center" ReadOnly="true"  />
                    <%--8--%><asp:TemplateField HeaderText="Causal Supervisor" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Eval("pys_causalSupervisor").ToString() %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCausalRechazo" runat="server" ValidationGroup="rechazarSupervisor"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCausal" runat="server" ControlToValidate="txtCausalRechazo" ErrorMessage="*" ValidationGroup="rechazarSupervisor"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                             </asp:TemplateField>
                    <%--9--%><asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton  ID="lnbAprobar" runat="server" CommandName="Aprobar" CommandArgument="<%# Container.DataItemIndex %>" Text="Aprobar" CssClass="btn btn-xs btn-success" />
                                </ItemTemplate>
                            </asp:TemplateField>
                    <%--10--%><asp:TemplateField ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>Apr. Varios
                                    <asp:CheckBox ID="chkActualizaVariosEncabezado" runat="server" onclick="CheckActualizaVarios(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActualizaVarios" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                    <%--11--%><asp:CommandField ShowEditButton="true" EditText="Rechazar"/>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Panel>

</asp:Content>


