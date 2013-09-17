using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.Administrator
{
    public partial class Create : System.Web.UI.Page
    {
        protected List< Models.DB.Role > Roles;//角色列表
        protected Models.DB.Role Role;
        protected Models.DB.AdminModel Admin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            initRoles();
            
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

        private void accessControl()
        {
            if (Session["user"] == null || Session["role"] == null)
            {
                Response.Redirect(ResolveUrl("~/Web/Login/Default.aspx"));
            }
            String UserID = Session["user"].ToString();
            String RoleID = Session["role"].ToString();
            Role = BLL.Role.SelectRoleOne(Convert.ToInt32(RoleID));
            Admin = BLL.AdminModel.SelectAdminModelByUserID(UserID);

            
            
        }

        private void initRoles()
        {
           
            Models.DB.RoleInfoModel RoleInfo = BLL.RoleInfoModel.SelectRoleInfoModel("Tb_AdminModel");
            Roles = BLL.Role.SelectRole(RoleInfo.Id);
        }
    }
}