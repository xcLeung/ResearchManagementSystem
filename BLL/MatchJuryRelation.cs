using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MatchJuryRelation
    {
        /// <summary>
        /// 创建一条记录
        /// </summary>
        public static int CreateMatchJuryRelation(string MatchId,string JudgeId,string Enable)
        {
            int matchId = 0;
            int judgeId = 0;
            bool enable = true;
            Models.DB.MatchJuryRelation MatchJuryRelation = new Models.DB.MatchJuryRelation();
            try
            {
                matchId = Convert.ToInt32(MatchId);
                judgeId = Convert.ToInt32(JudgeId);
                enable = Convert.ToBoolean(Enable);
            }
            catch {
                return 0;
            }
            MatchJuryRelation.JudgeId = judgeId;
            MatchJuryRelation.MatchId = matchId;
            MatchJuryRelation.Enable = enable;
            return DAL.Create.CreateOne(MatchJuryRelation);
        }

        /// <summary>
        /// 创建一列记录
        /// </summary>
        public static int CreateMatchJuryRelation(List<string> MatchIds, List<string> JudgeIds, List<string> Enables)
        {
            int matchId = 0;
            int judgeId = 0;
            bool enable = true;
            if (MatchIds.Count != JudgeIds.Count ? true : JudgeIds.Count != Enables.Count)
            {
                return 0;
            }
            Models.DB.MatchJuryRelation MatchJuryRelation;
            List<Models.DB.MatchJuryRelation> MatchJuryRelations=new List<Models.DB.MatchJuryRelation> ();
            for (int i = 0; i < MatchIds.Count; i++)
            {
                MatchJuryRelation = new Models.DB.MatchJuryRelation();
                try
                {
                    matchId = Convert.ToInt32(MatchIds[i]);
                    judgeId = Convert.ToInt32(JudgeIds[i]);
                    enable = Convert.ToBoolean(Enables[i]);
                }
                catch
                {
                    return 0;
                }
                MatchJuryRelation.JudgeId = judgeId;
                MatchJuryRelation.MatchId = matchId;
                MatchJuryRelation.Enable = enable;
                MatchJuryRelations.Add(MatchJuryRelation);
            }
            return DAL.Create.CreateList(MatchJuryRelations);
        }
        /// <summary>
        /// 指定MatchId删除
        /// </summary>
        /// <param name="MatchID"></param>
        /// <returns></returns>
        public static int DeleteMatchJuryRelation(string MatchID)
        {
            int matchId = 0;
            if (string.IsNullOrEmpty(MatchID))
            {
                return 0;
            }
            try
            {
                matchId = Convert.ToInt32(MatchID);
            }
            catch {
                return 0;
            }
            Models.DB.MatchJuryRelation MatchJuryRelation = new Models.DB.MatchJuryRelation();
            MatchJuryRelation.MatchId = matchId;
            return DAL.Delete.Work(MatchJuryRelation,"MatchId");
        }
        /// <summary>
        /// 指定MatchId统计记录数
        /// </summary>
        /// <param name="MatchID"></param>
        /// <returns></returns>
        public static double CountJudges(string MatchID)
        {
            int matchId = 0;
            if (string.IsNullOrEmpty(MatchID))
            {
                return 0;
            }
            try {
                matchId = Convert.ToInt32(MatchID);
            }
            catch{
                return 0;
            }
            Models.DB.MatchJuryRelation MatchJuryRelation=new Models.DB.MatchJuryRelation ();
            MatchJuryRelation.MatchId=matchId;
            return DAL.Select.GetCount(MatchJuryRelation, "MatchId");
        }


        #region lxc
        public static List<Models.FB.MatchAndJudge> FindJudgeMatch(String JudgeID)
        {
            if (String.IsNullOrEmpty(JudgeID))
            {
                return null;
            }
            List<Models.FB.MatchAndJudge> list = new List<Models.FB.MatchAndJudge>();
            Models.FB.MatchAndJudge model;
            DataTable dt = DAL.Select.FindJudgeMatch(Convert.ToInt32(JudgeID),System.DateTime.Now);
            foreach (DataRow row in dt.Rows)
            {
                model = new Models.FB.MatchAndJudge();
                if (row["Status"].ToString() == "结束")
                    continue;
                model.Address = row["Address"].ToString();
                model.Background = row["Background"].ToString();
                model.CampusId = row["CampusId"].ToString();
                model.DeadLine = Convert.ToDateTime(row["DeadLine"]);
                model.DeclarantDeadLine = Convert.ToDateTime(row["DeclarantDeadLine"]);
                model.JobId = row["JobId"].ToString();
                model.JudgeCollege = row["College"].ToString();
                model.JudgeId = Convert.ToInt32(row["JudgeId"]);
                model.Mail = row["Mail"].ToString();
                model.MatchCollege = row[24].ToString();
                model.MatchID = Convert.ToInt32(row["MatchID"]);
                model.MatchModel = Convert.ToInt32(row["MatchModel"]);
                model.MatchName = row["MatchName"].ToString();
                model.Phone = row["Phone"].ToString();
                model.RealName = row["RealName"].ToString();
                model.Research = row["Research"].ToString();
                model.Rule = row["Rule"].ToString();
                model.ScoreIntro = row["ScoreIntro"].ToString();
                model.Sex = row["Sex"].ToString();
                model.Status = row["Status"].ToString();
                model.Title = row["Title"].ToString();
                model.UserId = Convert.ToInt32(row["UserId"]);
                model.WorkUnits = row["WorkUnits"].ToString();
                list.Add(model);
            }
            return list;
        }

        #endregion
    }
}
