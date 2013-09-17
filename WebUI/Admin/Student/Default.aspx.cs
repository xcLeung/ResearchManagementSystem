using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Admin.Student
{
    public partial class Default : System.Web.UI.Page
    {
        protected List<Models.DB.StudentInfoModel> DisableStudents = new List<Models.DB.StudentInfoModel>();
        protected int current_page = 1;
        protected int page_count = 1;
        protected int page_size = 30;

        protected Models.DB.Role LoginRole;
        protected Models.DB.AdminModel LoginAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            initData();
        }
        public void initData()
        {
            // 获取当前页
            if (!string.IsNullOrEmpty(Request["page"]))
            {
                current_page = Convert.ToInt32(Request["page"]);
            }

            if (LoginRole.Name == "ScauAdmin")
            {
                // 获取总页数
                page_count = (int)Math.Ceiling(BLL.StudentInfoModel.CountByEnable("false") / (double)page_size);
                if (current_page > page_count) current_page = page_count;
                if (current_page <= 0) current_page = 1;
                DisableStudents = BLL.StudentInfoModel.SelectByEnable("false", page_size, current_page);
            }
            else if (LoginRole.Name == "CollegeAdmin")
            {
                page_count = (int)Math.Ceiling(BLL.StudentInfoModel.CountByEnableAndCollege("false",LoginAdmin.College) / (double)page_size);
                if (current_page > page_count) current_page = page_count;
                if (current_page <= 0) current_page = 1;
                DisableStudents = BLL.StudentInfoModel.SelectByEnableAndCollege("false", page_size, current_page, LoginAdmin.College);
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