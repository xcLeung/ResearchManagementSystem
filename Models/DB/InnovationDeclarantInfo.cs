using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class InnovationDeclarantInfo
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        public int Id {get; set;}

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID {get; set;}

        /// <summary>
        /// 电话
        /// </summary>
        public String Phone {get; set;}

        /// <summary>
        /// 经验经历
        /// </summary>
        public String Experience {get; set;}
    }
}
