using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class CupTeamMemberInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID {get;set;}

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age {get;set;}

        /// <summary>
        /// 指向的项目ID
        /// </summary>
        public int ProjectID {get;set;}

        /// <summary>
        /// 名字
        /// </summary>
        public String Name {get;set;}


        /// <summary>
        /// 学历
        /// </summary>
        public String BackGround {get;set;}

        /// <summary>
        /// 工作单位
        /// </summary>
        public String WorkUnit {get;set;}

        /// <summary>
        /// 性别
        /// </summary>
        public String Sex {get;set;}
    }
}
