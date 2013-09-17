<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ResearchManagementSystem.Admin.Student.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <link href="../css/list.css" rel="stylesheet" />
    <script>
        function Submit(UserId,Enable)
        {
            var s="";
            s=$.ajax({
                type: "POST",
                url: "AjaxAction.ashx",
                data: {
                    dowhat: "enable",
                    UserId:UserId,
                    Enable:Enable
                },
                async: false
            }).responseText;
            if(s="SUCCESS")
            {
                location.href="List.aspx?page="+<%=current_page%>;
            }else{
                alert("操作失败");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">

    <div class="ListContent">
        <div class="ListTitle">
                <h3>已审核学生账号</h3>
        </div>

        <div class="layout">
            <table class="LxcTable">
                 <tr>
                     <th>学号    </th>
                     <th>姓名    </th>
                     <th>性别    </th>
                     <th>入学年份</th>
                     <th>学校    </th>
                     <th>学院    </th>
                     <th>专业    </th>
                     <th>操作    </th>
                 </tr>
                 <%for (int i = 0; i < EnableStudents.Count; i++)
                   { %>
                 <tr>
                     <td><%=EnableStudents[i].StudentID%></td>
                     <td><%=EnableStudents[i].Name%></td>
                     <td><%=EnableStudents[i].Sex%></td>
                     <td><%=EnableStudents[i].InTimeYear%></td>
                     <td><%=EnableStudents[i].School%></td>
                     <td><%=EnableStudents[i].College%></td>
                     <td><%=EnableStudents[i].Major%></td>
                     <td><input type="button" value="禁用"onclick="Submit(<%=EnableStudents[i].UserId%>,'false')"/></td>
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
