using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descripción breve de LogExtracts
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class LogExtracts : System.Web.Services.WebService {

    public LogExtracts () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void Insertar(string ID_cliente, string ext_nombre, string ext_mail,
                           string ext_pdf, string ext_data, string ext_estadoEnvio,
                           string ext_machineName, string ext_manchineUser) 
    {
        Datos.DBpmSrv bpm = new Datos.DBpmSrv();
        bpm.LogInsertar(ID_cliente, ext_nombre, ext_mail, ext_pdf, ext_data, ext_estadoEnvio, ext_machineName, ext_manchineUser);
    }
    
}
