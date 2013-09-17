<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Master.Master" AutoEventWireup="true" CodeBehind="DeclarantInfo.aspx.cs" Inherits="ResearchManagementSystem.Web.CupProjectModel.CupInfoEdit.DeclarantInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/leftrightside.css" rel="stylesheet" />
     <link href="../../css/styles/JForm.css" rel="stylesheet" />
    <link href="../../css/projectinfo.css" rel="stylesheet" />
    <script src="../../js/scripts/RDatePicker.js"></script>
    <script src="../../js/scripts/JForm.js"></script>

    <script>
        $(function () {
            RUIKit.RDatePicker.pickBirthday.init("BirthDay");
            RUIKit.RDatePicker.pickBirthday.setBirthday('BirthDay', '<%=Declarant.Birthday.Year %>', '<%=Declarant.Birthday.Month %>', '<%=Declarant.Birthday.Day %>');

            <% if (CupModellist[0].Statu == "提交" || isDeadLine(Match.DeclarantDeadLine))
               {   %>
            var inputs = $('input');
            var selects = $('select');
            $.each(inputs, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            $.each(selects, function (i, elem) {
                $(elem).attr("disabled", "disabled");
            });
            <%}%>
            

            $('#SchoolSystme').val('<%=Declarant.SchoolSystme %>')

            $('#myFormId').jFormInit({
                triggerTargetId: "btn" //负责触发检查表单的默认按钮
            });

            $("#btn").bind("click", function () {
                if ($("#myFormId").jFormIsOk()) {
                    var s = "";
                    var birthday = RUIKit.RDatePicker.pickBirthday.getBirthday("BirthDay");
                    s = $.ajax({
                        type: "POST",
                        url: "AjaxAction.ashx",
                        data: {
                            dowhat: "DeclarantEdit",
                            BirthDay: birthday,
                            BackGround: $("#BackGround").val(),
                            SchoolSystme: $("#SchoolSystme").val(),
                            PaperTitle: $("#PaperTitle").val(),
                            TopAddress: $("#TopAddress").val(),
                            TopPostalCode: $("#TopPostalCode").val(),
                            TopPhone: $("#TopPhone").val(),
                            Address: $("#Address").val(),
                            PostalCode: $("#PostalCode").val(),
                            Phone: $("#Phone").val(),
                            ProjectID: '<%=ProjectID %>',
                        },
                        async: false
                    }).responseText;
                    if (s == "success") {
                        alert("操作成功！");
                        window.location.href = "../CupInfoCreate/DeclarantInfo.aspx?ProjectID=<%=ProjectID %>";
                    } else {
                        alert("操作失败！");
                    }
                }
            })
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="leftside">
        <div class="ProjectStatu">当前作品状态</div>
        <label style="margin:15px; color:#f00;"><%=CupModellist[0].Statu %></label>    
        <h3 style="margin:10px; " class="ProjectInfo">项目信息填写</h3>

       <ul>
            <li><a href="../CupInfoCreate/DeclarantInfo.aspx?ProjectID=<%=ProjectID %>">申报人信息</a></li>
           <li><a href="../CupInfoCreate/TeamMemberInfo.aspx?ProjectID=<%=ProjectID %>">团队成员信息</a></li>
           <li><a href="../CupInfoCreate/WorkInfo.aspx?ProjectID=<%=ProjectID %>">作品信息</a></li>
           <li><a href="../CupInfoCreate/RecommendInfo.aspx?ProjectID=<%=ProjectID %>">推荐人信息</a></li>
           <li><a href="../CupInfoCreate/UploadPaper.aspx?ProjectID=<%=ProjectID %>">上传论文文档</a></li>
           <li><a href="../CupInfoCreate/Remark.aspx?ProjectID=<%=ProjectID %>">备注</a></li>
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
                <input type="text" id="Name" style="background-color:#EBEBE4; " readonly="true" value="<%=Studentlist[0].Name %>"/>
                <label class="Remark" for="Name">必须实名</label>
            </div>

            <div>
                <label for="Sex">性别：</label>
                <input type="text" id="Sex"  readonly="true" style="background-color:#EBEBE4; " value="<%=Studentlist[0].Sex %>"/>
            </div>

             <div>
                <label for="StudentID">学号：</label>
                <input type="text" id="StudentID"  readonly="true" style="background-color:#EBEBE4; " value="<%=Studentlist[0].StudentID %>"/>
            </div>

            <div>
                <label for="BirthDay">生日：</label>
                <span id="BirthDay" style="width:250px; display:inline-block;"></span>
                <label class="Remark" for="BirthDay">如：1987-01-01</label>
            </div>

            <div>
                <label for="School">学校：</label>
                <input type="text" id="School" style="background-color:#EBEBE4; " value="<%=Studentlist[0].School %>" readonly="true"/>
    
            </div>

            <div>
                <label for="College">学院：</label>
                <input type="text" id="College" style="background-color:#EBEBE4; " value="<%=Studentlist[0].College %>" readonly="true"/>

            </div>

            <div>
                <label for="Major">专业：</label>
                <input type="text" id="Major" style="background-color:#EBEBE4; " value="<%=Studentlist[0].Major %>" readonly="true"/>
           
            </div>

            <div>
                <label for="BackGround">学历：</label>
                <input type="text" id="BackGround"  value="<%=Declarant.BackGround %>"/>
                <label class="Remark" for="BackGround">如：本科</label>             
            </div>


            <div>
                <label for="InTimeYear">入学年份：</label>
                <input type="text" id="InTimeYear" style="background-color:#EBEBE4; " value="<%=Studentlist[0].InTimeYear %>" readonly="true"/>
            </div>

            <div>
                <label for="SchoolSystme">学制：</label>
                <select id="SchoolSystme" class="InputSelect">
                    <option value="4年">4年</option>
                    <option value="2年">2年</option>
                    <option value="3年">3年</option>
                    <option value="5年">5年</option>
                </select>
                <label class="Remark" for="SchoolSystme">如：4年</label>
            </div>
            
            <div>
                <label for="PaperTitle">毕业论文题目：</label>
                <input type="text" id="PaperTitle" value="<%=Declarant.PaperTitle %>"/>
                <label class="Remark" for="PaperTitle">可空</label>
            </div>


            <div>
                <label for="TopAddress">常住通讯地址：</label>
                <input type="text" id="TopAddress" value="<%=Declarant.TopAddress %>"/>
                <label class="Remark" for="TopAddress">请填写便于假期联系的地址</label>
            </div>

            <div>
                <label for="TopPostalCode">邮政编码：</label>
                <input type="text" id="TopPostalCode" value="<%=Declarant.TopPostalCode %>" jformtype="postalcode"/>
                <label class="Remark" for="TopPostalCode">如：100191</label>
              
            </div>

            <div>
                <label for="TopPhone">住宅电话：</label>
                <input type="text" id="TopPhone" value="<%=Declarant.TopPhone %>" jformtype="homephone"/>
                <label class="Remark" for="TopPhone">8位</label>
             
            </div>

            <div>
                <label for="Address">通讯地址：</label>
                <input type="text" id="Address" value="<%=Declarant.Address %>"/>
                <label class="Remark" for="Address">请填写学校住宿地址</label>
            </div>


            <div>
                <label for="PostalCode">邮政编码：</label>
                <input type="text" id="PostalCode" value="<%=Declarant.PostalCode %>" jformtype="postalcode"/>
                <label class="Remark" for="PostalCode">如：100191</label>
            
            </div>

            <div>
                <label for="Phone">单位电话：</label>
                <input type="text" id="Phone" value="<%=Declarant.Phone %>" jformtype="mobile"/>
                <label class="Remark" for="Phone">11位</label>
             
            </div>

            <div>
                <input type="button" id="btn" value="保存" style="width:70px; height:25px; margin-left:135px;"/>
            </div>
                  
        </div>
          </form>
    </div>




</asp:Content>
