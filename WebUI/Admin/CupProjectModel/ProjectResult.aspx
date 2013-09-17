<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="ProjectResult.aspx.cs" Inherits="ResearchManagementSystem.Admin.CupProjectModel.ProjectResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <link href="../css/projectresult.css" rel="stylesheet" />
    <script>
        $(function () {
        });

        function Sure(id) {
            var s = "";
            s = $.ajax({
                type: "POST",
                url: "AjaxAction.ashx",
                data: {
                    dowhat: "PassProject",
                    ProjectID: id,
                    Type: '<%=Match.MatchModel %>'
                },
                async: false
            }).responseText;
            if (s == "success") {
                alert("操作成功！");
                window.location.href = "ProjectResult.aspx?MID=<%=MatchID %>";
            } else {
                alert("操作失败！");
            }
        };

        function Dely(id) {
            var s = "";
            s = $.ajax({
                type: "POST",
                url: "AjaxAction.ashx",
                data: {
                    dowhat: "DelyProject",
                    ProjectID: id,
                    Type: '<%=Match.MatchModel %>'
                },
                async: false
            }).responseText;
            if (s == "success") {
                alert("操作成功！");
                window.location.href = "ProjectResult.aspx?MID=<%=MatchID %>";
            } else {
                alert("操作失败！");
            }
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">

   <div class="ListContent">
        <div class="ListTitle">
            <h3>"<%=Match.MatchName %>"的比赛结果</h3>
        </div>


        <div class="layout">
            <table class="LxcTable">
                <%  if(Match.MatchModel==1){ %>
                <thead>
                    <tr>
                        <th style="width:280px;">项目名称</th>
                        <th>申报人</th>
                        <th>申报种类</th>
                        <th style="width:300px;">大类</th>
                        <th style="width:100px;">申报日期</th>
                        <th>分数</th>
                        <th>操作</th>
                    </tr>
                </thead>
               <%     for (int i = Math.Max((current_page - 1) * page_size, 0); i < ((current_page - 1) * page_size + page_size > Projects.Count ? Projects.Count : (current_page - 1) * page_size + page_size); i++)
                      { %>
                <tr>
                    <td><%=Projects[rankList[i].index].Name %></td>
                    <td><%=Students[rankList[i].index].Name %></td>
                    <td><%=Projects[rankList[i].index].DeclarationType %></td>
                    <td><%=Projects[rankList[i].index].Category %></td>
                    <td><%=Projects[rankList[i].index].DeclarationDate.ToShortDateString() %></td>
                    <td><%=Convert.ToInt32(rankList[i].score) %></td>
                    <%if(CheckRecords[rankList[i].index].AfterStatus=="终审通过"){ %>
                    <td><p onclick="Dely('<%=Projects[rankList[i].index].ID %>')">否决</p></td>
                    <%}else{ %>
                    <td><p onclick="Sure('<%=Projects[rankList[i].index].ID %>')">通过</p></td>
                    <%} %>
                </tr>
                <%} }else if(Match.MatchModel==2){%>
                <thead>
                    <tr>
                        <th style="width:280px;">项目名称</th>
                        <th>申报人</th>
                        <th>申报种类</th>
                        <th style="width:100px;">申报日期</th>
                        <th>分数</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <%for (int i = Math.Max((current_page - 1) * page_size, 0); i < ((current_page - 1) * page_size + page_size > Innovations.Count ? Innovations.Count : (current_page - 1) * page_size + page_size); i++)
                  { %>
                <tr>
                    <td><%=Innovations[rankList[i].index].Name %></td>
                    <td><%=Students[rankList[i].index].Name %></td>
                    <td><%=Innovations[rankList[i].index].DeclarationType %></td>
                    <td><%=Innovations[rankList[i].index].DeclarationDate.ToShortDateString() %></td>
                    <td><%=Convert.ToInt32(rankList[i].score) %></td>
                    <%if(CheckRecords[rankList[i].index].AfterStatus=="终审通过"){ %>
                    <td><p onclick="Dely('<%=Innovations[rankList[i].index].Id %>')">否决</p></td>
                    <%}else{ %>
                    <td><p onclick="Sure('<%=Innovations[rankList[i].index].Id %>')">通过</p></td>
                    <%} %>
                </tr>
                <%} }%>
                <tfoot>
                            <tr>
                              <td colspan="7">
                                <div class="pager">
                                   <a href="?MID=<%=MatchID %>&page=1">第一页</a>
                                    <a href="?MID=<%=MatchID %>&page=<%=current_page - 1%>">上一页</a>                          
                                       <span>当前：<%=current_page %> 页 | 总：<%=page_count %> 页</span>
                                       <a href="?MID=<%=MatchID %>&page=<%=current_page + 1 %>">下一页</a>
                                   <a href="?MID=<%=MatchID %>&page=<%=page_count %>">最后页</a>
                                </div>
                              </td>
                            </tr>
                        </tfoot>
            </table>
        </div>
   </div>>
</asp:Content>
