<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="HelpManual.aspx.cs" Inherits="TDC_HelpManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">AYUDAS ENTREGA TDC - MANUAL DE USUARIO</div>
    <br />
    <div class="alert alert-info">
        En este módulo encontrará el Manual de Usuario para cada uno de los pasos del Proyecto BPM. Manual creado para aclarar aquellas posibles dudas que se pueden generar
        <br />
        en los procesos, desde las TDC asignadas a clientes hasta la activación de las mismas.
        <br />
        <br />
        Si queda alguna duda en alguno de los pasos, por favor contactse con su jefe directo para que se gestione y aclara a la mayor brevedad posible.
        <br />
    </div>
    <%--Manual--%>
        <div class="x_panel; col-md-6">
            <div class="x_title">
                <h2>Para descargar el archivo haga click en el ícono </h2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <a href="/Imagenes/MU-pBPM_v2_20220615.pdf" target="_blank">
                     <span class="glyphicon glyphicon-save" style="font-size:19px;"></span>
                </a>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                
            </div> 
            <div class="clearfix"></div>
        </div> 
    
</asp:Content>

