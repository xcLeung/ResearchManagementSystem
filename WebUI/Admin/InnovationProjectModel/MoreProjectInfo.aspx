<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="MoreProjectInfo.aspx.cs" Inherits="ResearchManagementSystem.Admin.InnovationProjectModel.MoreProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <link href="../css/MoreProjectInfo.css" rel="stylesheet" />
    <script>
        $(function () {
            var Slide = function () {
                $(this).siblings(".InfoDiv").slideToggle(500);
            }
            $("#ProjectInfoDiv").bind("click", Slide);
            $("#DeclarantDiv").bind("click", Slide);
            $("#RecommenderDiv").bind("click", Slide);
            $("#TeamMemberDiv").bind("click", Slide);
            $("#WorkInfoDiv").bind("click", Slide);
            $("#CheckInfoDiv").bind("click", Slide);

            $(".InfoDiv").css("display", "none");
            $('#pdfUrl').bind("click", function (e) {
                e.stopPropagation();
                window.open($(this).attr("title"));
            });
            $('#paperDoc').bind("click", function (e) {
                e.stopPropagation();
                window.open($(this).attr("title"));
            });

            $("#btn").bind("click", function () {
                var s = "";
                s = $.ajax({
                    type: "POST",
                    url: "AjaxAction.ashx",
                    data: {
                        dowhat: "CheckRecrod",
                        ProjectID: '<%=ProjectID %>',
                        MatchModelID: '<%=Match.MatchModel %>',
                        Pass: $('#Pass').val(),
                        Reason: $('#Reason').val(),
                    },
                    async: false
                }).responseText;
                if (s == "success") {
                    alert("操作成功！");
                    window.location.href = "Default.aspx?match=<%=MatchID %>";
                } else {
                    alert("操作失败！");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">
    <div class="layout">
        <div class="Title">
            <h2>作品"<%=Innovation.Name %>"的详细信息</h2>
        </div>

        <div class="Info">
            <div class="TitleDiv">
                   <h3 id="ProjectInfoDiv"><span class="caret"></span>项目信息</h3>
                   
                   <div id="ProjectInfo" class="InfoDiv">
                       <div>
                           <label for="DeclarationType">申报种类：</label>
                            <span style="width:400px;"><%=Innovation.DeclarationType %></span>
                       </div>
                       <div>
                           <label for="DeclarationDate">申报日期：</label>
                           <span><%=Innovation.DeclarationDate.ToShortDateString() %></span>
                       </div>
                       <div>
                           <label for="">任务书：</label>
                           <a href="#pdfUrl"><span id="pdfUrl" title="<%=ResolveUrl(Innovation.PdfUrl!=null?Innovation.PdfUrl:"") %>" style="display:inline;"><%=Innovation.PdfUrl!=null?Innovation.PdfUrl:"" %></span></a>
                       </div>
                        <div>
                           <label for="">申报书：</label>
                           <a href="#paperDoc"><span id="paperDoc" title="<%=ResolveUrl(Innovation.ProjectDoc!=null?Innovation.ProjectDoc:"") %>" style="display:inline;"><%=Innovation.ProjectDoc!=null?Innovation.ProjectDoc:"" %></span></a>
                       </div>
                    </div>
             </div>

            <div class="TitleDiv" >
                   <h3 id="DeclarantDiv"><span class="caret"></span>申报人信息</h3>
                   <div id="Declarant" class="InfoDiv">
                       <table class="DeclarantTable">
                           <tr>
                               <td class="title">姓名：</td>
                               <td class="content"><%=Student.Name %></td>
                               <td class="title">性别：</td>
                               <td class="content"><%=Student.Sex %></td>
                               <td class="title">学号：</td>
                               <td class="content"><%=Student.StudentID %></td>
                               <td class="title">单位电话：</td>
                               <td class="content"><%=Declarant!=null?Declarant.Phone:"无" %></td>
                           </tr>
                           <tr>
                               <td class="title">学校：</td>
                               <td class="content"><%=Student.School %></td>
                               <td class="title">入学年份：</td>
                               <td class="content"><%=Student.InTimeYear %></td>
                               <td class="title">学院：</td>
                               <td class="content"><%=Student.College %></td>
                               <td class="title">专业：</td>
                               <td class="content"><%=Student.Major %></td>
                           </tr>
                           <tr>
                               <td class="title">邮箱：</td>
                               <td class="content"><%=Student.Mail %></td>
                           </tr>
                           <tr>
                                    <td class="title">个人经历：</td>
                                    <td colspan="8" ><textarea readonly="readonly"><%=Declarant!=null?Declarant.Experience:"无" %></textarea></td>
                           </tr>
                       </table>                                                               
                   </div>
               </div>

                <div class="TitleDiv" >
                   <h3 id="RecommenderDiv"><span class="caret"></span>推荐人信息</h3>
                   <div id="Recommender" class="InfoDiv">
                       <%for (int i = 0; i < Tutors.Count;i++ ){ %>
                        <table class="DeclarantTable" id="Tutor">
                               <tr>
                                   <td class="title">姓名：</td>
                                   <td class="content"><%=Tutors[i].Name%></td>
                                   <td class="title">年龄：</td>
                                   <td class="content"><%=Tutors[i].Age %></td>
                                   <td class="title">职称：</td>
                                   <td class="content"><%=Tutors[i].Work %></td>
                                   <td class="title">研究方向：</td>
                                   <td class="content"><%=Tutors[i].Reasearch %></td>
                               </tr>
                                <tr>
                                    <td class="title">个人经历：</td>
                                    <td colspan="8" ><textarea readonly="readonly"><%=Tutors[i].Achieves!=null?Tutors[i].Achieves:"无" %></textarea></td>
                                </tr>
                           
                                   
                       </table>
                       <%} %>

                       
                   </div>
                </div>


            <div class="TitleDiv" >
                   <h3 id="TeamMemberDiv"><span class="caret"></span>团队成员信息</h3>
                   <div id="TeamMember" class="InfoDiv">
                       <%for(int i=0;i<TeamMembers.Count;i++){ %>
                            <table class="DeclarantTable" id="TeamMemberTable" style="border-bottom:1px dashed #CCC; padding-bottom:20px; margin-bottom:20px;">
                                <tr>
                                    <td class="title">姓名：</td>
                                    <td class="content"><%=TeamMembers[i].Name%></td>
                                    <td class="title">学号：</td>
                                    <td class="content"><%=TeamMembers[i].StudentID%></td>
                                    <td class="title">电话：</td>
                                    <td class="content"><%=TeamMembers[i].Phone%></td>
                                    <td class="title">邮箱：</td>
                                    <td class="content"><%=TeamMembers[i].Mail%></td>
                                </tr>
                                <tr>
                                    <td class="title">入学年份：</td>
                                    <td class="content"><%=TeamMembers[i].InTimeYear%></td>
                                    <td class="title">学院：</td>
                                    <td class="content"><%=TeamMembers[i].College%></td>
                                    <td class="title">专业：</td>
                                    <td class="content"><%=TeamMembers[i].Major%></td>
                                </tr>
                                <tr>
                                    <td class="title">个人经历：</td>
                                    <td colspan="8" ><textarea readonly="readonly"><%=TeamMembers[i].Experience!=null?TeamMembers[i].Experience:"无" %></textarea></td>
                                </tr>
                            </table>
                       <%} %>
                    </div>
            </div>


             <div class="TitleDiv" >
                   <h3 id="WorkInfoDiv"><span class="caret"></span>作品信息</h3>
                   <div id="WorkInfo" class="InfoDiv">
                       <div>
                           <label for="InterimReport">中期报告：</label>
                           <span><%=Innovation.InterimReport!=null?Innovation.InterimReport:"无" %></span>           
                       </div>
                       <div>
                           <label for="SubmitAchievement">提交成果：</label>
                           <span><%=Innovation.SubmitAchievement %></span>
                       </div>
                       <div> 
                           <label for="Features" class="Textlabel">项目特色：</label>
                            <textarea name="Features" class="text" readonly="true" ><%=WorkInfo.Features!=null?WorkInfo.Features:"无" %></textarea>
                       </div>
                       <div> 
                           <label for="Expection" class="Textlabel">项目预期成果：</label>
                            <textarea name="Expection" class="text" readonly="true" ><%=WorkInfo.Expection!=null?WorkInfo.Expection:"无" %></textarea>
                       </div>
                       <div> 
                           <label for="Budget" class="Textlabel">经费预算：</label>
                            <textarea name="Budget" class="text" readonly="true" ><%=WorkInfo.Budget!=null?WorkInfo.Budget:"无" %></textarea>
                       </div>
                    </div>
            </div>

             <div class="TitleDiv">
                <h3 id="CheckInfoDiv"><span class="caret"></span>审核信息填写</h3>
                <div id="CheckInfo" class="InfoDiv">
                    <div>
                        <label for="Reason" class="Textlabel">原因：</label>
                        <textarea id="Reason"></textarea>
                    </div>
                    <div>
                        <select id="Pass">
                            <option value="初审通过">通过</option>
                            <option value="初审失败">否决</option>
                        </select>
                    </div>
                    <input type="button" id="btn" value="保存" class="lxcButton"  />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
