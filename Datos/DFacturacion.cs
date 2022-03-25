using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;


namespace Datos
{
    public class DFacturacion : DSQL
    {
        #region PREFACTURA

        public DataTable CalculaPrefactura(string nominas, string idFacturaNull)
        {
            //Hashtable param = new Hashtable();
            //param.Add("@nominas", nominas);
            //return procedureTable("fac_CalculaPrefactura", true, param);
            return queryTable(
                "SELECT SUM(totalEmpleados) totalEmpleados, AVG(prom_SalarioDevengado) AS prom_SalarioDevengado ," +
"sum(_nom_salarioDevengado) nom_salarioDevengado, sum(nom_dvAuxilioTransporte) nom_dvAuxilioTransporte, sum(nom_dvOtrosNoPrestacional) nom_dvOtrosNoPrestacional," +
"SUM(nom_ddEPSafiliado) nom_ddEPSafiliado, AVG(nom_ddEPSafiliado) prom_ddEPSafiliado, SUM(nom_ddAFPafiliado) nom_ddAFPafiliado," +
"AVG(nom_ddAFPafiliado) prom_ddAFPafiliado, SUM(nom_ddFSPafiliado) nom_ddFSPafiliado, AVG(nom_ddFSPafiliado) prom_ddFSPafiliado," +
"SUM(nom_ddReteFuente) nom_ddReteFuente, AVG(nom_ddReteFuente) prom_ddReteFuente, SUM(nom_EPSempresa) nom_EPSempresa, AVG(nom_EPSempresa) prom_EPSempresa," +
"SUM(nom_AFPempresa) nom_AFPempresa, AVG(nom_AFPempresa) prom_AFPempresa, SUM(nom_CCFempresa) nom_CCFempresa, AVG(nom_CCFempresa) prom_CCFempresa," +
"SUM(nom_PARAFempresa) nom_PARAFempresa, AVG(nom_PARAFempresa) prom_PARAFempresa, SUM(nom_ARLempresa) nom_ARLempresa, AVG(nom_ARLempresa) prom_ARLempresa," +
"SUM(nom_ProvPrima) nom_ProvPrima, AVG(nom_ProvPrima) prom_ProvPrima, SUM(nom_ProvCesantias) nom_ProvCesantias, AVG(nom_ProvCesantias) prom_ProvCesantias," +
"SUM(nom_ProvIntereses) nom_ProvIntereses, AVG(nom_ProvIntereses) prom_ProvIntereses, SUM(nom_ProvVacaciones) nom_ProvVacaciones," +
"AVG(nom_ProvVacaciones) prom_ProvVacaciones, MAX(ID_centroCostoFK) ID_centroCostoFK, MAX(nom_fechaInicio) nom_fechaInicio," +
"MAX(nom_fechaFin) nom_fechaFin, MAX(cli_nombre) cli_nombre, MAX(emp_nombre) emp_nombre, MAX(CAST(cen_provisiona AS INT)) cen_provisiona FROM (" +
                "SELECT count(ID_nominaBase) as totalEmpleados " +
                            //", sum(nom_salarioBase - rep_valorLicenciaMaternidad  - rep_valorLicenciaPaternidad) as nom_salarioDevengado " + 
                            ", AVG(nom_salarioDevengado) AS prom_SalarioDevengado " +
                            ", CASE WHEN nom_contratos.con_jornada='Medio Tiempo' " +
                            "  THEN sum((nom_salarioBase / 2) - rep_valorLicenciaMaternidad - rep_valorLicenciaPaternidad)" +
                            "  ELSE sum(nom_salarioBase - rep_valorLicenciaMaternidad - rep_valorLicenciaPaternidad) END as _nom_salarioDevengado" + 
                            ", sum(nom_dvAuxilioTransporte) nom_dvAuxilioTransporte " +
                            ", sum(nom_dvOtrosNoPrestacional) nom_dvOtrosNoPrestacional " +
                            ", SUM(nom_ddEPSafiliado) nom_ddEPSafiliado, AVG(nom_ddEPSafiliado) prom_ddEPSafiliado " +
                            ", SUM(nom_ddAFPafiliado) nom_ddAFPafiliado, AVG(nom_ddAFPafiliado) prom_ddAFPafiliado " +
                            ", SUM(nom_ddFSPafiliado) nom_ddFSPafiliado, AVG(nom_ddFSPafiliado) prom_ddFSPafiliado " +
                            ", SUM(nom_ddReteFuente) nom_ddReteFuente, AVG(nom_ddReteFuente) prom_ddReteFuente " +
                            ", SUM(nom_EPSempresa) nom_EPSempresa, AVG(nom_EPSempresa) prom_EPSempresa " +
                            ", SUM(nom_AFPempresa) nom_AFPempresa, AVG(nom_AFPempresa) prom_AFPempresa " +
                            ", SUM(nom_CCFempresa) nom_CCFempresa, AVG(nom_CCFempresa) prom_CCFempresa " +
                            ", SUM(nom_PARAFempresa) nom_PARAFempresa, AVG(nom_PARAFempresa) prom_PARAFempresa " +
                            ", SUM(nom_ARLempresa) nom_ARLempresa, AVG(nom_ARLempresa) prom_ARLempresa " +
                            ", SUM(nom_ProvPrima) nom_ProvPrima, AVG(nom_ProvPrima) prom_ProvPrima " +
                            ", SUM(nom_ProvCesantias) nom_ProvCesantias, AVG(nom_ProvCesantias) prom_ProvCesantias " +
                            ", SUM(nom_ProvIntereses) nom_ProvIntereses, AVG(nom_ProvIntereses) prom_ProvIntereses " +
                            ", SUM(nom_ProvVacaciones) nom_ProvVacaciones, AVG(nom_ProvVacaciones) prom_ProvVacaciones " +
                            ", MAX(nom_NomBase.ID_centroCostoFK) ID_centroCostoFK " +
                            ", MAX(nom_fechaInicio) nom_fechaInicio " +
                            ", MAX(nom_fechaFin) nom_fechaFin " +
                            ", MAX(cli_nombre) cli_nombre " +
                            ", MAX(emp_nombre) emp_nombre " +
                            ", MAX(CAST(cen_provisiona AS INT)) cen_provisiona " +
                            "FROM nom_Nominas " +
                            "INNER JOIN nom_NomBase ON nom_Nominas.ID_nomina = nom_NomBase.ID_nominaFK " +
                            "INNER JOIN nom_NomReporte ON nom_NomBase.ID_nominaBase = nom_NomReporte.ID_nominaBaseFK " +
                            "INNER JOIN car_Clientes ON nom_Nominas.ID_empresaUsuariaFK = car_Clientes.ID_cliente " +
                            "INNER JOIN car_CentrosCosto ON nom_NomBase.ID_centroCostoFK = car_CentrosCosto.ID_centroCosto " +
                            "INNER JOIN app_empresa ON nom_Nominas.ID_empresaTemporalFK = app_empresa.ID_empresa " +
                            "INNER JOIN nom_contratos ON nom_NomBase.ID_contratoFK = nom_contratos.ID_contrato " +
                            "WHERE nom_Nominas.ID_nomina IN(" + nominas + ") " + idFacturaNull + " GROUP BY nom_contratos.con_jornada ) TABLA");
        }


