using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;

namespace Datos
{
    public class DNomina : DSQL
    {
        #region NOMINA
        public DataTable ConsultaAfiliadosXEmpresaSinNomina(string ID_empresaTemporal, string ID_empresaUsuariaFK, string ID_centroCosto,string fechaInicio, string  fechaFin)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporal", ID_empresaTemporal);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@ID_centroCosto", ID_centroCosto);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);

            return procedureTable("nom_ConsultaAfiliadosXEmpresaSinNomina", true, param);
        }

        public DataTable ConsultaAfiliadosXEmpresaSinNominaIndividual(string ID_empresaTemporal, string ID_empresaUsuariaFK, string ID_centroCosto, string fechaInicio, string fechaFin, string afi_documento)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporal", ID_empresaTemporal);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@ID_centroCosto", ID_centroCosto);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@afi_documento", afi_documento);

            return procedureTable("nom_ConsultaAfiliadosXEmpresaSinNominaIndividual", true, param);
        }

        public DataTable ConsolidarNominasIndividuales(string ID_nominaFK,string ID_empresaTemporalFK, string ID_empresaUsuariaFK, string ID_centroCosto, string nom_tipoNomina, string nom_fechaInicio, string nom_fechaFin, string cli_tipoFacturacion)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaFK", ID_nominaFK);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@ID_centroCosto", ID_centroCosto);
            param.Add("@nom_tipoNomina", nom_tipoNomina);
            param.Add("@nom_fechaInicio", nom_fechaInicio);
            param.Add("@nom_fechaFin", nom_fechaFin);
            param.Add("@cli_tipoFacturacion", cli_tipoFacturacion);

            return procedureTable("nom_ConsolidarNominasIndividuales", true, param);
        }

        public DataTable InsertarBase(string ID_nominaBase,string ID_nominaFK, string ID_empresaTemporalFK, string ID_empresaUsuariaFK, string ID_centroCostoFK, string ID_contratoFK, string ID_afiliadoFK
                                     , string nom_cuentaAfiliado, string nom_cuentaBanco, string nom_cuentaNombre, string nom_soiEPS, string nom_soiAFP,string nom_soiCCF, string nom_soiARL
                                     , string nom_nivelRiesgo, double nom_salarioContrato, double nom_salarioBase, int nom_dias, double nom_salarioDevengado, double nom_salarioEPS, double nom_salarioAFP, double nom_salarioCCF, double nom_salarioARL
                                     , double nom_dvSalario, double nom_dvAuxilioTransporte, double nom_dvIncapacidad, int nom_diasIncapacidad, double nom_dvOtros, double nom_dvOtrosNoPrestacional
                                     , double nom_ddEPSafiliado, double nom_ddAFPafiliado, double nom_ddFSPPafiliado, double nom_ddARLafiliado, double nom_ddCCFafiliado, double nom_ddPARAFafiliado, double nom_ddReteFuente, double nom_ddOtros
                                     , double nom_EPSempresa, double nom_AFPempresa, double nom_ARLempresa, double nom_CCFempresa, double nom_PARAFempresa, double nom_TotalPagar, string ID_usuarioRegistraFK, int nom_diasNoLaborados
                                     , double nom_ProvPrima ,double nom_ProvCesantias, double nom_ProvIntereses, double nom_ProvVacaciones, double nom_ProvTotal, bool nom_IndividualLiquidacion, double nom_comisiones)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaBase", ID_nominaBase);
            param.Add("@ID_nominaFK", ID_nominaFK);
            param.Add("@ID_empresaTemporalFK",ID_empresaTemporalFK);
            param.Add("@ID_empresaUsuariaFK",ID_empresaUsuariaFK);
            param.Add("@ID_centroCostoFK",ID_centroCostoFK);
            param.Add("@ID_contratoFK",ID_contratoFK);
            param.Add("@ID_afiliadoFK",ID_afiliadoFK);
            param.Add("@nom_cuentaAfiliado",nom_cuentaAfiliado);
            param.Add("@nom_cuentaBanco",nom_cuentaBanco);
            param.Add("@nom_cuentaNombre",nom_cuentaNombre);
            param.Add("@nom_soiEPS",nom_soiEPS);
            param.Add("@nom_soiAFP",nom_soiAFP);
            param.Add("@nom_soiCCF",nom_soiCCF);
            param.Add("@nom_soiARL",nom_soiARL);
            param.Add("@nom_nivelRiesgo",nom_nivelRiesgo);
            param.Add("@nom_salarioContrato", nom_salarioContrato);
            param.Add("@nom_salarioBase", nom_salarioBase);
            param.Add("@nom_dias",nom_dias);
            param.Add("@nom_salarioDevengado", nom_salarioDevengado);
            param.Add("@nom_salarioEPS", nom_salarioEPS);
            param.Add("@nom_salarioAFP", nom_salarioAFP);
            param.Add("@nom_salarioCCF", nom_salarioCCF);
            param.Add("@nom_salarioARL", nom_salarioARL);
            param.Add("@nom_dvSalario", nom_dvSalario);
            param.Add("@nom_dvAuxilioTransporte", nom_dvAuxilioTransporte);
            param.Add("@nom_dvIncapacidad", nom_dvIncapacidad);
            param.Add("@nom_diasIncapacidad", nom_diasIncapacidad);
            param.Add("@nom_dvOtros", nom_dvOtros);
            param.Add("@nom_dvOtrosNoPrestacional", nom_dvOtrosNoPrestacional);
            param.Add("@nom_ddEPSafiliado",nom_ddEPSafiliado);
            param.Add("@nom_ddAFPafiliado",nom_ddAFPafiliado);
            param.Add("@nom_ddFSPafiliado", nom_ddFSPPafiliado);
            param.Add("@nom_ddARLafiliado",nom_ddARLafiliado);
            param.Add("@nom_ddCCFafiliado",nom_ddCCFafiliado);
            param.Add("@nom_ddPARAFafiliado", nom_ddPARAFafiliado);
            param.Add("@nom_ddReteFuente", nom_ddReteFuente);
            param.Add("@nom_ddOtros", nom_ddOtros);
            param.Add("@nom_EPSempresa",nom_EPSempresa);
            param.Add("@nom_AFPempresa",nom_AFPempresa);
            param.Add("@nom_ARLempresa",nom_ARLempresa);
            param.Add("@nom_CCFempresa",nom_CCFempresa);
            param.Add("@nom_PARAFempresa",nom_PARAFempresa);
            param.Add("@nom_TotalPagar",nom_TotalPagar);
            param.Add("@ID_usuarioRegistraFK",ID_usuarioRegistraFK);
            param.Add("@nom_diasNoLaborados", nom_diasNoLaborados);
            param.Add("@nom_ProvPrima", nom_ProvPrima);
            param.Add("@nom_ProvCesantias", nom_ProvCesantias);
            param.Add("@nom_ProvIntereses", nom_ProvIntereses);
            param.Add("@nom_ProvVacaciones", nom_ProvVacaciones);
            param.Add("@nom_ProvTotal", nom_ProvTotal);

            param.Add("@nom_IndividualLiquidacion", nom_IndividualLiquidacion);
            param.Add("@nom_comisiones", nom_comisiones);
            
            return procedureTable("nom_InsertarNomBase", true, param);
        }

        public void InsertarReporte(string ID_nominaBaseFK, string ID_nominaFK , string ID_empresaTemporalFK , string ID_empresaUsuariaFK , string ID_centroCostoFK , string ID_contratoFK 
           , int rep_diasEPS , int rep_diasAFP , int rep_diasARL , int rep_diasCCF , int rep_diasPARAF , int rep_diasLaborados , int rep_diasIncapacidad , double rep_valorIncapacidad 
           , int rep_diasIncapSoat , double rep_valorIncapSoat , int rep_diasIncapARL , double rep_valorIncapARL , int rep_diasLicenciaMaternidad , double rep_valorLicenciaMaternidad 
           , int rep_diasLicenciaPaternidad , double rep_valorLicenciaPaternidad , int rep_diasLicenciaNoRemunerada , double rep_valorLicenciaNoRemunerada , int rep_diasLicenciaRemunerada 
           , double rep_valorLicenciaRemunerada , int rep_diasLicenciaLuto , double rep_valorLicenciaLuto , int rep_diasVacaciones , double rep_valorVacaciones , int rep_diasSancion 
           , double rep_valorSancion , double rep_HED , double rep_valorHED , double rep_HEN , double rep_valorHEN , double rep_HEDD , double rep_valorHEDD , double rep_HEND , double rep_valorHEND 
           , double rep_HDDO , double rep_valorHDDO , double rep_HDDOR , double rep_valorHDDOR , double rep_HNDOR , double rep_valorHNDOR 
           , double rep_RN , double rep_valorRN , double rep_RND , double rep_valorRND, int rep_diasDevolucionLNR, double rep_valorDevolucionLNR)
        {
            Hashtable param = new Hashtable(1);

            param.Add("@ID_nominaBaseFK",ID_nominaBaseFK);
            param.Add("@ID_nominaFK", ID_nominaFK);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@ID_centroCostoFK", ID_centroCostoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@rep_diasEPS", rep_diasEPS);
            param.Add("@rep_diasAFP", rep_diasAFP);
            param.Add("@rep_diasARL", rep_diasARL);
            param.Add("@rep_diasCCF", rep_diasCCF);
            param.Add("@rep_diasPARAF", rep_diasPARAF);
            param.Add("@rep_diasLaborados", rep_diasLaborados);
            param.Add("@rep_diasIncapacidad", rep_diasIncapacidad);
            param.Add("@rep_valorIncapacidad", rep_valorIncapacidad);
            param.Add("@rep_diasIncapSoat", rep_diasIncapSoat);
            param.Add("@rep_valorIncapSoat", rep_valorIncapSoat);
            param.Add("@rep_diasIncapARL", rep_diasIncapARL);
            param.Add("@rep_valorIncapARL", rep_valorIncapARL);
            param.Add("@rep_diasLicenciaMaternidad", rep_diasLicenciaMaternidad);
            param.Add("@rep_valorLicenciaMaternidad", rep_valorLicenciaMaternidad);
            param.Add("@rep_diasLicenciaPaternidad", rep_diasLicenciaPaternidad);
            param.Add("@rep_valorLicenciaPaternidad", rep_valorLicenciaPaternidad);
            param.Add("@rep_diasLicenciaNoRemunerada", rep_diasLicenciaNoRemunerada);
            param.Add("@rep_valorLicenciaNoRemunerada", rep_valorLicenciaNoRemunerada);
            param.Add("@rep_diasLicenciaRemunerada", rep_diasLicenciaRemunerada);
            param.Add("@rep_valorLicenciaRemunerada", rep_valorLicenciaRemunerada);
            param.Add("@rep_diasLicenciaLuto", rep_diasLicenciaLuto);
            param.Add("@rep_valorLicenciaLuto", rep_valorLicenciaLuto);
            param.Add("@rep_diasVacaciones", rep_diasVacaciones);
            param.Add("@rep_valorVacaciones", rep_valorVacaciones);
            param.Add("@rep_diasSancion", rep_diasSancion);
            param.Add("@rep_valorSancion", rep_valorSancion);
            param.Add("@rep_HED", rep_HED);
            param.Add("@rep_valorHED", rep_valorHED);
            param.Add("@rep_HEN", rep_HEN);
            param.Add("@rep_valorHEN", rep_valorHEN);
            param.Add("@rep_HEDD", rep_HEDD);
            param.Add("@rep_valorHEDD", rep_valorHEDD);
            param.Add("@rep_HEND", rep_HEND);
            param.Add("@rep_valorHEND", rep_valorHEND);
            param.Add("@rep_HDDO", rep_HDDO);
            param.Add("@rep_valorHDDO", rep_valorHDDO);
            param.Add("@rep_HDDOR", rep_HDDOR);
            param.Add("@rep_valorHDDOR", rep_valorHDDOR);
            param.Add("@rep_HNDOR", rep_HNDOR);
            param.Add("@rep_valorHNDOR", rep_valorHNDOR);
            param.Add("@rep_RN", rep_RN);
            param.Add("@rep_valorRN", rep_valorRN);
            param.Add("@rep_RND", rep_RND);
            param.Add("@rep_valorRND", rep_valorRND);
            param.Add("@rep_diasDevolucionLNR", rep_diasDevolucionLNR);
            param.Add("@rep_valorDevolucionLNR", rep_valorDevolucionLNR);

            procedureTable("nom_InsertarNomReporte", true, param);

        }

        public DataTable Insertar(double ID_nomina, string ID_empresaTemporalFK, string ID_empresaUsuariaFK, string nom_tipoNomina, string nom_fechaInicio, string nom_fechaFin, string nom_tipoPago, string ID_usuarioRegistraFK, bool nom_Individual = false)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@nom_tipoNomina", nom_tipoNomina);
            param.Add("@nom_fechaInicio", nom_fechaInicio);
            param.Add("@nom_fechaFin", nom_fechaFin);
            param.Add("@nom_tipoPago", nom_tipoPago);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
            param.Add("@nom_Individual", nom_Individual);

            return procedureTable("nom_Insertar", true, param);
        }

        public DataTable ActualizaEstado(string ID_nomina, string nom_observacion, string nom_estado, string ID_usuarioActualiza)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);
            param.Add("@nom_observacion", nom_observacion);
            param.Add("@nom_estado", nom_estado);
            param.Add("@ID_usuarioActualiza", ID_usuarioActualiza);

            return procedureTable("nom_ActualizaEstado", true, param);
        }

        public DataTable ApruebaUsuaria(string ID_nomina, string nom_observacion, string nom_fechaApruebaDesembolso, string ID_usuarioActualiza)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);
            param.Add("@nom_observacion", nom_observacion);
            param.Add("@nom_fechaApruebaDesembolso", nom_fechaApruebaDesembolso);
            param.Add("@ID_usuarioActualiza", ID_usuarioActualiza);

            return procedureTable("nom_ApruebaUsuaria", true, param);
        }

        public DataTable nom_ActualizaSoportePago(string ID_nomina, string nom_tipoSoporte, string nom_soportePago, string nom_observacionSoporte, string ID_usuarioSoportePago)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);
            param.Add("@nom_tipoSoporte", nom_tipoSoporte);
            param.Add("@nom_soportePago", nom_soportePago);
            param.Add("@nom_observacionSoporte", nom_observacionSoporte);
            param.Add("@ID_usuarioSoportePago", ID_usuarioSoportePago);

            return procedureTable("nom_ActualizaSoportePago", true, param);
        }

        public DataTable ActualizaNomBaseObservacionUsuaria(string ID_nominaBase, string nom_observacionUsuria, string ID_usuarioObservacionUsuariaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaBase", ID_nominaBase);
            param.Add("@nom_observacionUsuria", nom_observacionUsuria);
            param.Add("@ID_usuarioObservacionUsuariaFK", ID_usuarioObservacionUsuariaFK);

            return procedureTable("nom_ActualizaNomBaseObservacionUsuaria", true, param);
        }

        public DataTable MigracionAcumulados(string ID_nomina, double totalIngresos, double totalAuxilio, double totalHorasExtra, double salarioAnterior)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);
            param.Add("@totalIngresos", totalIngresos);
            param.Add("@totalAuxilio", totalAuxilio);
            param.Add("@totalHorasExtra", totalHorasExtra);
            param.Add("@salarioAnterior", salarioAnterior);

            return procedureTable("nom_MigracionAcumulados", true, param);
        }

        public void EliminaNominaBase(string ID_nominaBase)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaBase", ID_nominaBase);

            procedureTable("nom_EliminarNomBase", true, param);
        }

        public void NoGirar(string ID_nominaBase, bool nom_noGirar)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaBase", ID_nominaBase);
            param.Add("@nom_noGirar", nom_noGirar);

            procedureTable("nom_NoGirar", true, param);
        }

        public DataTable Unificar(string ID_nomina1, string ID_nomina2, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina1", ID_nomina1);
            param.Add("@ID_nomina2", ID_nomina2);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            return procedureTable("nom_Unificar", true, param);
        }

        public DataTable Eliminar(string ID_nomina, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            return procedureTable("nom_Eliminar", true, param);
        }

        public DataTable HistoricoInsertar(string ID_nomina, string ID_nominaUnificada, string ID_usuarioRegistraFK, string his_tipoMovimiento)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);
            param.Add("@ID_nominaUnificada", ID_nominaUnificada);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
            param.Add("@his_tipoMovimiento", his_tipoMovimiento);

            return procedureTable("nom_HistoricoInsertar", true, param);
        }

        public DataTable Consultar(string nom_tipoNomina, string where)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@nom_tipoNomina", nom_tipoNomina);
            param.Add("@where", where);

            return procedureTable("nom_Consultar", true, param );
        }
        
        public DataTable ConsultaEstadosXPerfil(string estados)
        {
            return queryTable("SELECT * FROM app_Referencias WHERE ref_modulo='EstadosNomina' AND ref_valor IN (" + estados + ")");
        }

        public DataTable ConsultaEstadosLiquidacionXPerfil(string estados)
        {
            return queryTable("SELECT * FROM app_Referencias WHERE ref_modulo='EstadosLiquidacion' ");//AND ref_valor IN (" + estados + ")
        }

        public DataTable ConsultarXID(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("nom_ConsultarXID", true, param);
        }

        public DataTable ConsultarXIDXBancoDispersion(double ID_nomina, string bancoOrigen, string bancoDestino)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);
            param.Add("@bancoOrigen", bancoOrigen);
            param.Add("@bancoDestino", bancoDestino);

            return procedureTable("nom_ConsultarXIDXBancoDispersion", true, param);
        }

        public DataTable ConsultarMasivo(string idNominas, int tipoReporte)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@idNominas", idNominas);
            param.Add("@tipoReporte", tipoReporte);

            return procedureTable("nom_ConsultarMasivo", true, param);
        }

        public DataTable ConsultarMasivoXBancoDispersion(string idNominas, string bancoOrigen, string bancoDestino)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@idNominas", idNominas);
            param.Add("@bancoOrigen", bancoOrigen);
            param.Add("@bancoDestino", bancoDestino);

            return procedureTable("nom_ConsultarMasivoXBancoDispersion", true, param);
        }

        public DataTable ConsultarMasivoXBancoDispersionResumen(string idNominas, string bancoOrigen, string bancoDestino)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@idNominas", idNominas);
            param.Add("@bancoOrigen", bancoOrigen);
            param.Add("@bancoDestino", bancoDestino);

            return procedureTable("nom_ConsultarMasivoXBancoDispersionResumen", true, param);
        }

        public DataTable ConsultaNominaAnteriorXContrato(int ID_contratoFK, int ID_afiliadoFK, string ID_empresaTemporalFK, string nom_fechaInicio, string nom_fechaFin)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@nom_fechaInicio", nom_fechaInicio);
            param.Add("@nom_fechaFin", nom_fechaFin);

            return procedureTable("nom_ConsultaNominaAnteriorXContrato", true, param);
        }

        public DataTable ConsultaNomAnteriorXContratoTotal(string ID_contratoFK, string nom_fechaInicio)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@nom_fechaInicio", nom_fechaInicio);

            return procedureTable("nom_ConsultaNomAnteriorXContratoTotal", true, param);
        }

        public DataTable ConsultarXIDCuboDetallado(string ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("nom_ConsultarXIDCuboDetallado", true, param);
        }

        public DataTable ConsultarXIDDetalladoTotales(string ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("nom_ConsultarXIDdetalladoTotales", true, param);
        }

        public DataTable ConsultarXIDdetallado(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);
        
            return procedureTable("nom_ConsultarXIDdetallado", true, param);
        }

        public DataTable ConsultarXID_ExcelHorasExtra(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("nom_ConsultarXID_ExcelHorasExtra", true, param);
        }

        public DataTable ConsultarXID_ExcelMasivo(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("nom_ConsultarXID_ExcelMasivo", true, param);
        }

        public DataTable ConsultarXID_ExcelRetiroMasivo(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("nom_ConsultarXID_ExcelRetiroMasivo", true, param);
        }

        public DataTable ConsultarXID_ExcelNovGeneral(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("nom_ConsultarXID_ExcelNovGeneral", true, param);
        }

        public DataTable ConsultarXID_EmpledosEnCero(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("nom_ConsultarXID_EmpledosEnCero", true, param);
        }

        public DataTable ConsultarXIDconceptos(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("nom_ConsultarXIDConceptos", true, param);
        }

        public DataTable ConsultarXidNomBase(string ID_nominaBase)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaBase", ID_nominaBase);

            return procedureTable("nom_ConsultarXidNomBase", true, param);
        }

        public DataTable ConsultarDetalleXDocumento(string ID_nomina, string afi_documento)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);
            param.Add("@afi_documento", afi_documento);

            return procedureTable("nom_ConsultarDetalleXDocumento", true, param);
        }

        /// <summary>
        /// Se consume para cargar los exitos y rechazos. Trae menos campos y no viene con select *
        /// </summary>
        /// <param name="ID_nomina"></param>
        /// <param name="afi_documento"></param>
        /// <returns></returns>
        public DataTable ConsultarDetalleXDocumentoExitoso(string ID_nomina, string afi_documento)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);
            param.Add("@afi_documento", afi_documento);

            return procedureTable("nom_ConsultarDetalleXDocumentoExitoso", true, param);
        }

        public DataTable ActualizaGiroPlano(string ID_nominaBase, string ID_usuarioRegistraGiroFK, string nom_bancoPlano)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaBase", ID_nominaBase);
            param.Add("@ID_usuarioRegistraGiroFK", ID_usuarioRegistraGiroFK);
            param.Add("@nom_bancoPlano", nom_bancoPlano);

            return procedureTable("nom_ActualizaGiroPlano", true, param);
        }

        public DataTable ActualizaGiroEstado(string ID_nominaBase, bool nom_estadoGiro, string nom_planoGiro, string nom_pdfGiro, string nom_fechaPagoGiro, string ID_usuarioRegistraGiroFK, string nom_bancoExitoso)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaBase", ID_nominaBase);
            param.Add("@nom_estadoGiro", nom_estadoGiro);
            param.Add("@nom_planoGiro", nom_planoGiro);
            param.Add("@nom_pdfGiro", nom_pdfGiro);
            param.Add("@nom_fechaPagoGiro", nom_fechaPagoGiro);
            param.Add("@ID_usuarioRegistraGiroFK", ID_usuarioRegistraGiroFK);
            param.Add("@nom_bancoExitoso", nom_bancoExitoso);

            return procedureTable("nom_ActualizaGiroEstado", true, param);
        }

        public DataTable nom_VerificarPagoOK(string ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("nom_VerificarPagoOK", true, param);
        }

        public DataTable ConsultarXID_AutPago(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("nom_ConsultarXID_AutPago", true, param);
        }

        public DataTable ConsultaAfiliadosXSinNomina(string ID_empresaTemporal, string fechaInicio, string fechaFin)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporal", ID_empresaTemporal);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);

            return procedureTable("nom_ConsultaAfiliadosXSinNomina", true, param);
        }
        #endregion

        #region NOVEDADES LABORALES

        public DataTable novInsertar(string ID_nominaBaseFK, string nov_tipoNovedadFK, string nov_unidades, double nov_valor, string nov_valorPesos, double nov_valorTotal, string nov_fechaInicio, string nov_fechaFin, string nov_observacion, string ID_usuarioRegistraFK, bool nov_habilitadoBorrar, int ID_incapPadreFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaBaseFK", ID_nominaBaseFK);
            param.Add("@nov_tipoNovedadFK", nov_tipoNovedadFK);
            param.Add("@nov_unidades", nov_unidades);
            param.Add("@nov_valor", nov_valor);
            param.Add("@nov_valorPesos", nov_valorPesos);
            param.Add("@nov_valorTotal", nov_valorTotal);
            param.Add("@nov_fechaInicio", nov_fechaInicio);
            param.Add("@nov_fechaFin", nov_fechaFin);
            param.Add("@nov_observacion", nov_observacion);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
            param.Add("@nov_habilitadoBorrar", nov_habilitadoBorrar);
            param.Add("@ID_incapPadreFK", ID_incapPadreFK);

            return procedureTable("nov_Inserta", true, param);
        }

        public void novEliminar(string ID_novedad, string ID_usuarioEliminaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_novedad", ID_novedad);
            param.Add("@ID_usuarioEliminaFK", ID_usuarioEliminaFK);

            procedureTable("nov_Eliminar", true, param);
        }

        public DataTable novEliminarRetiroMasivo(string ID_nominaFK, string afi_documento)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaFK", ID_nominaFK);
            param.Add("@afi_documento", afi_documento);

            return procedureTable("nov_EliminarRetiroMasivo", true, param);
        }

        public DataTable novExistenNovedadesXNominaBase(string ID_nominaBaseFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaBaseFK", ID_nominaBaseFK);

            return procedureTable("nom_ExistenNovedades", true, param);
        }

        public DataTable novConsultaXNominaBase(string ID_nominaBaseFK, string ref_modulo)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaBaseFK", ID_nominaBaseFK);
            param.Add("@ref_modulo", ref_modulo);

            return procedureTable("nov_ConsultaXNominaBase", true, param);
        }

        public DataTable novRetiroMasivoValidar(string ID_nominaFK, string afi_documento, string fechaRetiro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaFK", ID_nominaFK);
            param.Add("@afi_documento", afi_documento);
            param.Add("@fechaRetiro", fechaRetiro);

            return procedureTable("nov_RetiroMasivoValidar", true, param);
        }

        public DataTable novConsultaLaboralesXNominaBase(string ID_nominaBaseFK, string TiposNovedad)
        {
            return queryTable("SELECT sum(nov_valor) as nov_valor, sum(nov_valorPesos) as valorPesos, min(nov_fechaInicio) as nov_fechaInicio, max(nov_fechaFin) as nov_fechaFin FROM nom_NovedadesLaborales WHERE ID_nominaBaseFK= '" + ID_nominaBaseFK + "' AND nov_tipoNovedadFK IN ( " + TiposNovedad + " )");
            //Hashtable param = new Hashtable(1);
            //param.Add("@ID_nominaBaseFK", ID_nominaBaseFK);
            //param.Add("@TiposNovedad", TiposNovedad);

            //return procedureTable("nov_ConsultaLaboralesXNominaBase", true, param);
        }

        public DataTable novConsultaTodasLaboralesXNominaBase(string ID_nominaBaseFK)
        {
            return queryTable("SELECT sum(nov_valor) as nov_valor, sum(nov_valorPesos) as valorPesos, min(nov_fechaInicio) as nov_fechaInicio, max(nov_fechaFin) as nov_fechaFin , nov_tipoNovedadFK , max(nov_observacion) nov_observacion FROM nom_NovedadesLaborales WHERE ID_nominaBaseFK= '" + ID_nominaBaseFK + "' group by nov_tipoNovedadFK");
        }

        public DataTable novConsultaLaboralesXNominaBaseXTipo(string ID_nominaBaseFK, string TiposNovedad, bool excluir)
        {
            if(excluir ==false)
                return queryTable("SELECT * FROM nom_NovedadesLaborales INNER JOIN app_Referencias ON nom_NovedadesLaborales.nov_tipoNovedadFK = app_Referencias.ref_valor WHERE ref_modulo='NovedadesLaborales' AND ID_nominaBaseFK= '" + ID_nominaBaseFK + "' AND nov_tipoNovedadFK IN ( " + TiposNovedad + " )");
            else
                return queryTable("SELECT * FROM nom_NovedadesLaborales INNER JOIN app_Referencias ON nom_NovedadesLaborales.nov_tipoNovedadFK = app_Referencias.ref_valor WHERE ref_modulo='NovedadesLaborales' AND ID_nominaBaseFK= '" + ID_nominaBaseFK + "' AND nov_tipoNovedadFK NOT IN ( " + TiposNovedad + " )");
            //Hashtable param = new Hashtable(1);
            //param.Add("@ID_nominaBaseFK", ID_nominaBaseFK);
            //param.Add("@TiposNovedad", TiposNovedad);

            //return procedureTable("nov_ConsultaLaboralesXNominaBase", true, param);
        }

        public DataTable novConsultaLaboralesXContratoXTipo(string ID_contratoFK, string TiposNovedad, bool excluir)
        {
            if (excluir == false)
                return queryTable("SELECT *, cast(nov_valor AS VARCHAR(50)) + ' ' + nov_unidades AS novValor FROM nom_NovedadesLaborales " +
                    "INNER JOIN dbo.nom_NomBase ON dbo.nom_NovedadesLaborales.ID_nominaBaseFK = dbo.nom_NomBase.ID_nominaBase " +
                    "INNER JOIN app_Referencias ON nom_NovedadesLaborales.nov_tipoNovedadFK = app_Referencias.ref_valor " +
                    "WHERE ref_modulo='NovedadesLaborales' AND ID_contratoFK= '" + ID_contratoFK + "' AND nov_tipoNovedadFK IN ( " + TiposNovedad + " )");
            else
                return queryTable("SELECT *, cast(nov_valor AS VARCHAR(50)) + ' ' + nov_unidades AS novValor FROM nom_NovedadesLaborales " +
                    "INNER JOIN dbo.nom_NomBase ON dbo.nom_NovedadesLaborales.ID_nominaBaseFK = dbo.nom_NomBase.ID_nominaBase " +
                    "INNER JOIN app_Referencias ON nom_NovedadesLaborales.nov_tipoNovedadFK = app_Referencias.ref_valor " +
                    "WHERE ref_modulo='NovedadesLaborales' AND ID_contratoFK= '" + ID_contratoFK + "' AND nov_tipoNovedadFK NOT IN ( " + TiposNovedad + " )");
        }

        public DataTable novConsultaLaboralesXNominaBaseXOrden(string ID_nominaBaseFK, string ref_orden)
        {
            return queryTable("SELECT * FROM nom_NovedadesLaborales INNER JOIN app_Referencias ON nom_NovedadesLaborales.nov_tipoNovedadFK = app_Referencias.ref_valor WHERE ref_modulo='NovedadesLaborales' AND ID_nominaBaseFK= '" + ID_nominaBaseFK + "' AND ref_orden IN ( " + ref_orden + " )");
        }

        public string novFiltroDescuentos()
        {
            DataTable tbResultado = queryTable("SELECT  STUFF(( SELECT ''',''' + ref_valor  from app_Referencias WHERE ref_modulo = 'NovedadesLaborales' and ref_enlazable=1 AND ref_TipoNov=-1 order by ref_orden FOR XML PATH('') ), 1, 2, '') + '''' as novedadesDescuento");
            return tbResultado.Rows[0]["novedadesDescuento"].ToString();
        }

        public string novFiltroDevengos()
        {
            DataTable tbResultado = queryTable("SELECT  STUFF(( SELECT ''',''' + ref_valor  from app_Referencias WHERE ref_modulo = 'NovedadesLaborales' and ref_enlazable=1 AND ref_TipoNov=1 order by ref_orden FOR XML PATH('') ), 1, 2, '') + '''' as novedadesDevengo");
            return tbResultado.Rows[0]["novedadesDevengo"].ToString();
        }
        #endregion

        #region INCAPACIDADES

        public DataTable incConsultarXEmpleado(int ID_afiliadoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);

            return procedureTable("inc_ConsultarXEmpleado", true, param);
        }

        public DataTable incConsultarXContrato(string ID_contratoFK, String ID_tipoNovedadFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@ID_tipoNovedadFK", ID_tipoNovedadFK);

            return procedureTable("inc_ConsultarXContrato", true, param);
        }

        public DataTable IncConsultarXEmpleadoXAplicar(string ID_afiliadoFK, string ID_contratoFK, string nom_fechaInicio, string nom_fechaFin)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@nom_fechaInicio", nom_fechaInicio);
            param.Add("@nom_fechaFin", nom_fechaFin);

            return procedureTable("inc_ConsultarXEmpleadoXAplicar", true, param);
        }

        public DataTable IncConsultaXEstado(string inc_estado, string where)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@inc_estado", inc_estado);
            param.Add("@where", where);

            return procedureTable("inc_ConsultarxEstado", true, param);
        }

        public void IncTramitar(string ID_incapacidad, string inc_estado, double inc_valorRecobrar, string inc_tramitarObservacion, string inc_tramitarCausal, int ID_usuarioTramitaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_incapacidad", ID_incapacidad);
            param.Add("@inc_estado", inc_estado);
            param.Add("@inc_valorRecobrar", inc_valorRecobrar);
            param.Add("@inc_tramitarObservacion", inc_tramitarObservacion);
            param.Add("@inc_tramitarCausal", inc_tramitarCausal);
            param.Add("@ID_usuarioTramitaFK", ID_usuarioTramitaFK);
            
            procedureTable("inc_Tramitar", true, param);
        }

        public void IncReconocer(string ID_incapacidad, string inc_estado, double inc_valorReconocer, string inc_ReconocerObservacion, string inc_ReconocerCausal, int ID_usuarioReconocerFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_incapacidad", ID_incapacidad);
            param.Add("@inc_estado", inc_estado);
            param.Add("@inc_valorReconocer", inc_valorReconocer);
            param.Add("@inc_ReconocerObservacion", inc_ReconocerObservacion);
            param.Add("@inc_ReconocerCausal", inc_ReconocerCausal);
            param.Add("@ID_usuarioReconocerFK", ID_usuarioReconocerFK);

            procedureTable("inc_Reconocer", true, param);
        }

        public void IncPagadoEPS(string ID_incapacidad, string inc_estado, double inc_valorPagadoEPS, string inc_PagadoEPSObservacion, int ID_usuarioPagadoEPSFK, string inc_fechaPagadoEPS)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_incapacidad", ID_incapacidad);
            param.Add("@inc_estado", inc_estado);
            param.Add("@inc_valorPagadoEPS", inc_valorPagadoEPS);
            param.Add("@inc_PagadoEPSObservacion", inc_PagadoEPSObservacion);
            param.Add("@ID_usuarioPagadoEPSFK", ID_usuarioPagadoEPSFK);
            param.Add("@inc_fechaPagadoEPS", inc_fechaPagadoEPS);

            procedureTable("inc_PagadoEPS", true, param);
        }

        public void IncReclamo(string ID_incapacidad, string inc_reclamoObservacion, int ID_usuarioReclamoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_incapacidad", ID_incapacidad);
            param.Add("@inc_reclamoObservacion", inc_reclamoObservacion);
            param.Add("@ID_usuarioReclamoFK", ID_usuarioReclamoFK);

            procedureTable("inc_Reclamo", true, param);
        }

        public void IncPagadoEU(string ID_incapacidad, string inc_estado, double inc_valorPagadoEU, string inc_PagadoEUTipoPago, string inc_PagadoEUObservacion, int ID_usuarioPagadoEUFK, string inc_fechaPagadoEU)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_incapacidad", ID_incapacidad);
            param.Add("@inc_estado", inc_estado);
            param.Add("@inc_valorPagadoEU", inc_valorPagadoEU);
            param.Add("@inc_PagadoEUTipoPago", inc_PagadoEUTipoPago);
            param.Add("@inc_PagadoEUObservacion", inc_PagadoEUObservacion);
            param.Add("@ID_usuarioPagadoEUFK", ID_usuarioPagadoEUFK);
            param.Add("@inc_fechaPagadoEU", inc_fechaPagadoEU);
            
            procedureTable("inc_PagadoEU", true, param);
        }

        public void IncActualizaFechaReal(string ID_incapacidad, string inc_fechaInicioReal, string inc_fechaFinReal, int ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_incapacidad", ID_incapacidad);
            param.Add("@inc_fechaInicioReal", inc_fechaInicioReal);
            param.Add("@inc_fechaFinReal", inc_fechaFinReal);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);

            procedureTable("inc_ActualizaFechaReal", true, param);
        }

        public void IncReversar(string ID_incapacidad)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_incapacidad", ID_incapacidad);

            procedureTable("inc_Reversar", true, param);
        }

        public void IncCargarArchivo(string ID_incapacidad, string inc_archivo, int ID_usuarioArchivoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_incapacidad", ID_incapacidad);
            param.Add("@inc_archivo", inc_archivo);
            param.Add("@ID_usuarioArchivoFK", ID_usuarioArchivoFK);

            procedureTable("inc_CargarArchivo", true, param);
        }

        public void IncVincular(string ID_incapacidad, string ID_vinculadaFK, int ID_usuariovinculadaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_incapacidad", ID_incapacidad);
            param.Add("@ID_vinculadaFK", ID_vinculadaFK);
            param.Add("@ID_usuariovinculadaFK", ID_usuariovinculadaFK);

            procedureTable("inc_Vincular", true, param);
        }

        public DataTable IncConsultaVinculadas(string ID_incapacidad)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_incapacidad", ID_incapacidad);

            return procedureTable("inc_ConsultaVinculadas", true, param);
        }

        public void IncInsertarManual(string ID_tipoNovedadFK, string ID_afiliadoFK, string ID_contratoFK, double inc_valor, string inc_fechaInicio, string inc_fechaFin, string inc_observaciones, int ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_tipoNovedadFK", ID_tipoNovedadFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@inc_valor", inc_valor);
            param.Add("@inc_fechaInicio", inc_fechaInicio);
            param.Add("@inc_fechaFin", inc_fechaFin);
            param.Add("@inc_observaciones", inc_observaciones);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            procedureTable("inc_InsertarManual", true, param);
        }
        #endregion

        #region LIQUIDACION DEFINITIVA

        public DataTable liqContratosXEmpleado(int ID_afiliadoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);

            return procedureTable("liq_ContratosXEmpleado", true, param);
        }

        public DataTable liqConsultaAcumuladoXEmpleado(int ID_afiliadoFK, int ID_contratoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);

            return procedureTable("liq_ConsultaAcumuladoXEmpleado", true, param);
        }

        public DataTable liqConsultaVacaciones(int ID_contratoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK", ID_contratoFK);

            return procedureTable("liq_Vacaciones", true, param);
        }

        public DataTable liqConsultaPrima(int ID_contratoFK, string fechaInicio, string fechaFin, string fechaInicioDias, string fechaFinDias)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@fechaInicioDias", fechaInicioDias);
            param.Add("@fechaFinDias", fechaFinDias);

            return procedureTable("liq_ConsultaPrima", true, param);
        }

        public DataTable liqConsultaPrimaPagada(int ID_contratoFK, string fechaInicio, string fechaFin)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);

            return procedureTable("liq_ConsultaPrimaPagada", true, param);
        }

        public DataTable liqConsultaCesantias(int ID_contratoFK, string fechaInicio, string fechaFin, string fechaInicioDias, string fechaFinDias)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@fechaInicioDias", fechaInicioDias);
            param.Add("@fechaFinDias", fechaFinDias);

            return procedureTable("liq_ConsultaCesantias", true, param);
        }

        public DataTable liqConsultaHorasExtraXContrato(int ID_afiliadoFK, int ID_contratoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);

            return procedureTable("liq_ConsultaHorasExtraXContrato", true, param);
        }

        public DataTable liqConsultaRecargosXContrato(int ID_afiliadoFK, int ID_contratoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);

            return procedureTable("liq_ConsultaRecargosXContrato", true, param);
        }

        public DataTable liqConsultaAusentismoXContrato(int ID_afiliadoFK, int ID_contratoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);

            return procedureTable("liq_ConsultaAusentismoXContrato", true, param);
        }

        public DataTable liqConsultaPrestamosXContrato(int ID_afiliadoFK, int ID_contratoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);

            return procedureTable("liq_ConsultaPrestamosXContrato", true, param);
        }

        public DataTable liqConsultaRetiroExtemporaneo(int ID_contratoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK", ID_contratoFK);

            return procedureTable("liq_RetiroExtemporaneo", true, param);
        }

        public DataTable liqInsertar(int ID_contratoFK, int ID_afiliadoFK,int liq_vacaDias, int liq_primaDias, int liq_cesanDiasAnterior, int liq_interesDiasAnterior, int liq_cesanDias, int liq_interesDias
            , double  liq_acumVacaValorPagar, double liq_acumPrimaValorPagar , double liq_acumCesanValorPagar , double liq_acumInteValorPagar , double liq_acumValorTotal , double liq_calcVacaSalario 
            , double liq_calcVacaHE , double liq_calcVacaOtros , double liq_calcVacaValorPagar , double liq_calcPrimaSalario , double liq_calcPrimaHE , double liq_calcPrimaAuxTransporte 
            , double liq_calcPrimaValorPagar , double liq_calcCesanSalarioAnterior , double liq_calCesanHEAnterior , double liq_calcCesanAuxTransporteAnterior , double liq_calcCesanValorPagarAnterior 
            , double liq_calcCesanSalario , double liq_calcCesanHE , double liq_calcCesanAuxTransporte , double liq_calcCesanValorPagar , double liq_calcInteValorPagarAnterior 
            , double liq_calcInteValorPagar , double liq_calcValorTotal , double liq_ValorGirar ,string ID_usuarioRegistraFK
            , bool liq_calcPrimaPagar, bool liq_calcCesanPagar , bool liq_calcCesanPagarAnterior, bool liq_calcIntePagar, bool liq_calcIntePagarAnterior)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK",ID_contratoFK);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@liq_vacaDias", liq_vacaDias);
            param.Add("@liq_primaDias", liq_primaDias);
            param.Add("@liq_cesanDiasAnterior", liq_cesanDiasAnterior);
            param.Add("@liq_interesDiasAnterior", liq_interesDiasAnterior);
            param.Add("@liq_cesanDias", liq_cesanDias);
            param.Add("@liq_interesDias", liq_interesDias);
            param.Add("@liq_acumVacaValorPagar", liq_acumVacaValorPagar);
            param.Add("@liq_acumPrimaValorPagar", liq_acumPrimaValorPagar);
            param.Add("@liq_acumCesanValorPagar", liq_acumCesanValorPagar);
            param.Add("@liq_acumInteValorPagar", liq_acumInteValorPagar);
            param.Add("@liq_acumValorTotal", liq_acumValorTotal);
            param.Add("@liq_calcVacaSalario", liq_calcVacaSalario);
            param.Add("@liq_calcVacaHE", liq_calcVacaHE);
            param.Add("@liq_calcVacaOtros", liq_calcVacaOtros);
            param.Add("@liq_calcVacaValorPagar", liq_calcVacaValorPagar);
            param.Add("@liq_calcPrimaSalario", liq_calcPrimaSalario);
            param.Add("@liq_calcPrimaHE", liq_calcPrimaHE);
            param.Add("@liq_calcPrimaAuxTransporte", liq_calcPrimaAuxTransporte);
            param.Add("@liq_calcPrimaValorPagar", liq_calcPrimaValorPagar);
            param.Add("@liq_calcCesanSalarioAnterior", liq_calcCesanSalarioAnterior);
            param.Add("@liq_calCesanHEAnterior", liq_calCesanHEAnterior);
            param.Add("@liq_calcCesanAuxTransporteAnterior", liq_calcCesanAuxTransporteAnterior);
            param.Add("@liq_calcCesanValorPagarAnterior", liq_calcCesanValorPagarAnterior);
            param.Add("@liq_calcCesanSalario", liq_calcCesanSalario);
            param.Add("@liq_calcCesanHE", liq_calcCesanHE);
            param.Add("@liq_calcCesanAuxTransporte", liq_calcCesanAuxTransporte);
            param.Add("@liq_calcCesanValorPagar", liq_calcCesanValorPagar);
            param.Add("@liq_calcInteValorPagarAnterior", liq_calcInteValorPagarAnterior);
            param.Add("@liq_calcInteValorPagar", liq_calcInteValorPagar);
            param.Add("@liq_calcValorTotal", liq_calcValorTotal);
            param.Add("@liq_ValorGirar", liq_ValorGirar);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            param.Add("@liq_calcPrimaPagar", liq_calcPrimaPagar);
            param.Add("@liq_calcCesanPagar", liq_calcCesanPagar);
            param.Add("@liq_calcCesanPagarAnterior", liq_calcCesanPagarAnterior);
            param.Add("@liq_calcIntePagar", liq_calcIntePagar);
            param.Add("@liq_calcIntePagarAnterior", liq_calcIntePagarAnterior);


            return procedureTable("liq_Insertar", true, param);
        }

        public DataTable liqInsertarConcepto(int ID_liquidacionFK, string liq_tipoConcepto, string liq_concepto, double liq_valor, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_liquidacionFK", ID_liquidacionFK);
            param.Add("@liq_tipoConcepto", liq_tipoConcepto);
            param.Add("@liq_concepto", liq_concepto);
            param.Add("@liq_valor", liq_valor);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            return procedureTable("liq_InsertarConcepto", true, param);
        }

        public DataTable liqConsultaConceptos(string ID_liquidacionFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_liquidacionFK", ID_liquidacionFK);

            return procedureTable("liq_ConsultaConceptos", true, param);
        }

        public DataTable liqConsultaXID(string ID_liquidacion, string ID_contrato)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_liquidacion", ID_liquidacion);
            param.Add("@ID_contrato", ID_contrato);

            return procedureTable("liq_ConsultaXID", true, param);
        }

        public DataTable liqActualizaEstado(string ID_liquidacion, string ID_contratoFK, string liq_estado, string liq_ObservacionActualiza, string ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_liquidacion", ID_liquidacion);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@liq_estado", liq_estado);
            param.Add("@liq_ObservacionActualiza", liq_ObservacionActualiza);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);

            return procedureTable("liq_ActualizaEstado", true, param);
        }

        public DataTable liqActualizaValorGirar(string ID_liquidacion, double liq_valorGirar, string ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_liquidacion", ID_liquidacion);
            param.Add("@liq_valorGirar", liq_valorGirar);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);

            return procedureTable("liq_ActualizaValorGirar", true, param);
        }

        #region  ADMINISTRAR PROCESOS DE LIQUIDACIONES  
        public DataTable liqConsultaXEstado(string liq_estado, string where)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@liq_estado", liq_estado);
            param.Add("@where", where);

            return procedureTable("liq_ConsultaXEstado", true, param);
        }

        public void liqAprobar(string ID_liquidacion, string ID_usuarioApruebaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_liquidacion", ID_liquidacion);
            param.Add("@ID_usuarioApruebaFK", ID_usuarioApruebaFK);

            procedureTable("liq_Aprobar", true, param);
        }

        public void liqSoportar(string ID_liquidacion, string liq_soporte, string ID_usuarioSoportaFK, bool actualizaEstado)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_liquidacion", ID_liquidacion);
            param.Add("@liq_soporte", liq_soporte);
            param.Add("@ID_usuarioSoportaFK", ID_usuarioSoportaFK);
            param.Add("@actualizaEstado", actualizaEstado);

            procedureTable("liq_Soportar", true, param);
        }

        public void liqPagar(string ID_liquidacion, string liq_planoGiro, string liq_PDFGiro, string liq_fechaPagoGiro, string ID_usuarioRegistroGiroFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_liquidacion", ID_liquidacion);
            param.Add("@liq_planoGiro", liq_planoGiro);
            param.Add("@liq_PDFGiro", liq_PDFGiro);
            param.Add("@liq_fechaPagoGiro", liq_fechaPagoGiro);
            param.Add("@ID_usuarioRegistroGiroFK", ID_usuarioRegistroGiroFK);

            procedureTable("liq_Pagar", true, param);
        }

        public void liqGeneraPlano(string ID_liquidacion, string ID_usuarioPlanoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_liquidacion", ID_liquidacion);
            param.Add("@ID_usuarioPlanoFK", ID_usuarioPlanoFK);

            procedureTable("liq_GeneraPlano", true, param);
        }

        public void liqDevolverEstado(string ID_liquidacion, string liq_estado, string ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_liquidacion", ID_liquidacion);
            param.Add("@liq_estado", liq_estado);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);

            procedureTable("liq_DevolverEstado", true, param);
        }

        public DataTable liqConsultaParaRechazos(string ID_empresaTemporalFK, string ID_empresaUsuariaFK, string afi_documento, int liq_ValorGirar)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@afi_documento", afi_documento);
            param.Add("@liq_ValorGirar", liq_ValorGirar);

            return procedureTable("liq_ConsultaParaRechazos", true, param);
        }

        #endregion

        #endregion

        #region PRIMAS

        public DataTable priConsultaAfiliadosXEmpresaSinPrima(string ID_empresaTemporal, string ID_empresaUsuariaFK, string ID_centroCosto, string fechaInicio, string fechaFin)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporal", ID_empresaTemporal);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@ID_centroCosto", ID_centroCosto);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);

            return procedureTable("pri_ConsultaAfiliadosXEmpresaSinPrima", true, param);
        }

        public DataTable priConsultaAcumuladoXEmpleado(int ID_afiliadoFK, int ID_contratoFK, string fechaInicio, string fechaFin)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);

            return procedureTable("pri_ConsultaAcumuladoXEmpleado", true, param);
        }

        public DataTable priConsultarXIDdetallado(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("pri_ConsultarXIDdetallado", true, param);
        }

        #endregion

        #region CESANTIAS E INTERESES
        public DataTable cesConsultaAfiliadosXEmpresaSinCesantia(string ID_empresaTemporal, string ID_empresaUsuariaFK, string ID_centroCosto, string fechaInicio, string fechaFin)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporal", ID_empresaTemporal);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@ID_centroCosto", ID_centroCosto);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);

            return procedureTable("ces_ConsultaAfiliadosXEmpresaSinCesantias", true, param);
        }

        public DataTable cesConsultarXID(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("ces_ConsultarXID", true, param);
        }

        public DataTable cesConsultarXIDdetallado(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("ces_ConsultarXIDdetallado", true, param);
        }

        public DataTable cesActualizaNomBase(string ID_nominaBase, string campoActualizar, double valorNuevo, string ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nominaBase", ID_nominaBase);
            param.Add("@campoActualizar", campoActualizar);
            param.Add("@valorNuevo", valorNuevo);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);

            return procedureTable("ces_ActualizaNomBase", true, param);
        }

        public DataTable cesConsultarXID_ExcelMasivo(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("ces_ConsultarXID_ExcelMasivo", true, param);
        }

        public DataTable cesConsultarXID_ExcelPago(double ID_nomina)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);

            return procedureTable("ces_ConsultarXID_ExcelPago", true, param);
        }

        public DataTable cesConsultarEmpresas_ExcelPago(string ID_empresaTemporalFK, string año)
        {
            Hashtable param = new Hashtable(2);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@año", año);

            return procedureTable("ces_ConsultarEmpresas_ExcelPago", true,param);
        }

        public DataTable cesGenerarEmpresas_ExcelPago(string ID_empresaTemporalFK, string año, string EmpresasSeleccionadas)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@año", año);
            param.Add("@EmpresasSeleccionadas", EmpresasSeleccionadas);

            return procedureTable("ces_GenerarEmpresas_ExcelPago", true, param);
        }
        public DataTable cesInsertaPago(string ces_nit, string tipo_doc, string num_doc, string Apellido1, string Apellido2, string Nombre1, string Nombre2, string ces_Codigo, string ces_Nombre,
            double ces_SalarioBase, double ces_ValorPagado, int ces_Periodo, int ces_TotalEmpleados, string ces_sucursal, string ces_clave, string ces_transaccion, string ces_banco, string ces_fechaPago,
            double ces_totalPagado, double ces_totalPagadoInteres, string ces_ArchivoPlanilla, string ID_usuarioRegistra
            )
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ces_nit", ces_nit);
            param.Add("@tipo_doc", tipo_doc);
            param.Add("@num_doc", num_doc);
            param.Add("@Apellido1", Apellido1);
            param.Add("@Apellido2", Apellido2);
            param.Add("@Nombre1", Nombre1);
            param.Add("@Nombre2", Nombre2);
            param.Add("@ces_Codigo", ces_Codigo);
            param.Add("@ces_Nombre", ces_Nombre);
            param.Add("@ces_SalarioBase", ces_SalarioBase);
            param.Add("@ces_ValorPagado", ces_ValorPagado);
            param.Add("@ces_Periodo", ces_Periodo);
            param.Add("@ces_TotalEmpleados", ces_TotalEmpleados);
            param.Add("@ces_sucursal", ces_sucursal);
            param.Add("@ces_clave", ces_clave);
            param.Add("@ces_transaccion", ces_transaccion);
            param.Add("@ces_banco", ces_banco);
            param.Add("@ces_fechaPago", ces_fechaPago);
            param.Add("@ces_totalPagado", ces_totalPagado);
            param.Add("@ces_totalPagadoInteres", ces_totalPagadoInteres);
            param.Add("@ces_ArchivoPlanilla", ces_ArchivoPlanilla);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);


            return procedureTable("ces_InsertaPago", true, param);
        }

        #endregion

        #region SEGURIDAD SOCIAL

        public void segInsertarSOI(string soi_nit, string tipo_doc, string num_doc, string tipo_cotizante, string subtipo_cotizante, string Cod_depar, string Cod_mun
           , string Apellido1, string Apellido2, string Nombre1, string Nombre2, string ING,string RET,string TDE,string TAE,string TDP,string TAP,string VSP
           , string COR,string VST,string SLN,string IGE,string LMA,string VAC,string AVP,string VCT,string IRP,string VIP
           , string AFP_CODIGO, string AFP_DIAS, double AFP_IBC, double AFP_APORTE
           , string EPS_CODIGO, string EPS_DIAS, double EPS_IBC, double EPS_APORTE, string CCF_CODIGO, string CCF_DIAS, double CCF_IBC, double CCF_APORTE
           , string ARL_CODIGO, string ARL_DIAS, double ARL_IBC, double ARL_APORTE, string PARAF_DIAS, double PARAF_IBC, double PARAF_APORTE
           , String soi_periodoSalud, String soi_periodoPension, String soi_sucursal, String soi_planilla, string soi_banco, String soi_fechaPago
           , string soi_codigoPago, double soi_totalPagado, String soi_ArchivoPlanilla, String ID_usuarioRegistra)
        {
            Hashtable param = new Hashtable(1);
            #region PARAMETROS

            param.Add("@soi_nit",soi_nit);
            param.Add("@tipo_doc", tipo_doc);
            param.Add("@num_doc", num_doc);
            param.Add("@tipo_cotizante", tipo_cotizante);
            param.Add("@subtipo_cotizante", subtipo_cotizante);
            param.Add("@Cod_depar", Cod_depar);
            param.Add("@Cod_mun", Cod_mun);
            param.Add("@Apellido1", Apellido1);
            param.Add("@Apellido2", Apellido2);
            param.Add("@Nombre1", Nombre1);
            param.Add("@Nombre2", Nombre2);
            param.Add("@ING", ING);
            param.Add("@RET", RET);
            param.Add("@TDE", TDE);
            param.Add("@TAE", TAE);
            param.Add("@TDP", TDP);
            param.Add("@TAP", TAP);
            param.Add("@VSP", VSP);
            param.Add("@COR", COR);
            param.Add("@VST", VST);
            param.Add("@SLN", SLN);
            param.Add("@IGE", IGE);
            param.Add("@LMA", LMA);
            param.Add("@VAC", VAC);
            param.Add("@AVP", AVP);
            param.Add("@VCT", VCT);
            param.Add("@IRP", IRP);
            param.Add("@VIP", VIP);
            param.Add("@AFP_CODIGO", AFP_CODIGO);
            param.Add("@AFP_DIAS", AFP_DIAS);
            param.Add("@AFP_IBC", AFP_IBC);
            param.Add("@AFP_APORTE", AFP_APORTE);
            param.Add("@EPS_CODIGO", EPS_CODIGO);
            param.Add("@EPS_DIAS", EPS_DIAS);
            param.Add("@EPS_IBC", EPS_IBC);
            param.Add("@EPS_APORTE", EPS_APORTE);
            param.Add("@CCF_CODIGO", CCF_CODIGO);
            param.Add("@CCF_DIAS", CCF_DIAS);
            param.Add("@CCF_IBC", CCF_IBC);
            param.Add("@CCF_APORTE", CCF_APORTE);
            param.Add("@ARL_CODIGO", ARL_CODIGO);
            param.Add("@ARL_DIAS", ARL_DIAS);
            param.Add("@ARL_IBC", ARL_IBC);
            param.Add("@ARL_APORTE", ARL_APORTE);
            param.Add("@PARAF_DIAS", PARAF_DIAS);
            param.Add("@PARAF_IBC", PARAF_IBC);
            param.Add("@PARAF_APORTE", PARAF_APORTE);
            param.Add("@soi_periodoSalud", soi_periodoSalud);
            param.Add("@soi_periodoPension", soi_periodoPension);
            param.Add("@soi_sucursal", soi_sucursal);
            param.Add("@soi_planilla", soi_planilla);
            param.Add("@soi_banco", soi_banco);
            param.Add("@soi_fechaPago", soi_fechaPago);
            param.Add("@soi_codigoPago", soi_codigoPago);
            param.Add("@soi_totalPagado", soi_totalPagado);
            param.Add("@soi_ArchivoPlanilla", soi_ArchivoPlanilla);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);

            #endregion

            procedureTable("soi_Inserta", true, param);

        }

        #endregion

        #region REPORTE DE PROVISIONES
        public DataTable provEmpleadosActivosXEmpresa(string ID_empresaTemporal, string ID_empresaUsuariaFK, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporal);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_ProvEmpleadosActivosXEmpresa", true, param);
        }
        #endregion

        #region DEDUCCIONES RTE FUENTE

        public DataTable deducRteConsultarVigentes(string ID_afiliadoFK, int ded_anno)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ded_anno", ded_anno);

            return procedureTable("nom_DeducRteConsultarVigentes", true, param);
        }

        #endregion
    }
}
