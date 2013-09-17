using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.InnovationProjectModel
{
    public partial class MoreProjectInfo : System.Web.UI.Page
    {
        protected String ProjectID;
        protected String MatchID;
        protected Models.DB.InnovationProjectModel Innovation = new Models.DB.InnovationProjectModel();
        protected Models.DB.InnovationDeclarantInfo Declarant = new Models.DB.InnovationDeclarantInfo();
        protected List<Models.DB.InnovationTeamMember> TeamMembers = new List<Models.DB.InnovationTeamMember>();
        protected List<Models.DB.TutorInfo> Tutors = new List<Models.DB.TutorInfo>();
        protected Models.DB.StudentInfoModel Student = new Models.DB.StudentInfoModel();
        protected Models.DB.InnovationWorksInfo WorkInfo = new Models.DB.InnovationWorksInfo();
        protected Models.DB.Match Match = new Models.DB.Match();

        protected List<Models.DB.InnovationProjectModel> Innovations = new List<Models.DB.InnovationProjectModel>();
        protected List<Models.DB.InnovationWorksInfo> Works = new List<Models.DB.InnovationWorksInfo>();

        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            initData();
        }

        private void initData()
        {
            if (String.IsNullOrEmpty(Request["ProjectID"]) || String.IsNullOrEmpty(Request["MID"]))
            {
                Response.Redirect("Default.aspx");
            }
            ProjectID = Request["ProjectID"].ToString();
            MatchID = Request["MID"].ToString();
            Match = BLL.Match.SelectOne(Convert.ToInt32(MatchID));
            Innovations = BLL.InnovationProjectModel.FindByInt(ProjectID, "Id");
            if (Innovations.Count > 0)
            {
                Innovation = Innovations[0];
            }
            else
            {
                Innovation = new Models.DB.InnovationProjectModel();
            }
            Declarant = BLL.InnovationDeclarantInfo.SelectOne(ProjectID);
            TeamMembers = BLL.InnovationTeamMember.SelectByProjectId(ProjectID);
            Tutors = BLL.Tutors.SelectByProjectId(ProjectID);
            Works = BLL.InnovationWorksInfo.FindByInt(ProjectID, "ProjectID");
            if (Works.Count > 0)
            {
                WorkInfo = Works[0];
            }
            else
            {
                WorkInfo = new Models.DB.InnovationWorksInfo();
            }
            Student = BLL.StudentInfoModel.SelectOneByUserId(Innovation.UserID.ToString());
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