        public DataTable cuentaEmpleadosXnovedad(string nominas, string campoFiltro)
        {
            return queryTable("SELECT COUNT(" + campoFiltro + ") total " +
                                 "FROM nom_Nominas " +
                                 "INNER JOIN nom_NomBase ON nom_Nominas.ID_nomina = nom_NomBase.ID_nominaFK " +
                                 "WHERE nom_Nominas.ID_nomina IN(" + nominas + ") AND " + campoFiltro + ">0");
        }

        public DataTable CalculaTotalXConcepto(string nominas, string ID_padre)
        {
            return queryTable("SELECT COUNT(ID_contratoFK) totalempleados, sum(nov_valorPesos) nov_valorPesos, ID_concepto, con_nombre,  AVG(nov_valorPesos) promedio " +
                              "FROM( " +
                              "      SELECT ID_contratoFK, sum(nov_valorPesos) AS nov_valorPesos, ID_concepto, con_nombre " +
                              "      FROM dbo.nom_NovedadesLaborales " +
                              "      INNER JOIN dbo.nom_NomBase ON dbo.nom_NovedadesLaborales.ID_nominaBaseFK = dbo.nom_NomBase.ID_nominaBase " +
                              "      INNER JOIN app_Referencias ON nom_NovedadesLaborales.nov_tipoNovedadFK = app_Referencias.ref_valor " +
                              "      INNER JOIN car_FacConceptosXNovedad ON car_FacConceptosXNovedad.ID_novedadFK = app_Referencias.ID_referencia " +
                              "      INNER JOIN car_FacConceptos ON car_FacConceptosXNovedad.ID_conceptoFK = car_FacConceptos.ID_concepto " +
                              "      WHERE dbo.nom_NomBase.ID_nominaFK IN(" + nominas + ")  AND car_FacConceptos.con_estado=1 " +
                              "      AND ID_padre = " + ID_padre + " " +
                              "      GROUP BY ID_contratoFK, ID_concepto, con_nombre " +
                              "  )tbl GROUP BY ID_concepto, con_nombre");
        }

