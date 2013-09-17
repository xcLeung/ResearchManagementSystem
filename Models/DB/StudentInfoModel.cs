using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class StudentInfoModel
    {
        /// <summary>
        /// 学生学号
        /// </summary>
        public String StudentID { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public String Sex { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public String Major { get; set; }


        /// <summary>
        /// 入学年份
        /// </summary>
        public int InTimeYear { get; set; }

        /// <summary>
        /// 所在院校
        /// </summary>
        public String School { get; set; }

        /// <summary>
        /// 学院
        /// </summary>
        public String College { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 学生邮箱
        /// </summary>
        public String Mail { get; set; }
    }
}
