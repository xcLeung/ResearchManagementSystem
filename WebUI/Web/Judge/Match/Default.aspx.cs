using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.Judge.Match
{
    public partial class Default : System.Web.UI.Page
    {
        protected Models.DB.JudgeInfoModel Judge;
        protected List<Models.FB.MatchAndJudge> MatchJudgeList;

        protected int current_page = 1;
        protected int page_count = 1;
        protected int page_size = 10;
        protected int PageSum = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Session["user"] == null)
            {
                Response.Redirect("~/Web/Login/Default.aspx");
            }
            String UserID = Context.Session["user"].ToString();
            Judge = BLL.JudgeInfoModel.FindByInt(UserID,"UserId")[0];
            MatchJudgeList =  BLL.MatchJuryRelation.FindJudgeMatch(Judge.Id.ToString());

            if (!string.IsNullOrEmpty(Request["page"]))
            {
                current_page = Convert.ToInt32(Request["page"].ToString());
            }
            PageSum = MatchJudgeList.Count;
            page_count = (int)Math.Ceiling(PageSum / (double)page_size);
            if (current_page <= 0) current_page = 1;
            if (current_page > page_count) current_page = page_count;
        }
    }
}