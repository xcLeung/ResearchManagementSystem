<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Web.Match.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/list.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="ListContent">
        <div class="ListTitle">
            <h3>"<%=StudentList[0].Name %>"可参与的比赛列表</h3>
        </div>


        <div class="layout">
                    <table class="LxcTable">
                        <thead>
                            <tr>
                                <th id="Matchth1">比赛名称</th>
                                <th id="Matchth2">比赛级别</th>
                                <th id="Matchth3">申报截止日期</th>
                                <th id="Matchth4">操作</th>
                            </tr>
                        </thead>
                        <%
                            for(int i=0;i<MatchList.Count;i++){
                                if (System.DateTime.Compare(System.DateTime.Now, MatchList[i].DeclarantDeadLine) > 0)
                                {
                                    MatchList.RemoveAt(i);
                                    i--;
                                    continue;
                                }                                                               
                                %>
                            <tr>
                                <td><%=MatchList[i].MatchName %></td>
                                <td><%=MatchList[i].College %></td>
                                <td><%=MatchList[i].DeclarantDeadLine %></td>
                                <% if(MatchList[i].MatchModel==1){%>
                                <td><a href="../CupProjectModel/CupInfoCreate/Default.aspx?MatchID=<%=MatchList[i].ID %>">申报项目</a></td>
                                <%}else if(MatchList[i].MatchModel==2){ %>
                                <td><a href="../InnovationProjectModel/InnovationProjectModelCreate/Default.aspx?MatchID=<%=MatchList[i].ID %>">申报项目</a></td>
                                <%} %>
                            </tr>
                        <%
                        }
                                                   
                             %>
                            
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
