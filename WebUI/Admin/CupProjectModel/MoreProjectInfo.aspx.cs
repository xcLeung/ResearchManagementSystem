using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.CupProjectModel
{
    public partial class MoreProjectInfo : System.Web.UI.Page
    {
        protected String ProjectID;
        protected String MatchID;
        protected Models.DB.CupProjectModel Project;
        protected Models.DB.CupDeclarantInfo Declarant;
        protected List<Models.DB.CupTeamMemberInfo> TeamMember;
        protected List<Models.DB.RecommendedInfo> Recommender;
        protected Models.DB.StudentInfoModel Student;
        protected Models.DB.CupWorksInfo WorkInfo = new Models.DB.CupWorksInfo();
        protected Models.DB.CupWorksSurvey SurveyInfo = new Models.DB.CupWorksSurvey();
        protected Models.DB.CupWorksInvention InventionInfo = new Models.DB.CupWorksInvention();
        protected Models.DB.Match Match;
        protected List<Models.DB.CupWorksInfo> WorkInfoList;
        protected List<Models.DB.CupWorksSurvey> SurveyInfoList;
        protected List<Models.DB.CupWorksInvention> InventionInfoList;
        protected List<Models.DB.CupProjectModel> Projects;


        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            if (String.IsNullOrEmpty(Request["ProjectID"]) || String.IsNullOrEmpty(Request["MID"]))
            {
                Response.Redirect("Default.aspx");
            }
            ProjectID = Request["ProjectID"].ToString();
            MatchID = Request["MID"].ToString();
            Match = BLL.Match.SelectOne(Convert.ToInt32(MatchID));
            Projects = BLL.CupProjectModel.FindByInt(ProjectID, "ID");
            if (Projects.Count > 0)
            {
                Project = Projects[0];
            }
            else
            {
                Project = new Models.DB.CupProjectModel();
            }
            Declarant = BLL.CupDeclarantInfo.SelectOneByProjectID(ProjectID);
            TeamMember = BLL.CupTeamMemberInfo.SelectByProjectId(ProjectID);
            Recommender = BLL.RecommendInfo.SelectByProjectId(ProjectID);
            Student = BLL.StudentInfoModel.SelectOneByUserId(Project.UserID.ToString());
            if (Project.Category == "自然科学类学术论文")
            {
                WorkInfoList = BLL.CupWorksInfo.FindByInt(ProjectID, "ProjectID");
                if (WorkInfoList.Count > 0)
                {
                    WorkInfo = WorkInfoList[0];
                }
                else
                {
                    WorkInfo = new Models.DB.CupWorksInfo();
                }
            }
            else if (Project.Category == "哲学社会科学类社会调查报告和学术论文")
            {
                SurveyInfoList = BLL.CupWorksSurvey.FindByInt(ProjectID, "ProjectID");
                if (SurveyInfoList.Count > 0)
                {
                    SurveyInfo = SurveyInfoList[0];
                }
                else
                {
                    SurveyInfo = new Models.DB.CupWorksSurvey();
                }
            }
            else if (Project.Category == "科技发明制作")
            {
                InventionInfoList = BLL.CupWorksInvention.FindByInt(ProjectID, "ProjectID");
                if (InventionInfoList.Count > 0)
                {
                    InventionInfo = InventionInfoList[0];
                }
                else
                {
                    InventionInfo = new Models.DB.CupWorksInvention();
                }
            }
        }

        private void accessControl()
        {
            if (Session["user"] == null || Session["role"] == null)
            {
                Response.Redirect(ResolveUrl("~/Web/Login/Default.aspx"));
            }
            String UserID = Session["user"].ToString();
            String RoleID = Session["role"].ToString();
            LoginRole = BLL.Role.SelectRoleOne(Convert.ToInt32(RoleID));
            LoginAdmin = BLL.AdminModel.SelectAdminModelByUserID(UserID);

          

        }
    }
}