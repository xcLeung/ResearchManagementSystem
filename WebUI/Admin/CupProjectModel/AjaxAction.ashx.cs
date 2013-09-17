using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ResearchManagementSystem.Admin.CupProjectModel
{
    /// <summary>
    /// AjaxAction 的摘要说明
    /// </summary>
    public class AjaxAction : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string dowhat=context.Request["dowhat"];

            #region 审核记录
            if (dowhat == "CheckRecrod")
            {
                if (context.Session["user"] == null)
                {
                    context.Response.Redirect("~/Web/Login/Default.aspx");
                }

                String UserID = context.Session["user"].ToString();
                String Checker = BLL.AdminModel.SelectAdminModelByUserID(UserID).Name;
                String ProjectID = context.Request["ProjectID"];
                String MatchModelID = context.Request["MatchModelID"];
                String AfterStatus = context.Request["Pass"];
                String Reason = context.Request["Reason"];
                Models.DB.CheckRecord model = BLL.CheckRecord.FindOne(ProjectID, MatchModelID);
                if (model == null)
                {
                    if (BLL.CheckRecord.Create(AfterStatus, Checker, MatchModelID, ProjectID, Reason) > 0)
                    {
                        context.Response.Write("success");
                    }
                    else
                    {
                        context.Response.Write("failed");
                    }
                    context.Response.End();
                    return;
                }
                else
                {
                    if (BLL.CheckRecord.Update(AfterStatus,Checker,MatchModelID,ProjectID,Reason,model.AfterStatus,model.Id.ToString()) > 0)
                    {
                        context.Response.Write("success");
                    }
                    else
                    {
                        context.Response.Write("failed");
                    }
                    context.Response.End();
                    return;
                }
            }
            #endregion


            #region 通过比赛
            if (dowhat == "PassProject")
            {
                if (context.Session["user"] == null)
                {
                    context.Response.Redirect("~/Web/Login/Default.aspx");
                }
                String UserID = context.Session["user"].ToString();
                String Checker = BLL.AdminModel.SelectAdminModelByUserID(UserID).Name;
                String ProjectID = context.Request["ProjectID"];
                String MatchModelID = context.Request["Type"].ToString();
                String AfterStatus = "终审通过";
                String Reason = "获得比赛名额";
                Models.DB.CheckRecord model = BLL.CheckRecord.FindOne(ProjectID, MatchModelID);
                if (BLL.CheckRecord.Update(AfterStatus, Checker, MatchModelID, ProjectID, Reason, model.AfterStatus, model.Id.ToString()) > 0)
                {
                    context.Response.Write("success");
                }
                else
                {
                    context.Response.Write("failed");
                }
                context.Response.End();
                return;
            }
            #endregion

            #region 否决比赛
            if (dowhat == "DelyProject")
            {
                if (context.Session["user"] == null)
                {
                    context.Response.Redirect("~/Web/Login/Default.aspx");
                }
                String UserID = context.Session["user"].ToString();
                String Checker = BLL.AdminModel.SelectAdminModelByUserID(UserID).Name;
                String ProjectID = context.Request["ProjectID"];
                String MatchModelID = context.Request["Type"].ToString();
                String AfterStatus = "终审失败";
                String Reason = "获得比赛名额";
                Models.DB.CheckRecord model = BLL.CheckRecord.FindOne(ProjectID, MatchModelID);
                if (BLL.CheckRecord.Update(AfterStatus, Checker, MatchModelID, ProjectID, Reason, model.AfterStatus, model.Id.ToString()) > 0)
                {
                    context.Response.Write("success");
                }
                else
                {
                    context.Response.Write("failed");
                }
                context.Response.End();
                return;
            }
            #endregion

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}