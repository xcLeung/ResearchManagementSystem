using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.Judge
{
    public partial class Default : System.Web.UI.Page
    {
        protected List<Models.DB.JudgeInfoModel> Judges=new List<Models.DB.JudgeInfoModel> ();
        protected int current_page = 1;
        protected int page_count = 1;
        protected int page_size = 30;

        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();

            // 获取当前页
            if (!string.IsNullOrEmpty(Request["page"]))
            {
                current_page = Convert.ToInt32(Request["page"]);
            }

            if (LoginRole.Name == "ScauAdmin")
            {
                // 获取总页数
                page_count = (int)Math.Ceiling(BLL.JudgeInfoModel.Count() / (double)page_size);

                if (current_page <= 0) current_page = 1;
                if (current_page > page_count) current_page = page_count;
                Judges = BLL.JudgeInfoModel.SelectOnePage(current_page, page_size);
            }
            else if (LoginRole.Name == "CollegeAdmin")
            {
                page_count = (int)Math.Ceiling(BLL.JudgeInfoModel.Count(LoginAdmin.College) / (double)page_size);
                if (current_page <= 0) current_page = 1;
                if (current_page > page_count) current_page = page_count;
                Judges = BLL.JudgeInfoModel.SelectOnePage(current_page, page_size, LoginAdmin.College);
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