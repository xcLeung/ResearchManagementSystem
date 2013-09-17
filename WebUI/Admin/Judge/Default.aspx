<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Admin.Judge.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <link href="../css/list.css" rel="stylesheet" />
    <script>
        function submit_delete(ID ,UserId) {
            if (!confirm("确定要删除吗？")) return;
            var s = "";
            s = $.ajax({
                type: "POST",
                url: "AjaxAction.ashx",
                data: {
                    dowhat: "delete",
                    ID: ID,
                    UserId: UserId,
                },
                async: false
            }).responseText;
            if (s == "SUCCESS") {
                location.href = "Default.aspx";
            } else if (s == "FAILD"){
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
                <h3>评委信息</h3>
        </div>
    <div class="layout">
        <table class="LxcTable JudgeTable">
            <tr>  
                <th>姓名</th>
                <th>职称    </th>
                <th>学历    </th>
                <th>研究方向</th>
                <th>学院    </th>
                <th>电话    </th>
                <th>操作    </th>
                
            </tr>
            <%for(int i=0;i<Judges.Count;i++){ %>
            <tr>
                <td><%=Judges[i].RealName %></td>
                <td><%=Judges[i].Title %></td>
                <td><%=Judges[i].Background %></td>
                <td><%=Judges[i].Research %></td>
                <td><%=Judges[i].College %></td>
                <td><%=Judges[i].Phone %></td>
                <td>
                    <input type="button" value="删除" onclick="submit_delete(<%=Judges[i].Id %>,<% =Judges[i].UserId%>)" />
                    <input type="button" value="详细" onclick="location.href='More.aspx?judge=<%=Judges[i].Id%>    '"/>
                    <input type="button" value="修改" onclick="location.href='Edit.aspx?edit=<%=Judges[i].Id%>    '"/>
                </td>
            </tr>
            <%} %>
            <tfoot>
               <tr>
                   <td colspan="7">
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
