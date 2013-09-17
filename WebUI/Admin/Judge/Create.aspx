<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="ResearchManagementSystem.Admin.Judge.Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <link href="../css/Form.css" rel="stylesheet" />
    <script>
        $(function () {
            <%if(LoginRole.Name=="ScauAdmin"){%>
            $('#College').prepend("<option value='校级'>校级</option>");
            $('#College').val("校级");
            <%}else if(LoginRole.Name=="CollegeAdmin"){%>
            $('#College option').remove();
            $('#College').prepend("<option value='<%=LoginAdmin.College%>'><%=LoginAdmin.College%></option>");
            $('#College').attr("disabled", "disabled");
            <%}%>
        });


        function Submit() {
            var RoleId = $("#RoleId").val();
            var UserName = $("#UserName").val();
            var Password = $("#Password").val();
            var ConfirmPassword = $("#ConfirmPassword").val();
            var Name = $("#Name").val();
            var Address = $("#Address").val();
            var WorkUnits = $("#WorkUnits").val();
            var Title = $("#Title").val();
            var Background = $("#Background").val();
            var Research = $("#Research").val();
            var JobId = $("#JobId").val();
            var CampusId = $("#CampusId").val();
            var College = $("#College").val();
            var Mail = $("#Mail").val();
            var Phone = $("#Phone").val();
            var Sex = $("#Sex").val();
            var Enable = $("#Enable").val();

            if (UserName == "") {
                alert("用户名不为空");
                return;
            }
            if (Password == "") {
                alert("密码不为空");
                return;
            }
            if (ConfirmPassword == "") {
                alert("确认密码不为空");
                return;
            }
            if (Password != ConfirmPassword) {
                alert("两次输入密码不同");
                return;
            }
            if (Address == "") {
                alert("地址不为空");
                return;
            }
            if (WorkUnits == "") {
                alert("公作单位不为空");
                return;
            }
            if (Title == "") {
                alert("职称不为空");
                return;
            }
            if (Background == "") {
                alert("学历不为空");
                return;
            }
            if (Research == "") {
                alert("研究方向不为空");
                return;
            }
            if (JobId == "") {
                alert("工号不为空");
                return;
            }
            if (CampusId == "") {
                alert("校园卡号不为空");
                return;
            }
            if (College == "") {
                alert("学院不为空");
                return;
            }
            if (Mail == "") {
                alert("邮箱不为空");
                return;
            }
            if (Phone == "") {
                alert("电话不为空");
                return;
            }


            var s = "";

            s = $.ajax({
                type: "POST",
                url: "AjaxAction.ashx",
                data: {
                    dowhat: "add",
                    RoleId:RoleId,
                    UserName:UserName,
                    Password:Password,
                    Name :Name,
                    Address :Address,  
                    WorkUnits :WorkUnits,
                    Title:Title,
                    Background:Background,
                    Research:Research ,
                    JobId:JobId,
                    CampusId :CampusId,
                    College:College,
                    Mail :Mail,
                    Phone: Phone,
                    Sex: Sex,
                    Enable: Enable
                },
                async: false
            }).responseText;
            if (s == "SUCCESS") {
                alert("创建成功");
                location.href = "Default.aspx";
            } else if (s == "FAILD") {
                alert("创建失败");
            } else if (s == "EXIST") {
                alert("用户名已经存在");
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">
     <div class="FormContent">
          <div class="FormTitle">
              <h3>添加评委</h3>
          </div>

            <%if(Roles.Count>0){ %>
            <input type="hidden" id="RoleId" name="RoleId" value="<%=Roles[0].ID %>"/>
            <%}else{ %>
            <input type="hidden" id="Hidden1" name="RoleId" value="0"/>
            <%} %>


          <div class="layout">
              <fieldset>
                 <legend>填写信息（* 为必填信息）</legend>
                  <div class="Form">
                   <div>
                     <label for="UserName">账号 * :</label> <input type="text" id="UserName" name="UserName" />
                   </div>
                   <div>
                    <label for="Password">密码 * :</label> <input type="password" id="Password" name=
                    "Password" />
                  </div>
                  <div>
                    <label for="ConfirmPassword">确认密码 * :</label> <input type="password" id="ConfirmPassword" name=
                    "ConfirmPassword" />
                  </div>
                  <div>
                    <label for="Name">姓名 * :</label> <input type="text" id="Name" name="Name" />
                  </div>
                  <div>
                    <label for="Address">地址 * :</label> <input type="text" id="Address" name="Address" />
                  </div>
                  <div>
                    <label for="WorkUnits">工作单位 * :</label> <input type="text" id="WorkUnits" name="WorkUnits" />
                  </div>
                  <div>
                    <label for="Title">职称 * :</label> <input type="text" id="Title" name="Title" />
                  </div>
                   <div>
                    <label for="Background">学历 * :</label> <input type="text" id="Background" name="Background" />
                  </div>
                   <div>
                    <label for="Research">研究方向 * :</label> <input type="text" id="Research" name="Research" />
                  </div>
                     <div>
                    <label for="JobId">工号 * :</label> <input type="text" id="JobId" name="JobId" />
                  </div>
                  <div>
                    <label for="CampusId">校园卡号 * :</label> <input type="text" id="CampusId" name="CampusId" />
                  </div>
                   <div>
                    <label for="College">学院 * :</label> 
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
                 <label for="Mail">邮箱 * :</label> <input type="text" id="Mail" name="Mail" />
               </div>
                  <div>
                 <label for="Phone">电话 * :</label> <input type="text" id="Phone" name="Phone" />
               </div>
               <div>
                 <label for="Sex">性别:</label> <select id="Sex" name="Sex">
                   <option value="男">
                     男
                   </option>
                   <option value="女">
                     女
                   </option>
                 </select>
               </div>
               <div>
               <label for="Enable">启用 :</label> <select id="Enable" name="Enable">
                 <option value="false">
                   否
                 </option>
                 <option value="true">
                   是
                 </option>
               </select>
             </div>
                      <div><input type="button" class="lxcButton" value="提交" onclick="Submit()" /></div>
                
             </div>
           </fieldset>

          </div>
       </div>
    
    
</asp:Content>
