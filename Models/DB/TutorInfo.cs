using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class TutorInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id {get; set;}

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age {get; set;}

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID {get; set;}

        /// <summary>
        /// 姓名
        /// </summary>
        public String Name {get; set;}

        /// <summary>
        /// 工作职称
        /// </summary>
        public String Work {get; set;}

        /// <summary>
        /// 研究方向
        /// </summary>
        public String Reasearch {get; set;}

        /// <summary>
        /// 成就
        /// </summary>
        public String Achieves {get; set;}

        /// <summary>
        /// 推荐人意见
        /// </summary>
        public String Recommendation {get; set;}
    }
}
