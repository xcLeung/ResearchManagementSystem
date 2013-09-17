<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ResearchManagementSystem.Admin.Match.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <link href="../css/Form.css" rel="stylesheet" />
    <script src="../xheditor-1.2.1/jquery/jquery-1.4.4.min.js"></script>
    <script src="../xheditor-1.2.1/xheditor-1.2.1.min.js"></script>
    <script src="../xheditor-1.2.1/xheditor_lang/zh-cn.js"></script>
    <script>
        $(function () {
            <%if(LoginRole.Name=="CollegeAdmin"){%>
            $("#College option").remove();
            $("#College").prepend("<option value='<%=LoginAdmin.College%>'><%=LoginAdmin.College%></option>").attr("disabled", "disabled");
            <%}%>


            $('#College').val('<%=Match.College%>');
            $('#Status').val('<%=Match.Status%>');
        });

            function Submit() {
             

                var Id = $("#Id").val();
                var MatchName = $("#MatchName").val();
                var DeadLine = $("#DeadLineYear").val() + "-" + $("#DeadLineMonth").val() + "-" + $("#DeadLineDay").val();
                var Status = $("#Status").val();
                var Level = $("#Level").val();
                var DeclarantDeadLine = $("#DeclarantDeadLineYear").val() + "-" + $("#DeclarantDeadLineMonth").val() + "-" + $("#DeclarantDeadLineDay").val();
                var MatchModel = $("#MatchModel").val();
                var ProjectId = $("#ProjectId").val();
                var College = $("#College").val();
                var Rule = $("#Rule").val();
                var ScoreIntro = $("#ScoreIntro").val();

                var s = "";

                s = $.ajax({
                    type: "POST",
                    url: "AjaxAction.ashx",
                    data: {
                        dowhat: "update",
                        Id:Id,
                        MatchName: MatchName,
                        DeadLine: DeadLine,
                        Status: Status,
                        Level: Level,
                        DeclarantDeadLine: DeclarantDeadLine,
                        MatchModel: MatchModel,
                        ProjectId: ProjectId,
                        College: College,
                        Rule: Rule,
                        ScoreIntro: ScoreIntro,
                        Judge: GetJudge()
                    },
                    async: false
                }).responseText;
                if (s == "SUCCESS") {
                    alert("修改成功");
                    location.href = "Default.aspx";
                } else if (s == "FAILD") {
                    alert("修改失败");
                }
            }
            ///选择日期
            function pickDate(year, month, day) {
                var yearSelected = year.selectedIndex;
                var monthSelected = month.selectedIndex;
                day.options.length = 0;
                if (month.options[monthSelected].value == 2) {
                    if (year.options[yearSelected].value % 4 != 0) {
                        for (var i = 1; i < 29; ++i) {
                            day.options.add(new Option(i, i));
                        }
                    }
                    else {
                        for (var i = 1; i < 30; ++i) {
                            day.options.add(new Option(i, i));
                        }
                    }
                }
                else if (month.options[monthSelected].value < 8) {
                    if (month.options[monthSelected].value % 2 == 0) {
                        for (var i = 1; i < 31; ++i) {
                            day.options.add(new Option(i, i));
                        }
                    }
                    else if (month.options[monthSelected].value % 2 == 1) {
                        for (var i = 1; i < 32; ++i) {
                            day.options.add(new Option(i, i));
                        }
                    }
                } else if (month.options[monthSelected].value >= 8) {
                    if (month.options[monthSelected].value % 2 == 0) {
                        for (var i = 1; i < 32; ++i) {
                            day.options.add(new Option(i, i));
                        }
                    }
                    else if (month.options[monthSelected].value % 2 == 1) {
                        for (var i = 1; i < 31; ++i) {
                            day.options.add(new Option(i, i));
                        }
                    }
                }
            }

            ///评委选择
            function Delete(select1,select2) {
                for (var i = select1.options.length - 1; i >= 0; --i)
                {
                    if (select1.options[i].selected)
                    {
                        select2.options.add(new Option(select1.options[i].text, select1.options[i].value));
                        select1.options.remove(i);
                    }
                }
            }

            function GetJudge() {
                var s = "";
                if (SelectedJudges.options.length > 0) {
                    s += SelectedJudges.options[0].value;
                    for (var i = 1; i < SelectedJudges.options.length; i++) {
                        s += "," + SelectedJudges.options[i].value;
                    }
                }
                return s;
            }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">
     <div class="FormContent">
          <div class="FormTitle">
              <h3>修改比赛</h3>
          </div>

        <div class="layout">
            <input type="hidden" id="Id" name="Id" value="<%=Match.ID %>"/>
            <fieldset>
           <legend>修改比赛：<%=Match.MatchName%>的信息</legend>
                <div class="Form MatchForm">
             <div>
               <label for="MatchName">比赛名称 * :</label> <input type="text" id="MatchName" name="MatchName" value="<%=Match.MatchName %>" />
             </div>
               <div>
                <label for="MatchModel">比赛模型 * :</label> <select id="MatchModel" name="MatchModel" >
                   <option value="<%=MatchModel.Id %>"><%=convertMatchModel(MatchModel.Name)  %></option>
                   <%for(int i=0;i<MatchModels.Count;i++){
                         if(MatchModels[i].Id!=MatchModel.Id){ %>
                   <option value="<%=MatchModels[i].Id %>"><%=MatchModels[i].Name %></option>
                   <%    } 
                     }%>
              </select>
             </div>
             <div>
               <label for="DeadLine">比赛结束时间 * :</label> 
                 <select id="DeadLineYear" name="DeadLineYear" class="DatePick" onchange="pickDate(DeadLineYear,DeadLineMonth,DeadLineDay)" >
                     <option ><%=Match.DeadLine.Year %> </option>
                           <%for(int i=0;i<6;++i){ %>
                           <option ><%=DateTime.Now.Year+i %> </option>
                           <%} %>
                  </select>
                  <select id="DeadLineMonth" name="DeadLineMonth" class="DatePick" onchange="pickDate(DeadLineYear,DeadLineMonth,DeadLineDay)">
                      <option ><%=Match.DeadLine.Month %> </option>
                      <%for(int i=1;i<13;++i){ %>
                      <option ><%=i %> </option>
                      <%} %>
                  </select>
                  <select id="DeadLineDay" class="DatePick" name="DeadLineDay" >
                      <option ><%=Match.DeadLine.Day %> </option>
                      <%for(int i=1;i<32;++i){ %>
                      <option ><%=i %> </option>
                      <%} %>
                  </select>
              </div>
              <div>
                <label for="DeclarantDeadLine">申报截止时间 * :</label> 
                  <select id="DeclarantDeadLineYear" class="DatePick" name="DeclarantDeadLineYear" onchange="pickDate(DeclarantDeadLineYear,DeclarantDeadLineMonth,DeclarantDeadLineDay)">
                      <option><%=Match.DeclarantDeadLine.Year %></option>
                      <%for(int i=0;i<6;++i){ %>
                      <option ><%=DateTime.Now.Year+i %> </option>
                      <%} %>
                  </select>
                  <select id="DeclarantDeadLineMonth" class="DatePick" name="DeclarantDeadLineMonth" onchange="pickDate(DeclarantDeadLineYear,DeclarantDeadLineMonth,DeclarantDeadLineDay)">
                      <option><%=Match.DeclarantDeadLine.Month %></option>
                      <%for(int i=1;i<13;++i){ %>
                      <option ><%=i %> </option>
                      <%} %>
                  </select>
                  <select id="DeclarantDeadLineDay" class="DatePick" name="DeclarantDeadLineDay">
                      <option><%=Match.DeclarantDeadLine.Day %></option>
                             <%for(int i=1;i<32;++i){ %>
                             <option ><%=i %> </option>
                             <%} %>
                   </select>
               </div>    
               <div>
                 <label for="Status">比赛状态：</label> 
                   <select id="Status">
                       <option value="未结束">未结束</option>
                       <option value="结束">结束</option>
                   </select>
               </div>
               <div >
                 <label for="College">学院 * :</label> 
                   <select id="College">
                                   <option value="校级">校级</option>
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
               <div  class="clearfix">
                 <label for="Rule" class="textarea">说明 * :</label> 
                    <div class="editor"><textarea id="Rule" name="Rule" class="xheditor {tools:'Bold,Italic, Underline,FontSize,FontColor,|,Cut,Copy,Paste,|,Align,Outdent,Indent',width:'600',height:'300'}">
                     <%=Match.Rule %></textarea></div>
               </div>
               <div class="clearfix">
                 <label for="ScoreIntro" class="textarea">评分细则 * :</label> 
                    <div class="editor"><textarea id="ScoreIntro" name="ScoreIntro" class="xheditor {tools:'Bold,Italic,Underline,FontSize,FontColor,|,Cut,Copy,Paste,|,Align,Outdent,Indent',width:'600',height:'300'}">
                     <%=Match.ScoreIntro %></textarea></div>
               </div>
               <div>
                   <label for="SelectedJudges">已选择评委：</label>
                   <select id="SelectedJudges" name="SelectedJudges" class="JudgePick" size="10" multiple="multiple">
                       <%for(int i=0;i<SelectedJudges.Count;i++){ %>
                       <option value="<%=SelectedJudges[i].Id %>"><%=SelectedJudges[i].RealName %></option>
                       <%} %>
                   </select>
                   <input type="button" value="删除选择评委" onclick="Delete(SelectedJudges, Judges)"/>
                </div>   
                 <div>  
                   <label for="SelectedJudges">未选择评委：</label>
                   <select id="Judges" name="Judges" class="JudgePick" size="10" multiple="multiple">
                       <%for(int i=0;i<Judges.Count;i++){ %>
                       <option value="<%=Judges[i].Id %>"><%=Judges[i].RealName %></option>
                       <%} %>
                   </select>
                   <input type="button" value="添加选择评委" onclick="Delete(Judges, SelectedJudges)"/>
               </div>
               <div>
                   <input type="button" value="提交" class="lxcButton" onclick="Submit()" />
               </div>
            </div>
          </fieldset>
        </div>
    </div>
    
    
</asp:Content>
