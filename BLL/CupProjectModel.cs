using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CupProjectModel
    {
        #region lxc
        public static int Create(String MatchID,String Statu,String UserID,String Date)
        {
            #region 检查输入的合法性
            if (string.IsNullOrEmpty(Statu))
            {
                return 0;
            }
            int matchid, userid;
            DateTime date;
            try
            {
                matchid = Convert.ToInt32(MatchID);
                userid = Convert.ToInt32(UserID);
                date = Convert.ToDateTime(Date);
                
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成一个对象
            Models.DB.CupProjectModel model = new Models.DB.CupProjectModel();
            model.MatchID = matchid;
            model.UserID = userid;
            model.Statu = Statu;
            model.DeclarationDate = date;
            #endregion

            return DAL.Create.CreateOneReturnID(model);
        }


        /// <summary>
        /// 单一条件查询Int型
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="ValueName"></param>
        /// <returns></returns>
        public static List<Models.DB.CupProjectModel> FindByInt(String Value,String ValueName)
        {
            #region 输入合法性检查
            int valueInt;
            try
            {
                valueInt = Convert.ToInt32(Value);
            }
            catch
            {
                return null;
            }
            #endregion

            List<Models.DB.CupProjectModel> list = new List<Models.DB.CupProjectModel>();


            DataTable dt = DAL.Select.QueryOne(valueInt,"Tb_CupProjectModel",ValueName);

            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                Models.DB.CupProjectModel e = new Models.DB.CupProjectModel();
           
                e.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                e.MatchID = Convert.ToInt32(dt.Rows[i]["MatchID"]);
                e.DeclarationType = dt.Rows[i]["DeclarationType"].ToString();
                e.Category = dt.Rows[i]["Category"].ToString();
                e.PaperDoc = dt.Rows[i]["PaperDoc"].ToString();
                e.Material = dt.Rows[i]["Material"].ToString();
                e.DeclarationDate = Convert.ToDateTime(dt.Rows[i]["DeclarationDate"]);
                e.ProjectPic = dt.Rows[i]["ProjectPic"].ToString();
                e.ProjectVideo = dt.Rows[i]["ProjectVideo"].ToString();
                e.Remark = dt.Rows[i]["Remark"].ToString();
                e.PdfUrl = dt.Rows[i]["PdfUrl"].ToString();
                e.Statu = dt.Rows[i]["Statu"].ToString();
                e.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                e.Name = dt.Rows[i]["Name"].ToString();
                list.Add(e);

            }
            return list;
        }


        public static int Updata(String ProjectID, String ProjectName, String DeclarationType, String Category, String DeclarationDate, String MatchID,String UserID)
        {
            #region 输入合法性检测
            if (string.IsNullOrEmpty(Category) || string.IsNullOrEmpty(DeclarationType) || string.IsNullOrEmpty(ProjectName))
            {
                return 0;
            }
            DateTime date;
            int matchid;
            int userid;
            try
            {
                date = Convert.ToDateTime(DeclarationDate);
                matchid = Convert.ToInt32(MatchID);
                userid = Convert.ToInt32(UserID);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成一个对象
            Models.DB.CupProjectModel model = new Models.DB.CupProjectModel();
            model.Name = ProjectName;
            model.DeclarationType = DeclarationType;
            model.Category = Category;
            model.DeclarationDate = date;
            model.UserID = userid;
            model.MatchID = matchid;
            model.ID = Convert.ToInt32(ProjectID);
            if (model.Name == null)
            {
                model.Name = "未填写作品名称";
            }
            return DAL.Update.ChangeSome(model,"ID");
            #endregion
        }

        #region 获取分页记录
        public static List<Models.DB.CupProjectModel> SelectOnePage(int page_size, int current_page, string order_field, string order_value)
        {
            DataTable dt = DAL.Select.GetSome("Tb_CupProjectModel",page_size, current_page, order_field, order_value);
            List<Models.DB.CupProjectModel> list = new List<Models.DB.CupProjectModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                Models.DB.CupProjectModel e = new Models.DB.CupProjectModel();
                e.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                e.MatchID = Convert.ToInt32(dt.Rows[i]["MatchID"]);
                e.DeclarationType = dt.Rows[i]["DeclarationType"].ToString();
                e.Category = dt.Rows[i]["Category"].ToString();
                e.PaperDoc = dt.Rows[i]["PaperDoc"].ToString();
                e.Material = dt.Rows[i]["Material"].ToString();
                e.DeclarationDate = Convert.ToDateTime(dt.Rows[i]["DeclarationDate"]);
                e.ProjectPic = dt.Rows[i]["ProjectPic"].ToString();
                e.ProjectVideo = dt.Rows[i]["ProjectVideo"].ToString();
                e.Remark = dt.Rows[i]["Remark"].ToString();
                e.PdfUrl = dt.Rows[i]["PdfUrl"].ToString();
                e.Statu = dt.Rows[i]["Statu"].ToString();
                e.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                e.Name = dt.Rows[i]["Name"].ToString();
                list.Add(e);

            }

            return list;
        }
        public static List<Models.DB.CupProjectModel> SelectOnePage(int page_size, int current_page)
        {
            return SelectOnePage(page_size, current_page, "ID", "asc");
        }
        #endregion


        public static int UpdatePaperUrl(String ID, String url)
        {
            #region 输入合法性检测
            if ( string.IsNullOrEmpty(url))
            {
                return 0;
            }
            int projectid;
            try
            {
                projectid = Convert.ToInt32(ID);
            }
            catch
            {
                return 0;
            }
            #endregion

            return DAL.Update.UpdatePaperUrl(projectid, url);           
        }

        public static int UpdateRemark(String ID, String Remark)
        {
            #region 输入合法性检测
            if (String.IsNullOrEmpty(Remark))
            {
                Remark = "无";
            }
            int id;
            try
            {
                id = Convert.ToInt32(ID);
            }
            catch
            {
                return 0;
            }
            #endregion

            return DAL.Update.UpdateRemark(id, Remark, "Tb_CupProjectModel");
        }

        public static int UpdateStatus(String Status,String ID)
        {
            #region 输入合法性检测
            if (String.IsNullOrEmpty(Status))
            {
                return 0;
            }
            int id;
            try
            {
                id = Convert.ToInt32(ID);
            }
            catch
            {
                return 0;
            }
            #endregion

            return DAL.Update.UpdateProjectStatus(Status, "Tb_CupProjectModel", id);
        }
        #endregion


        #region By云海
        /// <summary>
        /// 根据MatchId查询一页数据
        /// </summary>
        /// <param name="current_page"></param>
        /// <param name="page_size"></param>
        /// <param name="MatchId"></param>
        /// <returns></returns>
        public static List<Models.DB.CupProjectModel> SelectOnePage(int current_page, int page_size, string MatchId)
        {
            List<Models.DB.CupProjectModel> Projects = new List<Models.DB.CupProjectModel>();
            int matchId = 0;
            if (string.IsNullOrEmpty(MatchId))
            {
                return Projects;
            }
            try
            {
                matchId = Convert.ToInt32(MatchId);
            }
            catch
            {
                return Projects;
            }
            Models.DB.CupProjectModel Project = new Models.DB.CupProjectModel();
            Project.MatchID = matchId;
            Project.Statu = "提交";
            string[] targets = { "MatchID", "Statu" };
            System.Data.DataTable dt = DAL.Select.GetSome(Project, targets, page_size, current_page, "UserID", "asc");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Project = new Models.DB.CupProjectModel();

                    Project.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    Project.MatchID = Convert.ToInt32(dt.Rows[i]["MatchID"]);
                    Project.DeclarationType = dt.Rows[i]["DeclarationType"].ToString();
                    Project.Category = dt.Rows[i]["Category"].ToString();
                    Project.PaperDoc = dt.Rows[i]["PaperDoc"].ToString();
                    Project.Material = dt.Rows[i]["Material"].ToString();
                    Project.DeclarationDate = Convert.ToDateTime(dt.Rows[i]["DeclarationDate"]);
                    Project.ProjectPic = dt.Rows[i]["ProjectPic"].ToString();
                    Project.ProjectVideo = dt.Rows[i]["ProjectVideo"].ToString();
                    Project.Remark = dt.Rows[i]["Remark"].ToString();
                    Project.PdfUrl = dt.Rows[i]["PdfUrl"].ToString();
                    Project.Statu = dt.Rows[i]["Statu"].ToString();
                    Project.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                    Project.Name = dt.Rows[i]["Name"].ToString();

                    Projects.Add(Project);
                }
            }
            return Projects;
        }
        /// <summary>
        /// 根据比赛统计数据
        /// </summary>
        /// <param name="MatchId"></param>
        /// <returns></returns>
        public static double CountByMatchId(string MatchId)
        {
            int matchId = 0;
            if (string.IsNullOrEmpty(MatchId))
            {
                return 0;
            }
            try
            {
                matchId = Convert.ToInt32(MatchId);
            }
            catch
            {
                return 0;
            }
            Models.DB.CupProjectModel Project = new Models.DB.CupProjectModel();
            Project.MatchID = matchId;
            return DAL.Select.GetCount(Project, "MatchID");

        }
        /// <summary>
        /// 更新PdfUrl
        /// </summary>
        /// <param name="Project"></param>
        /// <param name="FileOne"></param>
        public static int UpdataPdfUrl(string ProjectID, string Url)
        {
            if (string.IsNullOrEmpty(ProjectID) || string.IsNullOrEmpty(Url))
            {
                return 0;
            }

            List<Models.DB.CupProjectModel> Projects=FindByInt(ProjectID,"ID");
            if(Projects.Count>0){
                Models.DB.CupProjectModel Project = Projects[0];
                Project.PdfUrl = Url;
                return DAL.Update.ChangeSome(Project,"ID");
            }
            return 0;
        }
        #endregion


        
    }
}
