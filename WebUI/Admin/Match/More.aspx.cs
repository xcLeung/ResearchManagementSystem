using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.Match
{
    public partial class More : System.Web.UI.Page
    {
        protected Models.DB.Match Match;
        protected Models.DB.MatchModel MatchModel;
        protected List<Models.DB.JudgeInfoModel> Judges = new List<Models.DB.JudgeInfoModel>();

        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            if (Request["more"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                try
                {
                    int Id = Convert.ToInt32(Request["more"]);
                    Match = BLL.Match.SelectOne(Id);
                    MatchModel = BLL.MatchModel.SelectOne(Match.MatchModel);
                    Judges = BLL.Match.SelectJudges(Id);
                }
                catch
                {
                    Response.Redirect("Default.aspx");
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