using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.CupProjectModel
{
    public partial class Default : System.Web.UI.Page
    {
        protected List<Models.DB.CupProjectModel> Projects;
        protected List<int> TeamMemberCount = new List<int>();
        protected List<Models.DB.CupDeclarantInfo> Declarants = new List<Models.DB.CupDeclarantInfo>();
        protected List<Models.DB.StudentInfoModel> Students = new List<Models.DB.StudentInfoModel>();
        protected List<List<Models.DB.RecommendedInfo>> Recommenders = new List<List<Models.DB.RecommendedInfo>>();
        protected int current_page = 1;
        protected int page_count = 1;
        protected int page_size = 30;
        protected string MatchID = "";


        protected Models.DB.Match Match = new Models.DB.Match();
        protected List<Models.DB.CheckRecord> CheckRecord = new List<Models.DB.CheckRecord>();

        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            initData();
        }
        private void initData()
        {

            if (string.IsNullOrEmpty(Request["match"]))
            {
                Response.Redirect("Default.aspx");
            }
            try
            {
                Convert.ToInt32(Request["match"]);
                MatchID = Request["match"];
            }
            catch
            {
                Response.Redirect("Default.aspx");
            }
            // 获取当前页
            if (!string.IsNullOrEmpty(Request["page"]))
            {
                try { current_page = Convert.ToInt32(Request["page"]); }
                catch { }

            }
            Match = BLL.Match.SelectOne(Convert.ToInt32(Request["match"]));
            // 获取总页数
            page_count = (int)Math.Ceiling(BLL.CupProjectModel.CountByMatchId(Request["match"]) / (double)page_size);
            if (current_page > page_count) current_page = page_count;
            if (current_page <= 0) current_page = 1;
            Projects = BLL.CupProjectModel.SelectOnePage(current_page, page_size, Request["match"]);
            for (int i = 0; i < Projects.Count; i++)
            {
                TeamMemberCount.Add((int)BLL.CupTeamMemberInfo.CountByProjectID(Projects[i].ID + ""));
                Declarants.Add(BLL.CupDeclarantInfo.SelectOne(Projects[i].ID + ""));
                Students.Add(BLL.StudentInfoModel.SelectOneByUserId(Projects[i].UserID + ""));
                Recommenders.Add(BLL.RecommendInfo.SelectByProjectId(Projects[i].ID + ""));
                CheckRecord.Add(BLL.CheckRecord.FindOne(Projects[i].ID.ToString(), Match.MatchModel.ToString()));
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