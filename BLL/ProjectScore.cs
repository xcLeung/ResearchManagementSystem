using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProjectScore
    {
        public static int CreateOne(String ProjectID,String Score,String JudgeID,String Remark,String MatchModelID)
        {
        
            int projectid, score, judgeid, matchmodelid;
            try
            {
                projectid = Convert.ToInt32(ProjectID);
                score = Convert.ToInt32(Score);
                judgeid = Convert.ToInt32(JudgeID);
                matchmodelid = Convert.ToInt32(MatchModelID);
            }
            catch
            {
                return 0;
            }
            Models.DB.ProjectScore model = new Models.DB.ProjectScore();
            model.JudgeID = judgeid;
            model.MatchModelID = matchmodelid;
            model.ProjectID = projectid;
            model.Remark = Remark;
            model.Score = score;
            return DAL.Create.CreateOne(model);
        }

        public static int Update(String ID, String ProjectID, String Score, String JudgeID, String Remark, String MatchModelID)
        {
           
            int projectid, score, judgeid, matchmodelid,id;
            try
            {
                id = Convert.ToInt32(ID);
                projectid = Convert.ToInt32(ProjectID);
                score = Convert.ToInt32(Score);
                judgeid = Convert.ToInt32(JudgeID);
                matchmodelid = Convert.ToInt32(MatchModelID);
            }
            catch
            {
                return 0;
            }
            Models.DB.ProjectScore model = new Models.DB.ProjectScore();
            model.JudgeID = judgeid;
            model.MatchModelID = matchmodelid;
            model.ProjectID = projectid;
            model.Remark = Remark;
            model.Score = score;
            model.ID = id;
            return DAL.Update.ChangeSome(model, "ID");
        }

        public static List<Models.DB.ProjectScore> FindProjectScore(String ProjectID,String MatchModelID)
        {
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
            List<Models.DB.ProjectScore> list = new List<Models.DB.ProjectScore>();
            Models.DB.ProjectScore model;
            DataTable dt = DAL.Select.FindProjectScore(projectid,matchmodelid);
            foreach (DataRow row in dt.Rows)
            {
                model = new Models.DB.ProjectScore();
                model.Score = Convert.ToInt32(row["Score"]);
                model.ProjectID = Convert.ToInt32(row["ProjectID"]);
                model.MatchModelID = Convert.ToInt32(row["MatchModelID"]);
                model.JudgeID = Convert.ToInt32(row["JudgeID"]);
                model.ID = Convert.ToInt32(row["ID"]);
                model.Remark = row["Remark"].ToString();
                list.Add(model);
            }
            return list;
        }


        public static Models.DB.ProjectScore FindOne(String ProjectID, String MatchModelID,String JudgeID)
        {
            int projectid, matchmodelid,judgeid;
            try
            {
                projectid = Convert.ToInt32(ProjectID);
                matchmodelid = Convert.ToInt32(MatchModelID);
                judgeid = Convert.ToInt32(JudgeID);
            }
            catch
            {
                return null;
            }
            Models.DB.ProjectScore model = new Models.DB.ProjectScore();
            DataTable dt = DAL.Select.FindProjectScore(projectid, matchmodelid,judgeid);
            if (dt.Rows.Count > 0)
            {
                model = new Models.DB.ProjectScore();
                model.Score = Convert.ToInt32(dt.Rows[0]["Score"]);
                model.ProjectID = Convert.ToInt32(dt.Rows[0]["ProjectID"]);
                model.MatchModelID = Convert.ToInt32(dt.Rows[0]["MatchModelID"]);
                model.JudgeID = Convert.ToInt32(dt.Rows[0]["JudgeID"]);
                model.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                model.Remark = dt.Rows[0]["Remark"].ToString();
                return model;
            }
           
            return null;
        }

        public static Boolean HasRecord(String ProjectID, String MatchModelID)
        {
            int projectid, matchmodelid;
            try
            {
                projectid = Convert.ToInt32(ProjectID);
                matchmodelid = Convert.ToInt32(MatchModelID);
            }
            catch
            {
                return false;
            }
            int count = 0;
            count = DAL.Select.ProjectScoreHasRecord(projectid, matchmodelid);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean HasRecord(String ProjectID, String MatchModelID,String JudgeID)
        {
            int projectid, matchmodelid,judgeid;
            try
            {
                judgeid = Convert.ToInt32(JudgeID);
                projectid = Convert.ToInt32(ProjectID);
                matchmodelid = Convert.ToInt32(MatchModelID);
            }
            catch
            {
                return false;
            }
            int count = 0;
            count = DAL.Select.ProjectScoreHasRecord(projectid, matchmodelid,judgeid);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
