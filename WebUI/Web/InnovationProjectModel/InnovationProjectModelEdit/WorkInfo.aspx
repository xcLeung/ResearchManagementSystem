<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="WorkInfo.aspx.cs" Inherits="ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelEdit.WorkInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/leftrightside.css" rel="stylesheet" />
    <link href="../../css/projectinfo.css" rel="stylesheet" />
    <script src="../../js/scripts/RDatePicker.js"></script>

    <script>
        $(document).ready(function () {
            RUIKit.RDatePicker.pickBirthday.init("StartTime");
            RUIKit.RDatePicker.pickBirthday.init("EndTime");
            RUIKit.RDatePicker.pickBirthday.setBirthday('StartTime', '<%=Projects[0].StartTime.Year %>', '<%=Projects[0].StartTime.Month %>', '<%=Projects[0].StartTime.Day %>');
            RUIKit.RDatePicker.pickBirthday.setBirthday('EndTime', '<%=Projects[0].EndTime.Year %>', '<%=Projects[0].EndTime.Month %>', '<%=Projects[0].EndTime.Day %>');
            $("#DeclarationType").val('<%=Projects[0].DeclarationType%>');


            <% if (Projects[0].Statu == "提交" || isDeadLine(Match.DeclarantDeadLine))
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



            $("#btn").bind("click", function () {
                var StartTime = RUIKit.RDatePicker.pickBirthday.getBirthday("StartTime");
                var EndTime = RUIKit.RDatePicker.pickBirthday.getBirthday("EndTime");

                var s = "";
                s = $.ajax({
                    type: "POST",
                    url: "AjaxAction.ashx",
                    data: {
                        dowhat: "WorkInfoEdit",
                        formsData: $('#MyFormID').serialize(),
                        PName: $("#PName").val(),
                        StartTime: StartTime,
                        EndTime: EndTime,
                        DeclarationType: $("#DeclarationType").val(),
                        InterimReport: $("#InterimReport").val(),
                        SubmitAchievement: $("#SubmitAchievement").val(),
                        Managementbasis: $("#Managementbasis").val(),
                        ProjectID: '<%=ProjectID %>',
                        ID: '<%=WorksInfo[0].Id %>'
                    },
                    async: false
                }).responseText;
                if (s == "success") {
                    alert("操作成功！");
                    window.location.href = "../InnovationProjectModelCreate/WorkInfo.aspx?ProjectID=<%=ProjectID %>";
                } else {
                    alert("操作失败！");
                }
            });


        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="leftside">
       <div class="ProjectStatu">当前作品状态</div>
        <label style="margin:15px; color:#f00;"><%=Projects[0].Statu %></label>    
        <h3 style="margin:10px; " class="ProjectInfo">项目信息填写</h3>

        <ul>
           <li><a href="../InnovationProjectModelCreate/DeclarantInfo.aspx?ProjectID=<%=ProjectID %>">申报人信息</a></li>
           <li><a href="../InnovationProjectModelCreate/TeamMemberInfo.aspx?ProjectID=<%=ProjectID %>">团队成员信息</a></li>
           <li><a href="../InnovationProjectModelCreate/WorkInfo.aspx?ProjectID=<%=ProjectID %>">作品信息</a></li>
           <li><a href="../InnovationProjectModelCreate/RecommendInfo.aspx?ProjectID=<%=ProjectID %>">推荐人信息</a></li>
           <li><a href="../InnovationProjectModelCreate/Remark.aspx?ProjectID=<%=ProjectID %>">备注</a></li>
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
                <input type="text" id="PName"  value="<%=Projects[0].Name %>"/>
                <label id="ProjectName" for="Name">必要时请加上\"--\"(两个英文减号)分隔主副标题,50字以内，生成的pdf中主副标题将分两行显示</label>
            </div>

            <div>
                <label for="DeclarationType">申报种类：</label>
                <select id="DeclarationType" style="width:250px;">
                    <option value="创新训练项目">创新训练项目</option>
                    <option value="创业训练项目">创业训练项目</option>
                    <option value="创业实践项目">创业实践项目</option>
                </select>
            </div>

            <div>
                <label for="StartTime">项目开始时间：</label>
                <span id="StartTime" style="width:250px; display:inline-block;"></span>
            </div>

            <div>
                <label for="EndTime">项目结束时间：</label>
                <span id="EndTime" style="width:250px; display:inline-block;"></span>
            </div>
    </div>

    <div class="layout">
        <form id="MyFormID">
            <div>
                <label for="Category">一级学科名称：</label>
                <input type="text" name="Category" value="<%=WorksInfo[0].Category %>" />              
            </div>
            <div>
                <label for="SecondCategories">二级学科名称：</label>
                <input type="text" name="SecondCategories"  value="<%=WorksInfo[0].SecondCategories %>" />              
            </div>
            <div> 
                <label for="Purpose" class="Textlabel">项目实施的目的和意义：</label>
                <textarea name="Purpose" class="text"><%=WorksInfo[0].Purpose %></textarea>
                <label class="TextlabelRemark" for="Purpose">200字以内</label>
            </div>


            <div> 
                <label for="References" class="Textlabel">参考文献：</label>
                <textarea name="References" class="text"><%=WorksInfo[0].References %></textarea>
                <label class="TextlabelRemark" for="References">300字以内</label>
            </div>

            <div> 
                <label for="BaseContent" class="Textlabel">项目研究的基本内容：</label>
                <textarea name="BaseContent" class="text"><%=WorksInfo[0].BaseContent %></textarea>
                <label class="TextlabelRemark" for="BaseContent">300字以内</label>
            </div>

            <div> 
                <label for="KeyProblem" class="Textlabel">拟解决的关键问题：</label>
                <textarea name="KeyProblem" class="text"><%=WorksInfo[0].KeyProblem %></textarea>
                <label class="TextlabelRemark" for="KeyProblem">300字以内</label>
            </div>

            <div> 
                <label for="ProjectBasic" class="Textlabel">开展本项目的基础：</label>
                <textarea name="ProjectBasic" class="text"><%=WorksInfo[0].ProjectBasic %></textarea>
                <label class="TextlabelRemark" for="ProjectBasic">300字以内</label>
            </div>


            <div> 
                <label for="SpecificPlan" class="Textlabel">具体研究方案：</label>
                <textarea name="SpecificPlan" class="text"><%=WorksInfo[0].SpecificPlan %></textarea>
                <label class="TextlabelRemark" for="SpecificPlan">300字以内，说明项目的技术路线</label>
            </div>


            <div> 
                <label for="PracticalsStep" class="Textlabel">实施步骤：</label>
                <textarea name="PracticalsStep" class="text"><%=WorksInfo[0].PracticalsStep %></textarea>
                <label class="TextlabelRemark" for="PracticalsStep">1000字以内，分点叙述实施步骤</label>
            </div>
            

            <div> 
                <label for="PersonnelDivision" class="Textlabel">人员分工：</label>
                <textarea name="PersonnelDivision" class="text"><%=WorksInfo[0].PersonnelDivision %></textarea>
                <label class="TextlabelRemark" for="PersonnelDivision">300字以内</label>
            </div>

            <div> 
                <label for="ProjectPlan" class="Textlabel">项目计划：</label>
                <textarea name="ProjectPlan" class="text"><%=WorksInfo[0].ProjectPlan %></textarea>
                <label class="TextlabelRemark" for="ProjectPlan">300字以内，分条叙述项目计划，说明各阶段时间及其安排</label>
            </div>


            <div> 
                <label for="Features" class="Textlabel">项目特色与创新点：</label>
                <textarea name="Features" class="text"><%=WorksInfo[0].Features %></textarea>
                <label class="TextlabelRemark" for="Features">500字以内</label>
            </div>


            <div> 
                <label for="Expection" class="Textlabel">预期成果：</label>
                <textarea name="Expection" class="text"><%=WorksInfo[0].Expection %></textarea>
                <label class="TextlabelRemark" for="Expection">250字以内，研究论文、设计、专利、产品、鉴定、推广应用等</label>
            </div>

            <div> 
                <label for="Budget" class="Textlabel">经费预算及使用计划：</label>
                <textarea name="Budget" class="text"><%=WorksInfo[0].Budget %></textarea>
                <label class="TextlabelRemark" for="Budget">250字以内</label>
            </div>

       </form>

            <div> 
                <label for="InterimReport" class="Textlabel">提交中期报告：</label>
                <textarea name="InterimReport" id="InterimReport" class="text"><%=Projects[0].InterimReport %></textarea>
                <label class="TextlabelRemark" for="InterimReport">50字以内，说明何时提交中期项目成果</label>
            </div>

            <div> 
                <label for="SubmitAchievement" class="Textlabel">提交成果：</label>
                <textarea name="SubmitAchievement" id="SubmitAchievement" class="text"><%=Projects[0].SubmitAchievement %></textarea>
                <label class="TextlabelRemark" for="SubmitAchievement">50字以内，说明何时提交项目成果</label>
            </div>

            <div> 
                <label for="Managementbasis" class="Textlabel">管理依据：</label>
                <textarea name="Managementbasis" id="Managementbasis" class="text"><%=Projects[0].Managementbasis %></textarea>
                <label class="TextlabelRemark" for="Managementbasis">50字以内</label>
            </div>

            <div>
                <input type="button" value="保存"  id="btn"  class="lxcButton" style="width:70px; height:25px; margin-left:135px;"  />
            </div>
     </div>


   </div>
</asp:Content>
