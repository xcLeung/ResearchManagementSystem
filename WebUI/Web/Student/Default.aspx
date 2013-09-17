<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Web.Student.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/styles/JForm.css" rel="stylesheet" />
    <link href="../css/InfoContent.css" rel="stylesheet" />
    <script src="../js/scripts/JForm.js"></script>
    <script>
        $(document).ready(function () {
            $("#MyFormID").jFormInit({
                triggerTargetId: "btn" //负责触发检查表单的默认按钮
            });


            $("#btn").bind("click", function () {
                var Password = $("#Password").val();
                var ConfirmPassword = $("#ConfirmPassword").val();
                if (Password != ConfirmPassword) {
                    alert("两次输入密码不同");
                    return;
                }
                if ($("#MyFormID").jFormIsOk()) {
                    var s = "";
                    s = $.ajax({
                        type: "POST",
                        url: "AjaxAction.ashx",
                        data: {
                            dowhat: "UpdateStudent",
                            StudentID: '<%=Student.StudentID%>',
                            UserID: '<%=Student.UserId%>',
                            Mail: $("#Mail").val(),
                            Password: $("#Password").val(),
                            InTimeYear: $("#InTimeYear").val(),
                        },
                        async: false
                    }).responseText;
                    if (s == "success") {
                        alert("操作成功");
                        window.location.href = "Default.aspx";
                    } else {
                        alert("操作失败");
                    }
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="InfoContent">
        <div class="Title">
            <h3>"<%=Student.Name %>"的个人信息</h3>
        </div>


        <div class="Warning">
            <div>提示</div>
            <div style="margin-left:10px;">部分信息学生无权限修改，若有需要，请联系我们。</div>
        </div>

        <div class="layout">
            <form class="JForm" id="MyFormID">
                 <div>
                    <label for="Password">新密码：</label>
                    <input type="password" id="Password" name="Password" />
                    <label class="Remark" for="Password">无需修改可不填</label>
                </div>
                <div>
                    <label for="ConfirmPassword">重新输入：</label>
                    <input type="password" id="ConfirmPassword" name="ConfirmPassword"  />
                </div>

                 <div>
                    <label for="Name">姓名：</label>
                    <input type="text" id="Name" style="background-color:#EBEBE4; " readonly="true" value="<%=Student.Name %>"/>
                    <label class="Remark" for="Name">必须实名</label>
                </div>

                <div>
                    <label for="Sex">性别：</label>
                    <input type="text" id="Sex"  readonly="true" style="background-color:#EBEBE4;" value="<%=Student.Sex %>"/>
                </div>

                <div>
                     <label for="StudentID">学号：</label>
                     <input type="text" id="StudentID"  readonly="true" style="background-color:#EBEBE4; " value="<%=Student.StudentID %>"/>
                 </div>

                <div>
                     <label for="School">学校：</label>
                     <input type="text" id="School"  readonly="true" style="background-color:#EBEBE4; " value="<%=Student.School %>"/>
                 </div>

                <div>
                     <label for="College">学院：</label>
                     <input type="text" id="College"  readonly="true" style="background-color:#EBEBE4; " value="<%=Student.College %>"/>
                 </div>

                <div>
                     <label for="InTimeYear">入学年份：</label>
                     <input type="text" id="InTimeYear"  readonly="true" style="background-color:#EBEBE4; " value="<%=Student.InTimeYear %>"/>
                 </div>

                <div>
                     <label for="Major">专业：</label>
                     <input type="text" id="Major"  readonly="true" style="background-color:#EBEBE4; " value="<%=Student.Major %>"/>
                 </div>

                <div>
                     <label for="Mail">邮箱：</label>
                     <input type="text" id="Mail" value="<%=Student.Mail %>" jformtype="email"/>
                </div>

                <div>
                    <input type="button" id="btn" value="保存"/>
                </div>

            </form>
        </div>

    </div>
   
</asp:Content>
