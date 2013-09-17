<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Admin.InnovationProjectModel.Default" %>
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
                    <thead>
                        <tr>
                            <th id="JudgeProject1">作品</th>
                            <th id="JudgeProject2">申报人</th>
                            <th id="JudgeProject3">团队人数</th>
                            <th id="JudgeProject4">推荐人</th>
                            <th id="JudgeProject5">申报类型</th>
                            <th id="JudgeProject7">申报日期</th>
                            <th id="JudgeProject9">状态</th>
                            <th id="JudgeProject8">操作</th>
                        </tr>
                    </thead>
                    <%for (int i = 0; i < Innovations.Count;i++ ){ %>
                        <tr>
                            <td><%=Innovations[i].Name %></td>
                            <td><%=Students[i].Name %></td>
                            <td><%=TeamMemberCount[i] %></td>
                            <td>
                                <%   for (int j = 0; j < Tutors[i].Count;j++ ){ %>
                                <%if (j < Tutors[i].Count - 1)
                                  { %>
                                      <%=Tutors[i][j].Name + ","%>
                                <%
                                  }
                                  else
                                  {%>
                                <%=Tutors[i][j].Name%>
                                 <% }
                                     } %>
                            </td>
                            <td><%=Innovations[i].DeclarationType %></td>
                            <td><%=Innovations[i].DeclarationDate.ToShortDateString() %></td>
                            <td><%=CheckRecord[i]==null?"提交":CheckRecord[i].AfterStatus %></td>
                            <td><input type="button" value="审核" onclick="location.href='MoreProjectInfo.aspx?ProjectID=<%=Innovations[i].Id %>&MID=<%=MatchID %>'"/></td>
                        </tr>
                    <%} %>
                     <tfoot>
                            <tr>
                                <td colspan="8">
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
