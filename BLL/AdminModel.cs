using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AdminModel
    {
        /// <summary>
        /// 插入管理员基本信息
        /// </summary>
        public static int CreateAdminModel(string Name, string JobId, string College, string Mail, string Phone, string Sex, int UserID)
        {

            #region 输入合法性检测
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Sex) || string.IsNullOrEmpty(JobId) || string.IsNullOrEmpty(Phone) ||
                string.IsNullOrEmpty(College) || string.IsNullOrEmpty(Mail))
            {
                return 0;
            }
            if (Name.Length > 255 || Sex.Length > 255 || JobId.Length > 255 || Phone.Length > 255 || College.Length > 255 || Mail.Length > 255)
            {
                return 0;
            }
            #endregion

            #region 把输入组装成类的实例
            Models.DB.AdminModel admin = new Models.DB.AdminModel();
            admin.Name = Name;
            admin.JobId = JobId;
            admin.College = College;
            admin.Mail = Mail;
            admin.Phone = Phone;
            admin.Sex = Sex;
            admin.UserId = UserID;
            #endregion

            return DAL.Create.CreateOne(admin);
        }

        /// <summary>
        /// 根据Id选择一条记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static Models.DB.AdminModel SelectAdminModelOne(int Id)
        {
            Models.DB.AdminModel Admin = null;
            System.Data.DataTable dt = DAL.Select.GetOne("Tb_AdminModel", Id);
            if (dt.Rows.Count > 0)
            {
                Admin = new Models.DB.AdminModel();
                Admin.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                Admin.Name = dt.Rows[0]["Name"].ToString();
                Admin.JobId = dt.Rows[0]["JobID"].ToString();
                Admin.College = dt.Rows[0]["College"].ToString();
                Admin.Mail = dt.Rows[0]["Mail"].ToString();
                Admin.Phone = dt.Rows[0]["Phone"].ToString();
                Admin.Sex = dt.Rows[0]["Sex"].ToString();
                Admin.UserId = Convert.ToInt32(dt.Rows[0]["UserID"]);
            }
            return Admin;
        }

        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="JobId"></param>
        /// <param name="College"></param>
        /// <param name="Mail"></param>
        /// <param name="Phone"></param>
        /// <param name="Sex"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static int UpdateAdminModel(string Id, string Name, string JobId, string College, string Mail, string Phone, string Sex, string UserID)
        {
            #region 输入合法性检测
            int id = 0;
            int userId = 0;
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Sex) || string.IsNullOrEmpty(JobId) || string.IsNullOrEmpty(Phone) ||
                string.IsNullOrEmpty(College) || string.IsNullOrEmpty(Mail) || string.IsNullOrEmpty(UserID))
            {
                return 0;
            }
            try
            {
                id = Convert.ToInt32(Id);
                userId = Convert.ToInt32(UserID);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把输入组装成类的实例
            Models.DB.AdminModel admin = new Models.DB.AdminModel();
            admin.Id = id;
            admin.Name = Name;
            admin.JobId = JobId;
            admin.College = College;
            admin.Mail = Mail;
            admin.Phone = Phone;
            admin.Sex = Sex;
            admin.UserId = userId;
            #endregion

            return DAL.Update.ChangeSome(admin, "Id");
        }

        /// <summary>
        /// 统计表中数据数量
        /// </summary>
        /// <returns></returns>
        public static double Count()
        {
            return DAL.Select.GetCount("Tb_AdminModel");
        }
        /// <summary>
        /// 获取一页记录
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<Models.DB.AdminModel> SelectOnePage(int Page, int PageSize)
        {
            List<Models.DB.AdminModel> Admins = new List<Models.DB.AdminModel>();
            Models.DB.AdminModel Admin;
            System.Data.DataTable dt = DAL.Select.GetSome("Tb_AdminModel", PageSize, Page, "Id", "");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Admin = new Models.DB.AdminModel();
                    Admin.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    Admin.Name = dt.Rows[i]["Name"].ToString();
                    Admin.JobId = dt.Rows[i]["JobID"].ToString();
                    Admin.College = dt.Rows[i]["College"].ToString();
                    Admin.Mail = dt.Rows[i]["Mail"].ToString();
                    Admin.Phone = dt.Rows[i]["Phone"].ToString();
                    Admin.Sex = dt.Rows[i]["Sex"].ToString();
                    Admin.UserId = Convert.ToInt32(dt.Rows[i]["UserID"]);
                    Admins.Add(Admin);
                }

            }
            return Admins;
        }


        #region lxc
        /// <summary>
        /// 根据学院统计管理员数量
        /// </summary>
        /// <param name="College"></param>
        /// <returns></returns>
        public static double Count(String College)
        {
            if (String.IsNullOrEmpty(College))
            {
                return 0;
            }
            return DAL.Select.CountByCollege(College,"Tb_AdminModel");
        }

        /// <summary>
        /// 根据学院选择一页记录
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<Models.DB.AdminModel> SelectOnePage(int Page, int PageSize,String College)
        {
            if (String.IsNullOrEmpty(College))
            {
                return null;
            }
            List<Models.DB.AdminModel> Admins = new List<Models.DB.AdminModel>();
            Models.DB.AdminModel Admin;
            System.Data.DataTable dt = DAL.Select.SearchByCollege("Tb_AdminModel", PageSize, Page, "Id", "", College);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Admin = new Models.DB.AdminModel();
                    Admin.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    Admin.Name = dt.Rows[i]["Name"].ToString();
                    Admin.JobId = dt.Rows[i]["JobID"].ToString();
                    Admin.College = dt.Rows[i]["College"].ToString();
                    Admin.Mail = dt.Rows[i]["Mail"].ToString();
                    Admin.Phone = dt.Rows[i]["Phone"].ToString();
                    Admin.Sex = dt.Rows[i]["Sex"].ToString();
                    Admin.UserId = Convert.ToInt32(dt.Rows[i]["UserID"]);
                    Admins.Add(Admin);
                }

            }
            return Admins;
        }


        public static Models.DB.AdminModel SelectAdminModelByUserID(String UserID)
        {
            int userid;
            try
            {
                userid = Convert.ToInt32(UserID);
            }
            catch
            {
                return null;
            }
            Models.DB.AdminModel Admin = null;
            System.Data.DataTable dt = DAL.Select.QueryOne(userid, "Tb_AdminModel", "UserId");
            if (dt.Rows.Count > 0)
            {
                Admin = new Models.DB.AdminModel();
                Admin.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                Admin.Name = dt.Rows[0]["Name"].ToString();
                Admin.JobId = dt.Rows[0]["JobID"].ToString();
                Admin.College = dt.Rows[0]["College"].ToString();
                Admin.Mail = dt.Rows[0]["Mail"].ToString();
                Admin.Phone = dt.Rows[0]["Phone"].ToString();
                Admin.Sex = dt.Rows[0]["Sex"].ToString();
                Admin.UserId = Convert.ToInt32(dt.Rows[0]["UserID"]);
            }
            return Admin;
        }
        #endregion
    }
}
