using System;
using System.Collections.Generic;
using System.Web;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Linq;

namespace Negocio
{
    public class NUsuario
    {
      Datos.DUsuarios DUsuario = new Datos.DUsuarios();
      #region USUARIOS
      
      private string CreaPasswordEncriptado(string password)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public string CrearEncriptadoSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        public DataTable Autenticar(string userName, string userPassword)
      {
          DataTable tablaUsuario;
          tablaUsuario = DUsuario.ConsultarXUserName(userName);
          if ((tablaUsuario.Rows.Count > 0) && (tablaUsuario.Rows[0]["usr_estado"].ToString() == "1"))
          {
              //    'Si vienen filas en el table, evaluo, sino , devuelvo que el user no existe
              string claveEncriptada = tablaUsuario.Rows[0]["usr_clave"].ToString();
              string passwordHash = this.CreaPasswordEncriptado(userPassword);

              if (passwordHash == claveEncriptada)
                  return tablaUsuario;
              else 
                  return null;
          }
          else { return null; }

      }

      public void ActualizaUltimoIngreso(string ID_usuario)
      {
          DUsuario.ActualizaUltimoIngreso(ID_usuario);
      }

      public string ActualizaClave(string ID_usuario,string usr_userName, string claveActual, string claveNueva)
      {
          DataTable dataUsuario;
          dataUsuario = Autenticar(usr_userName, claveActual);

        if ((dataUsuario!= null) && (dataUsuario.Rows.Count>0))
        {
            //Cambiar a la nueva clave
            try 
	        {	        
                DUsuario.ActualizaClave(ID_usuario, CreaPasswordEncriptado(claveNueva));
                return  "La clave ha sido actualizada con éxito";
	        }
	        catch (Exception)
	        {
              return "Los datos ingresados no son validos en el sistema<br>La clave no se actualizo.";
	        }
        }
        else
        {
            return "La clave actual no coincide";
        }
      }

      public void Editar(string ID_usuario, string usr_documento, Int16 ID_perfilFK, Int16 ID_oficinaFK, string ID_empresaFK, string usr_nombre1, string usr_nombre2, string usr_apellido1, string usr_apellido2,
          string usr_mail, string usr_telefono, string usr_direccion, string usr_userName, string usr_clave, bool  usr_estado, bool restaurar = false )
      {
          if (restaurar== true)
              usr_clave = this.CreaPasswordEncriptado(usr_userName);

          DUsuario.Editar(ID_usuario, usr_documento, ID_perfilFK, ID_oficinaFK,ID_empresaFK, usr_nombre1, usr_nombre2, usr_apellido1, usr_apellido2, usr_mail, usr_telefono, usr_direccion, usr_userName, usr_clave, Convert.ToInt16(usr_estado));
      }

      public string Insertar( string usr_documento, Int16 ID_perfilFK, Int16 ID_oficinaFK,string ID_empresaFK, string usr_nombre1, string usr_nombre2, string usr_apellido1, string usr_apellido2,
                            string usr_mail, string usr_telefono, string usr_direccion, string usr_userName, string usr_clave)
      {
          DataTable tbUser = DUsuario.ConsultarXUserName(usr_userName);
          if (tbUser.Rows.Count == 0)
          {
              usr_clave = this.CreaPasswordEncriptado(usr_userName);
              DUsuario.Insertar(usr_documento, ID_perfilFK, ID_oficinaFK,ID_empresaFK, usr_nombre1, usr_nombre2, usr_apellido1, usr_apellido2, usr_mail, usr_telefono, usr_direccion, usr_userName, usr_clave);
              return "Usuario creado con éxito";
          }
          else
              return "El usuario indicado ya existe";
      }
    
      public DataTable ConsultaGeneral(Int16 usr_estado)
        {
            return DUsuario.ConsultaGeneral(usr_estado);
        }

      public List<string> ConsultaMenuXPerfil(int ID_perfilFK)
      {
          DataTable dtbMenusXUsuario = DUsuario.consultaMenuXPerfil(ID_perfilFK);
          List<string> pagesForRole = new List<string>();
          string[] paginaSinParmetro ;

          pagesForRole.Add("Default.aspx");
          pagesForRole.Add("F_CambiarClave.aspx");
          pagesForRole.Add("F_Salir.aspx");
          
          foreach (DataRow dr in dtbMenusXUsuario.Rows)
          {
              paginaSinParmetro = dr["Enlace"].ToString().Split('?');
              pagesForRole.Add(paginaSinParmetro[0]);
          }
          
          return pagesForRole;
      }

        public DataTable consultaAccionesNoPermitidas(int ID_perfiFK)
        {
            return DUsuario.consultaAccionesNoPermitidas(ID_perfiFK);
        }

      public bool AutorizarModulo(List<string> menusXperfil, string modulo)
      {
          //return menusXperfil.Any(x => x.Split(new char[] { '/' }).Contains(modulo));
          return true; //------------>PIIIIIIIIIIIIIIIIIIIIIIIIIILAS
          
          //if (menusXperfil.Contains(modulo))
          //{
          //    return true;
          //}
          //else
          //    return false;

      }

        #endregion

        #region PERFILES
        public DataTable perConsultaGeneral(string per_estado)
        {
            return DUsuario.perConsultaGeneral(per_estado);
        }

        public DataTable perConsultaXID(int ID_perfil)
      {
          return DUsuario.perConsultaXID(ID_perfil);
      }

        public void EditarPerfiles(string ID_perfil, string per_nombre, string per_nivel, string per_proceso, string per_correoProceso, bool per_estado, string ID_usuarioActualizaFK)
      {
           DUsuario.EditarPerfiles(ID_perfil, per_nombre, per_nivel, per_proceso, per_correoProceso, per_estado, ID_usuarioActualizaFK);
      }

        public DataTable InsertarPerfiles(string per_nombre, string per_nivel, string per_proceso, string per_correoProceso, string ID_usuarioRegistraFK)
        {
            return DUsuario.InsertarPerfiles(per_nombre, per_nivel, per_proceso, per_correoProceso, ID_usuarioRegistraFK);
        }
        #endregion

        #region MODULOS
        public DataTable ModulosConsultar()
        {
            return DUsuario.ModulosConsultar();
        }

        public DataTable ModulosConsultarXPerfil(int ID_perfilFK)
        {
            return DUsuario.consultaMenuXPerfil(ID_perfilFK);
        }

        public DataTable ModuloXPerfilInsertar(string Id_Perfil, string Nombre_Perfil, string Id_ItemGrupo)
        {
            return DUsuario.ModuloXPerfilInsertar(Id_Perfil, Nombre_Perfil, Id_ItemGrupo);
        }

        public void ModuloXPerfilEliminar(string Id_Perfil)
        {
            DUsuario.ModuloXPerfilEliminar(Id_Perfil);
        }
        #endregion
    }
}
