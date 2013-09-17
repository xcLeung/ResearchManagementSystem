using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelEdit
{
    

    public partial class Default : System.Web.UI.Page
    {

        protected String ProjectID;
        protected List<Models.DB.InnovationProjectModel> Projects = new List<Models.DB.InnovationProjectModel>();

        protected void Page_Load(object sender, EventArgs e)
        {
            initData();
        }
    
    
        private void initData()
        {
            if (Context.Request["ProjectID"] == null)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            ProjectID = Request["ProjectID"];
            Projects = BLL.InnovationProjectModel.FindByInt(ProjectID, "Id");
            if (Projects.Count <= 0)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
        }

    }
}