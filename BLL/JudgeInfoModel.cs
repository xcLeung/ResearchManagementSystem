using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class JudgeInfoModel
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        public static int CreateJudgeInfoModel(string Name, string Address, string WorkUnits, string Title,
             string College, string Mail, string Phone, string Sex, string Background,
            string Research, string JobId, string CampusId, int UserId)
        {
            #region 检查输入
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Address) ||
                string.IsNullOrEmpty(WorkUnits) || string.IsNullOrEmpty(Title) ||
                string.IsNullOrEmpty(Background) || string.IsNullOrEmpty(Research) ||
                string.IsNullOrEmpty(JobId) || string.IsNullOrEmpty(CampusId) ||
                string.IsNullOrEmpty(College) || string.IsNullOrEmpty(Mail) ||
                string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Sex))
            {
                return 0;
            }
            if (Name.Length >= 255 || Address.Length >= 255 || WorkUnits.Length >= 255 || Title.Length >= 255 || Background.Length >= 255 || Phone.Length >= 255 ||
                Research.Length >= 255 || JobId.Length >= 255 || CampusId.Length >= 255 || College.Length >= 255 || Mail.Length >= 255 || Sex.Length >= 255)
            {
                return 0;
            }
            #endregion
            #region 封装数据
            Models.DB.JudgeInfoModel Judge = new Models.DB.JudgeInfoModel();
            Judge.Address = Address;
            Judge.Background = Background;
            Judge.CampusId = CampusId;
            Judge.College = College;
            Judge.JobId = JobId;
            Judge.Mail = Mail;
            Judge.Phone = Phone;
            Judge.RealName = Name;
            Judge.Research = Research;
            Judge.Sex = Sex;
            Judge.Title = Title;
            Judge.UserId = UserId;
            Judge.WorkUnits = WorkUnits;
            return DAL.Create.CreateOne(Judge);
            #endregion

        }
        /// <summary>
        /// 统计表中数据数量
        /// </summary>
        /// <returns></returns>
        public static double Count()
        {
            return DAL.Select.GetCount("Tb_JudgeInfoModel");
        }
        /// <summary>
        /// 获取一页记录
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<Models.DB.JudgeInfoModel> SelectOnePage(int Page, int PageSize)
        {
            List<Models.DB.JudgeInfoModel> Judges = new List<Models.DB.JudgeInfoModel>();
            Models.DB.JudgeInfoModel Judge = new Models.DB.JudgeInfoModel();
            System.Data.DataTable dt = DAL.Select.GetSome("Tb_JudgeInfoModel", PageSize, Page, "Id", "");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Judge = new Models.DB.JudgeInfoModel();
                    Judge.WorkUnits = dt.Rows[i]["WorkUnits"].ToString();
                    Judge.Address = dt.Rows[i]["Address"].ToString();
                    Judge.Background = dt.Rows[i]["Background"].ToString();
                    Judge.CampusId = dt.Rows[i]["CampusId"].ToString();
                    Judge.College = dt.Rows[i]["College"].ToString();
                    Judge.JobId = dt.Rows[i]["JobId"].ToString();
                    Judge.Mail = dt.Rows[i]["Mail"].ToString();
                    Judge.Phone = dt.Rows[i]["Phone"].ToString();
                    Judge.RealName = dt.Rows[i]["RealName"].ToString();
                    Judge.Research = dt.Rows[i]["Research"].ToString();
                    Judge.Sex = dt.Rows[i]["Sex"].ToString();
                    Judge.Title = dt.Rows[i]["Title"].ToString();
                    Judge.UserId = Convert.ToInt32(dt.Rows[i]["UserId"]);
                    Judge.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    Judges.Add(Judge);
                }

            }
            return Judges;
        }

        public static Models.DB.JudgeInfoModel SelectOne(int id)
        {
            Models.DB.JudgeInfoModel Judge = null;
            System.Data.DataTable dt = DAL.Select.GetOne("Tb_JudgeInfoModel", id);
            if (dt.Rows.Count > 0)
            {
                Judge = new Models.DB.JudgeInfoModel();
                Judge.WorkUnits = dt.Rows[0]["WorkUnits"].ToString();
                Judge.Address = dt.Rows[0]["Address"].ToString();
                Judge.Background = dt.Rows[0]["Background"].ToString();
                Judge.CampusId = dt.Rows[0]["CampusId"].ToString();
                Judge.College = dt.Rows[0]["College"].ToString();
                Judge.JobId = dt.Rows[0]["JobId"].ToString();
                Judge.Mail = dt.Rows[0]["Mail"].ToString();
                Judge.Phone = dt.Rows[0]["Phone"].ToString();
                Judge.RealName = dt.Rows[0]["RealName"].ToString();
                Judge.Research = dt.Rows[0]["Research"].ToString();
                Judge.Sex = dt.Rows[0]["Sex"].ToString();
                Judge.Title = dt.Rows[0]["Title"].ToString();
                Judge.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                Judge.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
            }
            return Judge;
        }

        public static int UpdateJudgeInfoModel(string Id, string Name, string Address, string WorkUnits, string Title,
             string College, string Mail, string Phone, string Sex, string Background,
            string Research, string JobId, string CampusId, string UserId)
        {
            int id;
            int userId;
            #region 检查输入
            if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(Name)  ||
                string.IsNullOrEmpty(JobId) || string.IsNullOrEmpty(CampusId) ||
                string.IsNullOrEmpty(College) ||
               string.IsNullOrEmpty(Sex) || string.IsNullOrEmpty(UserId))
            {
                return 0;
            }
            try
            {
                id = Convert.ToInt32(Id);
                userId = Convert.ToInt32(UserId);
            }
            catch
            {
                return 0;
            }
            if (Name.Length >= 255 ||    
                 JobId.Length >= 255 || CampusId.Length >= 255 || College.Length >= 255 || Sex.Length >= 255)
            {
                return 0;
            }
            #endregion
            #region 封装数据
            Models.DB.JudgeInfoModel Judge = new Models.DB.JudgeInfoModel();
            Judge.Address = Address;
            Judge.Background = Background;
            Judge.CampusId = CampusId;
            Judge.College = College;
            Judge.JobId = JobId;
            Judge.Mail = Mail;
            Judge.Phone = Phone;
            Judge.RealName = Name;
            Judge.Research = Research;
            Judge.Sex = Sex;
            Judge.Title = Title;
            Judge.UserId = userId;
            Judge.WorkUnits = WorkUnits;
            Judge.Id = id;
            return DAL.Update.ChangeSome(Judge, "Id");
            #endregion
        }


        public static List<Models.DB.JudgeInfoModel> FindByInt(String Value, String ValueName)
        {
            #region 输入合法性检查
            int valueInt;
            try
            {
                valueInt = Convert.ToInt32(Value);
            }
            catch
            {
                return null;
            }
            #endregion

            List<Models.DB.JudgeInfoModel> list = new List<Models.DB.JudgeInfoModel>();


            DataTable dt = DAL.Select.QueryOne(valueInt, "Tb_JudgeInfoModel", ValueName);

            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                Models.DB.JudgeInfoModel Judge = new Models.DB.JudgeInfoModel();
                Judge.WorkUnits = dt.Rows[i]["WorkUnits"].ToString();
                Judge.Address = dt.Rows[i]["Address"].ToString();
                Judge.Background = dt.Rows[i]["Background"].ToString();
                Judge.CampusId = dt.Rows[i]["CampusId"].ToString();
                Judge.College = dt.Rows[i]["College"].ToString();
                Judge.JobId = dt.Rows[i]["JobId"].ToString();
                Judge.Mail = dt.Rows[i]["Mail"].ToString();
                Judge.Phone = dt.Rows[i]["Phone"].ToString();
                Judge.RealName = dt.Rows[i]["RealName"].ToString();
                Judge.Research = dt.Rows[i]["Research"].ToString();
                Judge.Sex = dt.Rows[i]["Sex"].ToString();
                Judge.Title = dt.Rows[i]["Title"].ToString();
                Judge.UserId = Convert.ToInt32(dt.Rows[i]["UserId"]);
                Judge.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                list.Add(Judge);

            }
            return list;
        }


        #region lxc
        /// <summary>
        /// 根据学员统计评委人数
        /// </summary>
        /// <param name="College"></param>
        /// <returns></returns>
        public static double Count(String College)
        {
            if (String.IsNullOrEmpty(College))
            {
                return 0;
            }
            return DAL.Select.CountByCollege(College,"Tb_JudgeInfoModel");
        }


        public static List<Models.DB.JudgeInfoModel> SelectOnePage(int Page, int PageSize,String College)
        {
            if (String.IsNullOrEmpty(College))
            {
                return null;
            }
            List<Models.DB.JudgeInfoModel> Judges = new List<Models.DB.JudgeInfoModel>();
            Models.DB.JudgeInfoModel Judge = new Models.DB.JudgeInfoModel();
            System.Data.DataTable dt = DAL.Select.SearchByCollege("Tb_JudgeInfoModel", PageSize, Page, "Id", "", College);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Judge = new Models.DB.JudgeInfoModel();
                    Judge.WorkUnits = dt.Rows[i]["WorkUnits"].ToString();
                    Judge.Address = dt.Rows[i]["Address"].ToString();
                    Judge.Background = dt.Rows[i]["Background"].ToString();
                    Judge.CampusId = dt.Rows[i]["CampusId"].ToString();
                    Judge.College = dt.Rows[i]["College"].ToString();
                    Judge.JobId = dt.Rows[i]["JobId"].ToString();
                    Judge.Mail = dt.Rows[i]["Mail"].ToString();
                    Judge.Phone = dt.Rows[i]["Phone"].ToString();
                    Judge.RealName = dt.Rows[i]["RealName"].ToString();
                    Judge.Research = dt.Rows[i]["Research"].ToString();
                    Judge.Sex = dt.Rows[i]["Sex"].ToString();
                    Judge.Title = dt.Rows[i]["Title"].ToString();
                    Judge.UserId = Convert.ToInt32(dt.Rows[i]["UserId"]);
                    Judge.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    Judges.Add(Judge);
                }

            }
            return Judges;
        }

        #endregion
    }
}
