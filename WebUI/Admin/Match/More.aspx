<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master.Master" AutoEventWireup="true" CodeBehind="More.aspx.cs" Inherits="ResearchManagementSystem.Admin.Match.More" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <link href="../css/list.css" rel="stylesheet" />
    <link href="../css/Form.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">
    <div class="FormContent">
          <div class="FormTitle">
              <h3>"<%=Match.MatchName%>"的信息</h3>
          </div>
          <input type="hidden" id="Id" name="Id" value="<%=Match.ID %>"/>
        <div class="layout">
              <div class="Form" id="matchInfo">
                <div>
                  <label for="MatchName">比赛名称：</label> <%=Match.MatchName %>
                </div>
                <div>
                  <label for="DeadLine">比赛结束时间：</label> 
                    <%=Match.DeadLine.Year %>-<%=Match.DeadLine.Month %>-<%=Match.DeadLine.Day %>
                </div>
                <div>
                  <label for="DeclarantDeadLine">申报截止时间：</label> 
                    <%=Match.DeclarantDeadLine.Year %>-<%=Match.DeclarantDeadLine.Month %>-<%=Match.DeclarantDeadLine.Day %>
                </div>
            
                <div>
                  <label for="Status">比赛状态：</label> <%=Match.Status %>
                </div>
                 <div >
                  <label for="College">学院：</label> <%=Match.College %>
                </div>
                <div class="">
                  <label for="Rule" class="textarea">说明：</label>
                   <div id="Rule" class="text" style="border:solid 1px; width:600px;height:300px; overflow:scroll"><%=Match.Rule %></div> 
                </div>
                <div class="">
                  <label for="ScoreIntro" class="textarea">评分细则：</label>
                   <div id="ScoreIntro" class="text" style="border:solid 1px; width:600px;height:300px;overflow:scroll"> <%=Match.ScoreIntro %></div>
                </div>
                </div>
        </div>
       </div>

     <div class="ListContent">
        <div class="ListTitle">
                <h3>评委信息</h3>
        </div>

         <div class="layout">
             <table class="LxcTable">
                    <tr>  
                        <th>姓名</th>
                        <th>职称    </th>
                        <th>学历    </th>
                        <th>研究方向</th>
                        <th>学院    </th>
                        <th>电话    </th>
                        <th>操作    </th>
                    </tr>
                    <%for(int i=0;i<Judges.Count;i++){ %>
                    <tr>
                        <td><%=Judges[i].RealName %></td>
                        <td><%=Judges[i].Title %></td>
                        <td><%=Judges[i].Background %></td>
                        <td><%=Judges[i].Research %></td>
                        <td><%=Judges[i].College %></td>
                        <td><%=Judges[i].Phone %></td>
                        <td>
                            <input type="button" value="详细" onclick="location.href='../Judge/More.aspx?judge=<%=Judges[i].Id%>    '"/>
                        </td>
                    </tr>
                    <%} %>
                  </table>
              </div>
                <div>
                    <input type="button" style="margin: 15px 30px;" class="lxcButton" value="修改" onclick="location.href='Edit.aspx?edit=<%=Match.ID%>    '" />
                </div>
                  
     </div>
       
               
    

    
</asp:Content>
