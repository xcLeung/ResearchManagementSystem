using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.CupProjectModel
{
    public partial class ProjectResult : System.Web.UI.Page
    {
        protected int current_page = 1;
        protected int page_count = 1;
        protected int page_size = 10;
        protected string MatchID = "";

        protected Models.DB.Match Match;
        protected List<Models.DB.CupProjectModel> Projects;
        protected List<Models.DB.StudentInfoModel> Students = new List<Models.DB.StudentInfoModel>();

        protected List<Models.DB.InnovationProjectModel> Innovations;

        protected List<ProjectRank> rankList = new List<ProjectRank>();
        protected List<Models.DB.CheckRecord> CheckRecords = new List<Models.DB.CheckRecord>();

        protected Models.DB.ProjectScore ProjectScore;

        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            initData();
        }

        private void initData()
        {

            if (string.IsNullOrEmpty(Request["MID"]))
            {
                Response.Redirect("../Match/Default.aspx");
            }
            try
            {
                Convert.ToInt32(Request["MID"]);
                MatchID = Request["MID"];
            }
            catch
            {
                Response.Redirect("../Match/Default.aspx");
            }
            // 获取当前页
            if (!string.IsNullOrEmpty(Request["page"]))
            {
                try { current_page = Convert.ToInt32(Request["page"]); }
                catch { }

            }

            Match = BLL.Match.SelectOne(Convert.ToInt32(MatchID));

            if (Match.MatchModel == 1)
            {
                // 获取总页数
                Projects = BLL.CupProjectModel.FindByInt(MatchID, "MatchID");
                ProjectRank rank;

                for (int i = 0; i < Projects.Count; i++)
                {
                    if (!BLL.ProjectScore.HasRecord(Projects[i].ID.ToString(), Match.MatchModel.ToString()))
                    {
                        Projects.RemoveAt(i);
                        i--;
                        continue;
                    }
                    else
                    {
                        rank = new ProjectRank();
                        rank.index = i;
                        rank.score = ProjectAvgScore(Projects[i].ID);
                        rankList.Add(rank);
                        Students.Add(BLL.StudentInfoModel.SelectOneByUserId(Projects[i].UserID + ""));
                        CheckRecords.Add(BLL.CheckRecord.FindOne(Projects[i].ID.ToString(), Match.MatchModel.ToString()));
                    }
                }
                rankList.Sort(delegate(ProjectRank a, ProjectRank b) { return b.score.CompareTo(a.score); });

                page_count = (int)Math.Ceiling(Projects.Count / (double)page_size);

                if (current_page > page_count) current_page = page_count;
                if (current_page <= 0) current_page = 1;

            }
            else if(Match.MatchModel==2)
            {
                //科技创新
                Innovations = BLL.InnovationProjectModel.FindByInt(MatchID, "MatchID");
                ProjectRank rank;
                for (int i = 0; i < Innovations.Count; i++)
                {
                    if (!BLL.ProjectScore.HasRecord(Innovations[i].Id.ToString(), Match.MatchModel.ToString()))
                    {
                        Innovations.RemoveAt(i);
                        i--;
                        continue;
                    }
                    else
                    {
                        rank = new ProjectRank();
                        rank.index = i;
                        rank.score = ProjectAvgScore(Innovations[i].Id);
                        rankList.Add(rank);
                        Students.Add(BLL.StudentInfoModel.SelectOneByUserId(Innovations[i].UserID.ToString()));
                        CheckRecords.Add(BLL.CheckRecord.FindOne(Innovations[i].Id.ToString(), Match.MatchModel.ToString()));
                    }
                }
                rankList.Sort(delegate(ProjectRank a, ProjectRank b) { return b.score.CompareTo(a.score); });

                page_count = (int)Math.Ceiling(Innovations.Count / (double)page_size);

                if (current_page > page_count) current_page = page_count;
                if (current_page <= 0) current_page = 1;


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



        protected double ProjectAvgScore(int ProjectID)
        {
            List<Models.DB.ProjectScore> ScoreList = new List<Models.DB.ProjectScore>();
            ScoreList = BLL.ProjectScore.FindProjectScore(ProjectID.ToString(), Match.MatchModel.ToString());
            int sum=0;
            for (int i = 0; i < ScoreList.Count; i++)
            {
                sum += ScoreList[i].Score;
            }
            if (ScoreList.Count <= 0)
            {
                return 0;
            }
            return sum*1.0 / ScoreList.Count;
        }

    }


    public class ProjectRank 
    {
        public int index {get; set;}
        public double score { get; set;}

       

    }

}