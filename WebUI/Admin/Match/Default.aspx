<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Admin.Match.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <link href="../css/list.css" rel="stylesheet" />
    <script>
        function submit_delete(Id) {
            if (!confirm("确定要删除吗？")) return;
            var s = "";
            s = $.ajax({
                type: "POST",
                url: "AjaxAction.ashx",
                data: {
                    dowhat: "delete",
                    Id:Id
                },
                async: false
            }).responseText;
            if (s == "SUCCESS") {
                location.href = "Default.aspx";
            } else if (s == "FAILD") {
                alert("删除失败");
            } 
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">
    <div class="ListContent">
        <div class="ListTitle">
                <h3>比赛列表</h3>
        </div>

        <div class="layout">
            <table class="LxcTable">
                <tr>
                  <th style="width:195px;">比赛名称</th>
                  <th>比赛结束时间</th>
                  <th>比赛状态</th>     
                  <th>申报截止时间</th>
                  <th>比赛模型</th>
                  <th>学院</th>
                  <th>评委人数</th>
                  <th>操作</th>
                 </tr>
                <%for(int i=0;i<Matchs.Count;i++){ %>
                  <tr>
                      <td><%=Matchs[i].MatchName %></td>
                      <td><%=Matchs[i].DeadLine.ToShortDateString() %></td>
                      <td><%=Matchs[i].Status %></td>
                  
                      <td><%=Matchs[i].DeclarantDeadLine.ToShortDateString() %></td>
                      <td><%=convertMatchModelName(MatchModels[i].Name) %></td>
                      <td><%=Matchs[i].College %></td>
                      <td><%=JudgeCount[i] %></td>
                      <td>
                          <input type="button" value="删除" onclick="submit_delete(<%=Matchs[i].ID%>)" />
                          <input type="button" value="修改" onclick="location.href='Edit.aspx?edit=<%=Matchs[i].ID%>    '"/>
                          <input type="button" value="详细" onclick="location.href='More.aspx?more=<%=Matchs[i].ID%>    '"/>
                          <input type="button" value="参赛项目" onclick="location.href='<%=getHref(MatchModels[i].Name)%><%=Matchs[i].ID%>    '"/>
                           <input type="button" value="比赛结果" onclick="location.href='../CupProjectModel/ProjectResult.aspx?MID=<%=Matchs[i].ID%>    '"/>
                      </td>
                  </tr>
                  <%} %>
                    <tfoot>
                         <tr>
                             <td colspan="8">
                             <div class="pager">
                                  <a href="?page=1">第一页</a>
                                  <a href="?page=<%=current_page - 1 %>">上一页</a>
                             
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
