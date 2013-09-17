using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.MyWorks
{
    public partial class Maker : System.Web.UI.Page
    {
        protected string FileOne;
        protected string FileTwo = "";
        protected string UrlOne;
        protected string UrlTwo = "";
        private string Project;
        private string Model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["project"] == null || Request["model"] == null)
            {
                Response.Write("<script language=#>history.go(-1);</script>");
            }
            else {
                Project = Request["project"];
                Model = Request["model"];
                CreatePDF();
            }
        }
        private void CreatePDF()
        {
            if (Model == "CupProjectModel")
            {
                string path = Server.MapPath("../");
                CupProjectModelPDF Maker = new CupProjectModelPDF(Project, path + "PDF/", path + "MyWorks/Fonts/");
                FileOne = Maker.file;
                UrlOne = Maker.Url;
                string Url = "~/Web/PDF/" + Maker.Url;
                BLL.CupProjectModel.UpdataPdfUrl(Project,Url);
            }
            else if(Model=="InnovationProjectModel")
            {
                string path = Server.MapPath("../");
                InnovationProjectModelPDF Maker = new InnovationProjectModelPDF(Project, path + "PDF/", path + "MyWorks/Fonts/",path+"img/");
                FileOne = Maker.fileOne;
                UrlOne = Maker.UrlOne;
                FileTwo = Maker.fileTwo;
                UrlTwo = Maker.UrlTwo;
                string UrlPDF = "~/Web/PDF/" + Maker.UrlTwo;
                string UrlWORD = "~/Web/PDF/" + Maker.UrlOne;
                BLL.InnovationProjectModel.UpdataUrl(Project, UrlPDF,UrlWORD);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.ContentType = "application/x-zip-compressed";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", FileOne));
                string filename = Server.MapPath("../PDF/" +FileOne);
                Response.TransmitFile(filename);
                Response.Write("<script language=\"javascript\" type=\"text/javascript\">");
                Response.Write("alert(\"下载成功\");");
                Response.Write("window.location.href=\"Default.aspx\";");
                Response.Write("</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('没有文件记录！无法下载！');</script>");
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Response.ContentType = "application/x-zip-compressed";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", FileTwo));
                string filename = Server.MapPath("../PDF/" + FileTwo);
                Response.TransmitFile(filename);
                Response.Write("<script language=\"javascript\" type=\"text/javascript\">");
                Response.Write("alert(\"下载成功\");");
                Response.Write("window.location.href=\"Default.aspx\";");
                Response.Write("</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('没有文件记录！无法下载！');</script>");
            }
        }

       
    }
}