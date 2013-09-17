using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.ProjectInfo
{
    public partial class Remark : System.Web.UI.Page
    {
        protected String ProjectID;
        protected List<Models.DB.CupProjectModel> CupModelList;
        protected Models.DB.Match Match = new Models.DB.Match();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Request["ProjectID"] == null)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            ProjectID = Request["ProjectID"].ToString();
            CupModelList = BLL.CupProjectModel.FindByInt(ProjectID, "ID");
            if (CupModelList.Count <= 0 )
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            Match = BLL.Match.SelectOne(Convert.ToInt32(CupModelList[0].MatchID));
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