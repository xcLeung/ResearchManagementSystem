using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class CupWorksSurvey
    {
      

        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id {get; set;}

        /// <summary>
        /// 指向项目的ID
        /// </summary>
        public int ProjectID  {get; set;}

        /// <summary>
        /// 调查方式
        /// </summary>
        public String SurveyWay {get; set;}

        /// <summary>
        /// 主要调查单位
        /// </summary>
        public String SurveyUnits {get; set;}

        /// <summary>
        /// 分类
        /// </summary>
        public String Categories {get; set;}

        /// <summary>
        /// 作品目的
        /// </summary>
        public String Purpose {get; set;}

        /// <summary>
        /// 独特之处，优越性
        /// </summary>
        public String Features {get; set;}

        /// <summary>
        /// 应用价值
        /// </summary>
        public String ApplyValue {get; set;}

        /// <summary>
        /// 获得奖项
        /// </summary>
        public String ReceivedAwards {get; set;}

         /// <summary>
        /// 文章摘要
        /// </summary>
        public String PaperDigest {get; set;}

        /// <summary>
        /// 参考论文
        /// </summary>
        public String References {get; set;}

        /// <summary>
        /// 国内外研究的水平概况
        /// </summary>
        public String SameResearchLevel {get; set;}

    }
}
