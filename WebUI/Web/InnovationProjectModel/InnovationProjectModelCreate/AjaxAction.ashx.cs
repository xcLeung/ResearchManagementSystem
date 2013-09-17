using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelCreate
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

            String ProjectID = context.Request["ProjectID"];
            List<Models.DB.InnovationProjectModel> modellist = BLL.InnovationProjectModel.FindByInt(ProjectID, "ID");
            
            if (modellist.Count <= 0)
            {
                context.Response.Redirect("../../Match/Default.aspx");
            }
            Models.DB.Match Match = BLL.Match.SelectOne(Convert.ToInt32(modellist[0].MatchID));
            if (Match == null)
            {
                return;
            }
            if (modellist[0].Statu == "提交" || System.DateTime.Compare(System.DateTime.Now, Match.DeclarantDeadLine) > 0)
            {
                return;
            }


            #region 申报人信息提交
            if (dowhat == "DeclarantCreate")
            {
                String Phone = context.Request["Phone"];
                String Experience = context.Request["Experience"];
                if (BLL.InnovationDeclarantInfo.Create(ProjectID, Phone, Experience) > 0)
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

            #region 推荐人信息提交
            if (dowhat == "RecommendInfoCreate")
            {
                String[] DataList = context.Request.Form[1].ToString().Split(',');
                String[,] DataSource = Utility.Tool.SplitJson(DataList);
                if (BLL.Tutors.CreateMore(DataSource, ProjectID) > 0)
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

            #region 团队成员信息提交
            if (dowhat == "TeamMemberCreate")
            {
                String[] DataList = context.Request.Form[1].ToString().Split(',');
                String[,] DataSource = Utility.Tool.SplitJson(DataList);
                if (BLL.InnovationTeamMember.CreateMore(DataSource, ProjectID) > 0)
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

            #region 修改备注
            if (dowhat == "RemarkEdit")
            {
                String data = context.Request["Remark"].ToString();
                if (BLL.InnovationProjectModel.UpdateRemark(ProjectID, data) > 0)
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


            #region 作品信息提交
            if (dowhat == "WorkInfoCreate")
            {
                String ProjectName = context.Request["PName"];
                String DeclarationType = context.Request["DeclarationType"];
                String InterimReport = context.Request["InterimReport"];
                String SubmitAchievement = context.Request["SubmitAchievement"];
                String Managementbasis = context.Request["Managementbasis"];
                String StartTime = context.Request["StartTime"];
                String EndTime = context.Request["EndTime"];

                String[] DataList = context.Request.Form[1].ToString().Split(',');
                String[,] DataSource = Utility.Tool.SplitJson(DataList);


                if (BLL.InnovationProjectModel.Updata(ProjectID, ProjectName, DeclarationType, StartTime,EndTime,InterimReport,SubmitAchievement,Managementbasis, modellist[0].DeclarationDate.ToString(), modellist[0].MatchID.ToString(), modellist[0].UserID.ToString()) > 0)
                {
                    if (BLL.InnovationWorksInfo.CreateMore(DataSource,ProjectID)> 0)
                    {
                        context.Response.Write("success");
                    }
                    else
                    {
                        context.Response.Write("failed");
                    }
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