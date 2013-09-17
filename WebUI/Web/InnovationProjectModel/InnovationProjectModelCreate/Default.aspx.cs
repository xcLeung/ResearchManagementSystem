using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelCreate
{
    public partial class Default : System.Web.UI.Page
    {
        protected String MatchID;
        protected int ProjectID;


        protected void Page_Load(object sender, EventArgs e)
        {
            initData();
        }

        private void initData()
        {
            if (Context.Session["user"] == null)
            {
                Response.Redirect(ResolveUrl("~/Web/Login/Default/aspx"));
            }
            String UserID = Context.Session["user"].ToString();
            if (String.IsNullOrEmpty(Request["MatchID"]))
            {
                Response.Redirect("../../Match/Default.aspx");
            }
            MatchID = Request["MatchID"];  
            ProjectID = BLL.InnovationProjectModel.Create(MatchID,"未提交",UserID,System.DateTime.Now.ToString(),System.DateTime.Now.ToString(),System.DateTime.Now.ToString());
            Response.Redirect("DeclarantInfo.aspx?ProjectID=" + ProjectID);
        }
    }
}