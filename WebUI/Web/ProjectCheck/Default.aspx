<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Web.ProjectCheck.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/list.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="ListContent">
            

        <div class="ListTitle">
            <h3>"<%=ProjectName %>"的审核记录</h3>
        </div>
    
        <div class="Warning">
                <div>提示</div>
                <div style="margin-left:10px;">管理员审核后就有记录，请及时留意！</div>           
        </div>

        <div class="layout">
            <table class="LxcTable">
                <thead>
                    <tr>
                      <th id="Check1">审核的时间</th>
                      <th id="Check2">审核者</th>
                      <th id="Check3">审核之前的作品状态</th>
                      <th id="Check4">审核之后的作品状态</th>
                      <th id="Check5">原因</th>
                    </tr>
                </thead>
                <tr>
                    <td><%=CheckRecord.Time.ToShortDateString() %></td>
                    <td><%=CheckRecord.Checker %></td>
                    <td><%=CheckRecord.BeforeStatus %></td>
                    <td><%=CheckRecord.AfterStatus %></td>
                    <td><%=CheckRecord.Reason %></td>
                </tr>
            </table>                 
        </div>
    </div>
</asp:Content>
