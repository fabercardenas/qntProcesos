using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

public partial class TDC_SetValidation : System.Web.UI.Page
{
    Negocio.NTDC nTDC = new Negocio.NTDC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["ID_usuario"]!=null) && (!Page.IsPostBack))
        {
            dvIdConsulta.Visible = true;
            ltrFechaValidacion.Text = string.Format("{0:yyyyMMdd}", DateTime.Today);
            ConsultarSolicitudesXestado();
        }
    }

    protected void lnbConsultarTarjeta_Click(object sender, EventArgs e)
    {
        ConsultarSolicitudesXtarjeta();
    }

    protected void lnbConsultarFecha_Click(object sender, EventArgs e)
    {
        ltrFechaValidacion.Text = string.Format("{0:yyyyMMdd}", txtFechaVal.Text);
        DataTable ValidacionFecha = nTDC.consultaSolicitudXfechaValidacion(ltrFechaValidacion.Text);
        if (ValidacionFecha.Rows.Count>0)
        {
            ltrMensaje.Text = Messaging.Success("Validaciones encontradas para la Fecha Seleccionada " + ValidacionFecha.Rows.Count);
            lnbEnvio.Visible = true;
            lnbTerminar.Visible = false;
            dvIdConsulta.Visible = false;
        }
        else
            ltrMensaje.Text = Messaging.Warning("No hay Validaciones para la fecha seleccionada");
    }

    void ConsultarSolicitudesXestado()
    {
        gdvListaSolicitudes.DataSource = nTDC.ConsultarXestadoVal(4);
        gdvListaSolicitudes.DataBind();
    }

    public string TieneDireccion(string tdc_direccion)
    {
        string retornar = "";
        if (tdc_direccion.Length > 0)
            retornar = "<span class='glyphicon glyphicon-map-marker' style='color:red;'></span>";
        return retornar;
    }

    public string TienePrevalidacion(string tdc_fechaPrevalidacion)
    {
        string retornar = "";
        if (tdc_fechaPrevalidacion.Length > 0)
            retornar = "<span class='glyphicon glyphicon-ok-sign' style='color:green;'></span>";
        return retornar;
    }

    void ConsultarSolicitudesXtarjeta()
    {
        gdvListaSolicitudes.DataSource = nTDC.ConsultarXtarjetaVal(txtNumTarjeta.Text);
        gdvListaSolicitudes.DataBind();
    }

    protected void gdvListaSolicitudes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        gdvListaSolicitudes.SelectedIndex = e.NewSelectedIndex;
        string ltrCliDocumento = gdvListaSolicitudes.DataKeys[e.NewSelectedIndex].Value.ToString();
        nTDC.actualizaSolicitudValXDocumento(gdvListaSolicitudes.DataKeys[e.NewSelectedIndex].Value.ToString(), Session["ID_usuario"].ToString());
        ltrMensaje.Text = Messaging.Success("Documento " + ltrCliDocumento + " Validado con éxito");
        ConsultarSolicitudesXestado();
    }

    protected void lnbTerminar_Click(object sender, EventArgs e)
    {
        ltrFechaValidacion.Text = string.Format("{0:yyyyMMdd}", DateTime.Today);
        DataTable ValidacionProce = nTDC.consultaSolicitudXfechaValidacion(ltrFechaValidacion.Text);
        if (ValidacionProce.Rows.Count > 0)
        {
            ltrMensaje.Text = Messaging.Success("Validaciones encontradas para la Fecha Seleccionada " + ValidacionProce.Rows.Count);
            lnbTerminar.Visible = false;
            lnbEnvio.Visible = true;
            dvIdConsulta.Visible = false;
        }
        else
            ltrMensaje.Text = Messaging.Warning("No hay Validaciones ejecutadas el día de hoy");
    }

    #region GENERACIÓN ARCHIVO DE ENVIO INTERRAPIDISIMO
    protected void lnbEnvio_Click(object sender, EventArgs e)
    {
        consultaSolicitudXfechaValidacion(nTDC.consultaSolicitudXfechaValidacion(ltrFechaValidacion.Text));
    }

    protected void consultaSolicitudXfechaValidacion(DataTable tbl)
    {
        if (tbl.Rows.Count > 0)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                String nombreArchivo = "MeLoPela";
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(nombreArchivo);
                #region ENCABEZADO Y LOGO
                #region LOGO
                System.Drawing.Image img = System.Drawing.Image.FromFile(@"" + Server.MapPath("~\\Imagenes\\logoQnt.png"));
                ExcelPicture pic = ws.Drawings.AddPicture("Picture_Name", img);
                pic.SetPosition(0, 20, 1, 5);
                pic.SetSize(28);

                ws.Column(1).Width = 3;
                ws.Column(2).Width = 32;
                ws.Column(3).Width = 16;
                ws.Column(4).Width = 58;
                ws.Column(5).Width = 16;
                ws.Column(6).Width = 32;
                ws.Column(7).Width = 18;
                ws.Column(8).Width = 15;

                ws.Row(1).Height = 61;
                ws.Row(1).Style.Font.Bold = true;
                ws.Row(1).Style.Font.Size = 14;

                ws.Cells["B1"].Value = "Envio #";
                ws.Cells["B1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["B1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                
                
                ws.Cells["C1"].Value = String.Format("{0:dd/MM/yyyy}", DateTime.Today);
                ws.Cells["C1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["C1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                #endregion

                #region ENCABEZADO COLOR AZUL
                ws.Cells["A2"].Value = "#";
                ws.Cells["B2"].Value = "NOMBRE";
                ws.Cells["C2"].Value = "CIUDAD DE RESIDENCIA";
                ws.Cells["D2"].Value = "DIRECCIÓN DE CORREPONDENCIA";
                ws.Cells["E2"].Value = "TELÉFONO";
                ws.Cells["F2"].Value = "CORREO ELECTRÓNICO";
                ws.Cells["G2"].Value = "No. GUIA";
                ws.Cells["H2"].Value = "PRECIO";

                Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#203764");
                ws.Cells["A2:H2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["A2:H2"].Style.Fill.BackgroundColor.SetColor(colFromHex);

                ws.Cells["A2:H2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["A2:H2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                ws.Row(2).Height = 23;
                ws.Row(2).Style.Font.Size = 8;
                ws.Row(2).Style.Font.Color.SetColor(Color.White);
                ws.Row(2).Style.Font.Bold = true;
                #endregion
                #endregion

                #region PINTAR LOS DATOS
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    ws.Cells["A" + (i +3).ToString()].Value = (long)tbl.Rows[i]["Item"];
                    ws.Cells["B" + (i +3).ToString()].Value = tbl.Rows[i]["NOMBRE"].ToString();
                    ws.Cells["C" + (i +3).ToString()].Value = tbl.Rows[i]["CIUDAD_DE_RESIDENCIA"].ToString();
                    ws.Cells["D" + (i +3).ToString()].Value = tbl.Rows[i]["DIRECCION_DE_CORRESPONDENCIA"].ToString();
                    ws.Cells["E" + (i +3).ToString()].Value = tbl.Rows[i]["TELEFONO"].ToString();
                    ws.Cells["F" + (i +3).ToString()].Value = tbl.Rows[i]["CORREO_ELECTRONICO"].ToString();

                    ws.Cells["A" + (i + 3).ToString() + ":H" + (i + 3).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws.Cells["A" + (i + 3).ToString() + ":H" + (i + 3).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws.Cells["A" + (i + 3).ToString() + ":H" + (i + 3).ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    ws.Cells["A" + (i + 3).ToString() + ":H" + (i + 3).ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws.Row(i + 3).Style.Font.Size = 8;
                }
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
