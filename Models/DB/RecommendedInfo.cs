using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class RecommendedInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID {get; set;}

        /// <summary>
        /// 指向项目的ID
        /// </summary>
        public int ProjectID {get; set;}

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age {get; set;}

        /// <summary>
        /// 名字
        /// </summary>
        public String Name {get; set;}

        /// <summary>
        /// 性别
        /// </summary>
        public String Sex {get; set;}

        /// <summary>
        /// 职称
        /// </summary>
        public String Title {get; set;}

        /// <summary>
        /// 工作单位
        /// </summary>
        public String WorkUnits {get; set;}

        /// <summary>
        /// 地址
        /// </summary>
        public String Address {get; set;}

        /// <summary>
        /// 邮政编码
        /// </summary>
        public String PostalCode {get; set;}

        /// <summary>
        /// 电话
        /// </summary>
        public String Phone {get; set;}

        /// <summary>
        /// 对作品的阐述
        /// </summary>
        public String Elaborate {get; set;}

        /// <summary>
        /// 对作品的评价
        /// </summary>
        public String Evaluate {get; set;}

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark {get; set;}

        /// <summary>
        /// 住宅电话
        /// </summary>
        public String HomePhone {get; set;}
    }
}
