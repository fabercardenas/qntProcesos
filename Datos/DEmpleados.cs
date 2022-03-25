using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;

namespace Datos
{
    public class DEmpleados : DSQL
    {
        #region EMPLEADOS
 public DataTable Insertar(string afi_codigo,string afi_tipoDocumento,string afi_documento,string afi_fechaExpedicion,string afi_apellido1,string afi_apellido2,string afi_nombre1,string afi_nombre2
                                ,string ID_naciDepartamentoFK,string ID_naciMunicipioFK,string afi_fechaNacimiento,string ID_domicilioDepartamentoFK,string ID_domicilioMunicipioFK,string afi_direccion,string afi_barrio,string afi_telefonoFijo
                                ,string afi_celular,string afi_sexo,string afi_estadoCivil,string afi_grupoSanguineo,string afi_pase,string afi_categoria,string afi_libretaMilitar,string ID_libretaDistritoFK,string afi_libretaClase
                                ,string afi_contactoPersona,string afi_contactoTelefono,string afi_observaciones,string afi_nivelEducativo,string afi_tituloObtenido,string afi_entidadEducativa
                                , string ID_bancoFK, string afi_cuentaNumero, string afi_cuentaTipo, string afi_cuentaClase
                                , string ID_banco2FK, string afi_cuenta2Numero, string afi_cuenta2Tipo, string afi_cuenta2Clase
                                ,string afi_mail,string afi_EPS,string afi_AFP,string afi_subTipo,string afi_CCF,string afi_Cesantias, string ID_usuarioRegistraFK
                                , string afi_naciPaisFK, string afi_expedicionPaisFK ,string afi_expedicionDepartamentoFK , string afi_expedicionMunicipioFK )
        {
            Hashtable param = new Hashtable(1);
            param.Add("@afi_codigo", afi_codigo);
            param.Add("@afi_tipoDocumento", afi_tipoDocumento);
            param.Add("@afi_documento", afi_documento);
            param.Add("@afi_fechaExpedicion", afi_fechaExpedicion);
            param.Add("@afi_apellido1", afi_apellido1);
            param.Add("@afi_apellido2", afi_apellido2);
            param.Add("@afi_nombre1", afi_nombre1);
            param.Add("@afi_nombre2", afi_nombre2);
            param.Add("@ID_naciDepartamentoFK", ID_naciDepartamentoFK);
            param.Add("@ID_naciMunicipioFK", ID_naciMunicipioFK);
            param.Add("@afi_fechaNacimiento", afi_fechaNacimiento);
            param.Add("@ID_domicilioDepartamentoFK", ID_domicilioDepartamentoFK);
            param.Add("@ID_domicilioMunicipioFK", ID_domicilioMunicipioFK);
            param.Add("@afi_direccion", afi_direccion);
            param.Add("@afi_barrio", afi_barrio);
            param.Add("@afi_telefonoFijo", afi_telefonoFijo);
            param.Add("@afi_celular", afi_celular);
            param.Add("@afi_sexo", afi_sexo);
            param.Add("@afi_estadoCivil", afi_estadoCivil);
            param.Add("@afi_grupoSanguineo", afi_grupoSanguineo);
            param.Add("@afi_pase", afi_pase);
            param.Add("@afi_categoria", afi_categoria);
            param.Add("@afi_libretaMilitar", afi_libretaMilitar);
            param.Add("@ID_libretaDistritoFK", ID_libretaDistritoFK);
            param.Add("@afi_libretaClase", afi_libretaClase);
            param.Add("@afi_contactoPersona", afi_contactoPersona);
            param.Add("@afi_contactoTelefono", afi_contactoTelefono);
            param.Add("@afi_observaciones", afi_observaciones);
            param.Add("@afi_nivelEducativo", afi_nivelEducativo);
            param.Add("@afi_tituloObtenido", afi_tituloObtenido);
            param.Add("@afi_entidadEducativa", afi_entidadEducativa);
            param.Add("@ID_bancoFK", ID_bancoFK);
            param.Add("@afi_cuentaNumero", afi_cuentaNumero);
            param.Add("@afi_cuentaTipo", afi_cuentaTipo);
            param.Add("@afi_cuentaClase", afi_cuentaClase);
            param.Add("@afi_mail", afi_mail);
            param.Add("@ID_banco2FK", ID_banco2FK);
            param.Add("@afi_cuenta2Numero", afi_cuenta2Numero);
            param.Add("@afi_cuenta2Tipo", afi_cuenta2Tipo);
            param.Add("@afi_cuenta2Clase", afi_cuenta2Clase);
            param.Add("@afi_EPS", afi_EPS);
            param.Add("@afi_AFP", afi_AFP);
            param.Add("@afi_subTipo", afi_subTipo);
            param.Add("@afi_CCF", afi_CCF);
            param.Add("@afi_Cesantias", afi_Cesantias);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
            param.Add("@afi_naciPaisFK", afi_naciPaisFK);
            param.Add("@afi_expedicionPaisFK", afi_expedicionPaisFK);
            param.Add("@afi_expedicionDepartamentoFK", afi_expedicionDepartamentoFK);
            param.Add("@afi_expedicionMunicipioFK", afi_expedicionMunicipioFK);

            return procedureTable("afi_Insertar", true, param);
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
            Hashtable param = new Hashtable(1);

            param.Add("@ID_afiliado", ID_afiliado);
            param.Add("@afi_codigo", afi_codigo);
            param.Add("@afi_tipoDocumento", afi_tipoDocumento);
            param.Add("@afi_documento", afi_documento);
            param.Add("@afi_fechaExpedicion", afi_fechaExpedicion);
            param.Add("@afi_apellido1", afi_apellido1);
            param.Add("@afi_apellido2", afi_apellido2);
            param.Add("@afi_nombre1", afi_nombre1);
            param.Add("@afi_nombre2", afi_nombre2);
            param.Add("@ID_naciDepartamentoFK", ID_naciDepartamentoFK);
            param.Add("@ID_naciMunicipioFK", ID_naciMunicipioFK);
            param.Add("@afi_fechaNacimiento", afi_fechaNacimiento);
            param.Add("@ID_domicilioDepartamentoFK", ID_domicilioDepartamentoFK);
            param.Add("@ID_domicilioMunicipioFK", ID_domicilioMunicipioFK);
            param.Add("@afi_direccion", afi_direccion);
            param.Add("@afi_barrio", afi_barrio);
            param.Add("@afi_telefonoFijo", afi_telefonoFijo);
            param.Add("@afi_celular", afi_celular);
            param.Add("@afi_sexo", afi_sexo);
            param.Add("@afi_estadoCivil", afi_estadoCivil);
            param.Add("@afi_grupoSanguineo", afi_grupoSanguineo);
            param.Add("@afi_pase", afi_pase);
            param.Add("@afi_categoria", afi_categoria);
            param.Add("@afi_libretaMilitar", afi_libretaMilitar);
            param.Add("@ID_libretaDistritoFK", ID_libretaDistritoFK);
            param.Add("@afi_libretaClase", afi_libretaClase);
            param.Add("@afi_contactoPersona", afi_contactoPersona);
            param.Add("@afi_contactoTelefono", afi_contactoTelefono);
            param.Add("@afi_observaciones", afi_observaciones);
            param.Add("@afi_nivelEducativo", afi_nivelEducativo);
            param.Add("@afi_tituloObtenido", afi_tituloObtenido);
            param.Add("@afi_entidadEducativa", afi_entidadEducativa);
            param.Add("@ID_bancoFK", ID_bancoFK);
            param.Add("@afi_cuentaNumero", afi_cuentaNumero);
            param.Add("@afi_cuentaTipo", afi_cuentaTipo);
            param.Add("@afi_cuentaClase", afi_cuentaClase);
            param.Add("@ID_banco2FK", ID_banco2FK);
            param.Add("@afi_cuenta2Numero", afi_cuenta2Numero);
            param.Add("@afi_cuenta2Tipo", afi_cuenta2Tipo);
            param.Add("@afi_cuenta2Clase", afi_cuenta2Clase);
            param.Add("@afi_mail", afi_mail);
            param.Add("@afi_EPS", afi_EPS);
            param.Add("@afi_AFP", afi_AFP);
            param.Add("@afi_subTipo", afi_subTipo);
            param.Add("@afi_CCF", afi_CCF);
            param.Add("@afi_Cesantias", afi_Cesantias);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);
            param.Add("@afi_naciPaisFK", afi_naciPaisFK);
            param.Add("@afi_expedicionPaisFK", afi_expedicionPaisFK);
            param.Add("@afi_expedicionDepartamentoFK", afi_expedicionDepartamentoFK);
            param.Add("@afi_expedicionMunicipioFK", afi_expedicionMunicipioFK);

