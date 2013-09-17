using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.Judge.Match
{
    public partial class ScoreIntro : System.Web.UI.Page
    {
        protected Models.DB.Match Match = new Models.DB.Match();
        protected String MatchID;

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

        }
    }
}