        //SE CALCULAN PARA DEVOLVER EN LA PREFACTURA
        public DataTable CalculaTotalLicenciaNoRemunerada(string nominas)
        {
            return queryTable("SELECT COUNT(ID_contratoFK) totalempleados, sum(nov_valorPesos) nov_valorPesos, AVG(nov_valorPesos) promedio " +
                              "FROM( " +
                              "      SELECT ID_contratoFK, sum(nov_valorPesos) AS nov_valorPesos " +
                              "      FROM dbo.nom_NovedadesLaborales " +
                              "      INNER JOIN dbo.nom_NomBase ON dbo.nom_NovedadesLaborales.ID_nominaBaseFK = dbo.nom_NomBase.ID_nominaBase " +
                              "      WHERE nov_tipoNovedadFK IN('LicenciaNoRemunerada', 'Sancion') AND dbo.nom_NomBase.ID_nominaFK IN(" + nominas + ") " +
                              "      GROUP BY ID_contratoFK" +
                              "  )tbl");
        }

        public DataTable CalculaTotalXBanco(string nominas, string campo_centroCosto, string nom_cuentaBanco, string filtro)
        {
            return queryTable("SELECT COUNT(ID_NOMINABASE) cuantos , MAX(" + campo_centroCosto +") valorCuota, MAX(" + campo_centroCosto + ")*COUNT(ID_NOMINABASE) valorTotal " +
                              "FROM dbo.nom_NomBase  " +
                              "INNER JOIN car_CentrosCosto ON nom_NomBase.ID_centroCostoFK = car_CentrosCosto.ID_centroCosto " +
                              "WHERE dbo.nom_NomBase.ID_nominaFK IN(" + nominas + ") AND nom_cuentaBanco " + filtro + "(" + nom_cuentaBanco + ") ");
        }

        public DataTable CalculaTotalXAdministracion(string nominas)
        {
            return queryTable("SELECT COUNT(ID_contratoFK) totalempleados, max(cen_cuotaAdmon) cen_cuotaAdmon, max(cen_tipoCuotaAdmon) cen_tipoCuotaAdmon, max(cen_cuotaAdmonParametro) cen_cuotaAdmonParametro ,sum(cen_cuotaAdmon) valorTotal " +
                              "FROM( " +
                              "     SELECT nom_NomBase.id_contratofk, cen_tipoCuotaAdmon ," +
                              "  CASE cen_tipoCuotaAdmon" +
                              "      WHEN 'Porcentaje' THEN((nom_salarioBase - rep_valorLicenciaMaternidad - rep_valorLicenciaPaternidad + rep_valorLicenciaNoRemunerada + nom_dvAuxilioTransporte + nom_dvOtros + nom_dvOtrosNoPrestacional + dbo.nom_NomBase.nom_ProvTotal + rep_valorVacaciones+(nom_EPSempresa +  nom_AFPempresa + nom_CCFempresa + nom_ARLempresa + nom_PARAFempresa)) * (cen_cuotaAdmon / 100))" +
                              "      WHEN 'Fija' THEN cen_cuotaAdmon" +
                              "  END AS cen_cuotaAdmon" +
                              " , cen_cuotaAdmon cen_cuotaAdmonParametro " +
                              "  FROM nom_NomBase" +
                              "  INNER JOIN car_CentrosCosto ON nom_NomBase.ID_centroCostoFK = car_CentrosCosto.ID_centroCosto" +
                              "  INNER JOIN nom_NomReporte ON nom_NomBase.ID_nominaBase = nom_NomReporte.ID_nominaBaseFK" +
                              "     WHERE dbo.nom_NomBase.ID_nominaFK IN(" + nominas + ") " +
                              ")tbl ");
        }

        public DataTable CalculaTotalXContratacionIngresos(string nominas)
        {
            return queryTable("SELECT ISNULL(SUM(totalEmpleados),0) totalEmpleados, ISNULL(AVG(cen_cuotaContratacion),0) valorUnitario, ISNULL(sum(valorTotal),0) valorTotal " +
                              "FROM( " +
                              "     SELECT COUNT(ID_nominaBase) totalEmpleados, cen_cuotaContratacion, COUNT(ID_nominaBase)*cen_cuotaContratacion valorTotal" +
                              "     FROM nom_Nominas " +
                                    "INNER JOIN nom_NomBase ON nom_Nominas.ID_nomina = nom_NomBase.ID_nominaFK " +
                                    "INNER JOIN nom_contratos ON nom_NomBase.ID_contratoFK = nom_contratos.ID_contrato " +
                                    "INNER JOIN car_CentrosCosto ON nom_NomBase.ID_centroCostoFK = car_CentrosCosto.ID_centroCosto " +
                                    "WHERE ID_nominaFK in (" + nominas + ") " +
                                    "AND con_fechaInicio BETWEEN nom_fechaInicio AND nom_fechaFin " +
                                    "GROUP BY cen_cuotaContratacion)TABLA");
        }

