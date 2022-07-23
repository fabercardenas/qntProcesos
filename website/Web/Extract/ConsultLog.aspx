<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.master" Async="true" AutoEventWireup="true" CodeFile="ConsultLog.aspx.cs" Inherits="CosultLog" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div class="alert alert-info">
        En este módulo se consulta el estado del envío de correos electrónicos para los extractos de los clientes de QNT.
        <br />
        Efectuando la consulta por el número de identificación del cliente de interés.
        <br />
        <br />
    </div>
    <div class="clearfix"></div>

<asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>

<asp:Panel ID="pnlInicial" runat="server">
    <div id="dvIdConsulta" runat="server">
        <%--Tarjeta--%>
        <div class="x_panel">
            <div class="x_title" >
                <div class="col-md-2">
                    <h2>Documento del cliente</h2>
                </div>
                <div class="col-md-8 form-inline" >
                    <asp:RequiredFieldValidator ID="rfvNumTarjeta" runat="server" ControlToValidate="txtNumDocumento" ErrorMessage="* Requerido" ValidationGroup="principal"> </asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtNumDocumento" runat="server" CssClass="form-control" ValidationGroup="principal"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="fteNumTarjeta" runat="server" TargetControlID="txtNumDocumento" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                    
                     <asp:LinkButton ID="lnbConsultarDocumento" runat="server" CssClass="btn btn-primary" OnClick="lnbConsultarDocumento_Click" ValidationGroup="principal">
                        <span class="fa fa-send"></span>
                        &nbsp;&nbsp;Consultar
                    </asp:LinkButton>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div style="float:left; width:50%;">
                    <asp:GridView ID="gdvLista" runat="server" CssClass="table table-hover" 
                        EmptyDataText="No hay solicitues con los filtros seleccionados" 
                        AutoGenerateColumns="False" Width="100%" AllowPaging="true" PageSize="30" GridLines="None"
                        OnSelectedIndexChanging="gdvLista_SelectedIndexChanging" DataKeyNames="ext_data, ext_machineName"
                        >
                        <Columns>
                            <asp:BoundField DataField="ext_nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center"  />
                            <asp:BoundField DataField="ext_mail" HeaderText="Email" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="ext_manchineUser" HeaderText="Enviado por" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="ext_data" HeaderText="Id Envio" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="ext_fechaRegistro" HeaderText="Fecha Envio" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                            <asp:CommandField ShowSelectButton="true" SelectText="Ver" ItemStyle-CssClass="btn btn-xs btn-info" ButtonType="Link" />
                        </Columns>
                        <SelectedRowStyle BackColor="#e7e7e7" />
                    </asp:GridView>
                </div>
                <div style="float:left; padding-left:20px; width:50%;">
                    <div class="x_panel" id="dvDatosEnvio" runat="server" visible="false">
                        <div class="x_content">
                            <h2>Informacion de intico</h2>
                            <asp:Literal ID="ltrIntMensaje" runat="server"></asp:Literal>
                            <div id="dvIntInformacion" runat="server">
                                <b>Fecha Carga : </b><asp:Literal ID="ltrIntFechaCarga" runat="server"></asp:Literal><br />
                                <b>Estado : </b><asp:Literal ID="ltrIntEstado" runat="server"></asp:Literal><br />
                                <b>Fecha Estado : </b><asp:Literal ID="ltrIntHoraEstado" runat="server"></asp:Literal><br />
                                <b>Navegador : </b><asp:Literal ID="ltrIntBrowser" runat="server"></asp:Literal><br />
                                <b>Sistema Operativo : </b><asp:Literal ID="ltrIntOs" runat="server"></asp:Literal><br />
                                <b>Ubicacion : </b><asp:Literal ID="ltrIntUbicacion" runat="server"></asp:Literal><br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div> 
    </div>

</asp:Panel>

</asp:Content>
