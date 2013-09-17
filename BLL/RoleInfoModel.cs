using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RoleInfoModel
    {
        /// <summary>
        /// 根据表名获取记录
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static Models.DB.RoleInfoModel SelectRoleInfoModel(string TableName)
        {
            Models.DB.RoleInfoModel RoleInfo = new Models.DB.RoleInfoModel();
            RoleInfo.TableName = TableName;
            System.Data.DataTable dt = DAL.Select.GetList(RoleInfo, "TableName");
            if (dt.Rows.Count > 0)
            { 
                RoleInfo.Id=Convert.ToInt32( dt.Rows[0]["Id"]);
                RoleInfo.Name = dt.Rows[0]["Name"].ToString();
                RoleInfo.TableName = dt.Rows[0]["TableName"].ToString();
                RoleInfo.RoleId = Convert.ToInt32(dt.Rows[0]["RoleId"]);
                RoleInfo.AdminDirectory = dt.Rows[0]["AdminDirectory"].ToString();
            }
            return RoleInfo;
        }
    }
}
