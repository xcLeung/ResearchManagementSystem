using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ResearchManagementSystem.Web.MyWorks
{
    public class InnovationProjectModelPDF
    {
        private Models.DB.InnovationProjectModel Project;
        private Models.DB.Match Match;
        private List<Models.DB.InnovationTeamMember> TeamMembers;
        private Models.DB.InnovationWorksInfo WorksInfo;
        private Models.DB.InnovationDeclarantInfo Declarant;
        private Models.DB.StudentInfoModel Student;
        private List<Models.DB.TutorInfo> Tutors;
        private BaseFont KaiTiGB2312;
        private BaseFont HeiTi;
        private BaseFont LiShu;
        private BaseFont SongTi;
        private BaseFont FangSongGB2312;
        private BaseFont TimesNewRoman;
        private static float 一号 = 26;
        private static float 二号 = 22;
        private static float 小二 = 18;
        private static float 三号 = 16;
        private static float 小三 = 15;
        private static float 四号 = 14;
        private static float 小四 = 12;
        private static float 五号 = 10.5f;
        private static float 小五 = 9;
        private string ImgPath;
        public string fileOne;
        public string UrlOne;
        public string fileTwo;
        public string UrlTwo;

        public InnovationProjectModelPDF(string ProjectId, string path, string fontPath, string imgPath)
        {
            ImgPath = imgPath;
            initFont(fontPath);
            initData(ProjectId);
            CreateDeclaration(path);
            CreateAssignment(path);
        }
        private void initFont(string Path)
        {
            KaiTiGB2312 = BaseFont.CreateFont(Path + "楷体_GB2312.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            HeiTi = BaseFont.CreateFont(Path + "simhei.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            LiShu = BaseFont.CreateFont(Path + "SIMLI.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            SongTi = BaseFont.CreateFont(Path + "simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            FangSongGB2312 = BaseFont.CreateFont(Path + "仿宋_GB2312.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            TimesNewRoman = BaseFont.CreateFont(Path + "times.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        }
        private bool initData(string ProjectId)
        {
            List<Models.DB.InnovationProjectModel> Projects = BLL.InnovationProjectModel.FindByInt(ProjectId, "Id");
            if (Projects.Count <= 0)
            {
                return false;
            }
            Project = Projects[0];
            Match = BLL.Match.SelectOne(Project.MatchID);
            List<Models.DB.InnovationWorksInfo> WorksInfos = BLL.InnovationWorksInfo.FindByInt(ProjectId, "ProjectId");
            if (WorksInfos.Count > 0)
            {
                WorksInfo = WorksInfos[0];
            }
            TeamMembers = BLL.InnovationTeamMember.SelectByProjectId(ProjectId);
            Declarant = BLL.InnovationDeclarantInfo.SelectOne(ProjectId);
            Tutors = BLL.Tutors.SelectByProjectId(ProjectId);
            Student = BLL.StudentInfoModel.SelectOneByUserId(Project.UserID + "");
            return true;
        }
        private void CreateAssignment(string path)
        {
            try
            {
                float top = (float)(2.75 / 2.54) * 72;
                float left = (float)(2.80 / 2.54) * 72;
                float right = (float)(2.60 / 2.54) * 72;
                Document document = new Document(PageSize.A4, left, right, top, top);
                fileTwo =  Project.Name + "任务书.pdf";
                UrlTwo = Student.StudentID + "/" + Project.Name + "任务书.pdf";
                if (!Directory.Exists(path + Student.StudentID + "/"))
                    Directory.CreateDirectory(path + Student.StudentID + "/");
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + Student.StudentID + "/" + Project.Name + "任务书.pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + "任务书.pdf", FileMode.Create));
                document.Open();
                CreateAssignmentTable(document);
                document.Close();
            }
            catch (DocumentException de)
            {

            }
        }

        private void CreateAssignmentTable(Document document)
        {
            Font font;
            Font fillFont = new Font(SongTi, 五号, Font.NORMAL);
            #region 表名
            font = new Font(HeiTi, 小二, Font.BOLD);
            Paragraph paragrah = new Paragraph(28, "华南农业大学生创新创业训练计划项目任务书", font);
            paragrah.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragrah);
            #endregion
            #region 说明
            font = new Font(SongTi, 小四, Font.NORMAL);
            paragrah = new Paragraph(20, "为保证项目的有效实施，学校主管单位与获得立项资助的项目负责人订立本任务书，请遵照执行。", font);
            paragrah.FirstLineIndent = 24;
            document.Add(paragrah);
            document.Add(NewLine(SongTi, 2, 2));
            #endregion
            #region 表
            List<float> widths = new List<float>();
            List<PdfPCell> PdfPCells = new List<PdfPCell>();
            PdfPTable table = new PdfPTable(new float[] { 2.77f, 13.39f });
            table.TotalWidth = 458.1f;
            table.LockedWidth = true;
            #region 项目名称
            font = new Font(SongTi, 五号, Font.NORMAL);
            PdfPCell cell = new PdfPCell(new Phrase("项目名称", font));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 22.67f;
            table.AddCell(cell);
            widths.Add(8.95f); widths.Add(1.9f); widths.Add(2.54f);
            string str=Project==null?"":Project.Name;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 22.67f;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("项目编号", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 22.67f;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("", fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 22.67f;
            PdfPCells.Add(cell);
            table.AddCell(new PdfPCell(CreateCell(379.58f, widths, PdfPCells)));
            #endregion
            #region 项目负责人
            cell = new PdfPCell(new Phrase("项目负责人", font));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 22.67f;
            table.AddCell(cell);
            widths.Clear();
            widths.Add(2.5f); widths.Add(1.9f); widths.Add(8.99f);
            str = Student == null ? "" : Student.Name;
            PdfPCells[0].Phrase = new Phrase(str, fillFont);
            PdfPCells[1].Phrase = new Phrase("所在学校（院系）", font);
            str = Student == null ? "" : Student.School + " " + Student.College;
            PdfPCells[2].Phrase = new Phrase(str, fillFont);
            table.AddCell(new PdfPCell(CreateCell(379.58f, widths, PdfPCells)));
            #endregion
            #region 项目成员
            cell = new PdfPCell(new Phrase("项目成员", font));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 22.67f;
            table.AddCell(cell);
            widths.Clear();
            widths.Add(6.63f); widths.Add(2.32f); widths.Add(4.44f);
            str = "";
            for (int i = 0; i < TeamMembers.Count; i++)
            {
                str += TeamMembers[i].Name + " ";
            }
            PdfPCells[0].Phrase = new Phrase(str, fillFont);
            PdfPCells[1].Phrase = new Phrase("指导教师", font);
            str = "";
            for (int i = 0; i < Tutors.Count; i++)
            {
                str += Tutors[i].Name + " ";
            }
            PdfPCells[2].Phrase = new Phrase(str, fillFont);
            table.AddCell(new PdfPCell(CreateCell(379.58f, widths, PdfPCells)));
            #endregion
            #region 预期提供成果及成果形式
            cell = new PdfPCell(new Phrase("预期提供成果及成果形式", font));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 60.1f;
            table.AddCell(cell);

            str = WorksInfo == null ? "" : WorksInfo.Expection;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 60.1f;
            cell.SetLeading(0, 1.2f);
            table.AddCell(cell);

            #endregion
            #region 支助经费（元）及用途
            cell = new PdfPCell(new Phrase("支助经费（元）及用途", font));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 43.66f;
            table.AddCell(cell);

            str = WorksInfo == null ? "" : WorksInfo.Budget;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 43.66f;
            cell.SetLeading(0, 1.2f);
            table.AddCell(cell);
            #endregion
            #region 提交中期报告
            cell = new PdfPCell(new Phrase("提交中期报告", font));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 36.85f;
            table.AddCell(cell);
            str = Project == null ? "" : Project.InterimReport;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 36.85f;
            table.AddCell(cell);
            #endregion
            #region 提交成果
            cell = new PdfPCell(new Phrase("提交成果", font));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 36.85f;
            table.AddCell(cell);
            str = Project == null ? "" : Project.SubmitAchievement;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 36.85f;
            table.AddCell(cell);
            #endregion
            #region 管理依据
            cell = new PdfPCell(new Phrase("管理依据", font));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 36.85f;
            table.AddCell(cell);
            str = Project == null ? "" : Project.Managementbasis;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 36.85f;
            table.AddCell(cell);
            #endregion
            #region 同意以上所列内容，在此签字盖章
            cell = new PdfPCell(new Phrase("同意以上所列内容，在此签字盖章", font));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 82.77f;
            table.AddCell(cell);
            widths.Clear();
            widths.Add(6.42f); widths.Add(6.79f);
            PdfPCells.Clear();
            cell = new PdfPCell(new Phrase(" 甲方：        教务处（公章）" + Environment.NewLine
                + Environment.NewLine
                + Environment.NewLine +
                "     负责人：" + Environment.NewLine
                + Environment.NewLine
                + Environment.NewLine
                + Environment.NewLine
                + "              年    月    日", font));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 82.77f;
            cell.SetLeading(0, 1.5f);
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase(" 乙方：项目负责人：" + Environment.NewLine
                + Environment.NewLine
                + Environment.NewLine +
                "     指导教师：" + Environment.NewLine
                + Environment.NewLine
                + "    承担项目学院（盖章）" + Environment.NewLine
                + "              年    月    日", font));
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 82.77f;
            cell.SetLeading(0, 1.5f);
            PdfPCells.Add(cell);
            table.AddCell(new PdfPCell(CreateCell(379.58f, widths, PdfPCells)));
            #endregion

            document.Add(table);
            #endregion
            #region 备注
            font = new Font(KaiTiGB2312, 小四, Font.NORMAL);
            paragrah = new Paragraph(20, "注：本任务书一式三份，一份留计划项目主管部门，一份留项目负责人所在院系，一份由项目负责人保存。", font);
            paragrah.FirstLineIndent = 24;
            document.Add(paragrah);
            #endregion
        }
        private void CreateDeclaration(string path)
        {
            try
            {
                float top = (float)(2.75 / 2.54) * 72;
                float left = (float)(2.80 / 2.54) * 72;
                float right = (float)(2.60 / 2.54) * 72;
                Document document = new Document(PageSize.A4, left, right, top, top);
                fileOne =  Project.Name + "申报书.pdf";
                UrlOne = Student.StudentID + "/" + Project.Name + "申报书.pdf";
                if (!Directory.Exists(path + Student.StudentID + "/"))
                    Directory.CreateDirectory(path + Student.StudentID + "/");
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + Student.StudentID + "/" + Project.Name + "申报书.pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path +"申报书.pdf", FileMode.Create));
                document.Open();
                CreateCover(document);
                document.NewPage();
                CreateNotice(document);
                document.NewPage();
                CreateTablePartOne(document);
                document.NewPage();
                CreateTablePartTwo(document);
                document.Close();
            }
            catch (DocumentException de)
            {

            }
        }

        private void CreateCover(Document document)
        {
            Font font;
            document.Add(NewLine(SongTi, 28, 四号));
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(ImgPath + "scau.jpg");
            img.ScalePercent(10f);
            document.Add(img);
            font = new Font(HeiTi, 一号);
            Paragraph paragraph = new Paragraph(38, "国家级/省级大学生创新创业训练计划项目申报书", font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            document.Add(NewLine(FangSongGB2312, 32, 三号));
            document.Add(NewLine(FangSongGB2312, 32, 三号));
            document.Add(NewLine(FangSongGB2312, 32, 三号));
            document.Add(NewLine(FangSongGB2312, 32, 三号));

            #region 推荐学院
            string str = "推 荐 学 院";
            font = new Font(FangSongGB2312, 四号);
            Phrase phrase = new Phrase(28, str, font);
            paragraph = new Paragraph();
            paragraph.Add(phrase);
            str = " ";
            font = new Font(KaiTiGB2312, 四号, Font.UNDERLINE);
            phrase = new Phrase(28, DealWithString(20, str), font);
            paragraph.Add(phrase);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            paragraph.SetLeading(1.5f, 0);
            document.Add(paragraph);
            #endregion

            #region 项目名称
            str = "项 目 名 称";
            font = new Font(FangSongGB2312, 四号);
            phrase = new Phrase(28, str, font);
            paragraph = new Paragraph();
            paragraph.Add(phrase);
            str =Project==null?"": Project.Name;
            font = new Font(KaiTiGB2312, 四号, Font.UNDERLINE);
            phrase = new Phrase(28, DealWithString(20, str), font);
            paragraph.Add(phrase);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            paragraph.SetLeading(28f, 0);
            document.Add(paragraph);
            #endregion

            #region 项目类型
            str = "项 目 类 型";
            font = new Font(FangSongGB2312, 四号);
            phrase = new Phrase(28, str, font);
            paragraph = new Paragraph();
            paragraph.Add(phrase);
            str = "□创新训练项目□创业训练项目□创业实践项目";
            font = new Font(KaiTiGB2312, 四号, Font.UNDERLINE);
            phrase = new Phrase(28, DealWithString(20, str), font);
            paragraph.Add(phrase);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            paragraph.SetLeading(28f, 0);
            document.Add(paragraph);
            #endregion

            #region 所属一级学科名称
            str = "所属一级学科名称";
            font = new Font(FangSongGB2312, 四号);
            phrase = new Phrase(28, str, font);
            paragraph = new Paragraph();
            paragraph.Add(phrase);
            str = "";
            font = new Font(KaiTiGB2312, 四号, Font.UNDERLINE);
            phrase = new Phrase(28, DealWithString(18, str), font);
            paragraph.Add(phrase);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            paragraph.SetLeading(28f, 0);
            document.Add(paragraph);
            #endregion
            #region 所属二级学科名称
            str = "所属二级学科名称";
            font = new Font(FangSongGB2312, 四号);
            phrase = new Phrase(28, str, font);
            paragraph = new Paragraph();
            paragraph.Add(phrase);
            str = "";
            font = new Font(KaiTiGB2312, 四号, Font.UNDERLINE);
            phrase = new Phrase(28, DealWithString(18, str), font);
            paragraph.Add(phrase);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            paragraph.SetLeading(28f, 0);
            document.Add(paragraph);
            #endregion
            #region 项 目 负 责 人
            str = "项 目 负 责 人";
            font = new Font(FangSongGB2312, 四号);
            phrase = new Phrase(28, str, font);
            paragraph = new Paragraph();
            paragraph.Add(phrase);

            str = Student == null ? "" : Student.Name;
            font = new Font(KaiTiGB2312, 四号, Font.UNDERLINE);
            phrase = new Phrase(28, DealWithString(19, str), font);
            paragraph.Add(phrase);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            paragraph.SetLeading(28f, 0);
            document.Add(paragraph);
            #endregion
            #region 申 报 日 期
            str = "申 报 日 期";
            font = new Font(FangSongGB2312, 四号);
            phrase = new Phrase(28, str, font);
            paragraph = new Paragraph();
            paragraph.Add(phrase);
            str =Project==null?"": Project.DeclarationDate.Year + "年" + Project.DeclarationDate.Month + "月" + Project.DeclarationDate.Day + "日";
            font = new Font(KaiTiGB2312, 四号, Font.UNDERLINE);
            phrase = new Phrase(28, DealWithString(20, str), font);
            paragraph.Add(phrase);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            paragraph.SetLeading(28f, 0);
            document.Add(paragraph);
            #endregion

            document.Add(NewLine(HeiTi, 16, 三号));
            document.Add(NewLine(HeiTi, 16, 三号));
            document.Add(NewLine(HeiTi, 16, 三号));
            font = new Font(HeiTi, 三号, Font.BOLD);
            str = "广东省教育厅  制" + Environment.NewLine + "二○一二年三月";
            paragraph = new Paragraph(str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);

        }

        private string DealWithString(int length, string str)
        {
            string ret = "";
            if (str.Length >= length)
            {
                return str;
            }
            for (int i = 0; i < length - str.Length; ++i)
            {
                ret += " ";
            }
            int count = 0;
            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] > 0 && str[i] < 128)
                {
                    ++count;
                    if (count == 2)
                    {
                        ret += " ";
                        count = 0;
                    }
                }
            }
            if (count == 0)
            {
                return ret + str + ret;
            }
            return ret + str + ret + " ";
        }

        private void CreateNotice(Document document)
        {
            Font font = new Font(KaiTiGB2312, 二号);
            Paragraph paragraph = new Paragraph(28, "填 写 须 知", font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            document.Add(NewLine(KaiTiGB2312, 28, 小二));
            HTMLWorker worder = new HTMLWorker(document);
            StringReader sr = new StringReader(Match.Rule);
            string str = Match.Rule;
            font = new Font(FangSongGB2312, 小四);
            worder.Parse(sr, font);
        }
        private void CreateTablePartOne(Document document)
        {
            Font font = new Font(SongTi, 小四, Font.BOLD);
            Font fillFont = new Font(SongTi, 五号);
            PdfPTable table = new PdfPTable(new float[] { 1.12f, 1.69f, 13.19f });
            table.TotalWidth = 453.6f;
            table.LockedWidth = true;
            #region 项目名称
            PdfPCell cell = new PdfPCell(new Phrase("项目名称", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 22.68f;
            cell.Colspan = 2;
            table.AddCell(cell);
            string str = Project == null ? "" : Project.Name;
            cell = new PdfPCell(new Phrase(str, font));
            //cell = new PdfPCell(new Phrase(" ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 22.68f;

            table.AddCell(cell);
            #endregion
            #region 项目类型
            cell = new PdfPCell(new Phrase("项目类型", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 22.68f;
            cell.Colspan = 2;
            table.AddCell(cell);

            str = "（ ）创新训练项目  （ ）创业训练项目  （ ）创业实践项目";
            cell = new PdfPCell(new Phrase(str, font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 22.68f;
            table.AddCell(cell);
            #endregion
            #region 项目实施时间
            cell = new PdfPCell(new Phrase("项目实施时间", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 22.68f;
            cell.Colspan = 2;
            table.AddCell(cell);
            str =Project==null?"": "起始时间：" + Project.StartTime.Year + "年" + Project.StartTime.Month + "月" + "     " +
                         "结束时间：" + Project.EndTime.Year + "年" + Project.EndTime.Month + "月"; ;
            cell = new PdfPCell(new Phrase(str, font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 22.68f;
            table.AddCell(cell);
            #endregion
            #region 申请人或申请团队
            cell = new PdfPCell(new Phrase("申请人或申请团队", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.PaddingLeft = 10f;
            cell.Rowspan = 6;
            table.AddCell(cell);
            List<float> widths = new List<float>();
            widths.Add(1.69f); widths.Add(1.54f); widths.Add(1.21f); widths.Add(1.74f);
            widths.Add(3.19f); widths.Add(2.63f); widths.Add(2.98f);
            List<PdfPCell> PdfPCells = new List<PdfPCell>();
            cell = new PdfPCell(new Phrase(" ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("姓名", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0, 1.3f);
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("年" + Environment.NewLine + "级", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0, 1.3f);
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("学号", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0, 1.3f);
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("所在院系" + Environment.NewLine + "/专业", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0, 1.3f);
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("联系电话", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0, 1.3f);
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("E-mail", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0, 1.3f);
            PdfPCells.Add(cell);
            cell = new PdfPCell(CreateCell(421.82f, widths, PdfPCells));
            cell.Colspan = 2;
            table.AddCell(cell);
            PdfPCells[0].Phrase = new Phrase("主持人", font);
            str = Student==null ? "":Student.Name;
            PdfPCells[1].Phrase = new Phrase(str, fillFont);
            str = Student == null ? "" : Student.InTimeYear + "级";
            PdfPCells[2].Phrase = new Phrase(str, fillFont);
            str = Student == null ? "" : Student.StudentID;
            PdfPCells[3].Phrase = new Phrase(str, fillFont);
            str = Student == null ? "" : Student.College + "/" + Student.Major;
            PdfPCells[4].Phrase = new Phrase(str, fillFont);
            str = Declarant == null ? "" : Declarant.Phone;
            PdfPCells[5].Phrase = new Phrase(str, fillFont);
            str = Student == null ? "" : Student.Mail;
            PdfPCells[6].Phrase = new Phrase(str, fillFont);
            cell = new PdfPCell(CreateCell(421.82f, widths, PdfPCells));
            cell.Colspan = 2;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("成  员", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.Rowspan = 4;
            table.AddCell(cell);
            widths.RemoveAt(0);
            PdfPCells.RemoveAt(0);
            for (int i = 0; i < TeamMembers.Count; i++)
            {
                PdfPCells[0].Phrase = new Phrase(TeamMembers[i].Name, fillFont);
                PdfPCells[1].Phrase = new Phrase(TeamMembers[i].InTimeYear + "级", fillFont);
                PdfPCells[2].Phrase = new Phrase(TeamMembers[i].StudentID, fillFont);
                PdfPCells[3].Phrase = new Phrase(TeamMembers[i].College + "/" + TeamMembers[i].Major, fillFont);
                PdfPCells[4].Phrase = new Phrase(TeamMembers[i].Phone, fillFont);
                PdfPCells[5].Phrase = new Phrase(TeamMembers[i].Mail, fillFont);
                table.AddCell(new PdfPCell(CreateCell(373.91f, widths, PdfPCells)));
            }
            for (int i = 0; i < 4 - TeamMembers.Count; i++)
            {
                PdfPCells[0].Phrase = new Phrase(" ", fillFont);
                PdfPCells[1].Phrase = new Phrase(" ", fillFont);
                PdfPCells[2].Phrase = new Phrase(" ", fillFont);
                PdfPCells[3].Phrase = new Phrase(" ", fillFont);
                PdfPCells[4].Phrase = new Phrase(" ", fillFont);
                PdfPCells[5].Phrase = new Phrase(" ", fillFont);
                table.AddCell(new PdfPCell(CreateCell(373.91f, widths, PdfPCells)));
            }
            #endregion
            #region 指导教师
            fillFont = new Font(SongTi, 小四);
            cell = new PdfPCell(new Phrase("指导教师（校内导师或企业导师", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.Rowspan = 5;
            table.AddCell(cell);
            widths.Clear();
            widths.Add(1.69f); widths.Add(1.54f); widths.Add(1.21f); widths.Add(1.74f);
            widths.Add(3.19f); widths.Add(2.63f); widths.Add(2.98f);
            PdfPCells.RemoveAt(0); PdfPCells.RemoveAt(0);
            PdfPCells[0].Phrase = new Phrase("姓名", font);
            str = Tutors.Count > 0 ? Tutors[0].Name : " ";
            PdfPCells[1].Phrase = new Phrase(str, fillFont);
            PdfPCells[1].Colspan = 3;
            PdfPCells[2].Phrase = new Phrase("研究方向", font);
            str = Tutors.Count > 0 ? Tutors[0].Reasearch : " ";
            PdfPCells[3].Phrase = new Phrase(str, fillFont);
            PdfPCells[3].Colspan = 2;
            cell = new PdfPCell(CreateCell(421.82f, widths, PdfPCells));
            cell.Colspan = 2;
            table.AddCell(cell);
            PdfPCells[0].Phrase = new Phrase("年龄", font);
            str = Tutors.Count > 0 ? Tutors[0].Age + "" : " ";
            PdfPCells[1].Phrase = new Phrase(str, fillFont);
            PdfPCells[1].Colspan = 3;
            PdfPCells[2].Phrase = new Phrase("行政职务/专业" + Environment.NewLine + "技术职务", font);
            str = Tutors.Count > 0 ? Tutors[0].Work : " ";
            PdfPCells[3].Phrase = new Phrase(str, fillFont);
            PdfPCells[3].Colspan = 2;
            cell = new PdfPCell(CreateCell(421.82f, widths, PdfPCells));
            cell.Colspan = 2;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("主要" + Environment.NewLine + "成果", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 300f;
            cell.Rowspan = 3;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(" ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 1f;
            cell.BorderWidth = 0;
            table.AddCell(cell);
            str = Tutors.Count > 0 ? Tutors[0].Achieves : " ";
            int page = 1;
            str = AddCell(table, str, fillFont, page);
            if (str.Length > 0)
            {
                cell = new PdfPCell(new Phrase(str, fillFont));
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.SetLeading(0, 1.5f);
                table.AddCell(cell);
            }
            #endregion
            document.Add(table);
        }
        private string AddCell(PdfPTable table, string content, Font font, int page)
        {
            int s = 0, b = content.Length - 1, m = b + 1;
            string str;
            PdfPCell cell = new PdfPCell(new Phrase(content, font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 300f;
            cell.SetLeading(0, 1.5f);
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            table.AddCell(cell);
            while (b > s)
            {
                float f = table.TotalHeight;

                if (table.TotalHeight > 675 * page)
                {
                    b = m - 1;
                }
                else
                {
                    s = m + 1;
                }
                m = (b + s) / 2;
                table.DeleteLastRow();
                str = content.Substring(0, m);
                cell = new PdfPCell(new Phrase(str, font));
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.MinimumHeight = 300f;
                cell.SetLeading(0, 1.5f);
                cell.BorderWidthTop = 0;
                cell.BorderWidthLeft = 0;
                table.AddCell(cell);
            }
            s = content.Length - 1;
            return content.Substring(b, s - b);
        }
        private void CreateTablePartTwo(Document document)
        {
            Font font = new Font(SongTi, 小四, Font.BOLD);
            Font fillFont = new Font(SongTi, 小四, Font.NORMAL);
            Phrase phrase = new Phrase();
            Chunk chunk;
            PdfPTable table = new PdfPTable(1);
            table.TotalWidth = 453.6f;
            table.LockedWidth = true;
            #region 项目实施的目的、意义
            chunk = new Chunk("一、项目实施的目的、意义" + Environment.NewLine, font);
            phrase.Add(chunk);

            string str = WorksInfo == null ? "" : WorksInfo.Purpose;
            chunk = new Chunk( str+ Environment.NewLine, fillFont);
            phrase.Add(chunk);
            #endregion
            #region 参考文献
            chunk = new Chunk("参考文献" + Environment.NewLine, font);
            phrase.Add(chunk);
            str = WorksInfo == null ? "" : WorksInfo.References;
            chunk = new Chunk(str + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            #endregion
            #region 二、项目研究内容和拟解决的关键问题
            chunk = new Chunk("二、项目研究内容和拟解决的关键问题" + Environment.NewLine, font);
            phrase.Add(chunk);
            chunk = new Chunk("1. 项目研究的基本内容" + Environment.NewLine, font);
            phrase.Add(chunk);
            str = WorksInfo == null ? "" : WorksInfo.BaseContent;
            chunk = new Chunk(str + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            chunk = new Chunk("2．拟解决的关键问题：" + Environment.NewLine, font);
            phrase.Add(chunk);
            str = WorksInfo == null ? "" : WorksInfo.KeyProblem;
            chunk = new Chunk(str + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            #endregion
            #region 三、项目研究与实施的基础条件
            chunk = new Chunk("三、项目研究与实施的基础条件" + Environment.NewLine, font);
            phrase.Add(chunk);
            str = WorksInfo == null ? "" : WorksInfo.ProjectBasic;
            chunk = new Chunk(str + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            #endregion
            #region 四、项目实施方案（计划、技术路线、人员分工等、实施步骤、内容等）
            chunk = new Chunk("四、项目实施方案（", font);
            phrase.Add(chunk);
            chunk = new Chunk("计划、技术路线、人员分工等、实施步骤、内容等", fillFont);
            phrase.Add(chunk);
            chunk = new Chunk(")" + Environment.NewLine, font);
            phrase.Add(chunk);
            chunk = new Chunk("1.具体研究方案" + Environment.NewLine, font);
            phrase.Add(chunk);
            str = WorksInfo == null ? "" : WorksInfo.PracticalsStep;
            chunk = new Chunk(str + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            chunk = new Chunk("2. 人员分工" + Environment.NewLine, font);
            phrase.Add(chunk);
            str = WorksInfo == null ? "" : WorksInfo.PersonnelDivision;
            chunk = new Chunk(str + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            chunk = new Chunk("3. 项目计划" + Environment.NewLine, font);
            phrase.Add(chunk);
            str = WorksInfo == null ? "" : WorksInfo.ProjectPlan;
            chunk = new Chunk(str + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            #endregion
            #region 五、项目特色与创新点
            chunk = new Chunk("五、项目特色与创新点", font);
            phrase.Add(chunk);
            chunk = new Chunk("（原始创新：重大科学发现、技术发明；集成创新：融合多种相关技术，形成新产品、新产业；引进消化吸收再创新：在引进国内外先进技术的基础上，学习、分析、借鉴，形成具有自主知识产权的新技术）" + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            str = WorksInfo == null ? "" : WorksInfo.Features;
            chunk = new Chunk(str + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            PdfPCell cell = new PdfPCell(phrase);
            cell.SetLeading(0, 1.5f);
            table.AddCell(cell);
            #endregion
            #region 六、预期成果
            phrase = new Phrase();
            chunk = new Chunk("六、预期成果", font);
            phrase.Add(chunk);
            chunk = new Chunk("（研究论文、设计、专利、产品、鉴定、推广应用等）" + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            str = WorksInfo == null ? "" : WorksInfo.Expection;
            chunk = new Chunk(str + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            cell = new PdfPCell(phrase);
            cell.SetLeading(0, 1.5f);
            table.AddCell(cell);
            #endregion
            #region 七、经费预算及使用计划
            phrase = new Phrase();
            chunk = new Chunk("七、经费预算及使用计划" + Environment.NewLine, font);
            phrase.Add(chunk);
            str = WorksInfo == null ? "" : WorksInfo.Budget;
            chunk = new Chunk(str + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            cell = new PdfPCell(phrase);
            cell.SetLeading(0, 1.5f);
            table.AddCell(cell);
            #endregion
            #region 八、导师推荐意见
            phrase = new Phrase();
            chunk = new Chunk("八、导师推荐意见" + Environment.NewLine, font);
            phrase.Add(chunk);
            str = Tutors.Count > 0 ? Tutors[0].Recommendation : " ";
            chunk = new Chunk(str + Environment.NewLine, fillFont);
            phrase.Add(chunk);
            chunk = new Chunk("                                    签名： " + Environment.NewLine +
                              "                                                 年   月   日", font);
            phrase.Add(chunk);
            cell = new PdfPCell(phrase);
            cell.SetLeading(0, 1.5f);
            table.AddCell(cell);
            #endregion
            #region 九、院系推荐意见
            phrase = new Phrase();
            chunk = new Chunk("九、院系推荐意见" + Environment.NewLine +
                Environment.NewLine +
                Environment.NewLine +
                Environment.NewLine +
                Environment.NewLine +
                "院系负责人签名：                                   学院盖章" + Environment.NewLine +
                "                                                 年   月   日", font);
            phrase.Add(chunk);
            cell = new PdfPCell(phrase);
            cell.SetLeading(0, 2f);
            cell.MinimumHeight = 180f;
            table.AddCell(cell);
            #endregion
            #region 十、学校推荐意见：
            phrase = new Phrase();
            chunk = new Chunk("十、学校推荐意见：" + Environment.NewLine +
                Environment.NewLine +
                Environment.NewLine +
                Environment.NewLine +
                "                           学校负责人签名：        学校盖章 " + Environment.NewLine +
                "                                                 年   月   日", font);
            phrase.Add(chunk);
            cell = new PdfPCell(phrase);
            cell.SetLeading(0, 1.9f);
            cell.MinimumHeight = 138.9f;
            table.AddCell(cell);
            #endregion
            #region 十一、省教育厅评审意见：
            phrase = new Phrase();
            chunk = new Chunk("十一、省教育厅评审意见：" + Environment.NewLine +
                Environment.NewLine +
                Environment.NewLine +
                Environment.NewLine +
                "                                                   单位盖章 " + Environment.NewLine +
                "                                                 年   月   日", font);
            phrase.Add(chunk);
            cell = new PdfPCell(phrase);
            cell.SetLeading(0, 1.9f);
            cell.MinimumHeight = 147.41f;
            table.AddCell(cell);
            #endregion
            document.Add(table);
            document.Add(new Chunk("注：表格栏高不够可增加。", fillFont));
        }
        private PdfPTable CreateCell(float TotalWidth, List<float> widths, List<PdfPCell> PdfPCells)
        {
            PdfPTable table = new PdfPTable(widths.ToArray());
            table.TotalWidth = TotalWidth;
            for (int i = 0; i < PdfPCells.Count; i++)
            {
                table.AddCell(PdfPCells[i]);
            }
            return table;

        }
        /// <summary>
        /// 换行
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="Leading"></param>
        /// <param name="FontSize"></param>
        /// <returns></returns>
        private Paragraph NewLine(BaseFont bf, float Leading, float FontSize)
        {
            Font font = new Font(bf, FontSize, Font.NORMAL);
            string str = " ";
            Paragraph p = new Paragraph(str, font);
            p.SetLeading(Leading, 0);
            return p;
        }
    }
}
