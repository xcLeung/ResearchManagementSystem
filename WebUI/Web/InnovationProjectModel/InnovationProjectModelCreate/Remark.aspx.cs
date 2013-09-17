using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelCreate
{
    public partial class Remark : System.Web.UI.Page
    {

        protected String ProjectID;
        protected List<Models.DB.InnovationProjectModel> Projects = new List<Models.DB.InnovationProjectModel>();
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
            ProjectID = Request["ProjectID"].ToString();
            Projects = BLL.InnovationProjectModel.FindByInt(ProjectID, "Id");
            if (Projects.Count <= 0 )
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            Match = BLL.Match.SelectOne(Convert.ToInt32(Projects[0].MatchID));
            if (Match == null)
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