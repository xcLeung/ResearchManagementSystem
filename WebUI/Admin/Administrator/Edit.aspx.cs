using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.Administrator
{
    public partial class Edit : System.Web.UI.Page
    {
        public Models.DB.AdminModel Admin;
        protected List<Models.DB.Role> Roles;//角色列表
        protected Models.DB.User user;
        protected Models.DB.Role Role;

        protected Models.DB.User LoginUser;
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
                Admin = BLL.AdminModel.SelectAdminModelOne(Id);
                user = BLL.User.SelectUserOne(Admin.UserId);
                Role = BLL.Role.SelectRoleOne(user.RoleId);
                initRoles();
            }
            catch {
                Response.Redirect("Default.aspx");
            }
        }

        protected String ConvertRoleName(String RoleName)
        {
            if (RoleName == "ScauAdmin")
            {
                return "校级管理员";
            }
            else if (RoleName == "CollegeAdmin")
            {
                return "院级管理员";
            }
            else
            {
                return null;
            }
        }

        private void initRoles()
        {
            Models.DB.RoleInfoModel RoleInfo = BLL.RoleInfoModel.SelectRoleInfoModel("Tb_AdminModel");
            Roles = BLL.Role.SelectRole(RoleInfo.Id);
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