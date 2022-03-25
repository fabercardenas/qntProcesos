<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Viewer.aspx.cs" Inherits="Viewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>COMPROBANTE NOMINA</title>
	
</head>
<body>
	<form id="form1" runat="server">
		<div id="dvHtml" runat="server" style="margin: -10px 0 0 0;width:750px;font-family:Arial;font-size:10px; ">
			<table style="width:100%">
				<tr>
					<td colspan="4">
						<table style="width:100%;">
							<tr>
                                <td>
									COMPROBANTE DE PAGO
								</td>
								<td align="right" style="text-align:right;">
									<asp:Image ID="imgLogoTemporal" runat="server" ImageUrl="~/Imagenes/logo.jpg" Height="50px" ImageAlign="Right" />
								</td>
								
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2">Empleado : <asp:Literal ID="ltrEmpleadoNombre" runat="server"></asp:Literal></td>
					<td >Documento</td>
					<td ><asp:Literal ID="ltrEmpDocumento" runat="server"></asp:Literal></td>
				</tr>
                <tr>
                    <td colspan="2">
                        Cargo : <asp:Literal ID="ltrEmpCargo" runat="server"></asp:Literal>
                    </td>
                    <td>Salario</td>
                    <td><asp:Literal ID="ltrSalario" runat="server"></asp:Literal></td>
                </tr>
				<tr>
					<td colspan="2">Periodo : <asp:Literal ID="ltrPeriodoPagado" runat="server"></asp:Literal></td>
					<td >Días</td>
					<td ><asp:Literal ID="ltrDiasPagados" runat="server"></asp:Literal></td>
				</tr>
				<tr>
					<td colspan="2" class="tdTitulo02" style="text-align:center;">
						DEVENGADO</td>
					<td colspan="2" class="tdTitulo02" style="text-align:center;">
						DEDUCCIONES</td>
				</tr>
				<tr>
					<td colspan="4"></td>
				</tr>
				<tr style="font-size:7px;">
					<td colspan="2" valign="top" style="width:50%;">
						<table style="width:100%;" border="1" cellspacing="0" cellpadding="4">
                            <tr>
                                <td>CONCEPTO</td>
                                <td>UNIDADES</td>
                                <td>CANTIDAD</td>
                                <td style="text-align:right;">VALOR</td>
                            </tr>
							<tr id="trSalarioBasico" runat="server">
								<td >SALARIO BASICO</td>
								<td >DÍAS</td>
								<td style="text-align:right;">
                                    <asp:Literal ID="ltrSalarioDias" runat="server"></asp:Literal>
								</td>
								<td style="text-align:right;">
									<asp:Literal ID="ltrSalarioBasico" runat="server"></asp:Literal>
								</td>
							</tr>
                            <asp:Literal ID="ltrDescansoProporcional" runat="server"></asp:Literal>
                            
							<tr>
								<td >AUX. TRANSPORTE</td>
                                <td >DÍAS</td>
								<td style="text-align:right;">
                                    <asp:Literal ID="ltrAuxilioDias" runat="server"></asp:Literal>
								</td>
								<td style="text-align:right;">
									<asp:Literal ID="ltrAuxilioTransporte" runat="server"></asp:Literal>
								</td>
							</tr>
                            <asp:Literal ID="ltrAuxilioNoSalarial" runat="server"></asp:Literal>
                            <asp:Literal ID="ltrIncapacidad" runat="server"></asp:Literal>
                            <asp:ListView ID="lsvHorasExtra" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("ref_nombre").ToString().ToUpper() %></td>
                                        <td><%# Eval("nov_unidades").ToString().ToUpper() %></td>
                                        <td style="text-align:right;"><%# validaValor(Eval("ref_valor").ToString(), Convert.ToDouble(Eval("nov_valor"))) %></td>
                                        <td style="text-align:right;"><%# string.Format("{0:N0}", Eval("nov_valorPesos")) %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <asp:ListView ID="lsvLicencias" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("ref_nombre").ToString().ToUpper() %></td>
                                        <td><%# Eval("nov_unidades").ToString().ToUpper() %></td>
                                        <td style="text-align:right;"><%# validaValor(Eval("ref_valor").ToString(), Convert.ToDouble(Eval("nov_valor"))) %></td>
                                        <td style="text-align:right;"></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <tr>
								<td colspan="3">TOTAL DEVENGADO</td>
                                <td style="text-align:right;">
						            <asp:Literal ID="ltrTotalDevengado" runat="server"></asp:Literal>
					            </td>
							</tr>
						</table>
					</td>
					<td colspan="2" valign="top">
						<table style="width:100%;" border="1" cellspacing="0" cellpadding="4">
                            <tr>
                                <td>CONCEPTO</td>
                                <td>UNIDADES</td>
                                <td>CANTIDAD</td>
                                <td style="text-align:right;">VALOR</td>
                            </tr>
							<tr>
								<td>EPS <asp:Literal ID="ltrEPS" runat="server"></asp:Literal></td>
                                <td style="text-align:right;">
                                   DÍAS
                                </td>
                                <td style="text-align:right;">
                                    <asp:Literal ID="ltrDescEPSdias" runat="server"></asp:Literal>
                                </td>
								<td style="text-align:right;">
									<asp:Literal ID="ltrDescEPS" runat="server"></asp:Literal>
								</td>
							</tr>
							<tr>
								<td>PENSION <asp:Literal ID="ltrAFP" runat="server"></asp:Literal></td>
                                <td style="text-align:right;">
                                    DÍAS
                                </td>
                                <td style="text-align:right;">
                                    <asp:Literal ID="ltrDescAFPdias" runat="server"></asp:Literal>
                                </td>
								<td style="text-align:right;">
									<asp:Literal ID="ltrDescAFP" runat="server"></asp:Literal>
								</td>
							</tr>
							<asp:Literal ID="ltrDescFondoSolidaridad" runat="server"></asp:Literal>
							<asp:Literal ID="ltrDescReteFuente" runat="server"></asp:Literal>
                            <asp:ListView ID="lsvDeducciones" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("ref_nombre").ToString().ToUpper() %></td>
                                        <td style="text-align:right;">
                                            PESOS
                                        </td>
                                        <td style="text-align:right;">
                                    
                                        </td>
                                        <td style="text-align:right;"><%# string.Format("{0:N0}", Eval("nov_valorPesos")) %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <tr>
                                <td colspan="3">TOTAL DEDUCIDO</td>
                                <td style="text-align:right;">
						            <asp:Literal ID="ltrTotalDeducido" runat="server"></asp:Literal>
					            </td>
                            </tr>
						</table>
					</td>
				</tr>
                
                <tr><td colspan="4"><br /></td></tr>
				<tr>
					<td colspan="4">
                        <table style="width:100%;" border="1" cellspacing="0" cellpadding="4">
							<tr>
                                <td style="width:50%;">
                                    TOTAL A PAGAR
                                </td>
                                <td style="text-align:right;"><asp:Literal ID="ltrTotalPagado" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </td>
				</tr>
                <tr><td colspan="4"><br /></td></tr>
				<tr >
					<td colspan="2" style='font-family:Arial;font-size: 9px;'>Recibí Conforme:<br /><br /><br /><br /><br /></td>
					<td colspan="2" style='font-family:Arial;font-size: 9px;'>Entregado Por:<br /><br /><br /><br /><br /></td>
				</tr>
			</table>
		</div>

        <div class="tablaBorde" id="dvHtmlPrimas" runat="server" style="margin: -10px 0 0 0;width:750px;font-family:Arial;font-size: 10px; ">
			<table style="width:100%">
				<tr>
					<td colspan="4">
						<table style="width:100%;">
							<tr>
                                <td align="left" style="text-align:left;">
									<asp:Image ID="imgLogoTemporalPrima" runat="server" ImageUrl="~/Imagenes/logoAlianzaTemporalRH.jpg" Height="20px" ImageAlign="Left" />
								</td>
                                <td>
									<strong>COMPROBANTE DE PAGO DE PRIMA DE SERVICIOS</strong>
								</td>
							</tr>
						</table>
					</td>
				</tr>
                <tr>
                    <td colspan="4">
                        NOMBRE DE LA EMPRESA : <asp:Literal ID="ltrNombreTemporalPrima" runat="server"></asp:Literal>
                    </td>
                </tr>
				<tr>
					<td colspan="2">Empleado : <asp:Literal ID="ltrEmpleadoNombrePrima" runat="server"></asp:Literal></td>
					<td >Documento</td>
					<td ><asp:Literal ID="ltrEmpDocumentoPrima" runat="server"></asp:Literal></td>
				</tr>
				<tr>
					<td colspan="2">Periodo : <asp:Literal ID="ltrPeriodoPagadoPrima" runat="server"></asp:Literal></td>
					<td>Días</td>
					<td><asp:Literal ID="ltrDiasPagadosPrima" runat="server"></asp:Literal></td>
				</tr>
				<tr>
					<td colspan="4">
                        <table style="width:100%;" border="1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td>
                                    Promedio Sueldo<br />
                                    Promedio Auxilio de Transporte
                                    <asp:Literal ID="ltrSalarioProyectadoTexto" runat="server"></asp:Literal>
                                </td>
                                <td style="text-align:right; min-width:50px;">
                                    <asp:Literal ID="ltrPromedioSueldo" runat="server"></asp:Literal><br />
                                    <asp:Literal ID="ltrPromedioAuxTransp" runat="server"></asp:Literal>
                                    <asp:Literal ID="ltrSalarioProyectadoValor" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    Total Prima de servicios
                                </td>
                                <td style="text-align:right;">
                                    <asp:Literal ID="ltrTotalPrima" runat="server"></asp:Literal>
                                </td>
                            </tr>
							<tr>
                                <td>
                                    Deducciones
                                </td>
                                <td style="text-align:right;">
                                    <asp:Literal ID="ltrDeduccionesPrima" runat="server"></asp:Literal>    
                                </td>
                                <td>
                                    TOTAL A PAGAR
                                </td>
                                <td style="text-align:right;">
                                    <asp:Literal ID="ltrTotalPagarPrima" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td>
				</tr>
                <tr><td colspan="4"><br /></td></tr>
				<tr >
					<td colspan="2" style='font-family:Arial;font-size: 10px;'>Recibí Conforme:<br /><br /><br /><br /><br /></td>
					<td colspan="2" style='font-family:Arial;font-size: 10px;'>Entregado Por:<br /><br /><br /><br /><br /></td>
				</tr>
			</table>
		</div>

	</form>
</body>
</html>
