using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Create
    {

        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="UserNmae"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static int CreateUser(String UserNmae,String Password,int RoleId,String Enable)
        {
            Boolean enable;
            #region 检查输入的合法性
            if (string.IsNullOrEmpty(UserNmae) || string.IsNullOrEmpty(Password))
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
            #endregion

            #region 把数据组装成一个对象
            Models.DB.User user = new Models.DB.User();
            user.Name = UserNmae;
            user.Password = Password;
            user.RoleId = RoleId;
            user.Enable = enable;
            #endregion

            return DAL.Create.CreateOne(user);

        }

        /// <summary>
        /// 插入学生基本信息
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="Major"></param>
        /// <param name="InTimeYear"></param>
        /// <param name="School"></param>
        /// <param name="College"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
         public static int CreateStudentModelInfo(String StudentID,String Name,String Sex,String Major,String InTimeYear,String School,String College,String Mail,int UserId)
        {

            #region 输入合法性检测
            if (string.IsNullOrEmpty(StudentID) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Sex) || string.IsNullOrEmpty(Major) || string.IsNullOrEmpty(School) ||
                string.IsNullOrEmpty(College) || string.IsNullOrEmpty(Mail))
            {
                return 0;
            }
  
            int intimeyear;
            try
            {
                intimeyear = Convert.ToInt32(InTimeYear);
            }
            catch
            {
                return 0;
            }
            #endregion


            #region 把输入组装成类的实例
            Models.DB.StudentInfoModel student = new Models.DB.StudentInfoModel();
            student.StudentID = StudentID;
            student.Name = Name;
            student.Sex = Sex;
            student.Major = Major;
            student.School = School;
            student.College = College;
            student.Mail = Mail;
            student.UserId = UserId;
            student.InTimeYear = intimeyear;
            
            #endregion

            return DAL.Create.CreateOne(student);
        }
    }
}
