using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class JudgeInfoModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string WorkUnits { get; set; }
        /// <summary>
        /// 职称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 校园卡号
        /// </summary>
        public string CampusId { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string JobId { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string Background { get; set; }
        /// <summary>
        /// 研究方向
        /// </summary>
        public string Research { get; set; }
        /// <summary>
        /// 学院
        /// </summary>
        public string College { get; set; }

    }
}
