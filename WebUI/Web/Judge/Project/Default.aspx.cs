using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.Judge.Project
{
    public partial class Default : System.Web.UI.Page
    {
        protected Models.DB.Match Match = new Models.DB.Match();
        protected Models.DB.CheckRecord CheckRecrod = new Models.DB.CheckRecord();

        protected List<Models.DB.CupProjectModel> Projects = new List<Models.DB.CupProjectModel>();
        protected List<int> TeamMemberCount = new List<int>();
        protected List<Models.DB.CupDeclarantInfo> Declarants = new List<Models.DB.CupDeclarantInfo>();
        protected List<Models.DB.StudentInfoModel> Students = new List<Models.DB.StudentInfoModel>();
        protected List<List<Models.DB.RecommendedInfo>> Recommenders = new List<List<Models.DB.RecommendedInfo>>();


        protected List<Models.DB.InnovationProjectModel> Innovations = new List<Models.DB.InnovationProjectModel>();
        protected List<Models.DB.InnovationDeclarantInfo> InovationDeclarants = new List<Models.DB.InnovationDeclarantInfo>();
        protected List<List<Models.DB.TutorInfo>> Tutors = new List<List<Models.DB.TutorInfo>>();
        protected Models.DB.InnovationWorksInfo InnovationWork = new Models.DB.InnovationWorksInfo();

        protected List<Models.DB.InnovationWorksInfo> list = new List<Models.DB.InnovationWorksInfo>();

        protected int current_page = 1;
        protected int page_count = 1;
        protected int page_size = 10;
        protected string MatchID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            initData();
        }

        private void initData()
        {

            if (string.IsNullOrEmpty(Request["MatchID"]))
            {
                Response.Redirect("../Match/Default.aspx");
            }
            MatchID = Request["MatchID"];
            if (!string.IsNullOrEmpty(Request["page"]))
            {
                current_page = Convert.ToInt32(Request["page"].ToString());
            }
            int matchid = 0;
            try
            {
                matchid = Convert.ToInt32(MatchID);
            }
            catch
            {
                Response.Redirect("../Match/Default.aspx");
            }
            Match = BLL.Match.SelectOne(matchid);

            if (Match.MatchModel == 1)
            {

                Projects = BLL.CupProjectModel.FindByInt(MatchID, "MatchID");

               
                for (int i = 0; i < Projects.Count; i++)
                {
                    CheckRecrod = BLL.CheckRecord.FindOne(Projects[i].ID.ToString(), Match.MatchModel.ToString());
                   if (CheckRecrod==null || CheckRecrod.AfterStatus != "初审通过")
                    {
                        Projects.RemoveAt(i);
                        i--;
                        continue;
                    }
                    TeamMemberCount.Add((int)BLL.CupTeamMemberInfo.CountByProjectID(Projects[i].ID + ""));
                    Declarants.Add(BLL.CupDeclarantInfo.SelectOne(Projects[i].ID + ""));
                    Students.Add(BLL.StudentInfoModel.SelectOneByUserId(Projects[i].UserID + ""));
                    Recommenders.Add(BLL.RecommendInfo.SelectByProjectId(Projects[i].ID + ""));
                }

                page_count = (int)Math.Ceiling(Projects.Count / (double)page_size);
                // 获取总页数
                if (current_page > page_count) current_page = page_count;
                if (current_page <= 0) current_page = 1;

            }
            else if (Match.MatchModel == 2)
            {

                Innovations = BLL.InnovationProjectModel.FindByInt(MatchID, "MatchID");               

                for (int i = 0; i < Innovations.Count; i++)
                {
                    CheckRecrod = BLL.CheckRecord.FindOne(Innovations[i].Id.ToString(), Match.MatchModel.ToString());
                    if (CheckRecrod == null || CheckRecrod.AfterStatus != "初审通过")
                    {
                        Innovations.RemoveAt(i);
                        i--;
                        continue;
                    }
                    TeamMemberCount.Add((int)BLL.InnovationTeamMember.CountByProjectID(Innovations[i].Id.ToString()));
                    InovationDeclarants.Add(BLL.InnovationDeclarantInfo.SelectOne(Innovations[i].Id.ToString()));
                    Students.Add(BLL.StudentInfoModel.SelectOneByUserId(Innovations[i].UserID.ToString()));
                    Tutors.Add(BLL.Tutors.SelectByProjectId(Innovations[i].Id.ToString()));
                    list = BLL.InnovationWorksInfo.FindByInt(Innovations[i].Id.ToString(), "ProjectID");
                    if (list.Count > 0)
                    {
                        InnovationWork = list[0];
                    }
                    else
                    {
                        InnovationWork = new Models.DB.InnovationWorksInfo();
                    }
                }

                page_count = (int)Math.Ceiling(Innovations.Count / (double)page_size);
                if (current_page > page_count) current_page = page_count;
                if (current_page <= 0) current_page = 1;
            }

        }
    }
}