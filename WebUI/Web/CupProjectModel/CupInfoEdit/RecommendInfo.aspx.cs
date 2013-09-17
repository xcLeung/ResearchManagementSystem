using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.CupProjectModel.CupInfoEdit
{
    public partial class RecommendInfo : System.Web.UI.Page
    {
        protected List<Models.DB.CupProjectModel> CupModellist = new List<Models.DB.CupProjectModel>();
        protected String ProjectID;
        protected List<Models.DB.RecommendedInfo> Recommendedlist = new List<Models.DB.RecommendedInfo>();
        protected Models.DB.Match Match = new Models.DB.Match();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Request["ProjectID"] == null)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            ProjectID = Request["ProjectID"];
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
            Recommendedlist = BLL.RecommendInfo.SelectByProjectId(ProjectID);
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