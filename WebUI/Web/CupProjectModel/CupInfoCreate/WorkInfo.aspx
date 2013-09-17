<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="WorkInfo.aspx.cs" Inherits="ResearchManagementSystem.Web.ProjectInfo.WorkInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../css/leftrightside.css" rel="stylesheet" />
    <link href="../../css/projectinfo.css" rel="stylesheet" />
    <script>
        $(function () {
            <% if (CupModellist[0].Statu == "提交" || isDeadLine(Match.DeclarantDeadLine))
               {   %>
            var inputs = $('input');
            var selects = $('select');
            var textareas = $('textarea');
            $.each(inputs, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            $.each(selects, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            $.each(textareas, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            <%}%>

            $('#Category').change(function () {
               // alert($('#Category').val());
                var selectVal = $('#Category').val();
                if (selectVal == '自然科学类学术论文') {
                    // alert('1');
                    $('#a').fadeIn(1000);
                    $('#b').fadeOut(500);
                    $('#c').fadeOut(500);
                 
                } else if (selectVal == '哲学社会科学类社会调查报告和学术论文') {
                    $('#b').fadeIn(1000);
                    $('#a').fadeOut(500);
                    $('#c').fadeOut(500);
                } else if (selectVal == '科技发明制作') {
                    $('#c').fadeIn(1000);
                    $('#b').fadeOut(500);
                    $('#a').fadeOut(500);
                }
            });
              
        });

        function save() {
            var selectVal = $('#Category').val();
            var PName = $("#PName").val();
            var DeclarationType = $("#DeclarationType").val();
            var Category = $("#Category").val();
            if (!confirm("一旦确认大类将无法修改，确定吗？")) {
                return;
            }

            if (selectVal == '自然科学类学术论文') {
                var s = "";
                s = $.ajax({
                    type: "POST",
                    url: "AjaxAction.ashx",
                    data: {
                        dowhat: "WorkInfoCreateA",
                        formsData: $('#forma').serialize(),
                        PName: PName,
                        DeclarationType: DeclarationType,
                        Category: Category,
                        ProjectID: '<%=ProjectID %>',
                    },
                    async: false
                }).responseText;
                if (s == "success") {
                    alert("操作成功！");
                    window.location.href = "../CupInfoEdit/WorkInfo.aspx?ProjectID=<%=ProjectID %>";
                } else {
                    alert("操作失败！");
                }
                return;

            } else if (selectVal == '哲学社会科学类社会调查报告和学术论文') {
                var s = "";
                s = $.ajax({
                    type: "POST",
                    url: "AjaxAction.ashx",
                    data: {
                        dowhat: "WorkInfoCreateB",
                        formsData: $('#formb').serialize(),
                        PName: PName,
                        DeclarationType: DeclarationType,
                        Category: Category,
                        ProjectID: '<%=ProjectID %>',
                    },
                    async: false
                }).responseText;
                if (s == "success") {
                    alert("操作成功！");
                    window.location.href = "../CupInfoEdit/WorkInfo.aspx?ProjectID=<%=ProjectID %>";
                } else {
                    alert("操作失败！");
                }
                return;

            } else if (selectVal == '科技发明制作') {
                var s = "";
                s = $.ajax({
                    type: "POST",
                    url: "AjaxAction.ashx",
                    data: {
                        dowhat: "WorkInfoCreateC",
                        formsData: $('#formc').serialize(),
                        PName: PName,
                        DeclarationType: DeclarationType,
                        Category: Category,
                        ProjectID: '<%=ProjectID %>',
                    },
                    async: false
                }).responseText;
                if (s == "success") {
                    alert("操作成功！");
                    window.location.href = "../CupInfoEdit/WorkInfo.aspx?ProjectID=<%=ProjectID %>";
                } else {
                    alert("操作失败！");
                }
                return;

            }
        }

    

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="leftside">
       <div class="ProjectStatu">当前作品状态</div>
        <label style="margin:15px; color:#f00;"><%=CupModellist[0].Statu %></label>    
        <h3 style="margin:10px; " class="ProjectInfo">项目信息填写</h3>

       <ul>
            <li><a href="DeclarantInfo.aspx?ProjectID=<%=ProjectID %>">申报人信息</a></li>
           <li><a href="TeamMemberInfo.aspx?ProjectID=<%=ProjectID %>">团队成员信息</a></li>
           <li><a href="WorkInfo.aspx?ProjectID=<%=ProjectID %>">作品信息</a></li>
           <li><a href="RecommendInfo.aspx?ProjectID=<%=ProjectID %>">推荐人信息</a></li>
           <li><a href="UploadPaper.aspx?ProjectID=<%=ProjectID %>">上传论文文档</a></li>
           <li><a href="Remark.aspx?ProjectID=<%=ProjectID %>">备注</a></li>
       </ul>




    </div>


    <div id="rightside">
       <div class="TitleHead">
            <a class="Picture" href="javascript:history.back(-1)"><img src="../../img/arrow-left.png" " /></a>
            <h2 class="Title">作品信息</h2>
            <hr />
            <br />
        </div>


        <div class="Warning">
                <div style="font-weight:bold; font-size:20px;">提示</div>
                <div style="margin-left:10px;">特别注意！在填写作品信息时，请仔细注意右边提示语！按指示填写相关信息！</div>           
        </div>

    

        <div class="layout">

            <div style="margin-bottom:70px;">
                <label for="Name">* 项目名称：</label>
                <input type="text" id="PName"  />
                <label id="ProjectName" for="Name">必要时请加上\"--\"(两个英文减号)分隔主副标题,50字以内，生成的pdf中主副标题将分两行显示</label>
            </div>

            <div>
                <label for="DeclarationType">申报种类：</label>
                <select id="DeclarationType" style="width:250px;">
                    <option value="集体项目">集体项目</option>
                    <option value="个人项目">个人项目</option>
                </select>
            </div>

            <div>
                <label for="Category">大类(一级分类)：</label>
                <select id="Category" style="width:250px;">
                    <option value="自然科学类学术论文">自然科学类学术论文</option>
                    <option value="哲学社会科学类社会调查报告和学术论文">哲学社会科学类社会调查报告和学术论文</option>
                    <option value="科技发明制作">科技发明制作</option>
                </select>
            </div>

        </div>

        <div class="layout" id="a">
        <form id="forma">
            <div>
                <label for="SecondCategories">小类(二级分类)：</label>
                <select name="SecondCategories" style="width:250px;">
                    <option value="机械与控制">机械与控制</option>
                    <option value="信息技术">信息技术</option>
                    <option value="数理">数理</option>
                    <option value="生命科学">生命科学</option>
                    <option value="能源化工">能源化工</option>
                </select>
            </div>
            <div>
                <label for="ThirdCategories">三级分类：</label>
                <input type="text" name="ThirdCategories"  />              
            </div>
            <div>
                <label for="FourthCategories">四级分类：</label>
                <input type="text" name="FourthCategories"  />              
            </div>          

            <div> 
                <label for="Purpose" class="Textlabel">作品撰写的目的和基本思路：</label>
                <textarea name="Purpose" class="text"></textarea>
                <label class="TextlabelRemark" for="Purpose">200字以内</label>
            </div>

            <div> 
                <label for="Features" class="Textlabel">作品的科学性、先进性及独特之处：</label>
                <textarea name="Features" class="text"></textarea>
                <label class="TextlabelRemark" for="Features">200字以内</label>
            </div>

            <div> 
                <label for="ApplyValue" class="Textlabel">作品的实际应用价值和现实意义：</label>
                <textarea name="ApplyValue" class="text"></textarea>
                <label class="TextlabelRemark" for="ApplyValue">200字以内</label>
            </div>

            <div> 
                <label for="PaperDigest" class="Textlabel">作品摘要：</label>
                <textarea name="PaperDigest" class="text"></textarea>
                <label class="TextlabelRemark" for="PaperDigest">550字以内</label>
            </div>

            <div> 
                <label for="ReceivedAwards" class="Textlabel">作品在何时、何地、何种机构举行的会议上或报刊上发表及所获奖励：</label>
                <textarea name="ReceivedAwards" class="text"></textarea>
                <label class="TextlabelRemark" for="ReceivedAwards">350字以内（不超过5个）</label>
            </div>

            <div> 
                <label for="TestResult" class="Textlabel">鉴定结果：</label>
                <textarea name="TestResult" class="text"></textarea>
                <label class="TextlabelRemark" for="TestResult">100字以内</label>
            </div>

            <div> 
                <label for="References" class="Textlabel">请提供对于理解、审查、评价所申报作品具有参考价值的现有技术及技术文献的检索目录：</label>
                <textarea name="References" class="text"></textarea>
                <label class="TextlabelRemark" for="References">300字以内（不超过10条）</label>
            </div>

            <div> 
                <label for="MaterialsList" class="Textlabel">申报材料清单（申报论文一篇，相关资料名称及数量）：</label>
                <textarea name="MaterialsList" class="text"></textarea>
                <label class="TextlabelRemark" for="MaterialsList">250字以内</label>
            </div>

            <div> 
                <label for="SameResearchLevel" class="Textlabel">当前国内外同类课题研究水平概述：</label>
                <textarea name="SameResearchLevel" class="text"></textarea>
                <label class="TextlabelRemark" for="SameResearchLevel">1000字以内</label>
            </div>

            <div>
                <input type="button" value="保存" class="lxcButton" style="width:70px; height:25px; margin-left:135px;" onclick="save()" />
            </div>
        </form>
        </div>

    

        <div class="layout" id="b" hidden="hidden">
        <form id="formb">           
            <div>
                <label for="Categories">作品所属领域：</label>
                <select name="Categories" style="width:250px;">
                    <option value="哲学">哲学</option>
                    <option value="经济">经济</option>
                    <option value="社会">社会</option>
                    <option value="法律">法律</option>
                    <option value="教育">教育</option>
                    <option value="管理">管理</option>
                </select>
            </div>

            <div> 
                <label for="PurposeS" class="Textlabel">作品撰写的目的和基本思路：</label>
                <textarea name="PurposeS" class="text"></textarea>
                <label class="TextlabelRemark" for="PurposeS">200字以内</label>
            </div>

            <div> 
                <label for="FeaturesS" class="Textlabel">作品的科学性、先进性及独特之处：</label>
                <textarea name="FeaturesS" class="text"></textarea>
                <label class="TextlabelRemark" for="FeaturesS">200字以内</label>
            </div>

            <div> 
                <label for="ApplyValueS" class="Textlabel">作品的实际应用价值和现实意义：</label>
                <textarea name="ApplyValueS" class="text"></textarea>
                <label class="TextlabelRemark" for="ApplyValueS">200字以内</label>
            </div>

            <div> 
                <label for="PaperDigestS" class="Textlabel">作品摘要：</label>
                <textarea name="PaperDigestS" class="text"></textarea>
                <label class="TextlabelRemark" for="PaperDigestS">550字以内</label>
            </div>

            <div> 
                <label for="ReceivedAwardsS" class="Textlabel">作品在何时、何地、何种机构举行的会议上或报刊上发表及所获奖励：</label>
                <textarea name="ReceivedAwardsS" class="text"></textarea>
                <label class="TextlabelRemark" for="ReceivedAwardsS">350字以内（不超过5个）</label>
            </div>

            <div> 
                <label for="ReferencesS" class="Textlabel">请提供对于理解、审查、评价所申报作品具有参考价值的现有技术及技术文献的检索目录：</label>
                <textarea name="ReferencesS" class="text"></textarea>
                <label class="TextlabelRemark" for="ReferencesS">300字以内（不超过10条）</label>
            </div>

            <div> 
                <label for="SurveyWay" class="Textlabel">调查方式：</label>
                <textarea name="SurveyWay" class="text"></textarea>
                <label class="TextlabelRemark" for="SurveyWay">如：走访，问卷等</label>
            </div>

            <div> 
                <label for="SurveyUnit" class="Textlabel">主要调查单位及调查数量：</label>
                <textarea name="SurveyUnit" class="text"></textarea>     
                <label class="TextlabelRemark" for="SurveyUnit">省（市）县（区）乡（镇）村（街）单位邮编姓名电话调查单位个人次</label>                
            </div>
            
            <div> 
                <label for="SameResearchLevelS" class="Textlabel">当前国内外同类课题研究水平概述：</label>
                <textarea name="SameResearchLevelS" class="text"></textarea>
                <label class="TextlabelRemark" for="SameResearchLevelS">1000字以内</label>
            </div>


             <div>
                <input type="button" value="保存" class="lxcButton" style="width:70px; height:25px; margin-left:135px;" onclick="save()" />
            </div>
        </form> 
        </div>

        <div class="layout" id="c" hidden="hidden">
        <form id="formc"> 
            <div>
                <label for="CategoriesI">作品分类：</label>
                <select name="CategoriesI" style="width:250px;">
                    <option value="机械与控制">机械与控制</option>
                    <option value="信息技术">信息技术</option>
                    <option value="数理">数理</option>
                    <option value="生命科学">生命科学</option>
                    <option value="能源化工">能源化工</option>
                </select>
            </div>

            <div> 
                <label for="PurposeI" class="Textlabel">作品设计、发明的目的和基本思路，创新点，技术关键和主要技术指标：</label>
                <textarea name="PurposeI" class="text"></textarea>
                <label class="TextlabelRemark" for="PurposeI">200字以内</label>
            </div>

            <div> 
                <label for="FeaturesI" class="Textlabel">作品的科学性先进性：</label>
                <textarea name="FeaturesI" class="text"></textarea>
                <label class="TextlabelRemark" for="FeaturesI">必须说明与现有技术相比、该作品是否具有突出的实质性技术特点和显著进步。请提供技术性分析说明和参考文献资料</label>
            </div>

            <div> 
                <label for="ReceivedAwardsI" class="Textlabel">作品在何时、何地、何种机构举行的会议上或报刊上发表及所获奖励：</label>
                <textarea name="ReceivedAwardsI" class="text"></textarea>
                <label class="TextlabelRemark" for="ReceivedAwardsI">350字以内（不超过5个）</label>
            </div>

             <div> 
                <label for="WorkStatu" class="Textlabel">作品所处阶段：</label>
                <textarea name="WorkStatu" class="text"></textarea>
                <label class="TextlabelRemark" for="WorkStatu">A实验室阶段  B中试阶段 C生产阶段D（自填）</label>
            </div>

            <div> 
                <label for="AssignmentWay" class="Textlabel">技术转让方式：</label>
                <textarea name="AssignmentWay" class="text"></textarea>              
            </div>

            <div>
                <label for="WorkShow" class="Textlabel">作品可展示的形式：</label>
                <textarea name="WorkShow" class="text"></textarea>
                <label class="TextlabelRemark" for="WorkShow">如：实物、产品  模型  图纸  磁盘  现场演示  图片  录像  样品</label>
            </div>

            <div>
                <label for="ApplyValueI" class="Textlabel">使用说明及该作品的技术特点和优势，提供该作品的市场分析和经济效益预测：</label>
                <textarea name="ApplyValueI" class="text"></textarea>
              
            </div>


            <div>
                <label for="PatentStatu" class="Textlabel">专利申报情况：</label>
                <textarea name="PatentStatu" class="text"></textarea>
                <label class="TextlabelRemark" for="PatentStatu">如：提出专利申报，已获专利权批准，未提出专利申请。另外前两项需写出申报号与申报日期</label>
            </div>

            <div> 
                <label for="SameResearchLevelI" class="Textlabel">当前国内外同类课题研究水平概述：</label>
                <textarea name="SameResearchLevelI" class="text"></textarea>
                <label class="TextlabelRemark" for="SameResearchLevelI">1000字以内</label>
            </div>


             <div>
                <input type="button" value="保存" class="lxcButton" style="width:70px; height:25px; margin-left:135px;" onclick="save()" />
            </div>
        </form> 
        </div>

    </div>

</asp:Content>
