<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="CupProjectModel.aspx.cs" Inherits="ResearchManagementSystem.Admin.Match.CupProjectModel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">
    <h2>参赛项目列表</h2>
    <hr />
    <fieldset>
         <legend></legend>
        <table>
            <tr>
                <th>作品    </th>
                <th>申报人  </th>
                <th>团队成员</th>
                <th>推荐人  </th>
                <th>申报类型</th>
                <th>大类    </th>
                <th>申报日期</th>
                <th>状态    </th>
                <th>操作    </th>
            </tr>
            
                <%for(int i=0;i<Projects.Count; i++) {%>
            <tr>
                <td><%=Projects[i].Name           %></td>
                <td><%=Students[i].Name           %></td>
                <td><%=TeamMemberCount[i]         %></td>
                <td>
                    <%for (int j = 0; j < Recommenders[i].Count;j++ ){%>
                    <%=Recommenders[i][j].Name %><br />
                    <%} %>
                </td>
                <td><%=Projects[i].DeclarationType%></td>
                <td><%=Projects[i].Category       %></td>
                <td><%=Projects[i].DeclarationDate%></td>
                <td><%=Projects[i].Statu          %></td>
                
                <td><input type="button" value="审核" onclick=""/></td>
           </tr>
                <%} %>
            
        </table>
        <div class="pager">
           <a href="?match=<%=MatchID %>&page=1">第一页</a>
           <a href="?match=<%=MatchID %>&page=<%=current_page - 1 %>">上一页</a>

           <span>当前：<%=current_page %> 页 | 总：<%=page_count %> 页</span>

           <a href="?match=<%=MatchID %>&page=<%=current_page + 1 %>">下一页</a>
           <a href="?match=<%=MatchID %>&page=<%=page_count %>">最后页</a>
        </div>
    </fieldset>
</asp:Content>
