<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="Maker.aspx.cs" Inherits="ResearchManagementSystem.Web.MyWorks.Maker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/list.css" rel="stylesheet" />
    <script>
        function download(Path)
        {
            var s = "";
            s = $.ajax({
                type: "POST",
                url: "AjaxAction.ashx",
                data: {
                    dowhat: "download",
                    Path: Path,
                },
                async: false
            }).responseText;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="ListContent">
        <div class="layout">
        <table class="LxcTable">

            <thead>
                   <tr>
                        <th >文档</th>
                        <th >操作</th>
                   </tr>
            </thead>

            <tr>
                <td >
                    <%=FileOne %>
                    </td>
                <td>
                      <a href="<%="../PDF/" +UrlOne %>" target="_blank">预览</a>
                      <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">下载</asp:LinkButton>
                </td>
            </tr>
            <tr>
            <%if(FileTwo!="") { %>     
                <td>
                     <%=FileTwo %>
                 </td>
                <td>
                     <a href="<%="../PDF/" +UrlTwo %>" target="_blank">预览</a>
                     <asp:LinkButton ID="LinkButton2" runat="server"  OnClick="LinkButton2_Click" >下载</asp:LinkButton>
                 </td>
             <%} %>
             </tr>
</table>
         </div>
      </div>
    </form>
</asp:Content>
