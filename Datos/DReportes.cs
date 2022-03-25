using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;

namespace Datos
{
    public class DReportes : DSQL
    {
        public DataTable ConsultaReportesActivos(int ID_padre, int rep_nivel)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_padre", ID_padre);
            param.Add("@rep_nivel", rep_nivel);

            return procedureTable("rep_ReportesActivos", true, param);
        }

        public DataTable ConsultarReporteXRuta(string rep_ruta)
        {
            return queryTable("SELECT * FROM app_Reportes WHERE rep_ruta='" + rep_ruta + "'");
        }

        #region EMPLEADOS

        public DataTable empGeneral(string ID_empresaTemporalFK, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_EmpGeneral", true, param);
        }

        public DataTable empHorasExtra(string ID_empresaTemporalFK,string fechaInicio,string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_EmpHorasExtra", true, param);
        }

        public DataTable empIngresosXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_EmpIngresosXFecha", true, param);
        }

        public DataTable empRetirosXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_EmpRetirosXFecha", true, param);
        }

        public DataTable EmpRetirosTentativaXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_EmpRetirosTentativaXFecha", true, param);
        }
        public DataTable emp11Meses(string ID_empresaTemporalFK, string fechaInicio, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_Emp11Meses", true, param);
        }

        public DataTable empCesantias(string ID_empresaTemporalFK, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_EmpGeneral", true, param);
        }

        public DataTable empCumpleanios(string ID_empresaTemporalFK, string mes,string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@mes", mes);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_EmpCumpleanios", true, param);
        }

        public DataTable empBeneficiarios(string ID_empresaTemporalFK, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_EmpBeneficiarios", true, param);
        }

        public DataTable empAptitudSaludOcupacional(string ID_empresaTemporalFK, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_EmpAptitudSaludOcupacional", true, param);
        }

        #endregion

        #region NOMINA

        public DataTable nomConsolidado(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_NomConsolidado", true, param);
        }

        public DataTable nomDetallado(string ID_nomina, int incluirTotales)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_nomina", ID_nomina);
            param.Add("@incluirTotales", incluirTotales);

            return procedureTable("rep_NomDetallado", true, param);
        }

        public DataTable nomConsolidadoLiquidacion(string ID_contratoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK", ID_contratoFK);

            return procedureTable("rep_NomConsolidadoLiquidacion", true, param);
        }

        public DataTable nomConsolidadoSegSocial(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_NomConsolidadoSegSocial", true, param);
        }

        public DataTable nomLicencias(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_NomLicencias", true, param);
        }

        public DataTable nomEmbarazosYEspeciales(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_NomEmbarazosYEspeciales", true, param);
        }

        public DataTable nomTerceros(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_NomTerceros", true, param);
        }

        public DataTable nomIncapacidades(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_NomIncapacidades", true, param);
        }

        public DataTable nomIncapacidadesResumen(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_NomIncapacidadesResumen", true, param);
        }

        public DataTable nomIncapacidadesResumenTipo(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_NomIncapacidadesResumenTipo", true, param);
        }

        public DataTable nomIngresosRetenciones(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_NomIngresosRetenciones", true, param);
        }

        public DataTable nomContabilidad(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_NomContabilidad", true, param);
        }

        public DataTable nomHistorico(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_NomHistorico", true, param);
        }

        public DataTable Liquidaciones(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_Liquidaciones", true, param);
        }
        #endregion

        #region SEGURIDAD SOCIAL
        public DataTable segPlanoPilaBuscar(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_SegPlanoPilaBuscar", true, param);
        }

        public DataTable segPlanoPila(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_SegPlanoPila", true, param);
        }

        public DataTable segPlanoPilaCatorcenal(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string ID_contratoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@ID_contratoFK", ID_contratoFK);

            return procedureTable("rep_SegPlanoPilaCatorcenalXContrato", true, param);
        }

        public DataTable segPlanoPilaXContratoXNovedad(string ID_empresaTemporalFK, string ID_contratoFK, string fechaInicio, string fechaFin, string nov_tipoNovedadFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@nov_tipoNovedadFK", nov_tipoNovedadFK);

            return procedureTable("rep_SegPlanoPilaXContratoXNovedad", true, param);
        }

        public DataTable segPlanoPilaXContratoXNominaCatorcenal(string ID_empresaTemporalFK, string ID_contratoFK, string ID_nominaBase, string nov_tipoNovedadFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@ID_nominaBase", ID_nominaBase);
            param.Add("@nov_tipoNovedadFK", nov_tipoNovedadFK);

            return procedureTable("rep_SegPlanoPilaXContratoXNovedad", true, param);
        }

        public DataTable segPlanoPilaLicenciasNoRemuneradas(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_SegPlanoPilaLicenciasNoRemuneradas", true, param);
        }

        public DataTable ResumenSegSocial(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_ResumenSegSocial", true, param);
        }

        public DataTable TotalizadoSegSocial(string ID_empresaTemporalFK, string fechaInicio, string fechaFin)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            
            return procedureTable("rep_TotalizadoSegSocial", true, param);
        }
        #endregion

        #region PRESTAMOS
        public DataTable presConsolidado(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_PrestamosConsolidado", true, param);
        }
        #endregion

        #region GERENCIAL

        public DataTable gerencialPersonalActivo(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GerenPersonalActivo", true, param);
        }

        public DataTable gerencialRetiradoXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GerenPersonalRetiradoXFecha", true, param);
        }

        public DataTable gerencialIngresosXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GerenPersonalIngresosXFecha", true, param);
        }

        public DataTable gerencialNominaSalarios(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GerenNominaSalarios", true, param);
        }

        public DataTable gerencialNominaTiempoExtra(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GerenNominaTiempoExtra", true, param);
        }

        public DataTable gerencialNominaTiempoExtraHoras(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GerenNominaTiempoExtraHoras", true, param);
        }

        public DataTable gerencialNominaTiempoRecargoNocturno(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GerenNominaRecargoNocturno", true, param);
        }

        public DataTable gerencialNominaTiempoDominicales(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GerenNominaDominicales", true, param);
        }

        public DataTable gerencialAusentismoIncap(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos, string tipoNovedad)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@tipoNovedad", tipoNovedad);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GerenAusentismoIncap", true, param);
        }

        public DataTable gerencialAusentismoIncapGeneral(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos, string minimo, string maximo)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@minimo", minimo);
            param.Add("@maximo", maximo);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GerenAusentismoIncapGeneral", true, param);
        }

        public DataTable asesorXEmpresa(string ID_empresaTemporalFK, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_AsesorXEmpresa", true, param);
        }

        
        #endregion

        #region GERENCIAL GRAFICAS

        public DataTable gerencialGrafPersonalActivo(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GrafPersonalActivo", true, param);
        }

        public DataTable gerencialGrafIngresosXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GrafPersonalIngresosXFecha", true, param);
        }

        public DataTable gerencialGrafRetirosXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GrafPersonalRetiradoXFecha", true, param);
        }

        public DataTable gerencialGrafNomina(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);
            param.Add("@filCampoDos", filCampoDos);
            param.Add("@filFiltroDos", filFiltroDos);

            return procedureTable("rep_GrafNomina", true, param);
        }

        #endregion

        #region PROVISIONES
        public DataTable provEmpresasActivas(string ID_empresaTemporalFK, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_ProvEmpresasActivas", true, param);
        }

        public DataTable provCartera(string ID_empresaTemporalFK, string año)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@año", año);

            return procedureTable("rep_CarteraProviciones", true, param);
        }

        public DataTable provCarteraPagada(string ID_empresaTemporalFK, string ID_empresaUsuariaFK, string año)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@año", año);

            return procedureTable("rep_CarteraProvicionesPagadas", true, param);
        }
        #endregion

        #region INTERESES DE CESANTIAS
        public DataTable cesInteresConsolidado(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@filCampo", filCampo);
            param.Add("@filFiltro", filFiltro);

            return procedureTable("rep_cesInteresConsolidado", true, param);
        }
        #endregion

        #region MAESTROS
        public DataTable maeEmpresasUsuarias()
        {
            return procedureTable("rep_MaeEmpresasUsuarias", false );
        }

        public DataTable maeCentrosCosto(string ID_empresaUsuariaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            return procedureTable("rep_MaeCentrosCosto", true, param);
        }
        #endregion
    }
}
