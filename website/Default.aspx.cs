using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["noAcceso"] != null)
        {
            ltrMensaje.Text = Messaging.Error(Session["noAcceso"].ToString());
            Session["noAcceso"] = null;
        }
        if ((Session["per_nivel"] != null) && (Session["per_nivel"].ToString() == "50"))
            dvIndicadorEmpresaUsuaria.Visible = false;
        else
            dvIndicadorEmpresaUsuaria.Visible = false;
        
        
        
    }
}