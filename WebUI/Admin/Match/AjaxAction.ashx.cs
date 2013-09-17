using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchManagementSystem.Admin.Match
{
    /// <summary>
    /// AjaxAction 的摘要说明
    /// </summary>
    public class AjaxAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string dowhat=context.Request["dowhat"];

            #region 添加比赛信息
            if (dowhat == "add")
            {
                string MatchName = context.Request["MatchName"];
                string DeadLine = context.Request["DeadLine"];
                string Status = context.Request["Status"];
                string DeclarantDeadLine = context.Request["DeclarantDeadLine"];
                string MatchModel = context.Request["MatchModel"];
                string College = context.Request["College"];
                string Rule = context.Request["Rule"];
                string ScoreIntro = context.Request["ScoreIntro"];
                string Judges=context.Request["Judge"];
                if (BLL.Match.CreateMatch(MatchName,DeadLine,Status,DeclarantDeadLine,
                        MatchModel,College,Rule,ScoreIntro) > 0)
                {
                    Models.DB.Match Match=BLL.Match.SelectMatch(MatchName,DeadLine,Status,DeclarantDeadLine,
                        MatchModel,College,Rule,ScoreIntro);
                    if (Match.ID != 0)
                    {
                        List<string> judgeList = new List<string>();
                        List<string> matchList = new List<string>();
                        List<string> enableList = new List<string>();

                        string[] judges={};
                        if(!string.IsNullOrEmpty(Judges))
                        {
                            judges = Judges.Split(',');
                        }
                        for (int i = 0; i < judges.Length; i++)
                        {
                            judgeList.Add(judges[i]);
                            matchList.Add(Match.ID+"");
                            enableList.Add("true");
                        }
                        if (BLL.MatchJuryRelation.CreateMatchJuryRelation(matchList, judgeList, enableList) == judges.Length)
                        {
                            context.Response.Write("SUCCESS");
                            context.Response.End();
                            return;
                        }
                    }
                    context.Response.Write("JUDGEFAILD");
                    context.Response.End();
                    return;

                }
                context.Response.Write("FAILD");
                context.Response.End();
                return;

            }
            #endregion

            #region 删除比赛信息
            else if (dowhat == "delete")
            {
                string Id = context.Request["Id"];
                if (BLL.Delete.Word("Tb_Match",Id)>0)
                {
                    BLL.MatchJuryRelation.DeleteMatchJuryRelation(Id);
                    context.Response.Write("SUCCESS");
                    context.Response.End();
                    return;
                }
                context.Response.Write("FAILD");
                context.Response.End();
                return;
            }
            #endregion

            #region 修改比赛信息
            else if (dowhat == "update")
            {
                string Id = context.Request["Id"];
                string MatchName = context.Request["MatchName"];
                string DeadLine = context.Request["DeadLine"];
                string Status = context.Request["Status"];
                string DeclarantDeadLine = context.Request["DeclarantDeadLine"];
                string MatchModel = context.Request["MatchModel"];
                string College = context.Request["College"];
                string Rule = context.Request["Rule"];
                string ScoreIntro = context.Request["ScoreIntro"];
                string Judges = context.Request["Judge"];
                try { Convert.ToInt32(Id); }
                catch {
                    context.Response.Write("FAILD");
                    context.Response.End();
                    return;
                }
                if (BLL.Match.UpdateMatch(Id, MatchName, DeadLine, Status,  DeclarantDeadLine,
                        MatchModel, College, Rule, ScoreIntro) <= 0)
                {
                    context.Response.Write("FAILD");
                    context.Response.End();
                    return;
                }
                BLL.MatchJuryRelation.DeleteMatchJuryRelation(Id);
                List<string> judgeList = new List<string>();
                List<string> matchList = new List<string>();
                List<string> enableList = new List<string>();
                string[] judges = { };
                if (!string.IsNullOrEmpty(Judges))
                {
                    judges = Judges.Split(',');
                }
                for (int i = 0; i < judges.Length; i++)
                {
                    judgeList.Add(judges[i]);
                    matchList.Add(Id);
                    enableList.Add("true");
                }
                if (BLL.MatchJuryRelation.CreateMatchJuryRelation(matchList, judgeList, enableList) == judges.Length)
                {
                    context.Response.Write("SUCCESS");
                    context.Response.End();
                    return;
                }
                
                context.Response.Write("FAILD");
                context.Response.End();
                return;

            }
            #endregion

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}