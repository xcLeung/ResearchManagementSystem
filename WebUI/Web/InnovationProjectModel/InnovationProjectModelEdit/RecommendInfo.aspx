<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="RecommendInfo.aspx.cs" Inherits="ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelEdit.RecommendInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/leftrightside.css" rel="stylesheet" />
     <link href="../../css/styles/JForm.css" rel="stylesheet" />
    <link href="../../css/projectinfo.css" rel="stylesheet" />
    <script src="../../js/scripts/JForm.js"></script>
    <script>
        $(document).ready(function () {
            <% if (Projects[0].Statu == "提交" || isDeadLine(Match.DeclarantDeadLine))
               {   %>
            var inputs = $('input');
            var textareas = $('textarea');
            $.each(inputs, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            $.each(textareas, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            <%}%>
            var i = 1;

            //删除按钮处理事件
            var deleteHandle = function (e) {
                if (!confirm("确认操作不可恢复，确定吗？")) {
                    return;
                }
                var sid = $(this).parent().attr("title");
                if (sid != null) {
                    var s = "";
                    s = $.ajax({
                        type: "POST",
                        url: "AjaxAction.ashx",
                        data: {
                            dowhat: "deleteRecommend",
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

            //初始化验证表单
            var Initforms = $("form");
            $.each(Initforms, function (i, elem) {
                $(elem).jFormInit({
                    triggerTargetId: "SubmitAll" //负责触发检查表单的默认按钮
                });
            });
            $('.FormDelete').bind("click", deleteHandle);

            $('#SubmitAll').bind("click", function () {
                var forms = $("form");
                var data = [];
                var flag = "AllFormIsOk";

                if (forms.length <= 0) {
                    alert("无推荐人无法保存！");
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
                        data.push($(elem).serialize() + "&ID=" + $(elem).attr("title"));
                    } else {
                        data.push($(elem).serialize() + "&ID=0");
                    }
                });

                var s = "";
                s = $.ajax({
                    type: "POST",
                    url: "AjaxAction.ashx",
                    data: {
                        dowhat: "RecommendInfoEdit",
                        formsData: data,
                        ProjectID: '<%=ProjectID %>',
                    },
                    async: false
                }).responseText;
                if (s == "success") {
                    alert("操作成功！");
                    window.location.href = "../InnovationProjectModelCreate/RecommendInfo.aspx?ProjectID=<%=ProjectID %>";
                } else {
                    alert("操作失败！");
                }
            });



            $('#Add').bind("click", function () {
                var formHtml = '<form id="form' + (i++) + '" class="JForm"><div class="layout">'
                    + '<div><label for="Name">姓名：</label><input type="text" name="Name"  /><label class="Remark" for="Name">必须实名</label></div>'
                    + '<div><label for="Age">年龄：</label><input type="text" name="Age" jformtype="number"/></div><div><label for="Work">行政/技术职务：</label><input type="text" name="Work"/><label class="Remark" for="Work">如：系副主任/讲师</label></div>'
                    + '<div><label for="Reasearch">研究方向：</label><input type="text" name="Reasearch"  /></div>'
                    + '<div><label for="Achieves" class="Textlabel">个人主要成果：</label><textarea name="Achieves" class="text"></textarea><label class="TextlabelRemark" for="Achieves">500字以内</label></div><div><label for="Recommendation" class="Textlabel">对项目的推荐意见：</label><textarea name="Recommendation" class="text"></textarea><label class="TextlabelRemark" for="Recommendation">200字以内</label>'
                    + '</div></div><input type="button" class="FormDelete" style="width:100px; height:30px; margin-left:335px; margin-bottom:20px;" value="删除团队成员"/>'
                    + '</form>';
                var forms = $("form");
                if (forms.length > 0) {
                    alert("团队成员不得超过1人！");
                    return;
                }
                $('#form' + (i - 1)).jFormInit({
                    triggerTargetId: "SubmitAll" //负责触发检查表单的默认按钮
                });

                $('.fButton').before(formHtml);
                $('.FormDelete').unbind("click", deleteHandle);
                $('.FormDelete').bind("click",deleteHandle);
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
            <h2 class="Title">推荐人信息</h2>
            <hr />
            <br />
        </div>

        <%for(int i=0;i<Tutors.Count;i++){ %>
        <form id="form0" class="JForm" title="<%=Tutors[i].Id %>">
            <div class="layout">
                <div class="JFormDiv">
                    <label for="Name">姓名：</label>
                    <input type="text" name="Name"  value="<%=Tutors[i].Name %>"/>
                    <label class="Remark" for="Name">必须实名</label>
                </div>

                <div class="JFormDiv">
                    <label for="Age">年龄：</label>
                    <input type="text" name="Age" jformtype="number" value="<%=Tutors[i].Age %>"/>
                </div>
                
                 <div class="JFormDiv">
                    <label for="Work">行政/技术职务：</label>
                    <input type="text" name="Work" value="<%=Tutors[i].Work %>"/>
                    <label class="Remark" for="Work">如：系副主任/讲师</label>
                </div>


                <div class="JFormDiv">
                    <label for="Reasearch">研究方向：</label>
                    <input type="text" name="Reasearch" value="<%=Tutors[i].Reasearch %>"/>
                </div>


                <div class="JFormDiv"> 
                    <label for="Achieves" class="Textlabel">个人主要成果：</label>
                    <textarea name="Achieves" class="text"><%=Tutors[i].Achieves %></textarea>
                    <label class="TextlabelRemark" for="Achieves">500字以内</label>

                </div>
               
   

                <div class="JFormDiv">
                    <label for="Recommendation" class="Textlabel">对项目的推荐意见：</label>
                    <textarea name="Recommendation" class="text"><%=Tutors[i].Recommendation %></textarea>
                    <label class="TextlabelRemark" for="Recommendation">200字以内</label>
                </div>


            </div>
            <input type="button" class="FormDelete" style="width:100px; height:30px; margin-top:20px; margin-left:335px; margin-bottom:20px;" value="删除成员"/>
        </form>

        <%} %>


        <div class="fButton" >
            <hr />
            <br />
            <input type="button" id="SubmitAll" value="保存" class="lxcButton" style="width:70px; height:25px; margin-left:335px;" />
            <input type="button" id="Add" value="增加" class="lxcButton" style="width:70px; height:25px; margin-left:20px;" />
            <label style="color:#CCC; text-align:left;">最多2个</label>
            
        </div>
    </div>

</asp:Content>
