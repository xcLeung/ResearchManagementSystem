<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="MoreProjectInfo.aspx.cs" Inherits="ResearchManagementSystem.Admin.CupProjectModel.MoreProjectInfo" %>

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
            <h2>作品"<%=Project.Name %>"的详细信息</h2>
        </div>
        <div class="Info">
               <div class="TitleDiv">
                   <h3 id="ProjectInfoDiv"><span class="caret"></span>项目信息</h3>
                   
                   <div id="ProjectInfo" class="InfoDiv">
                       <div>
                           <label for="Category">大类：</label>
                           <span style="width:400px;"><%=Project.Category %></span>
                       </div>
                       <div>
                           <label for="DeclarationType">申报种类：</label>
                            <span><%=Project.DeclarationType %></span>
                       </div>
                       <div>
                           <label for="DeclarationDate">申报日期：</label>
                           <span><%=Project.DeclarationDate.ToShortDateString() %></span>
                       </div>
                       <div>
                           <label for="">申报书：</label>
                           <a href="#pdfUrl"><span id="pdfUrl" title="<%=ResolveUrl(Project.PdfUrl!=null?Project.PdfUrl:"") %>" style="display:inline;"><%=Project.PdfUrl!=null?Project.PdfUrl:"" %></span></a>
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
                               <td class="title">生日：</td>
                               <td class="content"><%=Declarant!=null?Declarant.Birthday.ToShortDateString():"无" %></td>
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
                               <td class="title">学历：</td>
                               <td class="content"><%=Declarant!=null?Declarant.BackGround:"无" %></td>
                           </tr>
                           <tr>
                               <td class="title">常住通讯地址：</td>
                               <td class="content"><%=Declarant!=null?Declarant.TopAddress:"无" %></td>
                               <td class="title">邮政编码：</td>
                               <td class="content"><%=Declarant!=null?Declarant.TopPostalCode:"无" %></td>
                               <td class="title">住宅电话：</td>
                               <td class="content"><%=Declarant!=null?Declarant.TopPhone:"无" %></td>
                           </tr>
                           <tr>
                               <td class="title">通讯地址：</td>
                               <td class="content"><%=Declarant!=null?Declarant.Address:"无" %></td>
                               <td class="title">邮政编码：</td>
                               <td class="content"><%=Declarant!=null?Declarant.PostalCode:"无" %></td>
                               <td class="title">单位电话：</td>
                               <td class="content"><%=Declarant!=null?Declarant.Phone:"无" %></td>
                           </tr>
                       </table>


                                                                   
                   </div>
               </div>
               
               
               <div class="TitleDiv" >
                   <h3 id="RecommenderDiv"><span class="caret"></span>推荐人信息</h3>
                   <div id="Recommender" class="InfoDiv">
                       <table class="LxcTable">
                           <thead>
                               <tr>
                                   <th id="Rname">姓名</th>
                                   <th id="Rsex">性别</th>
                                   <th id="Rage">年龄</th>
                                   <th id="Rtitle">职称</th>
                                   <th id="Rworkunits">工作单位</th>
                                   <th id="Raddress">通讯地址</th>
                                   <th id="Rpostal">邮政编码</th>
                                   <th id="Rphone">电话</th>
                                   <th id="Rhomephone">住宅电话</th>
                               </tr>
                           </thead>
                           <% for (int i = 0; i < Recommender.Count;i++ ){ %>
                           <tr>
                               <td><%=Recommender[i].Name %></td>
                               <td><%=Recommender[i].Sex %></td>
                               <td><%=Recommender[i].Age %></td>
                               <td><%=Recommender[i].Title %></td>
                               <td><%=Recommender[i].WorkUnits %></td>
                               <td><%=Recommender[i].Address %></td>
                               <td><%=Recommender[i].PostalCode %></td>
                               <td><%=Recommender[i].Phone %></td>
                               <td><%=Recommender[i].HomePhone %></td>
                           </tr>
                           <%} %>
                           
                       </table>
                   </div>
               </div>
               
               <div class="TitleDiv" >
                   <h3 id="TeamMemberDiv"><span class="caret"></span>团队成员信息</h3>
                   <div id="TeamMember" class="InfoDiv">
                       <table class="LxcTable">
                           <thead>
                              <tr>
                                  <th id="Tname">姓名</th>
                                  <th id="Tsex">性别</th>
                                  <th id="Tage">年龄</th>
                                  <th id="Tbackground">学历</th>
                                  <th id="Tworkunits">所在单位</th>
                              </tr>
                            </thead>
                           <% for (int i = 0; i < TeamMember.Count; i++)
                              { %>
                              <tr>
                                  <td><%=TeamMember[i].Name%></td>
                                  <td><%=TeamMember[i].Sex%></td>
                                  <td><%=TeamMember[i].Age%></td>
                                  <td><%=TeamMember[i].BackGround%></td>
                                  <td><%=TeamMember[i].WorkUnit%></td>
                              </tr>  
                           <% }%>
                       </table>
                   </div>
               </div>
               
               <div class="TitleDiv" >
                   <h3 id="WorkInfoDiv"><span class="caret"></span>作品信息</h3>
                   <div id="WorkInfo" class="InfoDiv">
                       <%if(Project.Category=="自然科学类学术论文"){ %>
                       <div>
                           <label for="SecondCategories">小类(二级分类)：</label>
                           <span><%=WorkInfo!=null?WorkInfo.SecondCategories:"无" %></span>           
                       </div>
                       <div>
                           <label for="ThirdCategories">三级分类：</label>
                           <span><%=WorkInfo!=null?WorkInfo.ThirdCategories:"无" %></span>
                       </div>
                       <div>
                           <label for="FourthCategories">四级分类：</label>
                           <span><%=WorkInfo!=null?WorkInfo.FourthCategories:"无" %></span>
                       </div>   
                       <div> 
                           <label for="Purpose" class="Textlabel">作品撰写的目的和基本思路：</label>
                           <textarea name="Purpose" class="text" readonly="true" ><%=WorkInfo!=null?WorkInfo.Purpose:"无" %></textarea>
                       </div>
                       <div> 
                            <label for="Features" class="Textlabel">作品的科学性、先进性及独特之处：</label>
                            <textarea name="Features" class="text" readonly="true"><%=WorkInfo!=null?WorkInfo.Features:"无" %></textarea>
                        </div>
                       
                        <div> 
                            <label for="ApplyValue" class="Textlabel">作品的实际应用价值和现实意义：</label>
                            <textarea name="ApplyValue" class="text" readonly="true"><%=WorkInfo!=null?WorkInfo.ApplyValue:"无" %></textarea>
                        </div>
                       
                        <div> 
                            <label for="PaperDigest" class="Textlabel">作品摘要：</label>
                            <textarea name="PaperDigest" class="text" readonly="true"><%=WorkInfo!=null?WorkInfo.PaperDigest:"无" %></textarea>
                        </div>
                        
                       <%}else if(Project.Category=="哲学社会科学类社会调查报告和学术论文"){ %>
                         <div>
                             <label for="Categories">作品所属领域：</label>
                                <span><%=SurveyInfo!=null?SurveyInfo.Categories:"无" %></span>
                         </div>

                          <div> 
                              <label for="PurposeS" class="Textlabel">作品撰写的目的和基本思路：</label>
                              <textarea name="PurposeS" class="text" readonly="true"><%=SurveyInfo!=null?SurveyInfo.Purpose:"无" %></textarea>
                          </div>
                          
                          <div> 
                              <label for="FeaturesS" class="Textlabel">作品的科学性、先进性及独特之处：</label>
                              <textarea name="FeaturesS" class="text" readonly="true"><%=SurveyInfo!=null?SurveyInfo.Features:"无" %></textarea>
                          </div>
                          
                          <div> 
                              <label for="ApplyValueS" class="Textlabel">作品的实际应用价值和现实意义：</label>
                              <textarea name="ApplyValueS" class="text" readonly="true"><%=SurveyInfo!=null?SurveyInfo.ApplyValue:"无" %></textarea>
                          </div>
                          
                          <div> 
                              <label for="PaperDigestS" class="Textlabel">作品摘要：</label>
                              <textarea name="PaperDigestS" class="text" readonly="true"><%=SurveyInfo!=null?SurveyInfo.PaperDigest:"无" %></textarea>
                          </div>
                       <%}else if(Project.Category=="科技发明制作"){ %>
                           <div>
                               <label for="CategoriesI">作品分类：</label>
                               <span><%=InventionInfo!=null?InventionInfo.Categories:"无" %></span>
                           </div>
                            <div> 
                                <label for="PurposeI" class="Textlabel">作品设计、发明的目的和基本思路，创新点，技术关键和主要技术指标：</label>
                                <textarea name="PurposeI" class="text" readonly="true"><%=InventionInfo!=null?InventionInfo.Purpose:"无" %></textarea>
                            </div>
                            
                            <div> 
                                <label for="FeaturesI" class="Textlabel">作品的科学性先进性：</label>
                                <textarea name="FeaturesI" class="text" readonly="true"><%=InventionInfo!=null?InventionInfo.Features:"无" %></textarea>
                            </div>
                                       <div>
                                <label for="ApplyValueI" class="Textlabel">使用说明及该作品的技术特点和优势，提供该作品的市场分析和经济效益预测：</label>
                                <textarea name="ApplyValueI" class="text" readonly="true"><%=InventionInfo!=null?InventionInfo.ApplyValue:"无" %></textarea>
                            </div>
                                       <div> 
                                <label for="WorkStatu" class="Textlabel">作品所处阶段：</label>
                                <textarea name="WorkStatu" class="text" readonly="true"><%=InventionInfo!=null?InventionInfo.WorkStatu:"无" %></textarea>
                            </div>
                       <%} %>
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
