using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.Judge
{
    public partial class Create : System.Web.UI.Page
    {
        protected List<Models.DB.Role> Roles;//角色列表


        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            initRoles();
        }
        private void initRoles()
        {
            Models.DB.RoleInfoModel RoleInfo = BLL.RoleInfoModel.SelectRoleInfoModel("Tb_JudgeInfoModel");
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