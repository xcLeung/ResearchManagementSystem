using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ResearchManagementSystem.Web.MyWorks
{
    public class CupProjectModelPDF 
    {
        private Models.DB.CupProjectModel Project;//比赛项目
        private Models.DB.Match Match;//比赛
        private Models.DB.CupWorksInfo CupWorsInfo;//项目情况
        private Models.DB.CupWorksSurvey CupWorksSurvey;//调查报告项目情况
        private Models.DB.CupWorksInvention CupWorksInvention;//专利项目情况
        private Models.DB.StudentInfoModel Student;//申报学生基本信息
        private Models.DB.CupDeclarantInfo Declarant;//申报学生详细信息
        private List<Models.DB.CupTeamMemberInfo> TeamMembers;//组员
        private List<Models.DB.RecommendedInfo> Recommendes;//推荐人
        private BaseFont KaiTiGB2312;
        private BaseFont HeiTi;
        private BaseFont LiShu;
        private BaseFont SongTi;
        private BaseFont FangSongGB2312;
        private static float 小二 = 18;
        private static float 三号 = 16;
        private static float 小三 = 15;
        private static float 四号 = 14;
        private static float 小四 = 12;
        private static float 五号 = 10.5f;
        private static float 小五 = 9;
        public string file;
        public string Url;
        public CupProjectModelPDF(string ProjectId, string path, string fontPath)
        {
            initFont(fontPath);
            initData(ProjectId);
            Create(path);
        }
        private void initFont(string Path)
        {
            string str = Path + "楷体_GB2312.ttf";
            KaiTiGB2312 = BaseFont.CreateFont(Path + "楷体_GB2312.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            HeiTi = BaseFont.CreateFont(Path + "simhei.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            LiShu = BaseFont.CreateFont(Path + "SIMLI.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            SongTi = BaseFont.CreateFont(Path + "simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            FangSongGB2312 = BaseFont.CreateFont(Path + "仿宋_GB2312.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        }
        private bool initData(string ProjectId)
        {
            List<Models.DB.CupProjectModel> Projects = BLL.CupProjectModel.FindByInt(ProjectId, "Id");
            if (Projects.Count <= 0)
            {
                return false;
            }
            Project = Projects[0];
            List<Models.DB.CupWorksInfo> CupWorsInfos = BLL.CupWorksInfo.FindByInt(ProjectId, "ProjectId");
            if (CupWorsInfos.Count > 0)
            {
                CupWorsInfo = CupWorsInfos[0];
            }
            List<Models.DB.CupWorksSurvey> CupWorksSurveys = BLL.CupWorksSurvey.FindByInt(ProjectId, "ProjectId");
            if (CupWorksSurveys.Count > 0)
            {
                CupWorksSurvey = CupWorksSurveys[0];
            }

            List<Models.DB.CupWorksInvention> CupWorksInventions = BLL.CupWorksInvention.FindByInt(ProjectId, "ProjectId");
            if (CupWorksInventions.Count > 0)
            {
                CupWorksInvention = CupWorksInventions[0];
            }

            TeamMembers = BLL.CupTeamMemberInfo.SelectByProjectId(ProjectId);
            Match = BLL.Match.SelectOne(Project.MatchID);
            Recommendes = BLL.RecommendInfo.SelectByProjectId(ProjectId);
            Student = BLL.StudentInfoModel.SelectOneByUserId(Project.UserID + "");
            Declarant = BLL.CupDeclarantInfo.SelectOneByProjectID(ProjectId);
            return true;
        }
        private void Create(string path)
        {
            try
            {
                float left = (float)(3.18 / 2.54) * 72;
                Document document = new Document(PageSize.A4, left, left, 72f, 72f);
                file =  Project.Name + ".pdf";
                Url = Student.StudentID + "/" + Project.Name + ".pdf";
                if (!Directory.Exists(path + Student.StudentID + "/"))
                    Directory.CreateDirectory(path + Student.StudentID + "/");
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + Student.StudentID+"/" + Project.Name + ".pdf", FileMode.Create));
                document.Open();
                CreateCover(document);
                document.NewPage();
                CreateRule(document);
                document.NewPage();
                if (Project.DeclarationType == "个人项目")
                {
                    CreateTableA1(document);
                }
                else if (Project.DeclarationType == "集体项目")
                {
                    CreateTableA2(document);
                }
                document.NewPage();
                if (CupWorsInfo != null)
                {
                    CreateTableB1(document);
                    document.NewPage();
                    CreateTableC(document, CupWorsInfo.SameResearchLevel);
                }
                else if (CupWorksSurvey != null)
                {
                    CreateTableB2(document);
                    document.NewPage();
                    CreateTableC(document, CupWorksSurvey.SameResearchLevel);
                }
                else if (CupWorksInvention != null)
                {
                    CreateTableB3(document);
                    document.NewPage();
                    CreateTableC(document, CupWorksInvention.SameResearchLevel);
                }
                document.NewPage();
                CreateTableD(document);
                document.NewPage();
                CreateTableE(document);
                document.NewPage();
                CreateFG1G2(document);
                document.Close();

            }
            catch (DocumentException de)
            {

            }

        }
        ///<summary>
        /// 封面
        /// </summary>
        private void CreateCover(Document document)
        {
            Font font = new Font(KaiTiGB2312, 15, Font.NORMAL);
            Phrase phrase;//短语
            Paragraph paragraph;//段落
            string str;

            document.Add(NewLine(KaiTiGB2312, 28, 15));//换行
            #region 序号 编码
            str = "序号：";
            phrase = new Phrase(28, str, font);
            paragraph = new Paragraph(phrase);
            str = "          ";
            font = new Font(KaiTiGB2312, 15, Font.UNDERLINE);
            phrase = new Phrase(28, str, font);
            paragraph.Add(phrase);
            document.Add(paragraph);

            str = "编码：";
            font = new Font(KaiTiGB2312, 15, Font.NORMAL);
            phrase = new Phrase(28, str, font);
            paragraph = new Paragraph(phrase);
            str = "          ";
            font = new Font(KaiTiGB2312, 15, Font.UNDERLINE);
            phrase = new Phrase(28, str, font);
            paragraph.Add(phrase);
            document.Add(paragraph);
            #endregion

            #region 比赛名称
            font = new Font(HeiTi, 16, Font.BOLD);
            //  paragraph = new Paragraph("test", font);
            str=Match==null?"":Match.MatchName;
            paragraph = new Paragraph(str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            #endregion

            document.Add(NewLine(LiShu, 28, 14));

            font = new Font(LiShu, 26, Font.NORMAL);
            paragraph = new Paragraph("作品申报书", font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);

            document.Add(NewLine(SongTi, 28, 10.5f));
            #region 作品名称
            str = "作品名称：";
            font = new Font(KaiTiGB2312, 小三, Font.BOLD);
            phrase = new Phrase(28, str, font);
            paragraph = new Paragraph();
            paragraph.Add(phrase);
            str = Project==null?"":Project.Name;
            font = new Font(KaiTiGB2312, 四号, Font.UNDERLINE);
            phrase = new Phrase(28, DealWithString(20, str), font);
            paragraph.Add(phrase);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            #endregion

            document.Add(NewLine(SongTi, 28, 10.5f));
            #region 学校全称
            int length = str.Length > 20 ? str.Length : 20;
            str = "学校全称：";
            font = new Font(KaiTiGB2312, 15, Font.BOLD);
            phrase = new Phrase(28, str, font);
            paragraph = new Paragraph();
            paragraph.Add(phrase);
            font = new Font(KaiTiGB2312, 14, Font.UNDERLINE);
            str=Student==null?"":Student.School;
            phrase = new Phrase(DealWithString(length,str ), font);
            paragraph.Add(phrase);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            #endregion

            document.Add(NewLine(SongTi, 28, 10.5f));
            #region 申报者姓名
            str = "申报者姓名";
            font = new Font(KaiTiGB2312, 15, Font.BOLD);
            phrase = new Phrase(28, str, font);
            paragraph = new Paragraph();
            paragraph.Add(phrase);
            str = "";
            phrase = new Phrase(18, DealWithString(length - 1, str) + Environment.NewLine, font);
            paragraph.Add(phrase);
            str = "（集体名称）：";
            phrase = new Phrase(28, str, font);
            paragraph.Add(phrase);
            font = new Font(KaiTiGB2312, 14, Font.UNDERLINE);
            str = Student.Name;
            int count = 0;
            for (int i = 0; i < 2 && i < TeamMembers.Count; i++)
            {
                str += "  " + TeamMembers[i].Name;
                ++count;
            }
            phrase = new Phrase(28, DealWithString(length - 3 + count, str) + "   ", font);
            paragraph.Add(phrase);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            #endregion

            document.Add(NewLine(SongTi, 28, 10.5f));
            document.Add(NewLine(SongTi, 28, 10.5f));
            #region 类别
            str = "    类别：";
            font = new Font(LiShu, 15, Font.BOLD);
            paragraph = new Paragraph(28, str, font);
            document.Add(paragraph);

            str = "         □自然科学类学术论文";
            font = new Font(KaiTiGB2312, 14, Font.NORMAL);
            paragraph = new Paragraph(28, str, font);
            document.Add(paragraph);
            str = "         □哲学社会科学类社会调查报告和学术论文";
            font = new Font(KaiTiGB2312, 14, Font.NORMAL);
            paragraph = new Paragraph(28, str, font);
            document.Add(paragraph);
            str = "         □科技发明制作A类";
            font = new Font(KaiTiGB2312, 14, Font.NORMAL);
            paragraph = new Paragraph(28, str, font);
            document.Add(paragraph);
            str = "         □科技发明制作B类";
            font = new Font(KaiTiGB2312, 14, Font.NORMAL);
            paragraph = new Paragraph(28, str, font);
            document.Add(paragraph);

            document.Add(NewLine(SongTi, 28, 10.5f));
            #endregion

            #region 报送方式
            str = "    报送方式：";
            font = new Font(LiShu, 15, Font.BOLD);
            paragraph = new Paragraph(28, str, font);
            document.Add(paragraph);

            str = "         □省级报送作品";
            font = new Font(KaiTiGB2312, 14, Font.NORMAL);
            paragraph = new Paragraph(28, str, font);
            document.Add(paragraph);
            str = "         □高校直送作品";
            font = new Font(KaiTiGB2312, 14, Font.NORMAL);
            paragraph = new Paragraph(28, str, font);
            document.Add(paragraph);
            #endregion
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
        private void CreateRule(Document document)
        {

            Font font = new Font(SongTi, 20, Font.BOLD);
            Paragraph paragraph = new Paragraph("说明", font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(NewLine(SongTi, 28, 20));
            document.Add(paragraph);
            document.Add(NewLine(SongTi, 28, 20));
            HTMLWorker worder = new HTMLWorker(document);
            StringReader sr = new StringReader(Match.Rule);
            string str = Match.Rule;
            font = new Font(FangSongGB2312, 小三);
            worder.Parse(sr, font);
        }

        private void CreateTableA1(Document document)
        {
            Font fillFont = new Font(SongTi, 五号, Font.NORMAL);
            #region 标题
            string str = "A1．申报者情况（个人项目）";
            Font font = new Font(HeiTi, 小三, Font.BOLD);
            Paragraph paragraph = new Paragraph(28, str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            #endregion
            #region 说明
            str = "说明：1．必须由申报者本人按要求填写，申报者情况栏内必须填写个人作品的第一作者（承担申报作品60%以上的工作者）；2．本表中的学籍管理部门签章视为对申报者情况的确认。";
            font = new Font(FangSongGB2312, 12, Font.NORMAL);
            Phrase phrase = new Phrase(18, str, font);
            document.Add(phrase);
            #endregion
            #region A1表
            PdfPTable table = new PdfPTable(new float[] { 6.2f, 93.8f });
            table.TotalWidth = 431.7f;
            table.LockedWidth = true;
            List<float> widths = new List<float>();
            List<PdfPCell> PdfPCells = new List<PdfPCell>();
            PdfPCell cell;
            #region 申报者情况
            cell = new PdfPCell(new Phrase(" ", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
            cell.FixedHeight = 24.1f;
            table.AddCell(cell);
            widths.Add(21.43f); widths.Add(21.43f);
            widths.Add(10.77f); widths.Add(12.05f); widths.Add(19.29f); widths.Add(21.32f);
            cell = new PdfPCell(new Phrase("姓  名", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.FixedHeight = 24.1f;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            str=Student==null?"":Student.Name;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.FixedHeight = 24.1f;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("性别", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.FixedHeight = 24.1f;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            str = Student == null ? "" : Student.Sex;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("出生年月", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            str = Declarant == null ? "" : Declarant.Birthday.Year + "年" + Declarant.Birthday.Month + "月";
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            table.AddCell(new PdfPCell(CreateCell(404.9346f, widths, PdfPCells)));
            str = "申报者代表情况";
            font = new Font(FangSongGB2312, 14, Font.NORMAL);
            phrase = new Phrase(18, str, font);
            cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.Rowspan = 6;
            table.AddCell(cell);

            PdfPCells.RemoveAt(PdfPCells.Count - 1);
            PdfPCells.RemoveAt(PdfPCells.Count - 1);

            PdfPCells[0].Phrase = new Phrase("学校全称", font);
            str = Student == null ? "" : Student.School;
            PdfPCells[1].Phrase = new Phrase(str, fillFont);
            PdfPCells[1].Colspan = 2;
            PdfPCells[2] = new PdfPCell(new Phrase("专业", font));
            str = Student == null ? "" : Student.Major;
            PdfPCells[3].Phrase = new Phrase(str, fillFont);
            PdfPCells[3].Colspan = 2;
            table.AddCell(new PdfPCell(CreateCell(404.9346f, widths, PdfPCells)));

            widths.Clear();
            widths.Add(21.43f);
            widths.Add(11.62f); widths.Add(9.38f); widths.Add(10.98f); widths.Add(8.74f); widths.Add(10.98f); widths.Add(16.52f); widths.Add(16.52f);
            PdfPCells[0].Phrase = new Phrase("现学历", font);
            str = Declarant == null ? "" : Declarant.BackGround;
            PdfPCells[1].Phrase = new Phrase(str, fillFont);
            PdfPCells[1].Colspan = 1;
            PdfPCells[2].Phrase = new Phrase("年级", font);
            str = Student == null ? "" : Student.InTimeYear + "级";
            PdfPCells[3].Phrase = new Phrase(str, fillFont);
            PdfPCells[3].Colspan = 1;
            cell = new PdfPCell(new Phrase("学制", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            str = Declarant == null ? "" : Declarant.SchoolSystme;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("入学时间", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            str = Student == null ? "" : Student.InTimeYear + "";
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            table.AddCell(new PdfPCell(CreateCell(404.9346f, widths, PdfPCells)));

            widths.Clear();
            widths.Add(28.46f); widths.Add(78.25f);
            PdfPCells.Clear();
            cell = new PdfPCell(new Phrase("作品名称", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 24.1f;
            PdfPCells.Add(cell);
            str = Project == null ? "" : Project.Name;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.UseAscender = true;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 24.1f;
            PdfPCells.Add(cell);
            table.AddCell(new PdfPCell(CreateCell(404.9346f, widths, PdfPCells)));
            PdfPCells[0].Phrase = new Phrase("毕业论文题目", font);
            str = Declarant == null ? "" : Declarant.PaperTitle;
            PdfPCells[1].Phrase = new Phrase(str, fillFont);
            table.AddCell(new PdfPCell(CreateCell(404.9346f, widths, PdfPCells)));

            widths.Clear();
            widths.Add(45.74f); widths.Add(55.25f);
            PdfPCells.Clear();
            cell = new PdfPCell(new Phrase("邮政编码", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 24.1f;
            PdfPCells.Add(cell);
            str = Declarant == null ? "" : Declarant.PostalCode;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 24.1f;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("办公电话", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 24.1f;
            PdfPCells.Add(cell);
            str = Declarant == null ? "" : Declarant.Phone;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.UseAscender = true;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 24.1f;
            PdfPCells.Add(cell);
            PdfPCell celln = new PdfPCell(CreateCell(152.2554f, widths, PdfPCells));
            str = Declarant == null ? "" : Declarant.TopPostalCode;
            PdfPCells[1].Phrase = new Phrase(str, fillFont);
            PdfPCells[2].Phrase = new Phrase("住宅电话", font);
            str = Declarant == null ? "" : Declarant.TopPhone;
            PdfPCells[3].Phrase = new Phrase(str, fillFont);
            PdfPCell cellm = new PdfPCell(CreateCell(152.2554f, widths, PdfPCells));

            widths.Clear();
            widths.Add(28.46f); widths.Add(37.95f); widths.Add(40.09f);
            PdfPCells.Clear();
            cell = new PdfPCell(new Phrase("通讯地址", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            str = Declarant == null ? "" : Declarant.Address;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            PdfPCells.Add(celln);
            table.AddCell(new PdfPCell(CreateCell(404.9346f, widths, PdfPCells)));
            PdfPCells[0].Phrase = new Phrase("常住地\n通讯地址", font);
            str = Declarant == null ? "" : Declarant.TopAddress;
            PdfPCells[1].Phrase = new Phrase(str, fillFont);
            PdfPCells[2] = cellm;
            table.AddCell(new PdfPCell(CreateCell(404.9346f, widths, PdfPCells)));
            #endregion

            #region 其他作者
            str = "合作者情况";
            font = new Font(FangSongGB2312, 14, Font.NORMAL);
            phrase = new Phrase(18, str, font);
            cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.Rowspan = TeamMembers.Count > 3 ? TeamMembers.Count + 1 : 3;
            table.AddCell(cell);

            widths.Clear();
            widths.Add(28.4f); widths.Add(9.38f); widths.Add(9.38f); widths.Add(13.97f); widths.Add(44.99f);
            PdfPCells.Clear();
            cell = new PdfPCell(new Phrase("姓  名", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.UseAscender = true;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 24.1f;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("性别", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.UseAscender = true;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("年龄", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.UseAscender = true;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("学历", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.UseAscender = true;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("所在单位", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.UseAscender = true;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            for (int i = 0; i < TeamMembers.Count; i++)
            {
                PdfPCells[0].Phrase = new Phrase(TeamMembers[i].Name, fillFont);
                PdfPCells[1].Phrase = new Phrase(TeamMembers[i].Sex, fillFont);
                PdfPCells[2].Phrase = new Phrase(TeamMembers[i].Age + "", fillFont);
                PdfPCells[3].Phrase = new Phrase(TeamMembers[i].BackGround, fillFont);
                PdfPCells[4].Phrase = new Phrase(TeamMembers[i].WorkUnit, fillFont);
                table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            }
            for (int i = 0; i < 2 - TeamMembers.Count; ++i)
            {
                PdfPCells[0].Phrase = new Phrase(" ", font);
                PdfPCells[1].Phrase = new Phrase(" ", font);
                PdfPCells[2].Phrase = new Phrase(" ", font);
                PdfPCells[3].Phrase = new Phrase(" ", font);
                PdfPCells[4].Phrase = new Phrase(" ", font);
                table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            }

            #endregion

            #region 资格认定
            str = "资格认定";
            font = new Font(FangSongGB2312, 14, Font.NORMAL);
            phrase = new Phrase(18, str, font);
            cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.Rowspan = 2;
            table.AddCell(cell);

            widths.Clear();
            widths.Add(28.46f); widths.Add(78.25f);
            PdfPCells.Clear();
            cell = new PdfPCell(new Phrase("学校学籍管理部门意见", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 108;
            cell.SetLeading(0, 1.5f);
            PdfPCells.Add(cell);
            str = "是否为" + Project.DeclarationDate.Year + "年" + Project.DeclarationDate.Month + "月" + Project.DeclarationDate.Day + "日"
                + "前正式注册在校的全日制非成人教育、非在职的各类高等院校中国学生（含专科生、本科生和研究生）。" + Environment.NewLine;
            font = new Font(FangSongGB2312, 小四, Font.NORMAL);
            Chunk chunk = new Chunk(str, font);
            phrase = new Phrase();
            phrase.Add(chunk);
            font = new Font(FangSongGB2312, 小三, Font.NORMAL);
            str = "    □是□否" + Environment.NewLine + "    若是，其学号为：（部门盖章）" + Environment.NewLine + "                     年  月  日";
            phrase.Add(new Chunk(str, font));
            cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0, 1.5f);
            PdfPCells.Add(cell);
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            PdfPCells[0].Phrase = new Phrase("院、系负责人或导师意见", font);
            PdfPCells[0].MinimumHeight = 77.39f;
            str = "本作品是否为课外学术科技或社会实践活动成果" + Environment.NewLine;
            font = new Font(FangSongGB2312, 小四, Font.NORMAL);
            chunk = new Chunk(str, font);
            phrase = new Phrase();
            phrase.Add(chunk);
            font = new Font(FangSongGB2312, 小三, Font.NORMAL);
            phrase.Add(new Chunk("□是□否             负责人签名：" + Environment.NewLine + "                     年  月  日", font));
            PdfPCells[1].Phrase = phrase;
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            #endregion
            document.Add(table);
            #endregion

        }
        private void CreateTableA2(Document document)
        {
            Font fillFont = new Font(SongTi, 五号, Font.NORMAL);
            #region 标题
            string str = "A2申报者情况（集体项目）";
            Font font = new Font(HeiTi, 15, Font.BOLD);
            Paragraph paragraph = new Paragraph(28, str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            #endregion
            #region 说明
            str = "说明：1．必须由申报者本人按要求填写；2．申报者代表必须是作者中学历最高者，其余作者按学历高低排列；3．本表中的学籍管理部门签章视为申报者情况的确认。";
            font = new Font(FangSongGB2312, 12, Font.NORMAL);
            Phrase phrase = new Phrase(18, str, font);
            document.Add(phrase);
            document.Add(NewLine(FangSongGB2312, 18, 12));
            #endregion
            #region A2表
            PdfPTable table = new PdfPTable(new float[] { 26.9f, 414.2f });
            table.TotalWidth = 441.1f;
            table.LockedWidth = true;
            List<float> widths = new List<float>();
            List<PdfPCell> PdfPCells = new List<PdfPCell>();
            #region 申报者情况
            str = "申报者代表情况";
            font = new Font(FangSongGB2312, 14, Font.NORMAL);
            phrase = new Phrase(18, str, font);
            PdfPCell cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.Rowspan = 7;
            table.AddCell(cell);
            widths.Add(15.2f); widths.Add(24f); widths.Add(15.2f); widths.Add(13f); widths.Add(15.2f); widths.Add(17.4f);
            cell = new PdfPCell(new Phrase("姓名", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 22.1f;
            PdfPCells.Add(cell);
            str = Student == null ? "" : Student.Name;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("性别", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            str = Student == null ? "" : Student.Sex;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("出生年月", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            str = Declarant == null ? "" : Declarant.Birthday.Year + "年" + Declarant.Birthday.Month + "月";
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            PdfPCells.RemoveAt(PdfPCells.Count - 1);
            PdfPCells.RemoveAt(PdfPCells.Count - 1);
            PdfPCells[0].Phrase = new Phrase("学校", font);
            str = Student == null ? "" : Student.School;
            PdfPCells[1].Phrase = new Phrase(str, fillFont);


            Chunk chunk = new Chunk("系别、专业、年级", font);
            chunk.SetHorizontalScaling(0.5f);
            PdfPCells[2].Phrase = new Phrase(chunk);
            PdfPCells[2].FixedHeight = 22.1f;
            PdfPCells[2].UseBorderPadding = true;
            str = Student == null ? "" : Student.College + Student.Major;
            PdfPCells[3].Phrase = new Phrase(str, fillFont);
            PdfPCells[3].Colspan = 3;
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));

            PdfPCells[0].Phrase = new Phrase("学历", font);
            str = Declarant == null ? "" : Declarant.BackGround;
            PdfPCells[1].Phrase = new Phrase(str, fillFont);
            PdfPCells[2].Phrase = new Phrase("学制", font);
            str = Declarant == null ? "" : Declarant.SchoolSystme;
            PdfPCells[3].Phrase = new Phrase(str, font);
            PdfPCells[3].Colspan = 1;
            cell = new PdfPCell(new Phrase("入学时间", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            str = Student == null ? "" : Student.InTimeYear + "年";
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            widths.Clear();
            widths.Add(31.87f); widths.Add(68.45f);
            PdfPCells.Clear();
            cell = new PdfPCell(new Phrase("作品名称", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 22.1f;
            PdfPCells.Add(cell);
            str = Project == null ? "" : Project.Name;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            PdfPCells[0].Phrase = new Phrase("毕业论文题目", font);
            str = Declarant == null ? "" : Declarant.PaperTitle;
            PdfPCells[1].Phrase = new Phrase(str, fillFont);
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));

            widths.Clear();
            widths.Add(46.56f); widths.Add(53.44f);
            PdfPCells.Clear();
            cell = new PdfPCell(new Phrase("邮政编码", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 22.1f;
            PdfPCells.Add(cell);
            str = Declarant == null ? "" : Declarant.PostalCode;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("办公电话", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 22.1f;
            PdfPCells.Add(cell);
            str = Declarant == null ? "" : Declarant.Phone;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            PdfPCell celln = new PdfPCell(CreateCell(134.5355f, widths, PdfPCells));
            str = Declarant == null ? "" : Declarant.TopPostalCode;
            PdfPCells[1].Phrase = new Phrase(str, fillFont);
            PdfPCells[2].Phrase = new Phrase("住宅电话", font);
            str = Declarant == null ? "" : Declarant.TopPhone;
            PdfPCells[3].Phrase = new Phrase(str, fillFont);
            PdfPCell cellm = new PdfPCell(CreateCell(134.5355f, widths, PdfPCells));

            widths.Clear();
            widths.Add(23.63f); widths.Add(43.85f); widths.Add(32.62f);
            PdfPCells.Clear();
            cell = new PdfPCell(new Phrase("通讯地址", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            str = Declarant == null ? "" : Declarant.Address;
            cell = new PdfPCell(new Phrase(str, fillFont));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            PdfPCells.Add(celln);
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            PdfPCells[0].Phrase = new Phrase("常住地\n通讯地址", font);
            str = Declarant == null ? "" : Declarant.TopAddress;
            PdfPCells[1].Phrase = new Phrase(str, fillFont);
            PdfPCells[2] = cellm;
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            #endregion

            #region 其他作者
            str = "其他作者情况";
            font = new Font(FangSongGB2312, 14, Font.NORMAL);
            phrase = new Phrase(18, str, font);
            cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.Rowspan = 8;
            table.AddCell(cell);

            widths.Clear();
            widths.Add(23.65f); widths.Add(12.62f); widths.Add(13.69f); widths.Add(13.69f); widths.Add(36.67f);
            PdfPCells.Clear();
            cell = new PdfPCell(new Phrase("姓  名", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.FixedHeight = 22.1f;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("性别", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("年龄", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("学历", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            cell = new PdfPCell(new Phrase("所在单位", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            PdfPCells.Add(cell);
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            for (int i = 0; i < TeamMembers.Count; i++)
            {
                PdfPCells[0].Phrase = new Phrase(TeamMembers[i].Name, fillFont);
                PdfPCells[1].Phrase = new Phrase(TeamMembers[i].Sex, fillFont);
                PdfPCells[2].Phrase = new Phrase(TeamMembers[i].Age + "", fillFont);
                PdfPCells[3].Phrase = new Phrase(TeamMembers[i].BackGround, fillFont);
                PdfPCells[4].Phrase = new Phrase(TeamMembers[i].WorkUnit, fillFont);
                table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            }
            for (int i = 0; i <= 6 - TeamMembers.Count; ++i)
            {
                PdfPCells[0].Phrase = new Phrase(" ", font);
                PdfPCells[1].Phrase = new Phrase(" ", font);
                PdfPCells[2].Phrase = new Phrase(" ", font);
                PdfPCells[3].Phrase = new Phrase(" ", font);
                PdfPCells[4].Phrase = new Phrase(" ", font);
                table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            }
            #endregion

            #region 资格认定
            str = "资格认定";
            font = new Font(FangSongGB2312, 14, Font.NORMAL);
            phrase = new Phrase(18, str, font);
            cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.Rowspan = 2;
            table.AddCell(cell);

            widths.Clear();
            widths.Add(23.65f); widths.Add(76.65f);
            PdfPCells.Clear();
            cell = new PdfPCell(new Phrase(28, "学校学籍管理部门意见", font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 80f;
            cell.SetLeading(0, 1.5f);
            PdfPCells.Add(cell);

            str = "以上作者是否为" + Project.DeclarationDate.Year + "年" + Project.DeclarationDate.Month + "月" + Project.DeclarationDate.Day + "日"
                + "前正式注册在校的全日制非成人教育、非在职的高等学校中国籍专科生、本科生、硕士研究生或博士研究生。" + Environment.NewLine;
            font = new Font(FangSongGB2312, 小四, Font.NORMAL);
            chunk = new Chunk(str, font);
            phrase = new Phrase();
            phrase.Add(chunk);
            font = new Font(KaiTiGB2312, 四号, Font.NORMAL);
            phrase.Add(new Chunk("□是□否", font));
            font = new Font(FangSongGB2312, 四号, Font.NORMAL);
            phrase.Add(new Chunk("（部门签章）" + Environment.NewLine + "                     年  月  日", font));
            cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.MinimumHeight = 100f;
            cell.SetLeading(0, 1.5f);
            PdfPCells.Add(cell);
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            PdfPCells[0].Phrase = new Phrase("院、系负责人或导师意见", font);
            PdfPCells[0].MinimumHeight = 45.92f;
            str = "本作品是否为课外学术科技或社会实践活动成果" + Environment.NewLine;
            font = new Font(FangSongGB2312, 小四, Font.NORMAL);
            chunk = new Chunk(str, font);
            phrase = new Phrase();
            phrase.Add(chunk);
            font = new Font(KaiTiGB2312, 四号, Font.NORMAL);
            phrase.Add(new Chunk("□是□否" + Environment.NewLine, font));
            font = new Font(FangSongGB2312, 四号, Font.NORMAL);
            phrase.Add(new Chunk("             负责人签名：" + Environment.NewLine + "                     年  月  日", font));
            PdfPCells[1].Phrase = phrase;
            table.AddCell(new PdfPCell(CreateCell(414.2f, widths, PdfPCells)));
            #endregion
            document.Add(table);
            #endregion

        }
        private void CreateTableB1(Document document)
        {
            Font fillFont = new Font(FangSongGB2312, 小四, Font.NORMAL);
            #region 标题
            string str = "B1．申报作品情况（自然科学类学术论文）";
            Font font = new Font(HeiTi, 15, Font.BOLD);
            Paragraph paragraph = new Paragraph(28, str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            #endregion

            #region 说明
            str = "说明：\n  1．必须由申报者本人填写；2．本部分中的科研管理部门签章视为对申报者所填内容的确认；3．作品分类请按作品的学术方向或所涉及的主要学科领域填写；4．硕士研究生、博士研究生作品不在此列。";
            font = new Font(FangSongGB2312, 12, Font.NORMAL);
            Phrase phrase = new Phrase(18, str, font);
            document.Add(phrase);
            #endregion

            #region B1表
            PdfPTable table = new PdfPTable(new float[] { 22.4f, 77.5f });
            table.TotalWidth = 441.1f;
            table.LockedWidth = true;

            #region 作品全称
            font = new Font(FangSongGB2312, 小三, Font.NORMAL);
            phrase = new Phrase(18, "作品全称", font);
            PdfPCell cell = new PdfPCell(phrase);
            cell.FixedHeight = 20.69f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.UseAscender = true;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            phrase = new Phrase(18, Project.Name, fillFont);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 20.69f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.UseAscender = true;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);
            #endregion

            #region  作品分类
            phrase = new Phrase(18, "作\n品\n分\n类", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 119.91f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            font = new Font(FangSongGB2312, 小四, Font.NORMAL);
            str = " ）A．机械与控制（包括机械、仪器仪表、自动化控" + Environment.NewLine +
                "     制、工程、交通、建筑等）" + Environment.NewLine +
                "     B．信息技术（包括计算机、电信、通讯、电子等）" + Environment.NewLine +
                "     C．数理（包括数学、物理、地球与空间科学等）" + Environment.NewLine +
                "     D．生命科学（包括生物、农学、药学、医学、健" + Environment.NewLine +
                "     康、卫生、食品等）" + Environment.NewLine +
                "     E．能源化工（包括能源、材料、石油、化学、化" + Environment.NewLine +
                "     工、生态、环保等）";
            if (CupWorsInfo.SecondCategories == "机械与控制")
            {
                str = "A" + str;
            }
            else if (CupWorsInfo.SecondCategories == "信息技术")
            {
                str = "B" + str;
            }
            else if (CupWorsInfo.SecondCategories == "数理")
            {
                str = "C" + str;
            }
            else if (CupWorsInfo.SecondCategories == "生命科学")
            {
                str = "D" + str;
            }
            else if (CupWorsInfo.SecondCategories == "能源化工")
            {
                str = "E" + str;
            }
            phrase = new Phrase(18, "( " +  str, fillFont);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 119.91f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品撰写的目的和基本思路
            font = new Font(FangSongGB2312, 小三, Font.NORMAL);
            phrase = new Phrase(18, "作品撰写的目的和基本思路", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 85.61f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorsInfo.Purpose, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 85.61f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1.5f, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品的科学性、先进性及独特之处
            phrase = new Phrase(18, "作品的科学性、先进性及独特之处", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 100.92f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorsInfo.Features, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 100.92f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0f, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品的实际应用价值和现实意义
            phrase = new Phrase(18, "作品的实际应用价值和现实意义", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 119.91f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1.5f, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorsInfo.ApplyValue, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 119.91f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1.5f, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  学术论文摘要
            phrase = new Phrase(18, "学\n\n术\n\n论\n\n文\n\n摘\n\n要", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 453.5f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorsInfo.PaperDigest, fillFont);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 453.5f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1.5f, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品在何时、何地、何种机构举行的会议上或报刊上发表及所获奖励
            font = new Font(FangSongGB2312, 小四, Font.NORMAL);
            phrase = new Phrase(18, "作品在何时、何地、何种机构举行的会议上或报刊上发表及所获奖励", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 110.275f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1.5f, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorsInfo.ReceivedAwards, fillFont);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 110.275f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1.5f, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  鉴定结果
            font = new Font(FangSongGB2312, 小三, Font.NORMAL);
            phrase = new Phrase(18, "鉴定结果", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 111.125f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorsInfo.TestResult, fillFont);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 111.125f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1.5f, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  技术文献的检索目录
            phrase = new Phrase(18, "请提供对于理解、审查、评价所申报作品具有参考价值的现有技术及技术文献的检索目录", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 368.5f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0f, 1.3f);
            table.AddCell(cell);

            fillFont = new Font(SongTi, 五号, Font.NORMAL);
            phrase = new Phrase(18, CupWorsInfo.References, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 368.5f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0f, 1.3f);
            table.AddCell(cell);
            #endregion

            #region  申报材料清单
            phrase = new Phrase(18, "申报材料清单（申报论文一篇，相关资料名称及数量）", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 116.79f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1.5f, 1.5f);
            table.AddCell(cell);

            fillFont = new Font(FangSongGB2312, 小四, Font.NORMAL);
            phrase = new Phrase(18, CupWorsInfo.MaterialsList, fillFont);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 116.79f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1.5f, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  科研管理部门签章
            phrase = new Phrase(18, "科研管理部门签章", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 192.77f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.Rowspan = 2;
            table.AddCell(cell);
            phrase = new Phrase(18, " ", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 172.08f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.BorderWidthBottom = 0;
            table.AddCell(cell);
            phrase = new Phrase(18, "年   月   日", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 20.69f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.BorderWidthTop = 0;
            table.AddCell(cell);
            #endregion

            document.Add(table);
            #endregion
        }
        private void CreateTableB2(Document document)
        {
            Font fillFont = new Font(FangSongGB2312, 小四, Font.NORMAL);
            #region 标题
            string str = "B2．申报作品情况" + Environment.NewLine + "（哲学社会科学类社会调查报告和学术论文）";
            Font font = new Font(HeiTi, 15, Font.BOLD);
            Paragraph paragraph = new Paragraph(28, str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            #endregion

            #region 说明
            str = "说明：1．必须由申报者本人填写；" + Environment.NewLine + "      2．本部分中的管理部门签章视为对申报者所填内容的确认。";
            font = new Font(FangSongGB2312, 12, Font.NORMAL);
            Phrase phrase = new Phrase(18, str, font);
            document.Add(phrase);
            #endregion

            #region B2表
            PdfPTable table = new PdfPTable(new float[] { 21.1f, 78.9f });
            table.TotalWidth = 441.1f;
            table.LockedWidth = true;

            #region 作品全称
            font = new Font(FangSongGB2312, 小三, Font.NORMAL);
            phrase = new Phrase(18, "作品全称", font);
            PdfPCell cell = new PdfPCell(phrase);
            cell.FixedHeight = 34.58f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            phrase = new Phrase(18, Project.Name, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 34.58f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);
            #endregion

            #region  作品所属领域
            phrase = new Phrase(18, "作品所属" + Environment.NewLine + "领  域", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 69.45f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, "（ " + CupWorksSurvey.Categories + " ）A哲学 B经济 C社会 D法律 E教育 F管理", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 119.91f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);
            #endregion

            #region  作品撰写的目的和基本思路
            phrase = new Phrase(18, "作品撰写的目的和基本思路", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 121.61f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorksSurvey.Purpose, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 121.61f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品的科学性、先进性及独特之处
            phrase = new Phrase(18, "作品的科学性、先进性及独特之处", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 109.42f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorksSurvey.Features, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 100.92f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品的实际应用价值和现实意义
            phrase = new Phrase(18, "作品的实际应用价值和现实意义", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 123.60f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorksSurvey.ApplyValue, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 123.60f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品摘要
            phrase = new Phrase(18, "作" + Environment.NewLine + Environment.NewLine + "品" + Environment.NewLine
                + Environment.NewLine + "摘" + Environment.NewLine + Environment.NewLine + "要", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 388.94f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorksSurvey.PaperDigest, font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 388.94f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品在何时、何地、何种机构举行的会议或报刊上发表登载、所获奖励及评定结果
            phrase = new Phrase(18, "作品在何时、何地、何种机构举行的会议或报刊上发表登载、所获奖励及评定结果", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 217.15f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorksSurvey.ReceivedAwards, font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 217.15f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品中资料来源的检索目录
            phrase = new Phrase(18, "请提供对于理解、审查、评价所申报作品，具有参考价值的现有对比数据及作品中资料来源的检索目录", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 326.57f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorksSurvey.References, font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 326.57f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  调查方式
            phrase = new Phrase(18, "调查方式", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 108.57f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            phrase = new Phrase(28, "□走访  □问卷  □现场采访  □人员介绍□个别交谈  □亲临实践  □会议"
                + Environment.NewLine + "□图片、照片   □书报刊物  □统计报表 "
                + Environment.NewLine + "□影视资料  □文件  □集体组织  □自发□其它", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 108.57f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  主要调查单位及调查数量
            phrase = new Phrase(18, "主要调查单位及调查数量", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 70.02f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);
            phrase = new Phrase(18, "省（市）县（区）乡（镇）村（街）单位邮编姓名电话调查单位个人次", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 70.02f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            #endregion

            #region  科研管理部门签章
            phrase = new Phrase(18, "科研管理部门签章", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 92.98f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.Rowspan = 2;
            table.AddCell(cell);
            phrase = new Phrase(18, " ", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 72.29f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            table.AddCell(cell);
            phrase = new Phrase(18, "年   月   日", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 20.69f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            table.AddCell(cell);
            #endregion

            document.Add(table);
            #endregion
        }
        private void CreateTableB3(Document document)
        {
            Font fillFont = new Font(SongTi, 五号, Font.NORMAL);
            #region 标题
            string str = "B3．申报作品情况（科技发明制作）";
            Font font = new Font(HeiTi, 15, Font.BOLD);
            Paragraph paragraph = new Paragraph(28, str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            #endregion

            #region 说明
            str = "说明：1．必须由申报者本人填写；2．本部分中的科研管理部门签章视为对申报者所填内容的确认；" +
                "3．本表必须附有研究报告，并提供图表、曲线、试验数据、原理结构图、外观图（照片）,也可附" +
                "鉴定证书和应用证书；4．作品分类请按照作品发明点或创新点所在类别填报。";
            font = new Font(FangSongGB2312, 小四, Font.NORMAL);
            Phrase phrase = new Phrase(18, str, font);
            document.Add(phrase);
            #endregion

            #region B3表
            PdfPTable table = new PdfPTable(new float[] { 3.88f, 11.04f });
            table.TotalWidth = 422.96f;
            table.LockedWidth = true;

            #region 作品全称
            font = new Font(FangSongGB2312, 小三, Font.NORMAL);
            phrase = new Phrase(18, "作品全称", font);
            PdfPCell cell = new PdfPCell(phrase);
            cell.FixedHeight = 32.884f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            phrase = new Phrase(18, Project.Name, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 32.884f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品分类
            phrase = new Phrase(18, "作品分类", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 138.62f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            font = new Font(FangSongGB2312, 小四, Font.NORMAL);
            str = " ）A．机械与控制（包括机械、仪器仪表、自动化控" + Environment.NewLine +
                "     制、工程、交通、建筑等）" + Environment.NewLine +
                "     B．信息技术（包括计算机、电信、通讯、电子等）" + Environment.NewLine +
                "     C．数理（包括数学、物理、地球与空间科学等）" + Environment.NewLine +
                "     D．生命科学（包括生物、农学、药学、医学、健" + Environment.NewLine +
                "     康、卫生、食品等）" + Environment.NewLine +
                "     E．能源化工（包括能源、材料、石油、化学、化" + Environment.NewLine +
                "     工、生态、环保等）";
            if (CupWorksInvention.Categories == "机械与控制")
            {
                str = "A" + str;
            }
            else if (CupWorksInvention.Categories == "信息技术")
            {
                str = "B" + str;
            }
            else if (CupWorksInvention.Categories == "数理")
            {
                str = "C" + str;
            }
            else if (CupWorksInvention.Categories == "生命科学")
            {
                str = "D" + str;
            }
            else if (CupWorksInvention.Categories == "能源化工")
            {
                str = "E" + str;
            }
            phrase = new Phrase(18, "( " + str, fillFont);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 138.62f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品设计、发明的目的和基本思路，创新点，技术关键和主要技术指标
            phrase = new Phrase(18, "作品设计、发明的目的和基本思路，创新点，技术关键和主要技术指标", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 139.19f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorksInvention.Purpose, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 139.19f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品的科学性先进性
            phrase = new Phrase(18, "作品的科学性先进性（必须说明与现有技术相比、该作品是否具有突出的实质性技术特点和显著进步。请提供技术性分析说明和参考文献资料）", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 170.66f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorksInvention.Features, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 170.62f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品在何时、何地、何种机构举行的评审、鉴定、评比、展示等活动中获奖及鉴定结果
            phrase = new Phrase(18, "作品在何时、何地、何种机构举行的评审、鉴定、评比、展示等活动中获奖及鉴定结果", font);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 396.31f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorksInvention.ReceivedAwards, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 396.31f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品所处阶段
            font = new Font(FangSongGB2312, 小三, Font.NORMAL);
            phrase = new Phrase(18, "作品所处" + Environment.NewLine + "阶  段", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 51.03f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.UseAscender = true;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            phrase = new Phrase(18, "（" + CupWorksInvention.WorkStatu + "）A实验室阶段  B中试阶段 C生产阶段" + Environment.NewLine + "     D（自填）", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 51.03f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  技术转让方式
            phrase = new Phrase(18, "技术转让方式", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 60.66f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorksInvention.AssignmentWay, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 60.66f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  作品可展示的形式
            phrase = new Phrase(28, "作品可展示的" + Environment.NewLine + "形  式", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 80.51f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            phrase = new Phrase(28, "□实物、产品  □模型  □图纸  □磁盘  □现场演示  □图片  □录像  □样品", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 80.51f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(2, 2);
            table.AddCell(cell);
            #endregion

            #region  市场分析和经济效益预测
            font = new Font(FangSongGB2312, 小四, Font.NORMAL);
            phrase = new Phrase(18, "使用说明及该作品的技术特点和优势，提供该作品的适应范围及推广前景的技术性说明及市场分析和经济效益预测", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 225.37f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);

            phrase = new Phrase(18, CupWorksInvention.ApplyValue, fillFont);
            cell = new PdfPCell(phrase);
            cell.MinimumHeight = 225.37f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            #endregion

            #region  专利申报情况
            phrase = new Phrase(18, "专利申报情况", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 172.22f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);

            font = new Font(FangSongGB2312, 小三, Font.NORMAL);
            phrase = new Phrase(38, "□提出专利申报" +
                 Environment.NewLine + "               申报号" +
                 Environment.NewLine + "             申报日期     年   月   日" +
                 Environment.NewLine + "□已获专利权批准" +
                 Environment.NewLine + "               批准号" +
                 Environment.NewLine + "             批准日期     年   月   日" +
                 Environment.NewLine + "□未提出专利申请"
            , font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 172.22f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.SetLeading(0, 1.5f);
            table.AddCell(cell);

            #endregion

            #region  科研管理部门签章
            phrase = new Phrase(18, "科研管理部门" + Environment.NewLine + "签  章", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 160.168f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.UseAscender = true;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.Rowspan = 2;
            table.AddCell(cell);
            phrase = new Phrase(18, " ", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 139.478f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.BorderWidth = 0;
            cell.BorderWidthRight = 1;
            table.AddCell(cell);
            phrase = new Phrase(18, "年   月   日", font);
            cell = new PdfPCell(phrase);
            cell.FixedHeight = 20.69f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            table.AddCell(cell);
            #endregion

            document.Add(table);

            #endregion
        }
        private void CreateTableC(Document document, string content)
        {
            Font fillFont = new Font(FangSongGB2312, 10, Font.NORMAL);
            #region 标题
            string str = "C.当前国内外同类课题研究水平概述";
            Font font = new Font(HeiTi, 15, Font.BOLD);
            Paragraph paragraph = new Paragraph(28, str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            #endregion

            document.Add(NewLine(SongTi, 28, 10));

            #region 说明
            str = "说明：1.申报者可根据作品类别和情况填写；" + Environment.NewLine + "      2.填写此栏有助于评审。";
            font = new Font(FangSongGB2312, 12, Font.NORMAL);
            Phrase phrase = new Phrase(18, str, font);
            document.Add(phrase);
            #endregion

            document.Add(NewLine(SongTi, 28, 10));

            #region 表C
            PdfPTable table = new PdfPTable(1);
            table.TotalWidth = 441.1f;
            table.LockedWidth = true;
            PdfPCell cell = new PdfPCell(new Phrase(18, content, fillFont));
            cell.MinimumHeight = 468.88f;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            document.Add(table);
            #endregion
        }
        private void CreateTableD(Document document)
        {
            Font fillFont = new Font(SongTi, 五号, Font.NORMAL);
            #region 标题
            string str = "D.推荐者情况及对作品的说明";
            Font font = new Font(HeiTi, 小二, Font.BOLD);
            Paragraph paragraph = new Paragraph(28, str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            #endregion

            document.Add(NewLine(SongTi, 28, 10));

            #region 说明
            str = "说明：1．由推荐者本人填写；2．推荐者必须具有高级专业技术职称，并是与申报作品相同或相关" +
                "领域的专家学者或专业技术人员（教研组集体推荐亦可）；3．推荐者填写此部分，即视为同意推荐" +
                "；4．推荐者所在单位签章仅被视为对推荐者身份的确认。";
            font = new Font(FangSongGB2312, 小四, Font.NORMAL);
            Phrase phrase = new Phrase(18, str, font);
            document.Add(phrase);
            #endregion

            document.Add(NewLine(SongTi, 28, 10));

            #region 表D
            if (Recommendes.Count == 0)
            {
                Recommendes.Add(new Models.DB.RecommendedInfo());
            }
            for (int i = 0; i < Recommendes.Count; i++)
            {
                PdfPTable table = new PdfPTable(new float[] { 4.42f, 10.8f });
                table.TotalWidth = 431.46f;
                table.LockedWidth = true;
                List<float> widths = new List<float>();
                List<PdfPCell> PdfPCells = new List<PdfPCell>();
                #region 推荐者情况
                widths.Add(2.01f); widths.Add(2.41f);
                font = new Font(FangSongGB2312, 小三, Font.NORMAL);
                PdfPCell cell = new PdfPCell(new Phrase("推荐者情况", font));
                cell.Rowspan = 4;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);

                cell = new PdfPCell(new Phrase("姓  名", font));
                cell.FixedHeight = 24.38f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                cell = new PdfPCell(new Phrase("工作单位", font));
                cell.FixedHeight = 24.38f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                cell = new PdfPCell(new Phrase("通讯地址", font));
                cell.FixedHeight = 24.38f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                cell = new PdfPCell(new Phrase("单位电话", font));
                cell.FixedHeight = 24.38f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                cell = new PdfPCell(CreateCell(125.30f, widths, PdfPCells));
                cell.Rowspan = 4;
                table.AddCell(cell);

                widths.Clear();
                widths.Add(2.05f); widths.Add(1.31f); widths.Add(1.3f); widths.Add(1.32f);
                widths.Add(1.03f); widths.Add(1.59f); widths.Add(2.22f);
                PdfPCells.Clear();
                cell = new PdfPCell(new Phrase(Recommendes[i].Name, fillFont));
                cell.FixedHeight = 24.38f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.UseAscender = true;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                cell = new PdfPCell(new Phrase("性别", font));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.UseAscender = true;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                cell = new PdfPCell(new Phrase(Recommendes[i].Sex, fillFont));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.UseAscender = true;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                cell = new PdfPCell(new Phrase("年龄", font));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.UseAscender = true;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                cell = new PdfPCell(new Phrase(Recommendes[i].Age + "", fillFont));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.UseAscender = true;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                cell = new PdfPCell(new Phrase("职称", font));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.UseAscender = true;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                cell = new PdfPCell(new Phrase(Recommendes[i].Title, fillFont));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.UseAscender = true;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                table.AddCell(new PdfPCell(CreateCell(135.08f, widths, PdfPCells)));

                cell = new PdfPCell(new Phrase(Recommendes[i].WorkUnits, fillFont));
                cell.FixedHeight = 24.38f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.UseAscender = true;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                table.AddCell(cell);

                widths.Clear(); widths.Add(5.78f); widths.Add(2.5f); widths.Add(2.53f);
                PdfPCells.Clear();
                cell = new PdfPCell(new Phrase(Recommendes[i].Address, fillFont));
                cell.FixedHeight = 24.38f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.UseAscender = true;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                cell = new PdfPCell(new Phrase("邮政编码", font));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.UseAscender = true;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                cell = new PdfPCell(new Phrase(Recommendes[i].PostalCode, fillFont));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.UseAscender = true;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                PdfPCells.Add(cell);
                table.AddCell(new PdfPCell(CreateCell(135.08f, widths, PdfPCells)));
                PdfPCells[0].Phrase = new Phrase(Recommendes[i].Phone, fillFont);
                PdfPCells[1].Phrase = new Phrase("住宅电话", font);
                PdfPCells[2].Phrase = new Phrase(Recommendes[i].HomePhone, fillFont);
                table.AddCell(new PdfPCell(CreateCell(135.08f, widths, PdfPCells)));
                #endregion

                #region 推荐者所在单位签章
                font = new Font(FangSongGB2312, 小四, Font.NORMAL);
                phrase = new Phrase(18, "推荐者所在" + Environment.NewLine + "单位签章", font);
                cell = new PdfPCell(phrase);
                cell.FixedHeight = 56.7f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.SetLeading(1, 1.5f);
                table.AddCell(cell);

                font = new Font(FangSongGB2312, 小三, Font.NORMAL);
                phrase = new Phrase(18, "（签章）     年   月   日", font);
                cell = new PdfPCell(phrase);
                cell.FixedHeight = 56.7f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.UseAscender = true;
                cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                table.AddCell(cell);
                #endregion

                #region 请对申报者申报情况的真实性作出阐述
                font = new Font(FangSongGB2312, 小四, Font.NORMAL);
                phrase = new Phrase(18, "请对申报者申报情况" + Environment.NewLine + "的真实性作出阐述", font);
                cell = new PdfPCell(phrase);
                cell.FixedHeight = 73.99f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.SetLeading(1, 1.5f);
                table.AddCell(cell);

                phrase = new Phrase(18, Recommendes[i].Elaborate, fillFont);
                cell = new PdfPCell(phrase);
                cell.FixedHeight = 73.99f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.SetLeading(1, 1.5f);
                table.AddCell(cell);
                #endregion

                #region 请对作品的意义、技术水平、适用范围及推广前景作出您的评价
                phrase = new Phrase(18, "请对作品的意义、技术水平、适用范围及推广前景作出您的评价", font);
                cell = new PdfPCell(phrase);
                cell.FixedHeight = 71.07f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.SetLeading(1, 1.5f);
                table.AddCell(cell);

                phrase = new Phrase(18, Recommendes[i].Evaluate, fillFont);
                cell = new PdfPCell(phrase);
                cell.FixedHeight = 71.07f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.SetLeading(1, 1.5f);
                table.AddCell(cell);
                #endregion

                #region 其它说明
                phrase = new Phrase(18, "其它说明", font);
                cell = new PdfPCell(phrase);
                cell.FixedHeight = 58.114f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                table.AddCell(cell);

                phrase = new Phrase(18, Recommendes[i].Remark, fillFont);
                cell = new PdfPCell(phrase);
                cell.FixedHeight = 58.114f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.SetLeading(1, 1.5f);
                table.AddCell(cell);
                #endregion

                #region 学校组织协调机构确认并盖章
                phrase = new Phrase(18, "学校组织协调机构确认并盖章", font);
                cell = new PdfPCell(phrase);
                cell.FixedHeight = 49.04f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.SetLeading(1, 1.5f);
                table.AddCell(cell);

                font = new Font(FangSongGB2312, 小三, Font.NORMAL);
                phrase = new Phrase(18, "（团委代章）       年   月   日", font);
                cell = new PdfPCell(phrase);
                cell.FixedHeight = 49.04f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cell.SetLeading(1, 1.5f);
                table.AddCell(cell);
                #endregion

                #region 学校组织协调机构确认并盖章
                font = new Font(FangSongGB2312, 小四, Font.NORMAL);
                phrase = new Phrase(18, "校主管领导或校主管部门确认盖章", font);
                cell = new PdfPCell(phrase);
                cell.FixedHeight = 49.04f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                cell.SetLeading(1, 1.5f);
                table.AddCell(cell);

                font = new Font(FangSongGB2312, 小三, Font.NORMAL);
                phrase = new Phrase(18, "       年   月   日", font);
                cell = new PdfPCell(phrase);
                cell.FixedHeight = 49.04f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                table.AddCell(cell);
                #endregion
                document.Add(table);
            }
            #endregion
        }
        private void CreateTableE(Document document)
        {
            #region 标题
            string str = "E．大赛组织委员会秘书处资格和形式审查意见";
            Font font = new Font(HeiTi, 小二, Font.BOLD);
            Paragraph paragraph = new Paragraph(48, str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            document.Add(NewLine(SongTi, 5, 5));
            #endregion

            #region 表E
            PdfPTable table = new PdfPTable(1);
            table.TotalWidth = 441.1f;
            table.LockedWidth = true;
            font = new Font(FangSongGB2312, 小三);

            str = "委会秘书处资格审查意见" + Environment.NewLine +
                  "" + Environment.NewLine +
                  "" + Environment.NewLine +
                  "" + Environment.NewLine +
                  "                     审查人（签名）" + Environment.NewLine +
                  "                              年   月   日";
            PdfPCell cell = new PdfPCell(new Phrase(28, str, font));
            cell.FixedHeight = 163.57f;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);

            str = "组委会秘书处形式审查意见" + Environment.NewLine +
                  "" + Environment.NewLine +
                  "" + Environment.NewLine +
                  "" + Environment.NewLine +
                  "                     审查人（签名）" + Environment.NewLine +
                  "                              年   月   日";
            cell = new PdfPCell(new Phrase(28, str, font));
            cell.FixedHeight = 149.96f;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);

            str = "组委会秘书处审查结果" + Environment.NewLine +
                  "" + Environment.NewLine +
                  "" + Environment.NewLine +
                  "" + Environment.NewLine +
                  "   □合格                    □不合格" + Environment.NewLine +
                  "                     审查人（签名）" + Environment.NewLine +
                  "                              年   月   日";
            cell = new PdfPCell(new Phrase(28, str, font));
            cell.FixedHeight = 170.66f;
            cell.SetLeading(1, 1.5f);
            table.AddCell(cell);
            document.Add(table);
            #endregion
        }
        private void CreateFG1G2(Document document)
        {
            string str = "F．参赛作品打印处";
            Font font = new Font(HeiTi, 小二, Font.BOLD);
            Paragraph paragraph = new Paragraph(28, str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            document.NewPage();
            str = "G1．大赛评审委员会预审意见粘贴处";
            paragraph = new Paragraph(28, str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
            document.NewPage();
            str = "G2．大赛评审委员会终审意见粘贴处";
            paragraph = new Paragraph(28, str, font);
            paragraph.Alignment = Rectangle.ALIGN_CENTER;
            document.Add(paragraph);
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
            return new Paragraph(Leading, str, font);
        }
    }
}
