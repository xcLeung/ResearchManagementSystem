using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.Judge
{
    public partial class Default : System.Web.UI.Page
    {

        protected List<Models.DB.JudgeInfoModel> JudgeList;
        protected Models.DB.JudgeInfoModel Judge;

        protected String UserID;

        protected void Page_Load(object sender, EventArgs e)
        {
            initData();
        }


        private void initData()
        {
            if (Session["user"] == null || Session["role"] == null)
            {
                Response.Redirect(ResolveUrl("~/Web/Login/Default.aspx"));
            }
            UserID = Session["user"].ToString();

            JudgeList = BLL.JudgeInfoModel.FindByInt(UserID,"UserId");
            if (JudgeList.Count > 0)
            {
                Judge = JudgeList[0];
            }
            else
            {
                Response.Redirect(ResolveUrl("~/Web/Login/Default.aspx"));
            }
        }

 
    }
}