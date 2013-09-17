using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class CupProjectModel
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID { get; set; }
        
        
        
        /// <summary>
        /// 杯赛ID
        /// </summary>
        public int MatchID {get; set;}
        
        /// <summary>
        /// 申报种类
        /// </summary>
        public String DeclarationType {get; set;}
        
        /// <summary>
        /// 类别
        /// </summary>
        public String Category {get; set;}
        
        /// <summary>
        /// 论文文档
        /// </summary>
        public String PaperDoc {get; set;}
        
        /// <summary>
        /// 附加材料
        /// </summary>
        public String Material {get; set;}
        
        /// <summary>
        /// 申报日期
        /// </summary>
        public DateTime DeclarationDate {get; set;}
       
        
        /// <summary>
        /// 项目图片
        /// </summary>
        public String ProjectPic {get; set;}
       
        /// <summary>
        /// 项目视频
        /// </summary>
        public String ProjectVideo {get; set;}
       
        /// <summary>
        /// 备注
        /// </summary>
        public String Remark {get; set;}
        
        
        /// <summary>
        /// pdf地址
        /// </summary>
        public String PdfUrl {get; set;}
        
        
        /// <summary>
        /// 项目状态
        /// </summary>
        public String Statu {get; set;}
        
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public String Name { get; set; }
    }
}
