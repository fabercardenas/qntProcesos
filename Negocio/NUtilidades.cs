using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Negocio
{
    public static class NUtilidades
    {
        private static Datos.DUtilidades dUtilidad = new Datos.DUtilidades();

        public static void registraFalla(string procedimiento, string error)
        {
            dUtilidad.registraFalla(procedimiento, error );
        }

        public static DataTable consultaNivelesRiesgo()
        {
            return dUtilidad.consultaNivelesRiesgo();
        }

        public static DataTable consultaBancosXEmpresa(string ID_empresaFK)
        {
            return dUtilidad.consultaBancosXEmpresa(ID_empresaFK);
        }

        public static DataTable consultaBancosXEmpresaXID(string ID_bancoXEmpresa)
        {
            return dUtilidad.consultaBancosXEmpresaXID(ID_bancoXEmpresa);
        }
        
        public static string consultaBancoXID(string ID_banco)
        {
            DataTable tabla = dUtilidad.consultaBancoXID(ID_banco);
            if ((tabla != null) && (tabla.Rows.Count > 0))
                return tabla.Rows[0]["ban_nombre"].ToString();
            else
                return "";
        }

        public static DataTable consultaEmpresaXID(int ID_empresa)
        {
            return dUtilidad.consultaEmpresaXID(ID_empresa);
        }
        
        public static DataTable consultaEmpresaXNombreCorto(string emp_nombreCorto)
        {
            return dUtilidad.consultaEmpresaXNombreCorto(emp_nombreCorto);
        }

        #region REFERENCIAS
		 public static DataTable consultarReferenciasXModulo(string modulo, string orderBy = "ref_nombre", string excluir="")
        {
            if (modulo != "")
                return dUtilidad.consultaReferenciaXModulo(modulo, orderBy, excluir);
            else
                return null;
        }

        public static DataTable consultaReferenciaXModulo_y_Valor(string modulo, string valor)
        {
            if (modulo != "")
                return dUtilidad.consultaReferenciaXModulo_y_Valor(modulo, valor);
            else
                return null;
        }

        public static DataTable consultaReferenciaXModulo_y_Parametro(string modulo, string parametro1, string excluir="", string orderBy = "ref_orden")
        {
            if (modulo != "")
                return dUtilidad.consultaReferenciaXModulo_y_Parametro(modulo, parametro1,excluir, orderBy);
            else
                return null;
        }

        public static DataTable consultaReferenciaXModulo_y_Parametro2(string modulo, string parametro1, string excluir = "")
        {
            if (modulo != "")
                return dUtilidad.consultaReferenciaXModulo_y_Parametro2(modulo, parametro1, excluir);
            else
                return null;
        }

        public static DataTable consultarReferenciasXID(string ID_referencia)
        {
            return dUtilidad.consultaReferenciaXID(ID_referencia);
        }

        public static string consultarReferenciaValorXModuloUnico(string modulo)
        {
            DataTable tabla = dUtilidad.consultaReferenciaXModulo(modulo, "ref_nombre");
            if (tabla.Rows.Count > 0)
                return tabla.Rows[0]["ref_valor"].ToString();
            else
                return null;
        }

        public static int consultarSalarioMinimo(int año, bool auxilioTransporte = false)
        {
            DataTable tabla = dUtilidad.consultaSalarioMinimo(año.ToString());
            if (tabla.Rows.Count > 0)
                return (auxilioTransporte==false) ? (int)tabla.Rows[0]["sal_SMLV"] : (int)tabla.Rows[0]["sal_AuxilioTransporte"];
            else
                return -1;
        }

        public static int consultarUVTXaño(int año)
        {
            DataTable tabla = dUtilidad.consultaSalarioMinimo(año.ToString());
            if (tabla.Rows.Count > 0)
                return (int)tabla.Rows[0]["sal_UVT"];
            else
                return -1;
        }

        #endregion

        #region CARGAR DDLS

        public static void cargarDDLciiu(DropDownList ciiu)
        {
            ciiu.DataSource = dUtilidad.consultaCIIU();
            ciiu.DataTextField = "ciu_nombre";
            ciiu.DataValueField = "ciu_codigo";
            ciiu.DataBind();
        }

        public static void cargarDDLbancos(DropDownList bancos)
        {
            bancos.DataSource = dUtilidad.consultaBancos();
            bancos.DataTextField = "ban_nombre";
            bancos.DataValueField = "ID_banco";
            bancos.DataBind();
        }
        
        public static void cargarDDLbancosXempresa(DropDownList bancos, string empresa)
        {
            bancos.Items.Clear();
            bancos.DataSource = dUtilidad.consultaBancosXEmpresa(empresa);
            bancos.DataTextField = "nombreCuenta";
            bancos.DataValueField = "ID_bancoXEmpresa";
            bancos.DataBind();
        }

        public static void cargarDDLnivelesRiesgo(DropDownList nivelesRiesgo)
        {
            nivelesRiesgo.DataSource = dUtilidad.consultaNivelesRiesgo();
            nivelesRiesgo.DataTextField = "niv_nombre";
            nivelesRiesgo.DataValueField = "ID_nivelRiesgo";
            nivelesRiesgo.DataBind();
        }
        
        public static void cargarDDLasesoresComerciales(DropDownList asesores)
        {
            asesores.DataSource = dUtilidad.consultaAsesoresComerciales();
            asesores.DataTextField = "com_nombre";
            asesores.DataValueField = "ID_comercial";
            asesores.DataBind();
        }

        public static void cargarDDLempresas(DropDownList empresas, int ID_empresa = -1, bool seleccione = false)
        {
            if(ID_empresa ==-1)
                empresas.DataSource = dUtilidad.consultaEmpresas();
            else
                empresas.DataSource = dUtilidad.consultaEmpresaXID(ID_empresa);
            empresas.DataTextField = "emp_nombreCorto";
            empresas.DataValueField = "ID_empresa";
            empresas.DataBind();

            if (seleccione == true)
            {
                ListItem lt = new ListItem();
                lt.Text = "Seleccione uno";
                lt.Value = "-1";
                empresas.Items.Insert(0, lt);
            }
        }

        public static void cargarDLLDivipolaPais(DropDownList pais)
        {
            pais.DataSource = dUtilidad.consultaPaises();
            pais.DataTextField = "PAIS";
            pais.DataValueField = "COD_PAIS";
            pais.DataBind();
        }

        public static void cargarDLLDivipolaDepto(DropDownList depto)
        {
            depto.DataSource = dUtilidad.consultaDepartamentos();
            depto.DataTextField = "DEPARTAMENTO";
            depto.DataValueField = "COD_DEPTO";
            depto.DataBind();
        }

        public static void cargarDLLDivipolaDeptoXPais(DropDownList depto, string pais)
        {
            depto.DataSource = dUtilidad.consultaDepartamentosXPais(pais);
            depto.DataTextField = "DEPARTAMENTO";
            depto.DataValueField = "COD_DEPTO";
            depto.DataBind();
        }

        public static void cargarDLLDivipolaMunicipios(DropDownList municipio, string depto)
        {
            municipio.DataSource = dUtilidad.consultaMunicipios(depto);
            municipio.DataTextField = "MUNICIPIO";
            municipio.DataValueField = "COD_MUNI";
            municipio.DataBind();
        }

        /// <summary>
        /// Recibe la DropDown y lo llena
        /// </summary>
        /// <param name="ddlReferencia">El Objeto</param>
        /// <param name="modulo">Pues el modulo!</param>
        /// <param name="seleccione">Si se le añade el listItem de "Seleccione uno"</param>
        public static void cargarDDLReferenciaXModulo(DropDownList ddlReferencia, string modulo, bool seleccione = false)
        {
            ddlReferencia.DataSource = dUtilidad.consultaReferenciaXModulo(modulo, "ref_nombre");
            ddlReferencia.DataTextField = "ref_nombre";
            ddlReferencia.DataValueField = "ID_referencia";
            ddlReferencia.DataBind();

            if (seleccione == true)
            {
                ListItem lt = new ListItem();
                lt.Text = "Seleccione uno";
                lt.Value = "-1";
                ddlReferencia.Items.Insert(0, lt);
            }
        }

        public static void cargarDDLTiposDocumento(DropDownList ddlTipos, bool InclujeTI = false)
        {
            ListItem item1 = new ListItem();
            item1.Text = "Cédula de ciudadanía";
            item1.Value = "CC";
            ddlTipos.Items.Insert(0, item1);

            ListItem item2 = new ListItem();
            item2.Text = "Cédula de extranjería";
            item2.Value = "CE";
            ddlTipos.Items.Insert(0, item2);

            if (InclujeTI == true)
            {
                ListItem item3 = new ListItem();
                item3.Text = "Tarjeta de identidad";
                item3.Value = "TI";
                ddlTipos.Items.Insert(0, item3);
            }

            ListItem item4 = new ListItem();
            item4.Text = "Registro civil";
            item4.Value = "RC";
            ddlTipos.Items.Insert(0, item4);

            ListItem item5 = new ListItem();
            item5.Text = "Pasaporte";
            item5.Value = "PA";
            ddlTipos.Items.Insert(0, item5);

            ListItem item6 = new ListItem();
            item6.Text = "Permiso Especial de Permanencia";
            item6.Value = "PE";
            ddlTipos.Items.Insert(0, item6);

            ListItem item7 = new ListItem();
            item7.Text = "Permiso de Permanencia Temporal";
            item7.Value = "PT";
            ddlTipos.Items.Insert(0, item7);


            ddlTipos.SelectedValue = "CC";

            ddlTipos.DataBind();
        }

        public static void cargarDDLCargos(DropDownList cargos, bool seleccione = false)
        {
            cargos.DataSource = dUtilidad.consultaCargos();
            cargos.DataTextField = "car_nombre";
            cargos.DataValueField = "ID_cargo";
            cargos.DataBind();

            if (seleccione == true)
            {
                ListItem lt = new ListItem();
                lt.Text = "Seleccione uno";
                lt.Value = "-1";
                cargos.Items.Insert(0, lt);
            }
        }

        public static DataTable consultaCargoXID(string ID_cargo)
        {
            return dUtilidad.consultaCargoXID(ID_cargo);
        }

        public static void cargarDDLEmpresasUsuarias(DropDownList ddlEmpUsuarias, string per_nivel, string ID_empresaFK, bool seleccione = false, bool todas = false)
        {
            Datos.DClientes dCliente = new Datos.DClientes();
            ddlEmpUsuarias.DataSource = dCliente.ConsultaGeneral(1, per_nivel, ID_empresaFK); //-> 1 es cliente, 0 es proveedor
            ddlEmpUsuarias.DataTextField = "cli_nombre";
            ddlEmpUsuarias.DataValueField = "ID_cliente";
            ddlEmpUsuarias.DataBind();

            if (seleccione == true)
            {
                ListItem lt = new ListItem();
                lt.Text = "Seleccione uno";
                lt.Value = "-1";
                ddlEmpUsuarias.Items.Insert(0, lt);

                if (todas == true)
                {
                    ListItem lt2 = new ListItem();
                    lt2.Text = "Todas";
                    lt2.Value = "0";
                    ddlEmpUsuarias.Items.Insert(1, lt2);
                }
            }
            else
            {
                if (todas == true)
                {
                    ListItem lt2 = new ListItem();
                    lt2.Text = "Todas";
                    lt2.Value = "0";
                    ddlEmpUsuarias.Items.Insert(0, lt2);
                }
            }
        }
        
        public static void cargarLSBEmpresasUsuarias(CheckBoxList chkEmpUsuarias, string per_nivel, string ID_empresaFK, bool seleccione = false, bool todas = false)
        {
            Datos.DClientes dCliente = new Datos.DClientes();
            chkEmpUsuarias.DataSource = dCliente.ConsultaGeneral(1, per_nivel, ID_empresaFK); //-> 1 es cliente, 0 es proveedor
            chkEmpUsuarias.DataTextField = "cli_nombre";
            chkEmpUsuarias.DataValueField = "ID_cliente";
            chkEmpUsuarias.DataBind();

            if (seleccione == true)
            {
                ListItem lt = new ListItem();
                lt.Text = "Seleccione uno";
                lt.Value = "-1";
                chkEmpUsuarias.Items.Insert(0, lt);

                if (todas == true)
                {
                    ListItem lt2 = new ListItem();
                    lt2.Text = "Todas";
                    lt2.Value = "0";
                    chkEmpUsuarias.Items.Insert(1, lt2);
                }
            }
            else
            {
                if (todas == true)
                {
                    ListItem lt2 = new ListItem();
                    lt2.Text = "Todas";
                    lt2.Value = "0";
                    chkEmpUsuarias.Items.Insert(0, lt2);
                }
            }
        }

        public static void cargarGDVEmpresasUsuarias(GridView gdvDatos, string per_nivel, string ID_empresaFK, bool seleccione = false, bool todas = false)
        {
            Datos.DClientes dCliente = new Datos.DClientes();
            gdvDatos.DataSource = dCliente.ConsultaGeneral(1, per_nivel, ID_empresaFK); //-> 1 es cliente, 0 es proveedor
            gdvDatos.DataBind();
        }

        public static void cargarDDLCentrosCostoXEmpresasUsuarias(DropDownList ddlCentrosCosto, string EmpresaUsuaria, bool seleccione = false)
        {
            Datos.DClientes dCliente = new Datos.DClientes();
            ddlCentrosCosto.DataSource = dCliente.centroConsultarXCliente(EmpresaUsuaria);
            ddlCentrosCosto.DataTextField = "cen_nombre";
            ddlCentrosCosto.DataValueField = "ID_centroCosto";
            ddlCentrosCosto.DataBind();

            if (seleccione == true)
            {
                ListItem lt = new ListItem();
                lt.Text = "Seleccione uno";
                lt.Value = "-1";
                ddlCentrosCosto.Items.Insert(0, lt);
            }
        }

        public static void cargarDDLLaboratorios(DropDownList laboratorios, bool seleccione = false)
        {
            laboratorios.DataSource = dUtilidad.consultaLaboratorios();
            laboratorios.DataTextField = "cli_nombre";
            laboratorios.DataValueField = "ID_cliente";
            laboratorios.DataBind();

            if (seleccione == true)
            {
                ListItem lt = new ListItem();
                lt.Text = "Seleccione uno";
                lt.Value = "-1";
                laboratorios.Items.Insert(0, lt);
            }
        }

        public static void cargarDDLEnteros(DropDownList dropDown, int inicio, int fin, bool conCeros = true, bool descendiente = false)
        {
            if (descendiente == false)
            {
                for (int i = inicio; i <= fin; i++)
                {
                    ListItem lt = new ListItem();
                    if ((conCeros = true) && (i <= 9))
                    {
                        lt.Value = "0" + i.ToString();
                        lt.Text = "0" + i.ToString();
                    }
                    else
                    {
                        lt.Value = i.ToString();
                        lt.Text = i.ToString();
                    }
                    dropDown.Items.Add(lt);
                }
            }
            else {
                for (int i = fin; i >= inicio; i--)
                {
                    ListItem lt = new ListItem();
                    if ((conCeros = true) && (i <= 9))
                    {
                        lt.Value = "0" + i.ToString();
                        lt.Text = "0" + i.ToString();
                    }
                    else
                    {
                        lt.Value = i.ToString();
                        lt.Text = i.ToString();
                    }
                    dropDown.Items.Add(lt);
                }
            }
            dropDown.DataBind();

        }

        public static void cargarDDLMeses(DropDownList dropDown)
        {
            for (int i = 1; i <= 12; i++)
            {
                ListItem lt = new ListItem();
                lt.Value = i.ToString();
                lt.Text = MonthName(i);
                dropDown.Items.Add(lt);
            }
            dropDown.DataBind();
        }

        public static void cargarAdminSalud(DropDownList adminSalud, string tipo, bool aplica = false, string cobertura = "11")
        {
            adminSalud.DataSource = dUtilidad.consultaAdminSalud(tipo, cobertura);
            adminSalud.DataTextField = "adm_nombre";
            adminSalud.DataValueField = "adm_codigo";
            adminSalud.DataBind();

            if (aplica == true)
            {
                ListItem item = new ListItem();
                item.Text = "No solicita";
                item.Value = "NA";
                adminSalud.Items.Insert(0, item);
            }

        }

        #endregion

        #region NUMEROS Y REDONDEOS

        public static int redondea(double numero, int acercaA)
        {
            numero = numero / acercaA;
            return Convert.ToInt32(Math.Round(numero, 0, MidpointRounding.AwayFromZero) * acercaA);
        }

        public static int redondeaEncimaA(double numero, int acercaA)
        {
            numero = numero / acercaA;
            return Convert.ToInt32(Math.Ceiling(numero) * acercaA);

        }

        public static Boolean IsNumeric(string stringToTest)
        {
            int result;
            return int.TryParse(stringToTest, out result);
        }

        public static Boolean IsDouble(string stringToTest)
        {
            double result;
            return double.TryParse(stringToTest, out result);
        }

        public static Boolean IsDate(string stringToTest)
        {
            DateTime result;

            return DateTime.TryParse(stringToTest, out result);
        }

        public static string NumeroALetras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = NumeroALetras(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private static string NumeroALetras(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UN";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + NumeroALetras(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + NumeroALetras(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + NumeroALetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
            }
            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }

            return Num2Text;
        }

        public static double stringMoneyToDouble(string cadena)
        {
            return Convert.ToDouble(decimal.Parse(System.Text.RegularExpressions.Regex.Replace(cadena, @"[^\d.]", "")));
        }

        #endregion

        #region ADMINSALUD
        public static DataTable consultaAdminSaludAdministracion(string adm_tipo, string adm_estado)
        {
            return dUtilidad.consultaAdminSaludAdministracion(adm_tipo, adm_estado);
        }

        public static DataTable consultaAdminSaludXTipoXCodigo(string adm_tipo, string adm_codigo)
        {
            return dUtilidad.consultaAdminSaludXTipoXCodigo(adm_tipo, adm_codigo);
        }

        public static DataTable EditarAdminSalud(string adm_tipo, string adm_codigo, string adm_codigoAnterior, string adm_nit, string adm_nombre, string adm_nombreLargo, string adm_url, string adm_cobertura, bool adm_estado)
        {
            return dUtilidad.EditarAdminSalud(adm_tipo, adm_codigo, adm_codigoAnterior, adm_nit, adm_nombre, adm_nombreLargo, adm_url, adm_cobertura, adm_estado);
        }

        public static DataTable InsertarAdminSalud(string adm_tipo, string adm_codigo, string adm_nit, string adm_nombre, string adm_nombreLargo, string adm_url, string adm_cobertura)
        {
            return dUtilidad.InsertarAdminSalud(adm_tipo, adm_codigo, adm_nit, adm_nombre, adm_nombreLargo, adm_url, adm_cobertura);
        }

        public static string consultaAdminSaludNombre(string adm_codigo)
        {
            DataTable tabla = dUtilidad.consultaAdminSalud(adm_codigo);
            if (tabla.Rows.Count > 0)
                return tabla.Rows[0]["adm_nombre"].ToString();
            else
                return "";
        }

        #endregion

        /// <summary>
        /// COLOCA CEROS A IZQUIERDA O DERECHA DE LA CADENA
        /// </summary>
        /// <param name="cadena"></param>
        /// <param name="MaxCeros">SIEMPRE UNO ADICIONAL, POR QUE ES <, NO MENOR O IGUAL</param>
        /// <param name="izquierda"></param>
        /// <returns></returns>
        public static string rellenaCeros(string cadena, int MaxCeros, bool izquierda)
        {
            string ceros = "";
            if (cadena == null)
                cadena = "";

            //ES POR QUE DEBO LLENAR A LA IZQUIERDA ... 000CADENA
            if (izquierda == true)
            {
                for (int i = 1; i < (MaxCeros - cadena.Length); i++)
                {
                    ceros += "0";
                }
            }
            else
            {
                //ES POR QUE DEBO LLENAR A LA DERECHA  CADENA0000
                for (int i = cadena.Length; i < MaxCeros - 1; i++)
                {
                    cadena += "0";
                }
            }

            return ceros + cadena;
        }

        public static string rellenaCerosCorregido(string cadena, int MaxCeros, bool izquierda)
        {
            string ceros = "";
            if (cadena == null)
                cadena = "";

            //ES POR QUE DEBO LLENAR A LA IZQUIERDA ... 000CADENA
            if (izquierda == true)
            {
                for (int i = 1; i <= (MaxCeros - cadena.Length); i++)
                {
                    ceros += "0";
                }
            }
            else
            {
                //ES POR QUE DEBO LLENAR A LA DERECHA  CADENA0000
                for (int i = cadena.Length; i <= MaxCeros - 1; i++)
                {
                    cadena += "0";
                }
            }

            return ceros + cadena;
        }
        public static string rellenaEspacios(string cadena ,int MaxCadena ,bool  izquierda) 
        {
            string  ceros = "";
            if (cadena == null)
                cadena = "";

            //ES POR QUE DEBO LLENAR A LA IZQUIERDA ... 000CADENA
            if (izquierda == true)
            {
                for (int i = 1; i < (MaxCadena - cadena.Length); i++)
                {
                    ceros += " ";
                }
            }
            else
            {
                //ES POR QUE DEBO LLENAR A LA DERECHA  CADENA0000
                for (int i = cadena.Length; i < MaxCadena - 1; i++)
                {
                    cadena += " ";
                }
            }

            return ceros + cadena;
        }

        public static int calculaDias360(DateTime fechaInicial, DateTime fechaFinal)
        {
            //return ((fechaFinal.Month - fechaInicial.Month) * 30) + (fechaFinal.Day - (fechaInicial.Day));
            int diaFinal = fechaFinal.Day;
            int mesFinal = fechaFinal.Month;
            int añoFinal = fechaFinal.Year;
            if (fechaFinal.Day < fechaInicial.Day)
            {
                diaFinal = fechaFinal.Day + 30;
                mesFinal = fechaFinal.Month - 1;
            }

            if (fechaFinal.Month < fechaInicial.Month)
            {
                mesFinal = fechaFinal.Month + 12;
                añoFinal = fechaFinal.Year - 1;
            }
            return ((diaFinal + 1) - (fechaInicial.Day)) + (((mesFinal) - (fechaInicial.Month)) * 30) + ((añoFinal - fechaInicial.Year) * 360);
        }

        public static int calcularTiempo(DateTime fechaInicial, string fechaFinal = "HOY", string rango = "meses")
        {
            DateTime fechaFin;
            TimeSpan diferencia;
            int retornar = 0;

            if (fechaFinal != "HOY")
                fechaFin = DateTime.Parse(fechaFinal);
            else
                fechaFin = DateTime.Today; //DateTime.Parse("1900-01-01");

            diferencia = fechaFin - fechaInicial;

            switch (rango)
            {
                case "años":
                    retornar = diferencia.Days / 360;
                    break;
                case "meses":
                    retornar = diferencia.Days / 30;
                    break;
                case "dias":
                    retornar = diferencia.Days + 1;
                    break;

                default:
                    break;
            }

            return retornar;

        }

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            double monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(Convert.ToInt32(Math.Round(monthsApart, 0, MidpointRounding.AwayFromZero)));
        }

        public static Int64 IdentificadorUnico(string ID_usuario)
        {
            return Int64.Parse(ID_usuario + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString());

        }

        public static Int64 IdentificadorNomina(string ID_empresaTemporal)
        {
            return Int64.Parse(ID_empresaTemporal + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now));

        }

        public static string MonthName(int month)
        {
            System.Globalization.DateTimeFormatInfo dtinfo = new System.Globalization.CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }

        #region SEGURIDAD PERFILES

        public static void SeguridadPerfilOcultar(DataTable tbAcciones, Control contenedor, string pagina, string id_perfil)
        {
            if(id_perfil!="0")
            { 
                //FILTRO POR LO QUE ESTA EN SESSION
                DataRow[] result = tbAcciones.Select("acc_pagina='" + pagina + "'");
                foreach (DataRow row in result)
                {
                    switch ((int)row["acc_NivelAgrupacion"])
                    {
                        #region NIVEL AGRUPACION 0
                        case 0:
                            contenedor.FindControl(row["acc_objeto"].ToString()).Visible = false;
                            break;
                        #endregion
                        #region NIVEL 1
                        case 1:
                            Control controlNivel1 = contenedor.FindControl(row["acc_agrupador1"].ToString());
                            if(controlNivel1.FindControl(row["acc_objeto"].ToString())!=null)
                                controlNivel1.FindControl(row["acc_objeto"].ToString()).Visible = false;
                            break;
                        #endregion
                        #region NIVEL 2
                        case 2:
                            Control controlNivel2_1 = contenedor.FindControl(row["acc_agrupador1"].ToString());
                            Control controlNivel2 = controlNivel2_1.FindControl(row["acc_agrupador2"].ToString());
                            controlNivel2.FindControl(row["acc_objeto"].ToString()).Visible = false;
                            break;
                        #endregion
                        #region NIVEL 3
                        case 3:
                            Control controlNivel3_1 = contenedor.FindControl(row["acc_agrupador1"].ToString());
                            Control controlNivel3_2 = contenedor.FindControl(row["acc_agrupador2"].ToString());
                            Control controlNivel3 = controlNivel3_2.FindControl(row["acc_agrupador3"].ToString());
                            controlNivel3.FindControl(row["acc_objeto"].ToString()).Visible = false;
                            break;
                        #endregion
                        default:
                            break;
                    }
                }
            }
        }

        public static bool SeguridadPerfilOcultarEnGrilla(DataTable tbAcciones, string pagina, string objeto, bool estadoActual)
        {
            DataRow[] result = tbAcciones.Select("acc_pagina='" + pagina + "' AND acc_objeto='" + objeto + "'");
            if ((result != null) && (result.Length > 0))
                return false;
            else
                return estadoActual;
        }
        #endregion
    }
}

