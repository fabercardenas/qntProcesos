<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterReportesExcel.master" AutoEventWireup="true" CodeFile="R_EnvioInterrapidisimo.aspx.cs" Inherits="WebForms_Reportes_R_EnvioInterrapidisimo" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1252");
        Response.Charset = "utf-8";
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename= ReporteEmpleadosGeneral.xls");
    %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <asp:GridView ID="gdvDatos" runat="server" AutoGenerateColumns="False" CellPadding="5" CssClass="gdvExcelStyle" GridLines="Both" BorderColor="#389F7C"
                Width="100%" EmptyDataText = "No hay datos por mostrar" ShowFooter="true" >
                <Columns>
                    <asp:BoundField DataField="emp_nombreCorto" HeaderText="Empresa Temporal" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="cli_nombre" HeaderText="Empresa Usuaria" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="cli_nit" HeaderText="Empresa Usuaria Nit" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="cen_nombre" HeaderText="Centro Costo" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_tipoDocumento" HeaderText="Tipo Documento" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_documento" HeaderText="Documento" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_apellido1" HeaderText="Primer apellido" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_apellido2" HeaderText="Segundo apellido" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_nombre1" HeaderText="Primer nombre" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_nombre2" HeaderText="Segundo nombre" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    
                    <asp:BoundField DataField="afi_fechaExpedicion" HeaderText="Fecha Expedicion" DataFormatString="{0:yyyy-MM-dd}" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="expedicionPais" HeaderText="Expedicion Pais" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="expedicionDepartamento" HeaderText="Expedicion Departamento" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="expedicionMunicipio" HeaderText="Expedicion Municipio" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>

                    <asp:BoundField DataField="afi_fechaNacimiento" HeaderText="Fecha Nacimiento" DataFormatString="{0:yyyy-MM-dd}" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="naciPais" HeaderText="Nacimiento Pais" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="naciDepartamento" HeaderText="Nacimiento Departamento" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="naciMunicipio" HeaderText="Nacimiento Municipio" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>

                    <asp:BoundField DataField="afi_mail" HeaderText="Correo" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_direccion" HeaderText="Dirección" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_barrio" HeaderText="Barrio" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="domicilioDepartamento" HeaderText="Departamento" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="domicilioMunicipio" HeaderText="Municipio" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_celular" HeaderText="Celular" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_telefonoFijo" HeaderText="Teléfono" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_estadoCivil" HeaderText="Estado Civil" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_contactoPersona" HeaderText="Contacto" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="afi_contactoTelefono" HeaderText="Contacto Telefono" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>

                    <asp:BoundField DataField="afi_cuentaNumero" HeaderText="No Cuenta" DataFormatString="' {0:0}" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="ref_nombre" HeaderText="Banco" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="car_codigo" HeaderText="Codigo Ministerio" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="car_nombre" HeaderText="Cargo Ministerio" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="con_cargoHomologado" HeaderText="Cargo" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="_nivelRiesgo" HeaderText="Nivel Riesgo" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="ID_contrato" HeaderText="ID_contrato" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="con_fechaInicio" HeaderText="Fecha de Ingreso" DataFormatString="{0:yyyy-MM-dd}" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="con_fechaFin" HeaderText="Fecha de Retiro" DataFormatString="{0:yyyy-MM-dd}" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="con_fechaTentativaRetiro" HeaderText="Fecha Tentativa de Retiro" DataFormatString="{0:yyyy-MM-dd}" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="con_Tipo" HeaderText="Tipo" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField DataField="con_jornada" HeaderText="Jornada" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <%# estadoContrato((int)Eval("con_estado")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="con_salario" HeaderText="Sal. Básico" DataFormatString="{0:N0}" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
                    <asp:TemplateField HeaderText="EPS">
                        <ItemTemplate>
                            <%# Negocio.NUtilidades.consultaAdminSaludNombre(Eval("afi_EPS").ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AFP">
                        <ItemTemplate>
                            <%# Negocio.NUtilidades.consultaAdminSaludNombre(Eval("afi_AFP").ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CCF">
                        <ItemTemplate>
                            <%# Negocio.NUtilidades.consultaAdminSaludNombre(Eval("afi_CCF").ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="afi_fechaNacimiento" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Fecha Nacimiento"></asp:BoundField>
                    <asp:TemplateField HeaderText="Edad">
                        <ItemTemplate>
                            <%# Negocio.NUtilidades.calcularTiempo((DateTime)Eval("afi_fechaNacimiento"),"HOY","años") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="afi_sexo" HeaderText="Sexo" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="_nivelEducativo" HeaderText="Nivel Educativo" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>

                </Columns>
                <HeaderStyle CssClass="gdvHeaderstyle" />
                <RowStyle CssClass="gdvRowstyle" Font-Size="12px" />
                <AlternatingRowStyle CssClass="gdvAltrowstyle"  Font-Size="12px" />
            </asp:GridView>

</asp:Content>

