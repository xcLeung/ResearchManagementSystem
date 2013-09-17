using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.ProjectInfo
{
    public partial class PdfMaker : System.Web.UI.Page
    {
        protected int ProjectID;


        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Convert.ToInt32(Request["ProjectID"]);
            Response.Redirect("../../MyWorks/Maker.aspx?project=" + ProjectID + "&model=CupProjectModel");
        }
    }
}