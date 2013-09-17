<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelEdit.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/leftrightside.css" rel="stylesheet" />
    <link href="../../css/projectinfo.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="leftside">
        <div class="ProjectStatu">当前作品状态</div>
        <label style="margin:15px; color:#f00;"><%=Projects[0].Statu %></label>    
        <h3 class="ProjectInfo" style="margin:10px;">项目信息填写</h3>


         <ul>
           <li><a href="../InnovationProjectModelCreate/DeclarantInfo.aspx?ProjectID=<%=ProjectID %>">申报人信息</a></li>
           <li><a href="../InnovationProjectModelCreate/TeamMemberInfo.aspx?ProjectID=<%=ProjectID %>">团队成员信息</a></li>
           <li><a href="../InnovationProjectModelCreate/WorkInfo.aspx?ProjectID=<%=ProjectID %>">作品信息</a></li>
           <li><a href="../InnovationProjectModelCreate/RecommendInfo.aspx?ProjectID=<%=ProjectID %>">推荐人信息</a></li>
           <li><a href="../InnovationProjectModelCreate/Remark.aspx?ProjectID=<%=ProjectID %>">备注</a></li>
       </ul>

        <div class="PdfMaker">制作申报书(PDF)</div>
        <div style="margin: 10px;"><a href="PdfMaker.aspx?ProjectID=<%=ProjectID %>">点击制作</a></div>

    </div>


    <div id="rightside">
       this is ProjectInfo
    </div>
</asp:Content>
