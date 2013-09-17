using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelEdit
{
    public partial class WorkInfo : System.Web.UI.Page
    {

        protected String ProjectID;
        protected List<Models.DB.InnovationProjectModel> Projects = new List<Models.DB.InnovationProjectModel>();
        protected List<Models.DB.InnovationWorksInfo> WorksInfo = new List<Models.DB.InnovationWorksInfo>();
        protected Models.DB.Match Match = new Models.DB.Match();
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
            ProjectID = Context.Request["ProjectID"].ToString();
            Projects = BLL.InnovationProjectModel.FindByInt(ProjectID, "Id");
            if (Projects.Count <= 0)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            Match = BLL.Match.SelectOne(Convert.ToInt32(Projects[0].MatchID));
            WorksInfo = BLL.InnovationWorksInfo.FindByInt(ProjectID, "ProjectID");
            if (WorksInfo.Count <= 0 || Match==null)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            
        }

        protected Boolean isDeadLine(System.DateTime DeadLine)
        {
            if (System.DateTime.Compare(System.DateTime.Now, DeadLine) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}