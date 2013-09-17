using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.Match
{
    public partial class Default : System.Web.UI.Page
    {
        protected List<Models.DB.Match> Matchs;
        protected List<int> JudgeCount=new List<int>();
        protected List<Models.DB.MatchModel> MatchModels = new List<Models.DB.MatchModel>();
        protected int current_page = 1;
        protected int page_count = 1;
        protected int page_size = 30;

        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            initData();
        }
        private void initData()
        {
            // 获取当前页
            if (!string.IsNullOrEmpty(Request["page"]))
            {
                current_page = Convert.ToInt32(Request["page"]);
            }

            if (LoginRole.Name == "ScauAdmin")
            {
                // 获取总页数
                page_count = (int)Math.Ceiling(BLL.Match.Count() / (double)page_size);
                if (current_page <= 0) current_page = 1;
                if (current_page > page_count) current_page = page_count;
                Matchs = BLL.Match.SelectOnePage(current_page, page_size);
                for (int i = 0; i < Matchs.Count; i++)
                {
                    JudgeCount.Add((int)BLL.MatchJuryRelation.CountJudges(Matchs[i].ID + ""));
                    MatchModels.Add(BLL.MatchModel.SelectOne(Matchs[i].MatchModel));
                }
            }
            else if (LoginRole.Name == "CollegeAdmin")
            {
                page_count = (int)Math.Ceiling(BLL.Match.CountByCollege(LoginAdmin.College) / (double)page_size);
                if (current_page <= 0) current_page = 1;
                if (current_page > page_count) current_page = page_count;
                Matchs = BLL.Match.SelectOnePageByCollege(current_page,page_size,LoginAdmin.College);
                for (int i = 0; i < Matchs.Count; i++)
                {
                    JudgeCount.Add((int)BLL.MatchJuryRelation.CountJudges(Matchs[i].ID + ""));
                    MatchModels.Add(BLL.MatchModel.SelectOne(Matchs[i].MatchModel));
                }
            }
        }
        protected string getHref(string Model)
        {
            return "../"+Model.Substring(3)+"/Default.aspx?match=";
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

        protected String convertMatchModelName(String tableName)
        {
            if (tableName == "Tb_CupProjectModel")
            {
                return "挑战杯模型";
            }
            else if (tableName == "Tb_InnovationProjectModel")
            {
                return "科技创新模型";
            }
            else
            {
                return "";
            }
        }
    }
}