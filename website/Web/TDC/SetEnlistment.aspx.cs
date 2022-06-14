using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using Ionic.Zip;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

public partial class TDC_SetEnlistment: System.Web.UI.Page
{
    Negocio.NTDC nTDC = new Negocio.NTDC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"]!=null) && (!Page.IsPostBack))
        {
            dvIdConsulta.Visible = true;
            ltrFechaPrevalidacion.Text = string.Format("{0:yyyyMMdd}", DateTime.Today);
            ConsultarSolicitudesXestado();
        }
    }

    protected void lnbConsultarTarjeta_Click(object sender, EventArgs e)
    {
        ConsultarSolicitudesXtarjeta();
    }

    protected void lnbConsultarFecha_Click(object sender, EventArgs e)
    {
        ltrFechaPrevalidacion.Text = string.Format("{0:yyyyMMdd}", txtFechaPreV.Text);
        DataTable PrevalidacionFecha = nTDC.consultaSolicitudXfechaPrevalidacion(ltrFechaPrevalidacion.Text);
        if (PrevalidacionFecha.Rows.Count>0)
        {
            ltrMensaje.Text = Messaging.Success("Prevalidaciones encontradas para la Fecha Seleccionada " + PrevalidacionFecha.Rows.Count);
            lnbTerminar.Visible = false;
            lnbStikers.Visible = true;
            lnbBloqueo.Visible = true;
            lnbAcuse.Visible = true;
            dvIdConsulta.Visible = false;
        }
        else
            ltrMensaje.Text = Messaging.Warning("No hay Prevalidaciones para la fecha seleccionada");
    }

    void ConsultarSolicitudesXestado()
    {
        gdvListaSolicitudes.DataSource = nTDC.ConsultarXestado(3);
        gdvListaSolicitudes.DataBind();
    }

    public string TieneDireccion(string tdc_direccion)
    {
        string retornar = "";
        if (tdc_direccion.Length > 0)
            retornar = "<span class='glyphicon glyphicon-ok'></span>";
        return retornar;
    }

    void ConsultarSolicitudesXtarjeta()
    {
        gdvListaSolicitudes.DataSource = nTDC.ConsultarXtarjeta(txtNumTarjeta.Text);
        gdvListaSolicitudes.DataBind();
    }

    protected void gdvListaSolicitudes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        gdvListaSolicitudes.SelectedIndex = e.NewSelectedIndex;
        string ltrCliDocumento = gdvListaSolicitudes.DataKeys[e.NewSelectedIndex].Value.ToString();
        nTDC.actualizaSolicitudXDocumento(gdvListaSolicitudes.DataKeys[e.NewSelectedIndex].Value.ToString(), Session["ID_usuario"].ToString());
        ltrMensaje.Text = Messaging.Success("Documento " + ltrCliDocumento + " prevalidado con éxito");
        ConsultarSolicitudesXestado();
    }

    protected void lnbTerminar_Click(object sender, EventArgs e)
    {
        ltrFechaPrevalidacion.Text = string.Format("{0:yyyyMMdd}", DateTime.Today);
        DataTable PrevalidacionProce = nTDC.consultaSolicitudXfechaPrevalidacion(ltrFechaPrevalidacion.Text);
        if (PrevalidacionProce.Rows.Count > 0)
        {
            ltrMensaje.Text = Messaging.Success("Prevalidaciones encontradas para la Fecha Seleccionada " + PrevalidacionProce.Rows.Count);
            lnbTerminar.Visible = false;
            lnbStikers.Visible = true;
            lnbBloqueo.Visible = true;
            lnbAcuse.Visible = true;
            dvIdConsulta.Visible = false;
        }
        else
            ltrMensaje.Text = Messaging.Warning("No hay Prevalidaciones ejecutadas el día de hoy");
    }

    #region GENERACIÓN STIKERS
    protected void lnbStikers_Click(object sender, EventArgs e)
    {
        GenerarArchivoStickers(nTDC.consultaSolicitudXfPreValiStikcer(ltrFechaPrevalidacion.Text));
    }

    private void GenerarArchivoStickers(DataTable tbl)
    {
        if (tbl.Rows.Count > 0)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                String nombreArchivo = "PlantillaStickers" + ltrFechaPrevalidacion.Text;
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(nombreArchivo);

                #region ENCABEZADO - DEFINICION DE ANCHOS DE COLUMNA
                
                ws.Column(1).Width = 3.15;
                ws.Column(2).Width = 12.58;
                ws.Column(3).Width = 29.42;
                ws.Column(4).Width = 8.58;
                
                ws.Column(5).Width = 6.28;
                ws.Column(6).Width = 12.58;
                ws.Column(7).Width = 29.42;
                ws.Column(8).Width = 8.58;


                ws.Column(2).Style.Font.Size = 10;
                ws.Column(3).Style.Font.Size = 10;
                ws.Column(6).Style.Font.Size = 10;
                ws.Column(7).Style.Font.Size = 10;
                ws.Column(8).Style.Font.Size = 10;

                ws.Column(2).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Column(3).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Column(6).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Column(7).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                #endregion

                int indicadorFila = 1;
                int totalFilasDibujadas = 0;
                string colTitulo, colContenidoC, colContenidoD;
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    //si es un numero par va a la izquierda
                    if ((i % 2) == 0)
                    {
                        colTitulo = "B";
                        colContenidoC = "C";
                        colContenidoD = "D";
                    }
                    else
                    {
                        colTitulo = "F";
                        colContenidoC = "G";
                        colContenidoD = "H";
                    }

                    ws.Cells[colTitulo + indicadorFila.ToString()].Value = "NOMBRE";
                    ws.Row(indicadorFila).Height = 27;
                    ExcelRange rngNombre = ws.Cells[colContenidoC + indicadorFila.ToString() + ":" + colContenidoD + indicadorFila.ToString()];
                    rngNombre.Merge=true;
                    rngNombre.Value = tbl.Rows[i]["NOMBRE"].ToString();

                    ws.Cells[colTitulo + (indicadorFila + 1).ToString()].Value = "CIUDAD";
                    ws.Row(indicadorFila+1).Height = 15;
                    ExcelRange rngCiudad = ws.Cells[colContenidoC + (indicadorFila+1).ToString() + ":" + colContenidoD + (indicadorFila + 1).ToString()];
                    rngCiudad.Merge = true;
                    rngCiudad.Value = tbl.Rows[i]["CIUDAD"].ToString();

                    ws.Cells[colTitulo + (indicadorFila + 2).ToString()].Value = "DIRECCIÓN";
                    ws.Row(indicadorFila + 2).Height = 40.5;
                    ExcelRange rngDireccion = ws.Cells[colContenidoC + (indicadorFila + 2).ToString() + ":" + colContenidoD + (indicadorFila + 2).ToString()];
                    rngDireccion.Merge = true;
                    rngDireccion.Value = tbl.Rows[i]["DIRECCION"].ToString();

                    ws.Cells[colTitulo + (indicadorFila + 3).ToString()].Value = "TELÉFONO";
                    ws.Row(indicadorFila + 3).Height = 15;
                    ws.Cells[colContenidoC + (indicadorFila + 3).ToString()].Value = tbl.Rows[i]["TELEFONO"].ToString();
                    ws.Cells[colContenidoD + (indicadorFila + 3).ToString()].Value = tbl.Rows[i]["TARJETA"].ToString();
                    
                    ws.Row(indicadorFila + 4).Height = 22.5;

                    if ((i % 2) != 0)
                        indicadorFila += 5;
                    totalFilasDibujadas++;
                }

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + nombreArchivo + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }
        }
        else
            Response.Write("No hay datos para mostrar con los filtros seleccionados");
    }
    
    #endregion

    #region GENERACIÓN ARCHIVO DE BLOQUEO
    protected void lnbBloqueo_Click(object sender, EventArgs e)
    {
        consultaSolicitudXfechaPrevalidacion(nTDC.consultaSolicitudXfechaPrevalidacion(ltrFechaPrevalidacion.Text));
    }

    protected void consultaSolicitudXfechaPrevalidacion(DataTable tbl)
    {
        if (tbl.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page page = new Page();
            HtmlForm form = new HtmlForm();

            GridView gdvDatos = new GridView();
            gdvDatos.DataSource = tbl;
            gdvDatos.DataBind();

            page.EnableEventValidation = false;
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(gdvDatos);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename= BloqueoTemporal" + ltrFechaPrevalidacion.Text + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
    }
    #endregion

    #region GENERACIÓN ACUSE DE RECIBIDO

    protected void lnbAcuse_Click(object sender, EventArgs e)
    {
        consultaSolicitudXAcuseRecibido(nTDC.consultaSolicitudXacuseRecibido(ltrFechaPrevalidacion.Text));
    }

    protected void consultaSolicitudXAcuseRecibido(DataTable tbl)
    {
        if (tbl.Rows.Count > 0)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                String nombreArchivo = "AcuseRecibido" + String.Format("{0:yyyyMMdd}", DateTime.Today);
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(nombreArchivo);
                #region ENCABEZADO Y LOGO
                #region LOGO
                System.Drawing.Image img = System.Drawing.Image.FromFile(@"" + Server.MapPath("~\\Imagenes\\logoQnt.png"));
                ExcelPicture pic = ws.Drawings.AddPicture("Picture_Name", img);
                pic.SetPosition(0, 20, 1, 5);
                pic.SetSize(28);

                ws.Column(1).Width = 3;
                ws.Column(2).Width = 30;
                ws.Column(3).Width = 16;
                ws.Column(4).Width = 24;

                ws.Row(1).Height = 61;
                ws.Row(1).Style.Font.Bold = true;
                ws.Row(1).Style.Font.Size = 14;

                ws.Cells["B1"].Value = "Acuse Recibido";
                ws.Cells["B1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["B1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;


                ws.Cells["C1"].Value = String.Format("{0:dd/MM/yyyy}", DateTime.Today);
                ws.Cells["C1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["C1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                #endregion

                #region ENCABEZADO COLOR AZUL
                ws.Cells["A2"].Value = "#";
                ws.Cells["B2"].Value = "NOMBRE";
                ws.Cells["C2"].Value = "DOCUMENTO";
                ws.Cells["D2"].Value = "TARJETA";

                Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#203764");
                ws.Cells["A2:D2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["A2:D2"].Style.Fill.BackgroundColor.SetColor(colFromHex);

                ws.Cells["A2:D2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["A2:D2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                ws.Row(2).Height = 23;
                ws.Row(2).Style.Font.Size = 8;
                ws.Row(2).Style.Font.Color.SetColor(Color.White);
                ws.Row(2).Style.Font.Bold = true;
                #endregion
                #endregion

                #region PINTAR LOS DATOS
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    ws.Cells["A" + (i + 3).ToString()].Value = (long)tbl.Rows[i]["Item"];
                    ws.Cells["B" + (i + 3).ToString()].Value = tbl.Rows[i]["NOMBRE"].ToString();
                    ws.Cells["C" + (i + 3).ToString()].Value = tbl.Rows[i]["DOCUMENTO"].ToString();
                    ws.Cells["D" + (i + 3).ToString()].Value = tbl.Rows[i]["TARJETA"].ToString();

                    ws.Cells["A" + (i + 3).ToString() + ":D" + (i + 3).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws.Cells["A" + (i + 3).ToString() + ":D" + (i + 3).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws.Cells["A" + (i + 3).ToString() + ":D" + (i + 3).ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    ws.Cells["A" + (i + 3).ToString() + ":D" + (i + 3).ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws.Row(i + 3).Style.Font.Size = 8;
                }

                ws.Cells["B" + (tbl.Rows.Count + 5).ToString()].Value = "Firma Qnt";
                ws.Cells["D" + (tbl.Rows.Count + 5).ToString()].Value = "Firma Mensajeria";

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + nombreArchivo + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
                #endregion
            }
        }
    }

    #endregion


}
