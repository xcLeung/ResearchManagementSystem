using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Role
    {
        /// <summary>
        /// 根据角色名称查找角色的ID
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public static int Find(String RoleName)
        {
            #region 输入合法性检测
            if (string.IsNullOrEmpty(RoleName))
            {
                return 0;
            }
            #endregion

            #region 把得到数据组装成类的实例
            DataTable dt = DAL.Role.QueryOne(RoleName);
            if (dt.Rows.Count > 0)
            {
                Models.DB.Role role = new Models.DB.Role();
                role.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                role.Name = dt.Rows[0]["Name"].ToString();
                role.RoleValue = dt.Rows[0]["RoleValue"].ToString();
                role.Enable = Convert.ToBoolean(dt.Rows[0]["Enable"]);
                role.RoleModelID = Convert.ToInt32(dt.Rows[0]["RoleModelID"]);
                return role.ID;
            }
            #endregion

            return 0;
        }


        #region By云海
        /// <summary>
        /// 根据模型Id，获取记录
        /// </summary>
        /// <param name="RoleModelID"></param>
        /// <returns></returns>
        public static List<Models.DB.Role> SelectRole(int RoleModelID)
        {
            List<Models.DB.Role> Roles = new List<Models.DB.Role>();
            Models.DB.Role Role = new Models.DB.Role();
            Role.RoleModelID = RoleModelID;
            System.Data.DataTable dt = DAL.Select.GetList(Role, "RoleModelID");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Role = new Models.DB.Role();
                    Role.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    Role.Name = dt.Rows[i]["Name"].ToString();
                    Role.RoleValue = dt.Rows[i]["RoleValue"].ToString();
                    Role.Enable = Convert.ToBoolean(dt.Rows[i]["Enable"]);
                    Role.RoleModelID = Convert.ToInt32(dt.Rows[i]["RoleModelID"]);
                    Roles.Add(Role);
                }
            }
            return Roles;
        }
        /// <summary>
        /// 根据ID获取一条信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Models.DB.Role SelectRoleOne(int ID)
        {
            Models.DB.Role Role = new Models.DB.Role();
            System.Data.DataTable dt = DAL.Select.GetOne("Tb_Role", ID);
            if (dt.Rows.Count > 0)
            {
                Role.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                Role.Name = dt.Rows[0]["Name"].ToString();
                Role.RoleValue = dt.Rows[0]["RoleValue"].ToString();
                Role.Enable = Convert.ToBoolean(dt.Rows[0]["Enable"]);
                Role.RoleModelID = Convert.ToInt32(dt.Rows[0]["RoleModelID"]);
            }
            return Role;
        }
        #endregion

    }
}
