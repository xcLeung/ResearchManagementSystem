<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="DeclarantInfo.aspx.cs" Inherits="ResearchManagementSystem.Web.InnovationProjectModel.InnovationProjectModelCreate.DeclarantInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/leftrightside.css" rel="stylesheet" />
     <link href="../../css/styles/JForm.css" rel="stylesheet" />
    <link href="../../css/projectinfo.css" rel="stylesheet" />
    <script src="../../js/scripts/JForm.js"></script>

    <script>
        $(document).ready(function () {
            <% if (Projects[0].Statu == "提交" || isDeadLine(Match.DeclarantDeadLine))
               {   %>
            var inputs = $('input');
            var selects = $('select');
            var textareas = $('textarea');
            $.each(inputs, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            $.each(selects, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            $.each(textareas, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            <%}%>


            $('#myFormId').jFormInit({
                triggerTargetId: "btn" //负责触发检查表单的默认按钮
            });


            $("#btn").bind("click", function () {
                if ($("#myFormId").jFormIsOk()) {
                    var s = "";
                    s = $.ajax({
                        type: "POST",
                        url: "AjaxAction.ashx",
                        data: {
                            dowhat: "DeclarantCreate",
                            Phone: $("#Phone").val(),
                            ProjectID: '<%=ProjectID %>',
                            Experience: $("#Experience").val(),
                        },
                        async: false
                    }).responseText;
                    if (s == "success") {
                        alert("操作成功！");
                        window.location.href = "../InnovationProjectModelEdit/DeclarantInfo.aspx?ProjectID=<%=ProjectID %>";
                    } else {
                        alert("操作失败！");
                    }
                }
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="leftside">
       <div class="ProjectStatu">当前作品状态</div>
        <label style="margin:15px; color:#f00;"><%=Projects[0].Statu %></label>    
        <h3 class="ProjectInfo" style="margin:10px;">项目信息填写</h3>
        
   
  

      <ul>
           <li><a href="DeclarantInfo.aspx?ProjectID=<%=ProjectID %>">申报人信息</a></li>
           <li><a href="TeamMemberInfo.aspx?ProjectID=<%=ProjectID %>">团队成员信息</a></li>
           <li><a href="WorkInfo.aspx?ProjectID=<%=ProjectID %>">作品信息</a></li>
           <li><a href="RecommendInfo.aspx?ProjectID=<%=ProjectID %>">推荐人信息</a></li>
           <li><a href="Remark.aspx?ProjectID=<%=ProjectID %>">备注</a></li>
       </ul>




    </div>

    <div id="rightside">
        <div class="TitleHead">
            <a class="Picture" href="javascript:history.back(-1)"><img src="../../img/arrow-left.png" " /></a>
            <h2 class="Title">申报人信息</h2>
            <hr />
            <br />
        </div>


        <form class="JForm" id="myFormId">
            <div class="layout">
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
                    <input type="text" id="School" style="background-color:#EBEBE4; " value="<%=Student.School %>" readonly="true"/>
                
                </div>
                
                <div>
                    <label for="College">学院：</label>
                    <input type="text" id="College" style="background-color:#EBEBE4; " value="<%=Student.College %>" readonly="true"/>
                </div>
                
                <div>
                    <label for="Major">专业：</label>
                    <input type="text" id="Major" style="background-color:#EBEBE4; " value="<%=Student.Major %>" readonly="true"/>
                
                </div>


                <div>
                    <label for="InTimeYear">入学年份：</label>
                    <input type="text" id="InTimeYear" style="background-color:#EBEBE4; " value="<%=Student.InTimeYear %>" readonly="true"/>
                </div>

                 <div>
                      <label for="Phone">电话：</label>
                      <input type="text" id="Phone" jformtype="mobile"/>
                      <label class="Remark" for="Phone">11位</label>
                  </div>


                <div>
                    <label for="Experience" class="Textlabel">个人经历简介：</label>
                    <textarea name="Experience" id="Experience" class="text" style="margin-left:140px;"></textarea>
                    <label class="TextlabelRemark" for="Experience">200字以内</label>
                </div>

                <div>
                     <input type="button" id="btn" value="保存" style="width:70px; height:25px; margin-left:135px;" />
                 </div>
            </div>
        </form>
    </div>

</asp:Content>
