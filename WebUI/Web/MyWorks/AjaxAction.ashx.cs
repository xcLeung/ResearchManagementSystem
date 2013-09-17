using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ResearchManagementSystem.Web.MyWorks
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

            #region 确认提交
            if (dowhat == "SubmitProject")
            {
                String type = context.Request["Type"].ToString();
                String ProjectID = context.Request["ProjectID"].ToString();

                

                if (type == "CupProjectModel")
                {
                    Models.DB.CupProjectModel Project = BLL.CupProjectModel.FindByInt(ProjectID, "ID")[0];
                    Models.DB.Match Match = BLL.Match.SelectOne(Convert.ToInt32(Project.MatchID));
                    if (Match == null)
                    {
                        return;
                    }
                    else if (System.DateTime.Compare(System.DateTime.Now, Match.DeclarantDeadLine) > 0)
                    {
                        context.Response.Write("deadline");
                        context.Response.End();
                        return;
                    }
                    if (Project.Statu == "提交")
                    {

                        context.Response.Write("Submited");
                        context.Response.End();
                        return;
                    }

                    if (BLL.CupProjectModel.UpdateStatus("提交", ProjectID) > 0)
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
                else if (type == "InnovationProjectModel")
                {
                    Models.DB.InnovationProjectModel Project = BLL.InnovationProjectModel.FindByInt(ProjectID, "Id")[0];
                    Models.DB.Match Match = BLL.Match.SelectOne(Convert.ToInt32(Project.MatchID));
                    if (Match == null)
                    {
                        return;
                    }
                    else if (System.DateTime.Compare(System.DateTime.Now, Match.DeclarantDeadLine) > 0)
                    {
                        context.Response.Write("deadline");
                        context.Response.End();
                        return;
                    }
                    if (Project.Statu == "提交")
                    {

                        context.Response.Write("Submited");
                        context.Response.End();
                        return;
                    }
                    if (BLL.InnovationProjectModel.UpdateStatus("提交", ProjectID) > 0)
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


            #region 删除项目
            if (dowhat == "DeleteProject")
            {
                String type = context.Request["Type"].ToString();
                String ProjectID = context.Request["ProjectID"].ToString();
                
                if (type == "CupProjectModel")
                {
                    Models.DB.CupProjectModel Project = BLL.CupProjectModel.FindByInt(ProjectID, "ID")[0];
                    if (Project.Statu == "提交")
                    {

                        context.Response.Write("Submited");
                        context.Response.End();
                        return;
                    }
                    if (BLL.Delete.Word("Tb_CupProjectModel", ProjectID) > 0  )
                    {
                        BLL.Delete.WordList(ProjectID, "Tb_CupProjectModel");
                        context.Response.Write("success");
                    }
                    else
                    {
                        context.Response.Write("failed");
                    }
                    context.Response.End();
                    return;
                }
                else if (type == "InnovationProjectModel")
                {
                    Models.DB.InnovationProjectModel Project = BLL.InnovationProjectModel.FindByInt(ProjectID, "Id")[0];
                    if (Project.Statu == "提交")
                    {

                        context.Response.Write("Submited");
                        context.Response.End();
                        return;
                    }
                    if (BLL.Delete.Word("Tb_InnovationProjectModel", ProjectID) > 0 )
                    {
                        BLL.Delete.WordList(ProjectID, "Tb_InnovationProjectModel");
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