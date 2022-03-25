using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Globalization;

public partial class Viewer : System.Web.UI.Page
{
	Negocio.NNomina nNomina = new Negocio.NNomina();
    string tipoNomina = "";
    protected void Page_Load(object sender, EventArgs e)
	{
        //me piden un comprobante solamente
        if (Request.QueryString["idnB"] != null)
        {
            cargarDatos();
        }
        else
        {
            //me piden todos los comprobantes de una nómina
            if (Request.QueryString["idnom"] != null)
            {
                DataTable tbNomina = nNomina.ConsultarXID(Convert.ToDouble(Request.QueryString["idnom"].ToString()));
                if (tbNomina.Rows.Count > 0)
                {
                    System.IO.StringWriter pContenidoPaginaDestino = new StringWriter();
                    dvHtml.Visible = false;
                    dvHtmlPrimas.Visible = false;

                    for (int j = 0; j < tbNomina.Rows.Count; j++)
                    {
                        Server.Execute("Viewer.aspx?idnB=" + tbNomina.Rows[j]["ID_nominaBase"].ToString(), pContenidoPaginaDestino, true);
                        if (j % 2 != 0)
                            pContenidoPaginaDestino.WriteLine("<h1 style='page-break-after:always;'></h1>");
                        else
                            pContenidoPaginaDestino.WriteLine("<br /><br /><br /><br /><br /><br /><br /><br /><br />");

                    }
                    Response.Write(pContenidoPaginaDestino);
                }
            }
        }
	}

