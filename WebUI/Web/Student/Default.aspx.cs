using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.Student
{
    public partial class Default : System.Web.UI.Page
    {
        protected Models.DB.StudentInfoModel Student;

        protected String UserID;

        protected void Page_Load(object sender, EventArgs e)
        {
            initData();
        }


        private void initData()
        {
            if (Session["user"] == null)
            {
                Response.Redirect(ResolveUrl("~/Web/Login/Default.aspx"));
            }
            UserID = Session["user"].ToString();
            Student = BLL.StudentInfoModel.SelectOneByUserId(UserID);
        }

    }
}