<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="TeamMemberInfo.aspx.cs" Inherits="ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelCreate.TeamMemberInfo" %>
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
                    window.location.href = "../InnovationProjectModelEdit/TeamMemberInfo.aspx?ProjectID=<%=ProjectID %>";
                } else {
                    alert("操作失败！");
                }

            });



            $('#Add').bind("click", function () {
                var formHtml = '<form id="form' + (i++) + '" class="JForm">' +
                    '<div class="layout"><div><label for="Name">姓名：</label><input type="text" name="Name"  /> <label class="Remark" for="Name">必须实名</label> </div>'
                    +'<div><label for="StudentID">学号：</label><input type="text" id="StudentID" name="StudentID" /></div><div><label for="College">学院：</label><input type="text" id="College" name="College"/></div><div><label for="Major">专业：</label><input type="text" id="Major" name="Major" /></div>'
                    +'<div><label for="InTimeYear">入学年份：</label><select id="InTimeYear" name="InTimeYear" class="InputSelect" style="margin-left:5px;"><option value="2009">2009</option><option value="2010">2010</option><option value="2011">2011</option><option value="2012">2012</option><option value="2013">2013</option></select></div>'
                    +'<div><label for="Phone">电话：</label><input type="text" id="Phone" name="Phone" jformtype="mobile"/><label class="Remark" for="Phone">11位</label></div><div><label for="Mail">邮箱：</label><input type="text" id="Mail"  name="Mail" jformtype="email"/></div><div><label for="Experience" class="Textlabel">个人经历简介：</label><textarea name="Experience" id="Experience" class="text" ></textarea><label class="TextlabelRemark" for="Experience">200字以内</label></div>'
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
        <label style="margin:15px; color:#f00;"><%=Projects[0].Statu %></label>    
        <h3 style="margin:10px;" class="ProjectInfo">项目信息填写</h3>


         <ul>
           <li><a href="DeclarantInfo.aspx?ProjectID=<%=ProjectID %>">申报人信息</a></li>
           <li><a href="TeamMemberInfo.aspx?ProjectID=<%=ProjectID %>">团队成员信息</a></li>
           <li><a href="WorkInfo.aspx?ProjectID=<%=ProjectID %>">作品信息</a></li>
           <li><a href="RecommendInfo.aspx?ProjectID=<%=ProjectID %>">推荐人信息</a></li>
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


        <form class="JForm" id="form0">
            <div class="layout">
                <div>
                    <label for="Name">姓名：</label>
                    <input type="text" name="Name"  />
                    <label class="Remark" for="Name">必须实名</label>
                </div>


                <div>
                     <label for="StudentID">学号：</label>
                     <input type="text" id="StudentID" name="StudentID" />
                 </div>


                <div>
                    <label for="College">学院：</label>
                    <input type="text" id="College" name="College"/>
                </div>
                
                <div>
                    <label for="Major">专业：</label>
                    <input type="text" id="Major" name="Major" />
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
                      <input type="text" id="Phone" name="Phone" jformtype="mobile"/>
                      <label class="Remark" for="Phone">11位</label>
                  </div>

                <div>
                      <label for="Mail">邮箱：</label>
                      <input type="text" id="Mail"  name="Mail" jformtype="email"/>
                  </div>

                <div>
                    <label for="Experience" class="Textlabel">个人经历简介：</label>
                    <textarea name="Experience" id="Experience" class="text" style="margin-left:140px;"></textarea>
                    <label class="TextlabelRemark" for="Experience">200字以内</label>
                </div>

            </div>

            <input type="button" class="FormDelete" style="width:100px; height:30px; margin-left:340px; margin-bottom:20px;" value="删除团队成员"/>
        </form>

        <div class="fButton">
            <hr />
            <br />
             <input type="button" id="SubmitAll" value="保存" class="lxcButton" style="width:70px; height:25px; margin-left:335px;" />
            <input type="button" id="Add" value="增加" class="lxcButton" style="width:70px; height:25px; margin-left:20px;" />
            <label style="color:#CCC; text-align:left;">最多4个</label>
           
        </div>

    </div>


</asp:Content>
