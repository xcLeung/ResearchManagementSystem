<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResearchManagementSystem.Web.Login.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../css/styles/960_16_col.css" rel="stylesheet" />
    <link href="../css/styles/RForm.css" rel="stylesheet" />
    <link href="../css/styles/login.css" rel="stylesheet" />
    <script src="../js/jquery-1.9.1.min.js"></script>

    <script>
        $(function () {
            

            $('#Sign').bind("click", function (e) {
                e.preventDefault();
                window.location.href = "Register.aspx";
            });

            $(".content").height($(window).height() - 73 - 30 - 20);
            $(window).resize(function () {
                $(".content").height($(window).height() - 73 - 30 - 20);
            });

            $('#Login').bind("click", function (e) {
                e.preventDefault();
                var s = "";
                s = $.ajax({
                    type: "POST",
                    url: "AjaxAction.ashx",
                    data: {
                        dowhat: "login",
                        UserName: $('#UserName').val(),
                        Password: $('#Password').val()
                    },
                    async: false
                }).responseText;
             //   alert(s);
                if (s == "Student") {
                    window.location.href = "../MyWorks/Default.aspx";
                } else if (s == "Judge") {
                    window.location.href = "../Judge/Match/Default.aspx";
                } else if (s == "ScauAdmin" || s == "CollegeAdmin") {
                    window.location.href = "../../Admin/Default.aspx";
                }
                else if (s == "EnableFalse") {
                    alert("帐号仍未审核通过！");
                }else{
                    alert("操作失败，请确保学号和密码都正确！");
                }

            });
        });
            

            function reset() {
                $("#UserName").val("");
                $("#Password").val("");
            };

    </script>
    <title>项目申报系统</title>
</head>
<body>
    <div class="head">
        headpic
        <div class="img"></div>

    </div>
    <div class="clear"></div>
    <div class="content container_16">

        <div class="formbox grid_7">
            <form class="RForm_default RForm_loginblock">


                <label for="UserName">Username:</label>
                <input type="text" id="UserName" />

                <label for="password">Password:</label>
                <input type="password" id="Password" />

                <!--<label class="Rcheckbox">-->
                    <!--<input type="checkbox">-->
                    <!--Remember me-->
                <!--</label>-->

                <button id="Sign" class="submit sign">Sign</button>
                <button class="submit" id="Login" >Login</button>              
            </form>
        </div>
        <div class="clear"></div>

    </div>
    <div class="clear"></div>
     <div class="foot">
        <div class="container_16">
            <div class="copyright grid_6">
                Copyright @2013 poweredBy One Tributary Team
            </div>
        </div>
       
    </div>
</body>
</html>
