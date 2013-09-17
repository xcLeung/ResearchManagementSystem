using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchManagementSystem.Admin.Judge
{
    /// <summary>
    /// AjaxAction 的摘要说明
    /// </summary>
    public class AjaxAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string dowhat=context.Request["dowhat"];
            #region 添加评委信息
            if (dowhat == "add")
            {
                int RoleId = Convert.ToInt32(context.Request["RoleId"]);
                string UserName = context.Request["UserName"];
                string Password = context.Request["Password"];
                Password = Utility.Tool.MD5(Password);
                string Name = context.Request["Name"];
                string Address = context.Request["Address"];
                string WorkUnits = context.Request["WorkUnits"];
                string Title = context.Request["Title"];
                string Background = context.Request["Background"];
                string Research = context.Request["Research"];
                string JobId = context.Request["JobId"];
                string CampusId = context.Request["CampusId"];
                string College = context.Request["College"];
                string Mail = context.Request["Mail"];
                string Phone = context.Request["Phone"];
                string Sex = context.Request["Sex"];
                string Enable = context.Request["Enable"];
                if (BLL.User.Find(UserName) != null)
                {
                    context.Response.Write("EXIST");
                    context.Response.End();
                    return;
                }
                if (BLL.User.CreateUser(UserName, Password, RoleId, Enable) > 0)
                {
                    int UserId = BLL.User.Find(UserName,Password).ID;
                    if (BLL.JudgeInfoModel.CreateJudgeInfoModel(Name, Address, WorkUnits, Title, College, Mail,
                        Phone, Sex, Background, Research, JobId, CampusId, UserId) > 0)
                    {
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

            #region 删除评委信息
            else if(dowhat=="delete")
            {
                string Id = context.Request["ID"];
                string UserId = context.Request["UserId"];
                if (BLL.Delete.Word("Tb_JudgeInfoModel",Id) > 0)
                {
                    BLL.Delete.Word("Tb_User",UserId);
                    context.Response.Write("SUCCESS");
                    context.Response.End();
                    return;
                }
                context.Response.Write("FAILD");
                context.Response.End();
                return;
            }
            #endregion

            #region 修改评委信息
            else if(dowhat=="update")
            {
                string Id=context.Request["Id"];
                string PasswordChecked = context.Request["PasswordChecked"];
                string Password = context.Request["Password"];
                Password = Utility.Tool.MD5(Password);
                string Name = context.Request["Name"];
                string Address = context.Request["Address"];
                string WorkUnits = context.Request["WorkUnits"];
                string Title = context.Request["Title"];
                string Background = context.Request["Background"];
                string Research = context.Request["Research"];
                string JobId = context.Request["JobId"];
                string CampusId = context.Request["CampusId"];
                string College = context.Request["College"];
                string Mail = context.Request["Mail"];
                string Phone = context.Request["Phone"];
                string Sex = context.Request["Sex"];
                string UserId = context.Request["UserId"];
                string Enable = context.Request["Enable"];
                if (BLL.JudgeInfoModel.UpdateJudgeInfoModel(Id, Name, Address, WorkUnits, Title,
                     College, Mail, Phone, Sex, Background, Research, JobId, CampusId, UserId) > 0
                     && BLL.User.UpdateUserEnable(UserId, Enable) > 0)
                {
                    if (PasswordChecked == "true")
                    {
                        if (BLL.User.UpdateUserPassword(UserId, Password) > 0)
                        {
                            context.Response.Write("SUCCESS");
                            context.Response.End();
                            return;
                        }
                        else {
                            context.Response.Write("FAILD");
                            context.Response.End();
                            return;
                        }
                        
                    }
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