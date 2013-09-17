<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Judge/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Web.Judge.Match.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/list.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="ListContent">
        <div class="ListTitle">
            <h3>"<%=Judge.RealName %>"可点评的的比赛列表</h3>
        </div>

        <div class="Warning">
                <div>提示</div>
                <div style="margin-left:10px;">请各位评委进入比赛前，认真阅读比赛评分细则！</div>           
        </div>


        <div class="layout">
            <table class="LxcTable">
                <thead>
                    <tr>
                        <th id="Match1">比赛名称</th>
                        <th id="Match2">比赛级别</th>
                        <th id="Match3">比赛截止时间</th>
                        <th id="Match4">操作</th>
                    </tr>
                </thead>
                <%for (int i = Math.Max((current_page - 1) * page_size,0); i < ((current_page - 1) * page_size + page_size > PageSum ? PageSum : (current_page - 1) * page_size + page_size); i++)
                  { %>
                <tr>
                    <td><%=MatchJudgeList[i].MatchName %></td>
                    <td><%=MatchJudgeList[i].MatchCollege %></td>
                    <td><%=MatchJudgeList[i].DeadLine %></td>
                    <td id="Control"><a href="ScoreIntro.aspx?MatchID=<%=MatchJudgeList[i].MatchID %>">1.查看评分细则</a><a href="../Project/Default.aspx?MatchID=<%=MatchJudgeList[i].MatchID %>">2.参赛项目</a></td>
                </tr>
                <%} %>
                <tfoot>
                     <tr>
                         <td colspan="4">
                            <div class="pager">
                                <a href="?page=1">第一页</a>
                                <a href="?page=<%=current_page - 1%>">上一页</a>                          
                                   <span>当前：<%=current_page %> 页 | 总：<%=page_count %> 页</span>
                                <a href="?page=<%=current_page + 1 %>">下一页</a>
                                <a href="?page=<%=page_count %>">最后页</a>
                             </div>
                          </td>
                     </tr>
                 </tfoot>
            </table>
        </div>

    </div>

</asp:Content>
