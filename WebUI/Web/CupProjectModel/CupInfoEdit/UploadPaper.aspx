<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="UploadPaper.aspx.cs" Inherits="ResearchManagementSystem.Web.CupProjectModel.CupInfoEdit.UploadPaper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <link href="../../css/leftrightside.css" rel="stylesheet" />
         <link href="../../css/projectinfo.css" rel="stylesheet" />
         <script src="../../uploadify/jquery.uploadify.js"></script>
         <link href="../../uploadify/uploadify.css" rel="stylesheet" />
         <script src="../../js/uploadify.upload.material.js"></script>

    <script>
        $(function () {
            UploadInit();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="leftside">
        <div class="ProjectStatu">当前作品状态</div>
        <label style="margin:15px; color:#f00;"><%=CupModellist[0].Statu %></label>    
        <h3 style="margin:10px;" class="ProjectInfo">项目信息填写</h3>

       <ul>
            <li><a href="../CupInfoCreate/DeclarantInfo.aspx?ProjectID=<%=ProjectID %>">申报人信息</a></li>
           <li><a href="../CupInfoCreate/TeamMemberInfo.aspx?ProjectID=<%=ProjectID %>">团队成员信息</a></li>
           <li><a href="../CupInfoCreate/WorkInfo.aspx?ProjectID=<%=ProjectID %>">作品信息</a></li>
           <li><a href="../CupInfoCreate/RecommendInfo.aspx?ProjectID=<%=ProjectID %>">推荐人信息</a></li>
           <li><a href="../CupInfoCreate/UploadPaper.aspx?ProjectID=<%=ProjectID %>">上传论文文档</a></li>
           <li><a href="../CupInfoCreate/Remark.aspx?ProjectID=<%=ProjectID %>">备注</a></li>
       </ul>

        <div class="PdfMaker">制作申报书(PDF)</div>
        <div style="margin: 10px;"><a href="../CupInfoCreate/PdfMaker.aspx">点击制作</a></div>

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
           
        </div>

    </div>

</asp:Content>