            return procedureTable("afi_Actualizar", true, param);
        }
        
        public DataTable ConsultaXDocumento(string afi_documento)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@afi_documento", afi_documento);

            return procedureTable("afi_ConsultaXDocumento", true, param);
        }

        public DataTable ConsultaXDocumentoSimple(string afi_documento)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@afi_documento", afi_documento);

            return procedureTable("afi_ConsultaXdocumentoSimple", true, param);
        }

        public DataTable ConsultaXdocumentoAcumMasivo(string afi_documento)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@afi_documento", afi_documento);

            return procedureTable("afi_ConsultaXdocumentoAcumMasivo", true, param);
        }

        public DataTable ConsultaXNombre(string nombre, string per_nivel, string ID_empresaUsuariaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@nombre", nombre);
            param.Add("@per_nivel", per_nivel);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);

            return procedureTable("afi_ConsultaXnombre", true, param);
        }

        #endregion

        #region Ordenes de Contratacion

        public DataTable OrdenContratacionInsertar(string ID_empresaTemporalFK,string ID_empresaUsuariaFK,string ID_centroCostoFK,string ord_tipoDocumento
                                                    ,string ord_documento,string ord_apellido1,string ord_apellido2,string ord_nombre1,string ord_nombre2
                                                    , string ID_cargoFK,string ord_cargoHomologado, string ord_salario,string ord_ingresosNoSalariales
                                                    , string ord_fechaIngreso,string ord_examenesComentario, string ord_observaciones, string ID_usuarioRegistraFK
                                                    , string ord_CobroExamenes, string ord_tipoContrato, string ord_jornada, string ord_nivelRiesgo
                                                    , string ID_departamentoFuncionesFK, string ID_municipioFuncionesFK, string ord_fechaTentativaRetiro)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@ID_centroCostoFK", ID_centroCostoFK);
            param.Add("@ord_tipoDocumento", ord_tipoDocumento);
            param.Add("@ord_documento", ord_documento);
            param.Add("@ord_apellido1", ord_apellido1);
            param.Add("@ord_apellido2", ord_apellido2);
            param.Add("@ord_nombre1", ord_nombre1);
            param.Add("@ord_nombre2", ord_nombre2);
            param.Add("@ID_cargoFK", ID_cargoFK);
            param.Add("@ord_cargoHomologado", ord_cargoHomologado);
            param.Add("@ord_salario", ord_salario);
            param.Add("@ord_ingresosNoSalariales", ord_ingresosNoSalariales);
            param.Add("@ord_fechaIngreso", ord_fechaIngreso);
            param.Add("@ord_examenesComentario", ord_examenesComentario);
            param.Add("@ord_observaciones", ord_observaciones);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
            param.Add("@ord_CobroExamenes", ord_CobroExamenes); 
            param.Add("@ord_tipoContrato", ord_tipoContrato);
            param.Add("@ord_jornada", ord_jornada);
            param.Add("@ord_nivelRiesgo", ord_nivelRiesgo);
            param.Add("@ID_departamentoFuncionesFK", ID_departamentoFuncionesFK);
            param.Add("@ID_municipioFuncionesFK", ID_municipioFuncionesFK);
            param.Add("@ord_fechaTentativaRetiro", ord_fechaTentativaRetiro);

            return procedureTable("ordCont_Insertar", true, param);
        }

        public DataTable OrdenContratacionActualizaEstado(string ID_ordenContratacionFK, string ID_estadoNuevoFK, string ID_usuarioRegistraFK, string ord_observacion)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_ordenContratacionFK", ID_ordenContratacionFK);
            param.Add("@ID_estadoNuevoFK", ID_estadoNuevoFK);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
            param.Add("@ord_observacion", ord_observacion);

            return procedureTable("ordCont_ActualizaEstado", true, param);
        }

        public DataTable OrdenContratacionConsultaXEstado(int? estado, int? ID_empresaUsuariaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ord_estado", estado);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            return procedureTable("ordCont_ConsultarXEstado", true, param);
        }

        public DataTable OrdenContratacionConsultaXID(int ID_orden)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_orden", ID_orden);
            return procedureTable("ordCont_ConsultarXID", true, param);
        }

        public DataTable OrdenContratacionConsultaEstadosXPerfil(string estados)
        {
            return queryTable("SELECT * FROM app_Referencias WHERE ref_modulo='OrdenContratacion' AND ref_valor IN (" + estados + ") ORDER BY ref_orden");
        }

        public void OrdenContratacionActualizaLaboratorio(string ID_ordenContratacion, string ID_laboratorioFK, string ID_ProgramaMedico, string ord_CobroExamenes)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_ordenContratacion", ID_ordenContratacion);
            param.Add("@ID_laboratorioFK", ID_laboratorioFK);
            param.Add("@ID_ProgramaMedico", ID_ProgramaMedico);
            param.Add("@ord_CobroExamenes", ord_CobroExamenes);

            procedureTable("ordCont_ActualizaLaboratorio", true, param);
        }

        public void OrdenContratacionLaboratorioDocumentoInsertar(string ID_ordenContratacionFK, string exa_archivo, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_ordenContratacionFK", ID_ordenContratacionFK);
            param.Add("@exa_archivo", exa_archivo);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            procedureTable("ordCont_ExamenesLaboratorioInsertar", true, param);
        }

        #endregion

        #region Historico

        public void HistoricoInsertaXAfiliado(string ID_afiliadoFK, string his_tipoCambio, string his_campo, string his_valorAnterior, string his_valorNuevo, int ID_usuarioRegistra)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@his_tipoCambio", his_tipoCambio);
            param.Add("@his_campo", his_campo);
            param.Add("@his_valorAnterior", his_valorAnterior);
            param.Add("@his_valorNuevo", his_valorNuevo);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);

            procedureTable("afi_HistoricoInsertaXAfiliado", true, param);
        }

        public DataTable HistoricoConsultarXEmpleado(string ID_afiliadoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);

            return procedureTable("afi_HistoricoConsultaXAfiliado", true, param);
        }

        #endregion

        #region Contratos

        public DataTable ContratoInsertar(string ID_afiliadoFK, string ID_laboratorioFK, string con_fechaExamen, string con_examenNumero, string con_apto, string con_compromiso, string con_compromisoDias, string con_restriccion, string ID_empresaTemporalFK, string ID_empresaUsuariaFK, string ID_centroCostoFK, string ID_sedeFK, int ID_ordenContratacionFK, string con_cargoHomologado,
                                          string con_nivelRiesgo, string con_fechaIngreso, string con_tipoContrato, string con_subsidioTransporte, string con_sabadoLaboral, string con_jornada, string ID_cargoFK, int con_salario, int con_ingresosNoSalariales, string ID_usuarioRegistraFK, string con_pfp, string ID_departamentoFuncionesFK, string ID_municipioFuncionesFK, string con_fechaTentativaRetiro)
        {
            Hashtable param = new Hashtable(1);

            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_laboratorioFK", ID_laboratorioFK);
            param.Add("@con_fechaExamen", con_fechaExamen);
            param.Add("@con_examenNumero", con_examenNumero);
            param.Add("@con_apto", con_apto);
            param.Add("@con_compromiso", con_compromiso);
            param.Add("@con_compromisoDias", con_compromisoDias);
            param.Add("@con_restriccion", con_restriccion);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@ID_centroCostoFK", ID_centroCostoFK);
            param.Add("@ID_sedeFK", ID_sedeFK);
            param.Add("@ID_ordenContratacionFK", ID_ordenContratacionFK);
            param.Add("@con_cargoHomologado", con_cargoHomologado);
            param.Add("@con_nivelRiesgo", con_nivelRiesgo);
            param.Add("@con_fechaIngreso", con_fechaIngreso);
            param.Add("@con_tipoContrato", con_tipoContrato);
            param.Add("@con_subsidioTransporte", con_subsidioTransporte);
            param.Add("@con_sabadoLaboral", con_sabadoLaboral);
            param.Add("@con_jornada", con_jornada);
            param.Add("@ID_cargoFK", ID_cargoFK);
            param.Add("@con_salario", con_salario);
            param.Add("@con_ingresosNoSalariales", con_ingresosNoSalariales);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
            param.Add("@con_pfp", con_pfp);
            param.Add("@ID_departamentoFuncionesFK", ID_departamentoFuncionesFK);
            param.Add("@ID_municipioFuncionesFK", ID_municipioFuncionesFK);
            param.Add("@con_fechaTentativaRetiro", con_fechaTentativaRetiro);

            return procedureTable("cont_Insertar", true, param);
        }

        public DataTable ContratoInsertarHistorico(string ID_contratoFK, string ID_centroCostoFK, string ID_sedeFK, string ID_cargoFK, string conHis_cargoHomologado,
                                            string conHis_nivelRiesgo, string conHis_fechaInicio, int conHis_estado, int conHis_salario, 
                                            int conHis_ingresosNoSalariales,string con_subsidioTransporte, int con_anular, string ID_usuarioRegistraFK, 
                                            String conHis_Jornada, string ID_departamentoFuncionesFK, string ID_municipioFuncionesFK, string conHis_FechaTentativa)
        {
            Hashtable param = new Hashtable(1);

            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@ID_centroCostoFK", ID_centroCostoFK);
            param.Add("@ID_sedeFK", ID_sedeFK);
            param.Add("@ID_cargoFK", ID_cargoFK);
            param.Add("@conHis_cargoHomologado", conHis_cargoHomologado);
            param.Add("@conHis_nivelRiesgo", conHis_nivelRiesgo);
            param.Add("@conHis_fechaInicio", conHis_fechaInicio);
            param.Add("@conHis_estado", conHis_estado);
            param.Add("@conHis_salario", conHis_salario);
            param.Add("@conHis_ingresosNoSalariales", conHis_ingresosNoSalariales);
            param.Add("@con_subsidioTransporte", con_subsidioTransporte);
            param.Add("@con_anular", con_anular);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
            param.Add("@conHis_Jornada", conHis_Jornada);
            param.Add("@ID_departamentoFuncionesFK", ID_departamentoFuncionesFK);
            param.Add("@ID_municipioFuncionesFK", ID_municipioFuncionesFK);
            param.Add("@conHis_FechaTentativa", conHis_FechaTentativa);

            return procedureTable("contHis_Insertar", true, param);
        }
        
        public DataTable ContratosConsultarXEmpleado(string ID_afiliadoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);

            return procedureTable("cont_ConsultaXEmpleado", true, param);
        }

        public DataTable ContratosConsultaActivosXEmpleado(string ID_afiliadoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);

            return procedureTable("cont_ConsultaActivosXEmpleado", true, param);
        }

        public DataTable ContratosConsultaActivosParaPrestamoXEmpleado(string ID_afiliadoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);

            return procedureTable("cont_ConsultaActivosParaPrestamoXEmpleado", true, param);
        }

        public DataTable ContratosConsultarXID(string ID_contrato)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contrato", ID_contrato);

            return procedureTable("cont_ConsultaXID", true, param);
        }

        public DataTable ContratosCertPromedioHorasExtra(string ID_contratoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK", ID_contratoFK);

            return procedureTable("cont_CertPromedioHorasExtra", true, param);
        }

        public DataTable ContratosConsultaSalarioMasivoXEmpleado(int ID_empresaTemporalFK, int ID_empresaUsuariaFK, int ID_centroCostoFK, string afi_tipoDocumento, string afi_documento)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaTemporalFK", ID_empresaTemporalFK);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);
            param.Add("@ID_centroCostoFK", ID_centroCostoFK);
            param.Add("@afi_tipoDocumento", afi_tipoDocumento);
            param.Add("@afi_documento", afi_documento);

            return procedureTable("cont_ConsultaSalarioMasivoXEmpleado", true, param);
        }
        #endregion

        #region Documentos

        public void DocumentoInsertar(string ID_afiliadoFK,string ID_contratoFK, string doc_nombre, string ID_afiDocumentosTipo, int ID_usuarioRegistra)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@doc_nombre", doc_nombre);
            param.Add("@ID_afiDocumentosTipo", ID_afiDocumentosTipo);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);

            procedureTable("afi_DocumentosInsertar", true, param);
        }

        public DataTable DocumentosConsultarXEmpleado(string ID_afiliadoFK, string ID_contratoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);

            return procedureTable("afi_DocumentosConsultaXAfiliado", true, param);
        }

        public void DocumentosEliminar(string ID_Documento)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_Documento", ID_Documento);

            procedureTable("afi_DocumentosEliminar", true, param);
        }
        #endregion

        #region BENEFICIARIOS

        public DataTable BeneficiariosConsultarXEmpleado(string ID_afiliadoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);

            return procedureTable("afi_ConsultaBeneficiariosXAfiliado", true, param);
        }

        public void BeneficiariosInsertarXEmpleado(string ID_afiliadoFK, string ben_parentesco, string ben_tipoDocumento, string ben_documento, string ben_nombres, string ben_observacion, string ID_usuarioRegistra, string ben_fechaNacimiento)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ben_parentesco", ben_parentesco);
            param.Add("@ben_tipoDocumento", ben_tipoDocumento);
            param.Add("@ben_documento", ben_documento);
            param.Add("@ben_nombres", ben_nombres);
            param.Add("@ben_observacion", ben_observacion);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);
            param.Add("@ben_fechaNacimiento", ben_fechaNacimiento);

            procedureTable("afi_InsertaBeneficiarioXAfiliado", true, param);
        }

        #endregion

        #region INCLUSIONES

        public DataTable InclusionesConsultarXEmpleado(string ID_afiliadoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);

            return procedureTable("afi_InclusionesConsultarXAfiliado", true, param);
        }

        public DataTable InclusionesConsultarXID(string ID_inclusion, string ID_beneficiario, string seccion)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_inclusion", ID_inclusion);
            param.Add("@ID_beneficiario", ID_beneficiario);
            param.Add("@seccion", seccion);

            return procedureTable("afi_InclusionesConsultaXID", true, param);
        }

        public DataTable InclusionesInsertar(string ID_afiliadoFK,string ID_contratoFK, string inc_servicios,string inc_EPS, string inc_CCF,string inc_cuantos, string inc_fechaRecibido, string inc_observacion,int ID_usuarioRegistra)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@inc_servicios", inc_servicios);
            param.Add("@inc_EPS", inc_EPS);
            param.Add("@inc_CCF", inc_CCF);
            param.Add("@inc_cuantos", inc_cuantos);
            param.Add("@inc_fechaRecibido", inc_fechaRecibido);
            param.Add("@inc_observacion", inc_observacion);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);

            return procedureTable("afi_InclusionesInsertar", true, param);
        }

        public void InclusionesDocumentoInsertar(string ID_inclusionFK,string ID_TipoDocumentoInclusionFK, string ID_beneficiarioFK, string doc_nombre, int ID_usuarioRegistra)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_inclusionFK", ID_inclusionFK);
            param.Add("@ID_TipoDocumentoInclusionFK", ID_TipoDocumentoInclusionFK);
            param.Add("@ID_beneficiarioFK", ID_beneficiarioFK);
            param.Add("@doc_nombre", doc_nombre);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);

            procedureTable("afi_InclusionDocumentoInsertar", true, param);
        }

        #endregion

        #region ENVIO DE CARNETS

        public DataTable EnvioCarnetsInsertar(int env_tipo, long ID_carta, string ID_afiliadoFK, string ID_contratoFK, int ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@env_tipo", env_tipo);
            param.Add("@ID_carta", ID_carta);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            return procedureTable("cor_EnvioCarnetsInsertar", true, param);
        }

        public DataTable EnvioCarnetsConsultarXID(string ID_carta, string seccion, string ID_empresaUsuariaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_carta", ID_carta);
            param.Add("@seccion", seccion);
            param.Add("@ID_empresaUsuariaFK", ID_empresaUsuariaFK);

            return procedureTable("cor_EnvioCarnetsConsultarXID", true, param);
        }

        public DataTable EnvioCarnetsConsultar(int env_tipo,string env_estado)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@env_tipo", env_tipo);
            param.Add("@env_estado", env_estado);

            return procedureTable("cor_EnvioCarnetsConsultar", true, param);
        }

        public void EnvioCarnetsActualizar(string ID_envioCarnet, string env_estado,string env_soporte, string env_observacion, string ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_envioCarnet", ID_envioCarnet);
            param.Add("@env_estado", env_estado);
            param.Add("@env_soporte", env_soporte);
            param.Add("@env_observacion", env_observacion);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);

            procedureTable("cor_EnvioCarnetsActualizar", true, param);
        }

        public DataTable EnvioCarnetsConsultarXAfiliado(string ID_afiliado)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliado", ID_afiliado);

            return procedureTable("cor_EnvioCarnetsConsultarXAfiliado", true, param);
        }

        #endregion

        #region ENTREGA DE FORMULARIOS

        public void EntregaFormulariosInsertar(string ID_afiliadoFK, string ID_contratoFK, string ID_asesorFK, int ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@ID_asesorFK", ID_asesorFK);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            procedureTable("cor_EntregaFormulariosInsertar", true, param);
        }

        public DataTable EntregaFormulariosConsultaXAfiliado(string ID_afiliadoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);

            return procedureTable("cor_EntregaFormulariosConsultarXAfiliado", true, param);
        }

        public void EntregaFormulariosActualizar(string ID_entrega, string ent_estado, string ent_observacion, int ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_entrega", ID_entrega);
            param.Add("@ent_estado", ent_estado);
            param.Add("@ent_observacion", ent_observacion);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);

            procedureTable("cor_EntregaFormulariosActualizar", true, param);
        }
        
        #endregion

        #region PRESTAMOS

        public DataTable prestamoInsertar(string ID_contratoFK, string ID_afiliadoFK, string ID_tipoPrestamoFK, string pre_prestamista, double pre_valor, int pre_cuotas,
            string pre_fechaInicio, string pre_frecuenciaPago, string pre_frecuenciaDetalle, double pre_descontarPrima, double pre_descontarCesantias,
            double pre_valorCuota, double pre_valorCuotaAjustada, string pre_observaciones, string ID_usuarioRegistraFK, bool pre_liquidacion)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK",ID_contratoFK);
            param.Add("@ID_afiliadoFK",ID_afiliadoFK);
            param.Add("@ID_tipoPrestamoFK",ID_tipoPrestamoFK);
            param.Add("@pre_prestamista",pre_prestamista);
            param.Add("@pre_valor",pre_valor);
            param.Add("@pre_cuotas",pre_cuotas);
            param.Add("@pre_fechaInicio",pre_fechaInicio);
            param.Add("@pre_frecuenciaPago",pre_frecuenciaPago);
            param.Add("@pre_frecuenciaDetalle",pre_frecuenciaDetalle);
            param.Add("@pre_descontarPrima",pre_descontarPrima);
            param.Add("@pre_descontarCesantias",pre_descontarCesantias);
            param.Add("@pre_valorCuota", pre_valorCuota);
            param.Add("@pre_valorCuotaAjustada", pre_valorCuotaAjustada);
            param.Add("@pre_observaciones",pre_observaciones);
            param.Add("@ID_usuarioRegistraFK",ID_usuarioRegistraFK);
            param.Add("@pre_liquidacion", pre_liquidacion);


            return procedureTable("pre_Insertar", true, param);
        }

        public void prestamoCuotaInsertar(string ID_prestamoFK,int cuo_tipo, int cuo_numeroCuota, double cuo_valor,double cuo_saldo, string cuo_fechaPago, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_prestamoFK", ID_prestamoFK);
            param.Add("@cuo_tipo", cuo_tipo);
            param.Add("@cuo_numeroCuota", cuo_numeroCuota);
            param.Add("@cuo_valor", cuo_valor);
            param.Add("@cuo_saldo", cuo_saldo);
            param.Add("@cuo_fechaPago", cuo_fechaPago);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            procedureTable("pre_CuotaInsertar", true, param);
        }

        public DataTable prestamoConsultaCuotasParaAplicar(string ID_afliadoFK, string ID_contratoFK, string cuo_fechaPago,int cuo_tipo)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@cuo_fechaPago", cuo_fechaPago);
            param.Add("@cuo_tipo", cuo_tipo); // 1 Cuota, 2 Prestamos, 3 Cesantias

            return procedureTable("pre_ConsultaCuotasParaAplicar", true, param);
        }

        public void prestamoActualizaCuota(string ID_cuota, string ID_usuarioRegistraPago, string ID_nominaPago)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_cuota", ID_cuota);
            param.Add("@ID_usuarioRegistraPago", ID_usuarioRegistraPago);
            param.Add("@ID_nominaPago", ID_nominaPago);

            procedureTable("pre_ActualizaCuota", true, param);
        }

        public DataTable prestamoConsultaXEmpleado(string ID_afliadoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afliadoFK);

            return procedureTable("pre_ConsultaXEmpleado", true, param);
        }

        public void prestamoSoporteInsertar(string ID_prestamoFK, string sop_nombreOriginal, string sop_nombreAlmacenado, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_prestamoFK", ID_prestamoFK);
            param.Add("@sop_nombreOriginal", sop_nombreOriginal);
            param.Add("@sop_nombreAlmacenado", sop_nombreAlmacenado);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            procedureTable("pre_SoporteInsertar", true, param);
        }

        public DataTable prestamoSoporteConsultar(string ID_prestamoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_prestamoFK", ID_prestamoFK);

            return procedureTable("pre_SoportesConsultar", true, param);
        }

        public DataTable prestamoAnular(string ID_prestamoFK, string pre_anuladoObservacion, string ID_usuarioAnulaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_prestamoFK", ID_prestamoFK);
            param.Add("@pre_anuladoObservacion", pre_anuladoObservacion);
            param.Add("@ID_usuarioAnulaFK", ID_usuarioAnulaFK);

            return procedureTable("pre_Anular", true, param);
        }

        public DataTable prestamoCondonar(string ID_prestamoFK, string pre_anuladoObservacion, string ID_usuarioAnulaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_prestamoFK", ID_prestamoFK);
            param.Add("@pre_anuladoObservacion", pre_anuladoObservacion);
            param.Add("@ID_usuarioAnulaFK", ID_usuarioAnulaFK);

            return procedureTable("pre_Anular", true, param);
        }

        #endregion

        #region NOVEDADES FIJAS

        public DataTable nofInsertar(string ID_tipoNovedadFK, string ID_contratoFK, string ID_afiliadoFK, double nof_valor, string nof_frecuenciaPago, string nof_frecuenciaDetalle, string nof_fechaInicio, string ID_usuarioRegistraFK, string nof_observaciones)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_tipoNovedadFK", ID_tipoNovedadFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@nof_valor", nof_valor);
            param.Add("@nof_frecuenciaPago", nof_frecuenciaPago);
            param.Add("@nof_frecuenciaDetalle", nof_frecuenciaDetalle);
            param.Add("@nof_fechaInicio", nof_fechaInicio);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
            param.Add("@nof_observaciones", nof_observaciones);

            return procedureTable("nof_Inserta", true, param);
        }

        public DataTable nofConsultarXAfiliado(string ID_afiliadoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);

            return procedureTable("nof_ConsultaXAfiliado", true, param);
        }

        public DataTable nofConsultarActivasXAfiliado(string ID_afiliadoFK, string ID_contratoFK, string fechaInicio)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_contratoFK", ID_contratoFK);
            param.Add("@fechaInicio", fechaInicio);

            return procedureTable("nof_ConsultaActivasXAfiliado", true, param);
        }

        public DataTable nofInactivar(string ID_novedadFija, string ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_novedadFija", ID_novedadFija);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);

            return procedureTable("nof_Inactivar", true, param);
        }
        #endregion

        #region NOMINAS

        public DataTable NomConsultarContratosXID(string ID_contrato)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK", ID_contrato);

            return procedureTable("nom_ConsultarXIDContrato", true, param);
        }

        public DataTable NomConsultaXIDContratoRetiroExtemporaneo(string ID_contratoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_contratoFK", ID_contratoFK);

            return procedureTable("nom_ConsultaXIDContratoRetiroExtemporaneo", true, param);
        }

        #endregion

        #region DEDUCCIONES RTE FUENTE

        public DataTable DeducRteConsultar(string ID_afiliadoFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);

            return procedureTable("nom_DeducRteConsultar", true, param);
        }

        public DataTable DeducRteInsertar(string ID_afiliadoFK, string ID_tipoDeduccionFK, string ded_anno, string ded_valor, string ded_soporte, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_afiliadoFK", ID_afiliadoFK);
            param.Add("@ID_tipoDeduccionFK", ID_tipoDeduccionFK);
            param.Add("@ded_anno", ded_anno);
            param.Add("@ded_valor", ded_valor);
            param.Add("@ded_soporte", ded_soporte);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            return procedureTable("nom_DeducRteInsertar", true, param);
        }

        public void DeducRteAnular(string ID_deduccion, string ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_deduccion", ID_deduccion);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);

            procedureTable("nom_DeducRteAnular", true, param);
        }

        #endregion
    }
}