	public void cargarDatos()
	{
		if (Request.QueryString["idnB"] != null)
		{
			DataTable tbEmpleado = nNomina.ConsultarXidNomBase(Request.QueryString["idnB"].ToString());

			if (tbEmpleado.Rows.Count > 0)
			{
                //si me piden que lo envie por mai, pero no tiene, no hago nada, dejo así.
                if ((Request.QueryString["operation"] != null) && (Request.QueryString["operation"].ToString() == "send") && ((tbEmpleado.Rows[0]["afi_mail"].ToString() == "") || (tbEmpleado.Rows[0]["afi_mail"].ToString().Length <=6)))
                    return;

                string idNomina = tbEmpleado.Rows[0]["ID_nominaFK"].ToString();
                tipoNomina = tbEmpleado.Rows[0]["nom_tipoNomina"].ToString();

                #region SALARIOS
                if (tbEmpleado.Rows[0]["nom_tipoNomina"].ToString() == "Salarios")
                {
                    dvHtmlPrimas.Visible = false;
                    imgLogoTemporal.ImageUrl = ConfigurationManager.AppSettings["RutaLogo"] + tbEmpleado.Rows[0]["emp_logoSmall"].ToString();
                    ltrEmpleadoNombre.Text = tbEmpleado.Rows[0]["nombreCuenta"].ToString().ToUpper();
                    ltrEmpDocumento.Text = tbEmpleado.Rows[0]["afi_documento"].ToString();
                    ltrPeriodoPagado.Text = string.Format("{0:yyyy-MM-dd}", tbEmpleado.Rows[0]["nom_fechaInicio"]);
                    ltrPeriodoPagado.Text += "  A  " + string.Format("{0:yyyy-MM-dd}", tbEmpleado.Rows[0]["nom_fechaFin"]);
                    ltrDiasPagados.Text = tbEmpleado.Rows[0]["nom_dias"].ToString();

                    if (tbEmpleado.Rows[0]["con_jornada"].ToString() == "Medio Tiempo")
                        ltrSalario.Text = string.Format("{0:N0}", (double)tbEmpleado.Rows[0]["nom_salarioContrato"] / 2);
                    else
                        ltrSalario.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_salarioContrato"]);


                    ltrSalarioDias.Text = tbEmpleado.Rows[0]["nom_dias"].ToString();
                    ltrSalarioBasico.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_salarioBase"]);
                    ltrAuxilioTransporte.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_dvAuxilioTransporte"]);
                    ltrAuxilioDias.Text = ((int)tbEmpleado.Rows[0]["nom_dias"] - (int)tbEmpleado.Rows[0]["diasLicenciaRemunerada"]).ToString();
                    ltrEmpCargo.Text = tbEmpleado.Rows[0]["nombreCargo"].ToString().ToUpper();

                    if (tbEmpleado.Rows[0]["con_jornada"].ToString() == "Tiempo Parcial")
                        trSalarioBasico.Visible = false;
                    //if (Convert.ToDouble(tbEmpleado.Rows[0]["nom_dvOtrosNoPrestacional"]) > 0)
                    //    ltrAuxilioNoSalarial.Text = "<tr><td>AUX. NO SALARIALES</td><td style='text-align:right;'>" + string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_dvOtrosNoPrestacional"]) + "</td></tr>";
                    if (tbEmpleado.Rows[0]["con_tipo"].ToString() == "SABATINO")
                        ltrDescansoProporcional.Text = "<tr><td>DESCANSO PROPORCIONAL</td><td></td><td style='text-align:right;'>" + string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_dvOtros"]) + "</td></tr>";

                    if (tbEmpleado.Rows[0]["con_pfp"].ToString() == "Si")
                        ltrAuxilioNoSalarial.Text = "<tr><td>PRESTAMO FONDO PRESTACIONAL</td><td></td><td></td><td style='text-align:right;'>" + string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_ProvTotal"]) + "</td></tr>";

                    if (Convert.ToDouble(tbEmpleado.Rows[0]["nom_dvIncapacidad"]) > 0)
                        ltrAuxilioNoSalarial.Text += "<tr><td>INCAPACIDAD</td><td>DÍAS</td><td style='text-align:right;'>" + tbEmpleado.Rows[0]["nom_diasIncapacidad"] + "</td><td style='text-align:right;'>" + string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_dvIncapacidad"]) + "</td></tr>";

                    ltrTotalDevengado.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["totalDevengado"]);
                    ltrTotalDeducido.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["totalDeducido"]);

                    ltrDescEPS.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_ddEPSafiliado"]);
                    ltrDescAFP.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_ddAFPafiliado"]);
                    

                    if ((double)tbEmpleado.Rows[0]["nom_ddfspafiliado"] != 0)
                    {
                        ltrDescFondoSolidaridad.Text = "<tr><td>FONDO SOLIDARIDAD PENSIONAL</td>" +
                                    "<td style='text-align:right;'>DÍAS</td><td style='text-align:right;'>" + ltrSalarioDias.Text + "</td>" +
                                    "<td style='text-align:right;'>" + string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_ddFSPafiliado"]) + "</td></tr>";
                    }

                    if ((double)tbEmpleado.Rows[0]["nom_ddReteFuente"] != 0)
                    {
                        ltrDescReteFuente.Text = "<tr><td>RETEFUENTE</td>" +
                                    "<td style='text-align:right;'>DÍAS</td><td style='text-align:right;'>" + ltrSalarioDias.Text + "</td>" +
                                    "<td style='text-align:right;'>" + string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_ddReteFuente"]) + "</td>" +
                                    "</tr>";
                    }

                    ltrEPS.Text = tbEmpleado.Rows[0]["nomsoiEPS"].ToString();
                    ltrAFP.Text = tbEmpleado.Rows[0]["nomsoiAFP"].ToString();

                    ltrTotalPagado.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_TotalPagar"]);

                    DataTable tbHorasExtra = nNomina.novConsultaLaboralesXNominaBaseXOrden(Request.QueryString["idnB"].ToString(), "'1','2','6'");
                    lsvHorasExtra.DataSource = tbHorasExtra;
                    lsvHorasExtra.DataBind();

                    DataTable tbLicencias = nNomina.novConsultaLaboralesXNominaBaseXOrden(Request.QueryString["idnB"].ToString(), "'5'");
                    lsvLicencias.DataSource = tbLicencias;
                    lsvLicencias.DataBind();
                    object HABER = tbLicencias.Compute("sum(nov_valor)", "ref_valor IN ('LicenciaMaternidad')");

                    int diasOtroEPS = (HABER == System.DBNull.Value) ? 0: Convert.ToInt16(HABER);
                    HABER = tbLicencias.Compute("sum(nov_valor)", "ref_valor IN ('LicenciaMaternidad','LicenciaNoRemunerada')");
                    int diasOtroAFP = (HABER == System.DBNull.Value) ? 0 : Convert.ToInt16(HABER);

                    if (tbEmpleado.Rows[0]["con_jornada"].ToString() != "Tiempo Parcial")
                    {
                        ltrDescEPSdias.Text = ((int)tbEmpleado.Rows[0]["nom_dias"] + (int)tbEmpleado.Rows[0]["nom_diasIncapacidad"] + diasOtroEPS).ToString();
                        ltrDescAFPdias.Text = ((int)tbEmpleado.Rows[0]["nom_dias"] + (int)tbEmpleado.Rows[0]["nom_diasIncapacidad"] + diasOtroAFP).ToString();
                    }
                    else
                    {
                        if (Convert.ToDateTime(tbEmpleado.Rows[0]["nom_fechaFin"]).Day <= 28)
                        {
                            ltrDescEPSdias.Text = "15";
                            ltrDescAFPdias.Text = "15";
                        }
                        else
                        {
                            ltrDescEPSdias.Text = "30";
                            ltrDescAFPdias.Text = "30";
                        }
                    }
                    


                    //BUSCAR NOVEDADES DE DESCUENTOS
                    lsvDeducciones.DataSource = nNomina.novConsultaLaboralesXNominaBaseXOrden(Request.QueryString["idnB"].ToString(), "'3'");
                    lsvDeducciones.DataBind();
                }
                #endregion
                
                #region PRIMAS
                else
                {
                    dvHtml.Visible = false;
                    imgLogoTemporalPrima.ImageUrl = ConfigurationManager.AppSettings["RutaLogo"] + tbEmpleado.Rows[0]["emp_logoSmall"].ToString();
                    ltrNombreTemporalPrima.Text = tbEmpleado.Rows[0]["emp_nombre"].ToString();
                    ltrEmpleadoNombrePrima.Text = tbEmpleado.Rows[0]["nombreCuenta"].ToString().ToUpper();
                    ltrEmpDocumentoPrima.Text = tbEmpleado.Rows[0]["afi_documento"].ToString();
                    ltrPeriodoPagadoPrima.Text = string.Format("{0:yyyy-MM-dd}", tbEmpleado.Rows[0]["nom_fechaInicio"]);
                    ltrPeriodoPagadoPrima.Text += "  A  " + string.Format("{0:yyyy-MM-dd}", tbEmpleado.Rows[0]["nom_fechaFin"]);
                    ltrDiasPagadosPrima.Text = tbEmpleado.Rows[0]["nom_dias"].ToString();
                    ltrDeduccionesPrima.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_ddOtros"]);

                    //ltrEmpCargoPrima.Text = tbEmpleado.Rows[0]["nombreCargo"].ToString().ToUpper();
                    //ltrSalarioPrima.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_salarioContrato"]);

                    ltrPromedioSueldo.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_ddEPSafiliado"]);
                    ltrPromedioAuxTransp.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_ddAFPafiliado"]);

                    ltrTotalDevengado.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["totalDevengado"]);
                    ltrTotalDeducido.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["totalDeducido"]);
                    ltrTotalPrima.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_dvSalario"]); 
                    ltrTotalPagarPrima.Text = string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_TotalPagar"]);
                    if(Convert.ToDouble(tbEmpleado.Rows[0]["nom_dvOtros"]) > 0)
                    {
                        ltrSalarioProyectadoTexto.Text = "<br />Salario Proyectado";
                        ltrSalarioProyectadoValor.Text = "<br />" + string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_dvOtros"]);
                    }
                    ltrSalarioProyectadoTexto.Text = "<br />Promedio HE";
                    ltrSalarioProyectadoValor.Text = "<br />" + string.Format("{0:N0}", tbEmpleado.Rows[0]["nom_ddFSPafiliado"]);
                    
                }
                #endregion

                #region GESTIONAR PDF
                if (Request.QueryString["cre"] != null)
				{
					string pdfFileName;
                    
                    if ((Request.QueryString["operation"] != null))
                    {
                        pdfFileName = Request.PhysicalApplicationPath + "\\files\\" + idNomina + "\\Comp" + idNomina + "_" + tbEmpleado.Rows[0]["afi_documento"].ToString() + ".pdf";

                        if (System.IO.File.Exists(pdfFileName) == false)
                        {
                            switch (tbEmpleado.Rows[0]["nom_tipoNomina"].ToString())
                            {
                                case "Salarios":
                                    CrearPDF(pdfFileName, dvHtml);
                                    break;
                                case "Primas":
                                    CrearPDF(pdfFileName, dvHtmlPrimas);
                                    break;
                                default:
                                    break;
                            }
                        }

                        if ((Request.QueryString["operation"] != null) && (Request.QueryString["operation"].ToString() == "download"))
                            EntregarAlCliente(pdfFileName, "Cert" + tbEmpleado.Rows[0]["afi_documento"].ToString() + ".pdf");

                        if ((Request.QueryString["operation"] != null) && (Request.QueryString["operation"].ToString() == "send") && (tbEmpleado.Rows[0]["afi_mail"].ToString() != ""))
                        {
                            Negocio.NCorreo nCorreo = new Negocio.NCorreo();
                            string body = "<table><tbody>";

                            body += "<tr><td><img src='" + ConfigurationManager.AppSettings["RutaLogo"] + tbEmpleado.Rows[0]["emp_logoSmall"].ToString() + "' alt='' ></td><td style='width:640px;height:40px;background-color:#ECECEC;padding-top:16px;padding-left:14px;padding-bottom:16px;text-align:center;'>Comprobante de Pago</td></tr>" +
                                    "<tr><td colspan='2'>Buen día " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tbEmpleado.Rows[0]["afi_nombre1"].ToString().ToLower()) +".<br /><br />Anexo remitimos el comprobante de pago correspondiente. <br /><br /></td></tr>";
                            body += "</tbody></table>";

                            //nCorreo.Envio_Correo("fabercardenas@hotmail.com", "", body, "Comprobante", "", pdfFileName, "Comprobante.pdf");
                            if ((Session["ID_usuario"] != null) && (Session["ID_usuario"].ToString() == "1"))
                                nCorreo.Envio_Correo("fabercardenas@gmail.com", "", body, "Comprobante", "", pdfFileName, "destinatarios.txt");
                            else
                                nCorreo.Envio_Correo(tbEmpleado.Rows[0]["afi_mail"].ToString(), "", body, "Comprobante", "", pdfFileName, "Comprobante.pdf");
                        }

                    }
				}
				#endregion
			}
		}
	}

    public string validaValor(string tipoNovedad, double nov_valor)
    {
        if (tipoNovedad == "InteresesCesantias")
            return "";
        else
            return string.Format("{0:N0}", nov_valor);
    }

	public void CrearPDF(string FileName, System.Web.UI.HtmlControls.HtmlGenericControl divHTML_PARAM)
	{
        string strHtml = string.Empty;
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

        StringWriter sw = new StringWriter();
		HtmlTextWriter hw = new HtmlTextWriter(sw);
        divHTML_PARAM.RenderControl(hw);
		StringReader sr = new StringReader(sw.ToString());
		strHtml = sr.ReadToEnd();
		sr.Close();

		try
		{
			object TargetFile = FileName;
			string ModifiedFileName = string.Empty;
			string FinalFileName = string.Empty;
			iTextSharp.text.FontFactory.Register(@"c:\windows\fonts\Arial.ttf", "Arial");
            FontFactory.GetFont("Arial","6", Font.NORMAL);

			HTMLtoPDF.GeneratePDF.HtmlToPdfBuilder builder = new HTMLtoPDF.GeneratePDF.HtmlToPdfBuilder(iTextSharp.text.PageSize.LETTER);
			HTMLtoPDF.GeneratePDF.HtmlPdfPage first = builder.AddPage();
			first.AppendHtml(strHtml);
			byte[] file = builder.RenderPdf();
			File.WriteAllBytes(TargetFile.ToString(), file);

			iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(TargetFile.ToString());
			
			ModifiedFileName = TargetFile.ToString();
			ModifiedFileName = ModifiedFileName.Insert(ModifiedFileName.Length - 4, "1");

			iTextSharp.text.pdf.PdfEncryptor.Encrypt(reader, new FileStream(ModifiedFileName, FileMode.Append), iTextSharp.text.pdf.PdfWriter.STRENGTH128BITS, "", "", iTextSharp.text.pdf.PdfWriter.AllowPrinting);
			reader.Close();
			if (File.Exists(TargetFile.ToString()))
				File.Delete(TargetFile.ToString());
			FinalFileName = ModifiedFileName.Remove(ModifiedFileName.Length - 5, 1);
			File.Copy(ModifiedFileName, FinalFileName);
			if (File.Exists(ModifiedFileName))
				File.Delete(ModifiedFileName);

		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

    public void CrearPDFImpresion(string FileName, StringWriter sw, System.Web.UI.HtmlControls.HtmlGenericControl divHTML_PARAM)
    {
        string strHtml = string.Empty;

        HtmlTextWriter hw = new HtmlTextWriter(sw);
        divHTML_PARAM.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        strHtml = sr.ReadToEnd();
        sr.Close();

        try
        {
            object TargetFile = FileName;
            string ModifiedFileName = string.Empty;
            string FinalFileName = string.Empty;
            iTextSharp.text.FontFactory.Register(@"c:\windows\fonts\Arial.ttf", "Arial");

            HTMLtoPDF.GeneratePDF.HtmlToPdfBuilder builder = new HTMLtoPDF.GeneratePDF.HtmlToPdfBuilder(iTextSharp.text.PageSize.LETTER);
            HTMLtoPDF.GeneratePDF.HtmlPdfPage first = builder.AddPage();
            first.AppendHtml(strHtml);
            byte[] file = builder.RenderPdf();
            File.WriteAllBytes(TargetFile.ToString(), file);

            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(TargetFile.ToString());

            ModifiedFileName = TargetFile.ToString();
            ModifiedFileName = ModifiedFileName.Insert(ModifiedFileName.Length - 4, "1");

            iTextSharp.text.pdf.PdfEncryptor.Encrypt(reader, new FileStream(ModifiedFileName, FileMode.Append), iTextSharp.text.pdf.PdfWriter.STRENGTH128BITS, "", "", iTextSharp.text.pdf.PdfWriter.AllowPrinting);
            reader.Close();
            if (File.Exists(TargetFile.ToString()))
                File.Delete(TargetFile.ToString());
            FinalFileName = ModifiedFileName.Remove(ModifiedFileName.Length - 5, 1);
            File.Copy(ModifiedFileName, FinalFileName);
            if (File.Exists(ModifiedFileName))
                File.Delete(ModifiedFileName);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void EntregarAlCliente(string pdfFileName, string nombreEntregar)
	{
		Response.ContentType = "application/x-download";
		Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", nombreEntregar));
		Response.WriteFile(pdfFileName);
		Response.Flush();
		Response.End();
	}

	public void sample()
	{
		//Create a byte array that will eventually hold our final PDF
		Byte[] bytes;

		//Boilerplate iTextSharp setup here
		//Create a stream that we can write to, in this case a MemoryStream
		using (var ms = new MemoryStream())
		{

			//Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
			using (var doc = new Document())
			{

				//Create a writer that's bound to our PDF abstraction and our stream
				using (var writer = PdfWriter.GetInstance(doc, ms))
				{

					//Open the document for writing
					doc.Open();

					//Our sample HTML and CSS
					var example_html = @"<div class=""headline"" style=""margin: 0 0 0 0;width:850px;font-family:Arial;font-size: 10px;"">" +
						"<table style='width:100%;'>" +
						"<tr><td style='width:50px; border:solid 1px;'>NOMBRE</td>" +
						"    <td style='width:100%;'>JUAN GUILLERMO CUADRADO REDONDO</td>" +
						"    <td style='width:50px;'>DOCUMENTO</td>" +
						"    <td style='width:100px;'>79889198798798</td></tr></table></div>"
						;
					var example_css = @".headline{font-size:10px; border:solid 1px;}";

					/**************************************************
					 * Example #1                                     *
					 *                                                *
					 * Use the built-in HTMLWorker to parse the HTML. *
					 * Only inline CSS is supported.                  *
					 * ************************************************/

					//Create a new HTMLWorker bound to our document
					using (var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc))
					{

						//HTMLWorker doesn't read a string directly but instead needs a TextReader (which StringReader subclasses)
						using (var sr = new StringReader(example_html))
						{

							//Parse the HTML
							htmlWorker.Parse(sr);
						}
					}

					/**************************************************
					 * Example #2                                     *
					 *                                                *
					 * Use the XMLWorker to parse the HTML.           *
					 * Only inline CSS and absolutely linked          *
					 * CSS is supported                               *
					 * ************************************************/

					//XMLWorker also reads from a TextReader and not directly from a string
					//using (var srHtml = new StringReader(example_html))
					//{

					//    //Parse the HTML
					//    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
					//}

					/**************************************************
					 * Example #3                                     *
					 *                                                *
					 * Use the XMLWorker to parse HTML and CSS        *
					 * ************************************************/

					//In order to read CSS as a string we need to switch to a different constructor
					//that takes Streams instead of TextReaders.
					//Below we convert the strings into UTF8 byte array and wrap those in MemoryStreams
					//using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_css)))
					//{
					//    using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_html)))
					//    {

					//        //Parse the HTML
					//        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
					//    }
					//}


					doc.Close();
				}
			}

			//After all of the PDF "stuff" above is done and closed but **before** we
			//close the MemoryStream, grab all of the active bytes from the stream
			bytes = ms.ToArray();
		}

		//Now we just need to do something with those bytes.
		//Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
		//You could also write the bytes to a database in a varbinary() column (but please don't) or you
		//could pass them to another function for further PDF processing.
		//var testFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\Muestras\\" + DateTime.Now.Millisecond.ToString() + "testFaber.pdf");
		var testFile = Request.PhysicalApplicationPath + "\\files\\" + DateTime.Now.Millisecond.ToString() + "GenerateHTMLTOPDTEST.pdf";
		System.IO.File.WriteAllBytes(testFile, bytes);
	}

	public void sample2()
	{
		string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		string filename = Request.PhysicalApplicationPath + "\\files\\" + DateTime.Now.Millisecond.ToString() + "SAMPLE2.pdf";
		Document document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
		try
		{
			PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create)); 
			PdfPTable table = new PdfPTable(4);
			table.TotalWidth = 750f;
			//fix the absolute width of the table
			table.LockedWidth = true;
			//relative col widths in proportions - 1/3 and 2/3
			float[] widths = new float[] { 2f, 4f, 6f , 8f};
			table.SetWidths(widths);
			table.HorizontalAlignment = 0;
			//leave a gap before and after the table
			table.SpacingBefore = 20f;
			table.SpacingAfter = 30f;

			PdfPCell cell = new PdfPCell(new Phrase("COMPROBANTE DE PAGO"));
			cell.Colspan = 4;
			cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
			table.AddCell(cell);//AGREGO ENCABEZADO

			table.AddCell("Col 1 Row 1");
			table.AddCell("Col 2 Row 1");
			table.AddCell("Col 3 Row 1");
			table.AddCell("Col 1 Row 2");
			table.AddCell("Col 2 Row 2");
			table.AddCell("Col 3 Row 2");
 
			document.Open();
			document.Add(table);
		}
		catch (Exception)
		{
			
			throw;
		}
		finally
		{
			document.Close();
			//ShowPdf(filename);
		}
	}

	public void ShowPdf(string filename)
	{
		//Clears all content output from Buffer Stream
		Response.ClearContent();
		//Clears all headers from Buffer Stream
		Response.ClearHeaders();
		//Adds an HTTP header to the output stream
		Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
		//Gets or Sets the HTTP MIME type of the output stream
		Response.ContentType = "application/pdf";
		//Writes the content of the specified file directory to an HTTP response output stream as a file block
		Response.WriteFile(filename);
		//sends all currently buffered output to the client
		Response.Flush();
		//Clears all content output from Buffer Stream
		Response.Clear();
	}  

}