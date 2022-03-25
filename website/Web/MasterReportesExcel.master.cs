using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_MasterReportesExcel : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        imgLogoEmpresa.Attributes.Add("margin-top", "-25px");
    }
}
