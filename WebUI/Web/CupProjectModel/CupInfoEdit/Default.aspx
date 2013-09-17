<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Web.CupProjectModel.CupInfoEdit.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/leftrightside.css" rel="stylesheet" />
    <link href="../../css/projectinfo.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div id="leftside">
      <div class="ProjectStatu">当前作品状态</div>
        <label style="margin:15px; color:#f00;"><%=modellist[0].Statu %></label>    
        <br />
        

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
       this is ProjectEditInfo
    </div>

</asp:Content>
