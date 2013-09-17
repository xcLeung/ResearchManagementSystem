using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class Role
    {

        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 角色名字
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 角色权限值
        /// </summary>
        public String RoleValue {get; set;}

        /// <summary>
        /// 角色是否可用
        /// </summary>
        public Boolean Enable { get; set; }

        /// <summary>
        /// 角色模型ID
        /// </summary>
        public int RoleModelID { get; set; }
    }
}
