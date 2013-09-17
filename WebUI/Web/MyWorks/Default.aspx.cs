using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.MyWorks
{
    public partial class Default : System.Web.UI.Page
    {
        protected String UserID;
        protected Models.DB.StudentInfoModel Student;


        protected List<Models.DB.CupProjectModel> CupList;
        protected List<Models.DB.InnovationProjectModel> InnovationList;
        protected List<object> ProjectList = new List<object>();
        protected Models.DB.Match Match;
        protected Boolean isEdit = true;

        protected int current_page = 1;
        protected int page_count = 1;
        protected int page_size = 10;
        protected int PageSum = 1;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Context.Session["user"] == null)
            {
                Response.Redirect("~/Web/Login/Default.aspx");
            }
           UserID = Context.Session["user"].ToString();
           Student = BLL.StudentInfoModel.SelectOneByUserId(UserID);
           CupList = BLL.CupProjectModel.FindByInt(UserID.ToString(), "UserID");
           InnovationList = BLL.InnovationProjectModel.FindByInt(UserID.ToString(), "UserID");
           ProjectList.AddRange(CupList);
           ProjectList.AddRange(InnovationList);
           PageSum = ProjectList.Count;

           if (!string.IsNullOrEmpty(Request["page"]))
           {
               current_page = Convert.ToInt32(Request["page"].ToString());
           }
           page_count = (int)Math.Ceiling(PageSum/ (double)page_size);
           if (current_page <= 0) current_page = 1;
           if (current_page > page_count) current_page = page_count;
         

 
        }
    }
}