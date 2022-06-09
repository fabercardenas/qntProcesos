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
                <label>Filtrar por:</label>
                <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged" AutoPostBack ="true">
                    <asp:ListItem Value="tdc_numeroDocumento" Text="No. Documento"></asp:ListItem>
                    <asp:ListItem Value="tdc_contrato" Text="No. Contrato"></asp:ListItem>
                    <asp:ListItem Value="tdc_numeroTarjeta" Text="No. Tarjeta"></asp:ListItem>
                    <asp:ListItem Value="Paso" Text="Paso"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div id="dvFiltro2" runat="server" class="col-md-4" visible="false" >
                <label><br /></label>
                <asp:DropDownList ID="ddlPasos" runat="server" CssClass="form-control" ValidationGroup="principal" >
                    <asp:ListItem Value="Paso1" Text="Carga Tarjetas Aprobadas - Paso 1"></asp:ListItem>
                    <asp:ListItem Value="Paso2" Text="Carga de Información Tarjetas - Paso 2"></asp:ListItem>
                    <asp:ListItem Value="Paso3" Text="Sincronización de Ubicaciones - Paso 3"></asp:ListItem>
                    <asp:ListItem Value="Paso4" Text="Consulta Prevalidación - Paso 4"></asp:ListItem>
                    <asp:ListItem Value="Paso5" Text="Consulta Validación - Paso 5"></asp:ListItem>
                    <asp:ListItem Value="Paso6" Text="Consulta Confirmación de Entrega - Paso 6"></asp:ListItem>
                    <asp:ListItem Value="Paso7.1" Text="Sincronización de Flujo Digital - Paso 7.1"></asp:ListItem>
                    <asp:ListItem Value="Paso7.2" Text="Carga de Pagaré - Paso 7.2"></asp:ListItem>
                    <asp:ListItem Value="Paso7.3" Text="Validar Documentación - Paso 7.3"></asp:ListItem>
                    <asp:ListItem Value="Paso8.1" Text="Sincronización de Días Mora - Paso 8.1"></asp:ListItem>
                    <asp:ListItem Value="Paso8.2" Text="Tarjetas para Activación - Paso 8.2"></asp:ListItem>
                    <asp:ListItem Value="Paso9" Text="Carga de Archivo para Activación TDC - Paso 9"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div id="dvConsultaF" runat="server" class="col-md-4" visible="True" >
                <label><br /></label>
                <asp:TextBox ID="txtConsultaFiltro" runat="server" CssClass="form-control" ValidationGroup="principal"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNumTarjeta" runat="server" ControlToValidate="txtConsultaFiltro" ErrorMessage="* Campo Requerido" ValidationGroup="principal"> </asp:RequiredFieldValidator>
            </div>
            <div class="col-md-4" style="padding-top:20px;">
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click"
                        CssClass="btn btn-primary"  ValidationGroup="principal" />
            </div>
            <br />
            <br />
            <br />
            
            <asp:FormView ID="frvCondultarSolicitud" runat="server" CssClass="tdGris02"  
                CaptionAlign="Left" Width="100%" AllowPaging="True" PagerSettings-Mode="NumericFirstLast" PagerSettings-PageButtonCount="50"
                CellPadding="0" PagerStyle-BackColor="White">
                <ItemTemplate>
                    <div id="dvDetSolicitud" style="width:100%; text-align:center;">
                        <div class="x_panel">
                            <div class="x_content">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div style="float:left;">
                                            <i class="fa fa-user icoInfo" ></i>
                                        </div>
                                        <div style="float:left;padding-left:10px;text-align:left;">
                                            <span style="margin-top:0px;font-size:16px;">
                                                <b><%# Eval("NOMBRE").ToString()%></b>
                                            </span>
                                            <br />
                                            <span> <%#Eval("ref_nombre").ToString()%> <%#Eval("tdc_numeroDocumento").ToString()%></span>
                                            <br />
                                            <span> <%#Eval("tdc_direccion").ToString()%></span>
                                            <br />
                                            <span> <%#Eval("tdc_ciudad").ToString()%></span>
                                            <br />
                                            <span> <%#Eval("TELEFONOS").ToString()%></span>
                                        </div> 
                                    </div>
                                    <div class="col-md-3">
                                        <div style="float:left;">
                                            <i class="fa fa-credit-card-alt icoInfo" ></i>
                                        </div>
                                        <div style="float:left;padding-left:10px;text-align:left;">
                                            <b><span> <%#Eval("TARJETA").ToString()%></span></b>
                                            <br />
                                            <span>No. Contrato: <%#Eval("tdc_contrato").ToString()%></span>
                                            <br />
                                            <span>Fecha Realce: <%# string.Format("{0:yyyy-MM-dd}", Eval("tdc_fechaRealce"))%></span>
                                            <br />
                                            <span>Fecha Registro: <%# string.Format("{0:yyyy-MM-dd hh:mm}", Eval("tdc_fechaRegistraInfo"))%></span>
                                            <br />
                                            <span>Usuario Registro: <%# Eval("USUARIOREGISTRA").ToString()%></span>
                                        </div> 
                                    </div>
                                    <div class="col-md-3">
                                        <div style="float:left;">
                                            <i class="fa fa-info-circle icoInfo" ></i>
                                        </div>
                                        <div style="float:left;padding-left:10px;text-align:left;">
                                            <span>Prevalidación: <%# string.Format("{0:yyyy-MM-dd hh:mm}", Eval("tdc_fechaPrevalidacion"))%></span>
                                            <br />
                                            <span>Prevalidado por: <%# Eval("USUARIOPREVALIDA").ToString()%></span>
                                            <br />
                                            <span>Validación: <%# string.Format("{0:yyyy-MM-dd hh:mm}", Eval("tdc_fechaValidacion"))%></span>
                                            <br />
                                            <span>Validado por: <%# Eval("USUARIOVALIDA").ToString()%></span>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div style="float:left;">
                                            <i class="fa fa-spinner icoInfo" ></i>
                                        </div>
                                        <div style="float:left;padding-left:10px;text-align:left;">
                                            <span>Flujo Digital: <%# string.Format("{0:yyyy-MM-dd hh:mm}", Eval("tdc_fechaRegistroFD"))%></span>
                                            <br />
                                            <span>FD Registrado por: <%# Eval("USUARIOFD").ToString()%></span>
                                            <br />
                                            <span>Pagaré: <%# string.Format("{0:yyyy-MM-dd hh:mm}", Eval("tdc_fechaRegistroPagare"))%></span>
                                            <br />
                                            <span>Registrado por: <%# Eval("USUARIOPAGARE").ToString()%></span>
                                           
                                        </div>
                                    </div>
                                </div> 
                                <br />
                                <br />
                                <div class="row justify-content-md-center form-inline">
                                    <div class="col col-md-1" style="text-align:center;">
                                        &nbsp;
                                    </div>
                                    <div class="col col-md-3">
                                        <div style="float:left;">
                                            <i class="fa fa-file-text icoInfo" ></i>
                                        </div>
                                        <div style="float: left;padding-left:10px;text-align:left;">
                                             <span>Documento de Identidad: <%# string.Format("{0:yyyy-MM-dd}", Eval("tdc_fechaRegistroDocIdent"))%></span>
                                            <br />
                                            <span>Registrado por: <%#Eval("USUARIODOCIDENTIDAD")%></span>
                                            <br />
                                            <span>Soporte de Ingresos: <%# string.Format("{0:yyyy-MM-dd}", Eval("tdc_fechaEntrega"))%></span>
                                            <br />
                                            <span>Registrado por: <%# Eval("USUARIOENTREGA").ToString()%></span>
                                        </div>
                                    </div>
                                    <div class="col col-md-1" style="text-align:center;">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3">
                                        <div style="float:left;">
                                            <i class=" fa fa-space-shuttle icoInfo" ></i>
                                        </div>
                                        <div style="float:left;padding-left:10px;text-align:left;">
                                            <span>Fecha Entrega: <%# string.Format("{0:yyyy-MM-dd}", Eval("tdc_fechaEntrega"))%></span>
                                            <br />
                                            <span>No. Guía: <%#Eval("tdc_numeroGuia")%></span>
                                            <br />
                                            <span>Entregado por: <%# Eval("USUARIOENTREGA").ToString()%></span>
                                            <br />
                                            <span>Tarjeta Activada el: <%# string.Format("{0:yyyy-MM-dd}", Eval("tdc_fechaActivacion"))%></span>
                                            <br />
                                            <span>Activada por: <%# Eval("USUARIOACTIVACION").ToString()%></span>
                                        </div>
                                    </div>
                                    <div class="col col-md-4" style="text-align:center;">
                                        <b><span>PASO: <%#Eval("tdc_paso")%></span></b>
                                        <br />
                                        <asp:Button ID="btnDevolverPaso" runat="server" Text="Cambiar de Paso" OnClick="btnDevolverPaso_Click" 
                                            CssClass="btn btn-warning"  ValidationGroup="principal"
                                             Visible='<%# ((Eval("tdc_paso").ToString()=="9")? false:true)%>'/>
                                        <br />
                                        <br />
                                            <asp:DropDownList ID="ddlCambioPaso" runat="server" CssClass="form-control" ValidationGroup="principal" Visible="false"  >
                                                <asp:ListItem Value="Paso1" Text="Carga Tarjetas Aprobadas - Paso 1"></asp:ListItem>
                                                <asp:ListItem Value="Paso2" Text="Carga de Información Tarjetas - Paso 2"></asp:ListItem>
                                                <asp:ListItem Value="Paso3" Text="Sincronización de Ubicaciones - Paso 3"></asp:ListItem>
                                                <asp:ListItem Value="Paso4" Text="Consulta Prevalidación - Paso 4"></asp:ListItem>
                                                <asp:ListItem Value="Paso5" Text="Consulta Validación - Paso 5"></asp:ListItem>
                                                <asp:ListItem Value="Paso6" Text="Consulta Confirmación de Entrega - Paso 6"></asp:ListItem>
                                                <asp:ListItem Value="Paso7.1" Text="Sincronización de Flujo Digital - Paso 7.1"></asp:ListItem>
                                                <asp:ListItem Value="Paso7.2" Text="Carga de Pagaré - Paso 7.2"></asp:ListItem>
                                                <asp:ListItem Value="Paso7.3" Text="Validar Documentación - Paso 7.3"></asp:ListItem>
                                                <asp:ListItem Value="Paso8.1" Text="Sincronización de Días Mora - Paso 8.1"></asp:ListItem>
                                                <asp:ListItem Value="Paso8.2" Text="Tarjetas para Activación - Paso 8.2"></asp:ListItem>
                                                <asp:ListItem Value="Paso9" Text="Carga de Archivo para Activación TDC - Paso 9"></asp:ListItem>
                                                </asp:DropDownList>
                                            <asp:Button ID="btnDevolver" runat="server" Text="Cambiar" OnClick="btnDevolver_Click" 
                                                CssClass="btn btn-danger"  ValidationGroup="principal"
                                                Visible="false"/>
                                    </div>
                                </div>
                            </div> 
                        </div> 
                    </div> 
                </ItemTemplate>
                 <PagerSettings FirstPageText="<<  " LastPageText="  >>" PageButtonCount="10" Position="TopAndBottom" />
                 <PagerStyle HorizontalAlign="Center" CssClass="pager"  />
                 <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            </asp:FormView>
            
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


