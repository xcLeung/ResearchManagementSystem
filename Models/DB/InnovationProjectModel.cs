using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class InnovationProjectModel
    {
   
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id {get; set;}
        
        

        /// <summary>
        /// 杯赛信息表ID
        /// </summary>
        public int MatchID {get; set;}

        /// <summary>
        /// 申报种类
        /// </summary>
        public String DeclarationType {get; set;}

        /// <summary>
        /// 申报日期
        /// </summary>
        public DateTime DeclarationDate {get; set;}

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark {get; set;}

        /// <summary>
        /// 项目开始时间
        /// </summary>
        public DateTime StartTime {get; set;}

        /// <summary>
        /// 项目结束时间
        /// </summary>
        public DateTime EndTime {get; set;}

        /// <summary>
        /// 项目名称
        /// </summary>
        public String Name {get; set;}

        /// <summary>
        /// 中期报告
        /// </summary>
        public String InterimReport {get; set;}

        /// <summary>
        /// 提交成果
        /// </summary>
        public String SubmitAchievement {get; set;}

        /// <summary>
        /// 管理依据
        /// </summary>
        public String Managementbasis {get; set;}

        /// <summary>
        /// pdf保存地址
        /// </summary>
        public String PdfUrl {get; set;}

        /// <summary>
        /// 项目状态
        /// </summary>
        public String Statu {get; set;}

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID {get; set;}


        /// <summary>
        /// 论文文档
        /// </summary>
        public String ProjectDoc { get; set; }
    }
}
