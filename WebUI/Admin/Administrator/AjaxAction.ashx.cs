using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchManagementSystem.Admin.Administrator
{
    /// <summary>
    /// AjaxAction 的摘要说明
    /// </summary>
    public class AjaxAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string dowhat = context.Request["dowhat"];
            #region 添加管理员
            if (dowhat == "add")
            {
                int RoleId = Convert.ToInt32(context.Request["RoleId"]);
                string UserName= context.Request["UserName"];
                string Password= context.Request["Password"];
                Password = Utility.Tool.MD5(Password);
                string ConfirmPassword= context.Request["ConfirmPassword"];
                string Name= context.Request["Name"];
                string JobId= context.Request["JobId"];
                string College= context.Request["College"];
                string Mail= context.Request["Mail"];
                string Phone= context.Request["Phone"];
                string Sex = context.Request["Sex"];
                string Enable = context.Request["Enable"];
                if (BLL.User.Find(UserName) != null)
                {
                    context.Response.Write("EXIST");
                    context.Response.End();
                    return;
                }
                if (BLL.User.CreateUser(UserName, Password, RoleId,Enable) > 0)
                {
                    int UserId = BLL.User.Find(UserName, Password).ID;
                    if (BLL.AdminModel.CreateAdminModel(Name, JobId, College, Mail, Phone, Sex, UserId) > 0)
                    {
                        context.Response.Write("SUCCESS");
                        context.Response.End();
                        return;
                    }
                }
                context.Response.Write("FAILD");
                context.Response.End();
                return;            }
            #endregion

            #region 删除管理员
            else if (dowhat == "delete")
            {
                string AdminModelID =context.Request["AdminModelID"];
                string UserId=context.Request["UserId"];
                if (BLL.Delete.Word("Tb_AdminModel", AdminModelID) > 0)
                {
                    BLL.Delete.Word("Tb_User", UserId);
                    context.Response.Write("SUCCESS");
                    context.Response.End();
                    return;
                }
                context.Response.Write("FAILD");
                context.Response.End();
                return;
            }
            #endregion

            #region 修改管理员信息
            else if (dowhat == "edit")
            {
                string Id = context.Request["Id"];
                string PasswordChecked = context.Request["PasswordChecked"];
                string Password = context.Request["Password"];
                Password = Utility.Tool.MD5(Password);
                string RoleId = context.Request["RoleId"];
                string UserId = context.Request["UserId"];
                string Name = context.Request["Name"];
                string JobId = context.Request["JobId"];
                string College = context.Request["College"];
                string Mail = context.Request["Mail"];
                string Phone = context.Request["Phone"];
                string Sex = context.Request["Sex"];
                string Enable = context.Request["Enable"];
                if (BLL.AdminModel.UpdateAdminModel(Id, Name, JobId, College, Mail, Phone, Sex, UserId) > 0 && BLL.User.UpdateUserEnable(UserId, Enable) > 0)
                {
                    if (BLL.User.UpdateUserRoleID(UserId, RoleId) > 0)
                    {
                        if (PasswordChecked == "true")
                        {
                            if (BLL.User.UpdateUserPassword(UserId, Password) > 0)
                            {
                                context.Response.Write("SUCCESS");
                                context.Response.End();
                                return;
                            }
                            else
                            {
                                context.Response.Write("FAILD");
                                context.Response.End();
                                return;
                            }

                        }
                        context.Response.Write("SUCCESS");
                        context.Response.End();
                        return;
                    }
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