using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin
{
    public partial class Master1 : System.Web.UI.MasterPage
    {

        protected Models.DB.Role LoginRole = new Models.DB.Role();

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
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

            /***********非管理员角色跳出*************/
            if (!(LoginRole.Name == "ScauAdmin" || LoginRole.Name=="CollegeAdmin"))
            {
                Response.Redirect(ResolveUrl("~/Web/Login/Default.aspx"));
            }

        }
    }
}