        public DataTable CalculaTotalXContratacionRetiros(string nominas)
        {
            return queryTable("SELECT ISNULL(SUM(totalEmpleados),0) totalEmpleados, ISNULL(AVG(cen_cuotaContratacion),0) valorUnitario, ISNULL(sum(valorTotal),0) valorTotal " +
                              "FROM( " +
                              "     SELECT COUNT(ID_nominaBase) totalEmpleados, cen_cuotaContratacion, COUNT(ID_nominaBase)*cen_cuotaContratacion valorTotal" +
                              "     FROM nom_Nominas " +
                                    "INNER JOIN nom_NomBase ON nom_Nominas.ID_nomina = nom_NomBase.ID_nominaFK " +
                                    "INNER JOIN nom_contratos ON nom_NomBase.ID_contratoFK = nom_contratos.ID_contrato " +
                                    "INNER JOIN car_CentrosCosto ON nom_NomBase.ID_centroCostoFK = car_CentrosCosto.ID_centroCosto " +
                                    "WHERE ID_nominaFK in (" + nominas + ") " +
                                    "AND con_fechaFin BETWEEN nom_fechaInicio AND nom_fechaFin " +
                                    "GROUP BY cen_cuotaContratacion)TABLA");
        }

        public void ActualizaTotalesXID(Int32 ID_factura, double subTotalNeto, double subTotalOtrosIngresos, double totalAntesIva, double fac_valorIVA, double reteiva, double retefuente, double reteica, double fac_valorTotal
            , double baseGravable, double por_baseGravable, double por_IVA, double por_reteIVA, double por_retefuente, double por_reteica
            , bool incluye_IVA, bool incluye_reteIVA, bool incluye_retefuente, bool incluye_reteica)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_factura", ID_factura);
            param.Add("@subTotalNeto", subTotalNeto);
            param.Add("@subTotalOtrosIngresos", subTotalOtrosIngresos);
            param.Add("@totalAntesIva", totalAntesIva);
            param.Add("@fac_valorIVA", fac_valorIVA);
            param.Add("@reteiva", reteiva);
            param.Add("@retefuente", retefuente);
            param.Add("@reteica", reteica);
            param.Add("@fac_valorTotal", fac_valorTotal);

            param.Add("@baseGravable", baseGravable);
            param.Add("@por_baseGravable", por_baseGravable);
            param.Add("@por_IVA", por_IVA);
            param.Add("@por_reteIVA", por_reteIVA);
            param.Add("@por_retefuente", por_retefuente);
            param.Add("@por_reteica", por_reteica);
            param.Add("@incluye_IVA", incluye_IVA);
            param.Add("@incluye_reteIVA", incluye_reteIVA);
            param.Add("@incluye_retefuente", incluye_retefuente);
            param.Add("@incluye_reteica", incluye_reteica);

            procedureTable("fac_ActualizaTotalesXID", true, param);
        }


        public DataTable ConsultaGeneral(string where)
        {
            Hashtable param = new Hashtable();
            param.Add("@where", where);
            return procedureTable("fac_ConsultaGeneral", true, param);
        }

