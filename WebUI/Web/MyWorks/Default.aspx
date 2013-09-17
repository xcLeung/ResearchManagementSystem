<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Web.MyWorks.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/list.css" rel="stylesheet" />
    <script>
        $(function () {

        });
        


        function Sure(id, type) {
            if (!confirm("确认操作不可恢复，确定吗？")) {
                return;
            }
            var s = "";
            s = $.ajax({
                type: "POST",
                url: "AjaxAction.ashx",
                data: {
                    dowhat: "SubmitProject",
                    ProjectID: id,
                    Type: type,
                },
                async: false
            }).responseText;
            if (s == "success") {
                alert("操作成功！");
                window.location.href = "Default.aspx";
            } else if (s == "deadline") {
                alert("已过申报截止时间！");
            } else if (s == 'Submited') {
                alert("项目已提交，无法再次提交！");
            } else {
                alert("操作失败！");
            }
        };

        function del(id, type) {
            if (!confirm("删除操作不可恢复，确定吗？")) {
                return;
            }
            var s = "";
            s = $.ajax({
                type: "POST",
                url: "AjaxAction.ashx",
                data: {
                    dowhat: "DeleteProject",
                    ProjectID: id,
                    Type: type,
                },
                async: false
            }).responseText;
            if (s == "success") {
                alert("操作成功！");
                window.location.href = "Default.aspx";
            } else if (s == "Submited") {
                alert("项目已提交，无法删除！");
            } else {
                alert("操作失败！");
            }
        };


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



   <div class="ListContent">
        <div class="ListTitle">
            <h3>"<%=Student.Name %>"的作品列表</h3>
        </div>
      
        <div class="layout">
                    <table class="LxcTable">
                        <thead>
                           <tr>
                                <th id="Mywork1">作品名称</th>
                                <th id="Mywork2">能否修改</th>
                                <th id="Mywork3">作品状态</th>
                                <th id="Mywork4">查看审核情况</th>
                                <th id="Mywork5">操作</th>
                            </tr>
                        </thead>
                        <%
                            for (int i =Math.Max((current_page - 1) * page_size,0); i < ((current_page - 1) * page_size + page_size > PageSum ? PageSum : (current_page - 1) * page_size + page_size); i++)
                           {
                                
                                if(i<CupList.Count){
                                    Match = BLL.Match.SelectOne(((Models.DB.CupProjectModel)(ProjectList[i])).MatchID);
                                    if (((Models.DB.CupProjectModel)(ProjectList[i])).Statu == "提交" || System.DateTime.Compare(System.DateTime.Now, Match.DeclarantDeadLine) > 0)
                                    {
                                        isEdit = false;
                                    }
                                    else
                                    {
                                        isEdit = true;
                                    }
                                %>
                       
                        <tr>
                            <td><a href="../CupProjectModel/CupInfoEdit/DeclarantInfo.aspx?ProjectID=<%=((Models.DB.CupProjectModel)(ProjectList[i])).ID   %>"><%=((Models.DB.CupProjectModel)(ProjectList[i])).Name==""?"未填写作品名称":((Models.DB.CupProjectModel)(ProjectList[i])).Name %></a></td>
                            <td><%=isEdit?"可修改":"不可修改" %></td>
                            <td><%= ((Models.DB.CupProjectModel)(ProjectList[i])).Statu %></td>
                            <td><a href="../ProjectCheck/Default.aspx?ProjectID=<%=((Models.DB.CupProjectModel)(ProjectList[i])).ID %>&type=CupProjectModel">查看审核情况</a></td>
                            <td><p  class="WorkP" onclick="Sure('<%=((Models.DB.CupProjectModel)(ProjectList[i])).ID %>','CupProjectModel')">确认提交</p><p class="WorkP2"  onclick="del('<%=((Models.DB.CupProjectModel)(ProjectList[i])).ID %>','CupProjectModel')">删除项目</p><p><a href="Maker.aspx?project=<%=((Models.DB.CupProjectModel)ProjectList[i]).ID%>&model=CupProjectModel" target="_blank">制作PDF文档</a></p></td>
                          
                        </tr>
                        <%   
                        }else{
                            Match = BLL.Match.SelectOne(((Models.DB.InnovationProjectModel)ProjectList[i]).MatchID);
                            if (((Models.DB.InnovationProjectModel)ProjectList[i]).Statu == "提交" || System.DateTime.Compare(System.DateTime.Now, Match.DeclarantDeadLine) > 0)
                            {
                                isEdit = false;
                            }
                            else
                            {
                                isEdit = true;
                            }%>
                        <tr>
                            <td><a href="../InnovationProjectModel/InnovationProjectModelEdit/DeclarantInfo.aspx?ProjectID=<%=((Models.DB.InnovationProjectModel)ProjectList[i]).Id   %>"><%=((Models.DB.InnovationProjectModel)ProjectList[i]).Name==""?"未填写作品名称":((Models.DB.InnovationProjectModel)ProjectList[i]).Name %></a></td>
                            <td><%=isEdit?"可修改":"不可修改" %></td>
                            <td><%=((Models.DB.InnovationProjectModel)ProjectList[i]).Statu %></td>
                            <td><a href="../ProjectCheck/Default.aspx?ProjectID=<%=((Models.DB.InnovationProjectModel)ProjectList[i]).Id %>&type=InnovationProjectModel">查看审核情况</a></td>
                            <td><p class="WorkP" onclick="Sure('<%=((Models.DB.InnovationProjectModel)ProjectList[i]).Id %>','InnovationProjectModel')">确认提交</p><p class="WorkP2" onclick="del('<%=((Models.DB.InnovationProjectModel)ProjectList[i]).Id %>','InnovationProjectModel')">删除项目</p><p><a href="Maker.aspx?project=<%=((Models.DB.InnovationProjectModel)ProjectList[i]).Id%>&model=InnovationProjectModel" target="_blank">制作PDF文档</a></p></td>
                            
                        </tr>
                        
                        <%
                        }
                                }%>
                        <tfoot>
                            <tr>
                                <td colspan="5">
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
