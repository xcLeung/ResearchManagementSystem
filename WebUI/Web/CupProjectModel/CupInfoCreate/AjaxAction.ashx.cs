using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace ResearchManagementSystem.Web.CupProjectModel.CupInfoCreate
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
            if (modellist[0].Statu == "提交" || System.DateTime.Compare(System.DateTime.Now,Match.DeclarantDeadLine)>0)
            {
                return;
            }

            #region 申报人信息提交
            if (dowhat == "DeclarantCreate")
            {
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

                if (BLL.CupDeclarantInfo.Create(Birthday, SchoolSystme, PaperTitle, TopAddress, BackGround, TopPostalCode,
                    TopPhone, Address, PostalCode, Phone, ProjectID) > 0)
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
                  String[,] DataSource = new String[DataList.Length,5];
                for (int i = 0; i < DataList.Length; i++)
                {
                    String[] Datas = DataList[i].Split('&');
                    for (int j = 0; j < Datas.Length; j++)
                    {
                        String[] DataPair = Datas[j].Split('=');
                        DataSource[i,j] = HttpUtility.UrlDecode(DataPair[1]); 
                    }
                }
                if (BLL.CupTeamMemberInfo.CreateMore(DataSource, ProjectID) > 0)
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
            if (dowhat == "WorkInfoCreateA")
            {
                String ProjectName = context.Request["PName"];
                String DeclarationType = context.Request["DeclarationType"];
                String Category = context.Request["Category"];
                String[] DataList = context.Request.Form[1].ToString().Split(',');
                int column = DataList[0].Split('&').Length;      
                String[,] DataSource = new String[DataList.Length, column];
                for (int i = 0; i < DataList.Length; i++)
                {
                    String[] Datas = DataList[i].Split('&');
                    for (int j = 0; j < column; j++)
                    {
                        String[] DataPair = Datas[j].Split('=');
                        DataSource[i,j] = HttpUtility.UrlDecode(DataPair[1]);
                    }
                }
               
                if (BLL.CupProjectModel.Updata(ProjectID, ProjectName, DeclarationType, Category,modellist[0].DeclarationDate.ToString(),modellist[0].MatchID.ToString(),modellist[0].UserID.ToString())>0)
                {
                    if (BLL.CupWorksInfo.CreateMore(DataSource,ProjectID) > 0)
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
            if (dowhat == "WorkInfoCreateB")
            {
                String ProjectName = context.Request["PName"];
                String DeclarationType = context.Request["DeclarationType"];
                String Category = context.Request["Category"];
                String[] DataList = context.Request.Form[1].ToString().Split(',');
                int column = DataList[0].Split('&').Length;
                String[,] DataSource = new String[DataList.Length, column];
                for (int i = 0; i < DataList.Length; i++)
                {
                    String[] Datas = DataList[i].Split('&');
                    for (int j = 0; j < column; j++)
                    {
                        String[] DataPair = Datas[j].Split('=');
                        DataSource[i, j] = HttpUtility.UrlDecode(DataPair[1]);
                    }
                }
                if (BLL.CupProjectModel.Updata(ProjectID, ProjectName, DeclarationType, Category, modellist[0].DeclarationDate.ToString(), modellist[0].MatchID.ToString(), modellist[0].UserID.ToString()) > 0)
                {
                    if (BLL.CupWorksSurvey.CreateMore(DataSource, ProjectID) > 0)
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
            if (dowhat == "WorkInfoCreateC")
            {
                String ProjectName = context.Request["PName"];
                String DeclarationType = context.Request["DeclarationType"];
                String Category = context.Request["Category"];
                String[] DataList = context.Request.Form[1].ToString().Split(',');
                int column = DataList[0].Split('&').Length;
                String[,] DataSource = new String[DataList.Length, column];
                for (int i = 0; i < DataList.Length; i++)
                {
                    String[] Datas = DataList[i].Split('&');
                    for (int j = 0; j < column; j++)
                    {
                        String[] DataPair = Datas[j].Split('=');
                        DataSource[i, j] = HttpUtility.UrlDecode(DataPair[1]);
                    }
                }
                if (BLL.CupProjectModel.Updata(ProjectID, ProjectName, DeclarationType, Category, modellist[0].DeclarationDate.ToString(), modellist[0].MatchID.ToString(), modellist[0].UserID.ToString()) > 0)
                {
                    if (BLL.CupWorksInvention.CreateMore(DataSource, ProjectID) > 0)
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

            #region 推荐人信息提交
            if (dowhat == "RecommendInfoCreate")
            {
                String[] DataList = context.Request.Form[1].ToString().Split(',');
                int column = DataList[0].Split('&').Length;
                String[,] DataSource = new String[DataList.Length, column];
                for (int i = 0; i < DataList.Length; i++)
                {
                    String[] Datas = DataList[i].Split('&');
                    for (int j = 0; j < column; j++)
                    {
                        String[] DataPair = Datas[j].Split('=');
                        DataSource[i, j] = HttpUtility.UrlDecode(DataPair[1]);
                    }
                }
                if (BLL.RecommendInfo.CreateMore(DataSource, ProjectID) > 0)
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


            #region 上传论文文档
            if (dowhat == "UploadPaper")
            {
                String data = context.Request["Url"].ToString();
                if((BLL.CupProjectModel.UpdatePaperUrl(ProjectID,data))>0){
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
                if(BLL.CupProjectModel.UpdateRemark(ProjectID,data)>0){
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