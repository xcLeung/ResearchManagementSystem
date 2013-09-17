using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelCreate
{
    public partial class DeclarantInfo : System.Web.UI.Page
    {

        protected String ProjectID;
        protected String UserID;

        protected Models.DB.StudentInfoModel Student;
        protected List<Models.DB.InnovationProjectModel> Projects = new List<Models.DB.InnovationProjectModel>();
        protected Models.DB.InnovationDeclarantInfo Declarant;
        protected Models.DB.Match Match = new Models.DB.Match();
        protected void Page_Load(object sender, EventArgs e)
        {
            initData();
        }

        private void initData()
        {
            if (Context.Request["ProjectID"] == null)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            if (Context.Session["user"] == null)
            {
                Response.Redirect(ResolveUrl("~/Web/Login/Default.aspx"));
            }

            ProjectID = Context.Request["ProjectID"].ToString();
            UserID = Context.Session["user"].ToString();
            Student = BLL.StudentInfoModel.SelectOneByUserId(UserID);
            Projects = BLL.InnovationProjectModel.FindByInt(ProjectID, "Id");
            Declarant = BLL.InnovationDeclarantInfo.SelectOne(ProjectID);
            if (Declarant != null)
            {
                Response.Redirect(String.Format("~/Web/InnovationProjectModel/InnovationProjectModelEdit/DeclarantInfo.aspx?ProjectID={0}", Declarant.ProjectID));
            }


            if (Projects.Count <= 0 )
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            Match = BLL.Match.SelectOne(Convert.ToInt32(Projects[0].MatchID));
            if (Match == null)
            {
                Response.Redirect("../../Match/Default.aspx");
            }
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