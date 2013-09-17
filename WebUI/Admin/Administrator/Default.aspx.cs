using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.Administrator
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Models.DB.AdminModel> Admins;
        protected int current_page = 1;
        protected int page_count = 1;
        protected int page_size = 30;

        protected Models.DB.Role Role;
        protected Models.DB.AdminModel Admin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();

            // 获取当前页
            if (!string.IsNullOrEmpty(Request["page"]))
            {
                current_page = Convert.ToInt32(Request["page"]);
            }

            if (Role.Name == "ScauAdmin")
            {
                page_count = (int)Math.Ceiling(BLL.AdminModel.Count() / (double)page_size);
                if (current_page <= 0) current_page = 1;
                if (current_page > page_count) current_page = page_count;
                Admins = BLL.AdminModel.SelectOnePage(current_page, page_size);
            }
            else if (Role.Name == "CollegeAdmin")
            {
                page_count = (int)Math.Ceiling(BLL.AdminModel.Count(Admin.College) / (double)page_size);
                if (current_page <= 0) current_page = 1;
                if (current_page > page_count) current_page = page_count;
                Admins = BLL.AdminModel.SelectOnePage(current_page, page_size,Admin.College);
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
    }
}