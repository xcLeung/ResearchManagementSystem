using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class CupDeclarantInfo
    {
        /// <summary>
        /// 自增id
        /// </summary>
        public int Id {get; set;}

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday {get;set;}

        /// <summary>
        /// 学制
        /// </summary>
        public String SchoolSystme {get;set;}

        /// <summary>
        /// 论文题目
        /// </summary>
        public String PaperTitle {get;set;}

        /// <summary>
        /// 通讯地址
        /// </summary>
        public String Address {get;set;}

        /// <summary>
        /// 通讯地址邮政编码
        /// </summary>
        public String PostalCode {get;set;}

        /// <summary>
        /// 电话
        /// </summary>
        public String Phone {get;set;}

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID {get;set;}

        /// <summary>
        /// 学历
        /// </summary>
        public String BackGround {get;set;}

        /// <summary>
        /// 常住通讯地址
        /// </summary>
        public String TopAddress {get;set;}

        /// <summary>
        /// 常住通讯地址邮政编码
        /// </summary>
        public String TopPostalCode {get;set;}

        /// <summary>
        /// 住宅电话
        /// </summary>
        public String TopPhone {get;set;}
    }
}
