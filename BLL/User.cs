using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class User
    {
        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="UserNmae"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static int CreateUser(String UserNmae, String Password, int RoleId, String Enable)
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

            return DAL.Create.CreateOneReturnID(user);

        }

        public static Models.DB.User Find(String UserName, String Password)
        {
            #region 检查输入的合法性
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                return null;
            }
            #endregion

            #region 把得到数据组装成类的实例
            DataTable dt = DAL.User.QueryOne(UserName,Password);
            if (dt.Rows.Count > 0)
            {
                Models.DB.User user = new Models.DB.User();
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.Name = dt.Rows[0]["Name"].ToString();
                user.Password = dt.Rows[0]["Password"].ToString();
                user.RoleId = Convert.ToInt32(dt.Rows[0]["RoleID"]);
                user.Enable = Convert.ToBoolean(dt.Rows[0]["Enable"]);
            
                return user;
            }
            #endregion

            return null;
        }

        #region by云海
        /// <summary>
        /// 根据用户名获取记录
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static Models.DB.User Find(String UserName)
        {
            #region 检查输入的合法性
            if (string.IsNullOrEmpty(UserName))
            {
                return null;
            }
            #endregion

            #region 把得到数据组装成类的实例
            DataTable dt = DAL.User.QueryOne(UserName);
            if (dt.Rows.Count > 0)
            {
                Models.DB.User user = new Models.DB.User();
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.Name = dt.Rows[0]["Name"].ToString();
                user.Password = dt.Rows[0]["Password"].ToString();
                user.RoleId = Convert.ToInt32(dt.Rows[0]["RoleID"]);
                user.Enable = Convert.ToBoolean(dt.Rows[0]["Enable"]);

                return user;
            }
            #endregion
            return null;
        }
        /// <summary>
        /// 根据ID获取一条记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static Models.DB.User SelectUserOne(int Id)
        {
            Models.DB.User User = new Models.DB.User();
            System.Data.DataTable dt = DAL.Select.GetOne("Tb_User", Id);
            if (dt.Rows.Count > 0)
            {
                User.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                User.Name = dt.Rows[0]["Name"].ToString();
                User.Password = dt.Rows[0]["Password"].ToString();
                User.RoleId = Convert.ToInt32(dt.Rows[0]["RoleID"]);
                User.Enable = Convert.ToBoolean(dt.Rows[0]["Enable"]);
            }
            return User;
        }
        /// <summary>
        /// 更新角色Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public static int UpdateUserRoleID(string Id, string RoleId)
        {
            int id = 0;
            int roleId = 0;
            if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(RoleId))
            {
                return 0;
            }
            try
            {
                id = Convert.ToInt32(Id);
                roleId = Convert.ToInt32(RoleId);
            }
            catch { return 0; }
            #region 把输入组装成类的实例
            Models.DB.User User = BLL.User.SelectUserOne(id);
            User.ID = id;
            User.RoleId = roleId;
            #endregion

            return DAL.Update.ChangeSome(User, "ID");
        }
        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static int UpdateUserPassword(string Id, string Password)
        {
            int id = 0;
            if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(Password))
            {
                return 0;
            }
            try
            {
                id = Convert.ToInt32(Id);
            }
            catch { return 0; }
            #region 把输入组装成类的实例
            Models.DB.User User = BLL.User.SelectUserOne(id);
            User.ID = id;
            User.Password = Password;
            #endregion

            return DAL.Update.ChangeSome(User, "ID");
        }
        /// <summary>
        /// 更改Enable
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Enable"></param>
        /// <returns></returns>
        public static int UpdateUserEnable(string UserId, string Enable)
        {
            int userId = 0;
            bool enable = true;
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(Enable))
            {
                return 0;
            }
            try
            {
                userId = Convert.ToInt32(UserId);
                enable = Convert.ToBoolean(Enable);
            }
            catch
            {
                return 0;
            }
            Models.DB.User User = BLL.User.SelectUserOne(userId);
            User.Enable = enable;
            return DAL.Update.ChangeSome(User, "ID");
        }

        #endregion
    }
}
