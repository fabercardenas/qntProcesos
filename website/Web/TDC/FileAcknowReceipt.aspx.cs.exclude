using System;
using System.Data;

public partial class Web_TDC_FileSMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Negocio.NTDC nTDC = new Negocio.NTDC();
        DataTable tbDatos = nTDC.ConsultarDocumental();
        if (tbDatos.Rows.Count > 0)
        {
            String separador = "|";
            Response.Write("CEDULA" + separador + "NOMBRE" + separador + "MENSAJE" + separador + "CELULAR" + separador + "PENDIENTES");
            Response.Write(System.Environment.NewLine);
            for (int i = 0; i < tbDatos.Rows.Count; i++)
            {
                Response.Write(tbDatos.Rows[i]["tdc_numeroDocumento"]);
                Response.Write(separador);
                Response.Write(tbDatos.Rows[i]["Nombre"]);
                Response.Write(separador);
                Response.Write(tbDatos.Rows[i]["ref_nombre"]);
                Response.Write(separador);
                Response.Write(tbDatos.Rows[i]["tdc_celular"]);
                Response.Write(separador);
                Response.Write(tbDatos.Rows[i]["ref_descripcion"]);
                Response.Write(System.Environment.NewLine);
            }
        }
        
    }
}