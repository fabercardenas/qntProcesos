using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Negocio
{
    public class NPagos
    {
        private readonly Datos.DPagos dPagos = new Datos.DPagos();

        #region   INSERTAR CARGUE 
        public DataTable InsertarCargue(string car_codigoCargue, string ID_usuarioRegistroFK, string car_archivoCargue,
             string car_numeroDocumento, string car_valorPago, string car_fechaPago, string car_horaPago, string car_identificadorUnico, string car_canal, string car_IDacuerdo)
        {
            return dPagos.InsertarCargue(car_codigoCargue, ID_usuarioRegistroFK, car_archivoCargue, car_numeroDocumento, car_valorPago, car_fechaPago, car_horaPago, car_identificadorUnico, car_canal, car_IDacuerdo);
        }

        #endregion
    }
}
