using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StudentInfoModel
    {
        /// <summary>
        /// 单一条件查询Int型
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="ValueName"></param>
        /// <returns></returns>
        public static List<Models.DB.StudentInfoModel> FindByInt(String Value, String ValueName)
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

            List<Models.DB.StudentInfoModel> list = new List<Models.DB.StudentInfoModel>();


            DataTable dt = DAL.Select.QueryOne(valueInt, "Tb_StudentInfoModel", ValueName);

            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                Models.DB.StudentInfoModel e = new Models.DB.StudentInfoModel();
                e.StudentID = dt.Rows[i]["StudentID"].ToString();
                e.Name = dt.Rows[i]["Name"].ToString();
                e.Sex = dt.Rows[i]["Sex"].ToString();
                e.Major = dt.Rows[i]["Major"].ToString();
                e.InTimeYear = Convert.ToInt32(dt.Rows[i]["InTimeYear"]);
                e.School = dt.Rows[i]["School"].ToString();
                e.College = dt.Rows[i]["College"].ToString();
                e.UserId = Convert.ToInt32(dt.Rows[i]["UserId"]);
                e.Mail = dt.Rows[i]["Mail"].ToString();

                list.Add(e);

            }
            return list;
        }

        /// <summary>
        /// 单一条件String查询
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="ValueName"></param>
        /// <returns></returns>
        public static List<Models.DB.StudentInfoModel> FindByString(String Value, String ValueName)
        {
            #region 输入合法性检查
            if (String.IsNullOrEmpty(Value))
            {
                return null;
            }
            #endregion

            List<Models.DB.StudentInfoModel> list = new List<Models.DB.StudentInfoModel>();


            DataTable dt = DAL.Select.QueryOne(Value, "Tb_StudentInfoModel", ValueName);

            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                Models.DB.StudentInfoModel e = new Models.DB.StudentInfoModel();
                e.StudentID = dt.Rows[i]["StudentID"].ToString();
                e.Name = dt.Rows[i]["Name"].ToString();
                e.Sex = dt.Rows[i]["Sex"].ToString();
                e.Major = dt.Rows[i]["Major"].ToString();
                e.InTimeYear = Convert.ToInt32(dt.Rows[i]["InTimeYear"]);
                e.School = dt.Rows[i]["School"].ToString();
                e.College = dt.Rows[i]["College"].ToString();
                e.UserId = Convert.ToInt32(dt.Rows[i]["UserId"]);
                e.Mail = dt.Rows[i]["Mail"].ToString();

                list.Add(e);

            }
            return list;
        }


        public static double CountByEnableAndCollege(string Enable,String College)
        {
            bool enable = true;
            if (string.IsNullOrEmpty(Enable) || string.IsNullOrEmpty(College))
            {
                return 0;
            }
            try
            {
                enable = Convert.ToBoolean(Enable);
            }
            catch
            {
                return 0;
            }
            DataTable dt = DAL.Select.CountByEnableAndCollege(enable,College);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToDouble(dt.Rows[0][0]);
            }
            return 0;
        }

        public static List<Models.DB.StudentInfoModel> SelectByEnableAndCollege(string Enable, int pagesize, int currentPage,String College)
        {
            List<Models.DB.StudentInfoModel> list = new List<Models.DB.StudentInfoModel>();
            bool enable = true;
            if (string.IsNullOrEmpty(Enable) || String.IsNullOrEmpty(College))
            {
                return null;
            }
            try
            {
                enable = Convert.ToBoolean(Enable);
            }
            catch
            {
                return null;
            }
            System.Data.DataTable dt = DAL.Select.SelectByEnableAndCollege(pagesize, currentPage, "Tb_User.ID", "", College, enable);
            Models.DB.StudentInfoModel Student = new Models.DB.StudentInfoModel();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                Models.DB.StudentInfoModel e = new Models.DB.StudentInfoModel();
                e.StudentID = dt.Rows[i]["StudentID"].ToString();
                e.Name = dt.Rows[i]["Name"].ToString();
                e.Sex = dt.Rows[i]["Sex"].ToString();
                e.Major = dt.Rows[i]["Major"].ToString();
                e.InTimeYear = Convert.ToInt32(dt.Rows[i]["InTimeYear"]);
                e.School = dt.Rows[i]["School"].ToString();
                e.College = dt.Rows[i]["College"].ToString();
                e.UserId = Convert.ToInt32(dt.Rows[i]["UserId"]);
                e.Mail = dt.Rows[i]["Mail"].ToString();

                list.Add(e);
            }            
            return list;
        }


        public static int UpdateStudent(String StudentID, String Mail,String UserID,String InTimeYear)
        {
            if (String.IsNullOrEmpty(StudentID))
            {
                return 0;
            }
            int userid,intimeyear;
            try
            {
                userid = Convert.ToInt32(UserID);
                intimeyear = Convert.ToInt32(InTimeYear);
            }
            catch
            {
                return 0;
            }
            Models.DB.StudentInfoModel Student = new Models.DB.StudentInfoModel();
            Student.StudentID = StudentID;
            Student.Mail = Mail;
            Student.UserId = userid;
            Student.InTimeYear = intimeyear;
            return DAL.Update.ChangeSome(Student, "StudentID");
            

        }


        #region By云海
        /// <summary>
        /// 根据UserID获取记录
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static Models.DB.StudentInfoModel SelectOneByUserId(string UserID)
        {
            Models.DB.StudentInfoModel Student = new Models.DB.StudentInfoModel();
            int userId = 0;
            if (string.IsNullOrEmpty(UserID))
            {
                return Student;
            }
            try
            {
                userId = Convert.ToInt32(UserID);
            }
            catch
            {
                return Student;
            }
            Student.UserId = userId;
            System.Data.DataTable dt = DAL.Select.GetList(Student, "UserId");
            if (dt.Rows.Count > 0)
            {
                Student.StudentID = dt.Rows[0]["StudentID"].ToString();
                Student.Name = dt.Rows[0]["Name"].ToString();
                Student.Sex = dt.Rows[0]["Sex"].ToString();
                Student.Major = dt.Rows[0]["Major"].ToString();
                Student.InTimeYear = Convert.ToInt32(dt.Rows[0]["InTimeYear"]);
                Student.School = dt.Rows[0]["School"].ToString();
                Student.College = dt.Rows[0]["College"].ToString();
                Student.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                Student.Mail = dt.Rows[0]["Mail"].ToString();
            }
            return Student;
        }
        /// <summary>
        /// 根据Enable获取数据
        /// </summary>
        /// <param name="Enable"></param>
        /// <param name="pagesize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public static List<Models.DB.StudentInfoModel> SelectByEnable(string Enable, int pagesize, int currentPage)
        {
            List<Models.DB.StudentInfoModel> Students = new List<Models.DB.StudentInfoModel>();
            bool enable = true;
            if (string.IsNullOrEmpty(Enable))
            {
                return Students;
            }
            try
            {
                enable = Convert.ToBoolean(Enable);
            }
            catch
            {
                return Students;
            }
            Models.DB.StudentInfoModel Student = new Models.DB.StudentInfoModel();
            Models.DB.User User = new Models.DB.User();
            Models.DB.RoleInfoModel Model = BLL.RoleInfoModel.SelectRoleInfoModel("Tb_StudentInfoModel");
            List<Models.DB.Role> Roles = BLL.Role.SelectRole(Model.Id);
            if (Roles.Count <= 0)
            {
                return Students;
            }
            User.Enable = enable;
            User.RoleId = Roles[0].ID;
            string[] targets = { "RoleId", "Enable" };
            System.Data.DataTable dt = DAL.Select.GetSome(User, targets, pagesize, currentPage, "ID", "");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Student = BLL.StudentInfoModel.SelectOneByUserId(dt.Rows[i]["Id"].ToString());
                    Students.Add(Student);
                }
            }
            return Students;
        }


        public static double CountByEnable(string Enable)
        {
            bool enable = true;
            if (string.IsNullOrEmpty(Enable))
            {
                return 0;
            }
            try
            {
                enable = Convert.ToBoolean(Enable);
            }
            catch
            {
                return 0;
            }
            Models.DB.StudentInfoModel Student = new Models.DB.StudentInfoModel();
            Models.DB.User User = new Models.DB.User();
            User.Enable = enable;
            return DAL.Select.GetCount(Student, User, "Tb_StudentInfoModel.UserId=Tb_User.ID", "Enable");
        }

        #endregion
    }
}
