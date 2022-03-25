using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

using System.Security;
using System.Security.Cryptography;

namespace Datos
{
    public class DUtilidades: DSQL 
    {

        #region  DIVIPOLA 

        public DataTable consultaPaises()
        {
            return queryTable("SELECT COD_PAIS, PAIS FROM app_divipola GROUP BY COD_PAIS, PAIS ORDER BY PAIS");
        }

        public DataTable consultaDepartamentos()
        {
            return queryTable("SELECT COD_DEPTO, DEPARTAMENTO FROM app_divipola WHERE COD_PAIS='1' GROUP BY COD_DEPTO, DEPARTAMENTO ORDER BY DEPARTAMENTO");
        }

        public DataTable consultaDepartamentosXPais(string COD_PAIS)
        {
            return queryTable("SELECT COD_DEPTO, DEPARTAMENTO FROM app_divipola WHERE COD_PAIS='" + COD_PAIS + "' GROUP BY COD_DEPTO, DEPARTAMENTO ORDER BY DEPARTAMENTO");
        }

        public DataTable consultaDepartamentoXId(string COD_DEPTO)
        {
            return queryTable("SELECT COD_DEPTO, DEPARTAMENTO FROM app_divipola WHERE COD_DEPTO='" + COD_DEPTO + "' GROUP BY COD_DEPTO, DEPARTAMENTO");
        }

        public DataTable  consultaMunicipios(string  depto)
        {
            return queryTable("SELECT COD_MUNI, MUNICIPIO FROM app_divipola WHERE COD_DEPTO='" + depto + "' ORDER BY MUNICIPIO");
        }

        public DataTable consultaMunicipioXId(string COD_MUNI, string COD_DEPTO)
        {
            return queryTable("SELECT COD_MUNI, MUNICIPIO FROM app_divipola WHERE COD_DEPTO='" + COD_DEPTO + "' AND COD_MUNI='" + COD_MUNI + "'");
        }

        #region EDITAR E INSERTAR

        public DataTable consultaDivipolXPais(string COD_PAIS)
        {
            return queryTable("SELECT * FROM app_divipola WHERE COD_PAIS='" + COD_PAIS + "' ORDER BY DEPARTAMENTO, MUNICIPIO");
        }

        public DataTable divipolInsertar(string COD_PAIS, string PAIS, string COD_DEPTO, string DEPARTAMENTO, string COD_MUNI, string MUNICIPIO)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@COD_PAIS", COD_PAIS);
            param.Add("@PAIS", PAIS);
            param.Add("@COD_DEPTO", COD_DEPTO);
            param.Add("@DEPARTAMENTO", DEPARTAMENTO);
            param.Add("@COD_MUNI", COD_MUNI);
            param.Add("@MUNICIPIO", MUNICIPIO);
            return procedureTable("uti_InsertarDivipol", true, param);
        }

