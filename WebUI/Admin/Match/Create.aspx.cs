using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.Match
{
    public partial class Create : System.Web.UI.Page
    {
        protected List<Models.DB.MatchModel> MatchModels;
        protected List<Models.DB.JudgeInfoModel> JudgeInfoModels;

        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            initData();
        }
        private void initData()
        {
            MatchModels = BLL.MatchModel.SelectMatchModel();
            JudgeInfoModels = BLL.JudgeInfoModel.SelectOnePage(1, Int32.MaxValue);
            if (LoginRole.Name == "CollegeAdmin")
            {
                for (int i = JudgeInfoModels.Count - 1; i >= 0; --i)
                {
                    if (LoginAdmin.College != JudgeInfoModels[i].College)
                    {
                        JudgeInfoModels.RemoveAt(i);
                    }
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