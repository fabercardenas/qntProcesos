using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Negocio
{
    public class NReportes
    {
        Datos.DReportes dReporte = new Datos.DReportes();

        public DataTable ConsultaReportesActivos(string ID_padre, string rep_nivel)
        {
            return dReporte.ConsultaReportesActivos(int.Parse(ID_padre), int.Parse(rep_nivel));
        }

        public DataTable ConsultarReporteXRuta(string rep_ruta)
        {
            return dReporte.ConsultarReporteXRuta(rep_ruta);
        }

        #region EMPLEADOS

        public DataTable empGeneral(string ID_empresaTemporalFK, string filCampo, string filFiltro)
        {
            return dReporte.empGeneral(ID_empresaTemporalFK, filCampo, filFiltro);
        }

        public DataTable empHorasExtra(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.empHorasExtra(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }

        public DataTable empIngresosXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.empIngresosXFecha(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }

        public DataTable empRetirosXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.empRetirosXFecha(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }

        public DataTable EmpRetirosTentativaXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.EmpRetirosTentativaXFecha(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }

        public DataTable emp11Meses(string ID_empresaTemporalFK, string fechaInicio, string filCampo, string filFiltro)
        {
            return dReporte.emp11Meses(ID_empresaTemporalFK, fechaInicio, filCampo, filFiltro);
        }

        public DataTable empCesantias(string ID_empresaTemporalFK, string filCampo, string filFiltro)
        {
            return dReporte.empCesantias(ID_empresaTemporalFK, filCampo, filFiltro);
        }

        public DataTable empCumpleanios(string ID_empresaTemporalFK, string mes, string filCampo, string filFiltro)
        {
            return dReporte.empCumpleanios(ID_empresaTemporalFK, mes, filCampo, filFiltro);
        }

        public DataTable empBeneficiarios(string ID_empresaTemporalFK, string filCampo, string filFiltro)
        {
            return dReporte.empBeneficiarios(ID_empresaTemporalFK, filCampo, filFiltro);
        }

        public DataTable empAptitudSaludOcupacional(string ID_empresaTemporalFK, string filCampo, string filFiltro)
        {
            return dReporte.empAptitudSaludOcupacional(ID_empresaTemporalFK, filCampo, filFiltro);
        }


        #endregion

        #region NOMINA
        public DataTable nomConsolidado(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.nomConsolidado(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }

        public DataTable nomDetallado(string ID_nomina, int incluirTotales)
        {
            return dReporte.nomDetallado(ID_nomina, incluirTotales);
        }

        public DataTable nomConsolidadoLiquidacion(string ID_contratoFK)
        {
            return dReporte.nomConsolidadoLiquidacion(ID_contratoFK);
        }

        public DataTable nomConsolidadoSegSocial(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.nomConsolidadoSegSocial(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }

        public DataTable nomLicencias(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.nomLicencias(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable nomTerceros(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.nomTerceros(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable nomIncapacidades(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.nomIncapacidades(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable nomIncapacidadesResumen(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.nomIncapacidadesResumen(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable nomIncapacidadesResumenTipo(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.nomIncapacidadesResumenTipo(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable nomEmbarazosYEspeciales(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.nomEmbarazosYEspeciales(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable nomIngresosRetenciones(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.nomIngresosRetenciones(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }

        public DataTable nomContabilidad(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.nomContabilidad(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }

        public DataTable nomHistorico(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.nomHistorico(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable Liquidaciones(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.Liquidaciones(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        } 
        #endregion

        #region SEGURIDAD SOCIAL
        public DataTable segPlanoPilaBuscar(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.segPlanoPilaBuscar(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }

        public DataTable segPlanoPila(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.segPlanoPila(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }

        public DataTable segPlanoPilaCatorcenal(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string ID_contratoFK)
        {
            return dReporte.segPlanoPilaCatorcenal(ID_empresaTemporalFK, fechaInicio, fechaFin, ID_contratoFK);
        }

        public DataTable segPlanoPilaXContratoXNovedad(string ID_empresaTemporalFK, string ID_contratoFK, string fechaInicio, string fechaFin, string nov_tipoNovedadFK)
        {
            return dReporte.segPlanoPilaXContratoXNovedad(ID_empresaTemporalFK, ID_contratoFK, fechaInicio, fechaFin, nov_tipoNovedadFK);
        }

        public DataTable segPlanoPilaLicenciasNoRemuneradas(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.segPlanoPilaLicenciasNoRemuneradas(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }

        public DataTable ResumenSegSocial(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.ResumenSegSocial(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }

        public DataTable TotalizadoSegSocial(string ID_empresaTemporalFK, string fechaInicio, string fechaFin)
        {
            return dReporte.TotalizadoSegSocial(ID_empresaTemporalFK, fechaInicio, fechaFin);
        }

        #endregion

        #region PRESTAMOS

        public DataTable presConsolidado(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.presConsolidado(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        #endregion
    
        #region GERENCIAL

        public DataTable gerencialPersonalActivo(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.gerencialPersonalActivo(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable gerencialRetiradoXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.gerencialRetiradoXFecha(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable gerencialIngresosXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.gerencialIngresosXFecha(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable gerencialNominaSalarios(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.gerencialNominaSalarios(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable gerencialNominaTiempoExtra(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.gerencialNominaTiempoExtra(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable gerencialNominaTiempoExtraHoras(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.gerencialNominaTiempoExtraHoras(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable gerencialNominaTiempoRecargoNocturno(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.gerencialNominaTiempoRecargoNocturno(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable gerencialNominaTiempoDominicales(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.gerencialNominaTiempoDominicales(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable gerencialAusentismoIncap(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos, string tipoNovedad)
        {
            return dReporte.gerencialAusentismoIncap(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos, tipoNovedad);
        }

        public DataTable gerencialAusentismoIncapGeneral(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos, string minimo, string maximo)
        {
            return dReporte.gerencialAusentismoIncapGeneral(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos, minimo, maximo);
        }

        public DataTable asesorXEmpresa(string ID_empresaTemporalFK, string filCampo, string filFiltro)
        {
            return dReporte.asesorXEmpresa(ID_empresaTemporalFK, filCampo, filFiltro);
        }

        public DataTable gerencialProvisiones(string ID_empresaTemporalFK, string filCampo, string filFiltro)
        {
            return dReporte.provEmpresasActivas(ID_empresaTemporalFK, filCampo, filFiltro);
        }

        public DataTable gerencialCarteraProvisiones(string ID_empresaTemporalFK, string año)
        {
            return dReporte.provCartera(ID_empresaTemporalFK, año);
        }

        public DataTable gerencialCarteraProvisionesPagadas(string ID_empresaTemporalFK, string ID_empresaUsuariaFK, string año)
        {
            return dReporte.provCarteraPagada(ID_empresaTemporalFK, ID_empresaUsuariaFK, año);
        }

        #endregion

        #region GERENCIAL GRAFICAS

        public DataTable gerencialGrafPersonalActivo(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.gerencialGrafPersonalActivo(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable gerencialGrafIngresosXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.gerencialGrafIngresosXFecha(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable gerencialGrafRetirosXFecha(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.gerencialGrafRetirosXFecha(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        public DataTable gerencialGrafNomina(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro, string filCampoDos, string filFiltroDos)
        {
            return dReporte.gerencialGrafNomina(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro, filCampoDos, filFiltroDos);
        }

        #endregion

        #region INTERESES DE CESANTIAS
        public DataTable cesInteresConsolidado(string ID_empresaTemporalFK, string fechaInicio, string fechaFin, string filCampo, string filFiltro)
        {
            return dReporte.cesInteresConsolidado(ID_empresaTemporalFK, fechaInicio, fechaFin, filCampo, filFiltro);
        }
        #endregion

        #region MAESTROS
        public DataTable maeEmpresasUsuarias()
        {
            return dReporte.maeEmpresasUsuarias();
        }
        public DataTable maeCentrosCosto(string ID_empresaUsuariaFK)
        {
            return dReporte.maeCentrosCosto(ID_empresaUsuariaFK);
        }
        #endregion
    }
}
