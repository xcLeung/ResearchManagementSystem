using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelCreate
{
    public partial class PdfMaker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ProjectID = Request["ProjectID"];
            Response.Redirect("../../MyWorks/Maker.aspx?project=" + ProjectID + "&model=InnovationProjectModel");
        }
    }
}