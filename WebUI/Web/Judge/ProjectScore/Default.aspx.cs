using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.Judge.ProjectScore
{
    public partial class Default : System.Web.UI.Page
    {
        protected String UserID;
        protected Models.DB.JudgeInfoModel Judge;
        protected List<Models.DB.JudgeInfoModel> JudgeList;

        protected String Index;
        protected int index;
        protected int currentPage;
        protected int ProjectCount;

        protected Models.DB.CupProjectModel Project;
        protected Models.DB.Match Match;
        protected String FilePath;
        protected Models.DB.ProjectScore Score;
        protected List<Models.DB.ProjectScore> ScoreList;
        protected Boolean HasRecord;
        protected String MatchID = "";

        protected Boolean showPdf;

        protected List<Models.DB.CupProjectModel> Projects = new List<Models.DB.CupProjectModel>();
        protected List<Models.DB.InnovationProjectModel> Innovations = new List<Models.DB.InnovationProjectModel>();
        protected Models.DB.CheckRecord CheckRecrod = new Models.DB.CheckRecord();

        protected Models.DB.InnovationProjectModel Innovation;
        protected String ProjectID;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (String.IsNullOrEmpty(Request["MatchID"]) || String.IsNullOrEmpty(Request["Index"]) || String.IsNullOrEmpty(Request["currentPage"]))
            {
                Response.Redirect("../Project/Default.aspx?MatchID="+MatchID);
            }
            currentPage = 1;
            try
            {
                currentPage = Convert.ToInt32(Request["currentPage"]);
                index = Convert.ToInt32(Request["Index"]);
            }
            catch
            {
                Response.Redirect("../Project/Default.aspx?MatchID="+MatchID);
            }
            MatchID = Request["MatchID"];
            Match = BLL.Match.SelectOne(Convert.ToInt32(MatchID));



            /*************根据模型*************************/
            if (Match.MatchModel == 1)
            {
                Projects = BLL.CupProjectModel.FindByInt(MatchID, "MatchID");
                for (int i = 0; i < Projects.Count; i++)
                {
                    CheckRecrod = BLL.CheckRecord.FindOne(Projects[i].ID.ToString(), Match.MatchModel.ToString());
                    if (CheckRecrod == null || CheckRecrod.AfterStatus != "初审通过")
                    {
                        Projects.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
                ProjectCount = Projects.Count;
                try{
                    Project = Projects[Math.Max((currentPage - 1) * 10, 0) + (index - 1)];
                    
                }
                catch
                {
                    Project = new Models.DB.CupProjectModel();
                }
                ProjectID = Project.ID.ToString();
                if (Project.PdfUrl != "")
                {
                    String swf = Project.PdfUrl.Substring(0, Project.PdfUrl.LastIndexOf('.'));
                    if (Utility.PDF2Swf.DoPDF2Swf(Server.MapPath(Project.PdfUrl), Server.MapPath(swf + ".swf")))
                    {
                        FilePath = ResolveUrl(swf + ".swf");
                        showPdf = true;
                    }
                    else
                    {
                        Response.Write("<script language=\"javascript\" type=\"text/javascript\">");
                        Response.Write("alert(\"无法找到源文件\");");
                        Response.Write("</script>");
                        showPdf = false;
                        //     Response.Redirect("../Project/Default.aspx");
                    }

                }
                else
                {
                    showPdf = false;
                }
            }
            else if (Match.MatchModel == 2)
            {
                Innovations = BLL.InnovationProjectModel.FindByInt(MatchID,"MatchID");
                for (int i = 0; i < Innovations.Count; i++)
                {
                    CheckRecrod = BLL.CheckRecord.FindOne(Innovations[i].Id.ToString(), Match.MatchModel.ToString());
                    if (CheckRecrod == null || CheckRecrod.AfterStatus != "初审通过")
                    {
                        Innovations.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
                ProjectCount = Innovations.Count;
                try
                {
                    Innovation = Innovations[Math.Max((currentPage - 1) * 10, 0) + (index - 1)];
                }
                catch
                {
                    Innovation = new Models.DB.InnovationProjectModel();
                }
                ProjectID = Innovation.Id.ToString();
                if (Innovation.PdfUrl != "")
                {
                    String swf = Innovation.ProjectDoc.Substring(0, Innovation.ProjectDoc.LastIndexOf('.'));
                    if (Utility.PDF2Swf.DoPDF2Swf(Server.MapPath(Innovation.ProjectDoc), Server.MapPath(swf + ".swf")))
                    {
                        FilePath = ResolveUrl(swf + ".swf");
                        showPdf = true;
                    }
                    else
                    {
                        Response.Write("<script language=\"javascript\" type=\"text/javascript\">");
                        Response.Write("alert(\"无法找到源文件\");");
                        Response.Write("</script>");
                        showPdf = false;
                        //     Response.Redirect("../Project/Default.aspx");
                    }

                }
                else
                {
                    showPdf = false;
                }
                
            }
            /**************************************/

           

             if (Session["user"] == null)
            {
                Response.Redirect(ResolveUrl("~/Web/Login/Default.aspx"));
            }
            UserID = Session["user"].ToString();
            JudgeList  = BLL.JudgeInfoModel.FindByInt(UserID,"UserID");
            if (JudgeList.Count > 0)
            {
                Judge = JudgeList[0];
            }
            else
            {
                Judge = new Models.DB.JudgeInfoModel();
            }

            Score = BLL.ProjectScore.FindOne(ProjectID,Match.MatchModel.ToString(),Judge.Id.ToString());
            if(Score==null)
            {
                HasRecord = false;
                Score = new Models.DB.ProjectScore();
            }
            else
            {
                HasRecord=true;
            }
            

           

        }
    }
}