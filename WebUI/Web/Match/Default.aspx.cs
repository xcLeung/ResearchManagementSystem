using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.Match
{
    public partial class Default : System.Web.UI.Page
    {
        protected List<Models.DB.Match> MatchList = new List<Models.DB.Match>();
        protected List<Models.DB.StudentInfoModel> StudentList;
        protected int PageSum = 1;
        protected List<Models.DB.Match> MatchSum;

        protected int current_page = 1;
        protected int page_count = 1;
        protected int page_size = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Session["user"] == null)
            {
                Response.Redirect("~/Web/Login/Default.aspx");
            }

            String UserID = Context.Session["user"].ToString();
            StudentList = BLL.StudentInfoModel.FindByInt(UserID, "UserId");
            MatchSum =  BLL.Match.MatchCountByStudent(StudentList[0].College);
            for (int i = MatchSum.Count - 1; i >= 0; i--)
            {
                if (System.DateTime.Compare(System.DateTime.Now, MatchSum[i].DeclarantDeadLine) > 0)
                {
                    MatchSum.RemoveAt(i);
                }
                
            }
            PageSum = MatchSum.Count;
            if (!string.IsNullOrEmpty(Request["page"]))
            {
                current_page = Convert.ToInt32(Request["page"].ToString());
            }
            page_count = (int)Math.Ceiling(PageSum / (double)page_size);
            if (current_page <= 0) current_page = 1;
            if (current_page > page_count) current_page = page_count;
            MatchList = BLL.Match.SelectOnePageByStudent(current_page, page_size, "ID", "desc", StudentList[0].College);
        }
    }
}