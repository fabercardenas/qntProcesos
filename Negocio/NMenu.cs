using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Negocio
{
    public class NMenu : Datos.DSQL 
    {
    public DataTable consultaItem(string idItem)
        {
          return queryTable("SELECT * FROM app_MenuGrupo WHERE Id_MenuItem='" + idItem + "'");
        }

        //CONSULTA LOS SUBMENOS O HIJOS ASOCIADOS A UN PADRE
    public DataTable consultaSubMenu(Int32  idPadre ,string   idPerfil )
    {
        if (idPerfil == "0")
        {
            return queryTable("SELECT * FROM app_MenuGrupo " + 
                       "WHERE (app_MenuGrupo.Id_PadreItem = '" + idPadre + "') AND app_MenuGrupo.Estado=1 " +
                       "ORDER BY app_MenuGrupo.orden ASC");
        }
        else
        {
            return queryTable("SELECT * FROM app_MenuGrupo " +
                    "INNER JOIN  app_MenuPerfil ON app_MenuPerfil.Id_ItemGrupo = app_MenuGrupo.Id_MenuItem  " +
                    "WHERE (app_MenuGrupo.Id_PadreItem = '" + idPadre + "') " +
                    "AND (app_MenuPerfil.Id_Perfil = '" + idPerfil + "') AND (app_MenuGrupo.Estado=1) " +
                    "ORDER BY app_MenuGrupo.orden ASC");
        }
    }
                
    //CONSULTA LOS ITEM A LOS QUE TENGA PERSMISO Y TENGAN IMAGEN PARA EL MENU SUPERIOR, ROOT TRAE TODO
    public DataTable  consultaPadresImagen(string  idPerfil ) 
    {
        if (idPerfil == "0")
            return queryTable("SELECT * FROM app_MenuGrupo WHERE app_MenuGrupo.Id_PadreItem IS NULL AND app_MenuGrupo.Estado=1 AND app_MenuGrupo.Imagen IS NOT NULL ORDER BY app_MenuGrupo.orden ASC");
        else
            return queryTable("SELECT * FROM app_MenuGrupo INNER JOIN app_MenuPerfil ON app_MenuGrupo.Id_MenuItem = app_MenuPerfil.Id_ItemGrupo WHERE (app_MenuGrupo.Id_PadreItem IS NULL) AND (app_MenuGrupo.Estado = 1)  AND (app_MenuGrupo.Imagen IS NOT NULL) AND (app_MenuPerfil.Id_Perfil = '" + idPerfil + "') ORDER BY app_MenuGrupo.orden ASC");
    }

    //CONSULTA TODOS LOS ITEM A QUE TIENE ACCESO UN PEFIL. SINO TIENE ACCESO DEVUELVE FALSE
    public bool  consultaMenuXPerfil(string  control , Int32  idPerfil )
    {
        DataTable  tabla ;
        tabla = queryTable("SELECT * FROM app_MenuPerfil INNER JOIN app_MenuGrupo ON app_MenuPerfil.Id_ItemGrupo = app_MenuGrupo.Id_MenuItem WHERE (app_MenuPerfil.Id_Perfil = '" + idPerfil + "') AND (app_MenuGrupo.Enlace LIKE '%" +  control + "%') AND (estado=1)");
        if ((tabla.Rows.Count > 0) || (control == "F_salir") || (idPerfil == 0))
            return true;
        else
            return false;
    }
        
    }
}
