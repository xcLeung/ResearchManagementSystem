using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.ProjectInfo
{
    public partial class DeclarantInfo : System.Web.UI.Page
    {
        protected String ProjectID;
        protected List<Models.DB.StudentInfoModel> Studentlist = new List<Models.DB.StudentInfoModel>();
        protected Models.DB.CupDeclarantInfo model;
        protected List<Models.DB.CupProjectModel> CupModellist = new List<Models.DB.CupProjectModel>();
        protected Models.DB.Match Match = new Models.DB.Match();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Request["ProjectID"] == null)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            ProjectID = Request["ProjectID"];
            CupModellist = BLL.CupProjectModel.FindByInt(ProjectID, "ID");
            if (CupModellist.Count <= 0 )
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            Match = BLL.Match.SelectOne(Convert.ToInt32(CupModellist[0].MatchID));
            if (Match == null)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            model = BLL.CupDeclarantInfo.SelectOneByProjectID(ProjectID);
            if (model!= null)
            {        
                Response.Redirect(String.Format("~/Web/CupProjectModel/CupInfoEdit/DeclarantInfo.aspx?ProjectID={0}",model.ProjectID));
            }

          
            if (Context.Session["user"] == null)
            {
                Response.Redirect("~/Web/Login/Default.aspx");
            }
        
             String UserID = Context.Session["user"].ToString();
            Studentlist = BLL.StudentInfoModel.FindByInt(UserID, "UserId");

        }

        protected Boolean isDeadLine(System.DateTime DeadLine)
        {
            if (System.DateTime.Compare(System.DateTime.Now, DeadLine) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}