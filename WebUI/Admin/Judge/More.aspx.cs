using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.Judge
{
    public partial class More : System.Web.UI.Page
    {
        protected Models.DB.JudgeInfoModel Judge;

        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            initData();
        }
        private void initData()
        {
            int Id = 0;
            if(Request["judge"]!=null)
            {
                try
                {
                    Id = Convert.ToInt32(Request["judge"]);
                    Judge = BLL.JudgeInfoModel.SelectOne(Id);
                    return;
                }
                catch { }
            }
            Response.Redirect("../Judge");
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