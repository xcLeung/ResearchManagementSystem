using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.ProjectCheck
{
    public partial class Default : System.Web.UI.Page
    {

        protected int PageSum = 1;
        protected List<Models.DB.CupProjectModel> CupList;
        protected List<Models.DB.InnovationProjectModel> InnovationList;
        protected Models.DB.CheckRecord CheckRecord;

        protected String ProjectID;
        protected String ProjectName=null;
        protected String MatchModelID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Request["ProjectID"]) || String.IsNullOrEmpty(Request["type"]))
            {
                Response.Redirect("~/Web/Login/Default.aspx");
            }
                   
            String ModelType = Request["type"].ToString();
            ProjectID = Request["ProjectID"].ToString();
            if (ModelType == "CupProjectModel")
            {
                CupList = BLL.CupProjectModel.FindByInt(ProjectID, "ID");
                if (CupList.Count <= 0)
                {
                    Response.Redirect("../MyWorks/Default.aspx");
                }
                ProjectName = CupList[0].Name;
                MatchModelID = "1";
            }
            else if (ModelType == "InnovationProjectModel")
            {
                InnovationList = BLL.InnovationProjectModel.FindByInt(ProjectID.ToString(), "ID");
                if (InnovationList.Count <= 0)
                {
                    Response.Redirect("../MyWorks/Default.aspx");
                }
                ProjectName = InnovationList[0].Name;
                MatchModelID = "2";
            }
            CheckRecord = BLL.CheckRecord.FindOne(ProjectID, MatchModelID);
           if(CheckRecord==null){
                CheckRecord = new Models.DB.CheckRecord();
                CheckRecord.Time = System.DateTime.Now;
               CheckRecord.Reason = "无";
               CheckRecord.Checker = "无";
               CheckRecord.BeforeStatus = "无";
               CheckRecord.AfterStatus = "无";
           }
        }
    }
}