using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    string itemSubmenu = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = System.Configuration.ConfigurationManager.AppSettings["nameSite"].ToString();
        if (Session["nombreUsuario"] != null)
        {
            CargarMenu();
            Datos.DUsuarios dUsuario = new Datos.DUsuarios();
            Negocio.NUsuario nUsuario = new Negocio.NUsuario();
            List<string> MenuXPerfil = (List<string>)Session["MenuXPerfil"];

            if ((Session["per_nivel"].ToString() != "0") && (nUsuario.AutorizarModulo(MenuXPerfil, HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1]) == false))
            {
                Session["noAcceso"] = "No tiene permisos de acceso a este modulo";
                Response.Redirect("~/Web/Default.aspx");
            }
        }
        else
        {
            System.Web.Security.FormsAuthentication.SignOut();
            System.Web.Security.FormsAuthentication.RedirectToLoginPage();
        }
    }

    void CargarMenu()
    {
        Negocio.NMenu menu = new Negocio.NMenu();
        DataTable tabla;
        tabla = menu.consultaPadresImagen(Session["ID_perfilFK"].ToString());
        if (tabla.Rows.Count > 0)
        {
            rptMenuLateral.DataSource = tabla;
            rptMenuLateral.DataBind();
        }
    }

    public string cargarSubmenuBoos(Int32 Id_MenuItem)
    {
        string subMenu = "";
        if (Session["ID_perfilFK"] != null)
        {
            DataTable tabla;
            Negocio.NMenu menu = new Negocio.NMenu();
            tabla = menu.consultaSubMenu(Id_MenuItem, Session["ID_perfilFK"].ToString());

            if (tabla.Rows.Count > 0)
            {
                for (int i = 0; i <= tabla.Rows.Count - 1; i++)
                {
                    subMenu += "<li><a class='menu' href='" + ResolveClientUrl("~/Web/") + tabla.Rows[i]["Enlace"] + "'>" + tabla.Rows[i]["Nombre_Item"] + "</a></li>";
                }
            }
        }
        return subMenu;
    }

    public string CargarSubmenu(Int32  Id_MenuItem ) 
    {
        DataTable tabla;
        string subMenu = "";
        Negocio.NMenu menu = new Negocio.NMenu();
        if (Session["ID_perfilFK"] != null)
        {
            tabla = menu.consultaSubMenu(Id_MenuItem, Session["ID_perfilFK"].ToString());

            if (tabla.Rows.Count > 0)
            {
                subMenu += "<ul>";
                for (int i = 0; i <= tabla.Rows.Count - 1; i++)
                {
                    if (tabla.Rows[i]["Tipo"].ToString() == "interno")
                        subMenu += "<li><a class='menu' href='" + ResolveClientUrl("~/Web/") + tabla.Rows[i]["Enlace"] + "'>" + tabla.Rows[i]["Nombre_Item"] + "</a></li>";
                    else
                        subMenu += "<li><a class='menu' href='" + tabla.Rows[i]["Enlace"] + "' target='_blank'>" + tabla.Rows[i]["Nombre_Item"] + "</a></li>";

                }
                subMenu += "</ul>";
            }
        }
        return subMenu;
    }
    public string CargarSubmenuLateral(Int32 Id_MenuItem)
    {
        DataTable tabla;
        string subMenu = "";
        string classActive = "";
        Negocio.NMenu menu = new Negocio.NMenu();
        if (Session["ID_perfilFK"] != null)
        {
            tabla = menu.consultaSubMenu(Id_MenuItem, Session["ID_perfilFK"].ToString());

            if (tabla.Rows.Count > 0)
            {
                subMenu += "<ul class='nav-pills nav-stacked child_menu' style='list-style-type:none; '>";
                for (int i = 0; i <= tabla.Rows.Count - 1; i++)
                {
                    subMenu += "<li><a href='" + ResolveClientUrl("~/Web/") + tabla.Rows[i]["Enlace"] + "'>" + tabla.Rows[i]["Nombre_Item"] + "</a></li>";
                    if ((classActive=="") &&( HttpContext.Current.Request.Url.AbsolutePath == "/Web/" + tabla.Rows[i]["Enlace"].ToString()))
                        classActive = "class='active'";
                }
                subMenu += "</ul>";
            }
        }
        itemSubmenu = subMenu;
        return classActive;

    }

    public string ConsultaItems()
    {
        return itemSubmenu;
    }
    public string ValidaClassActive()
    {
        return "";
    }

    protected void ibtSalir_Click(object sender, ImageClickEventArgs e)
    {
        System.Web.Security.FormsAuthentication.SignOut();
        System.Web.Security.FormsAuthentication.RedirectToLoginPage();
    }

}


