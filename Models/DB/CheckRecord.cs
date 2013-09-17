using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class CheckRecord
    {
      
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id {get; set;}

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID {get; set;}

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime Time {get; set;}

        /// <summary>
        /// 审核者
        /// </summary>
        public String Checker {get; set;}

        /// <summary>
        /// 审核原因
        /// </summary>
        public String Reason {get; set;}

        /// <summary>
        /// 审核前的状态
        /// </summary>
        public String BeforeStatus {get; set;}


        /// <summary>
        /// 审核后的状态
        /// </summary>
        public String AfterStatus {get; set;}

        /// <summary>
        /// 项目模型ID
        /// </summary>
        public int MatchModelID { get; set; }
    }
}
