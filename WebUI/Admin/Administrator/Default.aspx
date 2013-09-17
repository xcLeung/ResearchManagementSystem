<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Admin.Administrator.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <link href="../css/Administrator.css" rel="stylesheet" />
    <link href="../css/list.css" rel="stylesheet" />
    <script>
        function submit_delete(_ID,UserId) {
            if (!confirm("确定要删除吗？")) return;
            var s = "";
            s = $.ajax({
                type: "POST",
                url: "AjaxAction.ashx",
                data: {
                    dowhat: "delete",
                    AdminModelID: _ID,
                    UserId:UserId
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
                <h3>管理员列表</h3>
        </div>
        

        <div class="layout">
         <table class="LxcTable">
             <tr>
                 <th>姓名</th>
                 <th>工号</th>
                 <th>学院</th>
                 <th>邮箱</th>
                 <th>电话</th>
                 <th>性别</th>
                 <th>操作</th>
             </tr>
             <%for(int i=0;i<Admins.Count;i++){ %>
             <tr>
                 <td><%=Admins[i].Name %></td>
                 <td><%=Admins[i].JobId %></td>
                 <td><%=Admins[i].College %></td>
                 <td><%=Admins[i].Mail %></td>
                 <td><%=Admins[i].Phone %></td>
                 <td><%=Admins[i].Sex %></td>
                 <td>
                     <input type="button" value="删除" onclick="submit_delete(<%=Admins[i].Id%>,<%=Admins[i].UserId%>)" />
                     <input type="button" value="修改" onclick="location.href='Edit.aspx?edit=<%=Admins[i].Id%>    '"/>
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
