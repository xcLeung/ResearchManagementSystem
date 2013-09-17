<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Judge/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Web.Judge.Project.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/list.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                            <th id="JudgeProject6">大类</th>
                            <th id="JudgeProject7">申报日期</th>
                            <th id="JudgeProject8">操作</th>
                        </tr>
                    </thead>
                    <%                      
                        if(Match.MatchModel==1){
                            for (int i = Math.Max((current_page - 1) * page_size, 0); i < ((current_page - 1) * page_size + page_size > Projects.Count ? Projects.Count : (current_page - 1) * page_size + page_size); i++)
                            { %>
                    <tr>
                        <td><%=Projects[i].Name %></td>
                        <td><%=Students[i].Name %></td>
                        <td><%=TeamMemberCount[i] %></td>
                        <td>
                            <%   for (int j = 0; j < Recommenders[i].Count;j++ ){ %>
                            <%if (j < Recommenders[i].Count - 1)
                              { %>
                                  <%=Recommenders[i][j].Name + ","%>
                            <%
                              }
                              else
                              {%>
                            <%=Recommenders[i][j].Name%>
                             <% }
                                 } %>
                        </td>
                        <td><%=Projects[i].DeclarationType %></td>
                        <td><%=Projects[i].Category %></td>
                        <td><%=Projects[i].DeclarationDate.ToShortDateString() %></td>
                        <td><a href="../ProjectScore/Default.aspx?MatchID=<%=MatchID %>&Index=<%=i%10+1 %>&currentPage=<%=current_page %>">审核项目</a></td>
                    </tr>
                    <%} }else if(Match.MatchModel==2){
                          for (int i = Math.Max((current_page - 1) * page_size, 0); i < ((current_page - 1) * page_size + page_size > Innovations.Count ? Innovations.Count : (current_page - 1) * page_size + page_size); i++)
                          {%>
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
                            <td><%=InnovationWork.Category %></td>
                            <td><%=Innovations[i].DeclarationDate.ToShortDateString() %></td>
                            <td><a href="../ProjectScore/Default.aspx?MatchID=<%=MatchID %>&Index=<%=i%10+1 %>&currentPage=<%=current_page %>">审核项目</a></td>
                        </tr>

                    <%}} %>
                    <tfoot>
                            <tr>
                                <td colspan="8">
                                    <div class="pager">
                                        <a href="?MatchID=<%=MatchID %>&page=1">第一页</a>
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
