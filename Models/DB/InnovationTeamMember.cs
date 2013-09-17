using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class InnovationTeamMember
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        public int Id {get; set;}

        /// <summary>
        /// 入学年份
        /// </summary>
        public int InTimeYear {get; set;}

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID {get; set;}

        /// <summary>
        /// 项目名字
        /// </summary>
        public String Name {get; set;}

        /// <summary>
        /// 学生学号
        /// </summary>
        public String StudentID {get; set;}

        /// <summary>
        /// 专业
        /// </summary>
        public String Major {get; set;}

        /// <summary>
        /// 学院
        /// </summary>
        public String College {get; set;}

        /// <summary>
        /// 电话
        /// </summary>
        public String Phone {get; set;}

        /// <summary>
        /// 邮箱
        /// </summary>
        public String Mail {get; set;}
        
        /// <summary>
        /// 经验经历
        /// </summary>
        public String Experience {get; set;}
    }
}
