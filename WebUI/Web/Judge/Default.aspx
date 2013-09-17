<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Judge/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Web.Judge.Default" %>
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
                            dowhat: "UpdateJudge",
                            Id: '<%=Judge.Id%>',
                            UserID: '<%=Judge.UserId%>',
                            JobID: $("#JobID").val(),
                            CampusID: $("#CampusID").val(),
                            RealName: $("#RealName").val(),
                            Sex: $("#Sex").val(),
                            College: $("#College").val(),
                            Background: $("#Background").val(),
                            Research: $("#Research").val(),
                            WorkUnits: $("#WorkUnits").val(),
                            Title: $("#Title").val(),
                            Address: $("#Address").val(),
                            Phone: $("#Phone").val(),
                            Mail: $("#Mail").val(),
                            Password: $("#Password").val(),
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
            <h3>"<%=Judge.RealName %>"的个人信息</h3>
        </div>


        <div class="Warning">
            <div>提示</div>
            <div style="margin-left:10px;">部分信息评委无权限修改，若有需要，请联系我们。</div>
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
                    <label for="RealName">姓名：</label>
                    <input type="text" id="RealName" style="background-color:#EBEBE4; " readonly="true" value="<%=Judge.RealName %>"/>
                    <label class="Remark" for="RealName">必须实名</label>
                </div>

                <div>
                    <label for="Sex">性别：</label>
                    <input type="text" id="Sex"  readonly="true" style="background-color:#EBEBE4;" value="<%=Judge.Sex %>"/>
                </div>

                <div>
                     <label for="JobID">工号：</label>
                     <input type="text" id="JobID"  readonly="true" style="background-color:#EBEBE4; " value="<%=Judge.JobId %>"/>
                 </div>

                <div>
                    <label for="CampusID">校园卡号：</label>
                    <input type="text" id="CampusID"  readonly="true" style="background-color:#EBEBE4; " value="<%=Judge.CampusId %>"/>
                </div>


                <div>
                    <label for="College">所属学院：</label>
                    <input type="text" id="College"  readonly="true" style="background-color:#EBEBE4; " value="<%=Judge.College %>"/>
                </div>


                <div>
                     <label for="WorkUnits">工作单位：</label>
                     <input type="text" id="WorkUnits" value="<%=Judge.WorkUnits %>"/>
                </div>

                <div>
                     <label for="BackGround">学历：</label>
                     <input type="text" id="BackGround" value="<%=Judge.Background %>"/>
                </div>

                <div>
                     <label for="Title">职称：</label>
                     <input type="text" id="Title" value="<%=Judge.Title %>"/>
                </div>

                <div>
                     <label for="Research">研究方向：</label>
                     <input type="text" id="Research" value="<%=Judge.Research %>"/>
                </div>

                <div>
                     <label for="Address">住址：</label>
                     <input type="text" id="Address" value="<%=Judge.Address %>"/>
                </div>

                <div>
                     <label for="Phone">电话：</label>
                     <input type="text" id="Phone" value="<%=Judge.Phone %>" jformtype="mobile"/>
                     <label class="Remark" for="Phone">11位</label>
                </div>


                <div>
                     <label for="Mail">邮箱：</label>
                     <input type="text" id="Mail" value="<%=Judge.Mail %>" jformtype="email"/>
                </div>


                <div>
                    <input type="button" id="btn" value="保存"/>
                </div>

            </form>
        </div>

    </div>

</asp:Content>
