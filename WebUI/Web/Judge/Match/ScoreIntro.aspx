<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Judge/Master.Master" AutoEventWireup="true" CodeBehind="ScoreIntro.aspx.cs" Inherits="ResearchManagementSystem.Web.Judge.Match.ScoreIntro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/intro.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="introContent">
        <div class="introTitle">
            <h3>"<%=Match.MatchName %>"的评分细则</h3>
        </div>
        <div class="layout clearfix">
            <div class="textarea"><%=Match.ScoreIntro %></div>
            <div class="after">
                <a href="../Project/Default.aspx?MatchID=<%=Match.ID %>">进入参赛项目列表</a>
            </div>
        </div>
    </div>
     
</asp:Content>
