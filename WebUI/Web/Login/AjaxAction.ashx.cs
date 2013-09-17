using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ResearchManagementSystem.Web.Login
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


            #region 登录
            if (dowhat == "login")
            {
                string UserName = context.Request["UserName"];
                string Password = context.Request["Password"];
                Password = Utility.Tool.MD5(Password);

                Models.DB.User user = BLL.User.Find(UserName, Password);
                if (user.Enable == false)
                {
                    context.Response.Write("EnableFalse");
                    context.Response.End();
                    return;
                }
                if (user != null)
                {
                    context.Session["user"] = user.ID;
                    context.Session["role"] = user.RoleId;
                    Models.DB.Role role = BLL.Role.SelectRoleOne(user.RoleId);

                    if (role != null)
                    {
                        context.Response.Write(role.Name);
                        context.Response.End();
                        return;
                    }
                    else
                    {
                        context.Response.Write("faild");
                        context.Response.End();
                        return;
                    }           
                //    context.Response.Write("success");
                }
                else
                {
                    context.Response.Write("faild");
                }
                context.Response.End();
                return;
            }
            #endregion


            #region 注册
            if (dowhat == "register")
            {

                String UserName = context.Request["UserName"];
                String Password = context.Request["Password"];
                Password = Utility.Tool.MD5(Password);
                String StudentID = context.Request["StudentID"];
                String Name = context.Request["Name"];
                String Sex = context.Request["Sex"];
                String InTimeYear = context.Request["InTimeYear"];
                String School = context.Request["School"];
                String College = context.Request["College"];
                String Major = context.Request["Major"];
                String Mail = context.Request["Mail"];


                int RoleID = BLL.Role.Find("Student");
                if (RoleID == 0)
                    return;

                if (BLL.User.Find(UserName) != null)
                {
                    context.Response.Write("USEREXIST");
                    context.Response.End();
                    return;
                }
                if (BLL.StudentInfoModel.FindByString(StudentID, "StudentID").Count > 0)
                {
                    context.Response.Write("STUDENTEXIST");
                    context.Response.End();
                    return;
                }

                int UserID = BLL.User.CreateUser(UserName, Password, RoleID,"False");


                if (UserID > 0)
                {
                    if (BLL.Create.CreateStudentModelInfo(StudentID, Name, Sex, Major, InTimeYear, School, College, Mail, UserID) > 0)
                    {
                        context.Response.Write("success");
                    }
                    else
                    {
                        BLL.Delete.Word("Tb_User", UserID.ToString());
                        context.Response.Write("faild");
                    }
                }
                else
                {
                    context.Response.Write("faild");
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