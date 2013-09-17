<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ResearchManagementSystem.Web.Login.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../css/styles/960_16_col.css" rel="stylesheet" />
    <link href="../css/styles/cssReset.css" rel="stylesheet" />
    <link href="../css/styles/login.css" rel="stylesheet" />
    <link href="../css/styles/JForm.css" rel="stylesheet" />
    <link href="../css/Register.css" rel="stylesheet" />
    
        <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="../js/scripts/JForm.js"></script>
    <script>
        $(function () {
            $('#myFormId').jFormInit({
                triggerTargetId: "btn" //负责触发检查表单的默认按钮
            });

            $(".content").height($(window).height() - 73 - 30 - 20);
            $(window).resize(function () {
                $(".content").height($(window).height() - 73 - 30 - 20);
            });


            $("#btn").bind("click", function (e) {
                if ($("#myFormId").jFormIsOk()) {
                    if ($("#Password").val() != $("#PasswordConfirm").val()) {
                        alert("密码不一致！");
                        return;
                    }
                    
                    var s = "";
                    s = $.ajax({
                        type: "POST",
                        url: "AjaxAction.ashx",
                        data: {
                            dowhat: "register",
                            UserName: $("#UserName").val(),
                            Password: $("#Password").val(),
                            StudentID: $("#StudentID").val(),
                            Name: $("#Name").val(),
                            Sex: $("#Sex").val(),
                            InTimeYear: $("#InTimeYear").val(),
                            School: $("#School").val(),
                            College: $("#College").val(),
                            Major: $("#Major").val(),
                            Mail: $("#Mail").val()
                        },
                        async: false
                    }).responseText;
                    if (s == "success") {
                        alert("操作成功！");
                        window.location.href = "Default.aspx";
                    } else if (s == "USEREXIST") {
                        alert("用户名已经存在");
                    } else if (s == "STUDENTEXIST") {
                        alert("学生学号已经存在");
                    }
                    else {
                        alert("创建失败！");
                    }

                }
            });



        });

        


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
        <div class="grid_14 prefix_1">
            <fieldset style="border:1px solid #CCC;">
            <legend style="font-weight:bold;">学生信息注册</legend>
             <form class="JForm" id="myFormId">
                 <div class="Info">
               
                    <div>
                        <label for="UserName">用户名：</label>
                        <input type="text" id="UserName" jformtype="username" jformNotAllowNull="true"/>
                    </div>
                    
                    <div>
                        <label for="Password">密码：</label>
                        <input type="password" id="Password" jformtype="password" jformNotAllowNull="true"/>
                    </div>
                    
                    <div>
                        <label for="PasswordConfirm">密码确认：</label>
                        <input type="password" id="PasswordConfirm" jformtype="password" jformNotAllowNull="true"/>
                    </div>
                        
                    <div>
                        <label for="StudentID">学生学号：</label>
                        <input type="text" id="StudentID" jformNotAllowNull="true"/>
                    </div>
                    
                    <div>
                        <label for="Name">真实姓名：</label>
                        <input type="text" id="Name" jformNotAllowNull="true"/>
                    </div>
                    
                    <div class="SelectMenu">
                        <div class="Sex">
                        <label for="Sex">性别：</label>
                        <select id="Sex">
                                <option value="男">男</option>
                                <option value="女">女</option>
                        </select>
                    </div>
                       
                    
                    <div class="InTimeYear">
                        <label for="InTimeYear">入学年份:</label>
                        <select id="InTimeYear">
                                <option value="2013">2013</option>
                                <option value="2012">2012</option>
                                <option value="2011">2011</option>
                                <option value="2010">2010</option>
                                <option value="2009">2009</option>
                        </select>
                    </div>
                    </div>
                    
                    
                    
                    <div>
                        <label for="School">所在院校：</label>
                        <input type="text" id="School" jformNotAllowNull="true"/>
                    </div>
                    
                    <div>
                        <label for="College">学院：</label>
                        <select id="College">
                            <option value="农学院">农学院</option>
                            <option value="资源环境学院">资源环境学院</option>
                            <option value="生命科学学院">生命科学学院</option>
                            <option value="经济管理学院">经济管理学院</option>
                            <option value="工程学院">工程学院</option>
                            <option value="动物科学学院">动物科学学院</option>
                            <option value="兽医学院">兽医学院</option>
                            <option value="园艺学院">园艺学院</option>
                            <option value="食品学院">食品学院</option>
                            <option value="林学院">林学院</option>
                            <option value="人文与法学学院">人文与法学学院</option>
                            <option value="理学院">理学院</option>
                            <option value="信息学院">信息学院</option>
                            <option value="艺术学院">艺术学院</option>
                            <option value="外国语学院">外国语学院</option>
                            <option value="水利与土木工程">水利与土木工程</option>
                            <option value="公共管理学院">公共管理学院</option>
                        </select>
                    </div>
                    
                     <div>
                        <label for="Major">专业：</label>
                        <input type="text" id="Major" jformNotAllowNull="true" />
                    </div>
                    
                    <div>
                        <label for="Mail">邮箱：</label>
                        <input type="text" id="Mail" jformtype="email" jformNotAllowNull="true" />
                    </div>
                    
                     <div class="submit">
                         <input type="button" id="btn" value="提交注册单"  />
                      </div>
              
                    </div>
                  </form> 
            </fieldset>   
        </div>
            
    </div>
    


    <div class="foot">
        <div class="container_16">
            <div class="copyright grid_6">
                Copyright @2013 poweredBy One Tributary Team
            </div>
        </div>
       
    </div>

</body>
</html>
