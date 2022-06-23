<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Web/MasterPage.master" AutoEventWireup="true" CodeFile="HelpVideos.aspx.cs" Inherits="TDC_HelpVideos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tdImgVerde04">AYUDAS TARJETAS DE CRÉDITO CLIENTES QNT- VIDEOS</div>
    <br />
    <div class="alert alert-info">
        En este módulo encontrará los videos para cada uno de los pasos del Proyecto BPM. Videos creados para aclarar aquellas posibles dudas que se pueden generar
        <br />
        en los procesos, desde las TDC asignadas a clientes hasta la activación de las mismas.
        <br />
        <br />
        Si queda alguna duda en alguno de los pasos, por favor contactse con su jefe directo para que se gestione y aclara a la mayor brevedad posible.
        <br />
    </div>
    <%--Videos--%>
    <div class="row">
        <div class="x_panel; col-md-6">
            <div class="x_title">
                <h2>Carga Tarjetas Aprobadas - Paso 1</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="col-md-3">
                   <video id="video1" controls type="video/mp4" width="500px" height="300px" >
                        <source src="/Imagenes/BPM-Paso1.mp4" type="video/mp4" />
                        Tu navegador no soporta vídeo
                    </video>
                </div>
            </div> 
            <div class="clearfix"></div>
        </div> 
        <div class="x_panel; col-md-6">
            <div class="x_title">
                <h2>Carga de Información de Tarjetas - Paso 2</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="col-md-3">
                   <video id="video2" controls type="video/mp4" width="500px" height="300px" >
                        <source src="/Imagenes/BPM-Paso2.mp4" type="video/mp4" />
                        Tu navegador no soporta vídeo
                    </video>
                </div>
            </div> 
            <div class="clearfix"></div>
        </div>
        <br />
    </div>
    <div class="row">
        <div class="x_panel; col-md-6">
            <div class="x_title">
                <h2>Sincronización de Ubicaciones - Paso 3</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="col-md-3">
                   <video id="video3" controls type="video/mp4" width="500px" height="300px" >
                        <source src="/Imagenes/BPM-Paso3.mp4" type="video/mp4" />
                        Tu navegador no soporta vídeo
                    </video>
                </div>
            </div> 
            <div class="clearfix"></div>
        </div>
        <div class="x_panel; col-md-6">
            <div class="x_title">
                <h2>Consulta Prevalidación - Paso 4</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="col-md-3">
                   <video id="video4" controls type="video/mp4" width="500px" height="300px" >
                        <source src="/Imagenes/BPM-Paso4.mp4" type="video/mp4" />
                        Tu navegador no soporta vídeo
                    </video>
                </div>
            </div> 
            <div class="clearfix"></div>
        </div>
        <br />
    </div>
    <div class="row">
        <div class="x_panel; col-md-6">
            <div class="x_title">
                <h2>Consulta Validación - Paso 5</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="col-md-3">
                   <video id="video5" controls type="video/mp4" width="500px" height="300px" >
                        <source src="/Imagenes/BPM-Paso5.mp4" type="video/mp4" />
                        Tu navegador no soporta vídeo
                    </video>
                </div>
            </div> 
            <div class="clearfix"></div>
        </div>
        <div class="x_panel; col-md-6">
            <div class="x_title">
                <h2>Consulta Confirmación de Entrega- Paso 6</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="col-md-3">
                   <video id="video6" controls type="video/mp4" width="500px" height="300px" >
                        <source src="/Imagenes/BPM-Paso6.mp4" type="video/mp4" />
                        Tu navegador no soporta vídeo
                    </video>
                </div>
            </div> 
            <div class="clearfix"></div>
        </div>
        <br />
    </div>
    <div class="row">
        <div class="x_panel; col-md-6">
            <div class="x_title">
                <h2>Documentos TDC - Pasos 7</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="col-md-3">
                   <video id="video7" controls type="video/mp4" width="500px" height="300px" >
                        <source src="/Imagenes/BPM-Paso7.mp4" type="video/mp4" />
                        Tu navegador no soporta vídeo
                    </video>
                </div>
            </div> 
            <div class="clearfix"></div>
        </div>
        <div class="x_panel; col-md-6">
            <div class="x_title">
                <h2>Activación TDC - Pasos 8 y Paso 9</h2>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <div class="col-md-3">
                   <video id="video8" controls type="video/mp4" width="500px" height="300px" >
                        <source src="/Imagenes/BPM-Paso8.mp4" type="video/mp4" />
                        Tu navegador no soporta vídeo
                    </video>
                </div>
            </div> 
            <div class="clearfix"></div>
        </div>
        <br />
    </div>
</asp:Content>

