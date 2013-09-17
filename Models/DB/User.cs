using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }


        /// <summary>
        /// 用户名
        /// </summary>
        public String Name { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public Boolean Enable { get; set; }
    }
}
