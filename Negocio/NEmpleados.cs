using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Negocio
{
    public class NEmpleados
    {
        Datos.DEmpleados dEmpleado = new Datos.DEmpleados();

        #region Ordenes de Contratacion

        public DataTable OrdenContratacionInsertar(string ID_empresaTemporalFK, string ID_empresaUsuariaFK, string ID_centroCostoFK, string ord_tipoDocumento
                                                    , string ord_documento, string ord_apellido1, string ord_apellido2, string ord_nombre1, string ord_nombre2
                                                    , string ID_cargoFK, string ord_cargoHomologado, string ord_salario, string ord_ingresosNoSalariales
                                                    , string ord_fechaIngreso, string ord_examenesComentario, string ord_observaciones, string ID_usuarioRegistraFK
                                                    , string ord_CobroExamenes, string ord_tipoContrato, string ord_jornada, string ord_nivelRiesgo, string ID_departamentoFuncionesFK
                                                    , string ID_municipioFuncionesFK, string ord_fechaTentativaRetiro)
        {
            return dEmpleado.OrdenContratacionInsertar(ID_empresaTemporalFK, ID_empresaUsuariaFK, ID_centroCostoFK, ord_tipoDocumento
                                                    , ord_documento, ord_apellido1, ord_apellido2, ord_nombre1, ord_nombre2
                                                    , ID_cargoFK, ord_cargoHomologado, ord_salario, ord_ingresosNoSalariales, ord_fechaIngreso, ord_examenesComentario
                                                    , ord_observaciones, ID_usuarioRegistraFK, ord_CobroExamenes, ord_tipoContrato, ord_jornada, ord_nivelRiesgo
                                                    , ID_departamentoFuncionesFK, ID_municipioFuncionesFK, ord_fechaTentativaRetiro);
        }

        public DataTable OrdenContratacionActualizaEstado(string ID_ordenContratacionFK, string ID_estadoNuevoFK, string ID_usuarioRegistraFK, string ord_observacion)
        {
            return dEmpleado.OrdenContratacionActualizaEstado(ID_ordenContratacionFK, ID_estadoNuevoFK, ID_usuarioRegistraFK, ord_observacion);
        }

        public DataTable OrdenContratacionConsultarXEstado(int? estado, int? ID_empresaUsuariaFK)
        {
            return dEmpleado.OrdenContratacionConsultaXEstado(estado, ID_empresaUsuariaFK);
        }

        public DataTable OrdenContratacionConsultaXID(int ID_orden)
        {
            return dEmpleado.OrdenContratacionConsultaXID(ID_orden);
        }

        public DataTable OrdenContratacionConsultaEstadosXPerfil(string estados)
        {
            return dEmpleado.OrdenContratacionConsultaEstadosXPerfil(estados);
        }

        public void OrdenContratacionActualizaLaboratorio(string ID_ordenContratacion, string ID_laboratorioFK, string ID_ProgramaMedico, string ord_CobroExamenes)
        {
            dEmpleado.OrdenContratacionActualizaLaboratorio(ID_ordenContratacion, ID_laboratorioFK, ID_ProgramaMedico, ord_CobroExamenes);
        }

        public void OrdenContratacionLaboratorioDocumentoInsertar(string ID_ordenContratacion, string exa_archivo, string ID_usuarioRegistra)
        {
            dEmpleado.OrdenContratacionLaboratorioDocumentoInsertar(ID_ordenContratacion, exa_archivo, ID_usuarioRegistra);
        }

        #endregion

        #region Empleado
        public DataTable Insertar(string afi_codigo, string afi_tipoDocumento, string afi_documento, string afi_fechaExpedicion, string afi_apellido1, string afi_apellido2, string afi_nombre1, string afi_nombre2
                                , string ID_naciDepartamentoFK, string ID_naciMunicipioFK, string afi_fechaNacimiento, string ID_domicilioDepartamentoFK, string ID_domicilioMunicipioFK, string afi_direccion, string afi_barrio, string afi_telefonoFijo
                                , string afi_celular, string afi_sexo, string afi_estadoCivil, string afi_grupoSanguineo, string afi_pase, string afi_categoria, string afi_libretaMilitar, string ID_libretaDistritoFK, string afi_libretaClase
                                , string afi_contactoPersona, string afi_contactoTelefono, string afi_observaciones, string afi_nivelEducativo, string afi_tituloObtenido, string afi_entidadEducativa
                                , string ID_bancoFK, string afi_cuentaNumero, string afi_cuentaTipo, string afi_cuentaClase
                                , string ID_banco2FK, string afi_cuenta2Numero, string afi_cuenta2Tipo, string afi_cuenta2Clase
                                , string afi_mail, string afi_EPS, string afi_AFP, string afi_subTipo, string afi_CCF, string afi_Cesantias, string ID_usuarioRegistraFK
                                , string afi_naciPaisFK, string afi_expedicionPaisFK, string afi_expedicionDepartamentoFK, string afi_expedicionMunicipioFK)
        {
            return dEmpleado.Insertar(afi_codigo, afi_tipoDocumento, afi_documento, afi_fechaExpedicion, afi_apellido1, afi_apellido2, afi_nombre1, afi_nombre2
                                , ID_naciDepartamentoFK, ID_naciMunicipioFK, afi_fechaNacimiento, ID_domicilioDepartamentoFK, ID_domicilioMunicipioFK, afi_direccion, afi_barrio, afi_telefonoFijo
                                , afi_celular, afi_sexo, afi_estadoCivil, afi_grupoSanguineo, afi_pase, afi_categoria, afi_libretaMilitar, ID_libretaDistritoFK, afi_libretaClase
                                , afi_contactoPersona, afi_contactoTelefono, afi_observaciones, afi_nivelEducativo, afi_tituloObtenido, afi_entidadEducativa
                                , ID_bancoFK, afi_cuentaNumero, afi_cuentaTipo, afi_cuentaClase
                                , ID_banco2FK, afi_cuenta2Numero, afi_cuenta2Tipo, afi_cuenta2Clase
                                , afi_mail, afi_EPS, afi_AFP, afi_subTipo, afi_CCF, afi_Cesantias, ID_usuarioRegistraFK
                                , afi_naciPaisFK, afi_expedicionPaisFK, afi_expedicionDepartamentoFK, afi_expedicionMunicipioFK);
        }

        public DataTable Actualizar(string ID_afiliado, string afi_codigo, string afi_tipoDocumento, string afi_documento, string afi_fechaExpedicion, string afi_apellido1, string afi_apellido2, string afi_nombre1, string afi_nombre2
                                , string ID_naciDepartamentoFK, string ID_naciMunicipioFK, string afi_fechaNacimiento, string ID_domicilioDepartamentoFK, string ID_domicilioMunicipioFK, string afi_direccion, string afi_barrio, string afi_telefonoFijo
                                , string afi_celular, string afi_sexo, string afi_estadoCivil, string afi_grupoSanguineo, string afi_pase, string afi_categoria, string afi_libretaMilitar, string ID_libretaDistritoFK, string afi_libretaClase
                                , string afi_contactoPersona, string afi_contactoTelefono, string afi_observaciones, string afi_nivelEducativo, string afi_tituloObtenido, string afi_entidadEducativa
                                , string ID_bancoFK, string afi_cuentaNumero, string afi_cuentaTipo, string afi_cuentaClase
                                , string ID_banco2FK, string afi_cuenta2Numero, string afi_cuenta2Tipo, string afi_cuenta2Clase
                                , string afi_mail, string afi_EPS, string afi_AFP, string afi_subTipo, string afi_CCF, string afi_Cesantias, string ID_usuarioActualizaFK
                                , string afi_naciPaisFK, string afi_expedicionPaisFK, string afi_expedicionDepartamentoFK, string afi_expedicionMunicipioFK)
        {
            return dEmpleado.Actualizar(ID_afiliado, afi_codigo, afi_tipoDocumento, afi_documento, afi_fechaExpedicion, afi_apellido1, afi_apellido2, afi_nombre1, afi_nombre2
                                , ID_naciDepartamentoFK, ID_naciMunicipioFK, afi_fechaNacimiento, ID_domicilioDepartamentoFK, ID_domicilioMunicipioFK, afi_direccion, afi_barrio, afi_telefonoFijo
                                , afi_celular, afi_sexo, afi_estadoCivil, afi_grupoSanguineo, afi_pase, afi_categoria, afi_libretaMilitar, ID_libretaDistritoFK, afi_libretaClase
                                , afi_contactoPersona, afi_contactoTelefono, afi_observaciones, afi_nivelEducativo, afi_tituloObtenido, afi_entidadEducativa
                                , ID_bancoFK, afi_cuentaNumero, afi_cuentaTipo, afi_cuentaClase
                                , ID_banco2FK, afi_cuenta2Numero, afi_cuenta2Tipo, afi_cuenta2Clase
                                , afi_mail, afi_EPS, afi_AFP, afi_subTipo, afi_CCF, afi_Cesantias, ID_usuarioActualizaFK, afi_naciPaisFK, afi_expedicionPaisFK, afi_expedicionDepartamentoFK, afi_expedicionMunicipioFK);
        }

        public DataTable ConsultaXDocumento(string afi_documento)
        {
            return dEmpleado.ConsultaXDocumento(afi_documento);
        }

        public DataTable ConsultaXDocumentoSimple(string afi_documento)
        {
            return dEmpleado.ConsultaXDocumentoSimple(afi_documento);
        }

        public DataTable ConsultaXdocumentoAcumMasivo(string afi_documento)
        {
            return dEmpleado.ConsultaXdocumentoAcumMasivo(afi_documento);
        }

        public DataTable ConsultaXNombre(string nombre, string per_nivel, string ID_empresaUsuariaFK)
        {
            return dEmpleado.ConsultaXNombre(nombre, per_nivel, ID_empresaUsuariaFK);
        }

        #endregion

        #region Historico
        public void HistoricoInsertaXAfiliado(string ID_afiliadoFK, string his_tipoCambio, string his_campo, string his_valorAnterior, string his_valorNuevo, string ID_usuarioRegistra)
        {
            dEmpleado.HistoricoInsertaXAfiliado( ID_afiliadoFK , his_tipoCambio, his_campo, his_valorAnterior, his_valorNuevo, int.Parse(ID_usuarioRegistra));
        }
        public DataTable HistoricoConsultarXEmpleado(string ID_afiliadoFK)
        {
            return dEmpleado.HistoricoConsultarXEmpleado(ID_afiliadoFK);
        }
        #endregion

        #region Contratos

        public DataTable ContratoInsertar(string ID_afiliadoFK, string ID_laboratorioFK, string con_fechaExamen, string con_examenNumero, string con_apto, string con_compromiso, string con_compromisoDias, string con_restriccion, string ID_empresaTemporalFK, string ID_empresaUsuariaFK, string ID_centroCostoFK, string ID_sedeFK, int ID_ordenContratacionFK, string con_cargoHomologado,
                                          string con_nivelRiesgo, string con_fechaIngreso, string con_tipoContrato, string con_subsidioTransporte, string con_sabadoLaboral, string con_jornada, string ID_cargoFK, int con_salario, int con_ingresosNoSalariales, string ID_usuarioRegistraFK, string con_pfp,string ID_departamentoFuncionesFK, string ID_municipioFuncionesFK, string con_fechaTentativaRetiro)
        {
            DataTable tabla = dEmpleado.ContratoInsertar(ID_afiliadoFK, ID_laboratorioFK, con_fechaExamen, con_examenNumero, con_apto, con_compromiso, con_compromisoDias, con_restriccion, ID_empresaTemporalFK, ID_empresaUsuariaFK, ID_centroCostoFK,ID_sedeFK, ID_ordenContratacionFK,con_cargoHomologado,
                                          con_nivelRiesgo, con_fechaIngreso, con_tipoContrato, con_subsidioTransporte, con_sabadoLaboral, con_jornada, ID_cargoFK, con_salario,con_ingresosNoSalariales, ID_usuarioRegistraFK, con_pfp, ID_departamentoFuncionesFK, ID_municipioFuncionesFK, con_fechaTentativaRetiro);
            //cuando se inserta un contrato, de una vez guardo el historico
            if((tabla!=null) && (tabla.Rows.Count>0))
                dEmpleado.ContratoInsertarHistorico(tabla.Rows[0]["ID_contrato"].ToString(), ID_centroCostoFK, ID_sedeFK, ID_cargoFK, con_cargoHomologado, con_nivelRiesgo, con_fechaIngreso, 2, con_salario, con_ingresosNoSalariales, con_subsidioTransporte, 0, ID_usuarioRegistraFK, con_jornada, ID_departamentoFuncionesFK, ID_municipioFuncionesFK, con_fechaTentativaRetiro);
            
            return tabla;
        }

        public DataTable ContratoInsertarHistorico(string ID_contratoFK, string ID_centroCostoFK, string ID_sedeFK, string ID_cargoFK, string con_cargoHomologado,
                                          string conHis_nivelRiesgo, string conHis_fechaInicio, int conHis_estado, int conHis_salario, int conHis_ingresosNoSalariales, 
                                          string con_subsidioTransporte, int con_anular, string ID_usuarioRegistraFK, string con_jornada, 
                                          string ID_departamentoFuncionesFK, string ID_municipioFuncionesFK, string conHis_FechaTentativa)
        {
            return dEmpleado.ContratoInsertarHistorico(ID_contratoFK, ID_centroCostoFK, ID_sedeFK, ID_cargoFK, con_cargoHomologado, conHis_nivelRiesgo, conHis_fechaInicio, conHis_estado, conHis_salario, conHis_ingresosNoSalariales, con_subsidioTransporte, con_anular, ID_usuarioRegistraFK, con_jornada, ID_departamentoFuncionesFK, ID_municipioFuncionesFK, conHis_FechaTentativa);
        }
        
        public DataTable ContratosConsultarXEmpleado(string ID_afiliadoFK)
        {
            return dEmpleado.ContratosConsultarXEmpleado(ID_afiliadoFK);
        }

        public DataTable ContratosConsultaActivosXEmpleado(string ID_afiliadoFK)
        {
            return dEmpleado.ContratosConsultaActivosXEmpleado(ID_afiliadoFK);
        }

        public DataTable ContratosConsultaActivosParaPrestamoXEmpleado(string ID_afiliadoFK)
        {
            return dEmpleado.ContratosConsultaActivosParaPrestamoXEmpleado(ID_afiliadoFK);
        }

        public DataTable ContratosConsultarXID(string ID_contrato)
        {
            return dEmpleado.ContratosConsultarXID(ID_contrato);
        }

        public DataTable ContratosCertPromedioHorasExtra(string ID_contratoFK)
        {
            return dEmpleado.ContratosCertPromedioHorasExtra(ID_contratoFK);
        }

        public DataTable ContratosConsultaSalarioMasivoXEmpleado(int ID_empresaTemporalFK, int ID_empresaUsuariaFK, int ID_centroCostoFK, string afi_tipoDocumento, string afi_documento)
        {
            return dEmpleado.ContratosConsultaSalarioMasivoXEmpleado(ID_empresaTemporalFK, ID_empresaUsuariaFK, ID_centroCostoFK, afi_tipoDocumento, afi_documento);
        }

        #endregion

        #region Documentos

        public void DocumentoInsertar(string ID_afiliadoFK, string ID_contratoFK,  string doc_nombre, string ID_afiDocumentosTipo, int ID_usuarioRegistra)
        {
            dEmpleado.DocumentoInsertar(ID_afiliadoFK,ID_contratoFK, doc_nombre, ID_afiDocumentosTipo, ID_usuarioRegistra);
        }

        public DataTable DocumentosConsultarXEmpleado(string ID_afiliadoFK, string ID_contratoFK)
        {
            return dEmpleado.DocumentosConsultarXEmpleado(ID_afiliadoFK, ID_contratoFK);
        }

        public void DocumentosEliminar(string ID_Documento)
        {
            dEmpleado.DocumentosEliminar(ID_Documento);
        }
        #endregion

        #region BENEFICIARIOS

        public DataTable BeneficiariosConsultarXEmpleado(string ID_afiliadoFK)
        {
            return dEmpleado.BeneficiariosConsultarXEmpleado(ID_afiliadoFK);
        }

        public void BeneficiariosInsertarXEmpleado(string ID_afiliadoFK, string ben_parentesco, string ben_tipoDocumento, string ben_documento, string ben_nombres, string ben_observacion, string ID_usuarioRegistra, string ben_fechaNacimiento)
        {
            dEmpleado.BeneficiariosInsertarXEmpleado(ID_afiliadoFK, ben_parentesco, ben_tipoDocumento, ben_documento, ben_nombres, ben_observacion, ID_usuarioRegistra, ben_fechaNacimiento);
        }
        #endregion

        #region INCLUSIONES

        public DataTable InclusionesConsultarXEmpleado(string ID_afiliadoFK)
        {
            return dEmpleado.InclusionesConsultarXEmpleado(ID_afiliadoFK);
        }

        /// <summary>
        /// Se divide en secciones para armar un solo procedure para el reporte
        /// </summary>
        /// <param name="ID_inclusion"></param>
        /// <param name="ID_beneficiario"></param>
        /// <param name="seccion">1 principal. 2 group de beneficiarios. 3 Documentos de un beneficiario</param>
        /// <returns></returns>
        public DataTable InclusionesConsultarXID(string ID_inclusion,string ID_beneficiario, string seccion)
        {
            return dEmpleado.InclusionesConsultarXID(ID_inclusion,ID_beneficiario,  seccion);
        }

        public DataTable InclusionesInsertar(string ID_afiliadoFK, string ID_contratoFK, string inc_servicios, string inc_EPS, string inc_CCF, string inc_cuantos, string inc_fechaRecibido, string inc_observacion, int ID_usuarioRegistra)
        {
            return dEmpleado.InclusionesInsertar(ID_afiliadoFK,ID_contratoFK, inc_servicios, inc_EPS, inc_CCF, inc_cuantos, inc_fechaRecibido, inc_observacion, ID_usuarioRegistra);
        }
        
        public void InclusionesDocumentoInsertar(string ID_inclusionFK,string ID_TipoDocumentoInclusionFK, string ID_beneficiarioFK, string doc_nombre, int ID_usuarioRegistra)
        {
            dEmpleado.InclusionesDocumentoInsertar(ID_inclusionFK,ID_TipoDocumentoInclusionFK, ID_beneficiarioFK, doc_nombre, ID_usuarioRegistra);
        }

        #endregion

        #region ENVIO DE CARNETS

        public DataTable EnvioCarnetsInsertar(int env_tipo, long ID_carta, string ID_afiliadoFK, string ID_contratoFK, int ID_usuarioRegistraFK)
            {
                return dEmpleado.EnvioCarnetsInsertar(env_tipo, ID_carta, ID_afiliadoFK, ID_contratoFK,ID_usuarioRegistraFK);
            }

            public DataTable EnvioCarnetsConsultarXID(string ID_carta, string seccion, string ID_empresaUsuariaFK)
            {
                return dEmpleado.EnvioCarnetsConsultarXID(ID_carta, seccion, ID_empresaUsuariaFK);
            }
            public DataTable EnvioCarnetsConsultar(int env_tipo,string env_estado)
            {
                return dEmpleado.EnvioCarnetsConsultar(env_tipo,env_estado);
            }

            public void EnvioCarnetsActualizar(string ID_envioCarnet, string env_estado,string env_soporte, string env_observacion, string ID_usuarioActualizaFK)
            {
                dEmpleado.EnvioCarnetsActualizar(ID_envioCarnet, env_estado, env_soporte, env_observacion, ID_usuarioActualizaFK);
            }

            public DataTable EnvioCarnetsConsultarXAfiliado(string ID_afiliado)
            {
                return dEmpleado.EnvioCarnetsConsultarXAfiliado(ID_afiliado);
            }
        #endregion

        #region ENTREGA DE FORMULARIOS

            public void EntregaFormulariosInsertar(string ID_afiliadoFK, string ID_contratoFK, string ID_asesorFK, int ID_usuarioRegistraFK)
            {
                dEmpleado.EntregaFormulariosInsertar(ID_afiliadoFK, ID_contratoFK, ID_asesorFK, ID_usuarioRegistraFK);
            }

            public DataTable EntregaFormulariosConsultaXAfiliado(string ID_afiliadoFK)
            {
                return dEmpleado.EntregaFormulariosConsultaXAfiliado(ID_afiliadoFK);
            }

            public void EntregaFormulariosActualizar(string ID_entrega, string ent_estado, string ent_observacion, int ID_usuarioActualizaFK)
            {
                dEmpleado.EntregaFormulariosActualizar(ID_entrega, ent_estado, ent_observacion, ID_usuarioActualizaFK);
            }
        
        #endregion

        #region PRESTAMOS

        public string prestamoInsertar(string ID_contratoFK, string ID_afliadoFK, string ID_tipoPrestamoFK, string pre_prestamista, double pre_valor, int pre_cuotas,
            string pre_fechaInicio, string pre_frecuenciaPago, string pre_frecuenciaDetalle, double pre_descontarPrima, double pre_descontarCesantias,
            double pre_valorCuota, double pre_valorCuotaAjustada, string pre_observaciones, string ID_usuarioRegistraFK, Boolean pre_liquidacion)
        {
            DataTable tbPrestamo = dEmpleado.prestamoInsertar(ID_contratoFK, ID_afliadoFK, ID_tipoPrestamoFK, pre_prestamista, pre_valor, pre_cuotas,
                        pre_fechaInicio, pre_frecuenciaPago, pre_frecuenciaDetalle, pre_descontarPrima, pre_descontarCesantias,
                        pre_valorCuota,pre_valorCuotaAjustada, pre_observaciones, ID_usuarioRegistraFK, pre_liquidacion);
            if (tbPrestamo.Rows[0]["ID_prestamo"].ToString() != "0")
            {
                #region CALCULAR CUOTA
                int cuotasRestan = 0;
                if (pre_descontarPrima > 0)
                    cuotasRestan++;

                if (pre_descontarCesantias > 0)
                    cuotasRestan++;

                pre_cuotas = pre_cuotas - cuotasRestan;

                double saldo = pre_valor;

                #endregion

                #region INSERTAR CUOTA
                DateTime fechaInicio = Convert.ToDateTime(pre_fechaInicio);
                string fechaPagoCuota = pre_fechaInicio;
                int i;
                for (i = 1; i <= pre_cuotas; i++)
                {
                    if (i != 1)
                    {
                        if (pre_frecuenciaPago == "Mensual")
                        {
                            if (pre_frecuenciaDetalle == "Primera Quincena")
                                fechaPagoCuota = string.Format("{0:yyyy-MM-01}", fechaInicio.AddMonths(i - 1));
                            else
                                fechaPagoCuota = string.Format("{0:yyyy-MM-16}", fechaInicio.AddMonths(i - 1));
                        }
                        else
                        { //frecuencia de pago quincenal
                            if (Convert.ToDateTime(fechaPagoCuota).Day == 1)
                                fechaPagoCuota = string.Format("{0:yyyy-MM-16}", Convert.ToDateTime(fechaPagoCuota));
                            else
                                fechaPagoCuota = string.Format("{0:yyyy-MM-01}", Convert.ToDateTime(fechaPagoCuota).AddMonths(1));
                        }
                    }


                    if (i != pre_cuotas)
                    {
                        dEmpleado.prestamoCuotaInsertar(tbPrestamo.Rows[0]["ID_prestamo"].ToString(), 1, i, pre_valorCuota, saldo, fechaPagoCuota, ID_usuarioRegistraFK);
                        saldo = saldo - pre_valorCuota;
                    }
                    else
                    {
                        dEmpleado.prestamoCuotaInsertar(tbPrestamo.Rows[0]["ID_prestamo"].ToString(), 1, i, pre_valorCuotaAjustada, saldo, fechaPagoCuota, ID_usuarioRegistraFK);
                        saldo = saldo - pre_valorCuotaAjustada;
                    }
                }
                #endregion

                #region PRIMA Y CESANTIAS
                //si tengo saldo, es por que me falta o prima o cesantias
                if (saldo > 0)
                {
                    if (pre_descontarPrima > 0)
                    {
                        dEmpleado.prestamoCuotaInsertar(tbPrestamo.Rows[0]["ID_prestamo"].ToString(), 2, i, pre_descontarPrima, saldo, fechaPagoCuota, ID_usuarioRegistraFK);
                        saldo = saldo - pre_descontarPrima;
                        i++;
                    }

                    if (pre_descontarCesantias > 0)
                    {
                        dEmpleado.prestamoCuotaInsertar(tbPrestamo.Rows[0]["ID_prestamo"].ToString(), 3, i, pre_descontarCesantias, saldo, fechaPagoCuota, ID_usuarioRegistraFK);
                        saldo = saldo - pre_descontarCesantias;
                        i++;
                    }
                }
                #endregion
            }
            
            return tbPrestamo.Rows[0]["mensaje"].ToString();
        }

            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="ID_afliadoFK"></param>
            /// <param name="ID_contratoFK"></param>
            /// <param name="cuo_fechaPago"></param>
            /// <param name="cuo_tipo">1 Cuota, 2 Prestamos, 3 Cesantias</param>
            /// <returns></returns>
            public DataTable prestamoConsultaCuotasParaAplicar(string ID_afliadoFK, string ID_contratoFK, string cuo_fechaPago, int cuo_tipo)
            {
                return dEmpleado.prestamoConsultaCuotasParaAplicar(ID_afliadoFK, ID_contratoFK, cuo_fechaPago, cuo_tipo);
            }

            public void prestamoActualizaCuota(string ID_cuota, string ID_usuarioRegistraPago, string ID_nominaPago)
            {
                dEmpleado.prestamoActualizaCuota(ID_cuota, ID_usuarioRegistraPago, ID_nominaPago);
            }

            public DataTable prestamoConsultaXEmpleado(string ID_afliadoFK)
            {
                return dEmpleado.prestamoConsultaXEmpleado(ID_afliadoFK);
            }

            public void prestamoSoporteInsertar(string ID_prestamoFK, string sop_nombreOriginal, string sop_nombreAlmacenado, string ID_usuarioRegistraFK)
            {
                dEmpleado.prestamoSoporteInsertar(ID_prestamoFK, sop_nombreOriginal, sop_nombreAlmacenado, ID_usuarioRegistraFK);
            }

            public DataTable prestamoSoporteConsultar(string ID_prestamoFK)
            {
                return dEmpleado.prestamoSoporteConsultar(ID_prestamoFK);
            }

            public void prestamoAnular(string ID_prestamoFK, string pre_anuladoObservacion, string ID_usuarioAnulaFK)
            {
                dEmpleado.prestamoAnular(ID_prestamoFK, pre_anuladoObservacion, ID_usuarioAnulaFK);
            }

            public void prestamoCondonar(string ID_prestamoFK, string pre_anuladoObservacion, string ID_usuarioAnulaFK)
            {
                dEmpleado.prestamoCondonar(ID_prestamoFK, pre_anuladoObservacion, ID_usuarioAnulaFK);
            }
        #endregion

        #region NOVEDADES FIJAS
        /// <summary>
        /// NOVEDADES FIJAS
        /// </summary>
        /// <param name="ID_tipoNovedadFK"></param>
        /// <param name="ID_contratoFK"></param>
        /// <param name="ID_afiliadoFK"></param>
        /// <param name="nof_valor"></param>
        /// <param name="nof_frecuenciaPago"></param>
        /// <param name="nof_frecuenciaDetalle"></param>
        /// <param name="nof_fechaInicio"></param>
        /// <param name="ID_usuarioRegistraFK"></param>
        /// <returns></returns>
        public DataTable nofInsertar(string ID_tipoNovedadFK, string ID_contratoFK, string ID_afiliadoFK, double nof_valor, string nof_frecuenciaPago, string nof_frecuenciaDetalle, string nof_fechaInicio, string ID_usuarioRegistraFK, string nof_observaciones)
            {
                return dEmpleado.nofInsertar(ID_tipoNovedadFK, ID_contratoFK, ID_afiliadoFK, nof_valor, nof_frecuenciaPago, nof_frecuenciaDetalle, nof_fechaInicio, ID_usuarioRegistraFK, nof_observaciones);
            }

            public DataTable nofConsultarXAfiliado(string ID_afiliadoFK)
            {
                return dEmpleado.nofConsultarXAfiliado(ID_afiliadoFK);
            }

            public DataTable nofConsultarActivasXAfiliado(string ID_afiliadoFK, string ID_contratoFK, string fechaInicio)
            {
                return dEmpleado.nofConsultarActivasXAfiliado(ID_afiliadoFK, ID_contratoFK, fechaInicio);
            }
            public DataTable nofInactivar(string ID_novedadFija, string ID_usuarioActualizaFK)
            {
                return dEmpleado.nofInactivar(ID_novedadFija, ID_usuarioActualizaFK);
            }
        #endregion

        #region NOMINAS

        public DataTable NomConsultarContratosXID(string ID_contrato)
        {
            return dEmpleado.NomConsultarContratosXID(ID_contrato);
        }

        public DataTable NomConsultaXIDContratoRetiroExtemporaneo(string ID_contratoFK)
        {
            return dEmpleado.NomConsultaXIDContratoRetiroExtemporaneo(ID_contratoFK);
        }
        #endregion

        #region DEDUCCIONES RTE FUENTE

        public DataTable DeducRteConsultar(string ID_afiliadoFK)
        {
            return dEmpleado.DeducRteConsultar(ID_afiliadoFK);
        }

        public DataTable DeducRteInsertar(string ID_afiliadoFK, string ID_tipoDeduccionFK, string ded_anno, string ded_valor, string ded_soporte, string ID_usuarioRegistraFK)
        {
            return dEmpleado.DeducRteInsertar(ID_afiliadoFK, ID_tipoDeduccionFK, ded_anno, ded_valor, ded_soporte, ID_usuarioRegistraFK);
        }

        public void DeducRteAnular(string ID_deduccion, string ID_usuarioActualizaFK)
        {
            dEmpleado.DeducRteAnular(ID_deduccion, ID_usuarioActualizaFK);
        }
        #endregion
    }
}
