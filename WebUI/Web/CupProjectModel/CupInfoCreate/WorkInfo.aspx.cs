using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.ProjectInfo
{
    public partial class WorkInfo : System.Web.UI.Page
    {
        protected String ProjectID;
        protected List<Models.DB.CupProjectModel> CupModellist;
        protected List<Models.DB.CupWorksInfo> WorkModel;
        protected List<Models.DB.CupWorksSurvey> SurveyModel;
        protected List<Models.DB.CupWorksInvention> InventionModel;
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
            if ((WorkModel = BLL.CupWorksInfo.FindByInt(ProjectID, "ProjectID")).Count > 0)
            {
                 Response.Redirect(String.Format("~/Web/CupProjectModel/CupInfoEdit/WorkInfo.aspx?ProjectID={0}",  WorkModel[0].ProjectID));
                
            }else if((SurveyModel=BLL.CupWorksSurvey.FindByInt(ProjectID,"ProjectID")).Count>0)
            {
                Response.Redirect(String.Format("~/Web/CupProjectModel/CupInfoEdit/WorkInfo.aspx?ProjectID={0}", SurveyModel[0].ProjectID));
            }
            else if ((InventionModel = BLL.CupWorksInvention.FindByInt(ProjectID, "ProjectID")).Count > 0)               
            {
                Response.Redirect(String.Format("~/Web/CupProjectModel/CupInfoEdit/WorkInfo.aspx?ProjectID={0}", InventionModel[0].ProjectID));
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