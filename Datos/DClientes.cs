using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace Datos
{
	public class DClientes : DSQL
	{

		#region   CLIENTES 
		 public DataTable Insertar(string cli_nombre,string cli_nit, string cli_dv, string cli_direccion1,string cli_direccion2,string ID_deptoFK, string ID_ciudadFK,string cli_telefono,string cli_celular, string cli_enviarCorrespondencia, string cli_mail
			  , string cli_tipoFacturacion, string cli_actividad, string cli_cont1Nombre, string cli_cont1Tratamiento, string cli_cont1FechaNacimiento, string cli_cont1Correo
			   ,string cli_cont2Nombre,string cli_cont2Tratamiento,string cli_cont2FechaNacimiento,string cli_cont2Correo,string cli_asesorCial,string cli_asesorEmpresa,string cli_gestorNomina
			   ,string cli_antecedentes,string cli_observaciones,string  ID_usuarioRegistraFK, int cli_tipo, string cli_liquidaHorasExtra, string cli_IngresosNoPrestacionales, string cli_Formatos, double cli_ProvVacaciones)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@cli_nombre", cli_nombre);
			param.Add("@cli_nit", cli_nit);
			param.Add("@cli_dv", cli_dv);
			param.Add("@cli_direccion1", cli_direccion1);
			param.Add("@cli_direccion2", cli_direccion2);
			param.Add("@ID_deptoFK", ID_deptoFK);
			param.Add("@ID_ciudadFK", ID_ciudadFK);
			param.Add("@cli_telefono", cli_telefono);
			param.Add("@cli_celular", cli_celular);
			param.Add("@cli_enviarCorrespondencia", cli_enviarCorrespondencia);
			param.Add("@cli_mail", cli_mail);
			param.Add("@cli_tipoFacturacion", cli_tipoFacturacion);
			param.Add("@cli_actividad", cli_actividad);
			param.Add("@cli_cont1Nombre", cli_cont1Nombre);
			param.Add("@cli_cont1Tratamiento", cli_cont1Tratamiento);
			param.Add("@cli_cont1FechaNacimiento", cli_cont1FechaNacimiento);
			param.Add("@cli_cont1Correo", cli_cont1Correo);
			param.Add("@cli_cont2Nombre", cli_cont2Nombre);
			param.Add("@cli_cont2Tratamiento", cli_cont2Tratamiento);
			param.Add("@cli_cont2FechaNacimiento", cli_cont2FechaNacimiento);
			param.Add("@cli_cont2Correo", cli_cont2Correo);
			param.Add("@cli_asesorCial", cli_asesorCial);
			param.Add("@cli_asesorEmpresa", cli_asesorEmpresa);
			param.Add("@cli_gestorNomina", cli_gestorNomina);
			param.Add("@cli_antecedentes", cli_antecedentes);
			param.Add("@cli_observaciones", cli_observaciones);
			param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);
			param.Add("@cli_tipo", cli_tipo); 
            param.Add("@cli_liquidaHorasExtra", cli_liquidaHorasExtra);
            param.Add("@cli_IngresosNoPrestacionales", cli_IngresosNoPrestacionales);
            param.Add("@cli_Formatos", cli_Formatos);
            param.Add("@cli_ProvVacaciones", cli_ProvVacaciones);

            return procedureTable("cli_Insertar", true, param);
		}

		public DataTable  Actualizar(Int64 ID_cliente, string cli_nombre, string cli_nit, string cli_dv, string cli_direccion1, string cli_direccion2,string ID_deptoFK, string ID_ciudadFK, string cli_telefono, string cli_celular,string cli_enviarCorrespondencia, string cli_mail
			   , string cli_tipoFacturacion, string cli_actividad, string cli_cont1Nombre, string cli_cont1Tratamiento, string cli_cont1FechaNacimiento, string cli_cont1Correo
			   , string cli_cont2Nombre, string cli_cont2Tratamiento, string cli_cont2FechaNacimiento, string cli_cont2Correo, string cli_asesorCial, string cli_asesorEmpresa, string cli_gestorNomina
			   , string cli_antecedentes, string cli_observaciones, bool cli_estado, string cli_liquidaHorasExtra, string cli_IngresosNoPrestacionales, string cli_Formatos, double cli_ProvVacaciones)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_cliente", ID_cliente);
			param.Add("@cli_nombre", cli_nombre);
			param.Add("@cli_nit", cli_nit);
			param.Add("@cli_dv", cli_dv);
			param.Add("@cli_direccion1", cli_direccion1);
			param.Add("@cli_direccion2", cli_direccion2);
			param.Add("@ID_deptoFK", ID_deptoFK);
			param.Add("@ID_ciudadFK", ID_ciudadFK);
			param.Add("@cli_telefono", cli_telefono);
			param.Add("@cli_celular", cli_celular);
			param.Add("@cli_enviarCorrespondencia", cli_enviarCorrespondencia);
			param.Add("@cli_mail", cli_mail);
			param.Add("@cli_tipoFacturacion", cli_tipoFacturacion);
			param.Add("@cli_actividad", cli_actividad);
			param.Add("@cli_cont1Nombre", cli_cont1Nombre);
			param.Add("@cli_cont1Tratamiento", cli_cont1Tratamiento);
			param.Add("@cli_cont1FechaNacimiento", cli_cont1FechaNacimiento);
			param.Add("@cli_cont1Correo", cli_cont1Correo);
			param.Add("@cli_cont2Nombre", cli_cont2Nombre);
			param.Add("@cli_cont2Tratamiento", cli_cont2Tratamiento);
			param.Add("@cli_cont2FechaNacimiento", cli_cont2FechaNacimiento);
			param.Add("@cli_cont2Correo", cli_cont2Correo);
			param.Add("@cli_asesorCial", cli_asesorCial);
			param.Add("@cli_asesorEmpresa", cli_asesorEmpresa);
			param.Add("@cli_gestorNomina", cli_gestorNomina);
			param.Add("@cli_antecedentes", cli_antecedentes);
			param.Add("@cli_observaciones", cli_observaciones); 
            param.Add("@cli_estado", Convert.ToInt16(cli_estado));
            param.Add("@cli_liquidaHorasExtra", cli_liquidaHorasExtra);
            param.Add("@cli_IngresosNoPrestacionales", cli_IngresosNoPrestacionales);
            param.Add("@cli_Formatos", cli_Formatos);
            param.Add("@cli_ProvVacaciones", cli_ProvVacaciones);


            return procedureTable("cli_Actualizar", true, param);
		}

		public DataTable ConsultarXID(string  ID_cliente)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_cliente", ID_cliente);

			return procedureTable("cli_consultaXID", true, param);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="cli_tipo">1 Cliente, 0 Proveedor</param>
		/// <returns></returns>
		public DataTable ConsultaGeneral(int cli_tipo, string per_nivel, string ID_empresaFK, string cli_estado = "1")
		{
			Hashtable param = new Hashtable(1);
			param.Add("@cli_tipo", cli_tipo);
			param.Add("@per_nivel", per_nivel);
			param.Add("@ID_empresaFK", ID_empresaFK);
			param.Add("@cli_estado", cli_estado);

			return procedureTable("cli_consultaGeneral", true, param );
		}

		public DataTable repClientesConCartera(Int64  fac_empresa,Int64  ID_cliente = 0)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_cliente", ID_cliente);
			param.Add("@fac_empresa", fac_empresa);

			return procedureTable("rep_ClientesConCartera", true, param);
		}
		#endregion

		#region COMUNICACIONES
		public void comunicacionInsertar(string com_motivo,string ID_clienteFK, string com_texto, string ID_usuarioRegistra, string com_fechaRecordatorio)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@com_motivo", com_motivo);
			param.Add("@ID_clienteFK", ID_clienteFK);
			param.Add("@com_texto", com_texto);
			param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);
			param.Add("@com_fechaRecordatorio", com_fechaRecordatorio);

			procedureTable("com_Insertar", true, param);
		}

		public DataTable comunicacionConsultarXCliente(string ID_clienteFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_clienteFK", ID_clienteFK);

			return procedureTable("com_consultaXcliente", true, param);
		}

		#endregion

		#region EXAMENES DE LABORATORIO

		public DataTable examenInsertar(string ID_laboratorioFK, string exa_nombre, string exa_descripcion, double exa_valor, string ID_usuarioRegistraFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_laboratorioFK", ID_laboratorioFK);
			param.Add("@exa_nombre", exa_nombre);
			param.Add("@exa_descripcion", exa_descripcion);
			param.Add("@exa_valor", exa_valor);
			param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

			return procedureTable("exa_Insertar", true, param);
		}

		public DataTable examenActualizar(string ID_examenXlaboratorio, string exa_nombre, string exa_descripcion, double exa_valor, string ID_usuarioActualizaFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_examenXlaboratorio", ID_examenXlaboratorio);
			param.Add("@exa_nombre", exa_nombre);
			param.Add("@exa_descripcion", exa_descripcion);
			param.Add("@exa_valor", exa_valor);
			param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);

			return procedureTable("exa_Actualizar", true, param);
		}

		public DataTable examenConsultaXLaboratorio(string ID_laboratorioFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_laboratorioFK", ID_laboratorioFK);

			return procedureTable("exa_ConsultaXLaboratorio", true, param);
		}

		public DataTable examenConsultaXID(string ID_examenXlaboratorio)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_examenXlaboratorio", ID_examenXlaboratorio);
			return procedureTable("exa_ConsultaXID", true, param);
		}

        #endregion

        #region CENTROS DE COSTO
        public DataTable centroInsertar(string ID_clienteFK, string cen_nombre, string cen_codigo, string ID_deptoFK, string ID_ciudadFK, string cen_direccion, string cen_tipoCuotaAdmon, double cen_cuotaAdmon, double cen_cuotaAdmonContratista, double cen_cuotaContratacion, double cen_cuotaReingreso, double cen_cuotaExamenIng, double cen_cuotaExamenRet, string cen_base4x1000, string cen_frecuenciaPago, string cen_tipoPersonal, string cen_condiciones, string ID_laboratorioFK, string ID_usuarioRegistra, string cen_cuotaDispBancolombia, string cen_cuotaDispDaviplata, string cen_cuotaDispAvVillas, string cen_cuotaDispersion, string cen_PagaIncapacidad, string cen_provisiona, string cen_dispersa, string cen_facturaExamenIng, string cen_facturaExamenRet, string cen_frecuenciaFacturacion, string cen_PagaIncapacidadToda)
        {
            Hashtable param = new Hashtable(1);

            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@cen_nombre", cen_nombre);
            param.Add("@cen_codigo", cen_codigo);
            param.Add("@ID_deptoFK", ID_deptoFK);
            param.Add("@ID_ciudadFK", ID_ciudadFK);
            param.Add("@cen_direccion", cen_direccion);
            param.Add("@cen_tipoCuotaAdmon", cen_tipoCuotaAdmon);
            param.Add("@cen_cuotaAdmon", cen_cuotaAdmon);
            param.Add("@cen_cuotaAdmonContratista", cen_cuotaAdmonContratista);
            param.Add("@cen_cuotaContratacion", cen_cuotaContratacion);
            param.Add("@cen_cuotaReingreso", cen_cuotaReingreso);
            param.Add("@cen_cuotaExamenIng", cen_cuotaExamenIng);
            param.Add("@cen_cuotaExamenRet", cen_cuotaExamenRet);
            param.Add("@cen_base4x1000", cen_base4x1000);
            param.Add("@cen_frecuenciaPago", cen_frecuenciaPago);
            param.Add("@cen_tipoPersonal", cen_tipoPersonal);
            param.Add("@cen_condiciones", cen_condiciones);
            param.Add("@ID_laboratorioFK", ID_laboratorioFK);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);
            param.Add("@cen_cuotaDispBancolombia", cen_cuotaDispBancolombia);
            param.Add("@cen_cuotaDispDaviplata", cen_cuotaDispDaviplata);
            param.Add("@cen_cuotaDispAvVillas", cen_cuotaDispAvVillas);
            param.Add("@cen_cuotaDispersion", cen_cuotaDispersion);
            param.Add("@cen_PagaIncapacidad", cen_PagaIncapacidad);
            param.Add("@cen_provisiona", cen_provisiona);
            param.Add("@cen_dispersa", cen_dispersa);
            param.Add("@cen_facturaExamenIng", cen_facturaExamenIng);
            param.Add("@cen_facturaExamenRet", cen_facturaExamenRet);
            param.Add("@cen_frecuenciaFacturacion", cen_frecuenciaFacturacion);
            param.Add("@cen_PagaIncapacidadToda", cen_PagaIncapacidadToda);

            return procedureTable("cen_Insertar", true, param);
        }

        public void centroActualizar(string ID_centroCosto, string ID_clienteFK, string cen_nombre, string cen_codigo, string ID_deptoFK, string ID_ciudadFK, string cen_direccion, string cen_tipoCuotaAdmon, double cen_cuotaAdmon, double cen_cuotaAdmonContratista, double cen_cuotaContratacion, double cen_cuotaReingreso, double cen_cuotaExamenIng, double cen_cuotaExamenRet, string cen_base4x1000, string cen_frecuenciaPago, string cen_tipoPersonal, string cen_condiciones, string ID_laboratorioFK, string ID_usuarioActualizaFK, string cen_cuotaDispBancolombia, string cen_cuotaDispDaviplata, string cen_cuotaDispAvVillas, string cen_cuotaDispersion, string cen_PagaIncapacidad, string cen_provisiona, string cen_dispersa, string cen_facturaExamenIng, string cen_facturaExamenRet, string cen_frecuenciaFacturacion, bool cen_estado, string cen_PagaIncapacidadToda)
		{
			Hashtable param = new Hashtable(1);

			param.Add("@ID_centroCosto", ID_centroCosto);

            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@cen_nombre", cen_nombre);
            param.Add("@cen_codigo", cen_codigo);
            param.Add("@ID_deptoFK", ID_deptoFK);
            param.Add("@ID_ciudadFK", ID_ciudadFK);
            param.Add("@cen_direccion", cen_direccion);
            param.Add("@cen_tipoCuotaAdmon", cen_tipoCuotaAdmon);
            param.Add("@cen_cuotaAdmon", cen_cuotaAdmon);
            param.Add("@cen_cuotaAdmonContratista", cen_cuotaAdmonContratista);
            param.Add("@cen_cuotaContratacion", cen_cuotaContratacion);
            param.Add("@cen_cuotaReingreso", cen_cuotaReingreso);
            param.Add("@cen_cuotaExamenIng", cen_cuotaExamenIng);
            param.Add("@cen_cuotaExamenRet", cen_cuotaExamenRet);
            param.Add("@cen_base4x1000", cen_base4x1000);
            param.Add("@cen_frecuenciaPago", cen_frecuenciaPago);
            param.Add("@cen_tipoPersonal", cen_tipoPersonal);
            param.Add("@cen_condiciones", cen_condiciones);
            param.Add("@ID_laboratorioFK", ID_laboratorioFK);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);
            param.Add("@cen_cuotaDispBancolombia", cen_cuotaDispBancolombia);
            param.Add("@cen_cuotaDispDaviplata", cen_cuotaDispDaviplata);
            param.Add("@cen_cuotaDispAvVillas", cen_cuotaDispAvVillas);
            param.Add("@cen_cuotaDispersion", cen_cuotaDispersion);
            param.Add("@cen_PagaIncapacidad", cen_PagaIncapacidad);
            param.Add("@cen_provisiona", cen_provisiona);
            param.Add("@cen_dispersa", cen_dispersa);
            param.Add("@cen_facturaExamenIng", cen_facturaExamenIng);
            param.Add("@cen_facturaExamenRet", cen_facturaExamenRet);
            param.Add("@cen_frecuenciaFacturacion", cen_frecuenciaFacturacion);
            param.Add("@cen_estado", cen_estado);
            param.Add("@cen_PagaIncapacidadToda", cen_PagaIncapacidadToda);

            procedureTable("cen_Actualizar", true, param);
		}

		public DataTable centroConsultarXCliente(string ID_clienteFK)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_clienteFK", ID_clienteFK);

			return procedureTable("cen_ConsultaXCliente", true, param);
		}

        public DataTable centroConsultarInactivosXCliente(string ID_clienteFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_clienteFK", ID_clienteFK);

            return procedureTable("cen_ConsultaInactivosXCliente", true, param);
        }

        public DataTable centroConsultarXID(string ID_centroCosto)
		{
			Hashtable param = new Hashtable(1);
			param.Add("@ID_centroCosto", ID_centroCosto);

			return procedureTable("cen_ConsultaXID", true, param);
		}

        public DataTable centroActivar(string ID_centroCosto, string ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_centroCosto", ID_centroCosto);
            param.Add("@ID_usuarioActualizaFK", ID_usuarioActualizaFK);

            return procedureTable("cen_Activar", true, param);
        }


        #endregion

        #region SEDE

        public DataTable sedeInsertar(string ID_centroCostoFK, string sed_nombre, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_centroCostoFK", ID_centroCostoFK);
            param.Add("@sed_nombre", sed_nombre);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            return procedureTable("sed_Insertar", true, param);
        }

        #endregion

        #region METODOS PARA MIGRACION
        public int mig_clienteConsultarXNombre(string cli_nombre)
        {
            int idCentro = 0;
            DataTable tbDatos = queryTable("SELECT ID_cliente FROM car_Clientes WHERE cli_nombre='" + cli_nombre + "'");
            if (tbDatos.Rows.Count > 0)
                idCentro = (int)tbDatos.Rows[0]["ID_cliente"];

            return idCentro;
        }

        public int mig_centroConsultarXNombre(string cen_nombre, string ID_clienteFK)
        {
            int idCentro=0;
            DataTable tbDatos = queryTable("SELECT ID_centroCosto FROM car_CentrosCosto WHERE cen_nombre='" + cen_nombre + "' AND ID_clienteFK='" + ID_clienteFK + "'");
            if (tbDatos.Rows.Count > 0)
                idCentro = (int)tbDatos.Rows[0]["ID_centroCosto"];

            return idCentro;
        }

        public int mig_sedeConsultarXNombre(string sed_nombre, string ID_centroCostoFK)
        {
            int idCentro = 0;
            DataTable tbDatos = queryTable("SELECT ID_sede FROM car_CentrosSedes WHERE sed_nombre='" + sed_nombre + "' AND ID_centroCostoFK='" + ID_centroCostoFK + "'");
            if (tbDatos.Rows.Count > 0)
                idCentro = (int)tbDatos.Rows[0]["ID_sede"];

            return idCentro;
        }

        public bool Mig_paisConsultarXNombre(string PAIS)
        {

            DataTable tbDatos = queryTable("SELECT DEPARTAMENTO FROM app_divipola WHERE PAIS='" + PAIS + "'");
            if (tbDatos.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool mig_departamentoConsultarXCodigo(string COD_DEPTO)
        {
            
            DataTable tbDatos = queryTable("SELECT COD_DEPTO FROM app_divipola WHERE COD_DEPTO='" + COD_DEPTO + "'");
            if (tbDatos.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool Mig_departamentoConsultarXNombre(string PAIS, string DEPARTAMENTO)
        {

            DataTable tbDatos = queryTable("SELECT DEPARTAMENTO FROM app_divipola WHERE DEPARTAMENTO='" + DEPARTAMENTO + "' AND PAIS = '" + PAIS + "'");
            if (tbDatos.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool mig_municipioConsultarXCodigo(string COD_DEPTO, string COD_MUNI)
        {

            DataTable tbDatos = queryTable("SELECT COD_MUNI FROM app_divipola WHERE COD_DEPTO='" + COD_DEPTO + "' AND COD_MUNI='" + COD_MUNI + "'");
            if (tbDatos.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool Mig_municipioConsultarXNombre(string PAIS, string DEPARTAMENTO, string MUNICIPIO)
        {
            DataTable tbDatos = queryTable("SELECT COD_MUNI FROM app_divipola WHERE DEPARTAMENTO='" + DEPARTAMENTO + "' AND MUNICIPIO='" + MUNICIPIO + "' AND PAIS='" + PAIS + "'");
            if (tbDatos.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool mig_ReferenciaExiste(string ID_referencia)
        {
            DataTable tbDatos = queryTable("SELECT ID_referencia FROM app_Referencias WHERE ID_referencia='" + ID_referencia + "'");
            if (tbDatos.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool Mig_ExisteReferenciaXModulo_y_Nombre(string modulo, string ref_nombre)
        {
            DataTable tbDatos = queryTable("SELECT ID_referencia FROM app_Referencias WHERE ref_modulo='" + modulo + "' AND ref_nombre='" + ref_nombre + "' AND ref_estado='1'");
            if (tbDatos.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool Mig_AdminSaludXTipoXNombre(string adm_tipo, string adm_nombre)
        {
            DataTable tbDatos = queryTable("SELECT adm_codigo FROM app_adminSalud WHERE adm_tipo = '" + adm_tipo + "' AND adm_nombre = '" + adm_nombre + "' and adm_estado=1");
            if (tbDatos.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool Mig_CargoXNombre(string car_nombre)
        {
            DataTable tbDatos = queryTable("SELECT ID_cargo FROM nom_cargos WHERE car_nombre='" + car_nombre + "'");
            if (tbDatos.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public string Mig_codigoCargoXNombre(string car_nombre)
        {
            DataTable tbDatos = queryTable("SELECT ID_cargo FROM nom_cargos WHERE car_nombre='" + car_nombre + "'");
            return tbDatos.Rows[0]["ID_cargo"].ToString();
        }

        public string Mig_codigoReferenciaXModulo_y_Nombre(string modulo, string ref_nombre)
        {
            DataTable tbDatos = queryTable("SELECT ID_referencia FROM app_Referencias WHERE ref_modulo='" + modulo + "' AND ref_nombre='" + ref_nombre + "' AND ref_estado='1'");
            return tbDatos.Rows[0]["ID_referencia"].ToString();
        }

        public string Mig_codigoPaisConsultarXNombre(string PAIS)
        {

            DataTable tbDatos = queryTable("SELECT COD_PAIS FROM app_divipola WHERE PAIS='" + PAIS + "'");
            return tbDatos.Rows[0]["COD_PAIS"].ToString();
        }

        public string Mig_codDepartamentoConsultarXNombre(string PAIS, string DEPARTAMENTO)
        {

            DataTable tbDatos = queryTable("SELECT COD_DEPTO FROM app_divipola WHERE DEPARTAMENTO='" + DEPARTAMENTO + "' AND PAIS = '" + PAIS + "'");
            return tbDatos.Rows[0]["COD_DEPTO"].ToString();
        }

        public string Mig_codMunicipioConsultarXNombre(string PAIS, string DEPARTAMENTO, string MUNICIPIO)
        {
            DataTable tbDatos = queryTable("SELECT COD_MUNI FROM app_divipola WHERE DEPARTAMENTO='" + DEPARTAMENTO + "' AND MUNICIPIO='" + MUNICIPIO + "' AND PAIS='" + PAIS + "'");
            return tbDatos.Rows[0]["COD_MUNI"].ToString();
        }

        public string Mig_codigoAdminSaludXTipoXNombre(string adm_tipo, string adm_nombre)
        {
            DataTable tbDatos = queryTable("SELECT adm_codigo FROM app_adminSalud WHERE adm_tipo = '" + adm_tipo + "' AND adm_nombre = '" + adm_nombre + "' and adm_estado=1");
            return tbDatos.Rows[0]["adm_codigo"].ToString();
        }

        public string Mig_codigoNivelRiesgo(string ID_nivelRiesgo)
        {
            DataTable tbDatos = queryTable("SELECT ID_nivelRiesgo FROM app_nivelesRiesgo WHERE ID_nivelRiesgo = '" + ID_nivelRiesgo + "'");
            return tbDatos.Rows[0]["ID_nivelRiesgo"].ToString();
        }

        public bool mig_ExisteContrato(string ID_empresaTemporalFK, string ID_afiliadoFK)
        {
            DataTable tbDatos = queryTable("SELECT ID_contrato FROM dbo.nom_contratos WHERE ID_empresaTemporalFK = '" + ID_empresaTemporalFK + "' AND con_estado = 1 AND ID_afiliadoFK = '" + ID_afiliadoFK + "'");
            if (tbDatos.Rows.Count > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region HISTORICO
        public DataTable HistoricoConsultarXCliente(string ID_clienteFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_clienteFK", ID_clienteFK);

            return procedureTable("cli_HistoricoConsultar", true, param);
        }

        public void HistoricoInsertar(string ID_clienteFK, string his_tipoCambio, string his_campo, string his_valorAnterior, string his_valorNuevo, string ID_usuarioRegistra, string ID_centroCostoFK = "0")
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_clienteFK", ID_clienteFK);
            param.Add("@ID_centroCostoFK", ID_centroCostoFK);
            param.Add("@his_tipoCambio", his_tipoCambio);
            param.Add("@his_campo", his_campo);
            param.Add("@his_valorAnterior", his_valorAnterior);
            param.Add("@his_valorNuevo", his_valorNuevo);
            param.Add("@ID_usuarioRegistra", ID_usuarioRegistra);

            procedureTable("cli_HistoricoInsertar", true, param);
        }

        #endregion
    }
}