        public DataTable divipolEditar(string antCOD_PAIS, string antCOD_DEPTO, string antCOD_MUNI,string COD_PAIS, string PAIS, string COD_DEPTO, string DEPARTAMENTO, string COD_MUNI, string MUNICIPIO)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@antCOD_PAIS", antCOD_PAIS);
            param.Add("@antCOD_DEPTO", antCOD_DEPTO);
            param.Add("@antCOD_MUNI", antCOD_MUNI);
            param.Add("@COD_PAIS", COD_PAIS);
            param.Add("@PAIS", PAIS);
            param.Add("@COD_DEPTO", COD_DEPTO);
            param.Add("@DEPARTAMENTO", DEPARTAMENTO);
            param.Add("@COD_MUNI", COD_MUNI);
            param.Add("@MUNICIPIO", MUNICIPIO);
            return procedureTable("uti_EditarDivipol", true, param);
        }

        #endregion

        #endregion

        public DataTable consultaCIIU()
        {
            return procedureTable("uti_consultaCIIU", false);
        }

        public DataTable consultaBancos()
        {
            return procedureTable("uti_consultaBancos", false);
        }

        public DataTable consultaBancosXEmpresa(string ID_empresaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_empresaFK", ID_empresaFK);
            return procedureTable("uti_ConsultaBancosXEmpresa", true, param );
        }
        public DataTable BancosXEmpresaInsertar(string ID_bancoFK, string ID_empresaFK, string ban_cuentaNumero, string ban_cuentaTipo, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_bancoFK", ID_bancoFK);
            param.Add("@ID_empresaFK", ID_empresaFK);
            param.Add("@ban_cuentaNumero", ban_cuentaNumero);
            param.Add("@ban_cuentaTipo", ban_cuentaTipo);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
            return procedureTable("uti_BancoXEmpresaInsertar", true, param);
        }
        public DataTable BancosXEmpresaEditar(string ID_bancoXEmpresa, string ID_bancoFK, string ID_empresaFK, string ban_cuentaNumero, string ban_cuentaNumeroAnterior, string ban_cuentaTipo, string ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_bancoXEmpresa", ID_bancoXEmpresa);
            param.Add("@ID_bancoFK", ID_bancoFK);
            param.Add("@ID_empresaFK", ID_empresaFK);
            param.Add("@ban_cuentaNumero", ban_cuentaNumero);
            param.Add("@ban_cuentaNumeroAnterior", ban_cuentaNumeroAnterior);
            param.Add("@ban_cuentaTipo", ban_cuentaTipo);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);
            return procedureTable("uti_BancoXEmpresaEditar", true, param);
        }
        public DataTable consultaBancosXEmpresaXID(string ID_bancoXEmpresa)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_bancoXEmpresa", ID_bancoXEmpresa);
            return procedureTable("uti_ConsultaBancoXEmpresaXID", true, param);
        }

        public DataTable consultaBancoXID(string ID_banco)
        {
            return queryTable("SELECT * FROM uti_bancos WHERE ID_banco='" + ID_banco  + "'");
        }

        public DataTable consultaAsesoresComerciales()
        {
            return queryTable("SELECT * FROM car_comerciales WHERE com_estado=1");
        }

        public DataTable consultaEmpresas(string where = "")
        {
            return queryTable("SELECT * FROM app_empresa WHERE emp_estado=1 " + where);
        }

        public DataTable consultaEmpresaXID(int ID_empresa)
        {
            return queryTable("SELECT * FROM app_empresa WHERE ID_empresa='" + ID_empresa + "'");
        }

        public DataTable consultaEmpresaXNombreCorto(string emp_nombreCorto)
        {
            return queryTable("SELECT * FROM app_empresa WHERE emp_nombreCorto='" + emp_nombreCorto + "'");
        }

        public DataTable consultaTerceros()
        {
            return procedureTable("uti_consultaTerceros", false );
        }

        public DataTable consultaLaboratorios()
        {
            return procedureTable("cli_ConsultaLaboratorios", false);
        }

        
        //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------
        public DataTable consultaActividadesEconomicas()
        {
            return procedureTable("uti_consultaActividadesEconomicas", false);
        }

        public DataTable consultaActividadesLaborales()
        {
            return procedureTable("uti_consultaActividadesLaborales", false);
        }

        #region ADMINSALUD
        public DataTable consultaAdminSalud(string adm_tipo, string adm_cobertura = "NA")
        {
            Hashtable param = new Hashtable(1);
            param.Add("@adm_tipo", adm_tipo);
            param.Add("@adm_cobertura", adm_cobertura);
            return procedureTable("uti_consultaAdminSaludXTipo", true, param);
        }

        public DataTable consultaAdminSaludAdministracion(string adm_tipo, string adm_estado)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@adm_tipo", adm_tipo);
            param.Add("@adm_estado", adm_estado);
            return procedureTable("uti_consultaAdminSaludXTipoAdministracion", true, param);
        }

        public DataTable consultaAdminSaludXTipoXCodigo(string adm_tipo, string adm_codigo)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@adm_tipo", adm_tipo);
            param.Add("@adm_codigo", adm_codigo);
            return procedureTable("uti_consultaAdminSaludXTipoXCodigo", true, param);
        }

        public DataTable EditarAdminSalud(string adm_tipo, string adm_codigo, string adm_codigoAnterior, string adm_nit, string adm_nombre, string adm_nombreLargo, string adm_url, string adm_cobertura, bool adm_estado)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@adm_tipo", adm_tipo);
            param.Add("@adm_codigo", adm_codigo);
            param.Add("@adm_codigoAnterior", adm_codigoAnterior);
            param.Add("@adm_nit", adm_nit);
            param.Add("@adm_nombre", adm_nombre);
            param.Add("@adm_nombreLargo", adm_nombreLargo);
            param.Add("@adm_url", adm_url);
            param.Add("@adm_cobertura", adm_cobertura);
            param.Add("@adm_estado", adm_estado);

            return procedureTable("uti_EditarAdminSalud", true, param);
        }

        public DataTable InsertarAdminSalud(string adm_tipo, string adm_codigo, string adm_nit, string adm_nombre, string adm_nombreLargo, string adm_url, string adm_cobertura)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@adm_tipo", adm_tipo);
            param.Add("@adm_codigo", adm_codigo);
            param.Add("@adm_nit", adm_nit);
            param.Add("@adm_nombre", adm_nombre);
            param.Add("@adm_nombreLargo", adm_nombreLargo);
            param.Add("@adm_url", adm_url);
            param.Add("@adm_cobertura", adm_cobertura);

            return procedureTable("uti_InsertarAdminSalud", true, param);
        }

        #endregion

        public DataTable consultaAdminSalud(string adm_codigo ) 
        {
            return queryTable("SELECT * FROM app_AdminSalud WHERE adm_codigo='" + adm_codigo + "'");
        }

        public DataTable consultaAsesoresEPS(int ase_estado = 1)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ase_estado", ase_estado);
            return procedureTable("uti_ConsultaAsesores", true, param);
        }

        public DataTable consultaCargos()
        {
            return procedureTable("uti_ConsultaCargos", false);
        }

        public DataTable consultaCargoXID(string ID_cargo)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_cargo", ID_cargo);

            return procedureTable("uti_ConsultaCargoXID", true,param);
        }

        public DataTable consultaNivelesRiesgo()
        {
            return queryTable("SELECT * FROM app_nivelesRiesgo");
        }

        public DataTable consultaReferenciasModulos()
        {
            return queryTable("select ref_modulo from app_Referencias where ref_estado=1 group by ref_modulo, ref_estado order by ref_estado desc, ref_modulo asc");
        }

        public DataTable ReferenciasInsertar(string ref_modulo, string ref_valor, string ref_nombre, string ref_descripcion, int ref_parametro1, int ref_parametro2
            , int ref_orden, int ref_TipoNov, string ref_ContableConcepto, string ref_ContableCuenta, string ref_ContableNaturaleza, string ref_ContableCuenta2, string ref_ContableNaturaleza2, int ref_parametro3, int ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ref_modulo", ref_modulo);
            param.Add("@ref_valor", ref_valor);
            param.Add("@ref_nombre", ref_nombre);
            param.Add("@ref_descripcion", ref_descripcion);
            param.Add("@ref_parametro1", ref_parametro1);
            param.Add("@ref_parametro2", ref_parametro2);
            param.Add("@ref_orden", ref_orden);
            param.Add("@ref_TipoNov", ref_TipoNov);
            param.Add("@ref_ContableConcepto", ref_ContableConcepto);
            param.Add("@ref_ContableCuenta", ref_ContableCuenta);
            param.Add("@ref_ContableNaturaleza", ref_ContableNaturaleza);
            param.Add("@ref_ContableCuenta2", ref_ContableCuenta2);
            param.Add("@ref_ContableNaturaleza2", ref_ContableNaturaleza2);
            param.Add("@ref_parametro3", ref_parametro3);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            return procedureTable("uti_ReferenciasInsertar", true, param);
        }

        public DataTable ReferenciasActualizar(int ID_referencia, string ref_modulo, string ref_valor, string ref_nombre, string ref_descripcion, int ref_parametro1, int ref_parametro2
            , int ref_orden, int ref_TipoNov, string ref_ContableConcepto, string ref_ContableCuenta, string ref_ContableNaturaleza, string ref_ContableCuenta2, string ref_ContableNaturaleza2, int ref_parametro3, bool ref_estado, int ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_referencia", ID_referencia);
            param.Add("@ref_modulo", ref_modulo);
            param.Add("@ref_valor", ref_valor);
            param.Add("@ref_nombre", ref_nombre);
            param.Add("@ref_descripcion", ref_descripcion);
            param.Add("@ref_parametro1", ref_parametro1);
            param.Add("@ref_parametro2", ref_parametro2);
            param.Add("@ref_orden", ref_orden);
            param.Add("@ref_TipoNov", ref_TipoNov);
            param.Add("@ref_ContableConcepto", ref_ContableConcepto);
            param.Add("@ref_ContableCuenta", ref_ContableCuenta);
            param.Add("@ref_ContableNaturaleza", ref_ContableNaturaleza);
            param.Add("@ref_ContableCuenta2", ref_ContableCuenta2);
            param.Add("@ref_ContableNaturaleza2", ref_ContableNaturaleza2);
            param.Add("@ref_parametro3", ref_parametro3);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);
            param.Add("@ref_estado", ref_estado);

            return procedureTable("uti_ReferenciasActualizar", true, param);
        }
        public DataTable consultaReferenciaXModulo(string modulo, string orderBy, string excluir = "")
        {
            if (excluir == "")
                return queryTable("SELECT * FROM app_Referencias WHERE ref_modulo='" + modulo  + "' AND ref_estado='1' ORDER BY " + orderBy);
            else
                return queryTable("SELECT * FROM app_Referencias WHERE ref_modulo='" + modulo + "' AND ref_valor NOT IN ('" + excluir + "') AND ref_estado='1' ORDER BY " + orderBy);
        }

        public DataTable consultaReferenciaXModulo_y_Valor(string modulo, string valor)
        {
            return queryTable("SELECT * FROM app_Referencias WHERE ref_modulo='" + modulo + "' AND ref_valor='" + valor + "' AND ref_estado='1'");
        }

        public DataTable consultaReferenciaXModulo_y_Parametro(string modulo, string parametro, string excluir, string orderBy = "ref_orden")
        {
            if(excluir=="")
                return queryTable("SELECT * FROM app_Referencias WHERE ref_modulo='" + modulo + "' AND ref_parametro1='" + parametro + "' AND ref_estado='1' ORDER BY " + orderBy);
            else
                return queryTable("SELECT * FROM app_Referencias WHERE ref_modulo='" + modulo + "' AND ref_parametro1='" + parametro + "' AND ref_valor NOT IN ('" + excluir + "') AND ref_estado='1' ORDER BY " + orderBy);
        }

        public DataTable consultaReferenciaXModulo_y_Parametro2(string modulo, string parametro, string excluir)
        {
            if (excluir == "")
                return queryTable("SELECT * FROM app_Referencias WHERE ref_modulo='" + modulo + "' AND ref_parametro2='" + parametro + "' AND ref_estado='1' ORDER BY ref_orden");
            else
                return queryTable("SELECT * FROM app_Referencias WHERE ref_modulo='" + modulo + "' AND ref_parametro2='" + parametro + "' AND ref_valor NOT IN ('" + excluir + "') AND ref_estado='1' ORDER BY ref_orden");
        }

        public DataTable consultaReferenciaXModulo_y_Parametro3(string modulo, string parametro, string excluir)
        {
            if (excluir == "")
                return queryTable("SELECT * FROM app_Referencias WHERE ref_modulo='" + modulo + "' AND ref_parametro3='" + parametro + "' AND ref_estado='1' ORDER BY ref_orden");
            else
                return queryTable("SELECT * FROM app_Referencias WHERE ref_modulo='" + modulo + "' AND ref_parametro3='" + parametro + "' AND ref_valor NOT IN ('" + excluir + "') AND ref_estado='1' ORDER BY ref_orden");
        }

        public DataTable consultaReferenciaXID(string ID_referencia)
        {
            return queryTable("SELECT * FROM app_Referencias WHERE ID_referencia='" + ID_referencia + "'");
        }

        public DataTable consultaSOIXId(string id_registroSOI)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@id_registroSOI", id_registroSOI);

            return procedureTable("soi_ConsultaXID", true, param);
        }

        public DataTable consultaSalarioMinimo(string año)
        {
            return queryTable("SELECT * FROM app_SalarioMinimo WHERE sal_year='" + año + "'");
        }

        #region SEGURIDAD PERFILES
        public DataTable acc_consultaPaginas()
        {
            return queryTable("SELECT acc_pagina FROM app_AccionesXArchivo GROUP BY acc_pagina ORDER BY acc_pagina");
        }

        public DataTable acc_consultaAcciones(string acc_pagina)
        {
            return queryTable("SELECT ID_accion, acc_nombre FROM app_AccionesXArchivo WHERE acc_pagina='" + acc_pagina + "' AND acc_estado=1 ORDER BY acc_nombre");
        }

        public DataTable acc_consultaAccionesXPerfil(string acc_pagina, string ID_perfilFK)
        {
            return queryTable("SELECT ID_accionFK FROM app_AccionesXPerfil INNER JOIN dbo.app_AccionesXArchivo ON dbo.app_AccionesXPerfil.ID_accionFK = dbo.app_AccionesXArchivo.ID_accion WHERE acc_pagina='" + acc_pagina + "' AND acc_estado=1 AND ID_perfilFK='" + ID_perfilFK + "'");
        }

        public void acc_EliminaAccionesXPerfil(string acc_pagina, string ID_perfilFK)
        {
            queryTable("DELETE app_AccionesXPerfil FROM app_AccionesXPerfil INNER JOIN dbo.app_AccionesXArchivo ON dbo.app_AccionesXPerfil.ID_accionFK = dbo.app_AccionesXArchivo.ID_accion WHERE acc_pagina='" + acc_pagina + "' AND ID_perfilFK='" + ID_perfilFK + "'");
        }

        public void acc_InsertaAccionesXPerfil(string ID_accionFK, string ID_perfilFK, string ID_usuarioRegistraFK)
        {
            queryTable("INSERT INTO app_AccionesXPerfil(ID_accionFK, ID_perfilFK, ID_usuarioRegistraFK) VALUES('" + ID_accionFK + "','" + ID_perfilFK + "','" + ID_usuarioRegistraFK + "')");
        }
        #endregion

        public void registraFalla(string procedimiento, string error)
        {
            queryTable("INSERT INTO px_fallas(fa_modulo, fa_descripcion, fa_usuario) VALUES ('" + procedimiento + "','" + error + "','PROCEDURE')");
        }

    }
}
