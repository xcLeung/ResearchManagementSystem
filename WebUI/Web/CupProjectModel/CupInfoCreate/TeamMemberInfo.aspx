<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="TeamMemberInfo.aspx.cs" Inherits="ResearchManagementSystem.Web.ProjectInfo.TeamMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../css/leftrightside.css" rel="stylesheet" />
    <link href="../../css/projectinfo.css" rel="stylesheet" />
     <link href="../../css/styles/JForm.css" rel="stylesheet" />
    <script src="../../js/scripts/JForm.js"></script>
    <script>
        var i = 1;

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
            $('.FormDelete').bind("click", function () {
                $(this).parent().remove();
               
            });

            $("#form0").jFormInit({
                triggerTargetId: "SubmitAll" //负责触发检查表单的默认按钮
            });


            $('#SubmitAll').bind("click", function () {
                var forms = $("form");
                var formsData = [];
                //alert(forms.length);
                var flag = "AllFormIsOk";
                $.each(forms, function (i, elem) {
                    if (!($(elem).jFormIsOk())) {
                        flag = "OneFormHasFalse";
                    }
                });
                if (flag == "OneFormHasFalse") {
                    return;
                }
                $.each(forms, function (i, elem) {               
                    formsData.push($(elem).serialize());
                });

                    var s = "";
                    s = $.ajax({
                        type: "POST",
                        url: "AjaxAction.ashx",
                        data: {
                            dowhat: "TeamMemberCreate",
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


            $('#Add').bind("click", function () {
                var formHtml = '<form id="form' + (i++) + '" class="JForm">' +
                    '<div class="layout"><div><label for="Name">姓名：</label><input type="text" name="Name"  /> <label class="Remark" for="Name">必须实名</label> </div>'
                    + '<div><label for="Sex">性别： </label><select name="Sex" class="InputSelect"><option value="男">男</option><option value="女">女</option></select></div>'
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
                $("#form" + (i - 1)).jFormInit({
                    triggerTargetId: "SubmitAll" //负责触发检查表单的默认按钮
                });

                $('.fButton').before(formHtml);
                $('.FormDelete').bind("click", function () {
                    $(this).parent().remove();
                });

                
            });


        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="leftside">
       <div class="ProjectStatu">当前作品状态</div>
        <label style="margin:15px; color:#f00;"><%=CupModellist[0].Statu %></label>    
        <h3 style="margin:10px;" class="ProjectInfo">项目信息填写</h3>

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
            <h2 class="Title">团队成员信息</h2>
            <hr />
            <br />
        </div>

        <form id="form0" class="JForm">
        <div class="layout">
            <div>
                <label for="Name">姓名：</label>
                <input type="text" name="Name"  />
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
                <input type="text" name="Age" jformtype="number" />
            </div>

             <div>
                <label for="BackGround">学历：</label>
                <input type="text" name="BackGround"/>
                <label class="Remark" for="BackGround">如：本科</label>
            </div>

            <div>
                <label for="WorkUnit">所在单位：</label>
                <input type="text" name="WorkUnit"  />
                <label class="Remark" for="WorkUnit">如：华南农业大学</label>
            </div>

     
        </div>
            <input type="button" class="FormDelete" style="width:100px; height:30px; margin-left:305px; margin-bottom:20px;" value="删除团队成员"/>
            </form>


        <div class="fButton">
            <hr />
            <br />
             <input type="button" id="SubmitAll" value="保存" class="lxcButton" style="width:70px; height:25px; margin-left:335px;" />
            <input type="button" id="Add" value="增加" class="lxcButton" style="width:70px; height:25px; margin-left:20px;" />
            <label style="color:#CCC; text-align:left;">最多7个</label>
           
        </div>

    </div>
</asp:Content>
