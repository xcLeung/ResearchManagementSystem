using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.InnovationProjectModel
{
    public partial class Default : System.Web.UI.Page
    {
        protected Models.DB.Match Match = new Models.DB.Match();

        protected int current_page = 1;
        protected int page_count = 1;
        protected int page_size = 10;
        protected string MatchID = "";

        protected List<Models.DB.InnovationProjectModel> Innovations = new List<Models.DB.InnovationProjectModel>();
        protected List<Models.DB.InnovationDeclarantInfo> InovationDeclarants = new List<Models.DB.InnovationDeclarantInfo>();
        protected List<int> TeamMemberCount = new List<int>();
        protected List<List<Models.DB.TutorInfo>> Tutors = new List<List<Models.DB.TutorInfo>>();
        protected Models.DB.InnovationWorksInfo InnovationWork = new Models.DB.InnovationWorksInfo();
        protected List<Models.DB.StudentInfoModel> Students = new List<Models.DB.StudentInfoModel>();
        protected List<Models.DB.InnovationWorksInfo> list = new List<Models.DB.InnovationWorksInfo>();

        protected List<Models.DB.CheckRecord> CheckRecord = new List<Models.DB.CheckRecord>();


        protected void Page_Load(object sender, EventArgs e)
        {
            initData();
        }


        private void initData()
        {
            if (string.IsNullOrEmpty(Request["match"]))
            {
                Response.Redirect("../Match/Default.aspx");
            }
            MatchID = Request["match"];
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

            page_count = (int)Math.Ceiling(BLL.InnovationProjectModel.CountByMatchId(MatchID) / (double)page_size);
            if (current_page > page_count) current_page = page_count;
            if (current_page <= 0) current_page = 1;
            Innovations = BLL.InnovationProjectModel.SelectOnePage(current_page, page_size, MatchID);

            

            for (int i = 0; i < Innovations.Count; i++)
            {
                TeamMemberCount.Add((int)BLL.InnovationTeamMember.CountByProjectID(Innovations[i].Id.ToString()));
                InovationDeclarants.Add(BLL.InnovationDeclarantInfo.SelectOne(Innovations[i].Id.ToString()));
                Students.Add(BLL.StudentInfoModel.SelectOneByUserId(Innovations[i].UserID.ToString()));
                Tutors.Add(BLL.Tutors.SelectByProjectId(Innovations[i].Id.ToString()));
                list = BLL.InnovationWorksInfo.FindByInt(Innovations[i].Id.ToString(), "ProjectID");
                CheckRecord.Add(BLL.CheckRecord.FindOne(Innovations[i].Id.ToString(), Match.MatchModel.ToString()));
                if (list.Count > 0)
                {
                    InnovationWork = list[0];
                }
                else
                {
                    InnovationWork = new Models.DB.InnovationWorksInfo();
                }
            }
        }
    }
}