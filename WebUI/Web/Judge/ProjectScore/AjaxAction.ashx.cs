using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ResearchManagementSystem.Web.Judge.ProjectScore
{
    /// <summary>
    /// AjaxAction 的摘要说明
    /// </summary>
    public class AjaxAction : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string dowhat = context.Request["dowhat"];
            if (dowhat == null) dowhat = "";  
            if (context.Session["user"] == null)
            {
                context.Response.Redirect("~/Web/Login/Default.aspx");
            }
            String UserID = context.Session["user"].ToString();
            String JudgeID = BLL.JudgeInfoModel.FindByInt(UserID, "UserID")[0].Id.ToString();

            #region 评分
            if (dowhat == "ProjectScore")
            {
                String Score = context.Request["Score"];
                String Remark = context.Request["Remark"];
                String ProjectID = context.Request["ProjectID"];
                String MatchModelID = context.Request["MatchModelID"];
                String ID = context.Request["ID"];
                
                if (ID=="0")
                {
                    if (BLL.ProjectScore.CreateOne(ProjectID, Score, JudgeID, Remark, MatchModelID) > 0)
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
                    if (BLL.ProjectScore.Update(ID,ProjectID, Score, JudgeID, Remark, MatchModelID) > 0)
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