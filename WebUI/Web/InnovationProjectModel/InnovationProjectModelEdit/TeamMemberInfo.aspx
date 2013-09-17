<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="TeamMemberInfo.aspx.cs" Inherits="ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelEdit.TeamMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/styles/JForm.css" rel="stylesheet" />
     <link href="../../css/leftrightside.css" rel="stylesheet" />
    <link href="../../css/projectinfo.css" rel="stylesheet" />
    <script src="../../js/scripts/JForm.js"></script>
    <script>
        var i = 1;
        $(document).ready(function () {
            <% if (Projects[0].Statu == "提交" || isDeadLine(Match.DeclarantDeadLine))
               {   %>
            var inputs = $('input');
            var selects = $('select');
            var textareas = $('textarea');
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



            var deleteHandle = function () {
                if (!confirm("删除操作不可恢复，确定吗？")) {
                    return;
                }
                var sid = $(this).parent().attr("title");
                if (sid != null) {
                    var s = "";
                    s = $.ajax({
                        type: "POST",
                        url: "AjaxAction.ashx",
                        data: {
                            dowhat: "deleteTeamMember",
                            ID: sid,
                            ProjectID: '<%=ProjectID %>',
                        },
                        async: false
                    }).responseText;
                    if (s == "success") {
                        alert("操作成功！");
                    } else {
                        alert("操作失败！");
                    }
                } else { alert("操作成功！")}
                $(this).parent().remove();
            };

            var InTtimeYearArray = [];
            <%  for(int i=0;i<TeamMembers.Count;i++ ){%>
            InTtimeYearArray.push('<%=TeamMembers[i].InTimeYear %>')
            <%}%>
            var Initforms = $("form");
            var j = Initforms.length;
            $.each(Initforms, function (i, elem) {
                $(elem).jFormInit({
                    triggerTargetId: "SubmitAll" //负责触发检查表单的默认按钮
                });
                $(elem).find('select[name="InTimeYear"]').val(InTtimeYearArray[i]);

            });

            $('.FormDelete').bind("click", deleteHandle);
           
            $('#Add').bind("click", function () {
                var formHtml = '<form id="form' + (i++) + '" class="JForm">' +
                    '<div class="layout"><div><label for="Name">姓名：</label><input type="text" name="Name"  /> <label class="Remark" for="Name">必须实名</label> </div>'
                    + '<div><label for="StudentID">学号：</label><input type="text" id="StudentID" name="StudentID" /></div><div><label for="College">学院：</label><input type="text" id="College" name="College"/></div><div><label for="Major">专业：</label><input type="text" id="Major" name="Major" /></div>'
                    + '<div><label for="InTimeYear">入学年份：</label><select id="InTimeYear" name="InTimeYear" class="InputSelect" style="margin-left:5px;"><option value="2009">2009</option><option value="2010">2010</option><option value="2011">2011</option><option value="2012">2012</option><option value="2013">2013</option></select></div>'
                    + '<div><label for="Phone">电话：</label><input type="text" id="Phone" name="Phone" jformtype="mobile"/><label class="Remark" for="Phone">11位</label></div><div><label for="Mail">邮箱：</label><input type="text" id="Mail"  name="Mail" jformtype="email"/></div><div><label for="Experience" class="Textlabel">个人经历简介：</label><textarea name="Experience" id="Experience" class="text" ></textarea><label class="TextlabelRemark" for="Experience">200字以内</label></div>'
                    + '</div><input type="button" class="FormDelete" style="width:100px; height:30px; margin-left:335px; margin-bottom:20px;" value="删除团队成员"/>'
                  + '</form>';
                var forms = $("form");
                if (forms.length > 3) {
                    alert("团队成员不得超过4人！");
                    return;
                }
                $("#form" + (i - 1)).jFormInit({
                    triggerTargetId: "SubmitAll" //负责触发检查表单的默认按钮
                });

                $('.fButton').before(formHtml);
                $('.FormDelete').unbind("click", deleteHandle);
                $('.FormDelete').bind("click", deleteHandle);

            });

                $('#SubmitAll').bind("click", function () {
                    var forms = $("form");
                    var formsData = [];
                    var flag = "AllFormIsOk";
                    if (forms.length <= 0) {
                        alert("无团队成员无法保存！");
                        return;
                    }


                    $.each(forms, function (i, elem) {
                        if (!($(elem).jFormIsOk())) {
                            flag = "OneFormHasFalse";
                        }
                    });
                    if (flag == "OneFormHasFalse") {
                        return;
                    }
                    $.each(forms, function (i, elem) {
                        if ($(elem).attr("title") != null) {
                            formsData.push($(elem).serialize() + "&ID=" + $(elem).attr("title"));
                        } else {
                            formsData.push($(elem).serialize() + "&ID=0");
                        }
                    });
                    var s = "";
                    s = $.ajax({
                        type: "POST",
                        url: "AjaxAction.ashx",
                        data: {
                            dowhat: "TeamMemberEdit",
                            formsData: formsData,
                            ProjectID: '<%=ProjectID %>',
                    },
                    async: false
                }).responseText;
                 if (s == "success") {
                        alert("操作成功！");
                        window.location.href = "../InnovationProjectModelCreate/TeamMemberInfo.aspx?ProjectID=<%=ProjectID %>";
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
        <h3 style="margin:10px;" class="ProjectInfo">项目信息填写</h3>


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
            <h2 class="Title">团队成员信息</h2>
            <hr />
            <br />
        </div>
        <% for(int i=0;i<TeamMembers.Count;i++){%>
            <form class="JForm" id="form0" title="<%=TeamMembers[i].Id %>">
            <div class="layout">
                <div>
                    <label for="Name">姓名：</label>
                    <input type="text" name="Name" value="<%=TeamMembers[i].Name %>" />
                    <label class="Remark" for="Name">必须实名</label>
                </div>


                <div>
                     <label for="StudentID">学号：</label>
                     <input type="text" id="StudentID" name="StudentID" value="<%=TeamMembers[i].StudentID %>" />
                 </div>


                <div>
                    <label for="College">学院：</label>
                    <input type="text" id="College" name="College" value="<%=TeamMembers[i].College %>"/>
                </div>
                
                <div>
                    <label for="Major">专业：</label>
                    <input type="text" id="Major" name="Major" value="<%=TeamMembers[i].Major %>"/>
                </div>


                <div>
                    <label for="InTimeYear">入学年份：</label>
                    <select id="InTimeYear" name="InTimeYear" class="InputSelect" style="margin-left:5px;">
                        <option value="2009">2009</option>
                        <option value="2010">2010</option>
                        <option value="2011">2011</option>
                        <option value="2012">2012</option>
                        <option value="2013">2013</option>
                    </select>
                </div>


                 <div>
                      <label for="Phone">电话：</label>
                      <input type="text" id="Phone" name="Phone" jformtype="mobile" value="<%=TeamMembers[i].Phone %>"/>
                      <label class="Remark" for="Phone">11位</label>
                  </div>

                <div>
                      <label for="Mail">邮箱：</label>
                      <input type="text" id="Mail"  name="Mail" jformtype="email" value="<%=TeamMembers[i].Mail %>"/>
                  </div>

                <div>
                    <label for="Experience" class="Textlabel">个人经历简介：</label>
                    <textarea name="Experience" id="Experience" class="text" style="margin-left:140px;"><%=TeamMembers[i].Experience %></textarea>
                    <label class="TextlabelRemark" for="Experience">200字以内</label>
                </div>

            </div>

            <input type="button" class="FormDelete" style="width:100px; height:30px; margin-left:340px; margin-bottom:20px;" value="删除团队成员"/>
        </form>
        
        <% }%>

        <div class="fButton">
            <hr />
            <br />
             <input type="button" id="SubmitAll" value="保存" class="lxcButton" style="width:70px; height:25px; margin-left:335px;" />
            <input type="button" id="Add" value="增加" class="lxcButton" style="width:70px; height:25px; margin-left:20px;" />
            <label style="color:#CCC; text-align:left;">最多4个</label>
           
        </div>
    </div>

</asp:Content>
