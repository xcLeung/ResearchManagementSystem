using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class ProjectScore
    { 
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID{get;set;}

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID{get;set;}

        /// <summary>
        /// 裁判ID
        /// </summary>
        public int JudgeID{get;set;}

        /// <summary>
        /// 比赛模型ID
        /// </summary>
        public int MatchModelID{get;set;}

        /// <summary>
        /// 分数
        /// </summary>
        public int Score{get;set;}

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark{get;set;}
    }
}
