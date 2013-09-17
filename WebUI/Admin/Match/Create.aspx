<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="ResearchManagementSystem.Admin.Match.Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <link href="../css/Form.css" rel="stylesheet" />
    <script src="../xheditor-1.2.1/jquery/jquery-1.4.4.min.js"></script>
    <script src="../xheditor-1.2.1/xheditor-1.2.1.min.js"></script>
    <script src="../xheditor-1.2.1/xheditor_lang/zh-cn.js"></script>
    <script>
        $(document).ready(function () {
            <%if(LoginRole.Name=="CollegeAdmin"){%>
            $("#College option").remove();
            $("#College").prepend("<option value='<%=LoginAdmin.College %>'><%=LoginAdmin.College %></option>").attr("disabled", "disabled");
            <%}%>
        });


        function Submit() {
            var MatchName = $("#MatchName").val();
            var DeadLine = $("#DeadLineYear").val() + "-" + $("#DeadLineMonth").val() + "-" + $("#DeadLineDay").val();
            var Status = $("#Status").val();
            var Level = $("#Level").val();
            var DeclarantDeadLine = $("#DeclarantDeadLineYear").val()+"-"+$("#DeclarantDeadLineMonth").val()+"-"+$("#DeclarantDeadLineDay").val();
            var MatchModel = $("#MatchModel").val();
            var ProjectId = $("#ProjectId").val();
            var College = $("#College").val();
            var Rule = $("#Rule").val();
            var ScoreIntro = $("#ScoreIntro").val();
            var Judge = getJudge();
            var s = "";

            s = $.ajax({
                type: "POST",
                url: "AjaxAction.ashx",
                data: {
                    dowhat: "add",
                    MatchName :MatchName,
                    DeadLine :DeadLine ,
                    Status :Status ,
                    Level :Level,
                    DeclarantDeadLine :DeclarantDeadLine,
                    MatchModel :MatchModel,
                    ProjectId :ProjectId,
                    College :College,
                    Rule :Rule,
                    ScoreIntro: ScoreIntro,
                    Judge:Judge
                },
                async: false
            }).responseText;
            if (s == "SUCCESS") {
                alert("创建成功");
                location.href = "Default.aspx";
            } else if (s == "FAILD") {
                alert("创建失败");
            }
        }

        function getJudge() {
            var Judges = $("#Judge").val();
            var Judge = "";
            if (Judges != null) {
                if (Judges.length > 0) {
                    Judge += Judges[0];
                    for (var i = 1; i < Judges.length; i++) {
                        Judge += "," + Judges[i];
                    }
                }
            }
            return Judge;
        }
        ///选择日期
        function pickDate(year,month,day)
        {
            var yearSelected = year.selectedIndex;
            var monthSelected = month.selectedIndex;
            day.options.length = 0;
            if (month.options[monthSelected].value == 2) {
                if (year.options[yearSelected].value % 4 != 0)
                {
                    for (var i = 1; i < 29; ++i) {
                            day.options.add(new Option(i, i));
                    }
                }
                else
                {
                    for (var i = 1; i < 30; ++i) {
                            day.options.add(new Option(i, i));
                    }
                }
            }
            else if(month.options[monthSelected].value<8)
            {
                if(month.options[monthSelected].value % 2 == 0){
                    for (var i = 1; i < 31; ++i) {   
                        day.options.add(new Option(i, i));
                    }
                }
                else if (month.options[monthSelected].value % 2 == 1) {
                    for (var i = 1; i < 32; ++i) {
                        day.options.add(new Option(i, i));
                    }
                }
            } else if (month.options[monthSelected].value >= 8)
            {
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">
    <div class="FormContent">
          <div class="FormTitle">
              <h3>添加比赛</h3>
          </div>

        <div class="layout">
            <fieldset>
                <legend>填写信息（* 为必填信息）</legend>
                <div class="Form MatchForm">
                  <div>
                    <label for="MatchName">比赛名称 * :</label> <input type="text" id="MatchName" name="MatchName" />
                  </div>
                  <div>
                    <label for="MatchModel">比赛模型 * :</label> <select id="MatchModel" name="MatchModel">
                        <%for(int i=0;i<MatchModels.Count;i++){ %>
                        <option value="<%=MatchModels[i].Id %>"><%=convertMatchModel(MatchModels[i].Name)  %></option>
                        <%} %>
                   </select>
                  </div>
                  <div>
                    <label for="DeadLine">比赛结束时间 * :</label> 
                      <select id="DeadLineYear" name="DeadLineYear" class="DatePick" onchange="pickDate(DeadLineYear,DeadLineMonth,DeadLineDay)">
                      <%for(int i=0;i<6;++i){ %>
                      <option ><%=DateTime.Now.Year+i %> </option>
                      <%} %>
                      </select>
                      <select id="DeadLineMonth" name="DeadLineMonth" class="DatePick" onchange="pickDate(DeadLineYear,DeadLineMonth,DeadLineDay)">
                      <%for(int i=1;i<13;++i){ %>
                      <option ><%=i %> </option>
                      <%} %>
                      </select>
                      <select id="DeadLineDay" class="DatePick" name="DeadLineDay">
                      <%for(int i=1;i<32;++i){ %>
                      <option ><%=i %> </option>
                      <%} %>
                      </select>
                  </div>
                  <div>
                    <label for="DeclarantDeadLine">申报截止时间 * :</label> 
                      <select id="DeclarantDeadLineYear" class="DatePick" name="DeclarantDeadLineYear" onchange="pickDate(DeclarantDeadLineYear,DeclarantDeadLineMonth,DeclarantDeadLineDay)">
                      <%for(int i=0;i<6;++i){ %>
                      <option ><%=DateTime.Now.Year+i %> </option>
                      <%} %>
                      </select>
                      <select id="DeclarantDeadLineMonth" class="DatePick" name="DeclarantDeadLineMonth" onchange="pickDate(DeclarantDeadLineYear,DeclarantDeadLineMonth,DeclarantDeadLineDay)">
                      <%for(int i=1;i<13;++i){ %>
                      <option ><%=i %> </option>
                      <%} %>
                      </select>
                      <select id="DeclarantDeadLineDay" class="DatePick" name="DeclarantDeadLineDay">
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
                  <div>
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
                  <div class="clearfix">
                    <label for="Rule">说明 * :</label> 
                     <div class="editor"><textarea id="Rule" name="Rule" class="xheditor {tools:'Bold,Italic, Underline,FontSize,FontColor,|,Cut,Copy,Paste,|,Align,Outdent,Indent',width:'600',height:'300'}">
                    </textarea></div>
                  </div>
                  <div class="clearfix">
                    <label for="ScoreIntro" class="float">评分细则 * :</label> 
                      <div class="editor"><textarea id="ScoreIntro" name="ScoreIntro" class="xheditor {tools:'Bold,Italic,Underline,FontSize,FontColor,|,Cut,Copy,Paste,|,Align,Outdent,Indent',width:'600',height:'300'}">
                    </textarea></div>
                  </div>
                  <div>
                      <label for="Judge">指定评委:(按住Ctrl多选)</label>
                      <select id="Judge" name="Judge" class="JudgePick" size="10" multiple="multiple">
                          <%for(int i=0;i<JudgeInfoModels.Count;i++){ %>
                          <option value="<%=JudgeInfoModels[i].Id %>"><%=JudgeInfoModels[i].RealName %></option>
                          <%} %>
                      </select>
                  </div>
                  <div>
                      <input type="button" class="lxcButton" value="提交" onclick="Submit()" />
                  </div>
                </div>
              </fieldset>
        </div>
    </div>


    
</asp:Content>
