<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="GetRequest.aspx.cs" Inherits="TDC_GetRequest" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
            <div class="col-md-2">
                <label>Filtrar por:</label>
                <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged" AutoPostBack ="true">
                    <asp:ListItem Value="tdc_numeroDocumento" Text="No. Documento"></asp:ListItem>
                    <asp:ListItem Value="tdc_contrato" Text="No. Contrato"></asp:ListItem>
                    <asp:ListItem Value="tdc_numeroTarjeta" Text="No. Tarjeta"></asp:ListItem>
                    <asp:ListItem Value="tdc_paso" Text="Paso"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div id="dvFiltro2" runat="server" class="col-md-4" visible="false" >
                <label><br /></label>
                <asp:DropDownList ID="ddlPasos" runat="server" CssClass="form-control" ValidationGroup="principal" >
                    <asp:ListItem Value="1" Text="Carga Tarjetas Aprobadas - Paso 1"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Carga de Información Tarjetas - Paso 2"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Sincronización de Ubicaciones - Paso 3"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Consulta Prevalidación - Paso 4"></asp:ListItem>
                    <asp:ListItem Value="5" Text="Consulta Validación - Paso 5"></asp:ListItem>
                    <asp:ListItem Value="6" Text="Consulta Confirmación de Entrega - Paso 6"></asp:ListItem>
                    <asp:ListItem Value="71" Text="Tienen Sincronización de Flujo Digital - Paso 7.1"></asp:ListItem>
                    <asp:ListItem Value="72" Text="Tienen Pagaré Cargado- Paso 7.2"></asp:ListItem>
                    <asp:ListItem Value="73" Text="Tienen Doc. Identidad y Soporte de Ingresos - Paso 7.3"></asp:ListItem>
                    <asp:ListItem Value="8" Text="Tarjetas para Activación  - Paso 8.1 y Paso 8.2"></asp:ListItem>
                    <asp:ListItem Value="9" Text="Carga de Archivo para Activación TDC - Paso 9"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div id="dvConsultaF" runat="server" class="col-md-4" visible="True" >
                <label><br /></label>
                <asp:TextBox ID="txtConsultaFiltro" runat="server" CssClass="form-control" ValidationGroup="principal"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNumTarjeta" runat="server" ControlToValidate="txtConsultaFiltro" ErrorMessage="* Campo Requerido" ValidationGroup="principal"> </asp:RequiredFieldValidator>
            </div>
            <div class="col-md-2" style="padding-top:20px;">
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click"
                        CssClass="btn btn-primary"  ValidationGroup="principal" />
            </div>
            <div class="col-md-2" style="padding-top:20px;" visible="False">
                <asp:Button ID="btnGenerarExcel" runat="server" Text="Generar Archivo con X Registros" OnClick="btnGenerarExcel_Click"
                        CssClass="btn btn-warning"  ValidationGroup="principal" />
            </div>
            <br />
            <br />
            <br />
            
            <asp:FormView ID="frvConsultarSolicitud" runat="server" CssClass="tdGris02"  
                CaptionAlign="Left" Width="100%" AllowPaging="True" PagerSettings-Mode="NumericFirstLast" PagerSettings-PageButtonCount="50"
                CellPadding="0" DataKeyNames="ID_tdcSolicitud" PagerStyle-BackColor="White">
                <ItemTemplate>
                    <div id="dvDetSolicitud" style="width:100%; text-align:center;">
                        <asp:Literal ID="ltrIdSolicitud" runat="server" Visible="false" Text='<%# Eval("ID_tdcSolicitud").ToString()%>'></asp:Literal>
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
                                        <b><span>Se encuentra en el Paso: <%#Eval("tdc_paso")%></span></b>
                                        <br />
                                        <asp:Literal ID="ltrHdfPasoActual" runat="server" Visible="false" Text='<%#Eval("tdc_paso").ToString()%>'></asp:Literal>
                                        <asp:Literal ID="ltrHdfDireccion" runat="server" Visible="false" Text='<%#Eval("tdc_direccion").ToString()%>'></asp:Literal>
                                        <asp:Literal ID="ltrMensajePaso" runat="server"></asp:Literal>
                                        <br />
                                        <asp:Button ID="btnDevolverPaso" runat="server" Text="Cambiar de Paso" OnClick="btnDevolverPaso_Click" 
                                            CssClass="btn btn-warning"  ValidationGroup="principal"
                                             Visible='<%# (((Eval("tdc_paso").ToString()=="9") || (Eval("tdc_paso").ToString()=="1"))? false:true)%>'/>
                                        <br />
                                        <br />
                                            <asp:DropDownList ID="ddlCambioPaso" runat="server" CssClass="form-control" ValidationGroup="principal" Visible="false"  >
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
                    AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gdvListaSolicitudes_PageIndexChanging"
                    AllowPaging="true" PageSize="30" GridLines="None" 
                    >
                <Columns>
                    <%--Columna 0--%><asp:BoundField DataField="ref_nombre" HeaderText="Tipo Documento" />
                    <%--Columna 1--%><asp:BoundField DataField="tdc_numeroDocumento" HeaderText="Documento" />
                    <%--Columna 2--%><asp:BoundField DataField="NOMBRE" HeaderText="Nombres y Apellidos" />
                    <%--Columna 3--%><asp:BoundField DataField="tdc_direccion" HeaderText="Dirección" />
                    <%--Columna 4--%><asp:BoundField DataField="tdc_ciudad" HeaderText="Ciudad" />
                    <%--Columna 5--%><asp:BoundField DataField="tdc_correo" HeaderText="Correo" />
                    <%--Columna 6--%><asp:BoundField DataField="tdc_celular" HeaderText="Celular" />
                    <%--Columna 7--%><asp:BoundField DataField="tdc_telefono" HeaderText="Teléfono" />
                    <%--Columna 8--%><asp:BoundField DataField="TARJETA" HeaderText="No. Tarjeta" />
                    <%--Columna 9--%><asp:BoundField DataField="tdc_fechaRealce" HeaderText="Fecha realce" DataFormatString="{0:yyyy-MM-dd}" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Panel>

</asp:Content>