        public DataTable ConsultaTotalXConcepto(int ID_concepto, int ID_facturaFK, int nivel, string zona)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_concepto", ID_concepto);
            param.Add("@ID_facturaFK", ID_facturaFK);
            param.Add("@nivel", nivel);
            param.Add("@zona", zona);
            return procedureTable("fac_TotalXConcepto", true, param);
        }

        public void AsociarNominasConPrefactura(string ID_factura, string nominas, string totalIngresos, string totalRetiros)
        {
            queryTable("UPDATE nom_Nominas SET ID_facturaFK='" + ID_factura + "' WHERE ID_nomina IN (" + nominas + ")");
            queryTable("UPDATE car_Facturas SET nominas='" +  nominas.Replace("'","") + "', totalIngresos=" + totalIngresos + ", totalRetiros=" + totalRetiros + " WHERE ID_factura=" + ID_factura + "");
        }

        public DataTable ConsultaConceptosGeneral(string con_estado)
        {
            Hashtable param = new Hashtable();
            param.Add("@con_estado", con_estado);
            return procedureTable("con_ConsultaGeneral", true, param);
        }

        public DataTable ConsultaConceptosEditables()
        {
            return procedureTable("con_ConsultaEditable", false);
        }

        public DataTable ConsultaNovedadesXConcepto(string ID_concepto)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_concepto", ID_concepto);
            return procedureTable("con_ConsultaNovedadesXConcepto", true, param);
        }

        public DataTable conceptoInsertar(string ID_padre, string con_nombre, string con_codigo, string con_orden, string ID_usuarioRegistra, string con_zona, bool con_editable)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_padre", ID_padre);
            param.Add("@con_nombre", con_nombre);
            param.Add("@con_codigo", con_codigo);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);
            param.Add("@con_orden", con_orden);
            param.Add("@con_zona", con_zona);
            param.Add("@con_editable", con_editable);
            return procedureTable("con_Insertar", true, param);
        }

        public void conceptoActualizar(string ID_concepto, string ID_padre, string con_nombre, string con_codigo, string con_orden, bool con_estado, string con_zona, bool con_editable)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_concepto", ID_concepto);
            param.Add("@ID_padre", ID_padre);
            param.Add("@con_nombre", con_nombre);
            param.Add("@con_codigo", con_codigo);
            param.Add("@con_orden", con_orden);
            param.Add("@con_estado", con_estado);
            param.Add("@con_zona", con_zona);
            param.Add("@con_editable", con_editable);

            procedureTable("con_Actualizar", true, param);
        }
        
        public void ConceptosXNovedadInsertar(string ID_conceptoFK, string ID_novedadFK, string cxn_ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_conceptoFK", ID_conceptoFK);
            param.Add("@ID_novedadFK", ID_novedadFK);
            param.Add("@cxn_ID_usuarioRegistraFK", cxn_ID_usuarioRegistraFK);

            procedureTable("car_ConceptosXNovedadInsertar", true, param);
        }

        public void ModuloXPerfilEliminar(string Id_Perfil)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@Id_Perfil", Id_Perfil);

            procedureTable("app_ModuloXPerfilEliminar", true, param);
        }

        public void ConceptosXNovedadEliminar(string ID_conceptoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_conceptoFK", ID_conceptoFK);

            procedureTable("car_ConceptosXNovedadEliminar", true, param);
        }

        public void AprobarPrefactura(string ID_factura, string ID_usuarioActualiza)
        {
            queryTable("UPDATE car_Facturas SET fac_estado='Aprobada', fac_fechaActualiza = getdate(),ID_usuarioActualiza = '" + ID_usuarioActualiza + "'  WHERE ID_factura='" + ID_factura + "'");
        }

        #endregion

        #region FACTURAS

        public DataTable consultaFacturaXID(Int32 ID_factura)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_factura", ID_factura);
            return procedureTable("fac_ConsultaXID", true, param);
        }

        public DataTable consultaFacConceptosPadre(string con_tipo)
        {
            Hashtable param = new Hashtable();
            param.Add("@con_tipo", con_tipo);
            return procedureTable("con_ConsultaPadres", true,param);
        }

        public DataTable consultaFacConceptosXPadre(Int32 ID_padre)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_padre", ID_padre);
            return procedureTable("con_ConsultaXPadre", true, param);
        }

        public DataTable consultaFacConceptosXID(Int32 ID_concepto)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_concepto", ID_concepto);
            return procedureTable("con_ConsultaXID", true, param);
        }

        public DataTable consultaConceptosXFactura(Int32 ID_facturaFK, Int16 conSaldo)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_facturaFK", ID_facturaFK);
            param.Add("@conSaldo", conSaldo);
            return procedureTable("con_ConsultaXFactura", true, param);
        }

        public DataTable consultaConceptosDisponible4X1000(string ID_centroCosto_FK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_centroCosto_FK", ID_centroCosto_FK);
            return procedureTable("con_ConsultaDisponible4X1000", true, param);
        }

        public DataTable consultaFacturasXCliente(Int64 ID_clienteFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            return procedureTable("fac_ConsultaXCliente", true, param);
        }

        public DataTable consultaFacturasXAsociarXCliente(Int64 ID_clienteFK, Int64 ID_empresaFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@ID_empresaFK", ID_empresaFK);

            return procedureTable("fac_ConsultaXAsociarXCliente", true, param);
        }

        public DataTable consultaRegistroPagoXCliente(Int64 ID_clienteFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            return procedureTable("fac_ConsultaRegistroPagoXCliente", true, param);
        }

        public DataTable consultaRegistroPagoXCliente(Int64 ID_clienteFK, string filtroEstado = "")
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@filtroEstado", filtroEstado);

            return procedureTable("fac_ConsultaRegistroPagoXCliente", true, param);
        }

        public DataSet consultaRegistroPagoXID(string ID_registroPago)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_registroPago", ID_registroPago);
            return procedureDataSet("fac_ConsultaRegistroPagoXID", true, param);
        }

        public DataTable Insertar(string ID_clienteFK, string ID_centroCostoFK,string fac_tipo, string fac_empresa, string fac_fecha, string fac_comentario, double fac_valorSaldo, double fac_valorIVA, double fac_valorTotal, string fac_estado, string ID_usuarioRegistra, string fac_fechaInicio, string fac_fechaFin)
        { 
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK",ID_clienteFK);
            param.Add("@ID_centroCostoFK", ID_centroCostoFK);
            param.Add("@fac_tipo", fac_tipo);
            param.Add("@fac_empresa",fac_empresa);
            param.Add("@fac_fecha", fac_fecha);
            param.Add("@fac_comentario", fac_comentario);
            param.Add("@fac_valorSaldo", fac_valorSaldo);
            param.Add("@fac_valorIVA",fac_valorIVA);
            param.Add("@fac_valorTotal",fac_valorTotal);
            param.Add("@fac_estado",fac_estado);
            param.Add("@ID_usuarioRegistra",ID_usuarioRegistra);
            param.Add("@fac_fechaInicio", fac_fechaInicio);
            param.Add("@fac_fechaFin", fac_fechaFin);

            return procedureTable("fac_Insertar", true, param);

        }

        public DataTable InsertaConceptoXFactura(string ID_conceptoFK, string ID_facturaFK, int con_empleados, double con_valorSaldo, double con_valor, double con_valorUnitario, string ID_usuarioRegistra)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_conceptoFK", ID_conceptoFK);
            param.Add("@ID_facturaFK", ID_facturaFK);
            param.Add("@con_empleados", con_empleados);
            param.Add("@con_valorSaldo", con_valorSaldo);
            param.Add("@con_valor", con_valor);
            param.Add("@con_valorUnitario", con_valorUnitario);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);
            return procedureTable("fac_InsertaConceptoXFactura", true, param);
        }

        public void EliminarConceptoXFactura(string ID_conceptoXfactura)
        {
            queryTable("DELETE FROM car_FacConceptosXFactura WHERE ID_conceptoXfactura='" + ID_conceptoXfactura + "'");
        }

        public void ActualizaPagoCredito(string ID_factura, double fac_pagoCredito)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_factura", ID_factura);
            param.Add("@fac_pagoCredito", fac_pagoCredito);
            procedureTable("fac_ActualizaPagoCredito", true, param);
        }

        public void PrefacturaReversar(string ID_factura, string ID_usuarioActualiza)
        {
            queryTable("UPDATE car_Facturas SET fac_estado='Generada', fac_fechaActualiza=getdate(),ID_usuarioActualiza = '" + ID_usuarioActualiza + "' WHERE ID_factura='" + ID_factura + "'");
        }

        public DataTable InsertarRegistroPago(string ID_clienteFK, string ID_empresaFK, long reg_valor, string reg_estado, string ID_usuarioRegistra)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@ID_empresaFK", ID_empresaFK);
            param.Add("@reg_valor", reg_valor);
            param.Add("@reg_estado", reg_estado);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);

            return procedureTable("fac_InsertaRegistroPago", true, param);

        }

        public DataTable InsertarPagoTransaccion(string ID_registroPagoFK, string tra_tipo, string tra_cheque, string tra_transferencia, string ID_bancoFK, string ID_bancoDestinoFK, Int64 tra_valor, string tra_fecha, string tra_observacion, string ID_usuarioRegistra)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_registroPagoFK", ID_registroPagoFK);
            param.Add("@tra_tipo", tra_tipo);
            param.Add("@tra_cheque", tra_cheque);
            param.Add("@tra_transferencia", tra_transferencia);
            param.Add("@ID_bancoFK", ID_bancoFK);
            param.Add("@ID_bancoDestinoFK", ID_bancoDestinoFK);
            param.Add("@tra_valor", tra_valor);
            param.Add("@tra_fecha", tra_fecha);
            param.Add("@tra_observacion", tra_observacion);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);

            return procedureTable("fac_InsertaPagoTran", true, param);

        }

        public void InsertarPagoXConcepto(string ID_registroPagoFK, string ID_conceptoXFacturaFK, string pag_valor, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_registroPagoFK", ID_registroPagoFK);
            param.Add("@ID_conceptoXFacturaFK", ID_conceptoXFacturaFK);
            param.Add("@pag_valor", pag_valor);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            procedureTable("fac_InsertaPagoXConcepto", true, param);

        }

        public void DesasociarPagoXFactura(string ID_registroPagoFK, string ID_usuarioEliminaFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_registroPagoFK", ID_registroPagoFK);
            param.Add("@ID_usuarioEliminaFK", ID_usuarioEliminaFK);

            procedureTable("fac_DeasociarPago", true, param);
        }

        public void AnularFactura(string ID_factura, string ID_usuarioActualiza, string fac_observacion)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_factura", ID_factura);
            param.Add("@ID_usuarioActualiza", ID_usuarioActualiza);
            param.Add("@fac_observacion", fac_observacion);

            procedureTable("fac_AnularXID", true, param);
        }
        
        public void ActualizaSaldoDeduccion(string ID_factura, string valorDeduccion)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_factura", ID_factura);
            param.Add("@valorDeduccion", valorDeduccion);

            procedureTable("fac_ActualizaSaldoDeduccion", true, param);
        }

        public void ActualizaConsecutivo(string ID_factura, string fac_consecutivo)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_factura", ID_factura);
            param.Add("@fac_consecutivo", fac_consecutivo);

            procedureTable("fac_ActualizaConsecutivo", true, param);
        }

        public DataTable ExisteDeduccion(string ID_factura)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_factura", ID_factura);

            return procedureTable("fac_ExistenDeducciones", true, param);
        }

        #endregion

        #region NOTAS DEBITO Y CREDITO

        public DataTable notasConsultarXCliente(int ID_clienteFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);

            return procedureTable("not_ConsultaXCliente", true, param);
        }

        public DataTable notasConsultarXId(int ID_nota)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_nota", ID_nota);

            return procedureTable("not_ConsultaXID", true, param);
        }

        public DataTable notasInsertar(string ID_facturaFK, string not_tipo, long not_valor, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_facturaFK", ID_facturaFK);
            param.Add("@not_tipo", not_tipo);
            param.Add("@not_valor", not_valor);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            return procedureTable("not_Insertar", true, param);
        }

        public DataTable notasInsertarConcepto(string ID_notaFK, string ID_facturaFK, string not_tipoFK, string ID_conceptoXfacturaFK, long not_valor, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_notaFK", ID_notaFK);
            param.Add("@ID_facturaFK", ID_facturaFK);
            param.Add("@not_tipoFK", not_tipoFK);
            param.Add("@ID_conceptoXfacturaFK", ID_conceptoXfacturaFK);
            param.Add("@not_valor", not_valor);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            return procedureTable("not_InsertarConcepto", true, param);
        }

        #endregion

        #region ANTICIPOS DE FACTURAS

        public DataTable consultaAnticiposXCliente(string ID_clienteFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            return procedureTable("fac_ConsultaAnticiposXCliente", true, param);
        }

        public DataTable consultaAnticiposXID(string ID_anticipo)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_anticipo", ID_anticipo);
            return procedureTable("fac_ConsultaAnticiposXID", true, param);
        }

        public DataTable InsertaAnticipo(string ID_clienteFK, string ID_centroCostoFK, string ant_valor, string ant_fechaDesembolso, string ant_observacion, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@ID_centroCostoFK", ID_centroCostoFK);
            param.Add("@ant_valor", ant_valor);
            param.Add("@ant_fechaDesembolso", ant_fechaDesembolso);
            param.Add("@ant_observacion", ant_observacion);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            return procedureTable("fac_InsertaAnticipo", true, param);
        }

        public DataTable consultaFacturasXAsociarAnticipo(string ID_clienteFK, string valorAnticipo)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@valorAnticipo", valorAnticipo);
            return procedureTable("fac_ConsultaXAsociarAnticipo", true, param);
        }

        public DataTable actualizaAnticipo(string ID_anticipo, string ID_facturaFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_anticipo", ID_anticipo);
            param.Add("@ID_facturaFK", ID_facturaFK);

            return procedureTable("fac_ActualizaAnticipo", true, param);
        }

        #endregion

        #region PROVEEDORES

        public void provInsertar(int ID_proveedor, int ID_empresaFK, string fac_consecutivo, string fac_concepto, double fac_valor, string fac_archivo, string fac_fechaExpedicion, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_proveedor", ID_proveedor);
            param.Add("@ID_empresaFK", ID_empresaFK);
            param.Add("@fac_consecutivo", fac_consecutivo);
            param.Add("@fac_concepto", fac_concepto);
            param.Add("@fac_valor", fac_valor);
            param.Add("@fac_archivo", fac_archivo);
            param.Add("@fac_fechaExpedicion", fac_fechaExpedicion);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            procedureTable("pro_InsertarFactura", true, param);

        }

        public DataTable provConsultar(int ID_proveedor)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_proveedor", ID_proveedor);

            return procedureTable("pro_ConsultaFacturas", true, param);

        }

        public DataTable proConsultaFacturaXID(int ID_facturaProveedor)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_facturaProveedor", ID_facturaProveedor);

            return procedureTable("pro_ConsultaFacturaXID", true, param);

        }

        public DataTable  provInsertarRegistroPago(string ID_proveedorFK,string ID_empresaFK, string ID_facturaProveedorFK, long reg_valor, string ID_usuarioRegistra)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_proveedorFK", ID_proveedorFK);
            param.Add("@ID_empresaFK", ID_empresaFK);
            param.Add("@ID_facturaProveedorFK", ID_facturaProveedorFK);
            param.Add("@reg_valor", reg_valor);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);

            return procedureTable("pro_InsertaRegistroPago", true, param);
        }

        public DataTable provConsultaRegistroPagoXProveedor(Int64 ID_proveedorFK, string filtroEstado = "")
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_proveedorFK", ID_proveedorFK);
            param.Add("@filtroEstado", filtroEstado);

            return procedureTable("pro_ConsultaRegistroPagoXProveedor", true, param);
        }

        public DataTable provConsultaFacConceptos()
        {
            return procedureTable("pro_ConsultaConceptos", false);
        }

        
        #endregion

        #region REPORTES

        public DataTable rep_empresas(string where = "")
        {
            return queryTable("SELECT * FROM app_empresa WHERE emp_estado=1 " + where);
        }

        public DataTable rep_FacturasXCliente(Int64 ID_clienteFK, string fac_empresa, Int64 ID_centroCosto)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@fac_empresa", fac_empresa);
            param.Add("@ID_centroCosto", ID_centroCosto);

            return procedureTable("rep_FacturasXCliente", true, param);
        }

        public DataTable rep_ValorXGrupoConcepto(Int64 ID_facturaFK, Int64  ID_padre)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_facturaFK", ID_facturaFK);
            param.Add("@ID_padre", ID_padre);
            return procedureTable("rep_ValorXGrupoConcepto", true, param);
        }

        public DataTable rep_ValorXGrupoXCliente(Int64 ID_clienteFK, Int64 ID_padre, Int64 ID_empresaFK, Int64 ID_centroCosto)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@ID_padre", ID_padre);
            param.Add("@ID_empresaFK", ID_empresaFK);
            param.Add("@ID_centroCosto", ID_centroCosto);

            return procedureTable("rep_ValorXGrupoXCliente", true, param);
        }

        public DataTable rep_ValorXGrupoXClientePagado(Int64 ID_clienteFK, Int64 ID_padre, Int64 ID_empresaFK, Int64 ID_centroCosto)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@ID_padre", ID_padre);
            param.Add("@ID_empresaFK", ID_empresaFK);
            param.Add("@ID_centroCosto", ID_centroCosto);

            return procedureTable("rep_ValorXGrupoXClientePagado", true, param);
        }

        public DataTable rep_AbonosProvisionalesXCliente(Int64 ID_clienteFK, Int64 ID_empresaFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@ID_empresaFK", ID_empresaFK);

            return procedureTable("rep_AbonosProvisionalesXCliente", true, param);
        }

        public DataTable rep_AnticiposXCliente(string ID_clienteFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);

            return procedureTable("rep_ConsultaAnticiposXCliente", true, param);
        }

        public DataTable rep_FacturasPagadasXCliente(Int64 ID_clienteFK, string fac_empresa, Int64 ID_centroCosto)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@fac_empresa", fac_empresa);
            param.Add("@ID_centroCosto", ID_centroCosto);

            return procedureTable("rep_FacturasPagadasXCliente", true, param);
        }

        public DataTable rep_ValorXGrupoConceptoPagado(Int64 ID_facturaFK, Int64 ID_padre, Int64 ID_registroPagoFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_facturaFK", ID_facturaFK);
            param.Add("@ID_padre", ID_padre);
            param.Add("@ID_registroPagoFK", ID_registroPagoFK);
            return procedureTable("rep_ValorXGrupoConceptoPagado", true, param);
        }

        public DataTable rep_MovimientosXfecha(string fechaInicio, string fechaFin, string tipoMovimiento, string empresa, string cliente)
        {
            Hashtable param = new Hashtable();
            param.Add("@fechaInicio", fechaInicio);
            param.Add("@fechaFin", fechaFin);
            param.Add("@tipoMovimiento", tipoMovimiento);
            param.Add("@empresa", empresa);
            param.Add("@cliente", cliente);

            return procedureTable("rep_MovimientosXFecha", true, param);
            
        }

        public DataTable rep_CuentasXPagarXEmpresa(Int64 ID_empresaFK,Int64 ID_proveedorFK)
        {
            Hashtable param = new Hashtable();
            param.Add("@ID_empresaFK", ID_empresaFK);
            param.Add("@ID_proveedorFK", ID_proveedorFK);

            return procedureTable("rep_CuentasXPagarXEmpresa", true, param);
        }

    #endregion

    }
}
