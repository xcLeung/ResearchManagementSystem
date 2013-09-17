using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class InnovationWorksInfo
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
        /// 项目目的
        /// </summary>
        public String Purpose {get; set;}

        /// <summary>
        /// 参考文献
        /// </summary>
        public String References {get; set;}

        /// <summary>
        /// 基本内容
        /// </summary>
        public String BaseContent {get; set;}

        /// <summary>
        /// 关键问题
        /// </summary>
        public String KeyProblem {get; set;}

        /// <summary>
        /// 项目基础
        /// </summary>
        public String ProjectBasic {get; set;}

        /// <summary>
        /// 详细计划
        /// </summary>
        public String SpecificPlan {get; set;}

        /// <summary>
        /// 技术路线
        /// </summary>
        public String PracticalsStep {get; set;}

        /// <summary>
        /// 人员分工
        /// </summary>
        public String PersonnelDivision {get; set;}

        /// <summary>
        /// 项目计划
        /// </summary>
        public String ProjectPlan {get; set;}

        /// <summary>
        /// 独特之处
        /// </summary>
        public String Features {get; set;}

        /// <summary>
        /// 预期成果
        /// </summary>
        public String Expection {get; set;}

        /// <summary>
        /// 经费预算
        /// </summary>
        public String Budget {get; set;}

        /// <summary>
        /// 二级分类
        /// </summary>
        public String SecondCategories {get; set;}

        /// <summary>
        /// 一级分类
        /// </summary>
        public String Category {get; set;}


    }
}
