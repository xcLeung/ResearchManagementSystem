using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ResearchManagementSystem.Web.CupProjectModel.CupInfoEdit
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
            List<Models.DB.CupProjectModel> modellist = BLL.CupProjectModel.FindByInt(ProjectID, "ID");
            
            
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
                String ID = BLL.CupDeclarantInfo.SelectOneByProjectID(ProjectID).Id.ToString();
                String Birthday = context.Request["BirthDay"];
                String BackGround = context.Request["BackGround"];
                String SchoolSystme = context.Request["SchoolSystme"];
                String PaperTitle = context.Request["PaperTitle"];
                String TopAddress = context.Request["TopAddress"];
                String TopPostalCode = context.Request["TopPostalCode"];
                String TopPhone = context.Request["TopPhone"];
                String Address = context.Request["Address"];
                String PostalCode = context.Request["PostalCode"];
                String Phone = context.Request["Phone"];

                if (BLL.CupDeclarantInfo.Updata(ID, Birthday, SchoolSystme, PaperTitle, TopAddress, BackGround, TopPostalCode, TopPhone, Address, PostalCode, Phone, ProjectID) > 0)
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

            #region 作品信息提交（自然科学）
            if (dowhat == "WorkInfoEditA")
            {
                String ID = BLL.CupWorksInfo.FindByInt(ProjectID, "ProjectID")[0].ID.ToString();
                String ProjectName = context.Request["PName"];
                String DeclarationType = context.Request["DeclarationType"];
                String Category = context.Request["Category"];
                String[] DataList = context.Request.Form[1].ToString().Split(',');
                String[,] DataSource = Utility.Tool.SplitJson(DataList);
                
                if (BLL.CupProjectModel.Updata(ProjectID, ProjectName, DeclarationType, Category, modellist[0].DeclarationDate.ToString(), modellist[0].MatchID.ToString(), modellist[0].UserID.ToString()) > 0)
                {
                    if (BLL.CupWorksInfo.UpData(ID,DataSource,ProjectID) > 0)
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

            #region 作品信息提交（哲学）
            if (dowhat == "WorkInfoEditB")
            {
                String ID = BLL.CupWorksSurvey.FindByInt(ProjectID, "ProjectID")[0].Id.ToString();
                String ProjectName = context.Request["PName"];
                String DeclarationType = context.Request["DeclarationType"];
                String Category = context.Request["Category"];
                String[] DataList = context.Request.Form[1].ToString().Split(',');
                String[,] DataSource = Utility.Tool.SplitJson(DataList);
                if (BLL.CupProjectModel.Updata(ProjectID, ProjectName, DeclarationType, Category, modellist[0].DeclarationDate.ToString(), modellist[0].MatchID.ToString(), modellist[0].UserID.ToString()) > 0)
                {
                    if (BLL.CupWorksSurvey.UpData(ID,DataSource,ProjectID) > 0)
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

            #region 作品信息提交（科技创作）
            if (dowhat == "WorkInfoEditC")
            {
                String ID = BLL.CupWorksInvention.FindByInt(ProjectID, "ProjectID")[0].Id.ToString();
                String ProjectName = context.Request["PName"];
                String DeclarationType = context.Request["DeclarationType"];
                String Category = context.Request["Category"];
                String[] DataList = context.Request.Form[1].ToString().Split(',');
                String[,] DataSource = Utility.Tool.SplitJson(DataList);
                if (BLL.CupProjectModel.Updata(ProjectID, ProjectName, DeclarationType, Category, modellist[0].DeclarationDate.ToString(), modellist[0].MatchID.ToString(), modellist[0].UserID.ToString()) > 0)
                {
                    if (BLL.CupWorksInvention.UpData(ID,DataSource,ProjectID) > 0)
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



            #region 删除推荐人
            if (dowhat == "deleteRecommend")
            {
                String ID = context.Request["ID"];
                if (BLL.Delete.Word("Tb_RecommendedInfo", ID) > 0)
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


            #region 修改推荐人
            if (dowhat == "RecommendInfoEidt")
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
                        if (BLL.RecommendInfo.Create(data, ProjectID)>0)
                        {
                            count++;
                        }
                    }
                    else
                    {
                        if (BLL.RecommendInfo.Updata(data, ProjectID) > 0)
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

            #region 删除团队成员
            if (dowhat == "deleteTeamMember")
            {
                String ID = context.Request["ID"];
                if (BLL.Delete.Word("Tb_CupTeamMemberInfo", ID) > 0)
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

            #region 修改团队成员
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
                        if (BLL.CupTeamMemberInfo.Create(data, ProjectID) > 0)
                        {
                            count++;
                        }
                    }
                    else
                    {
                        if (BLL.CupTeamMemberInfo.Updata(data, ProjectID) > 0)
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