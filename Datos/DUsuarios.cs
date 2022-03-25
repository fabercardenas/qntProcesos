using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Datos
{
    public class DUsuarios : DSQL
    {
        #region USUARIOS
        
        public DataTable ConsultarXUserName(string usr_userName)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@usr_userName", usr_userName);
            return procedureTable("usr_ConsultaUsuarioXuserName", true, param);
        }

        public DataTable ConsultarXID(string ID_usuario)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_usuario", ID_usuario);
            return procedureTable("usr_ConsultaXID", true, param);
        }

        public DataTable ConsultaGeneral(Int16 usr_estado)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@usr_estado", usr_estado);
            return procedureTable("usr_ConsultaGeneral", true, param);
        }

        public void ActualizaUltimoIngreso(string ID_usuario)
        {
            queryTable("UPDATE app_usuarios SET usr_fechaUltAcceso=getDate() WHERE ID_usuario='" + ID_usuario  + "'");
        }

        public void ActualizaClave(string ID_usuario, string usr_clave)
        {
            queryTable("UPDATE app_usuarios SET usr_clave = '" + usr_clave + "' WHERE ID_usuario = '" + ID_usuario + "'");
        }

        public void Editar(string ID_usuario , string usr_documento , Int16 ID_perfilFK , Int16 ID_oficinaFK, string ID_empresaFK, string usr_nombre1, string usr_nombre2,  string usr_apellido1, string  usr_apellido2,
            string usr_mail, string usr_telefono, string usr_direccion, string  usr_userName, string usr_clave,Int16 usr_estado)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_usuario", ID_usuario);
            param.Add("@usr_documento", usr_documento);
            param.Add("@ID_perfilFK", ID_perfilFK);
            param.Add("@ID_oficinaFK", ID_oficinaFK);
            param.Add("@ID_empresaFK", ID_empresaFK);
            param.Add("@usr_nombre1", usr_nombre1);
            param.Add("@usr_nombre2", usr_nombre2);
            param.Add("@usr_apellido1", usr_apellido1);
            param.Add("@usr_apellido2", usr_apellido2);
            param.Add("@usr_mail", usr_mail);
            param.Add("@usr_telefono", usr_telefono);
            param.Add("@usr_direccion", usr_direccion);
            param.Add("@usr_userName", usr_userName);
            param.Add("@usr_clave", usr_clave);
            param.Add("@usr_estado", usr_estado);

            procedureTable("usr_Editar", true, param);
        }

        public void Insertar(string usr_documento, Int16 ID_perfilFK, Int16 ID_oficinaFK,string ID_empresaFK, string usr_nombre1, string usr_nombre2, string usr_apellido1, string usr_apellido2,
           string usr_mail, string usr_telefono, string usr_direccion, string usr_userName, string usr_clave)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@usr_documento", usr_documento);
            param.Add("@ID_perfilFK", ID_perfilFK);
            param.Add("@ID_oficinaFK", ID_oficinaFK);
            param.Add("@ID_empresaFK", ID_empresaFK);
            param.Add("@usr_nombre1", usr_nombre1);
            param.Add("@usr_nombre2", usr_nombre2);
            param.Add("@usr_apellido1", usr_apellido1);
            param.Add("@usr_apellido2", usr_apellido2);
            param.Add("@usr_mail", usr_mail);
            param.Add("@usr_telefono", usr_telefono);
            param.Add("@usr_direccion", usr_direccion);
            param.Add("@usr_userName", usr_userName);
            param.Add("@usr_clave", usr_clave);

            procedureTable("usr_Insertar", true, param);
        }
                
        //PERFILES
        public DataTable ConsultaPerfiles()
        {
            return queryTable("SELECT * FROM app_perfiles WHERE ID_perfil<>0 AND per_estado=1 ORDER BY per_nombre ASC");
        }

        public DataTable ConsultaPerfilXID(string id_perfil)
        {
            return queryTable("SELECT * FROM app_perfiles WHERE ID_perfil='" + id_perfil + "'");
        }
        
        //CONSULTA TODOS LOS ITEM A QUE TIENE ACCESO UN PEFIL. SINO TIENE ACCESO DEVUELVE FALSE
        public DataTable consultaMenuXPerfil(int idPerfil) 
        {
            //if (control != "Default.aspx")
            //{
            //    //ALMACENAR EL LISTADO DE MODULOS EN UNA LISTA DE SESSION Y VALIDAR CONTRA LA LISTA
            //    DataTable tabla;
            //    tabla = queryTable("SELECT * FROM app_MenuPerfil INNER JOIN app_MenuGrupo ON app_MenuPerfil.Id_ItemGrupo = app_MenuGrupo.Id_MenuItem WHERE (app_MenuPerfil.Id_Perfil = '" + idPerfil + "') AND (app_MenuGrupo.Enlace LIKE '%" + control + "%') AND (estado=1)");
            return queryTable("SELECT * FROM app_MenuPerfil INNER JOIN app_MenuGrupo ON app_MenuPerfil.Id_ItemGrupo = app_MenuGrupo.Id_MenuItem WHERE (app_MenuPerfil.Id_Perfil = '" + idPerfil + "') AND (estado=1)");
            //    if ((tabla.Rows.Count > 0) || (control == "F_salir") || (idPerfil == 0))
            //        return true;
            //    else
            //        return false;
            //}
            //else
            //    return true;

        }

        public DataTable consultaAccionesNoPermitidas(int ID_perfiFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_perfiFK", ID_perfiFK);
            return procedureTable("app_AccionesNoPermitidas", true, param);

        }

        #endregion

        #region PERFILES

        public DataTable perConsultaGeneral(string per_estado)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@per_estado", per_estado);
            return procedureTable("per_Consultar", true, param);
        }

        public DataTable perConsultaXID(int ID_perfil)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_perfil", ID_perfil);
            return procedureTable("per_ConsultaXID", true, param);
        }


        public DataTable EditarPerfiles(string ID_perfil, string per_nombre, string per_nivel, string per_proceso, string per_correoProceso, bool per_estado, string ID_usuarioActualizaFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@ID_perfil", ID_perfil);
            param.Add("@per_nombre", per_nombre);
            param.Add("@per_nivel",per_nivel);
            param.Add("@per_proceso",per_proceso);
            param.Add("@per_correoProceso",per_correoProceso);
            param.Add("@per_estado",per_estado);
            param.Add("@ID_usuarioActualizaFK",ID_usuarioActualizaFK);
            
            return procedureTable("per_Editar", true, param);
        }

        public DataTable InsertarPerfiles(string per_nombre, string per_nivel, string per_proceso, string per_correoProceso, string ID_usuarioRegistraFK)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@per_nombre", per_nombre);
            param.Add("@per_nivel", per_nivel);
            param.Add("@per_proceso", per_proceso);
            param.Add("@per_correoProceso", per_correoProceso);
            param.Add("@ID_usuarioRegistraFK", ID_usuarioRegistraFK);

            return procedureTable("per_Insertar", true, param);
        }

        #endregion

        #region MODULOS
        public DataTable ModulosConsultar()
        {
            return procedureTable("app_ConsultaModulos", false );
        }

        public DataTable ModuloXPerfilInsertar(string Id_Perfil, string Nombre_Perfil, string Id_ItemGrupo)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@Id_Perfil", Id_Perfil);
            param.Add("@Nombre_Perfil", Nombre_Perfil);
            param.Add("@Id_ItemGrupo", Id_ItemGrupo);

            return procedureTable("app_ModuloXPerfilInsertar", true, param);
        }

        public void ModuloXPerfilEliminar(string Id_Perfil)
        {
            Hashtable param = new Hashtable(1);
            param.Add("@Id_Perfil", Id_Perfil);

            procedureTable("app_ModuloXPerfilEliminar", true, param);
        }

        #endregion
    }
}
