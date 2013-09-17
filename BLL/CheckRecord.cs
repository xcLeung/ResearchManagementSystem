using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CheckRecord
    {
        public static Models.DB.CheckRecord FindOne(String ProjectID, String MatchModelID)
        {
            #region 输入合法性检测
            int projectid, matchmodelid;
            try
            {
                projectid = Convert.ToInt32(ProjectID);
                matchmodelid = Convert.ToInt32(MatchModelID);
            }
            catch
            {
                return null;
            }
            #endregion

            #region 把数组组装成对象
            DataTable dt = DAL.Select.FindCheckRecord(projectid,matchmodelid);
            if (dt.Rows.Count > 0)
            {
                Models.DB.CheckRecord model = new Models.DB.CheckRecord();
                model.AfterStatus = dt.Rows[0]["AfterStatus"].ToString();
                model.BeforeStatus = dt.Rows[0]["BeforeStatus"].ToString();
                model.Checker = dt.Rows[0]["Checker"].ToString();
                model.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                model.MatchModelID = Convert.ToInt32(dt.Rows[0]["MatchModelID"]);
                model.ProjectID = Convert.ToInt32(dt.Rows[0]["ProjectID"]);
                model.Reason = dt.Rows[0]["Reason"].ToString();
                model.Time = Convert.ToDateTime(dt.Rows[0]["Time"]);
                return model;
            }
            return null;
            #endregion
        }

        public static int Create(String AfterStatus, String Checker, String MatchModelID,
            String ProjectID, String Reason)
        {
            #region 输入合法性检测
            if (String.IsNullOrEmpty(AfterStatus)  || String.IsNullOrEmpty(Checker))
            {
                return 0;
            }
            int projectid, matchmodelid;
            try
            {
                projectid = Convert.ToInt32(ProjectID);
                matchmodelid = Convert.ToInt32(MatchModelID);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 组装类
            Models.DB.CheckRecord model = new Models.DB.CheckRecord();
            model.AfterStatus = AfterStatus;
            model.BeforeStatus = "提交";
            model.Checker = Checker;
            model.MatchModelID = matchmodelid;
            model.ProjectID = projectid;
            model.Reason = Reason;
            model.Time = System.DateTime.Now;
            #endregion

            return DAL.Create.CreateOne(model);
        }

        public static int Update(String AfterStatus, String Checker, String MatchModelID,
            String ProjectID, String Reason,String BeforeStatus,String ID)
        {
            #region 输入合法性检测
            if (String.IsNullOrEmpty(AfterStatus)  || String.IsNullOrEmpty(Checker) || String.IsNullOrEmpty(BeforeStatus))
            {
                return 0;
            }
            int projectid, matchmodelid,id;
            try
            {
                id = Convert.ToInt32(ID);
                projectid = Convert.ToInt32(ProjectID);
                matchmodelid = Convert.ToInt32(MatchModelID);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 组装类
            Models.DB.CheckRecord model = new Models.DB.CheckRecord();
            model.AfterStatus = AfterStatus;
            model.BeforeStatus = BeforeStatus; ;
            model.Checker = Checker;
            model.MatchModelID = matchmodelid;
            model.ProjectID = projectid;
            model.Reason = Reason;
            model.Time = System.DateTime.Now;
            model.Id = id;
            #endregion

            return DAL.Update.ChangeSome(model, "Id");
        }
    }
}
