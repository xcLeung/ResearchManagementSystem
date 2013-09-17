using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.Match
{
    public partial class Edit : System.Web.UI.Page
    {
        protected Models.DB.Match Match;
        protected Models.DB.MatchModel MatchModel;
        protected List<Models.DB.MatchModel> MatchModels = new List<Models.DB.MatchModel>();
        protected List<Models.DB.JudgeInfoModel> Judges = new List<Models.DB.JudgeInfoModel>();
        protected List<Models.DB.JudgeInfoModel> SelectedJudges = new List<Models.DB.JudgeInfoModel>();

        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            initData();
        }

        private void initData()
        {
            if (Request["edit"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                try
                {
                    int Id = Convert.ToInt32(Request["edit"]);
                    Match = BLL.Match.SelectOne(Id);
                    MatchModel = BLL.MatchModel.SelectOne(Match.MatchModel);
                    MatchModels = BLL.MatchModel.SelectMatchModel();
                    SelectedJudges = BLL.Match.SelectJudges(Id);
                    Judges = BLL.JudgeInfoModel.SelectOnePage(1, Int32.MaxValue);
                    for (int i = Judges.Count - 1; i >= 0; --i)
                    {
                        for (int j = 0; j < SelectedJudges.Count; ++j)
                        {
                            if (Judges[i].Id == SelectedJudges[j].Id)
                            {
                                Judges.RemoveAt(i);
                                break;
                            }
                        }
                        
                    }
                    for (int i = Judges.Count - 1; i >= 0; --i)
                    {
                        if (LoginRole.Name == "CollegeAdmin" && LoginAdmin.College != Judges[i].College)
                        {
                            Judges.RemoveAt(i);
                        }
                    }

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

        protected String convertMatchModel(String str)
        {
            if (str == "Tb_CupProjectModel")
            {
                return "挑战杯模型";
            }
            else if (str == "Tb_InnovationProjectModel")
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