using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchManagementSystem.Web.Student
{
    /// <summary>
    /// AjaxAction 的摘要说明
    /// </summary>
    public class AjaxAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string dowhat = context.Request["dowhat"];
            if (dowhat == null) dowhat = "";

            if (dowhat == "UpdateStudent")
            {
                String StudentID = context.Request["StudentID"];
                string Password = context.Request["Password"];
                string Mail = context.Request["Mail"];
                string UserId = context.Request["UserID"];
                string InTimeYear = context.Request["InTimeYear"];
                Boolean PasswordChecked = false;
                if (Password != "")
                {
                    Password = Utility.Tool.MD5(Password);
                    PasswordChecked = true;
                }
                if (BLL.StudentInfoModel.UpdateStudent(StudentID, Mail, UserId, InTimeYear) > 0)
                {
                    if (PasswordChecked)
                    {
                        if (BLL.User.UpdateUserPassword(UserId, Password) > 0)
                        {
                            context.Response.Write("success");
                            context.Response.End();
                            return;
                        }
                        else
                        {
                            context.Response.Write("failed");
                            context.Response.End();
                            return;
                        }

                    }
                    context.Response.Write("success");
                    context.Response.End();
                    return;
                }
                context.Response.Write("failed");
                context.Response.End();
                return;
            }
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