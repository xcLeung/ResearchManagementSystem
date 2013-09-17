<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="UploadPaper.aspx.cs" Inherits="ResearchManagementSystem.Web.ProjectInfo.UploadPaper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../../css/leftrightside.css" rel="stylesheet" />
    <link href="../../css/projectinfo.css" rel="stylesheet" />
    <script src="../../uploadify/jquery.uploadify.js"></script>
    <link href="../../uploadify/uploadify.css" rel="stylesheet" />
    <script src="../../js/uploadify.upload.material.js"></script>

    <script>
        $(function () {
            <% if (CupModelList[0].Statu == "提交" || isDeadLine(Match.DeclarantDeadLine))
               {   %>
            var inputs = $('input');
            $.each(inputs, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            return;
            <%}%>
            UploadInit();

            $("#btn").bind("click", function () {
                var s = "";
                s = $.ajax({
                    type: "POST",
                    url: "AjaxAction.ashx",
                    data: {
                        dowhat: "UploadPaper",
                        ProjectID: '<%=ProjectID %>',
                        Url: $("#Material").val(),
                    },
                    async: false
                }).responseText;
                if (s == "success") {
                    alert("操作成功");
                } else {
                    alert("操作失败");
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
            <h2 class="Title">上传论文文档</h2>
            <hr />
            <br />
        </div>


            <div class="Warning">
                <div style="font-weight:bold; font-size:20px;">提示</div>
                <div style="margin-left:10px;">大小限制为100MB，如果文件太大，请打包压缩。文档无格式要求，pdf或doc均可(不推荐docx)</div>           
            </div>

        <div class="layout">
            <div id="upload_div"></div>
            <label>服务器地址：</label>
             <input type="text" id="Material" name="Material" value="" readonly="true" style="border:none; width:300px;"/>
            <input id="btn" type="button" class="lxcButton" value="保存" style="width:70px; margin-left:20px;"/>
             
        </div>

    </div>

</asp:Content>
