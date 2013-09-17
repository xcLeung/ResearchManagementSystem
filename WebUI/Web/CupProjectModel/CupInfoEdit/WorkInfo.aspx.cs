using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.CupProjectModel.CupInfoEdit
{
    public partial class WorkInfo : System.Web.UI.Page
    {
        protected List<Models.DB.CupProjectModel> CupModellist = new List<Models.DB.CupProjectModel>();
        protected String ProjectID;

        protected List<Models.DB.CupWorksInfo> WorkModel = new List<Models.DB.CupWorksInfo>();
        protected List<Models.DB.CupWorksSurvey> SurveyModel = new List<Models.DB.CupWorksSurvey>();
        protected List<Models.DB.CupWorksInvention> InventionModel = new List<Models.DB.CupWorksInvention>();
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
            if (CupModellist[0].Category == "自然科学类学术论文")
            {
                WorkModel = BLL.CupWorksInfo.FindByInt(ProjectID, "ProjectID");
                if (WorkModel.Count <= 0)
                {
                    Response.Redirect("../../Match/Default.aspx");
                }
            }
            else if (CupModellist[0].Category == "哲学社会科学类社会调查报告和学术论文")
            {
                SurveyModel = BLL.CupWorksSurvey.FindByInt(ProjectID, "ProjectID");
                if (SurveyModel.Count <= 0)
                {
                    Response.Redirect("../../Match/Default.aspx");
                }
            }
            else if (CupModellist[0].Category == "科技发明制作")
            {
                InventionModel = BLL.CupWorksInvention.FindByInt(ProjectID, "ProjectID");
                if (InventionModel.Count <= 0)
                {
                    Response.Redirect("../../Match/Default.aspx");
                }
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