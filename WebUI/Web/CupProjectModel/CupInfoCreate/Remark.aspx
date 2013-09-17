<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="Remark.aspx.cs" Inherits="ResearchManagementSystem.Web.ProjectInfo.Remark" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../css/leftrightside.css" rel="stylesheet" />
    <link href="../../css/projectinfo.css" rel="stylesheet" />
    <script>
        $(function () {
            <% if (CupModelList[0].Statu == "提交" || isDeadLine(Match.DeclarantDeadLine))
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
            $("#btn").bind("click", function () {
                var s = "";
                s = $.ajax({
                    type: "POST",
                    url: "AjaxAction.ashx",
                    data: {
                        dowhat: "RemarkEdit",
                        ProjectID: '<%=ProjectID %>',
                        Remark: $("#Remark").val()
                    },
                    async: false
                }).responseText;
                if (s == "success") {
                    alert("操作成功！");
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
        <label style="margin:15px; color:#f00;"><%=CupModelList[0].Statu %></label>    
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
            <h2 class="Title">备注</h2>
            <hr />
            <br />
        </div>

        <div class="Warning">
                <div style="font-weight:bold; font-size:20px;">提示</div>
                <div style="margin-left:10px;">如有需要，请将指导教师姓名等其他信息填写在备注里，如无备注信息则填写"无"</div>           
        </div>

        <div class="layoutText">
                <label for="Remark" ">备注：</label>
                <textarea id="Remark" class="text"><%=CupModelList[0].Remark==""?"无":CupModelList[0].Remark %></textarea>
                <input type="button" id="btn" class="lxcButton" value="保存" />            

        </div>
    </div>
 
</asp:Content>
