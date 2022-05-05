<%@ Page Title="" Language="C#" MasterPageFile="Web/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="gdvDatos" runat="server" AutoGenerateColumns="True" CellPadding="5" CssClass="gdvExcelStyle" GridLines="Both" BorderColor="#389F7C"
        Width="100%" EmptyDataText = "No hay datos por mostrar" ShowFooter="true"  >
        <HeaderStyle CssClass="gdvHeaderstyle" />
        <RowStyle CssClass="gdvRowstyle" Font-Size="12px" />
        <AlternatingRowStyle CssClass="gdvAltrowstyle"  Font-Size="12px" />
    </asp:GridView>
    <br />
    <div class="container body-content">
        <div class="col-lg-12">
            <h2 class="page-header">
                Business Project Management
            </h2>
        </div>
        <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
        <div class="row" style="text-align:center;">
            <img src="Imagenes/logoQnt.png" alt="" />
        </div>
        <div class="row" id="dvIndicadorEmpresaUsuaria" runat="server">
            <div class="col-lg-4">
                <div class="panel panel-green">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <span class="glyphicon glyphicon-user" style="font-size:40px;"></span>                
                            </div>
                            <div class="col-xs-9 text-right">
                                <div style="font-size:30px;">16</div>
                                <div>Empledos Nuevos</div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <span class="pull-left">Empleados Activos</span>
                        <span class="pull-right">76</span>
                        <div class="clearfix"></div>

                        <span class="pull-left">Empleados Retirados</span>
                        <span class="pull-right">15</span>
                        <div class="clearfix"></div>

                        <span class="pull-left">Empleados Por retirar</span>
                        <span class="pull-right">1</span>
                        <div class="clearfix"></div>

                        <span class="pull-left">Cumpleaños</span>
                        <span class="pull-right">3</span>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <span class="glyphicon glyphicon-blackboard" style="font-size:40px;"></span>                
                            </div>
                            <div class="col-xs-9 text-right">
                                <div style="font-size:30px;">8</div>
                                <div>Ordenes de contratación nuevas</div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="panel-footer">
                        <span class="pull-left">Ordenes solicitadas</span>
                        <span class="pull-right">12</span>
                        <div class="clearfix"></div>

                        <span class="pull-left">Ordenes en recepcion</span>
                        <span class="pull-right">2</span>
                        <div class="clearfix"></div>

                        <span class="pull-left">Ordenes en laboratorio</span>
                        <span class="pull-right">9</span>
                        <div class="clearfix"></div>

                        <span class="pull-left">Ordenes en contratación</span>
                        <span class="pull-right">12</span>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="panel panel-yellow">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <span class="glyphicon glyphicon-list-alt" style="font-size:40px;"></span>                
                            </div>
                            <div class="col-xs-9 text-right">
                                <div style="font-size:30px;">2</div>
                                <div>Nóminas por aprobar</div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <span class="pull-left">Nóminas aprobadas</span>
                        <span class="pull-right">5</span>
                        <div class="clearfix"></div>

                        <span class="pull-left">Nóminas soportadas</span>
                        <span class="pull-right">1</span>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

