using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Negocio
{
    public class NNomina
    {
        Datos.DNomina dNomina = new Datos.DNomina();

        #region NOMINA

        public Int64 CalcularNomina(string ID_empresaTemporal, string ID_empresaUsuariaFK, string ID_centroCosto, string fechaInicio, string fechaFin, string ID_usuarioRegistraFK, Int64 idNominaActualizar = 0, string IndividualDocumento = "")
        {

            DataTable tbAfiliados;
            bool nominaIndividual = false;
            if (IndividualDocumento == "")
                tbAfiliados = dNomina.ConsultaAfiliadosXEmpresaSinNomina(ID_empresaTemporal, ID_empresaUsuariaFK, ID_centroCosto, fechaInicio, fechaFin);
            else
            {
                tbAfiliados = dNomina.ConsultaAfiliadosXEmpresaSinNominaIndividual(ID_empresaTemporal, ID_empresaUsuariaFK, ID_centroCosto, fechaInicio, fechaFin, IndividualDocumento);
                nominaIndividual = true;
            }

            if (tbAfiliados.Rows.Count > 0)
            {

                double SMLV = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(fechaFin).Year);
                double AuxTransp = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(fechaFin).Year, true);
                Int64 idNomina = idNominaActualizar == 0 ? NUtilidades.IdentificadorNomina(ID_empresaTemporal) : idNominaActualizar;
                string novedadesDescuento = dNomina.novFiltroDescuentos();
                string novedadesDevengo = dNomina.novFiltroDevengos();
                dNomina.Insertar(idNomina, ID_empresaTemporal, ID_empresaUsuariaFK, "Salarios", fechaInicio, fechaFin, "POR REVISAR", ID_usuarioRegistraFK, nominaIndividual);

                string idNomBase = "";
                //SI HAY AFILIADOS SIN NOMINA, CALCULO Y LUEGO INSERTO LA NOMINA
                for (int i = 0; i < tbAfiliados.Rows.Count; i++)
                {
                    idNomBase = calcularNominaBase(idNomina.ToString(), tbAfiliados.Rows[i], ID_empresaTemporal, ID_empresaUsuariaFK, ID_centroCosto, fechaInicio, fechaFin, SMLV, AuxTransp, ID_usuarioRegistraFK, novedadesDescuento, novedadesDevengo, "0", nominaIndividual);
                    bool reliquidar = false;
                    #region VALIDAR SI TIENE PRESTAMOS POR ASOCIAR A ESTA NÓMINA
                    NEmpleados nEmpleado = new NEmpleados();
                    DataTable tbPrestamos = nEmpleado.prestamoConsultaCuotasParaAplicar(tbAfiliados.Rows[i]["ID_afiliado"].ToString(), tbAfiliados.Rows[i]["ID_contrato"].ToString(), fechaInicio, 1);
                    if ((tbPrestamos != null) && (tbPrestamos.Rows.Count > 0))
                    {
                        for (int j = 0; j < tbPrestamos.Rows.Count; j++)
                        {
                            novInsertar(idNomBase, "Prestamo", "Pesos", Convert.ToDouble(tbPrestamos.Rows[j]["cuo_valor"]), tbPrestamos.Rows[j]["cuo_valor"].ToString(), 0, fechaInicio, fechaFin, "Cuota " + tbPrestamos.Rows[j]["cuo_numeroCuota"].ToString() + " de " + tbPrestamos.Rows[j]["pre_cuotas"].ToString(), ID_usuarioRegistraFK, false, false);
                            //asociar este idNomina a la cuota para descontarla como paga
                            nEmpleado.prestamoActualizaCuota(tbPrestamos.Rows[j]["ID_cuota"].ToString(), ID_usuarioRegistraFK, idNomBase);
                        }
                        reliquidar = true;
                    }
                    #endregion

                    #region VALIDAR SI TIENE NOVEDADES FIJAS

                    DataTable tbNovedadesFijas = nEmpleado.nofConsultarActivasXAfiliado(tbAfiliados.Rows[i]["ID_afiliado"].ToString(), tbAfiliados.Rows[i]["ID_contrato"].ToString(), fechaInicio);
                    if ((tbNovedadesFijas != null) && (tbNovedadesFijas.Rows.Count > 0))
                    {
                        for (int j = 0; j < tbNovedadesFijas.Rows.Count; j++)
                        {
                            novInsertar(idNomBase, tbNovedadesFijas.Rows[j]["ref_valor"].ToString(), tbNovedadesFijas.Rows[j]["ref_descripcion"].ToString(), Convert.ToDouble(tbNovedadesFijas.Rows[j]["nof_valor"]), tbNovedadesFijas.Rows[j]["nof_valor"].ToString(), 0, fechaInicio, fechaFin, "[Automatico]", ID_usuarioRegistraFK, false, false);
                        }
                        reliquidar = true;
                    }
                    #endregion

                    #region VALIDAR SI TIENE INCAPACIDADES REGISTRADAS
                    tbNovedadesFijas = IncConsultarXEmpleadoXAplicar(tbAfiliados.Rows[i]["ID_afiliado"].ToString(), tbAfiliados.Rows[i]["ID_contrato"].ToString(), fechaInicio, fechaFin);
                    if ((tbNovedadesFijas != null) && (tbNovedadesFijas.Rows.Count > 0))
                    {
                        for (int j = 0; j < tbNovedadesFijas.Rows.Count; j++)
                        {
                            #region CALCULAR LOS DIAS DE INCAPACIDAD QUE APLICAN A ESTA NOMINA

                            DateTime incInicio, incFin;
                            int diasTranscurridos, diasIncapcidad;// los dias que se asocian de incapacidad, los que cubre el empleador y los que cubre la entidad EN ESTA NOMINA
                            incInicio = (DateTime)tbNovedadesFijas.Rows[j]["inc_fechaInicio"];
                            incFin = (DateTime)tbNovedadesFijas.Rows[j]["inc_fechaFin"];

                            //calcular el numero de días transcurridos desde el inicio de la incapacidad hasta el inicio de la nomina
                            diasTranscurridos = NUtilidades.calcularTiempo(incInicio, string.Format("{0:yyyy-MM-dd}", incFin), "dias");

                            //definir la fecha fin
                            if (DateTime.Compare(DateTime.Parse(fechaFin), (DateTime)tbNovedadesFijas.Rows[0]["inc_fechaFin"]) < 0)
                                incFin = DateTime.Parse(fechaFin);

                            diasIncapcidad = NUtilidades.calcularTiempo(Convert.ToDateTime(fechaInicio), string.Format("{0:yyyy-MM-dd}", incFin), "dias");
                            //si tiene la fecha fin es 28 de febrero
                            if ((incFin.Month == 2) && ((incFin.Day == 28) || (incFin.Day == 29)))
                            {
                                diasIncapcidad++;
                                //si esta incapacitado toda la quincena o todo el mes
                                if ((incInicio.Day == 16) && (diasIncapcidad >= 14))
                                    diasIncapcidad = 15;
                                if ((incInicio.Day == 1) && (diasIncapcidad >= 28))
                                    diasIncapcidad = 30;
                            }


                            //los dos primeros dias los cubre el empleador
                            if (diasTranscurridos < 2)
                                diasIncapcidad--;
                            //consulto si el centro de costo paga los dos primeros dias para calcular el valor de la novedad
                            Datos.DClientes dCliente = new Datos.DClientes();
                            DataTable tbCentroCosto = dCliente.centroConsultarXID(tbAfiliados.Rows[i]["conID_centroCostoFK"].ToString());
                            float valorTotal = 0;
                            //calcular el valor de la novedad
                            long valorNovedad = CalcularValorNovedad(tbNovedadesFijas.Rows[j]["ID_tipoNovedadFK"].ToString(), diasIncapcidad, Convert.ToInt32(tbAfiliados.Rows[i]["con_salario"]), Convert.ToDateTime(fechaInicio), incFin, DateTime.Parse(fechaInicio), DateTime.Parse(fechaFin), (int)tbCentroCosto.Rows[0]["cen_PagaIncapacidad"], ref valorTotal, habilitadoBorrar: false);

                            #region BORRAR ESTA MIERDA
                            //-------------------- VIEJO --------------------
                            //diasIncapcidad = NUtilidades.calcularTiempo(incInicio, string.Format("{0:yyyy-MM-dd}", incFin), "dias");
                            //if (diasIncapcidad > 2)
                            //{
                            //    //si la fecha de inicio de la nomina es mayor que la de la novedad
                            //    if (DateTime.Compare(DateTime.Parse(fechaInicio), (DateTime)tbNovedades.Rows[0]["nov_fechaFin"]) >= 0)
                            //    {
                            //        incInicio = DateTime.Parse(fechaInicio);
                            //        inicioAntes = true;
                            //    }

                            //    if (DateTime.Compare(DateTime.Parse(fechaFin), (DateTime)tbNovedades.Rows[0]["nov_fechaFin"]) < 0)
                            //        incFin = DateTime.Parse(fechaFin);

                            //    //vuelvo a calcular los dias que corresponden a esta nomina
                            //    diasIncapcidad = NUtilidades.calcularTiempo(incInicio, string.Format("{0:yyyy-MM-dd}", incFin), "dias");

                            //    //-----------***************------------
                            //    if (inicioAntes == false)
                            //    { //reviso si es de una incapacidad que inicia antes de la liquidacion de esta nomina
                            //        if (diasIncapcidad <= 2)
                            //        {
                            //            diasEmpleador = diasIncapcidad;
                            //            diasEntidad = 0;
                            //        }
                            //        else
                            //        {
                            //            diasEmpleador = 2;
                            //            diasEntidad = diasIncapcidad - 2;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        diasTranscurridos = NUtilidades.calcularTiempo((DateTime)tbNovedades.Rows[0]["nov_fechaInicio"], fechaInicio, "dias");
                            //        if (diasTranscurridos >= 2)
                            //        {
                            //            diasEmpleador = 0;
                            //            diasEntidad = diasIncapcidad;
                            //        }
                            //        else
                            //        {
                            //            diasEmpleador = 2 - diasTranscurridos;
                            //            diasEntidad = diasIncapcidad - diasEmpleador;
                            //        }
                            //    }
                            //}
                            //else
                            //{//si la incapacidad es por menos de tres dias no me importa el inicio y fin de la incapacidad, la sume el empleador
                            //    diasEmpleador = 2;
                            //    diasEntidad = 0;
                            //}
                            // --------------------- FIN VIEJO
                            #endregion
                            #endregion
                            //registrar la novedad de incapacidad
                            novInsertar(idNomBase, tbNovedadesFijas.Rows[j]["ID_tipoNovedadFK"].ToString(), "Días", Convert.ToDouble(diasIncapcidad), valorNovedad.ToString(), (double)tbNovedadesFijas.Rows[j]["inc_valor"], fechaInicio, string.Format("{0:yyyy-MM-dd}", incFin), "[Automatico]", ID_usuarioRegistraFK, false, false);


                        }
                        reliquidar = true;
                    }
                    #endregion

                    #region LIQUIDAR INTERESES DE CESANTIAS, SI ES ENERO
                    if ((Convert.ToDateTime(fechaInicio).Month == 1) && ((Convert.ToDateTime(fechaInicio).Day == 16) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (Convert.ToDateTime(fechaFin).Day == 30))))
                    {
                        string cesFechaInicio = Convert.ToDateTime(fechaInicio).AddYears(-1).Year.ToString() + "-01-01";
                        string cesFechaFin = Convert.ToDateTime(fechaInicio).AddYears(-1).Year.ToString() + "-12-30";
                        string con_fechaInicio = string.Format("{0:yyyy-MM-dd}", tbAfiliados.Rows[i]["con_fechaInicio"]);
                        if (DateTime.Compare(Convert.ToDateTime(tbAfiliados.Rows[i]["con_fechaInicio"]), Convert.ToDateTime(cesFechaInicio)) < 0)
                            con_fechaInicio = cesFechaInicio;

                        DataTable tbAcumulados = dNomina.liqConsultaCesantias((int)tbAfiliados.Rows[i]["ID_contrato"], cesFechaInicio, cesFechaFin, con_fechaInicio, cesFechaFin);
                        int interesCalculado = Convert.ToInt32(tbAcumulados.Rows[0]["valorInteresCalculado"]);
                        double valorCalculadoCesantias = Convert.ToDouble(tbAcumulados.Rows[0]["valorCesantiasCalculada"]);
                        //INSERTAR EL CONCEPTO DE INTERES

                        novInsertar(idNomBase, "InteresesCesantias", "Pesos", interesCalculado, interesCalculado.ToString(), valorCalculadoCesantias, cesFechaInicio, cesFechaFin, "Intereses de Cesantias", ID_usuarioRegistraFK, true, false);
                        reliquidar = true;
                    }
                    #endregion

                    //si vienen prestamos ó novedades fijas tengo que reliquidar
                    if (reliquidar == true)
                        calcularNominaBase(idNomina.ToString(), tbAfiliados.Rows[i], ID_empresaTemporal, ID_empresaUsuariaFK, ID_centroCosto, fechaInicio, fechaFin, SMLV, AuxTransp, ID_usuarioRegistraFK, novedadesDescuento, novedadesDevengo, idNomBase);

                }//FIN CALCULAR POR EMPLEADO

                //BUSCAR NOMINAS DE LIQUIDACIÓN INDIVIDUAL PARA CONSOLIDAR
                if (nominaIndividual == false)
                    ConsolidarNominasIndividuales(idNomina.ToString(), ID_empresaTemporal, ID_empresaUsuariaFK, ID_centroCosto, "Salarios", fechaInicio, fechaFin, tbAfiliados.Rows[0]["cli_tipoFacturacion"].ToString());

                //RETORNAR EL ID DE LA NÓMINA GENERADA
                return idNomina;
            }
            else
            {
                return 0;
            }
        }

        private string calcularNominaBase(string idNomina, DataRow filaBase, string ID_empresaTemporal, string ID_empresaUsuariaFK, string ID_centroCosto, string fechaInicio, string fechaFin,
                                        double SMLV, double AuxTransp, string ID_usuarioRegistraFK, string novedadesDescuento, string novedadesDevengo, string idNominaBase = "0", bool nom_IndividualLiquidacion = false)
        {
            int diasCotizados, diasNoLaborados, diasEPS, diasAFP, diasARP, diasCCF, diasIncapcidad, diasLR, diasLNR; //L icencia R emunerada , L icencia N o R emunerada
            int diasIncapGeneral, diasIncapSoat, diasIncapARL, diasDevolucionLNR;
            double valorIncapGeneral, valorIncapSoat, valorIncapARL, valorDevolucionLNR;
            double valorLicenciaMaternidad, valorLicenciaPaternidad, valorLR, valorVacacion, valorLNR, valorLicenciaLuto, valorSancion;

            int diasLicenciaLuto, diasLicenciaMaternidad, diasLicenciaPaternidad, diasSancion, diasVacacion;
            double afiEPS, afiAFP, empEPS, empAFP, empCCF, empARL, empPARAF, otrosDevengado, otrosDevengadoNoPrestacional, reteFuente, otrosDescuento;
            int auxTransporte, auxTrasnpLR, auxTranspLNR, auxTranspLicenciaLuto, auxTranspLicenciaMaternidad, auxTransSancion, auxVacacion, auxDevolucionLNR;
            double salarioBase, salarioDevengado, salarioIncap, dvSalario, salarioLicenciaNoRemunerada, salarioHExtra;
            double salarioLicenciaMaternidad, salarioSancion, salarioVacacion;

            double cantHED, cantHEN, cantHEDD, cantHEND, cantHDDO, cantHDDOR, cantHNDOR, cantRN, cantRND;
            double valorHED, valorHEN, valorHEDD, valorHEND, valorHDDO, valorHDDOR, valorHNDOR, valorRN, valorRND;
            double comision = 0;
            double salarioEPS, salarioAFP, salarioCCF, salarioARL;
            double totalDevengado, totalPagar;

            /* AJUSTE CORONA VIRUS  2020  */
            double porAFPEmpresa = 0.12;
            double porAFPEmpleado = 0.04;
            if ((DateTime.Compare(Convert.ToDateTime(fechaInicio), Convert.ToDateTime("2020-04-01")) >= 0) && (DateTime.Compare(Convert.ToDateTime(fechaFin), Convert.ToDateTime("2020-05-31")) == -1))
            {
                porAFPEmpresa = 0.0225;
                porAFPEmpleado = 0.0075;
            }

            //DataTable tbNovedades;
            DataTable tbNewNovedades;
            dvSalario = 0; //lo uso para guardar lo que es salario, independiente de si hay incapacidad + HExtra + Devengos Prestacionales, para efectos de liquidacion
            reteFuente = 0;
            salarioHExtra = 0;
            otrosDevengado = 0;
            otrosDevengadoNoPrestacional = 0;
            otrosDescuento = 0;

            auxTransSancion = 0;
            ///*******************************************************************************************
            #region INICIALIZO VARIABLES DE NOVEDADES PARA REPORTE

            diasSancion = 0;
            valorSancion = 0;
            salarioSancion = 0;

            diasLicenciaMaternidad = 0;
            valorLicenciaMaternidad = 0;
            salarioLicenciaMaternidad = 0;
            auxTranspLicenciaMaternidad = 0;

            diasLicenciaPaternidad = 0;
            valorLicenciaPaternidad = 0;

            diasLR = 0;
            valorLR = 0;
            auxTrasnpLR = 0;

            diasLNR = 0;
            valorLNR = 0;
            auxTranspLNR = 0;
            salarioLicenciaNoRemunerada = 0;

            diasDevolucionLNR = 0;
            valorDevolucionLNR = 0;
            auxDevolucionLNR = 0;

            diasLicenciaLuto = 0;
            valorLicenciaLuto = 0;
            auxTranspLicenciaLuto = 0;
            salarioVacacion = 0;

            diasVacacion = 0;
            valorVacacion = 0;
            auxVacacion = 0;

            diasIncapGeneral = 0;
            diasIncapSoat = 0;
            diasIncapARL = 0;
            valorIncapGeneral = 0;
            valorIncapSoat = 0;
            valorIncapARL = 0;
            salarioIncap = 0;
            diasIncapcidad = 0;

            cantHED = 0; cantHEN = 0; cantHEDD = 0; cantHEND = 0; cantHDDO = 0; cantHDDOR = 0; cantHNDOR = 0; cantRN = 0; cantRND = 0;
            valorHED = 0; valorHEN = 0; valorHEDD = 0; valorHEND = 0; valorHDDO = 0; valorHDDOR = 0; valorHNDOR = 0; valorRN = 0; valorRND = 0;
            #endregion

            //VALIDO SI TIENE SUBSIDIO DE TRANSPORTE POR EL CONTRATO
            if (filaBase["con_subsidioTransporte"].ToString() == "No")
                AuxTransp = 0;

            #region CALCULO DIAS
            //1) CALCULO CUANTOS DIAS DEBE COTIZAR 
            //si no tiene fecha de retiro
            if (filaBase["con_fechaFin"].ToString() == "")
            {
                if (DateTime.Compare((DateTime)filaBase["con_fechaInicio"], DateTime.Parse(fechaInicio)) == 1)//con_fechaInicio es mayor, osea tiene ingreso
                {   //DESDE LA FECHA DE INICIO DEL --CONTRATO-- HASTA EL FINAL DEL PERIODO
                    if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(fechaFin).Day == 28))
                        diasCotizados = NUtilidades.calcularTiempo((DateTime)filaBase["con_fechaInicio"], fechaFin, "dias") + 2;
                    else
                    {
                        if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(fechaFin).Day == 29))
                            diasCotizados = NUtilidades.calcularTiempo((DateTime)filaBase["con_fechaInicio"], fechaFin, "dias") + 1;
                        else
                        { //es un mes de 30 ó 31 días
                            diasCotizados = NUtilidades.calcularTiempo((DateTime)filaBase["con_fechaInicio"], fechaFin, "dias");
                            if (Convert.ToDateTime(fechaFin).Day == 31) // si son 31 dias, descuento uno
                                diasCotizados--;
                        }
                    }
                }
                else
                {   //DESDE LA FECHA DE INICIO DEL && PERIODO && HASTA EL FINAL DEL PERIODO
                    if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(fechaFin).Day == 28))
                        diasCotizados = NUtilidades.calcularTiempo(DateTime.Parse(fechaInicio), fechaFin, "dias") + 2;
                    else
                    {
                        if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(fechaFin).Day == 29))
                            diasCotizados = NUtilidades.calcularTiempo(DateTime.Parse(fechaInicio), fechaFin, "dias") + 1;
                        else
                        {
                            diasCotizados = NUtilidades.calcularTiempo(DateTime.Parse(fechaInicio), fechaFin, "dias");
                            if (Convert.ToDateTime(fechaFin).Day == 31) // si son 31 dias, descuento uno
                                diasCotizados--;
                        }
                    }
                }
            }
            else
            { //tiene fecha de retiro
                if (DateTime.Compare((DateTime)filaBase["con_fechaFin"], DateTime.Parse(fechaFin)) <= 0)
                {
                    #region TIENE RETIRO EN EL MES QUE LIQUIDO LA NOMINA
                    //si el retiro es del mes que INGRESO, me toca comparar la fecha INICIO y FIN, por lo de las catorcenas
                    if (DateTime.Parse(filaBase["con_fechaInicio"].ToString()).Month == DateTime.Parse(filaBase["con_fechaFin"].ToString()).Month)
                    {
                        if (DateTime.Compare((DateTime)filaBase["con_fechaInicio"], DateTime.Parse(fechaInicio)) == 1)
                        {
                            if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(filaBase["con_fechaFin"]).Day == 28))
                                diasCotizados = NUtilidades.calcularTiempo((DateTime)filaBase["con_fechaInicio"], string.Format("{0:yyyy-MM-dd}", filaBase["con_fechaFin"]), "dias") + 2;
                            else
                            {
                                if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(filaBase["con_fechaFin"]).Day == 29))
                                    diasCotizados = NUtilidades.calcularTiempo((DateTime)filaBase["con_fechaInicio"], string.Format("{0:yyyy-MM-dd}", filaBase["con_fechaFin"]), "dias") + 1;
                                else
                                {
                                    diasCotizados = NUtilidades.calcularTiempo((DateTime)filaBase["con_fechaInicio"], string.Format("{0:yyyy-MM-dd}", filaBase["con_fechaFin"]), "dias");
                                    if (Convert.ToDateTime(filaBase["con_fechaFin"]).Day == 31) // si son 31 dias, descuento uno
                                        diasCotizados--;
                                }
                            }
                        }
                        else
                        {
                            if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(filaBase["con_fechaFin"]).Day == 28))
                                diasCotizados = NUtilidades.calcularTiempo(DateTime.Parse(fechaInicio), string.Format("{0:yyyy-MM-dd}", filaBase["con_fechaFin"]), "dias") + 2;
                            else
                            {
                                if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(filaBase["con_fechaFin"]).Day == 29))
                                    diasCotizados = NUtilidades.calcularTiempo(DateTime.Parse(fechaInicio), string.Format("{0:yyyy-MM-dd}", filaBase["con_fechaFin"]), "dias") + 1;
                                else
                                {
                                    diasCotizados = NUtilidades.calcularTiempo(DateTime.Parse(fechaInicio), string.Format("{0:yyyy-MM-dd}", filaBase["con_fechaFin"]), "dias");
                                    if (Convert.ToDateTime(filaBase["con_fechaFin"]).Day == 31) // si son 31 dias, descuento uno
                                        diasCotizados--;
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(filaBase["con_fechaFin"]).Day == 28))
                            diasCotizados = NUtilidades.calcularTiempo(DateTime.Parse(fechaInicio), string.Format("{0:yyyy-MM-dd}", filaBase["con_fechaFin"]), "dias") + 2;
                        else
                        {
                            if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(filaBase["con_fechaFin"]).Day == 29))
                                diasCotizados = NUtilidades.calcularTiempo(DateTime.Parse(fechaInicio), string.Format("{0:yyyy-MM-dd}", filaBase["con_fechaFin"]), "dias") + 1;
                            else
                            {
                                diasCotizados = NUtilidades.calcularTiempo(DateTime.Parse(fechaInicio), string.Format("{0:yyyy-MM-dd}", filaBase["con_fechaFin"]), "dias");
                                if (Convert.ToDateTime(filaBase["con_fechaFin"]).Day == 31) // si son 31 dias, descuento uno
                                    diasCotizados--;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region TIENE RETIRO EN OTRA NOMINA
                    //si la fecha de ingreso es superior a la fecha de inicio de liquidacion de nomina
                    if (DateTime.Compare((DateTime)filaBase["con_fechaInicio"], DateTime.Parse(fechaInicio)) == 1)
                    {
                        if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(fechaFin).Day == 28))
                            diasCotizados = NUtilidades.calcularTiempo((DateTime)filaBase["con_fechaInicio"], fechaFin, "dias") + 2;
                        else
                        {
                            if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(fechaFin).Day == 29))
                                diasCotizados = NUtilidades.calcularTiempo((DateTime)filaBase["con_fechaInicio"], fechaFin, "dias") + 1;
                            else
                            {
                                diasCotizados = NUtilidades.calcularTiempo((DateTime)filaBase["con_fechaInicio"], fechaFin, "dias");
                                if (Convert.ToDateTime(fechaFin).Day == 31) // si son 31 dias, descuento uno
                                    diasCotizados--;
                            }
                        }
                    }
                    else
                    {//liquido desde la fecha de inicio de nomina hasta la fecha fin de nomina
                        if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(fechaFin).Day == 28))
                            diasCotizados = NUtilidades.calcularTiempo(DateTime.Parse(fechaInicio), fechaFin, "dias") + 2;
                        else
                        {
                            if ((Convert.ToDateTime(fechaFin).Month == 2) && (Convert.ToDateTime(fechaFin).Day == 29))
                                diasCotizados = NUtilidades.calcularTiempo(DateTime.Parse(fechaInicio), fechaFin, "dias") + 1;
                            else
                            {
                                diasCotizados = NUtilidades.calcularTiempo(DateTime.Parse(fechaInicio), fechaFin, "dias");
                                if (Convert.ToDateTime(fechaFin).Day == 31) // si son 31 dias, descuento uno
                                    diasCotizados--;
                            }
                        }
                    }
                    #endregion
                }
            }
            diasCotizados = diasCotizados <= 0 ? 1 : diasCotizados;
            diasEPS = diasCotizados;
            diasAFP = diasCotizados;
            diasARP = diasCotizados;
            diasCCF = diasCotizados;

            //TRAIGO TODAS LAS NOVEDADES Y LUEGO RECORRO
            tbNewNovedades = idNominaBase == "0" ? null : dNomina.novConsultaTodasLaboralesXNominaBase(idNominaBase);

            #region DIAS LABORADOS PARA TIPO TIEMPO PARCIAL - SABATINOS
            if (filaBase["con_jornada"].ToString() == "Tiempo Parcial")
            {
                otrosDevengadoNoPrestacional = NUtilidades.redondea(((double)filaBase["con_ingresosNoSalariales"] / 30) * diasCotizados, 1);
                diasCotizados = 1;
                if (tbNewNovedades != null)
                {
                    DataRow[] rowNovedadParcial = tbNewNovedades.Select("nov_tipoNovedadFK='DiasLaborados'", "");
                    if (rowNovedadParcial.Length > 0)
                        diasCotizados = Convert.ToInt32(rowNovedadParcial[0]["nov_valor"]);
                }
            }
            #endregion

            #endregion

            #region CALCULO SALARIO BASE PARA CALCULOS

            if (diasCotizados != 30)
            {
                salarioBase = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * diasCotizados, 1);
                if (filaBase["con_jornada"].ToString() != "Tiempo Parcial")
                    otrosDevengadoNoPrestacional = NUtilidades.redondea(((double)filaBase["con_ingresosNoSalariales"] / 30) * diasCotizados, 1);
            }
            else
            {
                salarioBase = (double)filaBase["con_salario"];
                otrosDevengadoNoPrestacional = Convert.ToInt32(filaBase["con_ingresosNoSalariales"]);
            }

            if (filaBase["con_jornada"].ToString() == "Medio Tiempo")
            {
                dvSalario = NUtilidades.redondea(salarioBase / 2, 1);
                salarioDevengado = dvSalario;
            }
            else
            {
                dvSalario = salarioBase;
                salarioDevengado = salarioBase;
            }

            salarioEPS = salarioBase;
            salarioAFP = salarioBase;
            salarioCCF = salarioBase;
            salarioARL = salarioBase;

            salarioIncap = 0;

            #endregion

            #region NOVEDADES

            #region NOVEDADES DE INCAP, LICENCIAS Y SANCION
            // SI TIENE NOVEDADES REVISO HABER DE QUE NOVEDADES APLICO -- TOCO CARGARLA EN EL CALCULO DE DIAS POR LOS DIAS DE TIEMPO PARCIAL
            if ((tbNewNovedades != null) && (tbNewNovedades.Rows.Count > 0))
            {
                DataRow[] rowNovedad;

                #region NOVEDADES DE INCAPACIDAD GENERAL, SOAT Y ARL
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='IncapGeneral'", "");
                if (rowNovedad.Length > 0)
                {
                    diasIncapGeneral = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorIncapGeneral = Convert.ToInt32(rowNovedad[0]["valorPesos"]);
                }

                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='Incapacidad a cargo del cliente'", "");
                if (rowNovedad.Length > 0)
                {
                    diasIncapGeneral += Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorIncapGeneral += Convert.ToInt32(rowNovedad[0]["valorPesos"]);
                }

                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='Incapacidad EG superior a 3 dias'", "");
                if (rowNovedad.Length > 0)
                {
                    diasIncapGeneral += Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorIncapGeneral += Convert.ToInt32(rowNovedad[0]["valorPesos"]);
                }

                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='IncapSoat'", "");
                if (rowNovedad.Length > 0)
                {
                    diasIncapSoat = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorIncapSoat = Convert.ToInt32(rowNovedad[0]["valorPesos"]);
                }

                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='IncapARL'", "");
                if (rowNovedad.Length > 0)
                {
                    diasIncapARL = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorIncapARL = Convert.ToInt32(rowNovedad[0]["valorPesos"]);
                }
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='Incapacidad por ACC LABORAL 1 dia cargo del cliente'", "");
                if (rowNovedad.Length > 0)
                {
                    diasIncapARL += Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorIncapARL += Convert.ToInt32(rowNovedad[0]["valorPesos"]);
                }
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='Incapacidad por ACC LABORAL superior a 2 dias'", "");
                if (rowNovedad.Length > 0)
                {
                    diasIncapARL += Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorIncapARL += Convert.ToInt32(rowNovedad[0]["valorPesos"]);
                }

                if ((diasIncapGeneral + diasIncapSoat + diasIncapARL) > 0)
                {
                    diasIncapcidad = diasIncapGeneral + diasIncapSoat + diasIncapARL;
                    salarioIncap = valorIncapGeneral + valorIncapSoat + valorIncapARL;
                    diasARP = diasARP - diasIncapcidad;

                    if ((int)filaBase["cen_PagaIncapacidadToda"] == 1)
                        salarioIncap = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * diasIncapcidad, 1);
                    if (filaBase["con_jornada"].ToString() == "Tiempo Parcial")
                    {
                        if (diasCotizados != 1)
                            diasCotizados += diasIncapcidad;
                        else
                        {
                            diasCotizados = diasIncapcidad;
                        }
                    }
                }
                #endregion

                #region NOVEDADES DE LICENCIA DE PATERNIDAD
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='LicenciaPaternidad'", "");
                if (rowNovedad.Length > 0)
                {
                    diasLicenciaPaternidad = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorLicenciaPaternidad = Convert.ToInt32(rowNovedad[0]["valorPesos"]);

                    diasLicenciaMaternidad = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    //valorLicenciaMaternidad = Convert.ToInt32(rowNovedad[0]["valorPesos"]);
                    auxTranspLicenciaMaternidad = NUtilidades.redondea(AuxTransp / 30 * diasLicenciaMaternidad, 1);
                    salarioLicenciaMaternidad = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * diasLicenciaMaternidad, 1);
                }
                #endregion

                #region NOVEDADES DE LICENCIA REMUNERADA y DIA DE LA FAMILIA
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='LicenciaRemunerada'", "");
                if (rowNovedad.Length > 0)
                {
                    diasLR = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorLR = Convert.ToInt32(rowNovedad[0]["valorPesos"]);

                    auxTrasnpLR = NUtilidades.redondea(AuxTransp / 30 * diasLR, 1);
                    diasARP = diasARP - diasLR;
                }

                #endregion

                #region NOVEDADES DE VACACIONES -->ANTICIPADAS

                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='Vacaciones'", "");
                if (rowNovedad.Length > 0)
                {
                    diasVacacion = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorVacacion = Convert.ToInt32(rowNovedad[0]["valorPesos"]);

                    auxVacacion = NUtilidades.redondea(AuxTransp / 30 * diasVacacion, 1);
                    diasARP = diasARP - diasVacacion;
                    salarioVacacion = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * diasVacacion, 1);
                }

                #endregion

                #region NOVEDADES DE VACACIONES DISFRUTADAS

                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='VacacionesDisfrutadas'", "");
                if (rowNovedad.Length > 0)
                {
                    diasVacacion = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorVacacion = Convert.ToInt32(rowNovedad[0]["valorPesos"]);

                    auxVacacion = NUtilidades.redondea(AuxTransp / 30 * diasVacacion, 1);
                    diasARP = diasARP - diasVacacion;
                    salarioVacacion = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * diasVacacion, 1);
                }

                #endregion

                #region NOVEDADES DE LICENCIA NO REMUNERADA
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='LicenciaNoRemunerada'", "");
                if (rowNovedad.Length > 0)
                {
                    diasLNR = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorLNR = Convert.ToInt32(rowNovedad[0]["valorPesos"]);

                    auxTranspLNR = NUtilidades.redondea(AuxTransp / 30 * diasLNR, 1);

                    if (filaBase["con_jornada"].ToString() == "Medio Tiempo")
                        salarioLicenciaNoRemunerada = NUtilidades.redondea(((double)filaBase["con_salario"] / 60) * diasLNR, 1);
                    else
                        salarioLicenciaNoRemunerada = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * diasLNR, 1);
                }
                #endregion

                #region NOVEDADES DE LICENCIA POR LUTO
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='LicenciaLuto'", "");
                if (rowNovedad.Length > 0)
                {
                    diasLicenciaLuto = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorLicenciaLuto = Convert.ToInt32(rowNovedad[0]["valorPesos"]);

                    auxTranspLicenciaLuto = NUtilidades.redondea(AuxTransp / 30 * diasLicenciaLuto, 1);
                    diasARP = diasARP - diasLicenciaLuto;
                }
                #endregion

                #region NOVEDADES DE LICENCIA DE MATERNINAD
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='LicenciaMaternidad'", "");
                if (rowNovedad.Length > 0)
                {
                    diasLicenciaMaternidad = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorLicenciaMaternidad = Convert.ToInt32(rowNovedad[0]["valorPesos"]);

                    auxTranspLicenciaMaternidad = NUtilidades.redondea(AuxTransp / 30 * diasLicenciaMaternidad, 1);
                    salarioLicenciaMaternidad = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * diasLicenciaMaternidad, 1);
                }
                #endregion

                #region NOVEDAD DE SANCION

                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='Sancion'", "");
                if (rowNovedad.Length > 0)
                {
                    diasSancion = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorSancion = Convert.ToInt32(rowNovedad[0]["valorPesos"]);

                    auxTransSancion = NUtilidades.redondea(AuxTransp / 30 * diasSancion, 1);
                    salarioSancion = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * diasSancion, 1);
                }

                #endregion

                #region NOVEDADES DE DEVOLUCION DE  LICENCIA NO REMUNERADA
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='DevolucionLNR'", "");
                if (rowNovedad.Length > 0)
                {
                    diasDevolucionLNR = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                    valorDevolucionLNR = Convert.ToInt32(rowNovedad[0]["valorPesos"]);

                    auxDevolucionLNR = NUtilidades.redondea(AuxTransp / 30 * diasLNR, 1);
                }
                #endregion

            }

            #endregion

            #region RECALCULO SALARIO BASE Y DIAS
            salarioARL = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * (diasCotizados - diasIncapcidad - diasLNR - diasSancion - diasLicenciaMaternidad - diasVacacion - diasLR - diasLicenciaLuto + diasDevolucionLNR), 1);
            //octubre 03 de 2017
            salarioCCF = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * (diasCotizados - diasIncapcidad - diasLNR - diasSancion + diasDevolucionLNR), 1);
            //VALIDO LOS DIAS LABORADOS ANTES DE VER CUANTO LE DOY DE AUX DE TRANSPORTE
            diasNoLaborados = diasLNR + diasSancion + diasLR + diasVacacion;
            //caso acctiservicios, las incapacidades no afectan los dias laborados
            if (((bool)filaBase["cen_ParcialSegSocial"] == false))
                diasCotizados = diasCotizados - diasIncapcidad - diasLNR - diasSancion - diasVacacion + diasDevolucionLNR;
            else
                diasCotizados = diasCotizados - diasLNR - diasSancion - diasVacacion + diasDevolucionLNR;
            //COMO PUEDEN VARIAR LOS DIAS COTIZADOS FINALES, RECALCULO EL SALARIO BASE
            if (diasCotizados != 30)
                salarioBase = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * (diasCotizados), 1);
            else
                salarioBase = (double)filaBase["con_salario"];
            //octubre 10 2017- parametro en cliente de liquidación por dias
            if ((bool)filaBase["cli_IngresosNoPrestacionales"] == true)
                otrosDevengadoNoPrestacional = NUtilidades.redondea(((double)filaBase["con_ingresosNoSalariales"] / 30) * (diasCotizados - diasLicenciaLuto - diasLicenciaMaternidad), 1);

            #endregion

            #region GARANTIZAR SALARIO DEVENGADO

            if (filaBase["con_jornada"].ToString() == "Medio Tiempo")
            {
                salarioDevengado = (salarioBase / 2) + salarioIncap;
            }
            else
            {
                salarioDevengado = salarioBase + salarioIncap;
                //NO PUEDE GANAR MENOS QUE EL SALARIO MINIMO
                if ((salarioDevengado) < ((SMLV / 30) * (diasCotizados + diasIncapcidad)))
                    salarioDevengado = (SMLV / 30) * (diasCotizados + diasIncapcidad);
            }
            #endregion

            #region NOVEDADES DE INGRESOS Y DESCUENTOS
            if ((tbNewNovedades != null) && (tbNewNovedades.Rows.Count > 0))
            {
                DataRow[] rowNovedad;
                #region NOVEDADES HORA EXTRA 
                #region HED
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='HED'", "");
                if (rowNovedad.Length > 0)
                {
                    cantHED = Convert.ToDouble(rowNovedad[0]["nov_valor"]);
                    valorHED = Convert.ToDouble(rowNovedad[0]["valorPesos"]);
                }
                #endregion

                #region HEN
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='HEN'", "");
                if (rowNovedad.Length > 0)
                {
                    cantHEN = Convert.ToDouble(rowNovedad[0]["nov_valor"]);
                    valorHEN = Convert.ToDouble(rowNovedad[0]["valorPesos"]);
                }
                #endregion

                #region HEDD
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='HEDD'", "");
                if (rowNovedad.Length > 0)
                {
                    cantHEDD = Convert.ToDouble(rowNovedad[0]["nov_valor"]);
                    valorHEDD = Convert.ToDouble(rowNovedad[0]["valorPesos"]);
                }
                #endregion

                #region HEND
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='HEND'", "");
                if (rowNovedad.Length > 0)
                {
                    cantHEND = Convert.ToDouble(rowNovedad[0]["nov_valor"]);
                    valorHEND = Convert.ToDouble(rowNovedad[0]["valorPesos"]);
                }
                #endregion

                #region HDDO
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='HDDO'", "");
                if (rowNovedad.Length > 0)
                {
                    cantHDDO = Convert.ToDouble(rowNovedad[0]["nov_valor"]);
                    valorHDDO = Convert.ToDouble(rowNovedad[0]["valorPesos"]);
                }
                #endregion

                #region HDDOR
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='HDDOR'", "");
                if (rowNovedad.Length > 0)
                {
                    cantHDDOR = Convert.ToDouble(rowNovedad[0]["nov_valor"]);
                    valorHDDOR = Convert.ToDouble(rowNovedad[0]["valorPesos"]);
                }
                #endregion

                #region HNDOR
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='HNDOR'", "");
                if (rowNovedad.Length > 0)
                {
                    cantHNDOR = Convert.ToDouble(rowNovedad[0]["nov_valor"]);
                    valorHNDOR = Convert.ToDouble(rowNovedad[0]["valorPesos"]);
                }
                #endregion

                #region RN
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='RN'", "");
                if (rowNovedad.Length > 0)
                {
                    cantRN = Convert.ToDouble(rowNovedad[0]["nov_valor"]);
                    valorRN = Convert.ToDouble(rowNovedad[0]["valorPesos"]);
                }
                #endregion

                #region RND
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='RND'", "");
                if (rowNovedad.Length > 0)
                {
                    cantRND = Convert.ToDouble(rowNovedad[0]["nov_valor"]);
                    valorRND = Convert.ToDouble(rowNovedad[0]["valorPesos"]);
                }
                #endregion

                //TOTAL NOVEDADES DE HORAS EXTRAS, QUE MODIFICAN LA BASE SALARIAL
                if ((valorHED + valorHEN + valorHEDD + valorHEND + valorHDDO + valorHDDOR + valorHNDOR + valorRN + valorRND) > 0)
                {
                    salarioHExtra = valorHED + valorHEN + valorHEDD + valorHEND + valorHDDO + valorHDDOR + valorHNDOR + valorRN + valorRND;
                    salarioEPS += salarioHExtra;
                    salarioAFP += salarioHExtra;
                    salarioCCF += salarioHExtra;
                    salarioARL += salarioHExtra;
                    //otrosDevengado += Convert.ToInt32(Convert.ToDouble(tbNovedades.Rows[0]["valorPesos"].ToString()));
                }

                #endregion

                #region OTROS DESCUENTOS
                DataTable tbFiltro = dNomina.SelectFromDataTable(tbNewNovedades, "nov_tipoNovedadFK IN(" + novedadesDescuento + ",'DescuentosParcial')", "");
                if ((tbFiltro != null) && (tbFiltro.Rows.Count > 0))
                {
                    for (int x = 0; x < tbFiltro.Rows.Count; x++)
                    {
                        otrosDescuento += Convert.ToInt32(tbFiltro.Rows[x]["nov_valor"]);
                    }
                }

                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='HoraDeducida'", "");
                if (rowNovedad.Length > 0)
                {
                    otrosDescuento += Convert.ToInt32(rowNovedad[0]["valorPesos"]);
                }
                #endregion

                #region HORAS ADICIONALES PARA TIPO TIEMPO PARCIAL - SABATINO
                int horasAdicionales = 0;
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='HoraAdicional'", "");
                if (rowNovedad.Length > 0)
                {
                    otrosDevengadoNoPrestacional += Convert.ToInt32(rowNovedad[0]["valorPesos"]);
                    horasAdicionales = Convert.ToInt32(rowNovedad[0]["nov_valor"]);
                }
                #endregion

                #region OTROS DEVENGOS PRESTACIONALES

                tbFiltro = dNomina.SelectFromDataTable(tbNewNovedades, "nov_tipoNovedadFK IN('Comisiones','DevengoSalarial','DevolucionLNRMes','Ajusteincapacidadpresentada','AjusteDiasLaborados','HoraCompensadaPrestacional')", "");
                if ((tbFiltro != null) && (tbFiltro.Rows.Count > 0))
                {
                    for (int x = 0; x < tbFiltro.Rows.Count; x++)
                    {
                        otrosDevengado += Convert.ToDouble(tbFiltro.Rows[x]["nov_valor"]);
                        if(tbFiltro.Rows[x]["nov_tipoNovedadFK"].ToString()== "Comisiones")
                            comision += Convert.ToDouble(tbFiltro.Rows[x]["nov_valor"]);
                    }

                    salarioEPS += otrosDevengado;
                    salarioAFP += otrosDevengado;
                    salarioCCF += otrosDevengado;
                    salarioARL += otrosDevengado;
                    // caso domenico
                    if (((bool)filaBase["cen_HEsobreComision"]) && (salarioLicenciaNoRemunerada>0))
                    { 
                        salarioLicenciaNoRemunerada = NUtilidades.redondea(((((double)filaBase["con_salario"] + comision) / 30) * diasLNR) , 1);
                    }
                }

                #endregion

                #region OTROS DEVENGOS - HORAS COMPENSADAS PARA TIPO TIEMPO PARCIAL Y SALARIO PARA PROVISION
                if (filaBase["con_tipo"].ToString() == "SABATINO")
                {
                    otrosDevengado = Convert.ToInt32(((double)filaBase["con_salario"] / 240) * (((diasCotizados * 8) + horasAdicionales) * 0.166666666666667));
                    if (diasCotizados < 7)
                        salarioEPS = (double)filaBase["con_salario"] / 4;
                    else
                    {
                        if (diasCotizados < 14)
                            salarioEPS = (double)filaBase["con_salario"] / 2;
                        else
                            salarioEPS = ((double)filaBase["con_salario"] / 4) * 3;
                    }
                    salarioAFP = salarioEPS;
                    salarioCCF = salarioEPS;
                    salarioARL = (double)filaBase["con_salario"];
                }
                #endregion

                #region OTROS DEVENGOS NO PRESTACIONALES
                //tbFiltro = dNomina.SelectFromDataTable(tbNewNovedades, "nov_tipoNovedadFK IN('BonificacionOcasional','DevengoOrdinario','AuxilioMovilizacion','AuxilioRodamiento','DevengoOtroNoPres','AportesNoPrestacionales','OtroAuxilioParcial','IngresoPorAjuste','InteresesCesantias')", "");
                tbFiltro = dNomina.SelectFromDataTable(tbNewNovedades, "nov_tipoNovedadFK IN(" + novedadesDevengo + ") AND nov_tipoNovedadFK NOT IN('Comisiones','DevengoSalarial','DevolucionLNRMes','Ajusteincapacidadpresentada','AjusteDiasLaborados')", "");
                if ((tbFiltro != null) && (tbFiltro.Rows.Count > 0))
                {
                    for (int x = 0; x < tbFiltro.Rows.Count; x++)
                    {
                        otrosDevengadoNoPrestacional += Convert.ToInt32(tbFiltro.Rows[x]["valorPesos"]);
                    }
                }
                #endregion

                #region EMBARGO DE ALIMENTOS
                //rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='EmbargoAlimentos'", "");
                //if (rowNovedad.Length > 0)
                //{   //ALIMENTOS ES LA MITAD DE LO QUE SE GANA
                //    otrosDescuento += Convert.ToInt32((salarioBase + salarioIncap + salarioHExtra + otrosDevengado + otrosDevengadoNoPrestacional) / 2);
                //}
                #endregion
            }

            #endregion 

            #region RETIRO -- AFECTA CCF
            //if ((filaBase["con_fechaFin"].ToString() != "") && (System.Configuration.ConfigurationSettings.AppSettings["ProvisionVacacionCCF"].ToString() == "NO")) //Noviembre 2019, salarioCCF sale en el plano tal cual, modifico acá para que se cobre a la usuaria, pero me salga bien en el plano
            double valorVacacionesEnRetiro = 0;
            if (filaBase["con_fechaFin"].ToString() != "")
            {
                DataTable tbVacaciones = liqConsultaVacaciones((int)filaBase["ID_contrato"]);
                valorVacacionesEnRetiro = (double)tbVacaciones.Rows[0]["valorVacaciones"];
                salarioCCF += valorVacacionesEnRetiro;
            }
            #endregion

            #endregion

            double SMLV_Calculado = 0;
            int diasLaboradosAnterior = 0;
            int diasNoLaboradosAnterior = 0;
            int diasVacacionesAnterior = 0;
            double salarioAnterior = 0;
            double epsAnterior = 0;
            double afpAnterior = 0;
            double totalHEAnterior = 0;
            int auxTransAnterior = 0;

            #region AUXILIO DE TRANSPORTE
            double salarioHEComparar = 0;
            if (System.Configuration.ConfigurationSettings.AppSettings["HEAfectanAuxTrans"].ToString() == "SI")
                salarioHEComparar = salarioHExtra;
            //otrosDevengadoNoPrestacional Mayo 25 -- 
            if ((Convert.ToDateTime(fechaInicio).Day == 16) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (Convert.ToDateTime(fechaFin).Day >= 16)) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (filaBase["con_fechaFin"].ToString() != "")))
            {
                //CALCULO AUX TRANSPORTE CON TODOS LOS INGRESOS DEL MES
                #region SALARIO ANTERIOR

                if (Convert.ToDateTime(fechaInicio).Day == 16)
                {   //buscar la nómina anterior
                    string fechaInicioAnterior = string.Format("{0:yyyy-MM-01}", Convert.ToDateTime(fechaInicio));
                    string fechaFinAnterior = string.Format("{0:yyyy-MM-15}", Convert.ToDateTime(fechaFin));

                    DataTable tbNominaAnterior = dNomina.ConsultaNominaAnteriorXContrato((int)filaBase["ID_contrato"], (int)filaBase["ID_afiliado"], ID_empresaTemporal, fechaInicioAnterior, fechaFinAnterior);
                    if ((tbNominaAnterior != null) && (tbNominaAnterior.Rows.Count > 0))
                    {
                        salarioAnterior = (double)tbNominaAnterior.Rows[0]["nom_dvSalario"] + (double)tbNominaAnterior.Rows[0]["nom_dvOtros"];
                        auxTransAnterior = Convert.ToInt32(tbNominaAnterior.Rows[0]["nom_dvAuxilioTransporte"]);
                        epsAnterior = (double)tbNominaAnterior.Rows[0]["nom_ddEPSafiliado"];
                        afpAnterior = (double)tbNominaAnterior.Rows[0]["nom_ddAFPafiliado"];
                        diasLaboradosAnterior = Convert.ToInt32(tbNominaAnterior.Rows[0]["diasAnterior"]);
                        diasVacacionesAnterior = Convert.ToInt32(tbNominaAnterior.Rows[0]["VacacionesDias"]);
                        diasNoLaboradosAnterior = Convert.ToInt32(tbNominaAnterior.Rows[0]["diasNoLaboradosAnterior"]);//Oct 31 2019- quite de los no laborados en la bd la LNR, por que se descuenta de los dias laborados aut
                        totalHEAnterior = (double)tbNominaAnterior.Rows[0]["totalHEAnterior"];
                    }
                }
                #endregion
                SMLV_Calculado = ((SMLV / 30) * ((diasCotizados + diasNoLaborados + diasIncapcidad - diasDevolucionLNR) + (diasLaboradosAnterior + diasNoLaboradosAnterior + diasVacacionesAnterior)));
                double totalValidarAux = 0;
                if (System.Configuration.ConfigurationSettings.AppSettings["HEAfectanAuxTrans"].ToString() == "SI")
                    totalValidarAux = salarioBase + salarioIncap + salarioHEComparar + otrosDevengado + salarioAnterior;
                else
                    totalValidarAux = salarioBase + salarioIncap + salarioHEComparar + otrosDevengado + salarioAnterior - totalHEAnterior;

                if (((double)filaBase["con_salario"] <= (SMLV * 2)) && ((filaBase["con_subsidioTransporte"].ToString() == "Si")) && ((totalValidarAux) <= (SMLV_Calculado * 2)))
                {   //tiene derecho a subsidio de transporte, verifico cuanto le pagué la quincena pasada, si no le pague, se lo sumo
                    if (auxTransAnterior > 0)
                        auxTransporte = NUtilidades.redondea(AuxTransp / 30 * (diasCotizados), 1) - (auxTranspLicenciaLuto + auxTranspLicenciaMaternidad + auxTrasnpLR);
                    else
                    {
                        if ((bool)filaBase["cli_AuxTransQuincenal"] == true) {//parametro impoveles,cyg
                            //en la quincena anterior no le pague auxilio
                            if ((diasCotizados + diasLaboradosAnterior - diasVacacionesAnterior) > 0)
                            {
                                auxTransporte = NUtilidades.redondea(AuxTransp / 30 * (diasCotizados + diasLaboradosAnterior - diasVacacionesAnterior), 1) - (auxTranspLicenciaLuto + auxTranspLicenciaMaternidad + auxTrasnpLR);
                                //NOVIEMBRE 07/2019 - quito de la resta diasNoLaboradosAnterior. Se paga el aux de lo laborado, mas lo de este mes
                                //auxTransporte = NUtilidades.redondea(AuxTransp / 30 * (diasCotizados + diasLaboradosAnterior - diasVacacionesAnterior - diasNoLaboradosAnterior), 1) - (auxTranspLicenciaLuto + auxTranspLicenciaMaternidad + auxTrasnpLR);
                            }
                            else
                            {
                                //julio 23 2019, borré de la resta los diasNoLaborados anterior. Ejemplo de oferta con vacaciones e incapacidad, que en la primera quincena da 0 dias laborados
                                auxTransporte = NUtilidades.redondea(AuxTransp / 30 * (diasCotizados + diasLaboradosAnterior), 1) - (auxTranspLicenciaLuto + auxTranspLicenciaMaternidad + auxTrasnpLR);
                            }
                        }
                        else
                        {//parametro impovelez cyg
                            auxTransporte = NUtilidades.redondea(AuxTransp / 30 * (diasCotizados + diasLaboradosAnterior), 1) - (auxTranspLicenciaLuto + auxTranspLicenciaMaternidad + auxTrasnpLR);
                        }
                    }
                }
                else
                {   //como no tiene derecho, verifico si le pague en la quincena anterior para descontarselo                    
                    if (auxTransAnterior > 0)
                        auxTransporte = auxTransAnterior * -1;
                    else
                        auxTransporte = 0;
                }


                //QUITAR OFERTA --- POR COVID, QUITO LA VALIDACION DE LA SEGUNDA QUINCENA
                //if (((bool)filaBase["cli_AuxTransQuincenal"] == true) && ((double)filaBase["con_salario"] < (SMLV * 2)) && ((filaBase["con_subsidioTransporte"].ToString() == "Si")) && (salarioBase + salarioIncap + salarioHEComparar + otrosDevengado < SMLV * 2))
                //    auxTransporte = NUtilidades.redondea(AuxTransp / 30 * (diasCotizados), 1) - (auxTranspLicenciaLuto + auxTranspLicenciaMaternidad + auxTrasnpLR);
                //else
                //{
                //    if ((bool)filaBase["cli_AuxTransQuincenal"] == true)
                //        auxTransporte = 0;
                //}
                //FIN QUITAR
            }
            else
            {   //CALCULO AUX TRANSPORTE NORMAL - ES PRIMERA QUINCENA
                if (((bool)filaBase["cli_AuxTransQuincenal"] == true) && ((double)filaBase["con_salario"] <= (SMLV * 2)) && ((filaBase["con_subsidioTransporte"].ToString() == "Si")) && (salarioBase + salarioIncap + salarioHEComparar + otrosDevengado <= SMLV * 2))//enero
                    auxTransporte = NUtilidades.redondea(AuxTransp / 30 * (diasCotizados), 1) - (auxTranspLicenciaLuto + auxTranspLicenciaMaternidad + auxTrasnpLR);
                else
                    auxTransporte = 0;
            }
            #endregion

            //INICIO A CALCULAR LOS VALORES A CANCELAR
            #region CALCULO EPS
            //FABER JULIO 2017 MIENTRAS 
            double totalDevengadoPrestacional;
            if (filaBase["con_jornada"].ToString() != "Medio Tiempo")
                totalDevengadoPrestacional = salarioBase + salarioVacacion + salarioIncap + salarioLicenciaNoRemunerada + salarioSancion + salarioHExtra + otrosDevengado;
            else
            {
                //AJUSTE 2021-08-27, esperando a que llege la captiva, jejeje
                //las licencias de maternidad de medio tiempo se liquidan sobre el salario minimo
                //debo recalcular cuando le pago por dias de medio tiempo + dias de licencia maternidad al 100%
                //dias de licencia de maternidad y paternidad son iguales
                if (diasLicenciaMaternidad > 0)
                {
                    int salarioCalculo = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * (diasCotizados - diasLicenciaMaternidad), 1);

                    totalDevengadoPrestacional = (salarioCalculo / 2) + valorLicenciaMaternidad + valorLicenciaPaternidad + salarioVacacion + salarioIncap + salarioLicenciaNoRemunerada + salarioSancion + salarioHExtra + otrosDevengado;
                }
                else
                {
                    //si no hay licencia de maternidad o paternidad, dejo todo igual, ya que ha venido funcionando
                    totalDevengadoPrestacional = (salarioBase / 2) + salarioVacacion + salarioIncap + salarioLicenciaNoRemunerada + salarioSancion + salarioHExtra + otrosDevengado;
                }
            }

            salarioEPS = totalDevengadoPrestacional;
            if ((filaBase["con_jornada"].ToString() != "Tiempo Parcial") && ((filaBase["con_jornada"].ToString() != "Medio Tiempo")))
            {
                if ((bool)filaBase["cli_SegSocialQuincenal"] == true)//si cobro seguridad social en todas las quincenas
                {
                    afiEPS = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioEPS - salarioLicenciaNoRemunerada - salarioSancion) * 0.04), 100); //JULIO 2017

                    //SI DEVENGA MAS DE 10 SALARIOS MINIMOS PAGA EL 8.5 DE SALUD
                    if (Convert.ToInt32(salarioAnterior + salarioEPS - salarioLicenciaNoRemunerada - salarioSancion) >= (SMLV * 10))
                        empEPS = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAnterior + salarioEPS - salarioLicenciaNoRemunerada - salarioSancion) * 0.085), 100);
                    else
                        empEPS = Convert.ToInt32(salarioEPS * 0.0);
                }
                else
                {
                    //se cobra solo en la segunda
                    if ((Convert.ToDateTime(fechaInicio).Day == 16) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (Convert.ToDateTime(fechaFin).Day >= 16)) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (filaBase["con_fechaFin"].ToString() != "")))
                    {
                        afiEPS = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAnterior + salarioEPS - salarioLicenciaNoRemunerada - salarioSancion) * 0.04), 100); //JULIO 2017

                        //SI DEVENGA MAS DE 10 SALARIOS MINIMOS PAGA EL 8.5 DE SALUD
                        if (Convert.ToInt32(salarioAnterior + salarioEPS - salarioLicenciaNoRemunerada - salarioSancion) >= (SMLV * 10))
                            empEPS = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAnterior + salarioEPS - salarioLicenciaNoRemunerada - salarioSancion) * 0.085), 100);
                        else
                            empEPS = Convert.ToInt32(salarioEPS * 0.0);
                    }
                    else
                    {
                        afiEPS = 0;
                        empEPS = 0;
                    }
                }
            }
            else
            { //RECALCULO PARA TIEMPO PARCIAL Y MEDIO TIEMPO
                //el descuento de eps al trabajador es en funcion de los dias laborados - normal
                if (((bool)filaBase["cli_SegSocialParcial"] == false)) // || (filaBase["con_fechaFin"].ToString() != "")-->Abril 05 2021 quito que tenga encuenta la nov retiro, solicita jorge cyg
                {
                    if ((salarioEPS < SMLV) && (diasLicenciaMaternidad==0))
                    {
                        salarioEPS = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * diasEPS, 1);
                        salarioARL = salarioEPS;
                        salarioCCF = salarioEPS;
                    }
                    double aporteTotalEPS = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioEPS) * 0.04), 100);
                    //si el salarioEPS es mayor a todos los devengos, ej: trabajo solo dos dias pero debo aportar por 30 dias
                    if ((salarioEPS >= totalDevengadoPrestacional) && ((bool)filaBase["cen_ParcialSegSocial"] == false))
                    {//se calcula el aporte solo sobre lo que recibió el trabajador y el resto lo asume la empresa
                        afiEPS = NUtilidades.redondeaEncimaA(Convert.ToInt32((totalDevengadoPrestacional) * 0.04), 100);
                        empEPS = aporteTotalEPS - afiEPS;
                    }
                    else
                    {
                        if (totalDevengadoPrestacional > salarioEPS)
                        {
                            // el aporte va normal, el empleado asume el 4 y el resto la empresa
                            afiEPS = NUtilidades.redondeaEncimaA(Convert.ToInt32(totalDevengadoPrestacional * 0.04), 100);
                            empEPS = NUtilidades.redondeaEncimaA(Convert.ToInt32(totalDevengadoPrestacional * 0.0), 100);
                            salarioEPS = totalDevengadoPrestacional;
                        }
                        else
                        {
                            // el aporte va normal, el empleado asume el 4 y el resto la empresa
                            afiEPS = NUtilidades.redondeaEncimaA(Convert.ToInt32(salarioEPS * 0.04), 100);
                            empEPS = NUtilidades.redondeaEncimaA(Convert.ToInt32(salarioEPS * 0.0), 100);
                        }
                    }
                } 
                else
                {   // para el caso de comercializadora, el empleado paga el aporte sobre el minimo pleno, sin importar los dias laborados
                    // comecializadora es quincenal
                    if (salarioEPS <= SMLV) 
                    {
                        afiEPS = NUtilidades.redondeaEncimaA(((SMLV/2) * 0.04), 100);
                        empEPS = 0;
                    }
                    else
                    {// el aporte va normal, el empleado asume el 4 y el resto la empresa
                        afiEPS = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioEPS) * 0.04), 100);
                        empEPS = NUtilidades.redondeaEncimaA(Convert.ToInt32(salarioEPS * 0.0), 100);
                    }

                }

            }

            #endregion

            #region CALCULO AFP
            int afiFondoSolidaridad = 0;
            salarioAFP = salarioEPS - valorDevolucionLNR;
            //SI GANA MAS DE 4 SALARIOS MINIMOS, COTIZA AL 0.17, EL 0.01 SE VA PARA FONDO DE SOLIDARIDAD PENSIONAL
            if (filaBase["afi_AFP"].ToString() != "NA")
            {
                #region SEGURIDAD SOCIAL QUINCENAL O MENSUAL
                if ((bool)filaBase["cli_SegSocialQuincenal"] == true)
                {
                    #region TIEMPO COMPLETO
                    if ((filaBase["con_jornada"].ToString() != "Tiempo Parcial") && ((filaBase["con_jornada"].ToString() != "Medio Tiempo")))
                    {
                        //si la licencia no remunerada es por todo el mes o quincena
                        if ((salarioAFP - salarioLicenciaNoRemunerada - salarioSancion) > 0)
                        {
                            //salarioAFP = salarioAFP - salarioLicenciaNoRemunerada - salarioSancion; //----revisar
                            empAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32(salarioAFP * porAFPEmpresa), 100);
                            double salarioAFPempleado = salarioAFP;
                            afiAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32(salarioAFPempleado * porAFPEmpleado), 100);

                            #region FONDO SOLIDARIDAD
                            if ((Convert.ToDateTime(fechaInicio).Day == 16) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (Convert.ToDateTime(fechaFin).Day >= 16)) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (filaBase["con_fechaFin"].ToString() != "")))
                            {

                                //SMLV = ((SMLV / 30) * ((diasCotizados + diasNoLaborados + diasIncapcidad) + (diasLaboradosAnterior)));

                                if (((salarioAFP + salarioAnterior) < (SMLV_Calculado * 4)) || (SMLV_Calculado == 0) || ((filaBase["con_jornada"].ToString() == "Tiempo Parcial")))
                                {
                                    afiAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32(salarioAFPempleado * porAFPEmpleado), 100);
                                }
                                else
                                {
                                    if ((salarioAFP + salarioAnterior) < (SMLV * 16))
                                    {
                                        //afiAFP = Convert.ToInt32(salarioAFPempleado * 0.05); 
                                        afiFondoSolidaridad = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFPempleado + salarioAnterior) * 0.01), 100);
                                    }
                                    else
                                    {
                                        if ((salarioAFP + salarioAnterior) < (SMLV * 17))
                                        {
                                            //afiAFP = Convert.ToInt32(salarioAFPempleado * 0.062);
                                            afiFondoSolidaridad = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFPempleado + salarioAnterior) * 0.012), 100);
                                        }
                                        else
                                        {
                                            if ((salarioAFP + salarioAnterior) < (SMLV * 18))
                                            {
                                                //afiAFP = Convert.ToInt32(salarioAFPempleado * 0.064);
                                                afiFondoSolidaridad = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFPempleado + salarioAnterior) * 0.014), 100);
                                            }
                                            else
                                            {
                                                if ((salarioAFP + salarioAnterior) < (SMLV * 19))
                                                {
                                                    //afiAFP = Convert.ToInt32(salarioAFPempleado * 0.066);
                                                    afiFondoSolidaridad = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFPempleado + salarioAnterior) * 0.016), 100);
                                                }
                                                else
                                                {
                                                    if ((salarioAFP + salarioAnterior) < (SMLV * 20))
                                                    {
                                                        //afiAFP = Convert.ToInt32(salarioAFPempleado * 0.068);
                                                        afiFondoSolidaridad = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFPempleado + salarioAnterior) * 0.018), 100);
                                                    }
                                                    else
                                                    {
                                                        //afiAFP = Convert.ToInt32(salarioAFPempleado * 0.07);
                                                        afiFondoSolidaridad = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFPempleado + salarioAnterior) * 0.02), 100);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }
                        else {//cuando la licencia no remunerada es por toda la quincena o todo el mes
                            empAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32(salarioAFP * (porAFPEmpleado + porAFPEmpresa)), 100);
                            double salarioAFPempleado = salarioAFP;
                            afiAFP = 0;
                        }
                    }
                    #endregion

                    #region TIEMPO PARCIAL Y MEDIO TIEMPO
                    else
                    {   //2021-03 aportes segun los dias laborados
                        double aporteTotalAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFP) * (porAFPEmpleado + porAFPEmpresa)), 100);
                        if (((bool)filaBase["cli_SegSocialParcial"] == false))// || (filaBase["con_fechaFin"].ToString() != "") abril 5/2021 quito nov retiro solicita jorge
                        {
                            //si el salarioAFP es mayor a todos los devengos, ej: trabajo solo dos dias pero debo aportar por 30 dias
                            if ((salarioAFP > totalDevengadoPrestacional) && ((bool)filaBase["cen_ParcialSegSocial"] == false))
                            {//se calcula el aporte solo sobre lo que recibió el trabajador y el resto lo asume la empresa
                                afiAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32(totalDevengadoPrestacional * porAFPEmpleado), 100);
                                empAFP = aporteTotalAFP - afiAFP;
                            }
                            else
                            {// el aporte va normal, el empleado asume el 4 y el resto la empresa
                                afiAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32(salarioAFP * porAFPEmpleado), 100);
                                empAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32(salarioAFP * porAFPEmpresa), 100);
                            }
                        }
                        else 
                        { //caso comercializadora cyg - el descuento al empleado siempre va sobre el salario minimo, SIEMPRE ES QUINCENAL
                            afiAFP = NUtilidades.redondeaEncimaA(((SMLV/2) * porAFPEmpleado), 100);
                            empAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32((SMLV / 2) * porAFPEmpresa), 100);
                        }
                    }
                    #endregion
                }
                #endregion

                #region SEG SOCIAL SOLO EN LA SEGUNDA QUINCENA
                else
                {
                    if ((Convert.ToDateTime(fechaInicio).Day == 16) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (Convert.ToDateTime(fechaFin).Day >= 16)) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (filaBase["con_fechaFin"].ToString() != "")))
                    {
                        #region TIEMPO COMPLETO
                        if ((filaBase["con_jornada"].ToString() != "Tiempo Parcial") && ((filaBase["con_jornada"].ToString() != "Medio Tiempo")))
                        {
                            salarioAFP = salarioAFP - salarioLicenciaNoRemunerada - salarioSancion;
                            empAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAnterior + salarioAFP) * porAFPEmpresa), 100);
                            double salarioAFPempleado = salarioAFP;
                            afiAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAnterior + salarioAFPempleado) * porAFPEmpleado), 100);

                            #region FONDO SOLIDARIDAD
                            if ((Convert.ToDateTime(fechaInicio).Day == 16) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (Convert.ToDateTime(fechaFin).Day >= 16)) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (filaBase["con_fechaFin"].ToString() != "")))
                            {
                                //SMLV = ((SMLV / 30) * ((diasCotizados + diasNoLaborados + diasIncapcidad) + (diasLaboradosAnterior)));

                                if (((salarioAFP + salarioAnterior) < (SMLV_Calculado * 4)) || (SMLV_Calculado == 0) || ((filaBase["con_jornada"].ToString() == "Tiempo Parcial")))
                                {
                                    afiAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAnterior + salarioAFPempleado) * porAFPEmpleado), 100);
                                }
                                else
                                {
                                    if ((salarioAFP + salarioAnterior) < (SMLV * 16))
                                    {
                                        //afiAFP = Convert.ToInt32(salarioAFPempleado * 0.05); 
                                        afiFondoSolidaridad = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFPempleado + salarioAnterior) * 0.01), 100);
                                    }
                                    else
                                    {
                                        if ((salarioAFP + salarioAnterior) < (SMLV * 17))
                                        {
                                            //afiAFP = Convert.ToInt32(salarioAFPempleado * 0.062);
                                            afiFondoSolidaridad = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFPempleado + salarioAnterior) * 0.012), 100);
                                        }
                                        else
                                        {
                                            if ((salarioAFP + salarioAnterior) < (SMLV * 18))
                                            {
                                                //afiAFP = Convert.ToInt32(salarioAFPempleado * 0.064);
                                                afiFondoSolidaridad = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFPempleado + salarioAnterior) * 0.014), 100);
                                            }
                                            else
                                            {
                                                if ((salarioAFP + salarioAnterior) < (SMLV * 19))
                                                {
                                                    //afiAFP = Convert.ToInt32(salarioAFPempleado * 0.066);
                                                    afiFondoSolidaridad = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFPempleado + salarioAnterior) * 0.016), 100);
                                                }
                                                else
                                                {
                                                    if ((salarioAFP + salarioAnterior) < (SMLV * 20))
                                                    {
                                                        //afiAFP = Convert.ToInt32(salarioAFPempleado * 0.068);
                                                        afiFondoSolidaridad = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFPempleado + salarioAnterior) * 0.018), 100);
                                                    }
                                                    else
                                                    {
                                                        //afiAFP = Convert.ToInt32(salarioAFPempleado * 0.07);
                                                        afiFondoSolidaridad = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAFPempleado + salarioAnterior) * 0.02), 100);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }
                        #endregion

                        #region TIEMPO PARCIAL Y MEDIO TIEMPO
                        else
                        {//RECALCULO PARA TIEMPO PARCIAL
                            salarioAFP = salarioAFP - salarioLicenciaNoRemunerada - salarioSancion;
                            double aporteTotalAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAnterior + salarioAFP) * (porAFPEmpleado + porAFPEmpresa)), 100);
                            //si el salarioAFP es mayor a todos los devengos, ej: trabajo solo dos dias pero debo aportar por 30 dias
                            if ((salarioAFP >= totalDevengadoPrestacional) && ((bool)filaBase["cen_ParcialSegSocial"] == false))
                            {//se calcula el aporte solo sobre lo que recibió el trabajador y el resto lo asume la empresa
                                afiAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32((totalDevengadoPrestacional + salarioAnterior) * porAFPEmpleado), 100);
                                empAFP = aporteTotalAFP - afiAFP;
                            }
                            else
                            {// el aporte va normal, el empleado asume el 4 y el resto la empresa
                                afiAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAnterior + salarioAFP) * porAFPEmpleado), 100);
                                empAFP = NUtilidades.redondeaEncimaA(Convert.ToInt32((salarioAnterior + salarioAFP) * porAFPEmpresa), 100);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        empAFP = 0;
                        afiAFP = 0;
                    }
                }
                #endregion
            }//FIN CALCULAR AFP, SI VIENE CON AFP
            else
            {
                empAFP = 0;
                afiAFP = 0;
            }
            #endregion

            #region CALCULO ARL Y PARAFISCALES
            if ((bool)filaBase["cli_SegSocialQuincenal"] == true)
            {
                #region SEG SOCIAL CADA QUINCENA
                if (((bool)filaBase["cli_SegSocialParcial"] == false) || (filaBase["con_jornada"].ToString() == "Tiempo Completo"))
                {
                    empARL = NUtilidades.redondeaEncimaA((salarioARL) * Convert.ToDouble(filaBase["niv_valor"]), 100);
                    empCCF = NUtilidades.redondeaEncimaA((salarioCCF) * 0.04, 100);
                    //SI DEVENGA MAS DE 10 SALARIOS MINIMOS PAGA EL 8.5 DE SALUD
                    if (Convert.ToInt32(salarioEPS - salarioLicenciaNoRemunerada - salarioSancion) < (SMLV * 10))
                        empPARAF = NUtilidades.redondeaEncimaA((salarioCCF) * 0.0, 100);
                    else
                        empPARAF = NUtilidades.redondeaEncimaA((salarioCCF) * 0.05, 100);
                }
                else
                {   //2021-03 - caso comercializadora cyg - siempre es quincenal 
                    empARL = NUtilidades.redondeaEncimaA((SMLV/2) * Convert.ToDouble(filaBase["niv_valor"]), 100);
                    empCCF = NUtilidades.redondeaEncimaA((SMLV/2) * 0.04, 100);
                    empPARAF = 0;
                }
                #endregion
            }
            else
            {
                #region SEG SOCIAL SOLO EN LA SEGUNDA QUINCENA
                if ((Convert.ToDateTime(fechaInicio).Day == 16) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (Convert.ToDateTime(fechaFin).Day >= 16)) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (filaBase["con_fechaFin"].ToString() != "")))
                {
                    empARL = NUtilidades.redondeaEncimaA((salarioAnterior + salarioARL) * Convert.ToDouble(filaBase["niv_valor"]), 100);
                    empCCF = NUtilidades.redondeaEncimaA((salarioAnterior + salarioCCF) * 0.04, 100);
                    //SI DEVENGA MAS DE 10 SALARIOS MINIMOS PAGA EL 8.5 DE SALUD
                    if (Convert.ToInt32(salarioAnterior + salarioEPS - salarioLicenciaNoRemunerada - salarioSancion) < (SMLV * 10))
                        empPARAF = NUtilidades.redondeaEncimaA((salarioAnterior + salarioCCF) * 0.0, 100);
                    else
                        empPARAF = NUtilidades.redondeaEncimaA((salarioAnterior + salarioCCF) * 0.05, 100);
                }
                else
                {
                    empARL = 0;
                    empCCF = 0;
                    empPARAF = 0;
                }
                #endregion
            }

            #endregion

            #region RETEFUENTE
            if ((Convert.ToDateTime(fechaInicio).Day == 16) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (Convert.ToDateTime(fechaFin).Day >= 16)) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (filaBase["con_fechaFin"].ToString() != "")))
            {
                double deduccionRteFte = 0;
                #region BUSCAR DEDUCCIONES DE RTE FUENTE
                DataTable tbDeducciones = dNomina.deducRteConsultarVigentes(filaBase["ID_afiliado"].ToString(), Convert.ToDateTime(fechaFin).Year);
                if ((tbDeducciones.Rows.Count > 0) && (tbDeducciones.Rows[0]["ded_valor"].ToString() != "") && (Convert.ToDouble(tbDeducciones.Rows[0]["ded_valor"]) > 0))
                    deduccionRteFte = Convert.ToDouble(tbDeducciones.Rows[0]["ded_valor"]);
                else
                {
                    //Si no hay registros de deducciones del año en curso y estoy liquidando enero, febrero o marzo, busca los registros de deduccion del año pasado.
                    if (Convert.ToDateTime(fechaFin).Month <= 3)
                    {
                        tbDeducciones = dNomina.deducRteConsultarVigentes(filaBase["ID_afiliado"].ToString(), Convert.ToDateTime(fechaFin).AddYears(-1).Year);
                        if ((tbDeducciones.Rows.Count > 0) && (tbDeducciones.Rows[0]["ded_valor"].ToString() != "") && (Convert.ToDouble(tbDeducciones.Rows[0]["ded_valor"]) > 0))
                            deduccionRteFte = Convert.ToDouble(tbDeducciones.Rows[0]["ded_valor"]);
                    }
                }
                #endregion

                double baseRteFte = (salarioBase + salarioIncap + salarioVacacion + auxTransporte + otrosDevengado + salarioAnterior) - afiEPS - afiAFP - afiFondoSolidaridad - epsAnterior - afpAnterior - deduccionRteFte;
                baseRteFte = baseRteFte - NUtilidades.redondeaEncimaA((baseRteFte * 0.25), 1000);

                int valorUTV = NUtilidades.consultarUVTXaño(Convert.ToDateTime(fechaFin).Year);
                double baseEnUVT = Convert.ToDouble(baseRteFte / valorUTV);
                if (baseEnUVT <= 95)
                    reteFuente = 0;
                else
                {
                    if (baseEnUVT <= 150)
                        reteFuente = NUtilidades.redondea(Convert.ToInt32(((baseEnUVT - 95) * 0.19) * valorUTV), 1000);
                    else
                    {
                        if (baseEnUVT <= 360)
                            reteFuente = NUtilidades.redondea(Convert.ToInt32(((baseEnUVT - 150) * 0.28) * valorUTV + (10 * valorUTV)), 1000);
                        else
                            reteFuente = NUtilidades.redondea(Convert.ToInt32(((baseEnUVT - 360) * 0.33) * valorUTV + (69 * valorUTV)), 1000);
                    }
                }
            }
            #endregion

            //OTROS DEVENGOS PRESTACIONALES SON TODO LO QUE NO ES SALARIO NI AUX TRANSPORTE
            otrosDevengado += Convert.ToInt32(salarioHExtra);
            if ((bool)filaBase["cen_HEsobreComision"] == true)
                dvSalario = NUtilidades.redondea(((double)filaBase["con_salario"] / 30) * diasCotizados, 1);
            else
            {
                if (filaBase["con_jornada"].ToString() == "Medio Tiempo")
                {
                    if (diasLicenciaMaternidad == 0)
                        dvSalario = dvSalario - salarioLicenciaNoRemunerada - salarioSancion;
                    else
                    {
                        //ajuste 2021-08-27 - licencia de maternidad-paternidad medio tiempo
                        dvSalario = totalDevengadoPrestacional - otrosDevengado - salarioLicenciaNoRemunerada - salarioSancion;
                    }
                }
                else
                    dvSalario = dvSalario - salarioLicenciaNoRemunerada - salarioSancion;
            }

            //CUANTO GANO DE SUELDO, SALARIO DEVENGADO 
            if (filaBase["con_jornada"].ToString() == "Medio Tiempo")
            {
                //como la licencia de maternidad-paternidad se liquida al 100, para medio tiempo hago ajuste - 2021-08-27
                if(diasLicenciaMaternidad==0)
                    totalDevengado = (salarioBase / 2) + salarioIncap + salarioVacacion + auxTransporte + otrosDevengado + otrosDevengadoNoPrestacional;
                else
                    totalDevengado = totalDevengadoPrestacional + auxTransporte + otrosDevengadoNoPrestacional;
            }
            else //Tiempo Completo y Jornada Flexible
                totalDevengado = salarioBase + salarioIncap + salarioVacacion + auxTransporte + otrosDevengado + otrosDevengadoNoPrestacional;

            #region PROVISIONES
            double nom_ProvPrima = 0;
            double nom_ProvCesantias;
            double nom_ProvIntereses;
            double nom_ProvVacaciones;
            double nom_ProvTotal;
            double factorPrestacional = 0.0833333;
            if ((bool)filaBase["cli_RedondeoProvision"] == true)
                factorPrestacional = 0.0833;
            //si el cliente provisiona
            if ((bool)filaBase["cen_provisiona"] == true)
            {
                if ((bool)filaBase["cli_SegSocialQuincenal"] == true)
                {
                    //si la licencia no remunerada es por todo el mes o quincena
                    if (((salarioAFP - salarioLicenciaNoRemunerada - salarioSancion) > 0))// || (ID_empresaUsuariaFK=="2096"))
                    {
                        //septiembre 6 2021, solicitan que las incapcidades y licencias (domenico) si se tengan en presentes en la provision, se volvio darez - email don jorge
                        //septiembre 22 2021, vuelvo y lo dejo como estaba a solicitud de don jorge
                        if ((bool)filaBase["cli_SegSocialParcial"] == false)
                            nom_ProvPrima = Convert.ToInt32((dvSalario + otrosDevengado + auxTransporte + salarioSancion + salarioLicenciaNoRemunerada) * factorPrestacional);
                        else
                            nom_ProvPrima = Convert.ToInt32((dvSalario + otrosDevengado + auxTransporte + salarioSancion + salarioLicenciaNoRemunerada + salarioIncap ) * factorPrestacional);
                        //Enero 2021, modifico para cyg, caso domenico
                        if ((bool)filaBase["cli_LicenciaAfectaProvision"] == false)
                        {
                            //abril 2021, ajuste para caso comercializadora anaya - cyg
                            if ((bool)filaBase["cli_SegSocialParcial"] == false)
                            {
                                nom_ProvCesantias = Convert.ToInt32((dvSalario + otrosDevengado + auxTransporte + salarioSancion + salarioLicenciaNoRemunerada) * factorPrestacional);
                                nom_ProvIntereses = Convert.ToInt32((((dvSalario + otrosDevengado + auxTransporte + salarioSancion + salarioLicenciaNoRemunerada) * factorPrestacional) * 0.12));
                                nom_ProvVacaciones = Convert.ToInt32((dvSalario + otrosDevengado + salarioSancion + salarioLicenciaNoRemunerada) * Convert.ToDouble(filaBase["cli_ProvVacaciones"]));//0.0417 Mayo - Parametrizar por cliente
                            }
                            else
                            {//abril 2021, ajuste para caso comercializadora anaya - cyg - incluye el salarioIncap en la provision
                                nom_ProvCesantias = Convert.ToInt32((dvSalario + otrosDevengado + auxTransporte + salarioSancion + salarioLicenciaNoRemunerada + salarioIncap) * factorPrestacional);
                                nom_ProvIntereses = Convert.ToInt32((((dvSalario + otrosDevengado + auxTransporte + salarioSancion + salarioLicenciaNoRemunerada + salarioIncap) * factorPrestacional) * 0.12));
                                nom_ProvVacaciones = Convert.ToInt32((dvSalario + otrosDevengado + salarioSancion + salarioLicenciaNoRemunerada + salarioIncap) * Convert.ToDouble(filaBase["cli_ProvVacaciones"]));//0.0417 Mayo - Parametrizar por cliente
                            }
                        }
                        else
                        {
                            nom_ProvCesantias = Convert.ToInt32((dvSalario + otrosDevengado + auxTransporte + salarioIncap) * factorPrestacional);
                            nom_ProvIntereses = Convert.ToInt32((((dvSalario + otrosDevengado + auxTransporte + salarioIncap) * factorPrestacional) * 0.12));
                            nom_ProvVacaciones = Convert.ToInt32((dvSalario + otrosDevengado + salarioIncap) * Convert.ToDouble(filaBase["cli_ProvVacaciones"]));//0.0417 Mayo - Parametrizar por cliente
                        }
                        
                        if ((System.Configuration.ConfigurationSettings.AppSettings["ProvisionVacacionCCF"].ToString() == "SI") && (((bool)filaBase["cli_SegSocialParcial"] == false)))
                        {
                            //salarioCCF += nom_ProvVacaciones;//Noviembre 2019, salarioCCF sale en el plano tal cual, modifico acá para que se cobre a la usuaria, pero me salga bien en el plano
                            empCCF = NUtilidades.redondeaEncimaA((salarioCCF - valorVacacionesEnRetiro + nom_ProvVacaciones) * 0.04, 100);
                        }
                        
                        nom_ProvTotal = Convert.ToInt32(nom_ProvPrima + nom_ProvCesantias + nom_ProvIntereses + nom_ProvVacaciones);
                    }
                    else
                    {
                        //enero 18 2022 - si tiene LNR de toda la quincena o mes, se provisiona solo la prima
                        nom_ProvPrima = Convert.ToInt32((dvSalario + otrosDevengado + auxTransporte + salarioSancion + salarioLicenciaNoRemunerada) * factorPrestacional);
                        nom_ProvCesantias = 0;
                        nom_ProvIntereses = 0;
                        nom_ProvVacaciones = 0;
                        nom_ProvTotal = nom_ProvPrima;
                    }
                }
                else
                {
                    if ((Convert.ToDateTime(fechaInicio).Day == 16) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (Convert.ToDateTime(fechaFin).Day >= 16)) || ((Convert.ToDateTime(fechaInicio).Day == 1) && (filaBase["con_fechaFin"].ToString() != "")))
                    {
                        if ((salarioAnterior + salarioAFP - salarioLicenciaNoRemunerada - salarioSancion) > 0)
                        {
                            if ((bool)filaBase["cli_LicenciaAfectaProvision"] == false)
                            {
                                nom_ProvPrima = Convert.ToInt32((salarioAnterior + dvSalario + otrosDevengado + auxTransporte + salarioSancion + salarioLicenciaNoRemunerada - valorDevolucionLNR) * 0.0833333);
                                nom_ProvCesantias = Convert.ToInt32((salarioAnterior + dvSalario + otrosDevengado + auxTransporte + salarioSancion + salarioLicenciaNoRemunerada - valorDevolucionLNR) * 0.0833333);
                                nom_ProvIntereses = Convert.ToInt32((((salarioAnterior + dvSalario + otrosDevengado + auxTransporte + salarioSancion + salarioLicenciaNoRemunerada - valorDevolucionLNR) * 0.0833333) * 0.12));
                                nom_ProvVacaciones = Convert.ToInt32((salarioAnterior + dvSalario + otrosDevengado + salarioSancion + salarioLicenciaNoRemunerada - valorDevolucionLNR) * Convert.ToDouble(filaBase["cli_ProvVacaciones"]));//0.0417 Mayo - Parametrizar por cliente
                            }
                            else
                            {
                                nom_ProvPrima = Convert.ToInt32((salarioAnterior + dvSalario + otrosDevengado + auxTransporte) * 0.0833333);
                                nom_ProvCesantias = Convert.ToInt32((salarioAnterior + dvSalario + otrosDevengado + auxTransporte) * 0.0833333);
                                nom_ProvIntereses = Convert.ToInt32((((salarioAnterior + dvSalario + otrosDevengado + auxTransporte) * 0.0833333) * 0.12));
                                nom_ProvVacaciones = Convert.ToInt32((salarioAnterior + dvSalario + otrosDevengado) * Convert.ToDouble(filaBase["cli_ProvVacaciones"]));//0.0417 Mayo - Parametrizar por cliente
                            }

                            if (System.Configuration.ConfigurationSettings.AppSettings["ProvisionVacacionCCF"].ToString() == "SI")
                            {
                                //salarioCCF += nom_ProvVacaciones;//Noviembre 2019, salarioCCF sale en el plano tal cual, modifico acá para que se cobre a la usuaria, pero me salga bien en el plano
                                empCCF = NUtilidades.redondeaEncimaA((salarioAnterior + salarioCCF - valorVacacionesEnRetiro + nom_ProvVacaciones) * 0.04, 100);
                            }

                            nom_ProvTotal = Convert.ToInt32(nom_ProvPrima + nom_ProvCesantias + nom_ProvIntereses + nom_ProvVacaciones);
                        }
                        else
                        {
                            nom_ProvCesantias = 0;
                            nom_ProvIntereses = 0;
                            nom_ProvVacaciones = 0;
                            nom_ProvTotal = 0;
                        }
                    }
                    else
                    {
                        nom_ProvCesantias = 0;
                        nom_ProvIntereses = 0;
                        nom_ProvVacaciones = 0;
                        nom_ProvTotal = 0;
                    }
                }
            }
            else
            { //el cliente no provisiona
                nom_ProvCesantias = 0;
                nom_ProvIntereses = 0;
                nom_ProvVacaciones = 0;
                nom_ProvTotal = 0;
            }
            #endregion

            #region PFP - PRESTAMO FONDO PRESTACIONAL
            if (filaBase["con_pfp"].ToString() == "Si")
                totalDevengado += nom_ProvTotal;
            #endregion

            totalPagar = totalDevengado - afiEPS - afiAFP - afiFondoSolidaridad - otrosDescuento - reteFuente;

            #region RETIRO EXTEMPORANEO
            //Marzo 15 2020 - Validar si es un retiro extemporaneo
            if ((filaBase["con_fechaFin"].ToString() != "") && (tbNewNovedades != null) && (tbNewNovedades.Rows.Count > 0))
            {
                DataRow[] rowNovedad;
                rowNovedad = tbNewNovedades.Select("nov_tipoNovedadFK='Retiro'", "");
                if (rowNovedad.Length > 0)
                {
                    if (rowNovedad[0]["nov_observacion"].ToString() == "Retiro Extemporaneo")
                    {
                        DataTable tbNomBaseRetiro = ConsultarXidNomBase(idNominaBase);
                        DataRow drBase = tbNomBaseRetiro.Rows[0];
                        //tengo la copia de como venia, comparar valores de lo que pagué de más

                        //calculo los valores a ajustar

                        //inserto la nomina de ajuste
                        Int64 idNominaAjuste = NUtilidades.IdentificadorNomina(ID_empresaTemporal);

                        //salarioCCF de esta nomina, menos la anterior, porque como se retira debo cobrar el incremento de vacaciones en ccf
                        dNomina.Insertar(idNominaAjuste, ID_empresaTemporal, ID_empresaUsuariaFK, "RetiroExtemporaneo", string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(fechaInicio)), string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(fechaFin)), "POR REVISAR", ID_usuarioRegistraFK, false);
                        DataTable tbNomBaseAjuste = dNomina.InsertarBase("0", idNominaAjuste.ToString(), ID_empresaTemporal, ID_empresaUsuariaFK, filaBase["conID_centroCostoFK"].ToString(), filaBase["ID_contrato"].ToString(), filaBase["ID_afiliado"].ToString(),
                                         filaBase["afi_cuentaNumero"].ToString(), filaBase["ID_bancoFK"].ToString(), filaBase["nombreCuenta"].ToString(), filaBase["afi_EPS"].ToString(), filaBase["afi_AFP"].ToString(), filaBase["afi_CCF"].ToString(), filaBase["con_ARP"].ToString(), filaBase["ID_nivelRiesgo"].ToString(), (double)filaBase["con_salario"],
                                         (double)tbNomBaseRetiro.Rows[0]["nom_salarioBase"] - salarioBase, (int)tbNomBaseRetiro.Rows[0]["nom_dias"] - diasCotizados,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_salarioDevengado"] - salarioDevengado, (double)tbNomBaseRetiro.Rows[0]["nom_salarioEPS"] - salarioEPS,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_salarioAFP"] - salarioAFP, salarioCCF - (double)tbNomBaseRetiro.Rows[0]["nom_salarioCCF"],
                                         (double)tbNomBaseRetiro.Rows[0]["nom_salarioARL"] - salarioARL, (double)tbNomBaseRetiro.Rows[0]["nom_dvSalario"] - dvSalario,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_dvAuxilioTransporte"] - auxTransporte, (double)tbNomBaseRetiro.Rows[0]["nom_dvIncapacidad"] - salarioIncap,
                                         (int)tbNomBaseRetiro.Rows[0]["nom_diasIncapacidad"] - diasIncapcidad, (double)tbNomBaseRetiro.Rows[0]["nom_dvOtros"] - otrosDevengado,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_dvOtrosNoPrestacional"] - otrosDevengadoNoPrestacional,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_ddEPSafiliado"] - afiEPS, (double)tbNomBaseRetiro.Rows[0]["nom_ddAFPafiliado"] - afiAFP,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_ddFSPafiliado"] - afiFondoSolidaridad, 0, 0, 0, (double)tbNomBaseRetiro.Rows[0]["nom_ddReteFuente"] - reteFuente,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_ddOtros"] - otrosDescuento, (double)tbNomBaseRetiro.Rows[0]["nom_EPSempresa"] - empEPS,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_AFPempresa"] - empAFP, (double)tbNomBaseRetiro.Rows[0]["nom_ARLempresa"] - empARL,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_CCFempresa"] - empCCF, (double)tbNomBaseRetiro.Rows[0]["nom_PARAFempresa"] - empPARAF,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_TotalPagar"] - totalPagar, ID_usuarioRegistraFK, 0,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_ProvPrima"] - nom_ProvPrima, (double)tbNomBaseRetiro.Rows[0]["nom_ProvCesantias"] - nom_ProvCesantias,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_ProvIntereses"] - nom_ProvIntereses, (double)tbNomBaseRetiro.Rows[0]["nom_ProvVacaciones"] - nom_ProvVacaciones,
                                         (double)tbNomBaseRetiro.Rows[0]["nom_ProvTotal"] - nom_ProvTotal, false,0);
                    }
                }
            }
            #endregion

            #region GUARDO O ACTUALIZO
            DataTable tbNomBase = dNomina.InsertarBase(idNominaBase, idNomina.ToString(), ID_empresaTemporal, ID_empresaUsuariaFK, filaBase["conID_centroCostoFK"].ToString(), filaBase["ID_contrato"].ToString(), filaBase["ID_afiliado"].ToString(),
                                     filaBase["afi_cuentaNumero"].ToString(), filaBase["ID_bancoFK"].ToString(), filaBase["nombreCuenta"].ToString(),
                                     filaBase["afi_EPS"].ToString(), filaBase["afi_AFP"].ToString(), filaBase["afi_CCF"].ToString(), filaBase["con_ARP"].ToString(),
                                     filaBase["ID_nivelRiesgo"].ToString(), (double)filaBase["con_salario"], salarioBase, diasCotizados, salarioDevengado, salarioEPS, salarioAFP, salarioCCF, salarioARL,
                                     dvSalario, auxTransporte, salarioIncap, diasIncapcidad, otrosDevengado, otrosDevengadoNoPrestacional,
                                     afiEPS, afiAFP, afiFondoSolidaridad, 0, 0, 0, reteFuente, otrosDescuento, empEPS, empAFP, empARL, empCCF, empPARAF, totalPagar, ID_usuarioRegistraFK, diasNoLaborados, 
                                     nom_ProvPrima, nom_ProvCesantias, nom_ProvIntereses, nom_ProvVacaciones, nom_ProvTotal, nom_IndividualLiquidacion, comision);

            if ((idNominaBase == "0") && (tbNomBase.Rows.Count > 0))
                idNominaBase = tbNomBase.Rows[0]["ID_nominaBase"].ToString();
            //no se por que se descuadra con licencia de paternidad
            if ((diasLicenciaMaternidad > 0) && (valorLicenciaMaternidad == 0))
                diasLicenciaMaternidad = 0;
                
            dNomina.InsertarReporte(idNominaBase, idNomina.ToString(), ID_empresaTemporal, ID_empresaUsuariaFK, filaBase["conID_centroCostoFK"].ToString(), filaBase["ID_contrato"].ToString(), diasEPS, diasAFP, diasARP, diasCCF, diasEPS, diasCotizados, diasIncapGeneral, valorIncapGeneral, diasIncapSoat, valorIncapSoat,
                    diasIncapARL, valorIncapARL, diasLicenciaMaternidad, valorLicenciaMaternidad, diasLicenciaPaternidad, valorLicenciaPaternidad, diasLNR, valorLNR, diasLR, valorLR, diasLicenciaLuto, valorLicenciaLuto, diasVacacion, valorVacacion, diasSancion, valorSancion, cantHED, valorHED, cantHEN, valorHEN, cantHEDD, valorHEDD,
                    cantHEND, valorHEND, cantHDDO, valorHDDO, cantHDDOR, valorHDDOR, cantHNDOR, valorHNDOR, cantRN, valorRN, cantRND, valorRND, diasDevolucionLNR, valorDevolucionLNR);
            
            #endregion

            return idNominaBase;
        }

        public DataTable ConsolidarNominasIndividuales(string ID_nominaFK, string ID_empresaTemporal, string ID_empresaUsuariaFK, string ID_centroCosto, string nom_tipoNomina, string fechaInicio, string fechaFin, string cli_tipoFacturacion)
        {
            return dNomina.ConsolidarNominasIndividuales(ID_nominaFK, ID_empresaTemporal, ID_empresaUsuariaFK, ID_centroCosto, nom_tipoNomina, fechaInicio, fechaFin, cli_tipoFacturacion);
        }

        public void ReliquidarNomina(Int64 ID_nomina)
        {
            DataTable tbAfiliados = dNomina.ConsultarXID(ID_nomina);
            if (tbAfiliados.Rows.Count > 0)
            {
                double SMLV = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(tbAfiliados.Rows[0]["nom_fechaFin"]).Year);
                double AuxTransp = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(tbAfiliados.Rows[0]["nom_fechaFin"]).Year, true);
                string novedadesDescuento = dNomina.novFiltroDescuentos();
                string novedadesDevengo = dNomina.novFiltroDevengos();

                //SI HAY AFILIADOS SIN NOMINA, CALCULO Y LUEGO INSERTO LA NOMINA
                for (int i = 0; i < tbAfiliados.Rows.Count; i++)
                {
                    calcularNominaBase(ID_nomina.ToString(), tbAfiliados.Rows[i], tbAfiliados.Rows[i]["ID_empresaTemporalFK"].ToString(), tbAfiliados.Rows[i]["ID_empresaUsuariaFK"].ToString()
                        , tbAfiliados.Rows[i]["ID_centroCostoFK"].ToString(), string.Format("{0:yyyy-MM-dd}", tbAfiliados.Rows[i]["nom_fechaInicio"]), string.Format("{0:yyyy-MM-dd}", tbAfiliados.Rows[i]["nom_fechaFin"])
                        , SMLV, AuxTransp, "1", novedadesDescuento, novedadesDevengo, tbAfiliados.Rows[i]["ID_nominaBase"].ToString());

                }//FIN CALCULAR POR EMPLEADO
            }
        }

        public void EliminaNominaBase(string ID_nominaBase)
        {
            dNomina.EliminaNominaBase(ID_nominaBase);
        }

        public void NoGirar(string ID_nominaBase, bool nom_noGirar)
        {
            dNomina.NoGirar(ID_nominaBase, nom_noGirar);
        }

        public DataTable Unificar(string ID_nomina1, string ID_nomina2, string ID_usuarioRegistraFK)
        {
            return dNomina.Unificar(ID_nomina1, ID_nomina2, ID_usuarioRegistraFK);
        }

        public DataTable Eliminar(string ID_nomina, string ID_usuarioRegistraFK)
        {
            return dNomina.Eliminar(ID_nomina, ID_usuarioRegistraFK);
        }

        public void HistoricoInsertar(string ID_nomina, string ID_nominaUnificada, string ID_usuarioRegistraFK, string his_tipoMovimiento)
        {
            dNomina.HistoricoInsertar(ID_nomina, ID_nominaUnificada, ID_usuarioRegistraFK, his_tipoMovimiento);
        }

        public DataTable ActualizaEstado(string ID_nomina, string nom_observacion, string nom_estado, string ID_usuarioActualiza)
        {
            return dNomina.ActualizaEstado(ID_nomina, nom_observacion, nom_estado, ID_usuarioActualiza);
        }

        public DataTable ApruebaUsuaria(string ID_nomina, string nom_observacion, string nom_fechaApruebaDesembolso, string ID_usuarioActualiza)
        {
            return dNomina.ApruebaUsuaria(ID_nomina, nom_observacion, nom_fechaApruebaDesembolso, ID_usuarioActualiza);
        }

        public DataTable nom_ActualizaSoportePago(string ID_nomina, string nom_tipoSoporte, string nom_soportePago, string nom_observacionSoporte, string ID_usuarioSoportePago)
        {
            return dNomina.nom_ActualizaSoportePago(ID_nomina, nom_tipoSoporte, nom_soportePago, nom_observacionSoporte, ID_usuarioSoportePago);
        }

        public void ActualizaNomBaseObservacionUsuaria(string ID_nominaBase, string nom_observacionUsuria, string ID_usuarioObservacionUsuariaFK)
        {
            dNomina.ActualizaNomBaseObservacionUsuaria(ID_nominaBase, nom_observacionUsuria, ID_usuarioObservacionUsuariaFK);
        }

        public DataTable MigracionAcumulados(string ID_nomina, double totalIngresos, double totalAuxilio, double totalHorasExtra, double salarioAnterior)
        {
            return dNomina.MigracionAcumulados(ID_nomina, totalIngresos, totalAuxilio, totalHorasExtra, salarioAnterior);
        }

        public DataTable ActualizaGiroEstado(string ID_nominaBase, bool nom_estadoGiro, string nom_planoGiro, string nom_pdfGiro, string nom_fechaPagoGiro, string ID_usuarioRegistraGiroFK, string nom_bancoExitoso)
        {
            return dNomina.ActualizaGiroEstado(ID_nominaBase, nom_estadoGiro, nom_planoGiro, nom_pdfGiro, nom_fechaPagoGiro, ID_usuarioRegistraGiroFK, nom_bancoExitoso);
        }

        public DataTable ActualizaGiroPlano(string ID_nominaBase, string ID_usuarioRegistraGiroFK, string nom_bancoPlano)
        {
            return dNomina.ActualizaGiroPlano(ID_nominaBase, ID_usuarioRegistraGiroFK, nom_bancoPlano);
        }

        public DataTable nom_VerificarPagoOK(string ID_nomina)
        {
            return dNomina.nom_VerificarPagoOK(ID_nomina);
        }

        public DataTable Consultar(string nom_tipoNomina, string where)
        {
            return dNomina.Consultar(nom_tipoNomina, where);
        }

        public DataTable ConsultaEstadosXPerfil(string estados)
        {
            return dNomina.ConsultaEstadosXPerfil(estados);
        }

        public DataTable ConsultaEstadosLiquidacionXPerfil(string estados)
        {
            return dNomina.ConsultaEstadosLiquidacionXPerfil(estados);
        }

        public DataTable ConsultarXID(double ID_nomina)
        {
            return dNomina.ConsultarXID(ID_nomina);
        }

        public DataTable ConsultarXIDXBancoDispersion(double ID_nomina, string bancoOrigen, string bancoDestino)
        {
            return dNomina.ConsultarXIDXBancoDispersion(ID_nomina, bancoOrigen, bancoDestino);
        }

        public DataTable ConsultarMasivo(string idNominas, int tipoReporte)
        {
            return dNomina.ConsultarMasivo(idNominas, tipoReporte);
        }

        public DataTable ConsultarMasivoXBancoDispersion(string idNominas, string bancoOrigen, string bancoDestino)
        {
            return dNomina.ConsultarMasivoXBancoDispersion(idNominas, bancoOrigen, bancoDestino);
        }

        public DataTable ConsultarMasivoXBancoDispersionResumen(string idNominas, string bancoOrigen, string bancoDestino)
        {
            return dNomina.ConsultarMasivoXBancoDispersionResumen(idNominas, bancoOrigen, bancoDestino);
        }

        public DataTable ConsultarXIDCuboDetallado(String ID_nomina)
        {
            return dNomina.ConsultarXIDCuboDetallado(ID_nomina);
        }

        public DataTable ConsultarXIDDetalladoTotales(String ID_nomina)
        {
            return dNomina.ConsultarXIDDetalladoTotales(ID_nomina);
        }

        public DataTable ConsultarXIDdetallado(double ID_nomina)
        {
            return dNomina.ConsultarXIDdetallado(ID_nomina);
        }

        public DataTable ConsultarXID_ExcelHorasExtra(double ID_nomina)
        {
            return dNomina.ConsultarXID_ExcelHorasExtra(ID_nomina);
        }

        public DataTable ConsultarXID_ExcelMasivo(double ID_nomina)
        {
            return dNomina.ConsultarXID_ExcelMasivo(ID_nomina);
        }

        public DataTable ConsultarXID_ExcelRetiroMasivo(double ID_nomina)
        {
            return dNomina.ConsultarXID_ExcelRetiroMasivo(ID_nomina);
        }

        public DataTable ConsultarXID_ExcelNovGeneral(double ID_nomina)
        {
            return dNomina.ConsultarXID_ExcelNovGeneral(ID_nomina);
        }

        public DataTable ConsultarXID_EmpledosEnCero(double ID_nomina)
        {
            return dNomina.ConsultarXID_EmpledosEnCero(ID_nomina);
        }

        public DataTable ConsultarXidNomBase(string ID_nominaBase)
        {
            return dNomina.ConsultarXidNomBase(ID_nominaBase);
        }

        public DataTable ConsultarDetalleXDocumento(string ID_nomina, string afi_documento)
        {
            return dNomina.ConsultarDetalleXDocumento(ID_nomina, afi_documento);
        }

        /// <summary>
        /// Se consume para cargar los exitos y rechazos. Trae menos campos y no viene con select *
        /// </summary>
        /// <param name="ID_nomina"></param>
        /// <param name="afi_documento"></param>
        /// <returns></returns>
        public DataTable ConsultarDetalleXDocumentoExitoso(string ID_nomina, string afi_documento)
        {
            return dNomina.ConsultarDetalleXDocumentoExitoso(ID_nomina, afi_documento);
        }

        public DataTable ConsultarXIDconceptos(double ID_nomina)
        {
            return dNomina.ConsultarXIDconceptos(ID_nomina);
        }

        public DataTable ConsultarXID_AutoPago(double ID_nomina)
        {
            return dNomina.ConsultarXID_AutPago(ID_nomina);
        }

        public DataTable ConsultaAfiliadosXSinNomina(string ID_empresaTemporal, string fechaInicio, string fechaFin)
        {
            return dNomina.ConsultaAfiliadosXSinNomina(ID_empresaTemporal, fechaInicio, fechaFin);
        }

        public DataTable ConsultaNomAnteriorXContratoTotal(string ID_contratoFK, string nom_fechaInicio)
        {
            return dNomina.ConsultaNomAnteriorXContratoTotal(ID_contratoFK, nom_fechaInicio);
        }
        #endregion

        #region NOVEDADES LABORALES

        public string ConsultaUnidadXNovedad(string tipoNovedad, string con_tipo)
        {
            DataTable tbReferencia;
            if (con_tipo == "SABATINO")
                tbReferencia = NUtilidades.consultaReferenciaXModulo_y_Valor("NovedadesTiempoParcial", tipoNovedad);            
            else
                tbReferencia = NUtilidades.consultaReferenciaXModulo_y_Valor("NovedadesLaborales", tipoNovedad);

            if (tipoNovedad != "HoraDeducida")
                return tbReferencia.Rows[0]["ref_descripcion"].ToString();
            else
                return "Minutos";
        }

        public Int64 CalcularValorNovedad(string tipoNovedad, double valor, int salarioBase, DateTime novFechaInicio, DateTime novFechaFin, DateTime nomFechaInicio, DateTime nomFechaFin, int pagaIncapacidad, ref float valorTotal, bool habilitadoBorrar = true, string esProrroga = "No")
        {
            Int64 valorNovedad = 0;
            //double valorHora = Math.Ceiling((double)salarioBase / 240);
            double valorHora = (double)salarioBase / 240;
            switch (tipoNovedad)
            {
                case "HED":
                    valorNovedad = Convert.ToInt64(Math.Ceiling(valorHora * valor * 1.25));
                    break;
                case "HEN":
                    valorNovedad = Convert.ToInt64(Math.Ceiling(valorHora * valor * 1.75));
                    break;
                case "HEDD":
                    valorNovedad = Convert.ToInt64(Math.Ceiling(valorHora * valor * 2));
                    break;
                case "HEND":
                    valorNovedad = Convert.ToInt64(Math.Ceiling(valorHora * valor * 2.5));
                    break;
                case "HDDOR"://HORA DIURNA DOMINICAL ORDINARIA
                    valorNovedad = Convert.ToInt64(Math.Ceiling(valorHora * valor * 1.75));
                    break;
                case "HNDOR"://HORA NOCTURNA DOMINICAL ORDINARIA
                    valorNovedad = Convert.ToInt64(Math.Ceiling(valorHora * valor * 2.1));
                    break;
                case "RN":
                    valorNovedad = Convert.ToInt64(Math.Ceiling(valorHora * valor * 0.35));
                    break;
                case "HDDO": //RECARGO DIURNO DOMINICAL O FESTIVO
                    valorNovedad = Convert.ToInt64(Math.Ceiling(valorHora * valor * 0.75));
                    break;
                case "RND":
                    valorNovedad = Convert.ToInt64(Math.Ceiling(valorHora * valor * 1.1));
                    break;
                case "HoraAdicional":
                case "HoraCompensadaPrestacional":
                    valorNovedad = Convert.ToInt64(Math.Ceiling(valorHora * valor)); //PARA TIPO TIEMPO PARCIAL - SABATINOS y la hora compensada es de oferta
                    break;
                case "HoraDeducida":
                    valorNovedad = Convert.ToInt64(Math.Ceiling((Convert.ToDouble(Convert.ToDouble(salarioBase) / 14400) * valor)));
                    break;
                case "IncapGeneral": case "IncapSoat":
                case "Incapacidad a cargo del cliente":
                case "Incapacidad EG superior a 3 dias":                
                    #region Incapacidad
                    DateTime incInicio, incFin;
                    int diasIncapcidad;// los dias que se asocian de incapacidad, los que cubre el empleador y los que cubre la entidad EN ESTA NOMINA
                    incInicio = novFechaInicio;
                    incFin = novFechaFin;

                    //si la fecha de inicio de la nomina es mayor que la de la novedad
                    if (DateTime.Compare(nomFechaInicio, novFechaInicio) >= 0)
                        incInicio = nomFechaInicio;

                    if (DateTime.Compare(nomFechaFin, novFechaFin) < 0)
                        incFin = nomFechaFin;
                    //novFechaFin = nomFechaFin;

                    //vuelvo a calcular los dias que corresponden a esta nomina
                    diasIncapcidad = NUtilidades.calcularTiempo(incInicio, string.Format("{0:yyyy-MM-dd}", incFin), "dias");
                    if ((Convert.ToDateTime(incFin).Month == 2) && (Convert.ToDateTime(incFin).Day == 28))
                        diasIncapcidad = diasIncapcidad + 2;
                    if ((Convert.ToDateTime(incFin).Month == 2) && (Convert.ToDateTime(incFin).Day == 29))
                        diasIncapcidad = diasIncapcidad + 1;

                    //Junio 14/2016 - Colocar parametro por centro de costo para saber si la entidad paga la incapacidad menor a dos dias al 100%
                    double SMLV = NUtilidades.consultarSalarioMinimo(novFechaFin.Year);
                    //para saber si la novedad de incapacidad, viene calculada de otra incapacidad
                    if ((habilitadoBorrar == false) || (esProrroga == "Si"))
                        pagaIncapacidad = 2;

                    if (pagaIncapacidad == 1)//caso innnova
                    {
                        if (diasIncapcidad <= 2)
                            valorNovedad = Convert.ToInt64((Convert.ToDouble(salarioBase) / 30) * diasIncapcidad);
                        else
                        { //los dos primeros dias los paga al 100 y el resto al 66
                            long valorDosPrimeros = Convert.ToInt64(Convert.ToDouble(salarioBase) / 30 * 2);
                            valorNovedad = Convert.ToInt64((Convert.ToDouble(salarioBase) / 30 * 0.6667) * (diasIncapcidad - 2));
                            if (valorNovedad < ((SMLV / 30) * (diasIncapcidad - 2)))
                                valorNovedad = Convert.ToInt64((SMLV / 30) * (diasIncapcidad - 2));

                            valorNovedad = valorNovedad + valorDosPrimeros;
                        }
                    }
                    else
                    {
                        valorNovedad = Convert.ToInt64((Convert.ToDouble(salarioBase) / 30 * 0.6667) * diasIncapcidad);
                        if (valorNovedad < ((SMLV / 30) * diasIncapcidad))
                            valorNovedad = Convert.ToInt64((SMLV / 30) * diasIncapcidad);
                    }

                    //-----------  CALCULO EL VALOR TOTAL DE LA INCAPACIDAD  ------------
                    diasIncapcidad = NUtilidades.calcularTiempo(novFechaInicio, string.Format("{0:yyyy-MM-dd}", novFechaFin), "dias");
                    if ((Convert.ToDateTime(novFechaFin).Month == 2) && (Convert.ToDateTime(novFechaFin).Day == 28))
                        diasIncapcidad = diasIncapcidad + 2;
                    if ((Convert.ToDateTime(novFechaFin).Month == 2) && (Convert.ToDateTime(novFechaFin).Day == 29))
                        diasIncapcidad = diasIncapcidad + 1;

                    if (pagaIncapacidad == 1)//caso innnova
                    {
                        if (diasIncapcidad <= 2)
                            valorTotal = Convert.ToInt64((Convert.ToDouble(salarioBase) / 30) * diasIncapcidad);
                        else
                        { //los dos primeros dias los paga al 100 y el resto al 66
                            long valorDosPrimeros = Convert.ToInt64(Convert.ToDouble(salarioBase) / 30 * 2);
                            valorTotal = Convert.ToInt64((Convert.ToDouble(salarioBase) / 30 * 0.6667) * (diasIncapcidad - 2));
                            if (valorTotal < ((SMLV / 30) * (diasIncapcidad - 2)))
                                valorTotal = Convert.ToInt64((SMLV / 30) * (diasIncapcidad - 2));

                            valorTotal = valorTotal + valorDosPrimeros;
                        }
                    }
                    else
                    {
                        valorTotal = Convert.ToInt64((Convert.ToDouble(salarioBase) / 30 * 0.6667) * diasIncapcidad);
                        if (valorTotal < ((SMLV / 30) * diasIncapcidad))
                            valorTotal = Convert.ToInt64((SMLV / 30) * diasIncapcidad);
                    }
                    //-----------***************------------
                    #endregion
                    break;
                case "IncapProfesional":
                case "LicenciaMaternidad":
                case "IncapARL":
                case "LicenciaPaternidad":
                case "Vacaciones":
                case "VacacionesDisfrutadas":
                case "Incapacidad por ACC LABORAL 1 dia cargo del cliente":
                case "Incapacidad por ACC LABORAL superior a 2 dias":
                    valorNovedad = Convert.ToInt64(((Convert.ToDouble(salarioBase) / 30)) * valor);
                    valorTotal = valorNovedad;
                    break;
                case "LicenciaNoRemunerada":
                case "Sancion":
                case "LicenciaRemunerada":
                case "DevolucionLNR":
                    valorNovedad = Negocio.NUtilidades.redondea(Convert.ToInt64(((Convert.ToDouble(salarioBase) / 30)) * valor), 100);
                    break;
                case "DiasLaborados":
                    valorNovedad = Negocio.NUtilidades.redondea(Convert.ToInt64(((Convert.ToDouble(salarioBase) / 30)) * valor), 100);
                    break;
                case "LicenciaLuto":
                case "OtrasDeducciones":
                case "BonificacionOcasional":
                case "Comisiones":
                case "DevengoSalarial":
                case "Retiro":
                    valorNovedad = Convert.ToInt64(valor);
                    break;
                case "EmbargoAlimentos":
                    //valorNovedad = Convert.ToInt64(((Convert.ToDouble(salarioBase) / 30)) * valor);
                    valorNovedad = Convert.ToInt64(valor);
                    break;
                default:
                    valorNovedad = Convert.ToInt64(valor);
                    break;
            }
            return valorNovedad;
        }

        public string novInsertar(string ID_nominaBaseFK, string nov_tipoNovedadFK, string nov_unidades, double nov_valor, string nov_valorPesos, double nov_valorTotal, string nov_fechaInicio, string nov_fechaFin, string nov_observacion, string ID_usuarioRegistraFK, bool nov_habilitadoBorrar = true, bool reliquidar = true, int ID_incapPadreFK = 0)
        {
            DataTable tbNovedad = dNomina.novInsertar(ID_nominaBaseFK, nov_tipoNovedadFK, nov_unidades, nov_valor, nov_valorPesos, nov_valorTotal, nov_fechaInicio, nov_fechaFin, nov_observacion, ID_usuarioRegistraFK, nov_habilitadoBorrar, ID_incapPadreFK);
            //reliquidar viene en false, cuando son prestamos
            if ((reliquidar == true) && (tbNovedad.Rows.Count > 0) && (tbNovedad.Rows[0]["resultado"].ToString() == "OK"))
            {
                //COMO INSERTO UNA NOVEDAD RELIQUIDO LA NOMINA BASE DE ESTE AFILIADO
                DataTable tbNomBase = ConsultarXidNomBase(ID_nominaBaseFK);
                DataRow drBase = tbNomBase.Rows[0];
                double SMLV = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(nov_fechaFin).Year);
                double AuxTransp = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(nov_fechaFin).Year, true);
                string novedadesDescuento = dNomina.novFiltroDescuentos();
                string novedadesDevengo = dNomina.novFiltroDevengos();

                calcularNominaBase(drBase["ID_nominaFK"].ToString(), drBase, drBase["ID_empresaTemporalFK"].ToString(), drBase["ID_empresaUsuariaFK"].ToString(), drBase["ID_centroCostoFK"].ToString(),
                                   drBase["nom_fechaInicio"].ToString(), drBase["nom_fechaFin"].ToString(), SMLV, AuxTransp, ID_usuarioRegistraFK,novedadesDescuento, novedadesDevengo, ID_nominaBaseFK);
                return "OK";
            }
            else {
                if (tbNovedad.Rows.Count > 0)
                    return tbNovedad.Rows[0]["mensaje"].ToString();
                else
                    return "Error no determinado en la captura de datos. Por favor tome una captura de pantalla y envíela al administrador del sistema.";
            }
        }

        public void novEliminar(string ID_nominaBaseFK, string ID_novedad, string ID_usuarioEliminaFK)
        {
            dNomina.novEliminar(ID_novedad, ID_usuarioEliminaFK);

            //COMO INSERTO UNA NOVEDAD RELIQUIDO LA NOMINA BASE DE ESTE AFILIADO
            DataTable tbNomBase = ConsultarXidNomBase(ID_nominaBaseFK);
            DataRow drBase = tbNomBase.Rows[0];
            double SMLV = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(drBase["nom_fechaFin"]).Year);
            double AuxTransp = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(drBase["nom_fechaFin"]).Year, true);
            string novedadesDescuento = dNomina.novFiltroDescuentos();
            string novedadesDevengo = dNomina.novFiltroDevengos();

            calcularNominaBase(drBase["ID_nominaFK"].ToString(), drBase, drBase["ID_empresaTemporalFK"].ToString(), drBase["ID_empresaUsuariaFK"].ToString(), drBase["ID_centroCostoFK"].ToString(),
                               drBase["nom_fechaInicio"].ToString(), drBase["nom_fechaFin"].ToString(), SMLV, AuxTransp, ID_usuarioEliminaFK, novedadesDescuento, novedadesDevengo, ID_nominaBaseFK);
        }

        public void novEliminarRetiroMasivo(string ID_nominaFK, string afi_documento, string ID_usuarioEliminaFK)
        {
            DataTable tbResultado = dNomina.novEliminarRetiroMasivo(ID_nominaFK, afi_documento);
            DataTable tbNomBase = ConsultarXidNomBase(tbResultado.Rows[0]["ID_nominaBaseFK"].ToString());
            DataRow drBase = tbNomBase.Rows[0];
            double SMLV = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(drBase["nom_fechaFin"]).Year);
            double AuxTransp = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(drBase["nom_fechaFin"]).Year, true);
            string novedadesDescuento = dNomina.novFiltroDescuentos();
            string novedadesDevengo = dNomina.novFiltroDevengos();

            calcularNominaBase(drBase["ID_nominaFK"].ToString(), drBase, drBase["ID_empresaTemporalFK"].ToString(), drBase["ID_empresaUsuariaFK"].ToString(), drBase["ID_centroCostoFK"].ToString(),
                               drBase["nom_fechaInicio"].ToString(), drBase["nom_fechaFin"].ToString(), SMLV, AuxTransp, ID_usuarioEliminaFK, novedadesDescuento, novedadesDevengo, tbResultado.Rows[0]["ID_nominaBaseFK"].ToString());
        }

        public int novExistenNovedadesXNominaBase(string ID_nominaBaseFK)
        {
            DataTable tbResult = dNomina.novExistenNovedadesXNominaBase(ID_nominaBaseFK);
            if (tbResult != null)
                return tbResult.Rows.Count;
            else
                return 0;
        }

        public DataTable novRetiroMasivoValidar(string ID_nominaFK, string afi_documento, string fechaRetiro)
        {
            return dNomina.novRetiroMasivoValidar(ID_nominaFK, afi_documento, fechaRetiro);
        }

        public void novRetiroMasivoEliminar(string ID_nominaFK, string afi_documento)
        {

        }

        public DataTable novConsultaXNominaBase(string ID_nominaBaseFK, string ref_modulo)
        {
            return dNomina.novConsultaXNominaBase(ID_nominaBaseFK, ref_modulo);
        }

        public DataTable novConsultaLaboralesXNominaBaseXTipo(string ID_nominaBaseFK, string TiposNovedad, bool excluir = false)
        {
            return dNomina.novConsultaLaboralesXNominaBaseXTipo(ID_nominaBaseFK, TiposNovedad, excluir);
        }

        public DataTable novConsultaLaboralesXContratoXTipo(string ID_contratoFK, string TiposNovedad, bool excluir)
        {
            return dNomina.novConsultaLaboralesXContratoXTipo(ID_contratoFK, TiposNovedad, excluir);
        }

        /// <summary>
        /// Lista las novedades según el orden. [1 y 2 ingresos] [3 deducciones] [4 incap] [5 Licencias]
        /// </summary>
        /// <param name="ID_nominaBaseFK"></param>
        /// <param name="TiposNovedad"></param>
        /// <param name="excluir"></param>
        /// <returns></returns>
        public DataTable novConsultaLaboralesXNominaBaseXOrden(string ID_nominaBaseFK, string ref_orden)
        {
            return dNomina.novConsultaLaboralesXNominaBaseXOrden(ID_nominaBaseFK, ref_orden);
        }

        #endregion

        #region INCAPACIDADES

        public DataTable incConsultarXEmpleado(int ID_afiliadoFK)
        {
            return dNomina.incConsultarXEmpleado(ID_afiliadoFK);
        }

        public DataTable incConsultarXContrato(string ID_contratoFK, String ID_tipoNovedadFK)
        {
            return dNomina.incConsultarXContrato(ID_contratoFK, ID_tipoNovedadFK);
        }

        public DataTable IncConsultarXEmpleadoXAplicar(string ID_afiliadoFK, string ID_contratoFK, string nom_fechaInicio, string nom_fechaFin)
        {
            return dNomina.IncConsultarXEmpleadoXAplicar(ID_afiliadoFK, ID_contratoFK, nom_fechaInicio, nom_fechaFin);
        }

        public DataTable IncConsultaXEstado(string inc_estado, string where)
        {
            return dNomina.IncConsultaXEstado(inc_estado, where);
        }

        public void IncTramitar(string ID_incapacidad, string inc_estado, double inc_valorRecobrar, string inc_tramitarObservacion, string inc_tramitarCausal, int ID_usuarioTramitaFK)
        {
            dNomina.IncTramitar(ID_incapacidad, inc_estado, inc_valorRecobrar, inc_tramitarObservacion, inc_tramitarCausal, ID_usuarioTramitaFK);
        }

        public void IncReconocer(string ID_incapacidad, string inc_estado, double inc_valorReconocer, string inc_ReconocerObservacion, string inc_ReconocerCausal, int ID_usuarioReconocerFK)
        {
            dNomina.IncReconocer(ID_incapacidad, inc_estado, inc_valorReconocer, inc_ReconocerObservacion, inc_ReconocerCausal, ID_usuarioReconocerFK);
        }

        public void IncPagadoEPS(string ID_incapacidad, string inc_estado, double inc_valorPagadoEPS, string inc_PagadoEPSObservacion, int ID_usuarioPagadoEPSFK, string inc_fechaPagadoEPS)
        {
            dNomina.IncPagadoEPS(ID_incapacidad, inc_estado, inc_valorPagadoEPS, inc_PagadoEPSObservacion, ID_usuarioPagadoEPSFK, inc_fechaPagadoEPS);
        }

        public void IncReclamo(string ID_incapacidad, string inc_reclamoObservacion, int ID_usuarioReclamoFK)
        {
            dNomina.IncReclamo(ID_incapacidad, inc_reclamoObservacion, ID_usuarioReclamoFK);
        }

        public void IncPagadoEU(string ID_incapacidad, string inc_estado, double inc_valorPagadoEU, string inc_PagadoEUTipoPago, string inc_PagadoEUObservacion, int ID_usuarioPagadoEUFK, string inc_fechaPagadoEU)
        {
            dNomina.IncPagadoEU(ID_incapacidad, inc_estado, inc_valorPagadoEU, inc_PagadoEUTipoPago, inc_PagadoEUObservacion, ID_usuarioPagadoEUFK, inc_fechaPagadoEU);
        }

        public void IncReversar(string ID_incapacidad)
        {
            dNomina.IncReversar(ID_incapacidad);
        }

        public void IncActualizaFechaReal(string ID_incapacidad, string inc_fechaInicioReal, string inc_fechaFinReal, int ID_usuarioActualizaFK)
        {
            dNomina.IncActualizaFechaReal(ID_incapacidad, inc_fechaInicioReal, inc_fechaFinReal, ID_usuarioActualizaFK);
        }

        public void IncCargarArchivo(string ID_incapacidad, string inc_archivo, int ID_usuarioArchivoFK)
        {
            dNomina.IncCargarArchivo(ID_incapacidad, inc_archivo, ID_usuarioArchivoFK);
        }

        public void IncVincular(string ID_incapacidad, string ID_vinculadaFK, int ID_usuariovinculadaFK)
        {
            dNomina.IncVincular(ID_incapacidad, ID_vinculadaFK, ID_usuariovinculadaFK);
        }

        public DataTable IncConsultaVinculadas(string ID_incapacidad)
        {
            return dNomina.IncConsultaVinculadas(ID_incapacidad);
        }

        public void IncInsertarManual(string ID_tipoNovedadFK, string ID_afiliadoFK, string ID_contratoFK, double inc_valor, string inc_fechaInicio, string inc_fechaFin, string inc_observaciones, int ID_usuarioRegistraFK)
        {
            dNomina.IncInsertarManual(ID_tipoNovedadFK, ID_afiliadoFK, ID_contratoFK, inc_valor, inc_fechaInicio, inc_fechaFin, inc_observaciones, ID_usuarioRegistraFK);
        }
        #endregion

        #region LIQUIDACION DEFINITIVA
        public DataTable liqContratosXEmpleado(int ID_afiliadoFK) => dNomina.liqContratosXEmpleado(ID_afiliadoFK);

        public DataTable liqConsultaAcumuladoXEmpleado(int ID_afiliadoFK, int ID_contratoFK)
        {
            return dNomina.liqConsultaAcumuladoXEmpleado(ID_afiliadoFK, ID_contratoFK);
        }

        public DataTable liqConsultaVacaciones(int ID_contratoFK)
        {
            return dNomina.liqConsultaVacaciones(ID_contratoFK);
        }

        public DataTable liqConsultaPrimas(int ID_contratoFK, string con_fechaInicio, string con_fechaFin)
        {
            string fechaInicio = string.Format("{0:yyyy-01-01}", Convert.ToDateTime(con_fechaInicio));
            string fechaFin = string.Format("{0:yyyy-06-30}", Convert.ToDateTime(con_fechaFin));
            if (Convert.ToDateTime(con_fechaInicio).Year == Convert.ToDateTime(con_fechaFin).Year)
            { // RETIRO EN EL MISMO AÑO
                if (Convert.ToDateTime(con_fechaFin).Month > 6)
                {
                    fechaInicio = string.Format("{0:yyyy-07-01}", Convert.ToDateTime(con_fechaFin));
                    fechaFin = string.Format("{0:yyyy-12-30}", Convert.ToDateTime(con_fechaFin));
                }

                if (DateTime.Compare(Convert.ToDateTime(con_fechaInicio), Convert.ToDateTime(fechaInicio)) < 0)
                    con_fechaInicio = fechaInicio;
            }
            else
            { //RETIRO EN OTRO AÑO
                con_fechaInicio = string.Format("{0:yyyy-01-01}", Convert.ToDateTime(con_fechaFin));
                fechaInicio = string.Format("{0:yyyy-01-01}", Convert.ToDateTime(con_fechaFin));
                fechaFin = string.Format("{0:yyyy-06-30}", Convert.ToDateTime(con_fechaFin));

                if (Convert.ToDateTime(con_fechaFin).Month > 6)
                {
                    con_fechaInicio = string.Format("{0:yyyy-07-01}", Convert.ToDateTime(con_fechaFin));
                    fechaInicio = string.Format("{0:yyyy-07-01}", Convert.ToDateTime(con_fechaFin));
                    fechaFin = string.Format("{0:yyyy-12-30}", Convert.ToDateTime(con_fechaFin));
                }
            }


            return dNomina.liqConsultaPrima(ID_contratoFK, fechaInicio, fechaFin, con_fechaInicio, con_fechaFin);
        }

        public DataTable liqConsultaPrimaPagada(int ID_contratoFK, string con_fechaInicio, string con_fechaFin)
        {
            string fechaInicio = string.Format("{0:yyyy-01-01}", Convert.ToDateTime(con_fechaInicio));
            string fechaFin = string.Format("{0:yyyy-06-30}", Convert.ToDateTime(con_fechaFin));
            if (Convert.ToDateTime(con_fechaInicio).Year == Convert.ToDateTime(con_fechaFin).Year)
            { // RETIRO EN EL MISMO AÑO
                if (Convert.ToDateTime(con_fechaFin).Month > 6)
                {
                    fechaInicio = string.Format("{0:yyyy-07-01}", Convert.ToDateTime(con_fechaFin));
                    fechaFin = string.Format("{0:yyyy-12-30}", Convert.ToDateTime(con_fechaFin));
                }

                if (DateTime.Compare(Convert.ToDateTime(con_fechaInicio), Convert.ToDateTime(fechaInicio)) < 0)
                    con_fechaInicio = fechaInicio;
            }
            else
            { //RETIRO EN OTRO AÑO
                con_fechaInicio = string.Format("{0:yyyy-01-01}", Convert.ToDateTime(con_fechaFin));
                fechaInicio = string.Format("{0:yyyy-01-01}", Convert.ToDateTime(con_fechaFin));
                fechaFin = string.Format("{0:yyyy-06-30}", Convert.ToDateTime(con_fechaFin));

                if (Convert.ToDateTime(con_fechaFin).Month > 6)
                {
                    con_fechaInicio = string.Format("{0:yyyy-07-01}", Convert.ToDateTime(con_fechaFin));
                    fechaInicio = string.Format("{0:yyyy-07-01}", Convert.ToDateTime(con_fechaFin));
                    fechaFin = string.Format("{0:yyyy-12-30}", Convert.ToDateTime(con_fechaFin));
                }
            }

            return dNomina.liqConsultaPrimaPagada(ID_contratoFK, fechaInicio, fechaFin);
        }

        public DataTable liqConsultaCesantias(int ID_contratoFK, string con_fechaInicio, string con_fechaFin)
        {
            string fechaInicio = string.Format("{0:yyyy-01-01}", Convert.ToDateTime(con_fechaInicio));
            string fechaFin = string.Format("{0:yyyy-12-30}", Convert.ToDateTime(con_fechaFin));

            if (DateTime.Compare(Convert.ToDateTime(con_fechaInicio), Convert.ToDateTime(fechaInicio)) < 0)
                con_fechaInicio = fechaInicio;

            return dNomina.liqConsultaCesantias(ID_contratoFK, fechaInicio, fechaFin, con_fechaInicio, con_fechaFin);
        }

        public DataTable liqConsultaHorasExtraXContrato(int ID_afiliadoFK, int ID_contratoFK)
        {
            return dNomina.liqConsultaHorasExtraXContrato(ID_afiliadoFK, ID_contratoFK);
        }

        public DataTable liqConsultaRecargosXContrato(int ID_afiliadoFK, int ID_contratoFK)
        {
            return dNomina.liqConsultaRecargosXContrato(ID_afiliadoFK, ID_contratoFK);
        }

        public DataTable liqConsultaAusentismoXContrato(int ID_afiliadoFK, int ID_contratoFK)
        {
            return dNomina.liqConsultaAusentismoXContrato(ID_afiliadoFK, ID_contratoFK);
        }

        public DataTable liqConsultaPrestamosXContrato(int ID_afiliadoFK, int ID_contratoFK)
        {
            return dNomina.liqConsultaPrestamosXContrato(ID_afiliadoFK, ID_contratoFK);
        }

        public DataTable liqConsultaRetiroExtemporaneo(int ID_contratoFK)
        {
            return dNomina.liqConsultaRetiroExtemporaneo(ID_contratoFK);
        }

        public DataTable liqInsertar(int ID_contratoFK, int ID_afiliadoFK, int liq_vacaDias, int liq_primaDias, int liq_cesanDiasAnterior, int liq_interesDiasAnterior, int liq_cesanDias, int liq_interesDias
            , double liq_acumVacaValorPagar, double liq_acumPrimaValorPagar, double liq_acumCesanValorPagar, double liq_acumInteValorPagar, double liq_acumValorTotal, double liq_calcVacaSalario
            , double liq_calcVacaHE, double liq_calcVacaOtros, double liq_calcVacaValorPagar, double liq_calcPrimaSalario, double liq_calcPrimaHE, double liq_calcPrimaAuxTransporte
            , double liq_calcPrimaValorPagar, double liq_calcCesanSalarioAnterior, double liq_calCesanHEAnterior, double liq_calcCesanAuxTransporteAnterior, double liq_calcCesanValorPagarAnterior
            , double liq_calcCesanSalario, double liq_calcCesanHE, double liq_calcCesanAuxTransporte, double liq_calcCesanValorPagar, double liq_calcInteValorPagarAnterior
            , double liq_calcInteValorPagar, double liq_calcValorTotal, double liq_ValorGirar, string ID_usuarioRegistraFK
            , bool liq_calcPrimaPagar, bool liq_calcCesanPagar, bool liq_calcCesanPagarAnterior, bool liq_calcIntePagar, bool liq_calcIntePagarAnterior)
        {
            return dNomina.liqInsertar(ID_contratoFK, ID_afiliadoFK, liq_vacaDias, liq_primaDias, liq_cesanDiasAnterior, liq_interesDiasAnterior, liq_cesanDias, liq_interesDias
            , liq_acumVacaValorPagar, liq_acumPrimaValorPagar, liq_acumCesanValorPagar, liq_acumInteValorPagar, liq_acumValorTotal, liq_calcVacaSalario
            , liq_calcVacaHE, liq_calcVacaOtros, liq_calcVacaValorPagar, liq_calcPrimaSalario, liq_calcPrimaHE, liq_calcPrimaAuxTransporte
            , liq_calcPrimaValorPagar, liq_calcCesanSalarioAnterior, liq_calCesanHEAnterior, liq_calcCesanAuxTransporteAnterior, liq_calcCesanValorPagarAnterior
            , liq_calcCesanSalario, liq_calcCesanHE, liq_calcCesanAuxTransporte, liq_calcCesanValorPagar, liq_calcInteValorPagarAnterior
            , liq_calcInteValorPagar, liq_calcValorTotal, liq_ValorGirar, ID_usuarioRegistraFK
            , liq_calcPrimaPagar, liq_calcCesanPagar, liq_calcCesanPagarAnterior, liq_calcIntePagar, liq_calcIntePagarAnterior);
        }

        public DataTable liqInsertarConcepto(int ID_liquidacionFK, string liq_tipoConcepto, string liq_concepto, double liq_valor, string ID_usuarioRegistraFK)
        {
            return dNomina.liqInsertarConcepto(ID_liquidacionFK, liq_tipoConcepto, liq_concepto, liq_valor, ID_usuarioRegistraFK);
        }

        public DataTable liqConsultaConceptos(string ID_liquidacionFK)
        {
            return dNomina.liqConsultaConceptos(ID_liquidacionFK);
        }

        public DataTable liqConsultaXID(string ID_liquidacion, string ID_contrato="0")
        {
            return dNomina.liqConsultaXID(ID_liquidacion, ID_contrato);
        }

        public DataTable liqActualizaEstado(string ID_liquidacion, string ID_contratoFK, string liq_estado, string liq_ObservacionActualiza, string ID_usuarioActualizaFK)
        {
            return dNomina.liqActualizaEstado(ID_liquidacion, ID_contratoFK, liq_estado, liq_ObservacionActualiza, ID_usuarioActualizaFK);
        }

        public void liqActualizaValorGirar(string ID_liquidacion, double liq_valorGirar, string ID_usuarioActualizaFK)
        {
            dNomina.liqActualizaValorGirar(ID_liquidacion, liq_valorGirar, ID_usuarioActualizaFK);
        }

        #region ADMINISTRAR PROCESOS DE LIQUIDACIONES  ----------------

        public DataTable liqConsultaXEstado(string liq_estado, string where)
        {
            return dNomina.liqConsultaXEstado(liq_estado, where);
        }

        public void liqAprobar(string ID_liquidacion, string ID_usuarioApruebaFK)
        {
            dNomina.liqAprobar(ID_liquidacion, ID_usuarioApruebaFK);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID_liquidacion"></param>
        /// <param name="liq_soporte"></param>
        /// <param name="ID_usuarioApruebaFK"></param>
        /// <param name="actualizaEstado">Actualiza el estado cuando pasa de aprobada a soportada. Va en false cuando solo estan actualizando el soporte</param>
        public void liqSoportar(string ID_liquidacion, string liq_soporte, string ID_usuarioApruebaFK, bool actualizaEstado)
        {
            dNomina.liqSoportar(ID_liquidacion, liq_soporte, ID_usuarioApruebaFK, actualizaEstado);
        }

        public void liqPagar(string ID_liquidacion, string liq_planoGiro, string liq_PDFGiro, string liq_fechaPagoGiro, string ID_usuarioRegistroGiroFK)
        {
            dNomina.liqPagar(ID_liquidacion, liq_planoGiro, liq_PDFGiro, liq_fechaPagoGiro, ID_usuarioRegistroGiroFK);
        }

        public void liqDevolverEstado(string ID_liquidacion, string liq_estado, string ID_usuarioActualizaFK)
        {
            dNomina.liqDevolverEstado(ID_liquidacion, liq_estado, ID_usuarioActualizaFK);
        }

        public void liqGeneraPlano(string ID_liquidacion, string ID_usuarioPlanoFK)
        {
            dNomina.liqGeneraPlano(ID_liquidacion, ID_usuarioPlanoFK);
        }

        public DataTable liqConsultaParaRechazos(string ID_empresaTemporalFK, string ID_empresaUsuariaFK, string afi_documento, int liq_ValorGirar)
        {
            return dNomina.liqConsultaParaRechazos(ID_empresaTemporalFK, ID_empresaUsuariaFK, afi_documento, liq_ValorGirar);
        }
        #endregion
            #endregion

        #region PRIMAS
        public Int64 priCalcularPrimas(string ID_empresaTemporal, string ID_empresaUsuariaFK, string ID_centroCosto, string frecuenciaNomina, string mes, string año, string ID_usuarioRegistraFK, Int64 idNominaActualizar = 0)
        {
            //1) busco los empleados activos sin pago prima a hoy
            string fechaInicio = año + "-01-01";
            string fechaFin = año + "-06-30";

            if (mes != "06")
            {
                fechaInicio = año + "-07-01";
                fechaFin = año + "-12-30";
            }
            double SMLV = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(fechaFin).Year);
            double AuxTransp = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(fechaFin).Year,true);

            DataTable tbAfiliados = dNomina.priConsultaAfiliadosXEmpresaSinPrima(ID_empresaTemporal, ID_empresaUsuariaFK, ID_centroCosto, fechaInicio, fechaFin);
            if (tbAfiliados.Rows.Count > 0)
            {
                Int64 idNomina = idNominaActualizar == 0 ? NUtilidades.IdentificadorNomina(ID_empresaTemporal) : idNominaActualizar;
                dNomina.Insertar(idNomina, ID_empresaTemporal, ID_empresaUsuariaFK,"Primas", fechaInicio, fechaFin, "POR REVISAR", ID_usuarioRegistraFK);

                string idNomBase = "";
                //**---SI HAY AFILIADOS SIN PRIMA, CALCULO Y LUEGO INSERTO LA PRIMA  --** 
                for (int i = 0; i < tbAfiliados.Rows.Count; i++)
                {
                    idNomBase = pricalcularPrimaBase(idNomina.ToString(), tbAfiliados.Rows[i], ID_empresaTemporal, ID_empresaUsuariaFK, ID_centroCosto, frecuenciaNomina, fechaInicio, fechaFin, SMLV, AuxTransp, ID_usuarioRegistraFK);
                    
                    #region VALIDAR SI TIENE PRESTAMOS POR ASOCIAR A ESTAS PRIMAS
                    NEmpleados nEmpleado = new NEmpleados();
                    DataTable tbPrestamos = nEmpleado.prestamoConsultaCuotasParaAplicar(tbAfiliados.Rows[i]["ID_afiliado"].ToString(), tbAfiliados.Rows[i]["ID_contrato"].ToString(),"",2);
                    if ((tbPrestamos != null) && (tbPrestamos.Rows.Count > 0))
                    {
                        for (int j = 0; j < tbPrestamos.Rows.Count; j++)
                        {
                            novInsertar(idNomBase, "Prestamo", "Pesos", Convert.ToDouble(tbPrestamos.Rows[j]["cuo_valor"]), tbPrestamos.Rows[j]["cuo_valor"].ToString(),0, fechaInicio, fechaFin, "Cuota Prima", ID_usuarioRegistraFK, false, false);
                            //**-- asociar este idNomina a la cuota de prima para descontarla como paga --**
                            nEmpleado.prestamoActualizaCuota(tbPrestamos.Rows[j]["ID_cuota"].ToString(), ID_usuarioRegistraFK, idNomBase);
                        }
                        pricalcularPrimaBase(idNomina.ToString(), tbAfiliados.Rows[i], ID_empresaTemporal, ID_empresaUsuariaFK, ID_centroCosto, frecuenciaNomina, fechaInicio, fechaFin, SMLV, AuxTransp, ID_usuarioRegistraFK, idNomBase);
                    }
                    #endregion
                }//FIN CALCULAR PRIMA POR EMPLEADO

                //RETORNAR EL ID DE LA NÓMINA GENERADA
                return idNomina;
            }
            else
            {
                return 0;
            }
        }

        private string pricalcularPrimaBase(string idNomina, DataRow filaBase, string ID_empresaTemporal, string ID_empresaUsuariaFK, string ID_centroCosto, string frecuenciaNomina, string fechaInicio, string fechaFin
            , double SMLV, double AuxTransp , string ID_usuarioRegistraFK, string idNominaBase = "0")
        {
            //1) TRAIGO EL ACUMULADO Y LA MAXIMA FECHA DE NOMINA PAGADA
            DataTable tbAcumulados = dNomina.priConsultaAcumuladoXEmpleado((int)filaBase["ID_afiliadoFK"], (int)filaBase["ID_contrato"], fechaInicio, fechaFin);
            if ((tbAcumulados.Rows.Count > 0))
            {
                double primaAcumulada = 0;
                double totalPagar = 0;
                int otrosDescuento = 0;
                int diasLaborados;
                double primaProyectada = 0;
                int promedioAuxTransporte=0;
                int promedioSalario = 0;
                int promedioHE = 0;
                int promedioOtros = 0;
                double salarioProyectado = 0;

                try
                {
                    diasLaborados = (int)tbAcumulados.Rows[0]["DIAS"];
                }
                catch (Exception)
                {
                    throw;
                }

                if (tbAcumulados.Rows[0][0] != System.DBNull.Value)
                {
                    primaAcumulada = Convert.ToDouble(tbAcumulados.Rows[0]["valorPrima"]);
                    DateTime ultimaNomina = (DateTime)tbAcumulados.Rows[0]["nom_fechaFin"];
                    totalPagar = primaAcumulada;
                    

                    //int cuantasNominas = (int)tbAcumulados.Rows[0]["cuantasNominas"];

                    promedioAuxTransporte = Convert.ToInt32(tbAcumulados.Rows[0]["promedioAuxTrans"]);
                    promedioSalario = Convert.ToInt32(tbAcumulados.Rows[0]["promedioSalario"]);
                    promedioHE = Convert.ToInt32(tbAcumulados.Rows[0]["promedioHE"]);//nom_ddFSPEmpleado
                    promedioOtros = Convert.ToInt32(tbAcumulados.Rows[0]["promedioOtros"]);//nom_ddARLAfiliado

                    //si no tiene las nominas completas, calculo lo que falta
                    if (string.Format("{0:yyyy-MM-dd}", ultimaNomina) != fechaFin)
                    {
                        double salarioContrato = (double)filaBase["con_salario"];
                        if ((frecuenciaNomina == "Mensual") || ((frecuenciaNomina == "Quincenal") && ((ultimaNomina.Month != 6) && (ultimaNomina.Month != 12))))
                        {
                            //si es mensual y falta una nómina es la de junio 
                            if (((double)filaBase["con_salario"] < (SMLV * 2)) && ((filaBase["con_subsidioTransporte"].ToString() == "Si")))
                                salarioContrato += AuxTransp;
                            //diasLaborados += 30;
                            salarioProyectado = salarioContrato;
                        }
                        else
                        {
                            //me falta la segunda quicena
                            salarioContrato = salarioContrato / 2;
                            salarioProyectado = salarioContrato;
                            if (((double)filaBase["con_salario"] < (SMLV * 2)) && ((filaBase["con_subsidioTransporte"].ToString() == "Si")))
                                salarioContrato += AuxTransp / 2;
                            //diasLaborados += 15;
                        }

                        if (filaBase["con_jornada"].ToString() == "Medio Tiempo")
                        {
                            primaProyectada = (salarioContrato / 2) * 0.0833333;
                            salarioProyectado = salarioProyectado/2;
                        }
                        else
                            primaProyectada = (salarioContrato) * 0.0833333;

                        totalPagar += primaProyectada;
                    }
                }
                else
                { // no tiene acumulados, calcula la prima de los dias laborados 
                    if ((((double)filaBase["con_salario"] < (SMLV * 2)) && ((filaBase["con_subsidioTransporte"].ToString() == "Si"))) == false)
                        AuxTransp = 0;

                    primaProyectada = (((double)filaBase["con_salario"] + AuxTransp) * diasLaborados) / 360;
                    totalPagar = primaProyectada;
                }

                #region PRESTAMOS
                DataTable tbNovedades = idNominaBase == "0" ? null : dNomina.novConsultaLaboralesXNominaBase(idNominaBase, "'Prestamo'");
                if ((tbNovedades != null) && (tbNovedades.Rows.Count > 0) && (tbNovedades.Rows[0]["valorPesos"].ToString() != ""))
                {
                    otrosDescuento = Convert.ToInt32(tbNovedades.Rows[0]["nov_valor"]);
                }
                #endregion

                totalPagar -= otrosDescuento;

                #region GUARDO O ACTUALIZO LA PRIMA
                int diasPrimaLaborados;
                if (tbAcumulados.Rows[0]["diasPrimaLaborados"] != System.DBNull.Value)
                    diasPrimaLaborados = Convert.ToInt32(tbAcumulados.Rows[0]["diasPrimaLaborados"]);
                else
                    diasPrimaLaborados = diasLaborados;
                DataTable tbNomBase = dNomina.InsertarBase(idNominaBase, idNomina.ToString(), ID_empresaTemporal, ID_empresaUsuariaFK, filaBase["conID_centroCostoFK"].ToString(), filaBase["ID_contrato"].ToString(), filaBase["ID_afiliado"].ToString(),
                                         filaBase["afi_cuentaNumero"].ToString(), filaBase["ID_bancoFK"].ToString(), filaBase["nombreCuenta"].ToString(),
                                         filaBase["afi_EPS"].ToString(), filaBase["afi_AFP"].ToString(), filaBase["afi_CCF"].ToString(), filaBase["con_ARP"].ToString(),
                                         filaBase["ID_nivelRiesgo"].ToString(), (double)filaBase["con_salario"], primaAcumulada, diasLaborados, primaProyectada, 0, 0, 0, 0,
                                         primaAcumulada + primaProyectada, 0, 0, (int)tbAcumulados.Rows[0]["diasSolo360"] , salarioProyectado , Convert.ToInt32(primaAcumulada),
                                         promedioSalario, promedioAuxTransporte, promedioHE, promedioOtros, diasPrimaLaborados , 0, 0, otrosDescuento, 0, 0, 0, 0, 0, totalPagar, ID_usuarioRegistraFK, 0, 0, 0, 0, 0, 0, false, 0);
                #endregion

                if ((idNominaBase == "0") && (tbNomBase.Rows.Count > 0))
                    return tbNomBase.Rows[0]["ID_nominaBase"].ToString();
                else
                    return idNominaBase;
            }
            else
            { //NO TIENE NI UNA NOMINA, DEBO PROYECTAR TODO
            
            }

            return "";
        }

        public DataTable priConsultarXIDdetallado(double ID_nomina)
        {
            return dNomina.priConsultarXIDdetallado(ID_nomina);
        }

        #endregion

        #region CESANTIAS E INTERESES

        public Int64 cesCalcularCesantias(string ID_empresaTemporal, string ID_empresaUsuariaFK, string ID_centroCosto, string frecuenciaNomina, string año, string ID_usuarioRegistraFK, Int64 idNominaActualizar = 0)
        {
            string fechaInicio = año + "-01-01";
            string fechaFin = año + "-12-30";

            double SMLV = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(fechaFin).Year);
            double AuxTransp = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(fechaFin).Year, true);

            DataTable tbAfiliados = dNomina.cesConsultaAfiliadosXEmpresaSinCesantia(ID_empresaTemporal, ID_empresaUsuariaFK, ID_centroCosto, fechaInicio, fechaFin);
            if (tbAfiliados.Rows.Count > 0)
            {
                Int64 idNomina = idNominaActualizar == 0 ? NUtilidades.IdentificadorNomina(ID_empresaTemporal) : idNominaActualizar;
                dNomina.Insertar(idNomina, ID_empresaTemporal, ID_empresaUsuariaFK, "Cesantias", fechaInicio, fechaFin, "POR REVISAR", ID_usuarioRegistraFK);

                string idNomBase = "";
                //&&&&&&    SI HAY AFILIADOS PARA LIQUIDAR CESANTIA, CALCULO Y LUEGO INSERTO EL REGISTRO  &&&&&&&&&&&&
                for (int i = 0; i < tbAfiliados.Rows.Count; i++)
                {
                    idNomBase = cesCalcularCesantiaBase(idNomina.ToString(), tbAfiliados.Rows[i], ID_empresaTemporal, ID_empresaUsuariaFK, ID_centroCosto, fechaInicio, fechaFin, SMLV, AuxTransp, ID_usuarioRegistraFK);

                    #region VALIDAR SI TIENE PRESTAMOS POR ASOCIAR A ESTAS PRIMAS
                    NEmpleados nEmpleado = new NEmpleados();
                    DataTable tbPrestamos = nEmpleado.prestamoConsultaCuotasParaAplicar(tbAfiliados.Rows[i]["ID_afiliado"].ToString(), tbAfiliados.Rows[i]["ID_contrato"].ToString(), "", 3);
                    if ((idNomBase!="") && (tbPrestamos != null) && (tbPrestamos.Rows.Count > 0))
                    {
                        for (int j = 0; j < tbPrestamos.Rows.Count; j++)
                        {
                            novInsertar(idNomBase, "Prestamo", "Pesos", Convert.ToDouble(tbPrestamos.Rows[j]["cuo_valor"]), tbPrestamos.Rows[j]["cuo_valor"].ToString(), 0, fechaInicio, fechaFin, "Cuota Cesantia", ID_usuarioRegistraFK, false, false);
                            //**-- asociar este idNomina a la cuota de prima para descontarla como paga --**
                            nEmpleado.prestamoActualizaCuota(tbPrestamos.Rows[j]["ID_cuota"].ToString(), ID_usuarioRegistraFK, idNomBase);
                        }
                        cesCalcularCesantiaBase(idNomina.ToString(), tbAfiliados.Rows[i], ID_empresaTemporal, ID_empresaUsuariaFK, ID_centroCosto, fechaInicio, fechaFin, SMLV, AuxTransp, ID_usuarioRegistraFK, idNomBase);
                    }
                    #endregion
                }//FIN CALCULAR PRIMA POR EMPLEADO

                //RETORNAR EL ID DE LA NÓMINA GENERADA
                return idNomina;
            }
            else
            {
                return 0;
            }
        }

        private string cesCalcularCesantiaBase(string idNomina, DataRow filaBase, string ID_empresaTemporal, string ID_empresaUsuariaFK, string ID_centroCosto, string fechaInicio, string fechaFin
            , double SMLV, double AuxTransp, string ID_usuarioRegistraFK, string idNominaBase = "0")
        {
            //1) TRAIGO EL ACUMULADO Y LA MAXIMA FECHA DE NOMINA PAGADA
            string con_fechaInicio = string.Format("{0:yyyy-MM-dd}", filaBase["con_fechaInicio"]);
            if (DateTime.Compare(Convert.ToDateTime(filaBase["con_fechaInicio"]), Convert.ToDateTime(fechaInicio)) < 0) //ENTRO HACE MAS DE UN AÑO
                con_fechaInicio = fechaInicio;

            DataTable tbAcumulados = dNomina.liqConsultaCesantias((int)filaBase["ID_contrato"], fechaInicio, fechaFin, con_fechaInicio, fechaFin);
            
            if ((tbAcumulados.Rows.Count > 0) && (tbAcumulados.Rows[0]["ultimaNomina"] != System.DBNull.Value))
            {
                double valorCalculadoCesantias = 0;
                double valorCesantiasAcumuladas = 0;
                int interesCalculado = 0;
                double interesAcumulado = 0;
                double totalPagar = 0;
                double promedioIngresos, promedioHorasExtra;
                int promedioAuxTrans;
                double valorCesantiasProyectado = 0;
                int otrosDescuento = 0;
                DateTime ultimaNomina;
                int diasLaborados = 0;
                int dias360 = 0;

                diasLaborados = (int)tbAcumulados.Rows[0]["diasCesantias"];
                dias360 = (int)tbAcumulados.Rows[0]["dias360"];
                promedioIngresos = Convert.ToDouble(tbAcumulados.Rows[0]["promedioIngresos"]);
                promedioAuxTrans = Convert.ToInt32(tbAcumulados.Rows[0]["promedioAuxTrans"]);
                promedioHorasExtra = Convert.ToInt32(tbAcumulados.Rows[0]["promedioHorasExtra"]);

                valorCalculadoCesantias = Convert.ToDouble(tbAcumulados.Rows[0]["valorCesantiasCalculada"]);
                interesCalculado = Convert.ToInt32(tbAcumulados.Rows[0]["valorInteresCalculado"]);

                valorCesantiasAcumuladas = Convert.ToDouble(tbAcumulados.Rows[0]["valorCesantiasAcumuladas"]);
                interesAcumulado = Convert.ToDouble(tbAcumulados.Rows[0]["valorInteresAcumulados"]);
                
                #region PROYECTADO CESANTIAS
                ultimaNomina = (DateTime)tbAcumulados.Rows[0]["ultimaNomina"];
                int diasFaltantes=0;
                //si no tiene las nominas completas, calculo lo que falta
                if (string.Format("{0:yyyy-MM-dd}", ultimaNomina) != fechaFin)
                {
                    double salarioFaltante = (double)filaBase["con_salario"];

                    diasFaltantes = Negocio.NUtilidades.calculaDias360(ultimaNomina, Convert.ToDateTime(fechaFin));

                    salarioFaltante = ((salarioFaltante / 30) * diasFaltantes);
                    valorCesantiasProyectado = (((salarioFaltante + Convert.ToDouble(tbAcumulados.Rows[0]["promedioAuxTrans"])) / 360) * diasFaltantes);

                    //valorCalculadoCesantias = (((Convert.ToDouble(tbAcumulados.Rows[0]["ultimoSalario"]) + Convert.ToDouble(tbAcumulados.Rows[0]["promedioAuxTrans"])) / 360) * ((int)tbAcumulados.Rows[0]["diasCesantias"] + diasFaltantes));
                    interesCalculado = Convert.ToInt32((valorCalculadoCesantias + valorCesantiasProyectado) * 0.12 *(diasLaborados+diasFaltantes)/360);
                }
                #endregion

                #region PRESTAMOS A CESANTIAS
                DataTable tbNovedades = idNominaBase == "0" ? null : dNomina.novConsultaLaboralesXNominaBase(idNominaBase, "'Prestamo'");
                if ((tbNovedades != null) && (tbNovedades.Rows.Count > 0) && (tbNovedades.Rows[0]["valorPesos"].ToString() != ""))
                {
                    otrosDescuento = Convert.ToInt32(tbNovedades.Rows[0]["nov_valor"]);
                }
                #endregion

                totalPagar = valorCalculadoCesantias + valorCesantiasProyectado - otrosDescuento;

                #region GUARDO O ACTUALIZO REGISTRO DE CESANTIA
                DataTable tbNomBase = dNomina.InsertarBase(idNominaBase, idNomina.ToString(), ID_empresaTemporal, ID_empresaUsuariaFK, filaBase["conID_centroCostoFK"].ToString(), filaBase["ID_contrato"].ToString(), filaBase["ID_afiliado"].ToString(),
                                         filaBase["afi_cuentaNumero"].ToString(), filaBase["ID_bancoFK"].ToString(), filaBase["nombreCuenta"].ToString(),
                                         filaBase["afi_EPS"].ToString(), filaBase["afi_AFP"].ToString(), filaBase["afi_CCF"].ToString(), filaBase["con_ARP"].ToString(),
                                         filaBase["ID_nivelRiesgo"].ToString(), (double)filaBase["con_salario"], promedioIngresos, diasLaborados + diasFaltantes, valorCesantiasProyectado, valorCesantiasAcumuladas, interesAcumulado, Convert.ToInt32(interesCalculado), 0,
                                         valorCalculadoCesantias , promedioAuxTrans, Convert.ToInt32(valorCalculadoCesantias + valorCesantiasProyectado), diasLaborados, Convert.ToInt32(interesCalculado), Convert.ToInt32(valorCesantiasAcumuladas),
                                         dias360, promedioHorasExtra, 0, 0, 0, 0,0, otrosDescuento, 0, 0, 0,0, 0, totalPagar, ID_usuarioRegistraFK,0,0,0,0,0,0, false,0);
                #endregion

                if ((idNominaBase == "0") && (tbNomBase.Rows.Count > 0))
                    return tbNomBase.Rows[0]["ID_nominaBase"].ToString();
                else
                    return idNominaBase;
            }
            else
                return "";
        }

        public DataTable cesConsultarXID(double ID_nomina)
        {
            return dNomina.cesConsultarXID(ID_nomina);
        }

        public DataTable cesConsultarXIDdetallado(double ID_nomina)
        {
            return dNomina.cesConsultarXIDdetallado(ID_nomina);
        }

        public DataTable cesConsultarXID_ExcelMasivo(double ID_nomina)
        {
            return dNomina.cesConsultarXID_ExcelMasivo(ID_nomina);
        }

        public DataTable cesActualizaNomBase(string ID_nominaBase, string campoActualizar, double valorNuevo, string ID_usuarioActualizaFK)
        {
            return dNomina.cesActualizaNomBase(ID_nominaBase, campoActualizar, valorNuevo, ID_usuarioActualizaFK);
        }

        public DataTable cesConsultarXID_ExcelPago(double ID_nomina)
        {
            return dNomina.cesConsultarXID_ExcelPago(ID_nomina);
        }

        public DataTable cesConsultarEmpresas_ExcelPago(string ID_empresaTemporalFK, string año)
        {
            return dNomina.cesConsultarEmpresas_ExcelPago(ID_empresaTemporalFK, año);
        }

        public DataTable cesGenerarEmpresas_ExcelPago(string ID_empresaTemporalFK, string año, string EmpresasSeleccionadas)
        {
            return dNomina.cesGenerarEmpresas_ExcelPago(ID_empresaTemporalFK, año, EmpresasSeleccionadas);
        }

        public DataTable cesInsertaPago(string ces_nit, string tipo_doc, string num_doc, string Apellido1, string Apellido2, string Nombre1, string Nombre2, string ces_Codigo, string ces_Nombre,
            double ces_SalarioBase, double ces_ValorPagado, int ces_Periodo, int ces_TotalEmpleados, string ces_sucursal, string ces_clave, string ces_transaccion, string ces_banco, string ces_fechaPago,
            double ces_totalPagado, double ces_totalPagadoInteres, string ces_ArchivoPlanilla, string ID_usuarioRegistra
            )
        {
            return dNomina.cesInsertaPago( ces_nit,  tipo_doc,  num_doc,  Apellido1,  Apellido2,  Nombre1,  Nombre2,  ces_Codigo,  ces_Nombre,
                                 ces_SalarioBase,  ces_ValorPagado,  ces_Periodo,  ces_TotalEmpleados,  ces_sucursal,  ces_clave,  ces_transaccion,  ces_banco,  ces_fechaPago,
                                 ces_totalPagado,  ces_totalPagadoInteres,  ces_ArchivoPlanilla,  ID_usuarioRegistra);
        }
        #endregion

        #region SEGURIDAD SOCIAL

        public void segInsertarSOI(string soi_nit, string tipo_doc, string num_doc, string tipo_cotizante, string subtipo_cotizante, string Cod_depar, string Cod_mun
           , string Apellido1, string Apellido2, string Nombre1, string Nombre2, string ING, string RET, string TDE, string TAE, string TDP, string TAP, string VSP
           , string COR, string VST, string SLN, string IGE, string LMA, string VAC, string AVP, string VCT, string IRP, string VIP
           , string AFP_CODIGO, string AFP_DIAS, double AFP_IBC, double AFP_APORTE
           , string EPS_CODIGO, string EPS_DIAS, double EPS_IBC, double EPS_APORTE, string CCF_CODIGO, string CCF_DIAS, double CCF_IBC, double CCF_APORTE
           , string ARL_CODIGO, string ARL_DIAS, double ARL_IBC, double ARL_APORTE, string PARAF_DIAS, double PARAF_IBC, double PARAF_APORTE
           , String soi_periodoSalud, String soi_periodoPension, String soi_sucursal, String soi_planilla, string soi_banco, String soi_fechaPago
           , string soi_codigoPago, double soi_totalPagado, String soi_ArchivoPlanilla, String ID_usuarioRegistra)
        {
            dNomina.segInsertarSOI( soi_nit,  tipo_doc,  num_doc,  tipo_cotizante,  subtipo_cotizante,  Cod_depar,  Cod_mun
           , Apellido1,  Apellido2,  Nombre1,  Nombre2, ING, RET, TDE, TAE, TDP, TAP, VSP, COR, VST, SLN, IGE, LMA, VAC, AVP, VCT, IRP, VIP
           , AFP_CODIGO,  AFP_DIAS,  AFP_IBC,  AFP_APORTE,  EPS_CODIGO,  EPS_DIAS,  EPS_IBC,  EPS_APORTE,  CCF_CODIGO,  CCF_DIAS,  CCF_IBC,  CCF_APORTE
           , ARL_CODIGO,  ARL_DIAS,  ARL_IBC,  ARL_APORTE,  PARAF_DIAS,  PARAF_IBC,  PARAF_APORTE
           , soi_periodoSalud,  soi_periodoPension,  soi_sucursal,  soi_planilla,  soi_banco,  soi_fechaPago
           , soi_codigoPago,  soi_totalPagado,  soi_ArchivoPlanilla,  ID_usuarioRegistra);
        }

        #endregion
        
        #region REPORTES DE PROVISIONES
        public double[] provPrimas(string ID_empresaTemporal, string ID_empresaUsuariaFK, string ID_centroCosto, string filCampo, string filFiltro, string fechaInicio, string fechaFin)
        {
            double SMLV = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(fechaFin).Year);
            double AuxTransp = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(fechaFin).Year, true);

            //1) busco los empleados activos 
            DataTable tbAfiliados = dNomina.provEmpleadosActivosXEmpresa(ID_empresaTemporal, ID_empresaUsuariaFK, filCampo, filFiltro);
            double[] totalPrimas = new double[3];
            if (tbAfiliados.Rows.Count > 0)
            {
                //**--- CALCULO PRIMA PARA CADA EMPLEADO ACTIVO  --** 
                for (int i = 0; i < tbAfiliados.Rows.Count; i++)
                {
                     double[] resultado = provPrimaBase(tbAfiliados.Rows[i], ID_empresaTemporal, ID_empresaUsuariaFK, fechaInicio, fechaFin, SMLV, AuxTransp);
                    totalPrimas[0] +=resultado[0];
                    totalPrimas[1] += resultado[1];
                    totalPrimas[2] += resultado[2];
                }//FIN CALCULAR PRIMA POR EMPLEADO

                //RETORNAR EL ID DE LA NÓMINA GENERADA
            }
            return totalPrimas;
        }

        private double[] provPrimaBase(DataRow filaBase, string ID_empresaTemporal, string ID_empresaUsuariaFK, string fechaInicio, string fechaFin, double SMLV, double AuxTransp)
        {
            //1) TRAIGO EL ACUMULADO Y LA MAXIMA FECHA DE NOMINA PAGADA
            DataTable tbAcumulados = dNomina.priConsultaAcumuladoXEmpleado((int)filaBase["ID_afiliadoFK"], (int)filaBase["ID_contrato"], fechaInicio, fechaFin);
            double valorPrimaProyectada = 0;
            double valorPrimaCalculada = 0;
            DateTime ultimaNomina;
            int diasLaborados = (int)tbAcumulados.Rows[0]["DIAS"];
            double primaAcumulada = 0;

            if ((tbAcumulados.Rows.Count > 0) && (tbAcumulados.Rows[0]["nom_fechaFin"] != System.DBNull.Value))
            {
                valorPrimaCalculada = Convert.ToDouble(tbAcumulados.Rows[0]["valorPrima"]);
                primaAcumulada = Convert.ToDouble(tbAcumulados.Rows[0]["provPrima"]);
                ultimaNomina = (DateTime)tbAcumulados.Rows[0]["nom_fechaFin"];
                //si no tiene las nominas completas, calculo lo que falta
                if (string.Format("{0:yyyy-MM-dd}", ultimaNomina) != fechaFin)
                {
                    double salarioFaltante = (double)filaBase["con_salario"];
                    double auxTransFaltante = 0;
                    double auxSinpromedio = Convert.ToDouble(tbAcumulados.Rows[0]["auxTransSinPromedio"]);
                    double salarioEPS = Convert.ToDouble(tbAcumulados.Rows[0]["salarioEPS"]);

                    if (((double)filaBase["con_salario"] < (SMLV * 2)) && ((filaBase["con_subsidioTransporte"].ToString() == "Si")))
                        auxTransFaltante = AuxTransp;

                    int diasFaltantes = Negocio.NUtilidades.calculaDias360(ultimaNomina, Convert.ToDateTime(fechaFin));
                    //le quito un dia, por que coje desde el dia de la ultima nomina, y debe ser desde el dia siguiente
                    diasFaltantes--;
                    salarioFaltante = (salarioFaltante / 30) * diasFaltantes;
                    auxTransFaltante = (auxTransFaltante / 30) * diasFaltantes;

                    //valorPrimaProyectada = (((((salarioFaltante + (double)tbAcumulados.Rows[0]["dvSalario"] + (double)tbAcumulados.Rows[0]["dvOtros"] + (double)tbAcumulados.Rows[0]["valorAusentismo"]) / diasLaborados) * 30) + (((Convert.ToDouble(tbAcumulados.Rows[0]["auxTrans"]) + auxTransFaltante) / diasLaborados) * 30)) * diasLaborados) / 360;
                    valorPrimaProyectada = (((((salarioFaltante + salarioEPS) / diasLaborados) * 30) + (((Convert.ToDouble(tbAcumulados.Rows[0]["auxTransSinPromedio"]) + auxTransFaltante) / diasLaborados) * 30)) * diasLaborados) / 360;
                }

            }
            else
            { // no tiene acumulados, calcula la prima de los dias laborados 
                if ((((double)filaBase["con_salario"] < (SMLV * 2)) && ((filaBase["con_subsidioTransporte"].ToString() == "Si"))) == false)
                    AuxTransp = 0;
                
                valorPrimaProyectada = (((double)filaBase["con_salario"] + AuxTransp) * diasLaborados) / 360;
            }
            double[] retornar = { primaAcumulada, valorPrimaCalculada, valorPrimaProyectada };
            return retornar;
        }

        public double[] provCesantias(string ID_empresaTemporal, string ID_empresaUsuariaFK, string filCampo, string filFiltro, string fechaInicio, string fechaFin)
        {
            //1) busco los empleados activos 
            DataTable tbAfiliados = dNomina.provEmpleadosActivosXEmpresa(ID_empresaTemporal, ID_empresaUsuariaFK, filCampo, filFiltro);
            double SMLV = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(fechaFin).Year);
            double AuxTransp = NUtilidades.consultarSalarioMinimo(Convert.ToDateTime(fechaFin).Year, true);

            double[] totalCesantias = new double[5];
            if (tbAfiliados.Rows.Count > 0)
            {
                //**--- CALCULO LAS CESANTIAS POR EMPLEADO  --** 
                for (int i = 0; i < tbAfiliados.Rows.Count; i++)
                {
                    double[] resultado = provCesantiasBase(tbAfiliados.Rows[i], ID_empresaTemporal, ID_empresaUsuariaFK, fechaInicio, fechaFin,SMLV, AuxTransp);
                    totalCesantias[0] += resultado[0];
                    totalCesantias[1] += resultado[1];
                    totalCesantias[2] += resultado[2];
                    totalCesantias[3] += resultado[3];
                    totalCesantias[4] += resultado[4];
                }//FIN CALCULAR PRIMA POR EMPLEADO

                //RETORNAR EL ID DE LA NÓMINA GENERADA
            }
            return totalCesantias;
        }

        private double[] provCesantiasBase(DataRow filaBase, string ID_empresaTemporal, string ID_empresaUsuariaFK, string fechaInicio, string fechaFin, double SMLV, double AuxTransp)
        {
            //1) TRAIGO EL ACUMULADO Y LA MAXIMA FECHA DE NOMINA PAGADA
            string con_fechaInicio = string.Format("{0:yyyy-MM-dd}", filaBase["con_fechaInicio"]);
            //valido que la fecha de inicio del contrato sea inferior ala fecha de fin a calcular - 2020
            if (DateTime.Compare(Convert.ToDateTime(filaBase["con_fechaInicio"]), Convert.ToDateTime(fechaFin)) <= 0)
            {
                if (DateTime.Compare(Convert.ToDateTime(filaBase["con_fechaInicio"]), Convert.ToDateTime(fechaInicio)) < 0)
                con_fechaInicio = fechaInicio;

                DataTable tbAcumulados = dNomina.liqConsultaCesantias((int)filaBase["ID_contrato"], fechaInicio, fechaFin, con_fechaInicio, fechaFin);

                double valorCesantiasAcumuladas = 0;
                double valorCalculadoCesantias = 0;
                double valorPoyectado = 0;
                double interesCalculado = 0;
                double interesAcumulado = 0;

                DateTime ultimaNomina;
                int diasLaborados = 0;
            
                if ((tbAcumulados.Rows.Count > 0) && (tbAcumulados.Rows[0]["ultimaNomina"] != System.DBNull.Value))
                {
                    diasLaborados = (int)tbAcumulados.Rows[0]["diasCesantias"];
                    valorCalculadoCesantias = Convert.ToDouble(tbAcumulados.Rows[0]["valorCesantiasCalculada"]);
                    interesCalculado = Convert.ToDouble(tbAcumulados.Rows[0]["valorInteresCalculado"]);

                    valorCesantiasAcumuladas = Convert.ToDouble(tbAcumulados.Rows[0]["valorCesantiasAcumuladas"]);
                    interesAcumulado = Convert.ToDouble(tbAcumulados.Rows[0]["valorInteresAcumulados"]);

                    ultimaNomina = (DateTime)tbAcumulados.Rows[0]["ultimaNomina"];
                    //si no tiene las nominas completas, calculo lo que falta
                    if (string.Format("{0:yyyy-MM-dd}", ultimaNomina) != fechaFin)
                    {
                        double salarioFaltante = (double)filaBase["con_salario"];
                        int diasFaltantes = Negocio.NUtilidades.calculaDias360(ultimaNomina, Convert.ToDateTime(fechaFin));

                        if ((((double)filaBase["con_salario"] < (SMLV * 2)) && ((filaBase["con_subsidioTransporte"].ToString() == "Si"))) == false)
                            AuxTransp = 0;

                        //salarioFaltante = ((salarioFaltante/30) + (AuxTransp / 30)) * diasFaltantes;
                        valorPoyectado = (((double)filaBase["con_salario"] + AuxTransp) * diasFaltantes) / 360;

                        //valorCalculadoCesantias = (((Convert.ToDouble(tbAcumulados.Rows[0]["ultimoSalario"]) + Convert.ToDouble(tbAcumulados.Rows[0]["promedioAuxTrans"])) / 360) * ((int)tbAcumulados.Rows[0]["diasCesantias"] + diasFaltantes));
                        interesCalculado = (valorCalculadoCesantias + valorPoyectado )* 0.12;
                    }
                }
                else
                { // no tiene acumulados, calcula cesantias de los dias de laborados
                    if ((((double)filaBase["con_salario"] < (SMLV * 2)) && ((filaBase["con_subsidioTransporte"].ToString() == "Si"))) == false)
                        AuxTransp = 0;
                    valorPoyectado = ((((double)filaBase["con_salario"] + AuxTransp) * ((int)tbAcumulados.Rows[0]["dias360"]))/360);
                    interesCalculado = valorPoyectado * 0.12;
                }
                double[] retornar = {valorCesantiasAcumuladas, valorCalculadoCesantias,(valorCalculadoCesantias + valorPoyectado), interesAcumulado, interesCalculado };
                return retornar;
            }
            else
            {//es de un contrato con fecha superior a la que me piden calcular
                double[] retornar = { 0, 0, 0, 0, 0 };
                return retornar;
            }
}

        public DataTable provCesantiaBase(int ID_contratoFK, string con_fechaInicio, string con_fechaFin)
        {
            string fechaInicio = string.Format("{0:yyyy-01-01}", DateTime.Today);
            string fechaFin = string.Format("{0:yyyy-12-31}", DateTime.Today);

            if (DateTime.Compare(Convert.ToDateTime(con_fechaInicio), Convert.ToDateTime(fechaInicio)) < 0)
                con_fechaInicio = fechaInicio;

            return dNomina.liqConsultaCesantias(ID_contratoFK, fechaInicio, fechaFin, con_fechaInicio, con_fechaFin);
        }
        
        #endregion
    }
}
