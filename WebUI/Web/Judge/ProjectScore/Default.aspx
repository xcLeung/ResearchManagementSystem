<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Judge/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Web.Judge.ProjectScore.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/styles/JForm.css" rel="stylesheet" />
    <link href="../../css/ProjectScore.css" rel="stylesheet" />
    <link href="../../css/styles/tt_button.css" rel="stylesheet" />
    <script src="../../js/scripts/tt_button.js"></script>
    <script src="../../js/scripts/JForm.js"></script>

    <script>
        $(function () {
            $("#myFormId").jFormInit({
                triggerTargetId: "btn" //负责触发检查表单的默认按钮
            });

            $('#go').click(function () {
              //   alert("testGo");
                <% if((currentPage-1)*10+(index)>=ProjectCount){%>
                alert("已到最后一个项目");
                return;
                <%}%>
                window.location.href = "Default.aspx?MatchID=<%=MatchID%>&Index=<%=index>=10?1:index+1%>&currentpage=<%=index>=10?currentPage+1:currentPage %>";
            });

            $('#back').click(function () {
                //  alert("testBack");
                <% if(index<=1 && currentPage==1){%>
                alert("已到第一个项目");
                return;
                <%}%>
                window.location.href = "Default.aspx?MatchID=<%=MatchID%>&Index=<%=index<=1?10:index-1%>&currentpage=<%=index<=1?currentPage-1:currentPage%>";

            });

            $('#projectBack').click(function () {
                window.location.href = "../Project/Default.aspx?MatchID=<%=MatchID%>";
            });


            $('#btn').bind("click", function () {
                if ($('#score').val() == "") {
                    alert("评分不能为空!");
                    return;
                }
                var s = "";
                s = $.ajax({
                    type: "POST",
                    url: "AjaxAction.ashx",
                    data: {
                        dowhat: "ProjectScore",
                        Score: $("#score").val(),
                        Remark: $("#Remark").val(),
                        ProjectID: '<%=ProjectID %>',
                        MatchModelID: '<%=Match.MatchModel %>',
                        ID: '<%=HasRecord?Score.ID.ToString():"0" %>'
                    },
                    async: false
                }).responseText;
                if (s == "success") {
                    alert("操作成功");
                } else {
                    alert("操作失败");
                }
            });
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <div class="layout">
        <%if(showPdf){ %>
        <div class="Flash">
            <embed id="flashContainer" src="<%=FilePath %>" width="100%" height="1600px;"></embed>
        </div>
        <%} %>

        <form class="JForm" id="myFormId">
            <div>
                <label for="score">评分：</label>
                <input type="text" id="score" jformtype="number" value="<%=HasRecord?Score.Score.ToString():"" %>"/>
            </div>

            <div id="textareaDiv">
                <label for="Remark">备注：</label>
                <textarea id="Remark"><%=HasRecord?Score.Remark:"" %></textarea>
            </div>

            <div>
                <input type="button" id="btn" class="lxcButton" name="name" value="保存" />
            </div>

            <div>
                <input type="button" name="" id="back" value="" class="tt_button" />
                <label class="buttonLabel">上一个项目</label>
                <input type="button" name="" id="go" value="" class="tt_button" />
                <label class="buttonLabel">下一个项目</label>
                <input type="button" name="" id="projectBack" value="" class="tt_button" />
                <label class="buttonLabel">返回项目列表</label>
            </div>
        </form>
        

    </div>


</asp:Content>
