using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class InnovationProjectModel
    {


        /// <summary>
        /// 单一条件查询Int型
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="ValueName"></param>
        /// <returns></returns>
        public static List<Models.DB.InnovationProjectModel> FindByInt(String Value, String ValueName)
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

            List<Models.DB.InnovationProjectModel> list = new List<Models.DB.InnovationProjectModel>();


            DataTable dt = DAL.Select.QueryOne(valueInt, "Tb_InnovationProjectModel", ValueName);

            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                Models.DB.InnovationProjectModel e = new Models.DB.InnovationProjectModel();
                e.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                e.Statu = dt.Rows[i]["Statu"].ToString();
                e.Name = dt.Rows[i]["Name"].ToString();
                e.MatchID = Convert.ToInt32(dt.Rows[i]["MatchID"]);
                e.DeclarationDate = Convert.ToDateTime(dt.Rows[i]["DeclarationDate"]);
                e.EndTime = Convert.ToDateTime(dt.Rows[i]["EndTime"]);
                e.StartTime =  Convert.ToDateTime(dt.Rows[i]["StartTime"]);
                e.DeclarationType = dt.Rows[i]["DeclarationType"].ToString();
                e.InterimReport = dt.Rows[i]["InterimReport"].ToString();
                e.Managementbasis = dt.Rows[i]["Managementbasis"].ToString();
                e.PdfUrl = dt.Rows[i]["PdfUrl"].ToString();
                e.Remark = dt.Rows[i]["Remark"].ToString();
                e.SubmitAchievement = dt.Rows[i]["SubmitAchievement"].ToString();
                e.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                e.ProjectDoc = dt.Rows[i]["ProjectDoc"].ToString();
                list.Add(e);

            }
            return list;
        }


        public static int UpdateStatus(String Status, String ID)
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

            return DAL.Update.UpdateProjectStatus(Status, "Tb_InnovationProjectModel", id);
        }


        public static int Updata(String ProjectID, String ProjectName, String DeclarationType, String StartTime, String EndTime,String InterimReport,String SubmitAchievement,String Managementbasis,String DeclarationDate, String MatchID, String UserID)
        {
            #region 输入合法性检测
            if ( string.IsNullOrEmpty(DeclarationType) || string.IsNullOrEmpty(ProjectName))
            {
                return 0;
            }
            DateTime date,start,end;
            int matchid;
            int userid;
            try
            {
                date = Convert.ToDateTime(DeclarationDate);
                start = Convert.ToDateTime(StartTime);
                end = Convert.ToDateTime(EndTime);
                matchid = Convert.ToInt32(MatchID);
                userid = Convert.ToInt32(UserID);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成一个对象
            Models.DB.InnovationProjectModel model = new Models.DB.InnovationProjectModel();
            model.Name = ProjectName;
            model.DeclarationDate = date;
            model.DeclarationType = DeclarationType;
            model.StartTime = start;
            model.EndTime = end;
            model.InterimReport = InterimReport;
            model.Managementbasis = Managementbasis;
            model.SubmitAchievement = SubmitAchievement;
            model.UserID = userid;
            model.MatchID = matchid;
            model.Id = Convert.ToInt32(ProjectID);
            if (model.Name == null)
            {
                model.Name = "未填写作品名称";
            }
            return DAL.Update.ChangeSome(model, "Id");
            #endregion
        }


        public static int Create(String MatchID, String Statu, String UserID, String Date, String StartTime, String EndTime)
        {
            #region 检查输入的合法性
            if (string.IsNullOrEmpty(Statu))
            {
                return 0;
            }
            int matchid, userid;
            DateTime date, starttime, endtime;
            try
            {
                matchid = Convert.ToInt32(MatchID);
                userid = Convert.ToInt32(UserID);
                date = Convert.ToDateTime(Date);
                starttime = Convert.ToDateTime(StartTime);
                endtime = Convert.ToDateTime(EndTime);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成一个对象
            Models.DB.InnovationProjectModel model = new Models.DB.InnovationProjectModel();
            model.MatchID = matchid;
            model.UserID = userid;
            model.Statu = Statu;
            model.DeclarationDate = date;
            model.StartTime = starttime;
            model.EndTime = endtime;
            #endregion

            return DAL.Create.CreateOneReturnID(model);
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

            return DAL.Update.UpdateRemark(id, Remark,"Tb_InnovationProjectModel");
        }


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
            Models.DB.InnovationProjectModel Project = new Models.DB.InnovationProjectModel();
            Project.MatchID = matchId;
            return DAL.Select.GetCount(Project, "MatchID");

        }



        public static List<Models.DB.InnovationProjectModel> SelectOnePage(int current_page, int page_size, string MatchId)
        {
            List<Models.DB.InnovationProjectModel> Projects = new List<Models.DB.InnovationProjectModel>();
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
            Models.DB.InnovationProjectModel Project = new Models.DB.InnovationProjectModel();
            Project.MatchID = matchId;
            Project.Statu = "提交";
            string[] targets = { "MatchID", "Statu" };
            System.Data.DataTable dt = DAL.Select.GetSome(Project, targets, page_size, current_page, "UserID", "asc");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Models.DB.InnovationProjectModel e = new Models.DB.InnovationProjectModel();
                    e.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    e.Statu = dt.Rows[i]["Statu"].ToString();
                    e.Name = dt.Rows[i]["Name"].ToString();
                    e.MatchID = Convert.ToInt32(dt.Rows[i]["MatchID"]);
                    e.DeclarationDate = Convert.ToDateTime(dt.Rows[i]["DeclarationDate"]);
                    e.EndTime = Convert.ToDateTime(dt.Rows[i]["EndTime"]);
                    e.StartTime = Convert.ToDateTime(dt.Rows[i]["StartTime"]);
                    e.DeclarationType = dt.Rows[i]["DeclarationType"].ToString();
                    e.InterimReport = dt.Rows[i]["InterimReport"].ToString();
                    e.Managementbasis = dt.Rows[i]["Managementbasis"].ToString();
                    e.PdfUrl = dt.Rows[i]["PdfUrl"].ToString();
                    e.Remark = dt.Rows[i]["Remark"].ToString();
                    e.SubmitAchievement = dt.Rows[i]["SubmitAchievement"].ToString();
                    e.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                    Projects.Add(e);
                }
            }
            return Projects;
        }
        #region By云海
        /// <summary>
        /// 更新Url
        /// </summary>
        /// <param name="Project"></param>
        /// <param name="FileOne"></param>
        public static int UpdataUrl(string ProjectID, string UrlPDF, string UrlWORD)
        {
            if (string.IsNullOrEmpty(ProjectID) || string.IsNullOrEmpty(UrlPDF) || string.IsNullOrEmpty(UrlWORD))
            {
                return 0;
            }

            List<Models.DB.InnovationProjectModel> Projects = FindByInt(ProjectID, "ID");
            if (Projects.Count > 0)
            {
                Models.DB.InnovationProjectModel Project = Projects[0];
                Project.PdfUrl = UrlPDF;
                Project.ProjectDoc = UrlWORD;
                return DAL.Update.ChangeSome(Project, "Id");
            }
            return 0;
        }
        #endregion

    }
}
