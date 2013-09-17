using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchManagementSystem.Web.Judge
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


            if (dowhat == "UpdateJudge")
            {
                string Id = context.Request["Id"];
               // string PasswordChecked = context.Request["PasswordChecked"];
                string Password = context.Request["Password"];
                string Name = context.Request["RealName"];
                string Address = context.Request["Address"];
                string WorkUnits = context.Request["WorkUnits"];
                string Title = context.Request["Title"];
                string Background = context.Request["Background"];
                string Research = context.Request["Research"];
                string JobId = context.Request["JobID"];
                string CampusId = context.Request["CampusID"];
                string College = context.Request["College"];
                string Mail = context.Request["Mail"];
                string Phone = context.Request["Phone"];
                string Sex = context.Request["Sex"];
                string UserId = context.Request["UserID"];

                Boolean PasswordChecked = false;
                if (Password != "")
                {
                    Password = Utility.Tool.MD5(Password);
                    PasswordChecked = true;
                }


                if (BLL.JudgeInfoModel.UpdateJudgeInfoModel(Id, Name, Address, WorkUnits, Title,
                     College, Mail, Phone, Sex, Background, Research, JobId, CampusId, UserId) > 0)
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