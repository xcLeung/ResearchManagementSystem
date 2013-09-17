using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.ProjectInfo
{
    public partial class RecommendInfo : System.Web.UI.Page
    {
        protected String ProjectID;
        List<Models.DB.RecommendedInfo> model;
        protected List<Models.DB.CupProjectModel> CupModellist;
        protected Models.DB.Match Match = new Models.DB.Match();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Request["ProjectID"] == null)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            ProjectID =Request["ProjectID"];
            CupModellist = BLL.CupProjectModel.FindByInt(ProjectID, "ID");
           
            if (CupModellist.Count <= 0 )
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            Match = BLL.Match.SelectOne(Convert.ToInt32(CupModellist[0].MatchID));
            if (Match == null)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            model = BLL.RecommendInfo.SelectByProjectId(ProjectID);
            if (model.Count > 0)
            {
                Response.Redirect(String.Format("~/Web/CupProjectModel/CupInfoEdit/RecommendInfo.aspx?ProjectID={0}",ProjectID));
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