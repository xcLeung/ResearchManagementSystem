<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="TeamMemberInfo.aspx.cs" Inherits="ResearchManagementSystem.Web.CupProjectModel.CupInfoEdit.TeamMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/leftrightside.css" rel="stylesheet" />
     <link href="../../css/styles/JForm.css" rel="stylesheet" />
        <link href="../../css/projectinfo.css" rel="stylesheet" />  
    <script src="../../js/scripts/JForm.js"></script>

    <script>
        $(function () {
            <% if (CupModellist[0].Statu == "提交" || isDeadLine(Match.DeclarantDeadLine))
               {   %>
            var inputs = $('input');
            var selects = $('select');
            $.each(inputs, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            $.each(selects, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            <%}%>
            var deleteHandle = function () {
                if (!confirm("删除操作不可恢复，确定吗？")) {
                    return;
                }
                var sid = $(this).parent().attr("value");
                if (s != null) {
                    var s = "";
                    $.ajax({
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
                } else { alert("操作成功！");}
                $(this).parent().remove();
            };



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
                    if ($(elem).attr("value") != null) {
                        formsData.push($(elem).serialize() + "&ID=" + $(elem).attr("value"));
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
                     window.location.href = "../CupInfoEdit/TeamMemberInfo.aspx?ProjectID=<%=ProjectID %>";
                } else {
                    alert("操作失败！");
                }

             });

            var SexArray = [];
            <%  for(int i=0;i<TeamMemberList.Count;i++ ){%>
                SexArray.push('<%=TeamMemberList[i].Sex %>')
            <%}%>
            var Initforms = $("form");
            var j = Initforms.length;
            $.each(Initforms, function (i, elem) {
                $(elem).jFormInit({
                    triggerTargetId: "SubmitAll" //负责触发检查表单的默认按钮
                });
                $(elem).find('select[name="Sex"]').val(SexArray[i]);

            });

            $('.FormDelete').bind("click", deleteHandle);


            $('#Add').bind("click", function () {
                var formHtml = '<form id="form' + (j++) + '" class="JForm">' +
                    '<div class="layout"><div><label for="Name">姓名：</label><input type="text" name="Name"  /> <label class="Remark" for="Name">必须实名</label> </div>'
                    + '<div><label for="Sex">性别：</label><select name="Sex" class="InputSelect"><option value="男">男</option><option value="女">女</option></select></div>'
                    + '  <div><label for="Age">年龄：</label><input type="text" name="Age" jformtype="number"/></div>'
                    + '<div><label for="BackGround">学历：</label><input type="text" name="BackGround"/><label class="Remark" for="BackGround">如：本科</label></div>'
                    + '<div><label for="WorkUnit">所在单位： </label><input type="text" name="WorkUnit"  /><label class="Remark" for="WorkUnit">如：华南农业大学</label></div>'
                    + '</div><input type="button" class="FormDelete" style="width:100px; height:30px; margin-left:335px; margin-bottom:20px;" value="删除团队成员"/>'
                  + '</form>';
                var forms = $("form");
                if (forms.length > 6) {
                    alert("团队成员不得超过7人！");
                    return;
                }
                $("#form" + (j - 1)).jFormInit({
                    triggerTargetId: "SubmitAll" //负责触发检查表单的默认按钮
                });

                $('.fButton').before(formHtml);
                $('.FormDelete').unbind("click", deleteHandle);
                $('.FormDelete').bind("click", deleteHandle);


            });


        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="leftside">
        <div class="ProjectStatu">当前作品状态</div>
        <label style="margin:15px; color:#f00;"><%=CupModellist[0].Statu %></label>    
        <h3 style="margin:10px; " class="ProjectInfo">项目信息填写</h3>

       <ul>
            <li><a href="../CupInfoCreate/DeclarantInfo.aspx?ProjectID=<%=ProjectID %>">申报人信息</a></li>
           <li><a href="../CupInfoCreate/TeamMemberInfo.aspx?ProjectID=<%=ProjectID %>">团队成员信息</a></li>
           <li><a href="../CupInfoCreate/WorkInfo.aspx?ProjectID=<%=ProjectID %>">作品信息</a></li>
           <li><a href="../CupInfoCreate/RecommendInfo.aspx?ProjectID=<%=ProjectID %>">推荐人信息</a></li>
           <li><a href="../CupInfoCreate/UploadPaper.aspx?ProjectID=<%=ProjectID %>">上传论文文档</a></li>
           <li><a href="../CupInfoCreate/Remark.aspx?ProjectID=<%=ProjectID %>">备注</a></li>
       </ul>


    </div>

    <div id="rightside">
        <div class="TitleHead">
            <a class="Picture" href="javascript:history.back(-1)"><img src="../../img/arrow-left.png" " /></a>
            <h2 class="Title">团队成员信息</h2>
            <hr />
            <br />
        </div>

        <%
            for(int i=0;i<TeamMemberList.Count;i++){
                
             %>

        <form id="form0" class="JForm" value="<%=TeamMemberList[i].ID %>">
        <div class="layout">
            <div>
                <label for="Name">姓名：</label>
                <input type="text" name="Name" value="<%=TeamMemberList[i].Name %>" />
                <label class="Remark" for="Name">必须实名</label>
            </div>

            <div>
                <label for="Sex">性别：</label>
                <select name="Sex" class="InputSelect">
                    <option value="男">男</option>
                    <option value="女">女</option>
                </select>
            </div>

            <div>
                <label for="Age">年龄：</label>
                <input type="text" name="Age" jformtype="number" value="<%=TeamMemberList[i].Age %>"/>
            </div>

             <div>
                <label for="BackGround">学历：</label>
                <input type="text" name="BackGround" value="<%=TeamMemberList[i].BackGround %>"/>
                <label class="Remark" for="BackGround">如：本科</label>
            </div>

            <div>
                <label for="WorkUnit">所在单位：</label>
                <input type="text" name="WorkUnit" value="<%=TeamMemberList[i].WorkUnit %>" />
                <label class="Remark" for="WorkUnit">如：华南农业大学</label>
            </div>

     
        </div>
            <input type="button" class="FormDelete" style="width:100px; height:30px; margin-left:335px; margin-bottom:20px;" value="删除团队成员"/>
            </form>
        <%
        }
             %>

        <div class="fButton">
            <hr />
            <br />
             <input type="button" id="SubmitAll" class="lxcButton" value="保存" style="width:70px; height:25px; margin-left:335px;" />
            <input type="button" id="Add" value="增加" class="lxcButton" style="width:70px; height:25px; margin-left:20px;" />
            <label style="color:#CCC; text-align:left;">最多7个</label>
           
        </div>

    </div>
</asp:Content>
