using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchManagementSystem.Admin.Student
{
    /// <summary>
    /// AjaxAction 的摘要说明
    /// </summary>
    public class AjaxAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string dowhat = context.Request["dowhat"];
            #region 审核通过
            if (dowhat == "enable")
            {
                string UserId = context.Request["UserId"];
                string Enable = context.Request["Enable"];
                if (BLL.User.UpdateUserEnable(UserId, Enable) > 0)
                {
                    context.Response.Write("SUCCESS");
                    context.Response.End();
                    return;
                }
                context.Response.Write("FAILD");
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