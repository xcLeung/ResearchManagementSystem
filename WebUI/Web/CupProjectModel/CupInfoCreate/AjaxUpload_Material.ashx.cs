using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ResearchManagementSystem.Web.CupProjectModel.CupInfoCreate
{
    /// <summary>
    /// AjaxUpload_Material 的摘要说明
    /// </summary>
    public class AjaxUpload_Material : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            Models.DB.CupProjectModel Project = new Models.DB.CupProjectModel();
            // 上传文件
            HttpPostedFile fileData = context.Request.Files["Filedata"];   //Filedata是定死的
            if (fileData != null)
            {
                string result = null;
                try
                {
                    //TODO通过Session获取id："../upload/Material/"+Session["id"].ToString()
                    if (context.Session["user"] == null)
                    {
                        context.Response.Redirect("~/Web/Login/Default.aspx");
                    }
                    if (context.Session["ProjectType"] == null || context.Session["ProjectID"]==null)
                    {
                        context.Response.Redirect("~/Web/CupInfoCreate/UploadPaper.aspx");
                    }

                    String UserID = context.Session["user"].ToString();
                    String ProjectType = context.Session["ProjectType"].ToString();
                    String ProjectID = context.Session["ProjectID"].ToString();
                    if (ProjectType == "CupProjectModel")
                    {
                        Project = BLL.CupProjectModel.FindByInt(ProjectID, "ID")[0];
                    }
                    else
                    {
                    }
                    String MatchName = BLL.Match.SelectOne(Project.MatchID).MatchName;

                    string fileExt = Path.GetExtension(fileData.FileName);
                    string fileName = Project.ID+ fileExt;
                    string dir = context.Server.MapPath("~/Web/upload/Material/" +MatchName + "/PaperDoc");
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    fileData.SaveAs(Path.Combine(dir, fileName));
                    result = "~/Web/upload/Material/" + MatchName + "/PaperDoc/" + fileName;
                }
                catch
                {
                    result = null;
                }
                context.Response.Write(result);
                context.Response.End();
            }
            context.Response.End();
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