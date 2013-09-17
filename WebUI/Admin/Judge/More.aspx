<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="More.aspx.cs" Inherits="ResearchManagementSystem.Admin.Judge.More" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <link href="../css/Form.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">
    <div class="FormContent">
        <div class="FormTitle">
        <h3>评委“<%=Judge.RealName %>”的详细信息</h3>
         </div>
        <div class="layout">
              <div class="Form" id="judgeInfo">
                  <div><label >职称：</label><%=Judge.Title     %></div>
                  <div><label >学历：</label><%=Judge.Background%></div>
                  <div><label >工作单位：</label><%=Judge.WorkUnits %></div>
                  <div><label >学院：</label><%=Judge.College   %></div>
                  <div><label >研究方向：</label><%=Judge.Research  %></div>
                  <div><label >校园卡号：</label><%=Judge.CampusId  %></div>
                  <div><label >工号：</label><%=Judge.JobId     %></div>
                  <div><label >性别：</label><%=Judge.Sex       %></div>
                  <div><label >电话：</label><%=Judge.Phone     %></div>
                  <div><label >邮箱：</label><%=Judge.Mail      %></div> 
                  <div><label >地址：</label><%=Judge.Address   %></div>
                   <div>
                    <input type="button" style="margin: 15px 80px;" class="lxcButton" value="修改" onclick="location.href='Edit.aspx?edit=<%=Judge.Id%>    '" />
                    </div>
             </div>
            </div>
        </div>
        
    
</asp:Content>
