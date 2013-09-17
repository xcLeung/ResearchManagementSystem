<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelCreate.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/leftrightside.css" rel="stylesheet" />
    <link href="../../css/projectinfo.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="leftside">
        <div class="ProjectStatu">当前作品状态</div>
        <label style="margin:15px; color:#f00;">未提交</label>    
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
       this is ProjectInfo
    </div>

</asp:Content>
