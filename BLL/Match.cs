using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Match
    {

        #region xcLeung
        public static List<Models.DB.Match> FindByString(String Value, String ValueName)
        {
            #region 输入合法性检查
            if (String.IsNullOrEmpty(Value))
            {
                return null;
            }
            #endregion

            List<Models.DB.Match> list = new List<Models.DB.Match>();

            DataTable dt = DAL.Select.QueryOne(Value,"Tb_Match",ValueName);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                Models.DB.Match e = new Models.DB.Match();
                e.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                e.MatchName = dt.Rows[i]["MatchName"].ToString();
                e.DeadLine = Convert.ToDateTime(dt.Rows[i]["DeadLine"]);
                e.DeclarantDeadLine = Convert.ToDateTime(dt.Rows[i]["DeclarantDeadLine"]);
                e.College = dt.Rows[i]["College"].ToString();
                e.Status = dt.Rows[i]["Status"].ToString();
                e.Rule = dt.Rows[i]["Rule"].ToString();
                e.ScoreIntro = dt.Rows[i]["ScoreIntro"].ToString();
            
                e.MatchModel = Convert.ToInt32(dt.Rows[i]["MatchModel"]);
               
                list.Add(e);

            }
            return list;
        }

        public static List<Models.DB.Match> MatchCountByStudent(String College)
        {
            List<Models.DB.Match> Matchs = new List<Models.DB.Match>();
            Models.DB.Match Match;
            DataTable dt =  DAL.Select.MatchCountByStudent("校级", College, "未结束");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Match = new Models.DB.Match();
                    Match.ID = Convert.ToInt32(dt.Rows[i]["Id"]);
                    Match.MatchName = dt.Rows[i]["MatchName"].ToString();
                    Match.Rule = dt.Rows[i]["Rule"].ToString();
                    Match.ScoreIntro = dt.Rows[i]["ScoreIntro"].ToString();
                    Match.Status = dt.Rows[i]["Status"].ToString();
                    Match.MatchModel = Convert.ToInt32(dt.Rows[i]["MatchModel"]);
                    Match.College = dt.Rows[i]["College"].ToString();
                    Match.DeadLine = Convert.ToDateTime(dt.Rows[i]["DeadLine"]);
                    Match.DeclarantDeadLine = Convert.ToDateTime(dt.Rows[i]["DeclarantDeadLine"]);
                    Matchs.Add(Match);
                }
            }
            return Matchs;
        }


        public static List<Models.DB.Match> SelectOnePageByStudent(int current_page, int page_size,String orderField,String orderValue,String College)
        {
            List<Models.DB.Match> Matchs = new List<Models.DB.Match>();
            Models.DB.Match Match;
            System.Data.DataTable dt = DAL.Select.MatchSearchByStudent("Tb_Match", page_size, current_page, orderField, orderValue, "校级", College,"未结束");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Match = new Models.DB.Match();
                    Match.ID = Convert.ToInt32(dt.Rows[i]["Id"]);
                    Match.MatchName = dt.Rows[i]["MatchName"].ToString();
                    Match.Rule = dt.Rows[i]["Rule"].ToString();
                    Match.ScoreIntro = dt.Rows[i]["ScoreIntro"].ToString();
                    Match.Status = dt.Rows[i]["Status"].ToString();
                    Match.MatchModel = Convert.ToInt32(dt.Rows[i]["MatchModel"]);
                    Match.College = dt.Rows[i]["College"].ToString();
                    Match.DeadLine = Convert.ToDateTime(dt.Rows[i]["DeadLine"]);
                    Match.DeclarantDeadLine = Convert.ToDateTime(dt.Rows[i]["DeclarantDeadLine"]);
                    Matchs.Add(Match);
                }
            }
            return Matchs;
        }

        public static double CountByCollege(String College)
        {
            if (String.IsNullOrEmpty(College))
            {
                return 0;
            }
            return DAL.Select.CountByCollege(College,"Tb_Match");
        }

        public static List<Models.DB.Match> SelectOnePageByCollege(int current_page, int page_size,String College)
        {
            if (String.IsNullOrEmpty(College))
            {
                return null;
            }
            List<Models.DB.Match> Matchs = new List<Models.DB.Match>();
            Models.DB.Match Match;
            System.Data.DataTable dt = DAL.Select.SearchByCollege("Tb_Match", page_size, current_page, "Id", "",College);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Match = new Models.DB.Match();
                    Match.ID = Convert.ToInt32(dt.Rows[i]["Id"]);
                    //   Match.Level = dt.Rows[i]["Level"].ToString();
                    Match.MatchName = dt.Rows[i]["MatchName"].ToString();
                    //    Match.ProjectID = dt.Rows[i]["ProjectID"].ToString();
                    Match.Rule = dt.Rows[i]["Rule"].ToString();
                    Match.ScoreIntro = dt.Rows[i]["ScoreIntro"].ToString();
                    Match.Status = dt.Rows[i]["Status"].ToString();
                    Match.MatchModel = Convert.ToInt32(dt.Rows[i]["MatchModel"]);
                    Match.College = dt.Rows[i]["College"].ToString();
                    Match.DeadLine = Convert.ToDateTime(dt.Rows[i]["DeadLine"]);
                    Match.DeclarantDeadLine = Convert.ToDateTime(dt.Rows[i]["DeclarantDeadLine"]);
                    Matchs.Add(Match);
                }
            }
            return Matchs;
        }

    #endregion

        #region By云海
        /// <summary>
        /// 新建一条记录
        /// </summary>
        public static int CreateMatch(string MatchName, string DeadLine, string Status,
        string DeclarantDeadLine, string MatchModel, string College, string Rule, string ScoreIntro)
        {
            DateTime deadLine = DateTime.Now;
            DateTime declarantDeadLine = DateTime.Now;
            int matchModel = 0;

            #region 合法性检测
            if (string.IsNullOrEmpty(MatchName) || string.IsNullOrEmpty(DeadLine) || string.IsNullOrEmpty(Status) ||
                string.IsNullOrEmpty(DeclarantDeadLine) || string.IsNullOrEmpty(MatchModel) ||
                string.IsNullOrEmpty(College) || string.IsNullOrEmpty(Rule) || string.IsNullOrEmpty(ScoreIntro))
            {
                return 0;
            }
            if (MatchName.Length > 255 || Status.Length > 255 ||  College.Length > 255)
            {
                return 0;
            }
            try
            {
                deadLine = Convert.ToDateTime(DeadLine);
                declarantDeadLine = Convert.ToDateTime(DeclarantDeadLine);
                matchModel = Convert.ToInt32(MatchModel);
            }
            catch
            {
                return 0;
            }
            #endregion

            Models.DB.Match Match = new Models.DB.Match();
            Match.College = College;
            Match.DeadLine = deadLine;
            Match.DeclarantDeadLine = declarantDeadLine;
            Match.MatchModel = matchModel;
            Match.MatchName = MatchName;
        //    Match.ProjectID = ProjectId;
            Match.Rule = Rule;
            Match.ScoreIntro = ScoreIntro;
            Match.Status = Status;
          //  Match.Level = Level;

            return DAL.Create.CreateOne(Match);
        }
        /// <summary>
        /// 统计
        /// </summary>
        /// <returns></returns>
        public static double Count()
        {
            return DAL.Select.GetCount("Tb_Match");
        }
        /// <summary>
        /// 获取一页记录
        /// </summary>
        public static List<Models.DB.Match> SelectOnePage(int current_page, int page_size)
        {
            List<Models.DB.Match> Matchs = new List<Models.DB.Match>();
            Models.DB.Match Match;
            System.Data.DataTable dt = DAL.Select.GetSome("Tb_Match", page_size, current_page, "Id", "");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Match = new Models.DB.Match();
                    Match.ID = Convert.ToInt32(dt.Rows[i]["Id"]);
                 //   Match.Level = dt.Rows[i]["Level"].ToString();
                    Match.MatchName = dt.Rows[i]["MatchName"].ToString();
                //    Match.ProjectID = dt.Rows[i]["ProjectID"].ToString();
                    Match.Rule = dt.Rows[i]["Rule"].ToString();
                    Match.ScoreIntro = dt.Rows[i]["ScoreIntro"].ToString();
                    Match.Status = dt.Rows[i]["Status"].ToString();
                    Match.MatchModel = Convert.ToInt32(dt.Rows[i]["MatchModel"]);
                    Match.College = dt.Rows[i]["College"].ToString();
                    Match.DeadLine = Convert.ToDateTime(dt.Rows[i]["DeadLine"]);
                    Match.DeclarantDeadLine = Convert.ToDateTime(dt.Rows[i]["DeclarantDeadLine"]);
                    Matchs.Add(Match);
                }
            }
            return Matchs;
        }
        /// <summary>
        /// 获取一条记录
        /// </summary>
        public static Models.DB.Match SelectOne(int Id)
        {
            Models.DB.Match Match = new Models.DB.Match();
            System.Data.DataTable dt = DAL.Select.GetOne("Tb_Match", Id);
            if (dt.Rows.Count > 0)
            {
                Match.ID = Convert.ToInt32(dt.Rows[0]["Id"]);
           //     Match.Level = dt.Rows[0]["Level"].ToString();
                Match.MatchName = dt.Rows[0]["MatchName"].ToString();
           //     Match.ProjectID = dt.Rows[0]["ProjectID"].ToString();
                Match.Rule = dt.Rows[0]["Rule"].ToString();
                Match.ScoreIntro = dt.Rows[0]["ScoreIntro"].ToString();
                Match.Status = dt.Rows[0]["Status"].ToString();
                Match.MatchModel = Convert.ToInt32(dt.Rows[0]["MatchModel"]);
                Match.College = dt.Rows[0]["College"].ToString();
                Match.DeadLine = Convert.ToDateTime(dt.Rows[0]["DeadLine"]);
                Match.DeclarantDeadLine = Convert.ToDateTime(dt.Rows[0]["DeclarantDeadLine"]);
            }
            return Match;
        }
        /// <summary>
        /// 更新一条记录
        /// </summary>
        public static int UpdateMatch(string Id, string MatchName, string DeadLine, string Status,
            string DeclarantDeadLine, string MatchModel,  string College, string Rule, string ScoreIntro)
        {
            DateTime deadLine = DateTime.Now;
            DateTime declarantDeadLine = DateTime.Now;
            int matchModel = 0;
            int id = 0;
            #region 合法性检测
            if (string.IsNullOrEmpty(MatchName) || string.IsNullOrEmpty(DeadLine) || string.IsNullOrEmpty(Status) ||
                 string.IsNullOrEmpty(DeclarantDeadLine) || string.IsNullOrEmpty(MatchModel) ||
                string.IsNullOrEmpty(College) || string.IsNullOrEmpty(Rule) || string.IsNullOrEmpty(ScoreIntro) || string.IsNullOrEmpty(Id))
            {
                return 0;
            }
            if (MatchName.Length > 255 || Status.Length > 255  || College.Length > 255)
            {
                return 0;
            }
            try
            {
                deadLine = Convert.ToDateTime(DeadLine);
                declarantDeadLine = Convert.ToDateTime(DeclarantDeadLine);
                matchModel = Convert.ToInt32(MatchModel);
                id = Convert.ToInt32(Id);
            }
            catch
            {
                return 0;
            }
            #endregion

            Models.DB.Match Match = new Models.DB.Match();
            Match.ID = id;
            Match.College = College;
            Match.DeadLine = deadLine;
            Match.DeclarantDeadLine = declarantDeadLine;
            Match.MatchModel = matchModel;
            Match.MatchName = MatchName;
        //    Match.ProjectID = ProjectId;
            Match.Rule = Rule;
            Match.ScoreIntro = ScoreIntro;
            Match.Status = Status;
         //   Match.Level = Level;

            return DAL.Update.ChangeSome(Match, "ID");
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        public static Models.DB.Match SelectMatch(string MatchName, string DeadLine, string Status, 
        string DeclarantDeadLine, string MatchModel, string College, string Rule, string ScoreIntro)
        {
            Models.DB.Match Match = new Models.DB.Match();
            DateTime deadLine = DateTime.Now;
            DateTime declarantDeadLine = DateTime.Now;
            int matchModel = 0;
            string[] targets = { "MatchName", "DeadLine", "Status",  "DeclarantDeadLine", "MatchModel", "College", "Rule", "ScoreIntro" };
            #region 合法性检测
            if (string.IsNullOrEmpty(MatchName) || string.IsNullOrEmpty(DeadLine) || string.IsNullOrEmpty(Status) ||
                 string.IsNullOrEmpty(DeclarantDeadLine) || string.IsNullOrEmpty(MatchModel) ||
                string.IsNullOrEmpty(College) || string.IsNullOrEmpty(Rule) || string.IsNullOrEmpty(ScoreIntro))
            {
                return Match;
            }
            if (MatchName.Length > 255 || Status.Length > 255  || College.Length > 255)
            {
                return Match;
            }
            try
            {
                deadLine = Convert.ToDateTime(DeadLine);
                declarantDeadLine = Convert.ToDateTime(DeclarantDeadLine);
                matchModel = Convert.ToInt32(MatchModel);
            }
            catch
            {
                return Match;
            }
            #endregion
            Match.College = College;
            Match.DeadLine = deadLine;
            Match.DeclarantDeadLine = declarantDeadLine;
            Match.MatchModel = matchModel;
            Match.MatchName = MatchName;
            Match.Rule = Rule;
            Match.ScoreIntro = ScoreIntro;
            Match.Status = Status;
          //  Match.Level = Level;
            System.Data.DataTable dt = DAL.Select.GetList(Match, targets);
            if (dt.Rows.Count > 0)
            {
                Match.ID = Convert.ToInt32(dt.Rows[0]["Id"]);
            //    Match.Level = dt.Rows[0]["Level"].ToString();
                Match.MatchName = dt.Rows[0]["MatchName"].ToString();
            //    Match.ProjectID = dt.Rows[0]["ProjectID"].ToString();
                Match.Rule = dt.Rows[0]["Rule"].ToString();
                Match.ScoreIntro = dt.Rows[0]["ScoreIntro"].ToString();
                Match.Status = dt.Rows[0]["Status"].ToString();
                Match.MatchModel = Convert.ToInt32(dt.Rows[0]["MatchModel"]);
                Match.College = dt.Rows[0]["College"].ToString();
                Match.DeadLine = Convert.ToDateTime(dt.Rows[0]["DeadLine"]);
                Match.DeclarantDeadLine = Convert.ToDateTime(dt.Rows[0]["DeclarantDeadLine"]);
            }
            return Match;
        }

        public static List<Models.DB.JudgeInfoModel> SelectJudges(int MatchId)
        {
            List<Models.DB.JudgeInfoModel> Judges = new List<Models.DB.JudgeInfoModel>();
            Models.DB.JudgeInfoModel Judge = new Models.DB.JudgeInfoModel();
            Models.DB.MatchJuryRelation MatchJuryRelation = new Models.DB.MatchJuryRelation();
            MatchJuryRelation.MatchId = MatchId;
            System.Data.DataTable dt = DAL.Select.GetList(MatchJuryRelation, "MatchId");
            System.Data.DataTable dtJudge;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int Id = Convert.ToInt32(dt.Rows[i]["JudgeId"]);
                    dtJudge = DAL.Select.GetOne("Tb_JudgeInfoModel", Id);
                    if (dtJudge.Rows.Count > 0)
                    {
                        Judge = new Models.DB.JudgeInfoModel();
                        Judge.WorkUnits = dtJudge.Rows[0]["WorkUnits"].ToString();
                        Judge.Address = dtJudge.Rows[0]["Address"].ToString();
                        Judge.Background = dtJudge.Rows[0]["Background"].ToString();
                        Judge.CampusId = dtJudge.Rows[0]["CampusId"].ToString();
                        Judge.College = dtJudge.Rows[0]["College"].ToString();
                        Judge.JobId = dtJudge.Rows[0]["JobId"].ToString();
                        Judge.Mail = dtJudge.Rows[0]["Mail"].ToString();
                        Judge.Phone = dtJudge.Rows[0]["Phone"].ToString();
                        Judge.RealName = dtJudge.Rows[0]["RealName"].ToString();
                        Judge.Research = dtJudge.Rows[0]["Research"].ToString();
                        Judge.Sex = dtJudge.Rows[0]["Sex"].ToString();
                        Judge.Title = dtJudge.Rows[0]["Title"].ToString();
                        Judge.UserId = Convert.ToInt32(dtJudge.Rows[0]["UserId"]);
                        Judge.Id = Convert.ToInt32(dtJudge.Rows[0]["Id"]);
                        Judges.Add(Judge);
                    }
                }
            }
            return Judges;
        }

        #endregion
    }
}
