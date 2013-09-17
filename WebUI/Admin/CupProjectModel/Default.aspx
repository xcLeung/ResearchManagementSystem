<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Admin.CupProjectModel.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <link href="../css/list.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">

    <div class="ListContent">
        <div class="ListTitle">
                <h3>"<%=Match.MatchName %>"的项目列表</h3>
        </div>

        <div class="layout">
            <table class="LxcTable">
            <tr>
                <th>作品    </th>
                <th style="width:60px;">申报人  </th>
                <th style="width:75px;">团队成员</th>
                <th style="width:75px;">推荐人  </th>
                <th style="width:75px;">申报类型</th>
                <th style="width:300px;">大类    </th>
                <th style="width:75px;">申报日期</th>
                <th style="width:75px;">状态    </th>
                <th style="width:45px;">操作    </th>
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
                <td><%=Projects[i].DeclarationDate.ToShortDateString().ToString()%></td>
                <td><%=CheckRecord[i]==null?"提交":CheckRecord[i].AfterStatus %></td>
                
                <td><input type="button" value="审核" onclick="location.href='MoreProjectInfo.aspx?ProjectID=<%=Projects[i].ID %>    &MID=<%=MatchID %>    '"/></td>
           </tr>
                <%} %>
                <tfoot>
                      <tr>
                          <td colspan="9">
                              <div class="pager">
                                  <a href="?MatchID=<%=MatchID%>&page=1">第一页</a>
                                  <a href="?MatchID=<%=MatchID %>&page=<%=current_page - 1%>">上一页</a>                          
                                  <span>当前：<%=current_page %> 页 | 总：<%=page_count %> 页</span>
                                  <a href="?MatchID=<%=MatchID %>&page=<%=current_page + 1 %>">下一页</a>
                                  <a href="?MatchID=<%=MatchID %>&page=<%=page_count %>">最后页</a>
                              </div>
                          </td>
                      </tr>
                </tfoot>
        </table>
        </div>

    </div>

</asp:Content>
