using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class CupWorksInfo
    {
        
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID {get; set;}

        /// <summary>
        /// 指向项目的ID
        /// </summary>
        public int ProjectID {get; set; }

        /// <summary>
        /// 二级分类
        /// </summary>
        public String SecondCategories {get; set;}

        /// <summary>
        /// 三级分类
        /// </summary>
        public String ThirdCategories {get; set;}

        /// <summary>
        /// 四级分类
        /// </summary>
        public String FourthCategories {get; set;}

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
        /// 文章摘要
        /// </summary>
        public String PaperDigest {get; set;}

        /// <summary>
        /// 获得奖项
        /// </summary>
        public String ReceivedAwards {get; set;}

        /// <summary>
        /// 鉴定结果
        /// </summary>
        public String TestResult {get; set;}

        /// <summary>
        /// 参考论文
        /// </summary>
        public String References {get; set;}

        /// <summary>
        /// 材料清单
        /// </summary>
        public String MaterialsList {get; set;}

        /// <summary>
        /// 国内外研究的水平概况
        /// </summary>
        public String SameResearchLevel {get; set;}
 
    }
}
