using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.Judge
{
    public partial class Master : System.Web.UI.MasterPage
    {

        protected Models.DB.Role LoginRole;

        protected String UserID;
        protected String RoleID;

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
            UserID = Session["user"].ToString();
            RoleID = Session["role"].ToString();
            LoginRole = BLL.Role.SelectRoleOne(Convert.ToInt32(RoleID));


            /**********非评委角色跳出********************/
            if (LoginRole.Name != "Judge")
            {
                Response.Redirect(ResolveUrl("~/Web/Login/Default.aspx"));
            }
        }
    }
}