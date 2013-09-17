using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelEdit
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
            List<Models.DB.InnovationProjectModel> modellist = BLL.InnovationProjectModel.FindByInt(ProjectID, "Id");
           
            if (modellist.Count <= 0)
            {
                context.Response.Redirect("../../Match/Default.aspx");
            }
            Models.DB.Match Match = BLL.Match.SelectOne(Convert.ToInt32(modellist[0].MatchID));
            if (Match == null)
            {
                return;
            }
            ///作品已提交，直接返回
            if (modellist[0].Statu == "提交" || System.DateTime.Compare(System.DateTime.Now, Match.DeclarantDeadLine) > 0)
            {
                return;
            }


            #region 申报人信息修改
            if (dowhat == "DeclarantEdit")
            {
                String ID = BLL.InnovationDeclarantInfo.SelectOne(ProjectID).Id.ToString();
                String Phone = context.Request["Phone"];
                String Experience = context.Request["Experience"];
                if (BLL.InnovationDeclarantInfo.Update(ID,ProjectID, Phone, Experience) > 0)
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


            #region 推荐人信息修改
            if (dowhat == "RecommendInfoEdit")
            {
                int count = 0;
                String[] DataList = context.Request.Form[1].ToString().Split(',');
                String[,] DataSource = Utility.Tool.SplitJson(DataList);
                for (int i = 0; i < DataSource.GetLength(0); i++)
                {
                    String[] data = new String[DataSource.GetLength(1)];
                    for (int j = 0; j < DataSource.GetLength(1); j++)
                    {
                        data[j] = DataSource[i, j];
                    }
                    if (DataSource[i, DataSource.GetLength(1) - 1] == "0")
                    {
                        if (BLL.Tutors.Create(data, ProjectID) > 0)
                        {
                            count++;
                        }
                    }
                    else
                    {
                        if (BLL.Tutors.Updata(data, ProjectID) > 0)
                        {
                            count++;
                        }
                    }
                }
                if (count >= DataSource.GetLength(0))
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

            #region 推荐人信息删除
            if (dowhat == "deleteRecommend")
            {
                String ID = context.Request["ID"];
                if (BLL.Delete.Word("Tb_TutorInfo", ID) > 0)
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


            #region 团队成员信息修改
            if (dowhat == "TeamMemberEdit")
            {
                int count = 0;
                String[] DataList = context.Request.Form[1].ToString().Split(',');
                String[,] DataSource = Utility.Tool.SplitJson(DataList);
                for (int i = 0; i < DataSource.GetLength(0); i++)
                {
                    String[] data = new String[DataSource.GetLength(1)];
                    for (int j = 0; j < DataSource.GetLength(1); j++)
                    {
                        data[j] = DataSource[i, j];
                    }
                    if (DataSource[i, DataSource.GetLength(1) - 1] == "0")
                    {
                        if (BLL.InnovationTeamMember.Create(data, ProjectID) > 0)
                        {
                            count++;
                        }
                    }
                    else
                    {
                        if (BLL.InnovationTeamMember.Updata(data, ProjectID) > 0)
                        {
                            count++;
                        }
                    }
                }
                if (count >= DataSource.GetLength(0))
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

            #region 团队成员信息删除
            if (dowhat == "deleteTeamMember")
            {
                String ID = context.Request["ID"];
                if (BLL.Delete.Word("Tb_InnovationTeamMember", ID) > 0)
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


            #region 作品信息修改
            if (dowhat == "WorkInfoEdit")
            {
                String ProjectName = context.Request["PName"];
                String DeclarationType = context.Request["DeclarationType"];
                String InterimReport = context.Request["InterimReport"];
                String SubmitAchievement = context.Request["SubmitAchievement"];
                String Managementbasis = context.Request["Managementbasis"];
                String StartTime = context.Request["StartTime"];
                String EndTime = context.Request["EndTime"];
                String ID = context.Request["ID"];

                String[] DataList = context.Request.Form[1].ToString().Split(',');
                String[,] DataSource = Utility.Tool.SplitJson(DataList);


                if (BLL.InnovationProjectModel.Updata(ProjectID, ProjectName, DeclarationType, StartTime, EndTime, InterimReport, SubmitAchievement, Managementbasis, modellist[0].DeclarationDate.ToString(), modellist[0].MatchID.ToString(), modellist[0].UserID.ToString()) > 0)
                {
                    if (BLL.InnovationWorksInfo.UpDate(ID,DataSource, ProjectID) > 0)
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