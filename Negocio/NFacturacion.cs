using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Negocio
{
    public class NFacturacion
    {
        Datos.DFacturacion dFactura = new Datos.DFacturacion();

        #region PREFACTURA
        public DataTable CalculaPrefactura(string nominas, string idFacturaNull = " AND nom_Nominas.ID_facturaFK IS NULL")
        {
            return dFactura.CalculaPrefactura(nominas, idFacturaNull);
        }

        public DataTable ConsultaGeneral(string where)
        {
            return dFactura.ConsultaGeneral(where);
        }

        public DataTable ConsultaTotalXConcepto(int ID_concepto, int ID_facturaFK, int nivel, string zona = "SubTotal")
        {
            return dFactura.ConsultaTotalXConcepto(ID_concepto, ID_facturaFK, nivel, zona);
        }

        public DataTable cuentaEmpleadosXnovedad(string nominas, string campoFiltro)
        {
            return dFactura.cuentaEmpleadosXnovedad(nominas, campoFiltro);
        }

        public void AsociarNominasConPrefactura(string ID_factura, string nominas, string totalIngresos, string totalRetiros)
        {
            dFactura.AsociarNominasConPrefactura(ID_factura, nominas, totalIngresos, totalRetiros);
        }

        public DataTable CalculaTotalXConcepto(string nominas, string ID_concepto)
        {
            return dFactura.CalculaTotalXConcepto(nominas, ID_concepto);
        }

        public DataTable CalculaTotalLicenciaNoRemunerada(string nominas)
        {
            return dFactura.CalculaTotalLicenciaNoRemunerada(nominas);
        }

        public DataTable CalculaTotalXBanco(string nominas, string campo_centroCosto, string nom_cuentaBanco, string filtro = "IN")
        {
            return dFactura.CalculaTotalXBanco(nominas, campo_centroCosto, nom_cuentaBanco, filtro);
        }

        public DataTable CalculaTotalXAdministracion(string nominas)
        {
            return dFactura.CalculaTotalXAdministracion(nominas);
        }

        public DataTable CalculaTotalXContratacionIngresos(string nominas)
        {
            return dFactura.CalculaTotalXContratacionIngresos(nominas);
        }

        public DataTable CalculaTotalXContratacionRetiros(string nominas)
        {
            return dFactura.CalculaTotalXContratacionRetiros(nominas);
        }

        public DataTable ConsultaConceptosGeneral(string con_estado)
        {
            return dFactura.ConsultaConceptosGeneral(con_estado);
        }

        public DataTable ConsultaConceptosEditables()
        {
            return dFactura.ConsultaConceptosEditables();
        }

        public DataTable ConsultaNovedadesXConcepto(string ID_concepto)
        {
            return dFactura.ConsultaNovedadesXConcepto(ID_concepto);
        }

        public DataTable conceptoInsertar(string ID_padre, string con_nombre, string con_codigo, string con_orden, string ID_usuarioRegistra, string con_zona, bool con_editable)
        {
            return dFactura.conceptoInsertar(ID_padre, con_nombre, con_codigo, con_orden, ID_usuarioRegistra, con_zona, con_editable);
        }

        public void conceptoActualizar(string ID_concepto, string ID_padre, string con_nombre, string con_codigo, string con_orden, bool con_estado, string con_zona, bool con_editable)
        {
            dFactura.conceptoActualizar(ID_concepto, ID_padre, con_nombre, con_codigo, con_orden, con_estado, con_zona, con_editable);
        }

        public void ConceptosXNovedadInsertar(string ID_conceptoFK, string ID_novedadFK, string cxn_ID_usuarioRegistraFK)
        {
            dFactura.ConceptosXNovedadInsertar(ID_conceptoFK, ID_novedadFK, cxn_ID_usuarioRegistraFK);
        }

        public void ConceptosXNovedadEliminar(string ID_conceptoFK)
        {
            dFactura.ConceptosXNovedadEliminar(ID_conceptoFK);
        }

        public void ActualizaTotalesXID(Int32 ID_factura, double subTotalNeto, double subTotalOtrosIngresos, double totalAntesIva, double fac_valorIVA, double reteiva, double retefuente, double reteica, double fac_valorTotal
            , double baseGravable, double por_baseGravable, double por_IVA, double por_reteIVA, double por_retefuente, double por_reteica
            , bool incluye_IVA, bool incluye_reteIVA, bool incluye_retefuente, bool incluye_reteica)
        {
            dFactura.ActualizaTotalesXID(ID_factura, subTotalNeto, subTotalOtrosIngresos, totalAntesIva, fac_valorIVA, reteiva, retefuente, reteica, fac_valorTotal
                                        , baseGravable, por_baseGravable, por_IVA, por_reteIVA, por_retefuente, por_reteica
                                        , incluye_IVA, incluye_reteIVA, incluye_retefuente, incluye_reteica);
        }

        public void AprobarPrefactura(string ID_factura, string ID_usuarioActualiza)
        {
            dFactura.AprobarPrefactura(ID_factura, ID_usuarioActualiza);
        }

        public void ActualizaPagoCredito(string ID_factura, double pagoCredito)
        {
            dFactura.ActualizaPagoCredito(ID_factura, pagoCredito);
        }
        #endregion

        #region FACTURAS

        public DataTable consultaFacuraXID(Int32 ID_factura)
        {
            return dFactura.consultaFacturaXID(ID_factura);
        }

        public DataTable consultaFacConceptosPadres(string con_tipo)
        {
            return dFactura.consultaFacConceptosPadre(con_tipo);
        }

        public DataTable consultaFacConceptosXPadre(Int32 ID_padre)
        {
            return dFactura.consultaFacConceptosXPadre(ID_padre);
        }

        public DataTable consultaFacConceptosXID(Int32 ID_concepto)
        {
            return dFactura.consultaFacConceptosXID(ID_concepto);
        }

        public DataTable consultaConceptosXFactura(Int32 ID_facturaFK, Int16 conSaldo = 0)
        {
            return dFactura.consultaConceptosXFactura(ID_facturaFK, conSaldo);
        }

        public DataTable consultaConceptosDisponible4X1000(string ID_centroCosto_FK)
        {
            return dFactura.consultaConceptosDisponible4X1000(ID_centroCosto_FK);
        }

        public DataTable consultaFacturasXCliente(Int64 ID_clienteFK)
        {
            return dFactura.consultaFacturasXCliente(ID_clienteFK);
        }

        public DataTable consultaFacturasXAsociarXCliente(Int64 ID_clienteFK, Int64 ID_empresaFK)
        {
            return dFactura.consultaFacturasXAsociarXCliente(ID_clienteFK, ID_empresaFK);
        }

        public DataTable consultaRegistroPagoXCliente(Int64 ID_clienteFK, string filtroEstado = "")
        {
            return dFactura.consultaRegistroPagoXCliente(ID_clienteFK, filtroEstado);
        }

        public DataSet consultaRegistroPagoXID(string ID_registroPago)
        {
            return dFactura.consultaRegistroPagoXID(ID_registroPago);
        }

        public DataTable Insertar(string ID_clienteFK, string ID_centroCostoFK, string fac_tipo, string fac_empresa, string fac_fecha, string fac_comentario, double fac_valorSaldo, double fac_valorIVA, double fac_valorTotal, string fac_estado, string ID_usuarioRegistra, string fac_fechaInicio, string fac_fechaFin)
        {
            return dFactura.Insertar(ID_clienteFK, ID_centroCostoFK, fac_tipo, fac_empresa, fac_fecha, fac_comentario, fac_valorSaldo, fac_valorIVA, fac_valorTotal, fac_estado, ID_usuarioRegistra, fac_fechaInicio, fac_fechaFin);
        }

        public DataTable InsertaConceptoXFactura(string ID_conceptoFK, string ID_facturaFK, int con_empleados, double con_valorSaldo, double con_valor, double con_valorUnitario, string ID_usuarioRegistra)
        {
            return dFactura.InsertaConceptoXFactura(ID_conceptoFK, ID_facturaFK, con_empleados, con_valorSaldo, con_valor, con_valorUnitario, ID_usuarioRegistra);
        }

        public void EliminarConceptoXFactura(string ID_conceptoXfactura)
        {
            dFactura.EliminarConceptoXFactura(ID_conceptoXfactura);
        }

        public DataTable InsertarRegistroPago(string ID_clienteFK, string ID_empresaFK, Int64 reg_valor, string reg_estado, string ID_usuarioRegistra)
        { 
            return dFactura.InsertarRegistroPago( ID_clienteFK, ID_empresaFK, reg_valor,  reg_estado,  ID_usuarioRegistra);
        }

        public DataTable InsertarPagoTransaccion(string ID_registroPagoFK, string tra_tipo, string tra_cheque, string tra_transferencia, string ID_bancoFK, string ID_bancoDestinoFK, Int64 tra_valor,string tra_fecha, string tra_observacion, string ID_usuarioRegistra)
        {
            return dFactura.InsertarPagoTransaccion(ID_registroPagoFK, tra_tipo, tra_cheque, tra_transferencia, ID_bancoFK, ID_bancoDestinoFK, tra_valor,tra_fecha, tra_observacion, ID_usuarioRegistra);
        }

        public void InsertarPagoXConcepto(string ID_registroPagoFK, string ID_conceptoXFacturaFK, string pag_valor, string ID_usuarioRegistraFK)
        {
            dFactura.InsertarPagoXConcepto(ID_registroPagoFK, ID_conceptoXFacturaFK, pag_valor, ID_usuarioRegistraFK);
        }

        public void DesasociarPagoXFactura(string ID_registroPagoFK, string ID_usuarioEliminaFK)
        {
            dFactura.DesasociarPagoXFactura(ID_registroPagoFK, ID_usuarioEliminaFK);
        }

        public void ActualizaConsecutivo(string ID_factura, string fac_consecutivo)
        {
            dFactura.ActualizaConsecutivo(ID_factura, fac_consecutivo);
        }

        public void AnularFactura(string ID_factura, string ID_usuarioActualiza, string fac_observacion)
        {
            dFactura.AnularFactura(ID_factura, ID_usuarioActualiza, fac_observacion);
        }

        public void PrefacturaReversar(string ID_factura, string ID_usuarioActualiza)
        {
            dFactura.PrefacturaReversar(ID_factura, ID_usuarioActualiza);
        }

        public void ActualizaSaldoDeduccion(string ID_factura, string valorDeduccion)
        {
            dFactura.ActualizaSaldoDeduccion(ID_factura, valorDeduccion);
        }

        public DataTable ExisteDeduccion(string ID_factura)
        {
            return dFactura.ExisteDeduccion(ID_factura);
        }
        
        #endregion

        #region NOTAS DEBITO Y CREDITO

        public DataTable notasInsertar(string ID_facturaFK, string not_tipo, long not_valor, string ID_usuarioRegistraFK)
        {
            return dFactura.notasInsertar(ID_facturaFK, not_tipo, not_valor, ID_usuarioRegistraFK);
        }

        public DataTable notasInsertarConcepto(string ID_notaFK, string ID_facturaFK, string not_tipoFK, string ID_conceptoXfacturaFK, long not_valor, string ID_usuarioRegistraFK)
        {
            return dFactura.notasInsertarConcepto(ID_notaFK, ID_facturaFK, not_tipoFK, ID_conceptoXfacturaFK, not_valor, ID_usuarioRegistraFK);
        }

        public DataTable notasConsultarXCliente(int ID_clienteFK)
        {
            return dFactura.notasConsultarXCliente(ID_clienteFK);
        }

        public DataTable notasConsultarXId(int ID_nota)
        {
            return dFactura.notasConsultarXId(ID_nota);
        }
        #endregion

        #region ORDENES DE DESEMBOLSO - ANTICIPOS A FACTURAS

        public DataTable consultaAnticiposXCliente(string ID_clienteFK)
        {
            return dFactura.consultaAnticiposXCliente(ID_clienteFK);
        }

        public DataTable consultaAnticiposXID(string ID_anticipo)
        {
            return dFactura.consultaAnticiposXID(ID_anticipo);
        }


        public DataTable InsertaAnticipo(string ID_clienteFK, string ID_centroCostoFK, string ant_valor, string ant_fechaDesembolso, string and_observacion, string ID_usuarioRegistraFK)
        {
            return dFactura.InsertaAnticipo(ID_clienteFK, ID_centroCostoFK, ant_valor, ant_fechaDesembolso, and_observacion, ID_usuarioRegistraFK);
        }

        public DataTable consultaFacturasXAsociarAnticipo(string ID_clienteFK, string valorAnticipo)
        {
            return dFactura.consultaFacturasXAsociarAnticipo(ID_clienteFK, valorAnticipo);
        }

        public void actualizaAnticipo(string ID_anticipo, string ID_facturaFK)
        {
            dFactura.actualizaAnticipo(ID_anticipo, ID_facturaFK);
        }

        #endregion

        #region PROVEEDORES
        public void provInsertar(int ID_proveedor, int ID_empresaFK, string fac_consecutivo, string fac_concepto, double fac_valor, string fac_archivo, string fac_fechaExpedicion, string ID_usuarioRegistraFK)
        {
            dFactura.provInsertar(ID_proveedor, ID_empresaFK, fac_consecutivo, fac_concepto, fac_valor, fac_archivo, fac_fechaExpedicion, ID_usuarioRegistraFK);
        }

        public DataTable provConsultar(int ID_proveedor)
        {
            return dFactura.provConsultar(ID_proveedor);
        }

        public DataTable proConsultaFacturaXID(int ID_facturaProveedor)
        {
            return dFactura.proConsultaFacturaXID(ID_facturaProveedor);
        }
        
        public DataTable  provInsertarRegistroPago(string ID_proveedorFK,string ID_empresaFK, string ID_facturaProveedorFK, long reg_valor, string ID_usuarioRegistra)
        {
            return dFactura.provInsertarRegistroPago(ID_proveedorFK,ID_empresaFK, ID_facturaProveedorFK, reg_valor, ID_usuarioRegistra);
        }

        public DataTable provConsultaRegistroPagoXProveedor(Int64 ID_proveedorFK, string filtroEstado = "")
        {
            return dFactura.provConsultaRegistroPagoXProveedor(ID_proveedorFK, filtroEstado);
        }

        public DataTable provConsultaFacConceptos()
        {
            return dFactura.provConsultaFacConceptos();
        }
        #endregion

        #region REPORTES

        public DataTable repEmpresas(string ID_empresa = "")
        {
            if (ID_empresa != null && ID_empresa != "" && ID_empresa != "-1" )
                return dFactura.rep_empresas(" AND ID_empresa='" + ID_empresa + "'");
            else
                return dFactura.rep_empresas();
        }

        public DataTable repFacturasXCliente(Int64 ID_clienteFK, string fac_empresa, string ID_centroCosto)
        {
            if( ID_centroCosto!=null && ID_centroCosto!="")
                return dFactura.rep_FacturasXCliente(ID_clienteFK, fac_empresa, Convert.ToInt64(ID_centroCosto ));
            else
                return dFactura.rep_FacturasXCliente(ID_clienteFK, fac_empresa, 0);
        }

        public DataTable repValorXGrupoConcepto(Int64 ID_facturaFK, Int64  ID_padre)
        {
            return dFactura.rep_ValorXGrupoConcepto(ID_facturaFK, ID_padre);
        }

        public DataTable repValorXGrupoXCliente(Int64 ID_clienteFK, Int64 ID_padre, Int64 ID_empresaFK, string ID_centroCosto)
        {
            if( ID_centroCosto!=null && ID_centroCosto!="")
                return dFactura.rep_ValorXGrupoXCliente(ID_clienteFK, ID_padre, ID_empresaFK, Convert.ToInt64(ID_centroCosto ));
            else
                return dFactura.rep_ValorXGrupoXCliente(ID_clienteFK, ID_padre, ID_empresaFK, 0);
        }

        public DataTable repValorXGrupoXClientePagado(Int64 ID_clienteFK, Int64 ID_padre, Int64 ID_empresaFK, string ID_centroCosto)
        {
            if (ID_centroCosto != null && ID_centroCosto != "")
                return dFactura.rep_ValorXGrupoXClientePagado(ID_clienteFK, ID_padre, ID_empresaFK, Convert.ToInt64(ID_centroCosto));
            else
                return dFactura.rep_ValorXGrupoXClientePagado(ID_clienteFK, ID_padre, ID_empresaFK, 0);
        }

        public DataTable repAbonosProvisionalesXCliente(string ID_clienteFK, string ID_empresaFK)
        {
            if (ID_clienteFK != null && ID_clienteFK != "")
                return dFactura.rep_AbonosProvisionalesXCliente(Convert.ToInt64(ID_clienteFK), Convert.ToInt64(ID_empresaFK));
            else
                return dFactura.rep_AbonosProvisionalesXCliente(0, Convert.ToInt64(ID_empresaFK));
        }

        public DataTable rep_AnticiposXCliente(string ID_clienteFK)
        {
            return dFactura.rep_AnticiposXCliente(ID_clienteFK);
        }

        public DataTable repFacturasPagadasXCliente(Int64 ID_clienteFK, string fac_empresa, string ID_centroCosto)
        {
            if (ID_centroCosto != null && ID_centroCosto != "")
                return dFactura.rep_FacturasPagadasXCliente(ID_clienteFK, fac_empresa, Convert.ToInt64(ID_centroCosto));
            else
                return dFactura.rep_FacturasPagadasXCliente(ID_clienteFK, fac_empresa, 0);
        }

        public DataTable rep_ValorXGrupoConceptoPagado(Int64 ID_facturaFK, Int64 ID_padre, Int64 ID_registroPagoFK)
        {
            return dFactura.rep_ValorXGrupoConceptoPagado(ID_facturaFK, ID_padre, ID_registroPagoFK);
        }

        public DataTable rep_MovimientosXfecha(string fechaInicio, string fechaFin, string tipoMovimiento, string empresa, string cliente)
        {
            return dFactura.rep_MovimientosXfecha(fechaInicio, fechaFin, tipoMovimiento, empresa, cliente);
        }

        public DataTable rep_CuentasXPagarXEmpresa(Int64 ID_empresaFK,Int64 ID_proveedorFK)
        {
            return dFactura.rep_CuentasXPagarXEmpresa(ID_empresaFK, ID_proveedorFK);
        }
        #endregion
    }
}
