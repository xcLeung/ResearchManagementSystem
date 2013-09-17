using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.CupProjectModel.CupInfoEdit
{
    public partial class Default : System.Web.UI.Page
    {
        protected String ProjectID;
        protected List<Models.DB.CupProjectModel> modellist;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Request["ProjectID"] == null)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            ProjectID = Request["ProjectID"];
            modellist = BLL.CupProjectModel.FindByInt(ProjectID,"ID");
            if (modellist.Count <= 0)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
        }
    }
}