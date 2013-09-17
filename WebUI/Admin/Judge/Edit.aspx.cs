using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.Judge
{
    public partial class Edit : System.Web.UI.Page
    {
        protected Models.DB.JudgeInfoModel Judge;
        protected Models.DB.User user;


        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            if (Request["edit"] == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }
            try
            {
                int Id = Convert.ToInt32(Request["edit"]);
                Judge = BLL.JudgeInfoModel.SelectOne(Id);
                user = BLL.User.SelectUserOne(Judge.UserId);
            }
            catch
            {
                Response.Redirect("Default.aspx");
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